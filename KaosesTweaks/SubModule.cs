using HarmonyLib;
using KaosesTweaks.Behaviors;
using KaosesTweaks.Event;
using KaosesTweaks.Models;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
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
            MethodInvoker.GetHandler(AccessTools.Method(typeof(RomanceCampaignBehavior), "RemoveUnneededPersuasionAttempts"));
        private Harmony _harmony;
        /* Another chance at marriage */




        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            ConfigLoader.LoadConfig();
            if (Statics.IsHarmonyLoaded())
            {
                Ux.MessageInfo("Loaded: " + Statics._settings.ModDisplayName);
            }
            else
            {
                Ux.MessageError(Statics.prePrend + " : Will not function properly with out Harmony ");
            }
        }

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            try
            {
                _harmony ??= new Harmony("kaoses.tweaks.patch");
                _harmony.PatchAll();
                //var harmony = new Harmony("kaoses.tweaks.patch");
                //harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                //Handle exceptions
                Logging.Lm(Statics.prePrend + "Error with harmony patch");
                Logging.Lm(Statics.prePrend + ex.ToString());
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



        }

        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            base.OnGameStart(game, gameStarter);
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


                /* Another chance at marriage */
                LastAttempts = new Dictionary<Hero, CampaignTime>();
                /* Another chance at marriage */

                campaignGameStarter.CampaignBehaviors.Add(new AnotherChanceBehavior());
                //_harmony ??= new Harmony("com.nexusmods.KaosesTweaks");
                //_harmony.PatchAll();
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
            _harmony?.UnpatchAll("kaoses.tweaks.patch");
            /* Another chance at marriage */
        }







    }
}