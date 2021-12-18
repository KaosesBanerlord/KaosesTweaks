using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Conversation.Persuasion;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

/* Another chance at marriage */
namespace KaosesTweaks.Patches
{
    public class GetPersuasionTasksForCourtship
    {

        // These are Harmony patches because player lines cannot be removed or replaced
        [HarmonyPatch(typeof(RomanceCampaignBehavior))]
        internal class Patches
        {
            private static readonly FastInvokeHandler GetReservationsStage1Handler =
                MethodInvoker.GetHandler(AccessTools.Method(typeof(RomanceCampaignBehavior), "GetPersuasionTasksForCourtshipStage1"));

            private static readonly FastInvokeHandler GetReservationsStage2Handler =
                MethodInvoker.GetHandler(AccessTools.Method(typeof(RomanceCampaignBehavior), "GetPersuasionTasksForCourtshipStage2"));

            // Checks the 1 day cool down
            [HarmonyPrefix]
            [HarmonyPatch("conversation_player_can_open_courtship_on_condition")]
            public static bool Prefix1(ref bool __result)
            {
                CampaignTime lastAttempt = SubModule.LastAttempts.TryGetValue(Hero.OneToOneConversationHero, out CampaignTime value)
                    ? value
                    : CampaignTime.DaysFromNow(-1f);

                if (Statics._settings.AnotherChanceAtMarriageDebug)
                {
                    IM.MessageDebug($"Another Chance At Marriage:can_open_courtship  CampaignTime.Now.ToDays < lastAttempt.ToDays = {CampaignTime.Now.ToDays < lastAttempt.ToDays}");
                }
                if (CampaignTime.Now.ToDays < lastAttempt.ToDays)
                {
                    __result = false;
                    return false;
                }

                return true;
            }

            // Replaces 1st persuasion stage setup code in order to reset progress
            [HarmonyPrefix]
            [HarmonyPatch("conversation_start_courtship_persuasion_pt1_on_consequence")]
            private static bool Prefix2(ref RomanceCampaignBehavior __instance, ref List<PersuasionTask> ____allReservations, ref float ____maximumScoreCap, float ____successValue, float ____failValue, float ____criticalSuccessValue, float ____criticalFailValue)
            {
                if (Romance.GetRomanticLevel(Hero.MainHero, Hero.OneToOneConversationHero) == Romance.RomanceLevelEnum.MatchMadeByFamily)
                    ChangeRomanticStateAction.Apply(Hero.MainHero, Hero.OneToOneConversationHero, Romance.RomanceLevelEnum.CourtshipStarted);
                Hero wooer = Hero.MainHero.MapFaction.Leader;
                if (Hero.MainHero.MapFaction == Hero.OneToOneConversationHero.MapFaction)
                    wooer = Hero.MainHero;
                ____allReservations = (List<PersuasionTask>)GetReservationsStage1Handler(__instance, new object[] { Hero.OneToOneConversationHero, wooer });
                ____maximumScoreCap = ____allReservations.Count() * 1f;
                SubModule.RemoveUnneededPersuasionAttemptsHandler(__instance, new object[0]);
                ConversationManager.StartPersuasion(____maximumScoreCap, ____successValue, ____failValue, ____criticalSuccessValue, ____criticalFailValue, 0, PersuasionDifficulty.Medium);
                return false;
            }

            // Replaces 2nd persuasion stage setup code in order to reset progress
            [HarmonyPrefix]
            [HarmonyPatch("conversation_continue_courtship_stage_2_on_consequence")]
            private static bool Prefix3(ref RomanceCampaignBehavior __instance, ref List<PersuasionTask> ____allReservations, ref float ____maximumScoreCap, float ____successValue, float ____failValue, float ____criticalSuccessValue, float ____criticalFailValue)
            {
                Hero wooer = Hero.MainHero.MapFaction.Leader;
                if (Hero.MainHero.MapFaction == Hero.OneToOneConversationHero.MapFaction)
                    wooer = Hero.MainHero;
                ____allReservations = (List<PersuasionTask>)GetReservationsStage2Handler(__instance, new object[] { Hero.OneToOneConversationHero, wooer });
                ____maximumScoreCap = ____allReservations.Count() * 1f;
                SubModule.RemoveUnneededPersuasionAttemptsHandler(__instance, new object[0]);
                ConversationManager.StartPersuasion(____maximumScoreCap, ____successValue, ____failValue, ____criticalSuccessValue, ____criticalFailValue, 0, PersuasionDifficulty.Medium);
                return false;
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.AnotherChanceAtMarriageEnabled;
    }
}
/* Another chance at marriage */