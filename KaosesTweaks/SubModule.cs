using HarmonyLib;
using KaosesCommon;
using KaosesCommon.Utils;
using KaosesTweaks.Behaviors;
using KaosesTweaks.Event;
using KaosesTweaks.Models;
using KaosesTweaks.Objects;
using KaosesTweaks.Settings;
using KaosesTweaks.Tweaks;
using SandBox;
using Serilog;
using StoryMode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using Module = TaleWorlds.MountAndBlade.Module;

namespace KaosesTweaks
{
    /// <summary>
    /// KaosesTweaks Mod
    /// </summary>
    public class SubModule : MBSubModuleBase
    {
        public const bool UsesHarmony = true;
        public const string ModuleId = "KaosesTweaks";
        public const string modulePath = @"..\\..\\Modules\\" + ModuleId + "\\";
        public const string HarmonyId = ModuleId + ".harmony";
        private Harmony? _harmony;

        /* Another chance at marriage */
        public static Dictionary<Hero, CampaignTime>? LastAttempts;
        public static readonly FastInvokeHandler RemoveUnneededPersuasionAttemptsHandler =
        MethodInvoker.GetHandler(AccessTools.Method(typeof(RomanceCampaignBehavior), "RemoveUnneededPersuasionAttempts"));
        /* Another chance at marriage */

        /// <summary>
        /// Called just before the main menu first appears, helpful if your mod depends on other things being set up during the initial load
        /// </summary>
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            new Init();
            try
            {
                if (UsesHarmony)
                {
                    if (Kaoses.IsHarmonyLoaded())
                    {
                        IM.MessageModLoaded();
                        try
                        {
                            if (_harmony == null)
                            {
                                Harmony.DEBUG = Factory.Settings.IsHarmonyDebug;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                                _harmony = new Harmony(ModuleId);
                                _harmony.PatchAll(Assembly.GetExecutingAssembly());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                            }

                        }
                        catch (Exception ex)
                        {
                            IM.ShowError(ex, Factory.Settings.ModName + " Harmony Error:");
                        }
                    }
                    else { IM.MessageHarmonyLoadError(); }
                }
                else
                {
#pragma warning disable CS0162 // Unreachable code detected
                    IM.MessageModLoaded();
#pragma warning restore CS0162 // Unreachable code detected
                }
            }
            catch (Exception ex)
            {
                IM.ShowError(ex, "initial Loading Error " + Factory.Settings.ModName);
            }

            if (Factory.Settings.DisableIntroVideo)
            {
                AccessTools.DeclaredField(typeof(Module), "_splashScreenPlayed")?.SetValue(Module.CurrentModule, true);
            }

        }

        /// <summary>
        /// Called during the first loading screen of the game, always the first override to be called, this is where you should be doing the bulk of your initial setup
        /// </summary>
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
        }

        /// <summary>
        /// Called once the initialization for a game mode has finished
        /// </summary>
        /// <param name="game"></param>
        public override void OnGameInitializationFinished(Game game)
        {

            base.OnGameInitializationFinished(game);
            Campaign gameType = game.GameType as Campaign;
            if (!(gameType is Campaign))
            {
                return;
            }

            //~ BT PrisonerImprisonmentTweak
            try
            {
                if (Factory.Settings is { } settings && settings.EnableMissingHeroFix && settings.PrisonerImprisonmentTweakEnabled) //
                {
                    CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, delegate
                    {
                        PrisonerImprisonmentTweak.DailyTick();
                    });
                    if (Factory.Settings.Debug)
                    {
                        IM.MessageDebug("Loaded DailyTickEvent PrisonerImprisonmentTweak");
                    }
                }
            }
            catch (Exception ex)
            {
                IM.ShowError(ex, "Game Initialization : Kaoses Tweaks Prisoner Imprisonment Tweak Error");
            }

            //~ KaosesItemTweaks
            try
            {
                if (Factory.Settings.MCMItemModifiers)
                {
                    new KaosesItemTweaks(Items.All);
                    if (Factory.Settings.Debug)
                    {
                        IM.MessageDebug("Loaded KaosesItemTweaks");
                    }
                }
            }
            catch (Exception ex)
            {
                IM.ShowError(ex, "Game Initialization: Kaoses Tweaks Item Tweaks Error");
            }
        }

        /// <summary>
        /// Called immediately upon loading after selecting a game mode (submodule) from the main menu
        /// </summary>
        /// <param name="game"></param>
        /// <param name="gameStarter"></param>
        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {

            base.OnGameStart(game, gameStarter);

            if (game.GameType is Campaign)
            {
                CampaignGameStarter campaignGameStarter = (CampaignGameStarter)gameStarter;

                //~ BT initializing game models
                try
                {
                    ModelsManager _modelsManager = new ModelsManager(campaignGameStarter);
                    _modelsManager.AddGameModels();
                }
                catch (Exception ex)
                {
                    IM.ShowError(ex, "Game Start : Kaoses Tweaks Error initializing game models");
                }

                //~ BT MCMKillingBanditsEnabled
                try
                {
                    if (Factory.Settings.MCMKillingBanditsEnabled)
                    {
                        PlayerBattleEndEventListener playerBattleEndEventListener = new PlayerBattleEndEventListener();
                        CampaignEvents.OnPlayerBattleEndEvent.AddNonSerializedListener(playerBattleEndEventListener, new Action<MapEvent>(playerBattleEndEventListener.IncreaseLocalRelationsAfterBanditFight));
                        if (Factory.Settings.Debug)
                        {
                            IM.MessageDebug("Loaded Killing Bandits raises relationships playerBattleEndEventListener Behavior");
                        }
                    }
                }
                catch (Exception ex)
                {
                    IM.ShowError(ex, "Game Start: Kaoses Tweaks Error initializing Killing Bandits");
                }

                //~ Another Chance At Marriage
                LastAttempts = new Dictionary<Hero, CampaignTime>();
                try
                {
                    /* Another chance at marriage */
                    if (Factory.Settings.AnotherChanceAtMarriageEnabled)
                    {
                        if (Factory.Settings.AnotherChanceAtMarriageDebug)
                        {
                            IM.MessageDebug($"Another Chance At Marriage ENABLED");
                        }
                        campaignGameStarter.CampaignBehaviors.Add(new AnotherChanceBehavior());
                        if (Factory.Settings.Debug)
                        {
                            IM.MessageDebug("Loaded AnotherChanceBehavior Behavior");
                        }
                    }
                    /* Another chance at marriage */
                }
                catch (Exception ex)
                {
                    IM.ShowError(ex, "Game Start:Kaoses Tweaks Error initializing Another chance");
                }

                //~ ChangeSettlementCulture
                try
                {
                    //~BT
                    if (Factory.Settings.EnableCultureChanger)
                    {
                        if (Factory.Settings.Debug)
                        {
                            IM.MessageDebug("Loaded ChangeSettlementCulture Behavior");
                        }
                        campaignGameStarter.AddBehavior(new ChangeSettlementCulture());
                    }
                }
                catch (Exception ex)
                {
                    IM.ShowError(ex, "Game Start: Kaoses Tweaks Error initializing Culture Changer");
                }

                //~ KaosesCraftingCampaignBehaviors
                try
                {
                    if (Factory.Settings.ArrowMultipliersEnabled || Factory.Settings.BoltsMultipliersEnabled
                        || Factory.Settings.ThrownMultiplierEnabled)
                    {
                        if (Factory.Settings.Debug)
                        {
                            IM.MessageDebug("Loaded KaosesCraftingCampaignBehaviors Behavior");
                        }
                        campaignGameStarter.AddBehavior(new CraftingCampaignBehaviors());
                    }
                }
                catch (Exception ex)
                {
                    IM.ShowError(ex, "Game Start: Kaoses Tweaks Error initializing CraftingCampaignBehaviors");
                }

                //~ KaosesWorkshopCampaignBehaviors
                try
                {
                    if (Factory.Settings.EnableWorkshopSellTweak || Factory.Settings.EnableWorkshopBuyTweak
                        || Factory.Settings.KeepWorkshopsOnWarDeclaration || Factory.Settings.KeepWorkshopsOnBankruptcy)
                    {
                        if (Factory.Settings.Debug)
                        {
                            IM.MessageDebug("Loaded KaosesWorkshopCampaignBehaviors Behavior");
                        }
                        campaignGameStarter.AddBehavior(new WorkshopsBehavior());
                    }
                }
                catch (Exception ex)
                {
                    IM.ShowError(ex, "Game Start: Kaoses Tweaks Error initializing WorkshopsCampaignBehavior");
                }
            }
        }

        /// <summary>
        /// Called immediately after loading the selected game mode (submodule) has completed
        /// </summary>
        /// <param name="game"></param>
        public override void BeginGameStart(Game game)
        {
            base.BeginGameStart(game);
        }

        /// <summary>
        /// Called seemingly as loading is ending, not entirely sure of this one
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public override bool DoLoading(Game game)
        {
            //~ PrisonerImprisonmentTweakEnabled
            try
            {
                if (Campaign.Current != null && Config.Instance is { } settings)
                {
                    if (settings.PrisonerImprisonmentTweakEnabled)
                        PrisonerImprisonmentTweak.Apply(Campaign.Current);
                }
            }
            catch (Exception ex)
            {
                IM.ShowError(ex, "Game Loading: Kaoses Tweaks Error initializing PrisonerImprisonmentTweakEnabled");
            }

            //~ DailyTroopExperienceTweakEnabled
            try
            {
                if (Campaign.Current != null && Config.Instance is { } settings)
                {
                    if (settings.DailyTroopExperienceTweakEnabled)
                        DailyTroopExperienceTweak.Apply(Campaign.Current);
                }
            }
            catch (Exception ex)
            {
                IM.ShowError(ex, "Game Loading: Kaoses Tweaks Error initializing DailyTroopExperienceTweakEnabled");
            }

            //~ TweakedConspiracyQuestTimerEnabled
            try
            {
                if (Campaign.Current != null && Config.Instance is { } settings)
                {
                    // 1.5.7.2 - Disable until we understand main quest changes.
                    //if (settings.TweakedConspiracyQuestTimerEnabled)
                    //    BTConspiracyQuestTimerTweak.Apply(Campaign.Current);
                }
            }
            catch (Exception ex)
            {
                IM.ShowError(ex, "Game Loading : Kaoses Tweaks Error initializing TweakedConspiracyQuestTimerEnabled");
            }
            return base.DoLoading(game);
        }

        /// <summary>
        /// Called once any game mode is started
        /// </summary>
        /// <param name="game"></param>
        /// <param name="starterObject"></param>
        public override void OnCampaignStart(Game game, object starterObject)
        {
            base.OnCampaignStart(game, starterObject);
        }

        /// <summary>
        /// Called on exiting out of a mission/campaign
        /// </summary>
        /// <param name="game"></param>
        public override void OnGameEnd(Game game)
        {
            base.OnGameEnd(game);
        }

        /// <summary>
        /// Called only after loading a save
        /// </summary>
        /// <param name="game"></param>
        /// <param name="initializerObject"></param>
        public override void OnGameLoaded(Game game, object initializerObject)
        {
            base.OnGameLoaded(game, initializerObject);
        }

        /// <summary>
        /// Called when starting a new save in the campaign mode specifically
        /// </summary>
        /// <param name="game"></param>
        /// <param name="initializerObject"></param>
        public override void OnNewGameCreated(Game game, object initializerObject)
        {
            base.OnNewGameCreated(game, initializerObject);

            if (Factory.Settings.DisableCharacterIntroVideo)
            {
                _harmony.Patch(AccessTools.Method(typeof(SandBoxGameManager), "OnLoadFinished"), transpiler: new HarmonyMethod(AccessTools.Method(typeof(Patches.PatchGameManager), "Transpiler")));
                _harmony.Patch(AccessTools.Method(typeof(StoryModeGameManager), "OnLoadFinished"), transpiler: new HarmonyMethod(AccessTools.Method(typeof(Patches.PatchGameManager), "Transpiler")));
            }
        }

        /// <summary>
        /// This is called once every frame, you should avoid expensive operations being called directly here and instead do as little work as possible for performance reasons.
        /// </summary>
        /// <param name="dt">The time in milliseconds the frame took to complete</param> 
        public override void OnMissionBehaviorInitialize(Mission mission)
        {

        }


        /// <summary>
        /// Called when exiting Bannerlord entirely
        /// </summary>
        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
        }


        private void AddModels(CampaignGameStarter campaignGameStarter)
        {

        }



        protected override void OnApplicationTick(float dt)
        {
            if (Campaign.Current != null && Config.Instance is { } settings2 && settings2.CampaignSpeed != 4)
            {
                Campaign.Current.SpeedUpMultiplier = settings2.CampaignSpeed;
            }
        }
        //~ BT
    }
}