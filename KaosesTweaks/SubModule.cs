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
using TaleWorlds.MountAndBlade;
using KaosesTweaks.BTTweaks;
using System.Text;
using System.Linq;


/*

/singleplayer _MODULES_*Bannerlord.Harmony*Bannerlord.ButterLib*Bannerlord.MBOptionScreen*Bannerlord.UIExtenderEx*BetterExceptionWindow*Native*SandBoxCore*CustomBattle*Sandbox*StoryMode*$(ModuleName)*_MODULES_


/singleplayer _MODULES_*Bannerlord.Harmony*Bannerlord.ButterLib*Bannerlord.MBOptionScreen*Bannerlord.UIExtenderEx*BetterExceptionWindow*Native*SandBoxCore*CustomBattle*Sandbox*StoryMode*KaosesTweaks*_MODULES_
*/
namespace KaosesTweaks
{
    public class SubModule : MBSubModuleBase
    {

        /* Another chance at marriage */
        public static Dictionary<Hero, CampaignTime> LastAttempts;
        public static readonly FastInvokeHandler RemoveUnneededPersuasionAttemptsHandler =
        HarmonyLib.MethodInvoker.GetHandler(AccessTools.Method(typeof(RomanceCampaignBehavior), "RemoveUnneededPersuasionAttempts"));
        private Harmony? harmonyKT;
        public static bool HasPatched = false;
        /* Another chance at marriage */

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
        }


        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

            try
            {
                ConfigLoader.LoadConfig();
                bool modUsesHarmoney = Statics.UsesHarmony;
                if (modUsesHarmoney)
                {
                    if (Kaoses.IsHarmonyLoaded())
                    {
                        IM.DisplayModLoadedMessage();
                        if (harmonyKT == null)
                        {
                            harmonyKT = new Harmony(Statics.HarmonyId);
                            harmonyKT.PatchAll(Assembly.GetExecutingAssembly());
                        }
                    }
                    else { IM.DisplayModHarmonyErrorMessage(); }
                }
                else { IM.DisplayModLoadedMessage(); }
            }
            catch (Exception ex)
            {
                //Handle exceptions
                IM.MessageError("Error loading initial config: " + ex.ToStringFull());
            }

            
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
                if (Statics._settings is { } settings && (settings.EnableMissingHeroFix && settings.PrisonerImprisonmentTweakEnabled)) //
                {
                    //~ BT
                    try
                    {
                        CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, delegate
                        {
                            PrisonerImprisonmentTweak.DailyTick();
                        });
                        if (Statics._settings.Debug)
                        {
                            IM.MessageDebug("Loaded DailyTickEvent PrisonerImprisonmentTweak");
                        }
                    }
                    catch (Exception ex)
                    {
                        IM.MessageError(ex.ToStringFull());
                        MessageBox.Show($":\n\n{ex.ToStringFull()}");
                    }

                }

                if (gameType != null && Statics._settings.MCMItemModifiers)
                {
                    new KaosesItemTweaks(gameType.Items);
                    if (Statics._settings.Debug)
                    {
                        IM.MessageDebug("Loaded KaosesItemTweaks");
                    }
                }

            }
            catch (Exception ex)
            {
                //Handle exceptions
                IM.MessageError("Error OnGameInitializationFinished "+ ex.ToStringFull());
            }


        }

        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            base.OnGameStart(game, gameStarter);

            if (game.GameType is Campaign)
            {
                CampaignGameStarter campaignGameStarter = (CampaignGameStarter)gameStarter;

                //~ BT
                try
                {
                    AddModels(campaignGameStarter);
                    PlayerBattleEndEventListener playerBattleEndEventListener = new PlayerBattleEndEventListener();
                    CampaignEvents.OnPlayerBattleEndEvent.AddNonSerializedListener(playerBattleEndEventListener, new Action<MapEvent>(playerBattleEndEventListener.IncreaseLocalRelationsAfterBanditFight));
                    if (Statics._settings.Debug)
                    {
                        IM.MessageDebug("Loaded Killing Bandits raises relationships playerBattleEndEventListener Behavior");
                    }
                }
                catch (Exception ex)
                {
                    IM.MessageError("Error OnGameStart: " + ex.ToStringFull());
                    MessageBox.Show($"Error Initialising Culture Changer:\n\n{ex.ToStringFull()}");
                }

                try
                {
                    /* Another chance at marriage */
                    LastAttempts = new Dictionary<Hero, CampaignTime>();
                    /* Another chance at marriage */
                    campaignGameStarter.CampaignBehaviors.Add(new AnotherChanceBehavior());
                    if (Statics._settings.Debug)
                    {
                        IM.MessageDebug("Loaded AnotherChanceBehavior Behavior");
                    }
                    /* Another chance at marriage */
                }
                catch (Exception ex)
                {
                    IM.MessageError("Error OnGameStart: "+ex.ToStringFull());
                    MessageBox.Show($"Error Initialising Culture Changer:\n\n{ex.ToStringFull()}");
                }
                try
                {
                    //~BT
                    if (Statics._settings.EnableCultureChanger)
                    {
                        if (Statics._settings.Debug)
                        {
                            IM.MessageDebug("Loaded ChangeSettlementCulture Behavior");
                        }
                        campaignGameStarter.AddBehavior(new ChangeSettlementCulture());
                    }
                }
                catch (Exception ex)
                {
                    IM.MessageError("Error OnGameStart: "+ex.ToStringFull());
                    MessageBox.Show($"Error Initialising Culture Changer:\n\n{ex.ToStringFull()}");
                }
            }
        }


        public override bool DoLoading(Game game)
        {
            try
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
            }
            catch (Exception ex)
            {
                //Handle exceptions
                IM.MessageError("Error DoLoading : " + ex.ToStringFull());
            }
            return base.DoLoading(game);
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
            //try
            //{
            //    harmonyKT?.UnpatchAll(Statics.HarmonyId);
            //    HasPatched = false;
            //}
            //catch (Exception ex)
            //{
                //Handle exceptions
                //IM.MessageError("Error OnGameEnd harmony un-patch: " + ex.ToStringFull());
            //}
        }

        public override void OnGameEnd(Game game)
        {
            //try
            //{
            //    harmonyKT?.UnpatchAll(Statics.HarmonyId);
            //    HasPatched = false;
            //}
            //catch (Exception ex)
            //{
                //Handle exceptions
                //IM.MessageError("Error OnGameEnd harmony un-patch: " + ex.ToStringFull());
            //}
            
        }


        //~ BT

/*
        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            if (mission == null) return;
            base.OnMissionBehaviourInitialize(mission);
        }*/

        private void AddModels(CampaignGameStarter campaignGameStarter)
        {

            if (campaignGameStarter != null && MCMSettings.Instance is { } settings)
            {
                
                

                if (settings.MCMClanModifiers)
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded Kaoses Clan Model Override");
                    }
                    campaignGameStarter.AddModel(new KaosesClanTierModel());
                }
                if (settings.HideoutBattleTroopLimitTweakEnabled)
                {
/*
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded Kaoses Bandit Density model Model Override");
                    }*/
                    //campaignGameStarter.AddModel(new KaosesBanditDensityModel());
                }
                if (settings.MCMArmy)
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded Kaoses Army Model Override");
                    }
                    campaignGameStarter.AddModel(new KaosesArmyManagementCalculationModel());
                }
                if (settings.MCMBattleRewardModifiers)
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loading Kaoses Battle rewards Model");
                    }
                    campaignGameStarter.AddModel(new KaosesBattleRewardModel());
                }
                if (settings.MCMCharacterDevlopmentModifiers || (Statics._settings.LearningRateEnabled || Statics._settings.LearningLimitEnabled))
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded Kaoses Character Development Model Override");
                    }
                    campaignGameStarter.AddModel(new KaosesCharacterDevelopmentModel());
                }
                if (settings.MCMPregnancyModifiers)
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded Kaoses Pregnancy Model Override");
                    }
                    campaignGameStarter.AddModel(new KaosesPregnancyModel());
                }
                if (settings.MCMSmithingModifiers && !settings.MCMSmithingHarmoneyPatches)
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded Kaoses Smithing Model Override");
                    }
                    campaignGameStarter.AddModel(new KaosesSmithingModel());
                }
                if (settings.PartyFoodConsumptionEnabled)
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded Kaoses party Food Consumption Model Override");
                    }
                    campaignGameStarter.AddModel(new KaosesMobilePartyFoodConsumptionModel());
                }
                if (settings.DifficultyTweakEnabled)
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded BT Difficulty Model Override");
                    }
                    campaignGameStarter.AddModel(new BTDifficultyModel());
                }
                if (settings.SettlementMilitiaEliteSpawnRateBonusEnabled)
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded BT Settlement Militia Model Override");
                    }
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
                        if (settings.Debug)
                        {
                            IM.MessageDebug("Loaded BT Age Model Override");
                        }
                        campaignGameStarter.AddModel(new BTAgeModel());
                    }

                }
                if (settings.SiegeTweaksEnabled)
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded BT Siege Model Override");
                    }
                    campaignGameStarter.AddModel(new BTSiegeEventModel());
                }
                if (settings.MaxWorkshopCountTweakEnabled || settings.WorkshopBuyingCostTweakEnabled || settings.WorkshopEffectivnessEnabled)
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded BT Workshop Model Override");
                    }
                    campaignGameStarter.AddModel(new BTWorkshopModel());
                }
                if (settings.TroopExperienceTweakEnabled || settings.ArenaHeroExperienceMultiplierEnabled || settings.TournamentHeroExperienceMultiplierEnabled)
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded BT ComabatXP Model Override");
                    }
                    campaignGameStarter.AddModel(new BTCombatXpModel());
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
            //IM.MessageDebug("Debug Message: DumpValues");

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

            //bool t = MCMSettings.Instance != null && MCMSettings.Instance.SmithingXpModifiers;
            //IM.MessageDebug("");
            //IM.MessageDebug("");

        }

    }
}