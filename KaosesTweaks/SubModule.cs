using HarmonyLib;
using KaosesTweaks.Behaviors;
using KaosesTweaks.Event;
using KaosesTweaks.Common;
using KaosesTweaks.Models;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using static TaleWorlds.Core.ItemObject;
using KaosesTweaks.BTTweaks;
using System.Text;
using System.Linq;

namespace KaosesTweaks
{
    public class SubModule : MBSubModuleBase
    {

        /* Another chance at marriage */
        public static Dictionary<Hero, CampaignTime> LastAttempts;
        public static readonly FastInvokeHandler RemoveUnneededPersuasionAttemptsHandler =
        HarmonyLib.MethodInvoker.GetHandler(AccessTools.Method(typeof(RomanceCampaignBehavior), "RemoveUnneededPersuasionAttempts"));
        private Harmony _harmony;
        /* Another chance at marriage */


        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            ConfigLoader.LoadConfig();
            bool modUsesHarmoney = Statics.UsesHarmony;
            if (modUsesHarmoney)
            {
                if (Kaoses.IsHarmonyLoaded())
                {
                    IM.DisplayModLoadedMessage();
                }
                else { IM.DisplayModHarmonyErrorMessage(); }
            }
            else { IM.DisplayModLoadedMessage(); }

        }

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
        }



        // Called 4th after choosing (Resume Game, Campaign, Custom Battle) from the main menu.
        public override void OnGameInitializationFinished(Game game)
        {
            base.OnGameInitializationFinished(game);
            Campaign gameType = game.GameType as Campaign;
            if (!(gameType is Campaign))
            {
                return;
            }

            try
            {
                var harmony = new Harmony("kaoses.wages.patch");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                //Handle exceptions
                Logging.Lm("Error with harmony patch");
                Logging.Lm(ex.ToString());
            }
            if (gameType != null && Statics._settings is { } settings && settings.PrisonerImprisonmentTweakEnabled) //(settings.EnableMissingHeroFix && 
            {
                //~ BT
                try
                {
                    CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, delegate
                    {
                        PrisonerImprisonmentTweak.DailyTick();
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($":\n\n{ex.ToStringFull()}");
                }

            }

            if (gameType != null && Statics._settings.MCMItemModifiers)
            {
                new KaosesItemTweaks(gameType.Items);
            }

        }

        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            base.OnGameStart(game, gameStarter);

            if (game.GameType is Campaign)
            {
                CampaignGameStarter campaignGameStarter = (CampaignGameStarter)gameStarter;

#pragma warning disable CS8604 // Possible null reference argument.
                AddModels(campaignGameStarter);
#pragma warning restore CS8604 // Possible null reference argument.


                PlayerBattleEndEventListener playerBattleEndEventListener = new PlayerBattleEndEventListener();
                CampaignEvents.OnPlayerBattleEndEvent.AddNonSerializedListener(playerBattleEndEventListener, new Action<MapEvent>(playerBattleEndEventListener.IncreaseLocalRelationsAfterBanditFight));


                //~ BT

                try
                {
                    /* Another chance at marriage */
                    LastAttempts = new Dictionary<Hero, CampaignTime>();
                    /* Another chance at marriage */
                    campaignGameStarter.CampaignBehaviors.Add(new AnotherChanceBehavior());
                    /* Another chance at marriage */
                    //~BT
                    campaignGameStarter.AddBehavior(new ChangeSettlementCulture());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error Initialising Culture Changer:\n\n{ex.ToStringFull()}");
                }



            }
            DumpValues();
        }


        public override bool DoLoading(Game game)
        {
            if (Campaign.Current != null && MCMSettings.Instance is { } settings)
            {
                if (settings.PrisonerImprisonmentTweakEnabled)
                    PrisonerImprisonmentTweak.Apply(Campaign.Current);
                if (settings.DailyTroopExperienceTweakEnabled)
                    DailyTroopExperienceTweak.Apply(Campaign.Current);
                // 1.5.7.2 - Disable until we understand main quest changes.
                //if (settings.TweakedConspiracyQuestTimerEnabled)
                //    BTConspiracyQuestTimerTweak.Apply(Campaign.Current);
            }
            return base.DoLoading(game);
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
        }

        public override void OnGameEnd(Game game)
        {
            _harmony?.UnpatchAll(Statics.HarmonyId);
        }


        //~ BT

        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            if (mission == null) return;
            base.OnMissionBehaviourInitialize(mission);
        }

        private void AddModels(CampaignGameStarter campaignGameStarter)
        {

            if (campaignGameStarter != null && MCMSettings.Instance is { } settings)
            {
                if (settings.MCMClanModifiers)
                {
                    campaignGameStarter.AddModel(new KaosesClanTierModel());
                }
                if (settings.MCMArmy)
                {
                    campaignGameStarter.AddModel(new KaosesArmyManagementCalculationModel());
                }
                if (settings.MCMBattleRewardModifiers)
                {
                    //campaignGameStarter.AddModel(new KaosesBattleRewardModel());
                }
                if (settings.MCMCharacterDevlopmentModifiers)
                {
                    campaignGameStarter.AddModel(new KaosesCharacterDevelopmentModel());
                }
                if (settings.MCMPregnancyModifiers)
                {
                    campaignGameStarter.AddModel(new KaosesPregnancyModel());
                }
                if (settings.MCMSmithingModifiers && !settings.MCMSmithingHarmoneyPatches)
                {
                    campaignGameStarter.AddModel(new KaosesSmithingModel());
                }
                if (settings.MCMSmithingModifiers)
                {
                    campaignGameStarter.AddModel(new KaosesMobilePartyFoodConsumptionModel());
                }
                if (settings.DifficultyTweakEnabled)
                {
                    campaignGameStarter.AddModel(new BTDifficultyModel());
                }
                if (settings.SettlementMilitiaEliteSpawnRateBonusEnabled)
                {
                    campaignGameStarter.AddModel(new BTSettlementMilitiaModel());
                }
                if (settings.AgeTweaksEnabled)
                {
                    BTAgeModel model = new();
                    List<string> configErrors = model.GetConfigErrors().ToList();

                    if (configErrors.Any())
                    {
                        StringBuilder sb = new();
                        sb.AppendLine("There is a configuration error in the \'Age\' tweaks from Bannerlord Tweaks.");
                        sb.AppendLine("Please check the below errors and fix the age settings in the settings menu:");
                        sb.AppendLine();
                        foreach (var e in configErrors)
                            sb.AppendLine(e);
                        sb.AppendLine();
                        sb.AppendLine("The age tweaks will not be applied until these errors have been resolved.");
                        sb.Append("Note that this is only a warning message and not a crash.");

                        MessageBox.Show(sb.ToString(), "Configuration Error in Bannerlord Tweaks");
                    }
                    else
                    {
                        campaignGameStarter.AddModel(new BTAgeModel());
                    }

                }
                if (settings.SiegeTweaksEnabled)
                {
                    campaignGameStarter.AddModel(new BTSiegeEventModel());
                }
                if (settings.MaxWorkshopCountTweakEnabled || settings.WorkshopBuyingCostTweakEnabled || settings.WorkshopEffectivnessEnabled)
                {
                    campaignGameStarter.AddModel(new BTWorkshopModel());
                }
                if (settings.TroopExperienceTweakEnabled || settings.ArenaHeroExperienceMultiplierEnabled || settings.TournamentHeroExperienceMultiplierEnabled)
                {
                    campaignGameStarter.AddModel(new BTCombatXpModel());
                }
                if (settings.MCMAutoLocks)
                {
                    //campaignGameStarter.AddModel(new KaosesWorkshopModel());
                }
            }
        }



        protected override void OnApplicationTick(float dt)
        {
            if (Campaign.Current != null && MCMSettings.Instance is { } settings2 && settings2.CampaignSpeed != 4)
            {
                Campaign.Current.SpeedUpMultiplier = settings2.CampaignSpeed;
            }
        }
        //~ BT


        public void DumpValues()
        {
            IM.MessageDebug("Debug Message: DumpValues");

/*
            IM.MessageDebug("");

            IM.MessageDebug("GetSkillXpForRefining");
            bool b1 = MCMSettings.Instance is { } settings && settings.SmithingRefiningXpModifiers;
            IM.MessageDebug("Prepare state: " + b1.ToString());

            IM.MessageDebug("GetSkillXpForSmelting");
            bool b2 = MCMSettings.Instance is { } settings2 && settings2.SmithingSmeltingXpModifiers;
            IM.MessageDebug("Prepare state: " + b2.ToString());

            IM.MessageDebug("GetSkillXpForSmithing");
            bool b3 = MCMSettings.Instance is { } settings3 && settings3.SmithingSmithingXpModifiers;
            IM.MessageDebug("Prepare state: " + b3.ToString());

            IM.MessageDebug("");
            IM.MessageDebug("");
            IM.MessageDebug("");
            IM.MessageDebug("");


            IM.MessageDebug("GetEnergyCostForRefining");
            bool b4 = MCMSettings.Instance is { } settings4 && (settings4.SmithingEnergyDisable || settings4.SmithingEnergyRefiningModifiers);
            IM.MessageDebug("Prepare state: " + b4.ToString());

            IM.MessageDebug("GetEnergyCostForSmithing");
            bool b5 = MCMSettings.Instance is { } settings5 && (settings5.SmithingEnergyDisable || settings5.SmithingEnergySmithingModifiers);
            IM.MessageDebug("Prepare state: " + b5.ToString());

            IM.MessageDebug("GetEnergyCostForSmelting");
            bool b6 = MCMSettings.Instance is { } settings6 && (settings6.SmithingEnergyDisable || settings6.SmithingEnergySmeltingModifiers);
            IM.MessageDebug("Prepare state: " + b6.ToString());

*/

            bool t = MCMSettings.Instance != null && MCMSettings.Instance.SmithingXpModifiers;
            IM.MessageDebug("");
            IM.MessageDebug("");

        }

    }
}