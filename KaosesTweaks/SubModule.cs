using HarmonyLib;
using KaosesPartySpeeds.Model;
using KaosesTweaks.Behaviors;
using KaosesTweaks.BTTweaks;
using KaosesTweaks.Common;
using KaosesTweaks.Event;
using KaosesTweaks.Models;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace KaosesTweaks
{
    public class SubModule : MBSubModuleBase
    {
        private Harmony? harmonyKT;

        /* Another chance at marriage */
        public static Dictionary<Hero, CampaignTime> LastAttempts;
        public static readonly FastInvokeHandler RemoveUnneededPersuasionAttemptsHandler =
        MethodInvoker.GetHandler(AccessTools.Method(typeof(RomanceCampaignBehavior), "RemoveUnneededPersuasionAttempts"));
        /* Another chance at marriage */

        /* KaosesPartySpeeds */
        public static Dictionary<MobileParty, CampaignTime> FleeingParties;
        public static Dictionary<MobileParty, int> FleeingHours;
        public static Dictionary<MobileParty, float> FleeingSpeedReduction;
        public static MobileParty FleeingPartyPlayer;
        /* KaosesPartySpeeds */


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
                            Harmony.DEBUG = true;
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
                IM.ShowError("Error loading", "initial config", ex);
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
            //~ KT Party Speeds
            FleeingParties = new Dictionary<MobileParty, CampaignTime>();
            FleeingHours = new Dictionary<MobileParty, int>();
            FleeingSpeedReduction = new Dictionary<MobileParty, float>();
            //~ KT Party Speeds

            //~ BT PrisonerImprisonmentTweak
            try
            {
                if (Statics._settings is { } settings && settings.EnableMissingHeroFix && settings.PrisonerImprisonmentTweakEnabled) //
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
            }
            catch (Exception ex)
            {
                IM.ShowError("Prisoner Imprisonment Tweak Error", " Game Initialization Finished Error", ex);
            }

            //~ KaosesItemTweaks
            try
            {
                if (Statics._settings.MCMItemModifiers)
                {
                    new KaosesItemTweaks(Items.All);
                    if (Statics._settings.Debug)
                    {
                        IM.MessageDebug("Loaded KaosesItemTweaks");
                    }
                }
            }
            catch (Exception ex)
            {
                IM.ShowError("Kaoses Item Tweaks Error", "Game Initialization Finished Error", ex);
            }

        }

        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            base.OnGameStart(game, gameStarter);

            if (game.GameType is Campaign)
            {
                CampaignGameStarter campaignGameStarter = (CampaignGameStarter)gameStarter;

                //~ BT initializing game models
                try
                {
                    AddModels(campaignGameStarter);
                }
                catch (Exception ex)
                {
                    IM.ShowError("Error initializing game models", "Game Start Error", ex);
                }

                //~ BT MCMKillingBanditsEnabled
                try
                {
                    if (Statics._settings.MCMKillingBanditsEnabled)
                    {
                        PlayerBattleEndEventListener playerBattleEndEventListener = new PlayerBattleEndEventListener();
                        CampaignEvents.OnPlayerBattleEndEvent.AddNonSerializedListener(playerBattleEndEventListener, new Action<MapEvent>(playerBattleEndEventListener.IncreaseLocalRelationsAfterBanditFight));
                        if (Statics._settings.Debug)
                        {
                            IM.MessageDebug("Loaded Killing Bandits raises relationships playerBattleEndEventListener Behavior");
                        }
                    }
                }
                catch (Exception ex)
                {
                    IM.ShowError("Error initializing Killing Bandits raises relationships", "Game Start Error", ex);
                }

                //~ Another Chance At Marriage
                LastAttempts = new Dictionary<Hero, CampaignTime>();
                try
                {
                    /* Another chance at marriage */
                    if (Statics._settings.AnotherChanceAtMarriageEnabled)
                    {
                        if (Statics._settings.AnotherChanceAtMarriageDebug)
                        {
                            IM.MessageDebug($"Another Chance At Marriage ENABLED");
                        }
                        campaignGameStarter.CampaignBehaviors.Add(new AnotherChanceBehavior());
                        if (Statics._settings.Debug)
                        {
                            IM.MessageDebug("Loaded AnotherChanceBehavior Behavior");
                        }
                    }
                    /* Another chance at marriage */
                }
                catch (Exception ex)
                {
                    IM.ShowError("Error initializing Another chance at marriage", "Game Start Error", ex);
                }

                //~ ChangeSettlementCulture
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
                }

                //~ KaosesCraftingCampaignBehaviors
                try
                {
                    if (Statics._settings.ArrowMultipliersEnabled || Statics._settings.BoltsMultipliersEnabled
                        || Statics._settings.ThrownMultiplierEnabled)
                    {
                        if (Statics._settings.Debug)
                        {
                            IM.MessageDebug("Loaded KaosesCraftingCampaignBehaviors Behavior");
                        }
                        campaignGameStarter.AddBehavior(new KaosesCraftingCampaignBehaviors());
                    }
                }
                catch (Exception ex)
                {
                    IM.ShowError("Error initializing KaosesCraftingCampaignBehaviors Changer", "Game Start Error", ex);
                }
            }
        }


        public override bool DoLoading(Game game)
        {
            //~ PrisonerImprisonmentTweakEnabled
            try
            {
                if (Campaign.Current != null && MCMSettings.Instance is { } settings)
                {
                    if (settings.PrisonerImprisonmentTweakEnabled)
                        PrisonerImprisonmentTweak.Apply(Campaign.Current);
                }
            }
            catch (Exception ex)
            {
                IM.ShowError("Error initializing PrisonerImprisonmentTweakEnabled tweak calls", "Game Loading Error", ex);
            }

            //~ DailyTroopExperienceTweakEnabled
            try
            {
                if (Campaign.Current != null && MCMSettings.Instance is { } settings)
                {
                    if (settings.DailyTroopExperienceTweakEnabled)
                        DailyTroopExperienceTweak.Apply(Campaign.Current);
                }
            }
            catch (Exception ex)
            {
                IM.ShowError("Error initializing DailyTroopExperienceTweakEnabled tweak calls", "Game Loading Error", ex);
            }

            //~ TweakedConspiracyQuestTimerEnabled
            try
            {
                if (Campaign.Current != null && MCMSettings.Instance is { } settings)
                {
                    // 1.5.7.2 - Disable until we understand main quest changes.
                    //if (settings.TweakedConspiracyQuestTimerEnabled)
                    //    BTConspiracyQuestTimerTweak.Apply(Campaign.Current);
                }
            }
            catch (Exception ex)
            {
                IM.ShowError("Error initializing TweakedConspiracyQuestTimerEnabled tweak calls", "Game Loading Error", ex);
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
                if (settings.KaosesStaticSpeedModifiersEnabled || settings.KaosesDynamicSpeedModifiersEnabled)
                {

                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded Kaoses Party Speed model Model Override");
                    }
                    campaignGameStarter.AddModel(new KaosesPartySpeedCalculatingModel());
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
                if ((settings.PartyWageTweaksEnabled && !settings.PartyWageTweaksHarmonyEnabled) || (settings.KingdomBalanceStrengthEnabled && !settings.KingdomBalanceStrengthHarmonyEnabled))
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded BT Wage model Model Override");
                    }
                    campaignGameStarter.AddModel(new BTDefaultPartyWageModel());
                }
                if (settings.MCMArmy)
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loaded Kaoses Army Model Override");
                    }
                    campaignGameStarter.AddModel(new KaosesArmyManagementCalculationModel());
                }
                if (settings.MCMBattleRewardModifiers && !settings.BattleRewardModifiersPatchOnly)
                {
                    if (settings.Debug)
                    {
                        IM.MessageDebug("Loading Kaoses Battle rewards Model");
                    }
                    campaignGameStarter.AddModel(new KaosesBattleRewardModel());
                }
                if (settings.MCMCharacterDevlopmentModifiers || Statics._settings.LearningRateMultiplier != 1.0  || Statics._settings.LearningLimitEnabled)
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
                        foreach (string? e in configErrors)
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