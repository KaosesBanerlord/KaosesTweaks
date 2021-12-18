using KaosesTweaks.Utils;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core;
using TaleWorlds.Localization;

/* Another chance at marriage */
namespace KaosesTweaks.Behaviors
{
    class AnotherChanceBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(OnSessionLaunched));
        }

        public void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
        {
            AddDialogs(campaignGameStarter);
            if (Statics._settings.AnotherChanceAtMarriageDebug)
            {
                IM.MessageDebug($"Another Chance At Marriage OnSessionLaunched Added Dialogs");
            }
        }

        public override void SyncData(IDataStore dataStore)
        {

        }

        protected void AddDialogs(CampaignGameStarter starter)
        {
            // 1st roll for success at getting 2nd chance
            starter.AddDialogLine("lord_start_courtship_response_jicksaw_another_chance",
                "lord_start_courtship_response",
                "lord_pretalk",
                "{=YHZsHohq}We meet from time to time, as is the custom, to see if we are right for each other. I hope to see you again soon.",
                Another_chance_on_condition,
                Another_chance_success_on_consequence,
                202);

            // If previous dialog condition roll failed, get this response and roll for relation penalty
            starter.AddDialogLine("lord_start_courtship_response_jicksaw_another_chance_rejected",
                "lord_start_courtship_response",
                "lord_pretalk",
                "{=2bKgL66e}{COURTSHIP_DECLINE_REACTION}",
                Another_chance_rejected_on_condition,
                Another_chance_rejected_on_consequence,
                201);
        }


        private static bool Another_chance_on_condition()
        {
            if (Romance.GetRomanticLevel(Hero.MainHero, Hero.OneToOneConversationHero) != Romance.RomanceLevelEnum.FailedInPracticalities &&
                Romance.GetRomanticLevel(Hero.MainHero, Hero.OneToOneConversationHero) != Romance.RomanceLevelEnum.FailedInCompatibility)
                return false;

            float relation = Hero.OneToOneConversationHero.GetRelationWithPlayer();
            if (relation < 0)
                return false;

            int attraction = Campaign.Current.Models.RomanceModel.GetAttractionValuePercentage(Hero.OneToOneConversationHero, Hero.MainHero);
            float chance = Math.Max(0.0f, Math.Min(20.0f + 2 * relation + 0.5f * attraction, 175.0f) / 200.0f);
            float randonNumber = MBRandom.RandomFloat;
            IM.MessageDebug($"attraction = {attraction} \n" +
                            $"relation = {relation}\n" +
                            $"chance = {chance}\n" +
                            $"randonNumber = {randonNumber}\n" +
                            $"pass random check = {randonNumber < chance}"
                            );
            return randonNumber < chance;
        }

        private static void Another_chance_success_on_consequence()
        {
            SubModule.LastAttempts[Hero.OneToOneConversationHero] = CampaignTime.DaysFromNow(Statics._settings.AnotherChanceAtMarriageDaysTillRetry);
            // Go straight to 2nd stage if completed 1st stage successfully before
            Romance.RomanceLevelEnum toLevel = (Romance.GetRomanticLevel(Hero.MainHero, Hero.OneToOneConversationHero) == Romance.RomanceLevelEnum.FailedInPracticalities)
                    ? Romance.RomanceLevelEnum.CoupleDecidedThatTheyAreCompatible
                    : Romance.RomanceLevelEnum.CourtshipStarted;
            ChangeRomanticStateAction.Apply(Hero.MainHero, Hero.OneToOneConversationHero, toLevel);
        }


        // Copied from vanilla
        private static bool Another_chance_rejected_on_condition()
        {
            if (Romance.GetRomanticLevel(Hero.MainHero, Hero.OneToOneConversationHero) == Romance.RomanceLevelEnum.FailedInPracticalities)
            {
                MBTextManager.SetTextVariable("COURTSHIP_DECLINE_REACTION", "{=emLBsWj6}I am terribly sorry. It is practically not possible for us to be married.", false);
                return true;
            }
            if (Romance.GetRomanticLevel(Hero.MainHero, Hero.OneToOneConversationHero) != Romance.RomanceLevelEnum.FailedInCompatibility)
                return false;
            MBTextManager.SetTextVariable("COURTSHIP_DECLINE_REACTION", "{=s7idfhBO}I am terribly sorry. We are not really compatible with each other.", false);
            return true;
        }

        private static void Another_chance_rejected_on_consequence()
        {
            SubModule.LastAttempts[Hero.OneToOneConversationHero] = CampaignTime.DaysFromNow(Statics._settings.AnotherChanceAtMarriageDaysTillRetry);
            float relation = Hero.OneToOneConversationHero.GetRelationWithPlayer();
            // 30% chance at relation loss at 0 relation, 15% at 4, 0% at 15 
            float criticalFailChance = (relation >= 15) ? 0 : 2f / 15f * relation * relation - 4 * relation + 30;
            if (MBRandom.RandomFloat < criticalFailChance)
            {
                InformationManager.DisplayMessage(new InformationMessage("Relation reduced"));
                ChangeRelationAction.ApplyPlayerRelation(Hero.OneToOneConversationHero, -1, true, true);
            }
        }
    }
}
/* Another chance at marriage */