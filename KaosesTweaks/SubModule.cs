using HarmonyLib;
using KaosesTweaks.Behaviors;
using KaosesTweaks.Event;
using KaosesTweaks.Helpers;
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
            try
            {
                _harmony ??= new Harmony(Statics.HarmonyId);
                _harmony.PatchAll();
            }
            catch (Exception ex)
            {
                //Handle exceptions
                Logging.Lm("Error with harmony patch");
                Logging.Lm(ex.ToString());
                MessageBox.Show($"Error Initialising Bannerlord Tweaks:\n\n{ex.ToStringFull()}");
            }
        }



        public override void OnGameInitializationFinished(Game game)
        {
            // Called 4th after choosing (Resume Game, Campaign, Custom Battle) from the main menu.
            base.OnGameInitializationFinished(game);
            Campaign gameType = game.GameType as Campaign;
            if (!(gameType is Campaign))
            {
                return;
            }

            if (gameType != null)
            {
                MBReadOnlyList<ItemObject> ItemsList = gameType.Items;
                new KaosesItemTweaks(gameType.Items);
            }

            //~ BT

/*
            if (Campaign.Current != null && BannerlordTweaksSettings.Instance is { } settings && (settings.EnableMissingHeroFix && settings.PrisonerImprisonmentTweakEnabled))
            {

                try
                {
                    CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, delegate
                    {
                        PrisonerImprisonmentTweak.DailyTick();
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error Initialising Missing Hero Fix:\n\n{ex.ToStringFull()}");
                }
            }*/

            //~ BT

        }

        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            base.OnGameStart(game, gameStarter);

#pragma warning disable CS8604 // Possible null reference argument.
            AddModels(gameStarter: gameStarter as CampaignGameStarter);
#pragma warning restore CS8604 // Possible null reference argument.

            if (game.GameType is Campaign)
            {
                CampaignGameStarter campaignGameStarter = (CampaignGameStarter)gameStarter;
                //campaignGameStarter.LoadGameTexts(BasePath.Name + "Modules/" + Statics.ModuleFolder + "/ModuleData/module_strings.xml");
                campaignGameStarter.AddModel(new KaosesBattleRewardModel());
                campaignGameStarter.AddModel(new KaosesCharacterDevelopmentModel());
                campaignGameStarter.AddModel(new KaosesClanTierModel());
                campaignGameStarter.AddModel(new KaosesPregnancyModel());
                campaignGameStarter.AddModel(new KaosesSmithingModel());
                campaignGameStarter.AddModel(new KaosesWorkshopModel());
                campaignGameStarter.AddModel(new KaosesArmyManagementCalculationModel());
                campaignGameStarter.AddModel(new KaosesMobilePartyFoodConsumptionModel());


                PlayerBattleEndEventListener playerBattleEndEventListener = new PlayerBattleEndEventListener();
                CampaignEvents.OnPlayerBattleEndEvent.AddNonSerializedListener(playerBattleEndEventListener, new Action<MapEvent>(playerBattleEndEventListener.IncreaseLocalRelationsAfterBanditFight));


                //~ BT

                try
                {
                    //campaignGameStarter.AddBehavior(new ChangeSettlementCulture());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error Initialising Culture Changer:\n\n{ex.ToStringFull()}");
                }

                //~BT

                /* Another chance at marriage */
                LastAttempts = new Dictionary<Hero, CampaignTime>();
                /* Another chance at marriage */
                campaignGameStarter.CampaignBehaviors.Add(new AnotherChanceBehavior());
                /* Another chance at marriage */


            }
        }



        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
        }

        public override void OnGameEnd(Game game)
        {

            /* Another chance at marriage */
            _harmony?.UnpatchAll(Statics.HarmonyId);
            /* Another chance at marriage */
        }


        //~ BT

        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            if (mission == null) return;
            base.OnMissionBehaviourInitialize(mission);
        }

        private void AddModels(CampaignGameStarter gameStarter)
        {
/*
            if (gameStarter != null && BannerlordTweaksSettings.Instance is { } settings)
            {
                if (settings.TroopExperienceTweakEnabled || settings.ArenaHeroExperienceMultiplierEnabled || settings.TournamentHeroExperienceMultiplierEnabled)
                    gameStarter.AddModel(new TweakedCombatXpModel());
                if (settings.MaxWorkshopCountTweakEnabled || settings.WorkshopBuyingCostTweakEnabled || settings.WorkshopEffectivnessEnabled)
                    gameStarter.AddModel(new TweakedWorkshopModel());
                if (settings.PartiesLimitTweakEnabled || settings.CompanionLimitTweakEnabled || settings.BalancingPartyLimitTweaksEnabled)
                    gameStarter.AddModel(new TweakedClanTierModel());
                if (settings.SettlementMilitiaEliteSpawnRateBonusEnabled)
                    gameStarter.AddModel(new TweakedSettlementMilitiaModel());
                if (settings.SiegeTweaksEnabled)
                    gameStarter.AddModel(new TweakedSiegeEventModel());
                if (settings.PregnancyTweaksEnabled)
                    gameStarter.AddModel(new TweakedPregnancyModel());
                if (settings.AgeTweaksEnabled)
                {
                    TweakedAgeModel model = new();
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
                        gameStarter.AddModel(new TweakedAgeModel());
                }
                if (settings.AttributeFocusPointTweakEnabled)
                    gameStarter.AddModel(new TweakedCharacterDevelopmentModel());
                if (settings.DifficultyTweakEnabled)
                    gameStarter.AddModel(new TweakedDifficultyModel());
            }*/
        }



        protected override void OnApplicationTick(float dt)
        {
/*
            if (Campaign.Current != null && BannerlordTweaksSettings.Instance is { } settings2 && settings2.CampaignSpeed != 4)
            {
                Campaign.Current.SpeedUpMultiplier = settings2.CampaignSpeed;
            }*/
        }
        //~ BT




    }
}