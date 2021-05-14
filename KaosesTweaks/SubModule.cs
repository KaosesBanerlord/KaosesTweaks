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
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using KaosesTweaks.BTTweaks;
using System.Text;
using System.Linq;


namespace KaosesTweaks
{
    public class SubModule : MBSubModuleBase
    {
        private Harmony? harmonyKT;

        /* Another chance at marriage */
        public static Dictionary<Hero, CampaignTime> LastAttempts;
        public static readonly FastInvokeHandler RemoveUnneededPersuasionAttemptsHandler =
        HarmonyLib.MethodInvoker.GetHandler(AccessTools.Method(typeof(RomanceCampaignBehavior), "RemoveUnneededPersuasionAttempts"));
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
                IM.ShowError("Error loading", "initial config", ex);
                //MessageBox.Show($":\n\n{ex.ToStringFull()}" + "\n\nGameVersion: " + Statics.GameVersion + "\nModVersion: " + Statics.ModVersion);
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
                        IM.ShowError("Imprisonment Tweak Error", "Prisoner Tweak Loading Error", ex);
                        //MessageBox.Show($":\n\n{ex.ToStringFull()}" + "\n\nGameVersion: " + Statics.GameVersion + "\nModVersion: " + Statics.ModVersion);
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
                IM.ShowError("Error initializing one of the game tweaks", "Game Initialization Finished Error", ex);
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
                    IM.ShowError("Error initializing Killing Bandits raises relationships", "Game Start Error", ex);
                    //MessageBox.Show($"Error Initialising Killing Bandits raises relationships:\n\n{ex.ToStringFull()}" + "\n\nGameVersion: " + Statics.GameVersion + "\nModVersion: " + Statics.ModVersion);
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
                    IM.ShowError("Error initializing Another chance at marriage", "Game Start Error", ex);
                    //MessageBox.Show($"Error Initialising Another chance at marriage:\n\n{ex.ToStringFull()}" + "\n\nGameVersion: " + Statics.GameVersion + "\nModVersion: " + Statics.ModVersion);
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
                    IM.ShowError("Error initializing Culture Changer", "Game Start Error", ex);
                    //MessageBox.Show($"Error Initialising Culture Changer:\n\n{ex.ToStringFull()}" + "\n\nGameVersion: " + Statics.GameVersion + "\nModVersion: " + Statics.ModVersion);
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
                IM.ShowError("Error initializing game loading tweak calls", "Game Loading Error", ex);
            }
            return base.DoLoading(game);
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
        }

        public override void OnGameEnd(Game game)
        {

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
                        //MessageBox.Show(sb.ToString(), "Configuration Error in Age Tweaks");
                        IM.ShowError(sb.ToString(), "Configuration Error in Age Tweaks");
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

    }
}