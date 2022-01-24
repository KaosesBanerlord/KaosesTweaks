using Bannerlord.BUTR.Shared.Helpers;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Dropdown;
using MCM.Abstractions.Settings.Base;
using MCM.Abstractions.Settings.Base.Global;
using System;
//using MCM.Abstractions.Settings.Base.PerSave;
using System.Collections.Generic;
using TaleWorlds.Localization;

namespace KaosesTweaks.Settings
{
    //public class MCMSettings : AttributePerSaveSettings<MCMSettings>, ISettingsProviderInterface
    //public class MCMSettings : AttributeGlobalSettings<MCMSettings>, ISettingsProviderInterface 
    public class MCMSettings : AttributeGlobalSettings<MCMSettings>
    {
        #region ModSettingsStandard
        public override string Id => Statics.InstanceID;

        // Build mod display name with name and version form the project properties version
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        string modName = Statics.DisplayName;
        public override string DisplayName => TextObjectHelper.Create("{=KaosesTweaksModDisplayName}" + modName + " {VERSION}", new Dictionary<string, TextObject>()
        {
            { "VERSION", TextObjectHelper.Create(typeof(MCMSettings).Assembly.GetName().Version?.ToString(3) ?? "")! }
        })!.ToString();
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

        public override string FolderName => Statics.ModuleFolder;
        public override string FormatType => Statics.FormatType;

        public bool LoadMCMConfigFile { get; set; } = false;
        public string ModDisplayName { get { return DisplayName; } }
        #endregion


        ///~ Mod Specific settings 

        //~ KT Debugging 
        #region Debugging
        private bool _Debug = false;
        [SettingPropertyBool("{=KT_Debug_000}Debug", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_000}Debug")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool Debug
        {
            get => _Debug;
            set
            {
                if (_Debug != value)
                {
                    _Debug = value;
                    if (value == false)
                    {
                        OnPropertyChanged(nameof(Debug));
                    }
                }
            }
        }

        [SettingPropertyBool("{=KT_Debug_001}Show Battle Rewards Calculation Message", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_001}Show Battle Rewards Calculation Message")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool BattleRewardShowDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_002}Debug mode - Army tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_002}Debug mode - Army tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool ArmyDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_020}Debug mode - Another Chance At marriage tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_020}Debug mode - Another Chance At marriage tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool AnotherChanceAtMarriageDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_003}Debug mode - Battle reward tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_003}Debug mode - Battle reward tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool BattleRewardsDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_004}Debug mode - Clan tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_004}Debug mode - Clan tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool ClanDebugging { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_005}Debug mode - Item tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_005}Debug mode - Item tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool ItemDebugMode { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_006}Debug mode - Pregnancy tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_006}Debug mode - Pregnancy tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool PregnancyDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_007}Debug mode - Xp tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_007}Debug mode - Xp tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool XpModifiersDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_008}Debug mode - Tournament tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_008}Debug mode - Tournament tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool TournamentDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_009}Debug mode - Prisoner tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_009}Debug mode - Prisoner tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool PrisonersDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_010}Debug mode - Settlement tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_010}Debug mode - Settlement tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool SettlementsDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_011}Debug mode - Wanderer location tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_011}Debug mode - Wanderer location tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool WandererLocationDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_012}Debug mode - Party size tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_012}Debug mode - Party size tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool PartySizeLimitsDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_013}Debug mode - Workshop tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_013}Debug mode - Workshop tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool WorkshopsDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_014}Debug mode - Crafting tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_014}Debug mode - Crafting tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool CraftingDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_015}Debug mode - Learning tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_015}Debug mode - Learning tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool LearningDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_016}Debug mode - Battlesize tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_016}Debug mode - Battlesize tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool BattleSizeDebug { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_017}Debug mode - Culture transformation tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_017}Debug mode - Culture transformation tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool CultureChangeDebug { get; set; } = false;


        [SettingPropertyBool("{=KT_Debug_018}Create Logfile", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_018}Create Logfile")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool LogToFile { get; set; } = false;

        [SettingPropertyBool("{=KT_Debug_019}Debug mode - Killing Bandits Raises RelationShips tweaks", IsToggle = false, Order = 0, RequireRestart = true,
            HintText = "{=KT_Debug_019}Debug mode - Killing Bandits Raises RelationShips tweaks")]
        [SettingPropertyGroup("Debug", GroupOrder = 100)]
        public bool KillingBanditsDebug { get; set; } = false;
        #endregion

        //~ Age Tweaks
        #region Age Tweaks
        [SettingPropertyBool("{=BT_Settings_002100}Age Tweaks" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_002100_Desc}Enables the tweaking of character age behavior."),
            SettingPropertyGroup("{=BT_Settings_002100}Age Tweaks")] //, GroupOrder = 1
        public bool AgeTweaksEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_002101}Become Infant Age", 0, 125, "0 Years", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_002101_Desc}Native: 3. Must be less than Become Child Age."),
            SettingPropertyGroup("{=BT_Settings_002100}Age Tweaks")]
        public int BecomeInfantAge { get; set; } = 3;

        [SettingPropertyInteger("{=BT_Settings_002102}Become Child Age", 0, 125, "0 Years", RequireRestart = false, Order = 3,
            HintText = "{=BT_Settings_002102_Desc}Native: 6. Must be less than Become Teenager Age."),
            SettingPropertyGroup("{=BT_Settings_002100}Age Tweaks")]
        public int BecomeChildAge { get; set; } = 6;

        [SettingPropertyInteger("{=BT_Settings_002103}Become Teenager Age", 0, 125, "0 Years", RequireRestart = false, Order = 4,
            HintText = "{=BT_Settings_002103_Desc}Native: 14. Must be less than Hero Comes Of Age."),
            SettingPropertyGroup("{=BT_Settings_002100}Age Tweaks")]
        public int BecomeTeenagerAge { get; set; } = 14;

        [SettingPropertyInteger("{=BT_Settings_002104}Hero Comes Of Age", 0, 125, "0 Years", RequireRestart = false, Order = 5,
            HintText = "{=BT_Settings_002104_Desc}Native: 18. Must be less than Become Old Age."),
            SettingPropertyGroup("{=BT_Settings_002100}Age Tweaks")]
        public int HeroComesOfAge { get; set; } = 18;

        [SettingPropertyInteger("{=BT_Settings_002105}Become Old Age", 0, 125, "0 Years", RequireRestart = false, Order = 6,
            HintText = "{=BT_Settings_002105_Desc}Native: 47. Must be less than Max Age."),
            SettingPropertyGroup("{=BT_Settings_002100}Age Tweaks")]
        public int BecomeOldAge { get; set; } = 47;

        [SettingPropertyInteger("{=BT_Settings_002106}Max Age", 0, 125, "0 Years", RequireRestart = false, Order = 7,
            HintText = "{=BT_Settings_002106_Desc}Native: 125."),
            SettingPropertyGroup("{=BT_Settings_002100}Age Tweaks")]
        public int MaxAge { get; set; } = 125;
        #endregion

        //~ Another Chance at marriage
        #region Another Chance at marriage
        [SettingPropertyBool("{=KT_ACAM}Another Chance At Marrige Enabled" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=KT_ACAMH}Enables the another chance at marriage tweaks."),
            SettingPropertyGroup("{=KT_ACAMC}Another Chance At Marrige")] //, GroupOrder = 1
        public bool AnotherChanceAtMarriageEnabled { get; set; } = false;

        [SettingPropertyInteger("{=KT_ACAMDBT}Days Between Tries", 1, 20, "0 Days", Order = 0, RequireRestart = false,
            HintText = "{=KT_ACAMDBT_Desc}The number of days that has to pass between attempts to restart the courtship. [Native: N/A].")]
        [SettingPropertyGroup("{=KT_ACAMC}Another Chance At Marrige")]
        public int AnotherChanceAtMarriageDaysTillRetry { get; set; } = 3;

        #endregion //~ Another Chance at marriage

        //~ ArmyManagement
        #region ArmyManagement
        [SettingPropertyBool("{=KTMCM_AT}Army Tweaks " + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_AMH}Enable army modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CArmy}Army")]
        public bool MCMArmy { get; set; } = false;
        //~ Cohesion
        #region Cohesion
        [SettingPropertyBool("{=KTMCM_AMCM}Cohesion Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_AMSMH}Allows Cohesion modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CArmy}Army" + "/" + "{=KTMCM_CCohesion}Cohesion")]
        public bool armyCohesionMultipliers { get; set; } = false;

        [SettingPropertyInteger("{=KTMCM_AMCMBC}Base Cohesion Change", -5, 5, "0 Cohesion", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_AMCMBCH}Base Cohesion Change for armies [Native : -2].")]
        [SettingPropertyGroup("{=KTMCM_CArmy}Army" + "/" + "{=KTMCM_CCohesion}Cohesion")]
        public int armyCohesionBaseChange { get; set; } = -2;

        [SettingPropertyBool("{=KTMCM_AMCMDCOO}Clan Only army disable cohesion", Order = 0, RequireRestart = false,
            HintText = "{=}Disable cohesion loss for armies that are made up of a single clans parties only.")]
        [SettingPropertyGroup("{=KTMCM_CArmy}Army" + "/" + "{=KTMCM_CCohesion}Cohesion")]
        public bool armyDisableCohesionLossClanOnlyParties { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_AMCMCO}Clan Only armies Use Multiplier", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_AMCMCOH}Apply the cohesion multiplier to armies that are made up of a single clans parties only.")]
        [SettingPropertyGroup("{=KTMCM_CArmy}Army" + "/" + "{=KTMCM_CCohesion}Cohesion")]
        public bool armyApplyMultiplerToClanOnlyParties { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_AMCHMM}Cohesion Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_AMCHMMH}Multiply the cohesion loss by this multiplier [Native : 1.0[100%]]. this applies to starving parties, low morale parties and low number of healthy troops parties")]
        [SettingPropertyGroup("{=KTMCM_CArmy}Army" + "/" + "{=KTMCM_CCohesion}Cohesion")]
        public float armyCohesionLossMultiplier { get; set; } = 1.0f;

        #endregion
        #endregion //~ArmyManagement

        //~ Battle Tweaks 
        #region Battle Tweaks
        [SettingPropertyBool("{=BT_Settings_000000}Battle Tweaks", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_BTH}Enables editing battle tweaks.")]
        [SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks")]
        public bool battleTweaksEnabled { get; set; } = true;

        //~ Hideout Tweaks
        #region Hideout Tweaks
        [SettingPropertyBool("{=BT_Settings_000200}Hideout Tweaks " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_000200_Desc}Changes game behavior inside hideout battles."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000200}Hideout Tweaks", GroupOrder = 2)]
        public bool HideoutBattleTroopLimitTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_000201}Hideout Battle Troop Limit", 5, 90, "0 Soldiers", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_000201_Desc}Native value is 9 or 10. Changes the maximum troop limit to the set value inside hideout battles. Cannot be higher than 90 because it causes bugs."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000200}Hideout Tweaks")]
        public int HideoutBattleTroopLimit { get; set; } = 10;

        [SettingPropertyBool("{=BT_Settings_000202}Continue Hideout Battle On Player Death" + "*", Order = 3, RequireRestart = true,
            HintText = "{=BT_Settings_000202_Desc}Native value is false. If enabled, you will not automatically lose the hideout battle if you die. Your troops will charge and the boss duel will be disabled."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000200}Hideout Tweaks")]
        public bool ContinueHideoutBattleOnPlayerDeath { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_000203}Continue Battle On Losing Duel" + "*", Order = 4, RequireRestart = true,
            HintText = "{=BT_Settings_000203_Desc}Native value is false. If enabled, your troops will rush to avenge you and finish everyone off."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000200}Hideout Tweaks")]
        public bool ContinueHideoutBattleOnPlayerLoseDuel { get; set; } = false;
        #endregion //~ Hideout Tweaks

        //~ Battle Size Tweak
        #region Battle Size Tweak
        [SettingPropertyBool("{=BT_Settings_001100}Battle Size Tweak" + "*", Order = 1, IsToggle = true, RequireRestart = true,
            HintText = "{=BT_Settings_001100_Desc}Allows you to set the battle size limit outside of native values. WARNING: Setting this above 1000 can cause performance degradation and crashes."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_001100}Battle Size Tweak" + "*")]
        public bool BattleSizeTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_001101}Battle Size Limit", 2, 1800, "0 Soldiers", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_001101_Desc}Sets the limit for number of troops on a battlefield, ignoring what is in Bannerlord Options. WARNING: Will crash if all troops + their horses exceed 2000."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_001100}Battle Size Tweak" + "*")]
        public int BattleSize { get; set; } = 1000;

        #endregion

        //~ Dynamic Battle Sizes
        #region Dynamic Battle Sizes
        [SettingPropertyBool("{=KTMCM_BSTEXP}Dynamic Battle Sizes !BETA! " + "*", Order = 1, IsToggle = true, RequireRestart = true,
            HintText = "{=KTMCM_BSTEXP_Desc}Allows you to set the battle size limit outside of native values. Will be dynamically adjusted to actual battle parameters. WARNING: BETA."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=KTMCM_BSTEXP}Dynamic Battle Sizes !BETA! " + "*")]
        public bool BattleSizeTweakExEnabled { get; set; } = false;

        [SettingPropertyInteger("{=KTMCM_BSTEXP_01}Battle Size Limit", 2, 2000, "0 Soldiers", Order = 2, RequireRestart = false,
            HintText = "{=KTMCM_BSTEXP_01_Desc}Sets the limit for number of troops on a battlefield. May get automatically reduced when a crash is imminent because of a too large battle size limit for a given battle."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=KTMCM_BSTEXP}Dynamic Battle Sizes !BETA! " + "*")]
        public int BattleSizeEx { get; set; } = 1000;

        [SettingPropertyInteger("{=KTMCM_BSTEXP_02}Min size of reinforcements", 0, 50, "0'%'", Order = 2, RequireRestart = false,
            HintText = "{=KTMCM_BSTEXP_02_Desc}Sets the amount of free slots needed in relation to battlesize before reinforcements can spawn. With a Battle Size Limit of 1000 and a set value of 10% --> Min size of reinforcement is 100 (for both sides combined). [Vanilla:10%]"),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=KTMCM_BSTEXP}Dynamic Battle Sizes !BETA! " + "*")]
        public int ReinforcementQuota { get; set; } = 20;

        [SettingPropertyFloatingInteger("{=KTMCM_BSTEXP_03}Reserved Slots for reinforcements", 0f, 1f, "0 %", Order = 3, RequireRestart = false,
            HintText = "{=KTMCM_BSTEXP_03_Desc}Reserve this amount of slots in relation to Min size of reinforcements value if close to the max slots limit. If this is set to low and you are near the max slot limit of 2048 reinforcements spawn very late or wont spawn fully/at all."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=KTMCM_BSTEXP}Dynamic Battle Sizes !BETA! " + "*")]
        public float SlotsForReinforcements { get; set; } = 0.5f;

        [SettingPropertyInteger("{=KTMCM_BSTEXP_04}Horses on Battlefield", 0, 500, "0 Horses", Order = 3, RequireRestart = false,
            HintText = "{=KTMCM_BSTEXP_04_Desc}The amount of horses that will stay on the battlefield. Any horses exceeding that amount will flee from the battlefield and free up agent slots."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=KTMCM_BSTEXP}Dynamic Battle Sizes !BETA! " + "*")]
        public int RetreatHorses { get; set; } = 50;

        [SettingPropertyFloatingInteger("{=KTMCM_BSTEXP_05}Safety Puffer", 1, 1.5f, "0 %", Order = 3, RequireRestart = false,
            HintText = "{=KTMCM_BSTEXP_05_Desc}Vanilla assumes each troop to spawn is mounted (2 agents). We instead get the mounted share in army composition before battle and update it during battle. This value increases that share for safety reasons, because reinforcements dont exactly spawn with initial army composition ratio. Increase if you get crashes."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=KTMCM_BSTEXP}Dynamic Battle Sizes !BETA! " + "*")]
        public float BattleSizeExSafePuffer { get; set; } = 1.1f;

        #endregion 

        //~ Battle Morale Tweaks
        #region Battle Moral
        [SettingPropertyBool("{=KTMCM_BMT}Battle Moral Tweaks " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=KTMCM_BMT_Desc}Tweaks for battle moral changes."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=KTMCM_BMT}Battle Moral Tweaks" + "*", GroupOrder = 3)]
        public bool BattleMoralTweaksEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_MC}Moral Change Modifier", 0f, 3f, "0%", RequireRestart = false, Order = 2,
            HintText = "{=KTMCM_MC_Desc}Increases or decreases the amount battle moral changes when different effects get applied. 0 % results in no battle moral changes at all and will prevent any unit fleeing for example [Native : 100%.]"),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=KTMCM_BMT}Battle Moral Tweaks" + "*")]
        public float BattleMoralTweaksMultiplier { get; set; } = 1f;

        #endregion

        //~ Siege Tweaks
        #region Siege Tweaks
        [SettingPropertyBool("{=BT_Settings_000300}Siege Tweaks " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_000300_Desc}Tweaks for siege engine construction speed and collateral casulaties during sieges."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000300}Siege Tweaks", GroupOrder = 3)]
        public bool SiegeTweaksEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_000301}Siege Construction Progress Per Day Amount", 0.1f, 10.0f, "0%", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_000301_Desc}This tweak adds a modifier to the construction progress per day for sieges. A smaller number results in longer siege times. [Native : 100%.]"),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000300}Siege Tweaks")]
        public float SiegeConstructionProgressPerDayMultiplier { get; set; } = 1f;

        [SettingPropertyInteger("{=BT_Settings_000302}Siege Collateral Damage Casualties", 0, 10, Order = 5, RequireRestart = false,
            HintText = "{=BT_Settings_000302_Desc}Native value is 0. This tweak adds to the base value (1 or 2 with Crossbow Terror Perk) used to calculate collateral casualties during the campaign map siege stage."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000300}Siege Tweaks")]
        public int SiegeCollateralDamageCasualties { get; set; } = 0;

        [SettingPropertyInteger("{=BT_Settings_000303}Siege Destruction Casualties", 0, 10, Order = 6, RequireRestart = false,
            HintText = "{=BT_Settings_000303_Desc}Native value is 0. This tweak adds to the base value (2) used to calculate destruction casualties during the campaign map siege stage."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000300}Siege Tweaks")]
        public int SiegeDestructionCasualties { get; set; } = 0;
        #endregion //~ Siege Tweaks

        //~ Weapon Cut Through Tweaks
        #region Weapon Cut Through Tweaks
        [SettingPropertyBool("{=BT_Settings_000500}Weapon Cut Through Tweaks" + "*", Order = 1, IsToggle = true, RequireRestart = true,
            HintText = "{=BT_Settings_000500_Desc}Allows all weapon types to cut through and hit multiple people."), SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000500}Weapon Cut Through Tweaks" + "*", GroupOrder = 5)]
        public bool SliceThroughEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_000501}All Two-Handed Weapons Cut Through", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_000501_Desc}Allows all two-handed weapon types to cut through and hit multiple people."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000500}Weapon Cut Through Tweaks" + "*")]
        public bool TwoHandedWeaponsSliceThroughEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_000502}All One-Handed Weapons Cut Through", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_000502_Desc}Allows all single-handed weapon types to cut through and hit multiple people."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000500}Weapon Cut Through Tweaks" + "*")]
        public bool SingleHandedWeaponsSliceThroughEnabled { get; set; } = false;

        [SettingPropertyBool("{=Titan_Settings_000007}All Weapons Cut Through", Order = 3, RequireRestart = false,
            HintText = "{=Titan_Settings_000007_Desc}Allows all weapon types to cut through and hit multiple people."),
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000500}Weapon Cut Through Tweaks" + "*")]
        public bool AllWeaponsSliceThroughEnabled { get; set; } = false;
        #endregion //~ Weapon Cut Through Tweaks
        #endregion //~ Battle Tweaks 

        //~ Battle Rewards
        #region Battle Rewards
        [SettingPropertyBool("{=KTMCM_BRMT}Battle Reward Tweaks " + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_BRMH}Enables modifying battle rewards.")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards")]
        public bool MCMBattleRewardModifiers { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_BRMTHP}Battle Reward Tweaks Patches Only " + "*", Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_BRMTHPH}Use harmony patches only for battle reward tweaks. Use for comaptability with other mods.")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards")]
        public bool BattleRewardModifiersPatchOnly { get; set; } = false;

        //~ Rewards apply to AI *needs testing is this needed or does it need to be the other way round
        //[SettingPropertyBool("{=BT_Settings_000104}Also Apply To AI", Order = 5, RequireRestart = false,
        //    HintText = "{=BT_Settings_000104_Desc}Applies the renown, influence and morale modifiers to AI parties."),
        //    SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards")]
        public bool BattleRewardApplyToAI { get; set; } = false;

        //~ Relationship Gain
        #region RelationshipGain
        [SettingPropertyBool("{=KTMCM_BRMRG}Relationship Gain", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_BRMRGH}Enables modifying Relationship gain.")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards" + "/" + "{=KTMCM_CRelationShipGain}RelationShip Gain")]
        public bool BattleRewardsRelationShipGainModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KTMCM_BRMRGM}Relationship gain Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_BRMRGMH}Multiply Relationship gain  by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards" + "/" + "{=KTMCM_CRelationShipGain}RelationShip Gain")]
        public float BattleRewardsRelationShipGainMultiplier { get; set; } = 1.0f;
        #endregion

        //~ Renown Gain
        #region  RenownGain
        [SettingPropertyBool("{=KTMCM_BRMRNG}Renown Gain", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_BRMRNGH}Enables modifying Renown gain.")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards" + "/" + "{=KTMCM_CRenownGain}Renown Gain")]
        public bool BattleRewardsRenownGainModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KTMCM_BRMRNGM}Renown gain Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_BRMRNGMH}Multiply Renown gain  by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards" + "/" + "{=KTMCM_CRenownGain}Renown Gain")]
        public float BattleRewardsRenownGainMultiplier { get; set; } = 1.0f;
        #endregion

        //~ Influence Gain
        #region  InfluenceGain
        [SettingPropertyBool("{=KTMCM_BRMIG}Influence Gain", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_BRMIGH}Enables modifying Influence gain.")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards" + "/" + "{=KTMCM_CInfluenceGain}Influence Gain")]
        public bool BattleRewardsInfluenceGainModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KTMCM_BRMIGM}Influence gain Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_BRMIGMH}Multiply Influence gain  by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards" + "/" + "{=KTMCM_CInfluenceGain}Influence Gain")]
        public float BattleRewardsInfluenceGainMultiplier { get; set; } = 1.0f;
        #endregion  

        //~ Morale Gain
        #region  MoraleGain
        [SettingPropertyBool("{=KTMCM_BRMMG}Morale Gain", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_BRMMGH}Enables modifying Morale gain.")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards" + "/" + "{=KTMCM_CMoraleGain}Morale Gain")]
        public bool BattleRewardsMoraleGainModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KTMCM_BRMMGM}Morale gain Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_BRMMGMH}Multiply Morale gain  by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards" + "/" + "{=KTMCM_CMoraleGain}Morale Gain")]
        public float BattleRewardsMoraleGainMultiplier { get; set; } = 1.0f;
        #endregion  

        //~ Gold loss after defeat
        #region  GoldLossAfterDefeat
        [SettingPropertyBool("{=KTMCM_BRMGL}Gold Loss", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_BRMGLH}Enables modifying Gold Loss on defeat .")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards" + "/" + "{=KTMCM_CGoldLoss}Gold Loss")]
        public bool BattleRewardsGoldLossModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KTMCM_BRMGLM}Gold Loss Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_BRMGLMH}Multiply Gold Loss on defeat by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards" + "/" + "{=KTMCM_CGoldLoss}Gold Loss")]
        public float BattleRewardsGoldLossMultiplier { get; set; } = 1.0f;
        #endregion
        #endregion

        //~ Campaign Tweaks
        #region Campaign Tweaks

        [SettingPropertyBool("{=BT_Settings_001000}Campaign Tweaks", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_CTH}Enables editing campaign tweaks")]
        [SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks")]
        public bool CampaignTweaksEnabled { get; set; } = false;

        //~ Difficulty Settings
        #region Difficulty Settings
        [SettingPropertyBool("{=BT_Settings_001200}Difficulty Tweaks " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_001200_Desc}Allows you to change the multiplier for several difficulty settings."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks", GroupOrder = 2)]
        public bool DifficultyTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_001201}Damage to Player Tweak", Order = 2, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_001201_Desc}Allows you to change the multiplier for damage the player receives."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "/" + "{=BT_Settings_001201}Damage to Player Tweak")]
        public bool DamageToPlayerTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_001202}Damage to Player Tweak Amount", 0.1f, 5.0f, "0%", RequireRestart = false, Order = 3,
            HintText = "{=BT_Settings_001202_Desc}Native values: Very Easy: 30%, Easy: 67%, Realistic: 100%. This value is used to calculate the damage player receives."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "/" + "{=BT_Settings_001201}Damage to Player Tweak")]
        public float DamageToPlayerMultiplier { get; set; } = 1.0f;

        /*
                [SettingPropertyBool("{=BT_Settings_001203}Damage to Friends Tweak", Order = 4, RequireRestart = false, IsToggle = true,
                    HintText = "{=BT_Settings_001203_Desc}Allows you to change the damage the player's friends receive."),
                    SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "/" + "{=BT_Settings_001203}Damage to Friends Tweak", GroupOrder = 2)]
                public bool DamageToFriendsTweakEnabled { get; set; } = false;

                [SettingPropertyFloatingInteger("{=BT_Settings_001204}Damage to Friends Tweak Amount", 0.1f, 5.0f, "0%", RequireRestart = false, Order = 5,
                    HintText = "{=BT_Settings_001204_Desc}Native values: Very Easy: 30%, Easy: 67%, Realistic: 100%. This value is used to calculate the damage the player's friends receive."),
                    SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "/" + "{=BT_Settings_001203}Damage to Friends Tweak")]
                public float DamageToFriendsMultiplier { get; set; } = 1.0f;*/

        [SettingPropertyBool("{=BT_Settings_001205}Damage to Player's Troops Tweak", Order = 6, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_001205_Desc}Allows you to change the multiplier for damage the player's troops receive."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "/" + "{=BT_Settings_001205}Damage to Player's Troops Tweak", GroupOrder = 3)]
        public bool DamageToTroopsTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_001206}Damage to Troops Tweak Amount", 0.1f, 5.0f, "0%", RequireRestart = false, Order = 7,
            HintText = "{=BT_Settings_001206_Desc}Native values: Very Easy: 30%, Easy: 67%, Realistic: 100%. This value is used to calculate the damage to the player's troops."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "/" + "{=BT_Settings_001205}Damage to Player's Troops Tweak")]
        public float DamageToTroopsMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("{=BT_Settings_001207}Combat AI Difficulty Tweak", Order = 8, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_001207_Desc}Allows you to change the AI combat difficulty."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "/" + "{=BT_Settings_001207}Combat AI Difficulty Tweak", GroupOrder = 4)]
        public bool CombatAIDifficultyTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_001208}Combat AI Difficulty Tweak Amount", 0.1f, 1.0f, "0%", RequireRestart = false, Order = 9,
            HintText = "{=BT_Settings_001208_Desc}Native values: Very Easy: 10%, Easy: 32%, Realistic: 96%. This value is used to calculate AI combat difficulty."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "/" + "{=BT_Settings_001207}Combat AI Difficulty Tweak")]
        public float CombatAIDifficultyMultiplier { get; set; } = 0.96f;

        [SettingPropertyBool("{=BT_Settings_001209}Player Map Movement Speed Tweak", Order = 10, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_001209_Desc}Allows you to change the bonus map movement speed multiplier the player receives."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "/" + "{=BT_Settings_001209}Player Map Movement Speed Tweak", GroupOrder = 5)]
        public bool PlayerMapMovementSpeedBonusTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_001210}Player Map Movement Tweak Amount", 0.0f, 2.0f, "0%", RequireRestart = false, Order = 11,
            HintText = "{=BT_Settings_001210_Desc}Native values: Very Easy: 10%, Easy: 5%, Realistic: 0%. This value is used to calculate player's map movement speed."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "/" + "{=BT_Settings_001209}Player Map Movement Speed Tweak")]
        public float PlayerMapMovementSpeedBonusMultiplier { get; set; } = 0.0f;


        [SettingPropertyBool("{=KT_PPBC}Player Persuasion Bonus Chance Tweak", Order = 10, RequireRestart = false, IsToggle = true,
            HintText = "{=KT_PPBCH}Allows you to change the bonus persuasion chance multiplier the player receives."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "/" + "{=KT_PPBC}Player Persuasion Bonus Chance Tweak", GroupOrder = 5)]
        public bool PlayerPersuasionBonusChanceTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KT_PPBCM}Player Persuasion Bonus Chance Tweak Amount", 0.00f, 1.0f, "0%", RequireRestart = false, Order = 11,
            HintText = "{=KT_PPBCMH}Native values: Very Easy: 0.1%, Easy: 0.05%, Realistic: 0%. This value is used to calculate player's persuasion bonus chance."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "/" + "{=KT_PPBC}Player Persuasion Bonus Chance Tweak")]
        public float PlayerPersuasionBonusChanceMultiplier { get; set; } = 0.00f;


        [SettingPropertyBool("{=KT_CMDCR}Clan Member Death Chance Reduction Tweak", Order = 10, RequireRestart = false, IsToggle = true,
            HintText = "{=KT_CMDCRH}Allows you to change the clan member death chance reduction multiplier."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "/" + "{=KT_CMDCR}Clan Member Death Chance Reduction Tweak", GroupOrder = 5)]
        public bool ClanMemberDeathChanceReductionTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KT_CMDCRM}Clan Member Death Chance Reduction Amount", 0.00f, 1.0f, "0%", RequireRestart = false, Order = 11,
            HintText = "{=KT_CMDCRMH}Native values: Very Easy: 0.1%, Easy: 0.05%, Realistic: 0%. This value is used to calculate Clan member death chance reduction in missions."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "/" + "{=KT_CMDCR}Clan Member Death Chance Reduction Tweak")]
        public float ClanMemberDeathChanceReductionMultiplier { get; set; } = 0.00f;


        #endregion //~ Difficulty Settings

        #endregion //~ Campaign Tweaks

        //~ Clan *AI Tweaks need Checking
        #region Clan
        [SettingPropertyBool("{=BT_Settings_003000}Clan Tweaks " + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_CLMH}Enables modifying clan variables.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks")]
        public bool MCMClanModifiers { get; set; } = false;

        //~ Party Limit
        #region PartyLimit
        [SettingPropertyBool("{=KTMCM_CLMBPL}Party Limits", IsToggle = true, RequireRestart = false,//, Order = 3
            HintText = "{=KTMCM_CLMBPLH}Enables additional Party limit per clan tier .")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=BT_Settings_003200}Party Limits")]
        public bool ClanAdditionalPartyLimitEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_003201}Player Parties Limit", IsToggle = true, RequireRestart = false,
            HintText = "{=BT_Settings_003201_Desc}Changes the base number of parties you can field and the bonus gained per clan tier.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=BT_Settings_003200}Party Limits" + "/" + "{=KTMCM_CPlayer}Player")]
        public bool ClanPlayerPartiesLimitEnabled { get; set; } = false;

        //~ Limits Player
        #region Clan Tweaks - Limits Player
        [SettingPropertyInteger("{=BT_Settings_003202}Base Parties Limit", 0, 10, "0 Parties", Order = 0, RequireRestart = false,
            HintText = "{=BT_Settings_003202_Desc}This is the number of parties you can field at the start.[Native: 1].")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=BT_Settings_003200}Party Limits" + "/" + "{=KTMCM_CPlayer}Player")]
        public int ClanPlayerBasePartiesLimit { get; set; } = 1;

        [SettingPropertyFloatingInteger("{=BT_Settings_003203}Parties Bonus Per Clan Tier", 0.0f, 6.0f, "0.0 Parties/Clan Tier", RequireRestart = false,
            HintText = "{=BT_Settings_003203_Desc}Native has a calculation for this: 1 party for under tier 3, 2 parties for under tier 5, 3 parties for over tier 5. This setting is multiplied by your clan tier. A value of 0.5 will equate to 1 extra party per 2 clan tiers, which eqautes to the same as native.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=BT_Settings_003200}Party Limits" + "/" + "{=KTMCM_CPlayer}Player")]
        public float ClanPlayerPartiesBonusPerClanTier { get; set; } = 0.5f;
        #endregion //~ Limits Player

        [SettingPropertyBool("{=BT_Settings_003204}AI Lord Parties Limit", IsToggle = true, RequireRestart = false,
            HintText = "{=BT_Settings_003204_Desc}Changes the base number of parties AI Lords can field.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=BT_Settings_003200}Party Limits" + "/" + "{=BT_Settings_003204}AI Lord Parties Limit", GroupOrder = 2)]
        public bool ClanAIPartiesLimitTweakEnabled { get; set; } = false;

        //~ AI Parties Limits
        #region Clan Tweaks - AI Lord Parties Limmit
        [SettingPropertyInteger("{=BT_Settings_003205}Add to AI Lord Parties Limit", 0, 10, "0 Parties", Order = 0, RequireRestart = false,
            HintText = "{=BT_Settings_003205_Desc}This adds to the the base number of parties AI Lords can field. [Native is 1 for Tier 3 and below, 2 at T4, 3 at T5 and up.] Minor Factions are not included unless the option below is also enabled.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=BT_Settings_003200}Party Limits" + "/" + "{=BT_Settings_003204}AI Lord Parties Limit")]
        public int ClanAIBaseClanPartiesLimit { get; set; } = 1;

        [SettingPropertyFloatingInteger("{=BT_Settings_003203}Parties Bonus Per Clan Tier", 0.0f, 6.0f, "0.0 Parties/Clan Tier", RequireRestart = false,
            HintText = "{=BT_Settings_003203_Desc}Native has a calculation for this: 1 party for under tier 3, 2 parties for under tier 5, 3 parties for over tier 5. This setting is multiplied by your clan tier. A value of 0.5 will equate to 1 extra party per 2 clan tiers, which eqautes to the same as native.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=BT_Settings_003200}Party Limits" + "/" + "{=BT_Settings_003204}AI Lord Parties Limit")]
        public float ClanAIPartiesBonusPerClanTier { get; set; } = 0.5f;

        [SettingPropertyBool("{=BT_Settings_003206}Also Adjust Minor Factions", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_003206_Desc}Changes the base number of parties AI Minor Faction Lords can field. [Native is 1-4, depending on Clan tier.]"),
            SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=BT_Settings_003200}Party Limits" + "/" + "{=BT_Settings_003204}AI Lord Parties Limit")]
        public bool ClanAIMinorClanPartiesLimitTweakEnabled { get; set; } = false;
        #endregion //~ AI Parties Limits

        [SettingPropertyBool("{=BT_Settings_003207}Custom Spawn Parties Limit", IsToggle = true, RequireRestart = false,
            HintText = "{=BT_Settings_003207_Desc}Changes the base number of parties Custom Spawn lords can field.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=BT_Settings_003200}Party Limits" + "/" + "{=BT_Settings_003207}Custom Spawns Parties Limit", GroupOrder = 3)]
        public bool AICustomSpawnPartiesLimitTweakEnabled { get; set; } = false;

        //~ CS Parties
        #region Clan Tweaks - Custom Spawns Parties Tweak

        [SettingPropertyInteger("{=BT_Settings_003208}Add to Custom Spawn Parties Limit", 0, 10, "0 Parties", Order = 0, RequireRestart = false,
            HintText = "{=BT_Settings_003208_Desc}This adds to the the base number of parties Custom Lords can field. [Native is 1 for Tier 3 and below, 2 at T4, 3 at T5 and up.].")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=BT_Settings_003200}Party Limits" + "/" + "{=BT_Settings_003207}Custom Spawns Parties Limit")]
        public int BaseAICustomSpawnPartiesLimit { get; set; } = 0;

        [SettingPropertyFloatingInteger("{=BT_Settings_003203}Parties Bonus Per Clan Tier", 0.0f, 6.0f, "0.0 Parties/Clan Tier", RequireRestart = false,
            HintText = "{=BT_Settings_003203_Desc}Native has a calculation for this: 1 party for under tier 3, 2 parties for under tier 5, 3 parties for over tier 5. This setting is multiplied by your clan tier. A value of 0.5 will equate to 1 extra party per 2 clan tiers, which eqautes to the same as native.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=BT_Settings_003200}Party Limits" + "/" + "{=BT_Settings_003207}Custom Spawns Parties Limit")]
        public float ClanCSPartiesBonusPerClanTier { get; set; } = 0.5f;
        #endregion //~ CS Parties

        #endregion //~ Party Limits

        //~ Companion Limit
        #region CompanionLimit
        [SettingPropertyBool("{=KTMCM_CLMCL}Bonus Companion Limit", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_CLMCLH}Enables additional Companion limit per clan tier .")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=KTMCM_CCompanionLimit}Companion Limit")]
        public bool ClanCompanionLimitEnabled { get; set; } = false;

        [SettingPropertyInteger("{=KTMCM_CLMCLBC}Bonus Companions", 0, 50, "0 Companions", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_CLMCLBCH}Additional Companion limit per clan tier [Native: 0].")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=KTMCM_CCompanionLimit}Companion Limit")]
        public int ClanAdditionalCompanionLimit { get; set; } = 0;

        [SettingPropertyInteger("{=BT_Settings_003101}Base Companion Limit", 1, 50, "0 Companions", Order = 0, RequireRestart = false,
            HintText = "{=BT_Settings_003101_Desc}Sets the base companion limit. [Native: 3].")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=KTMCM_CCompanionLimit}Companion Limit")]
        public int ClanCompanionBaseLimit { get; set; } = 3;

        [SettingPropertyInteger("{=BT_Settings_00310}Companion Spawn Interval", 0, 20, "0 Weeks", Order = 0, RequireRestart = false,
          HintText = "{=BT_Settings_003104_Desc}Number of weeks between a new companion being added to the world. Set to 0 for a new companion to be added to the taverns every week. [Native: 6]")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks" + "/" + "{=KTMCM_CCompanionLimit}Companion Limit")]
        public int CompanionSpawnInterval { get; set; } = 6;
        #endregion //~ Companion Limit
        #endregion //~ Clan

        //~ Crafting Tweaks
        #region Crafting Tweaks
        [SettingPropertyBool("{=BT_Settings_004000}Crafting Tweaks " + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_SMH}Enables modifying Crafting variables.")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks")]
        public bool MCMSmithingModifiers { get; set; } = false; // Activates the Model Override

        [SettingPropertyBool("{=KTMCM_SMPOM}Use Patches instead of model " + "*", Order = 1, RequireRestart = true,
            HintText = "{=KTMCM_SMPOMH}Enables using only harmoney patches and does not load a smithing model. This applies to energy cost multiplers and xp multipliers. Does not apply to " +
            "Max crafting stamina, stamina gains, or smelting tweaks they are patches only")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks")]
        public bool MCMSmithingHarmoneyPatches { get; set; } = false; // Activates the Model Override

        //~ Refining Formula Multipliers
        #region Refining Formula Multipliers
        [SettingPropertyBool("{=KT_CTRFME}Refining Formula Tweaks" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=KT_CTRFME_Desc}Enables refining formula tweaks which adjust formula cost and output values."),
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=KT_CTRFME}Refining Formula Tweaks")]
        public bool RefiningFormulaTweaksEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KT_CTRFICM}Input Cost Multiplier", 0.1f, 5.0f, "#0%", RequireRestart = false,
            HintText = "{=KT_CTRFICM_Desc}Multiply formula input cost by the multiplier [Native : 1.0[100%]]. at 50% 1 wood = 1 charcoal at 100% 2 wood = 1 charcoal. Minimum resource cost is 1")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=KT_CTRFME}Refining Formula Tweaks")]
        public float RefiningFormulaInputCostValue { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KT_CTRFOTM}Output Cost Multiplier", 0.1f, 5.0f, "#0%", RequireRestart = false,
            HintText = "{=KT_CTRFOTM_Desc}Multiply formula output result by the multiplier [Native : 1.0[100%]]. at 200% 2 wood = 2 charcoal, 300% 2 wood = 3 charcoal")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=KT_CTRFME}Refining Formula Tweaks")]
        public float RefiningFormulaOutputValue { get; set; } = 1.0f;
        #endregion //~ Refining Formula Multipliers

        //~ Stamina Tweaks
        #region Stamina
        [SettingPropertyBool("{=BT_Settings_004100}Crafting Stamina" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_004100_Desc}Enables tweaks which affect crafting stamina."),
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004100}Crafting Stamina")]
        public bool CraftingStaminaTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_004101}Max Crafting Stamina Multiplier", 0.0f, 10.0f, "0.00", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_004101_Desc}Multiply max crafting stamina by the multiplier [Native: 1.0]"),
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004100}Crafting Stamina")]
        public float MaxCraftingStaminaMultiplier { get; set; } = 1.0f;

        //~ Stamina Gains
        #region StaminGain
        [SettingPropertyInteger("{=BT_Settings_004102}Crafting Stamina Gain", 0, 100, "0 Stamina/h", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_004102_Desc}Native value is 5. You gain this amount of crafting stamina per hour."),
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004100}Crafting Stamina")]
        public int CraftingStaminaGainAmount { get; set; } = 5;

        [SettingPropertyFloatingInteger("{=BT_Settings_004103}Crafting Stamina Gain Outside Settlement", 0f, 1f, "0%", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_004103_Desc}Native value is 0%. In % of Crafting Stamina Gain. In native, you do not gain crafting stamina if you are not resting inside a settlement."),
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004100}Crafting Stamina")]
        public float CraftingStaminaGainOutsideSettlementMultiplier { get; set; } = 0f;
        #endregion

        //~ Energy Cost Multipliers
        #region EnergyCostMultiplier
        [SettingPropertyFloatingInteger("{=KTMCM_SMECCM}Energy Use Crafting", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_SMECCMH}Multiply the energy used for Crafting by the multiplier [Native : 1.0[100%]]. 50% uses only half the native energy per action")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004100}Crafting Stamina")]
        public float SmithingEnergySmithingValue { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=}Energy Use Smithing", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_SMECSMH}Multiply the energy used for Smelting by the multiplier [Native : 1.0[100%]]. 50% uses only half the native energy per action")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004100}Crafting Stamina")]
        public float SmithingEnergySmeltingValue { get; set; } = 1.0f;


        [SettingPropertyFloatingInteger("{=KTMCM_SMECRM}Energy Use Refining", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_SMECRMH}Multiply the energy used for refining by the multiplier [Native : 1.0[100%]]. 50% uses only half the native energy per action")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004100}Crafting Stamina")]
        public float SmithingEnergyRefiningValue { get; set; } = 1.0f;
        #endregion

        [SettingPropertyBool("{=KTMCM_SMSE}Energy Disable", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_SMSEH}Disable the energy for crafting and refining tasks [Native : false]")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004100}Crafting Stamina")]
        public bool SmithingEnergyDisable { get; set; } = false;
        #endregion

        //~ BT Smelting Tweaks
        #region Smelting

        [SettingPropertyBool("{=BT_Settings_004200}Smelting " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_004200_Desc}Enables tweaks which affect smelting of weapons."),
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004200}Smelting", GroupOrder = 2)]
        public bool SmeltingTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_004201}Hide Locked Weapons in Smelting Menu", Order = 1, RequireRestart = false,
            HintText = "{=BT_Settings_004201_Desc}Native value is false. Prevent weapons that you have locked in your inventory from showing up in the smelting list to prevent accidental smelting."),
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004200}Smelting")]
        public bool PreventSmeltingLockedItems { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_004202}Enable Unlocking Parts From Smelted Weapons", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_004202_Desc}Native value is false. Unlock the parts that a weapon is made of when you smelt it."),
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004200}Smelting")]
        public bool AutoLearnSmeltedParts { get; set; } = false;


        [SettingPropertyBool("{=BT_Settings_004203}Enable Unlocking All Parts Fror Smelted Weapons", Order = 4, RequireRestart = true,
            HintText = "{=BT_Settings_004203_Desc}Enables unlocking all wepaon parts for crafting weapons on next game load."),
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004200}Smelting")]
        public bool craftingUnlockAllParts { get; set; } = false;
        #endregion

        //~ Crafting Quality Bonus
        #region Crafting Quality Bonus
        [SettingPropertyBool("{=KT_CTCQME}Quality Tweaks" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=KT_CTCQME_Desc}Enablessetting the base bonus for each of the crafting bonus types, fine, master and legendary."),
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=KT_CTCQME}Quality Tweaks")]
        public bool CraftingQualityTweaksEnabled { get; set; } = false;

        [SettingPropertyInteger("{=KT_CTCQFBV}Quality Fine Base Value", 1, 10, Order = 1, RequireRestart = false,
            HintText = "{=KT_CTCQFBV_Desc}Set bonus base value for fine quality crafting [Native : 1].")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=KT_CTCQME}Quality Tweaks")]
        public int CraftingQualityFineValue { get; set; } = 1;

        [SettingPropertyInteger("{=KT_CTCQMBV}Quality Master Base Value", 1, 10, Order = 2, RequireRestart = false,
            HintText = "{=KT_CTCQMBV_Desc}Set bonus base value for master quality crafting [Native : 2].")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=KT_CTCQME}Quality Tweaks")]
        public int CraftingQualityMasterValue { get; set; } = 2;

        [SettingPropertyInteger("{=KT_CTCQLBV}Quality Legendary Base Value", 1, 10, Order = 3, RequireRestart = false,
            HintText = "{=KT_CTCQLBV_Desc}Set bonus base value for Legendary quality crafting [Native : 3].")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=KT_CTCQME}Quality Tweaks")]
        public int CraftingQualityLegendaryValue { get; set; } = 2;
        #endregion //~ Crafting Quality Bonus

        //~ Xp Modifiers
        #region XP Modifiers
        [SettingPropertyBool("{=KTMCM_SMRXP}Smithing Xp Multipliers" + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_SMRXPH}Enables xp multipliers to increase or decrease the xp gained per action.")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=KTMCM_CXP}XP")]
        public bool SmithingXpModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_SMRXPM}Refining Xp", 0.1f, 5.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_SMRXPMH}Multiply Refining Xp by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=KTMCM_CXP}XP")]
        public float SmithingRefiningXpValue { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_SMSXPM}Smelting Xp", 0.1f, 5.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_SMSXPMH}Multiply Smelting Xp by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=KTMCM_CXP}XP")]
        public float SmithingSmeltingXpValue { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_SMCXPM}Smithing Xp", 0.1f, 5.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_SMCXPMH}Multiply Smithing Xp by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=KTMCM_CXP}XP")]
        public float SmithingSmithingXpValue { get; set; } = 1.0f;
        #endregion
        #endregion

        //~ CharacterDevelopment
        #region CharacterDevelopment
        [SettingPropertyBool("{=KTMCM_CCharacterTw}Character Tweaks " + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_PCMH}Enables modifying Character variables.")]
        [SettingPropertyGroup("{=KTMCM_CCharacterTw}Character Tweaks")]
        public bool MCMCharacterDevlopmentModifiers { get; set; } = false;

        //~ Levels Per Attribute
        #region LevelsPerAttributePoint
        [SettingPropertyBool("{=KTMCM_PCMLPA}Levels Per Attribute modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PCMLPAH}Enable Number of Levels Per Attribute modifier")]
        [SettingPropertyGroup("{=KTMCM_CCharacterTw}Character Tweaks" + "/" + "{=KTMCM_CAttributes}Attributes" + "/" + "{=KTMCM_CLevel}Level")]
        public bool CharacterLevelsPerAttributeModifiers { get; set; } = false;

        [SettingPropertyInteger("{=KTMCM_PCMLPAN}Levels for Attribute", 1, 20, "0 Levels", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PCMLPANH}Number of Levels to acquire an attribute point [Native : 4].")]
        [SettingPropertyGroup("{=KTMCM_CCharacterTw}Character Tweaks" + "/" + "{=KTMCM_CAttributes}Attributes" + "/" + "{=KTMCM_CLevel}Level")]
        public int CharacterLevelsPerAttributeValue { get; set; } = 4;
        #endregion //~ Levels Per Attribute

        //~ Focus Per Level
        #region FocusPointsPerLevel
        [SettingPropertyBool("{=KTMCM_PCMFP}Focus points modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PCMFPH}Enables Focus points per level modifier.")]
        [SettingPropertyGroup("{=KTMCM_CCharacterTw}Character Tweaks" + "/" + "{=KTMCM_CFocus}Focus" + "/" + "{=KTMCM_CLevel}Level")]
        public bool CharacterFocusPerLevelModifiers { get; set; } = false;

        [SettingPropertyInteger("{=KTMCM_PCMFPPL}Focus points per level", 1, 20, "0 Focus", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PCMFPPLH}Number of Focus points per level [Native : 1].")]
        [SettingPropertyGroup("{=KTMCM_CCharacterTw}Character Tweaks" + "/" + "{=KTMCM_CFocus}Focus" + "/" + "{=KTMCM_CLevel}Level")]
        public int CharacterFocusPerLevelValue { get; set; } = 1;
        #endregion //~ Focus Per Level
        #endregion //~ CharacterDevelopment

        //~ Items
        #region Items
        [SettingPropertyBool("{=KTMCM_ItemTweaks}Item Tweaks " + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_ItemModifiersHint}Enables modifying Item weight and price.")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks")]
        public bool MCMItemModifiers { get; set; } = true;

        //~ Armor
        #region Armor
        [SettingPropertyBool("{=KTMCM_AM}Armor Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor")]
        public bool MCMArmorModifiers { get; set; } = true;

        //~ Weight
        #region Weight
        [SettingPropertyBool("{=KTMCM_AW}Armor Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor" + "/" + "{=KTMCM_Cweight}weight")]
        public bool ItemArmorWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemArmorTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemArmorTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemArmorTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemArmorTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemArmorTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemArmorTier6WeightMultiplier { get; set; } = 1.0f;

        #endregion

        //~ Price
        #region Price
        [SettingPropertyBool("{=KTMCM_AP}Armor Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor" + "/" + "{=KTMCM_CPrice}Price")]
        public bool ItemArmorValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemArmorTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemArmorTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemArmorTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemArmorTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemArmorTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CArmor}Armor" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemArmorTier6PriceMultiplier { get; set; } = 1.0f;

        #endregion

        #endregion

        //~ Food
        #region Food
        [SettingPropertyBool("{=KTMCM_FDM}Food by Morale Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CFood}Food")]
        public bool MCMFoodModifiers { get; set; } = true;

        //~ Weight
        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_FDM0}Morale 0 Multiplier", 0.1f, 5.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KTMCM_FDWMH}Multiply Food Weight for this morale Bonus by the multiplier")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CFood}Food" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemFoodWeightMorale0Multiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_FDM1}Morale 1 Multiplier", 0.1f, 5.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KTMCM_FDWMH}Multiply Food Weight for this morale Bonus by the multiplier")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CFood}Food" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemFoodWeightMorale1Multiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_FDM2}Morale 2 Multiplier", 0.1f, 5.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KTMCM_FDWMH}Multiply Food Weight for this morale Bonus by the multiplier")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CFood}Food" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemFoodWeightMorale2Multiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_FDM3}Morale 3 Multiplier", 0.1f, 5.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KTMCM_FDWMH}Multiply Food Weight for this morale Bonus by the multiplier")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CFood}Food" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemFoodWeightMorale3Multiplier { get; set; } = 1.0f;
        #endregion //~ Weight

        //~ Price
        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_FDM0}Morale 0 Multiplier", 0.1f, 5.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KTMCM_FDPMH}Multiply Food Price for this morale Bonus by the multiplier")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CFood}Food" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemFoodPriceMorale0Multiplier { get; set; } = 1.0f;


        [SettingPropertyFloatingInteger("{=KTMCM_FDM1}Morale 1 Multiplier", 0.1f, 5.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KTMCM_FDPMH}Multiply Food Price for this morale Bonus by the multiplier")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CFood}Food" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemFoodPriceMorale1Multiplier { get; set; } = 1.0f;


        [SettingPropertyFloatingInteger("{=KTMCM_FDM2}Morale 2 Multiplier", 0.1f, 5.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KTMCM_FDPMH}Multiply Food Price for this morale Bonus by the multiplier")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CFood}Food" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemFoodPriceMorale2Multiplier { get; set; } = 1.0f;


        [SettingPropertyFloatingInteger("{=KTMCM_FDM3}Morale 3 Multiplier", 0.1f, 5.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KTMCM_FDPMH}Multiply Food Price for this morale Bonus by the multiplier")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CFood}Food" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemFoodPriceMorale3Multiplier { get; set; } = 1.0f;
        #endregion //~ Price
        #endregion

        //~ Melee Weapons
        #region Melee Weapons
        [SettingPropertyBool("{=KTMCM_MWM}Melee Weapons Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons")]
        public bool MCMMeleeWeaponModifiers { get; set; } = false;

        //~ Weight
        #region Weight
        //[SettingPropertyBool("{=KTMCM_THMW}Melee Weapons Weight", IsToggle = true, Order = 0, RequireRestart = false,
        //    HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons" + "/" + "{=KTMCM_Cweight}weight")]
        public bool ItemMeleeWeaponWeightModifiers { get; set; } = true;

        //[SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
        //    HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemMeleeWeaponTier1WeightMultiplier { get; set; } = 1.0f;

        //[SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
        //    HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemMeleeWeaponTier2WeightMultiplier { get; set; } = 1.0f;

        //[SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
        //    HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemMeleeWeaponTier3WeightMultiplier { get; set; } = 1.0f;

        //[SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
        //    HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemMeleeWeaponTier4WeightMultiplier { get; set; } = 1.0f;

        //[SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
        //    HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemMeleeWeaponTier5WeightMultiplier { get; set; } = 1.0f;

        //[SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
        //    HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemMeleeWeaponTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion

        //~ Price
        #region Price
        [SettingPropertyBool("{=KTMCM_MWP}Melee Weapons Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons" + "/" + "{=KTMCM_CPrice}Price")]
        public bool ItemMeleeWeaponValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemMeleeWeaponTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
             HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemMeleeWeaponTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemMeleeWeaponTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemMeleeWeaponTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemMeleeWeaponTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CMW}Melee Weapons" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemMeleeWeaponTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion

        #endregion

        //~ Ranged Weapons
        #region Ranged Weapons
        [SettingPropertyBool("{=KTMCM_RWM}Ranged Weapons Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons")]
        public bool MCMRagedWeaponsModifiers { get; set; } = false;

        //~ Weight
        #region Weight
        //[SettingPropertyBool("{=KTMCM_BMW}Ranged Weapons Weight", IsToggle = true, Order = 0, RequireRestart = false,
        //    HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons" + "/" + "{=KTMCM_Cweight}weight")]
        public bool ItemRangedWeaponsWeightModifiers { get; set; } = true;

        //[SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
        //    HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemRangedWeaponsTier1WeightMultiplier { get; set; } = 1.0f;

        //[SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
        //    HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemRangedWeaponsTier2WeightMultiplier { get; set; } = 1.0f;

        //[SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
        //    HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemRangedWeaponsTier3WeightMultiplier { get; set; } = 1.0f;

        //[SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
        //    HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemRangedWeaponsTier4WeightMultiplier { get; set; } = 1.0f;

        //[SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
        //    HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemRangedWeaponsTier5WeightMultiplier { get; set; } = 1.0f;

        //[SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
        //    HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemRangedWeaponsTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion

        //~ Price
        #region Price
        [SettingPropertyBool("{=KTMCM_RWP}Ranged Weapons Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons" + "/" + "{=KTMCM_CPrice}Price")]
        public bool ItemRangedWeaponsValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemRangedWeaponsTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemRangedWeaponsTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemRangedWeaponsTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemRangedWeaponsTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemRangedWeaponsTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CRW}Ranged Weapons" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemRangedWeaponsTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion
        #endregion

        //~ Arrows
        #region Arrows
        [SettingPropertyBool("{=KPM_AME}Arrows Multipliers Enabled", IsToggle = true, Order = 1, RequireRestart = false,
            HintText = "{=KPM_AMEH}Enables arrows multipliers for price and stack size")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Arrows}Arrows")]
        public bool ArrowMultipliersEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KPM_ASSM}Arrows Stack Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KPM_SSMH}Multiply stack sizes by the multiplier [Native: 1.0(100%)].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Arrows}Arrows")]
        public float ArrowMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KPM_APM}Arrows Price Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KPM_PMH}Multiply price by the multiplier [Native: 1.0(100%)].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Arrows}Arrows")]
        public float ArrowValueMultiplier { get; set; } = 1.0f;
        //[SettingPropertyFloatingInteger("{=KPM_WM}Weight Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
        //    HintText = "{=KPM_WMH}Multiply item weight by the multiplier [Native: 1.0(100%)]")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Arrows}Arrows")]
        //public float ArrowWeightMultiplier { get; set; } = 1.0f;
        #endregion

        //~ Bolts
        #region Bolts
        [SettingPropertyBool("{=KPM_BME}Bolts Multiplier Enabled", IsToggle = true, Order = 1, RequireRestart = false,
            HintText = "{=KPM_BMEH}Enables Bolts stack Multiplier")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Bolts}Bolts")]
        public bool BoltsMultipliersEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KPM_BSSM}Bolts Stack Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KPM_SSMH}Multiply stack sizes by the multiplier [Native: 1.0(100%)].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Bolts}Bolts")]
        public float BoltsMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KPM_BPM}Bolts Price Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KPM_PMH}Multiply price by the multiplier [Native: 1.0(100%)].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Bolts}Bolts")]
        public float BoltsValueMultiplier { get; set; } = 1.0f;

        //[SettingPropertyFloatingInteger("{=KPM_WM}Weight Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
        //    HintText = "{=KPM_WMH}Multiply item weight by the multiplier [Native: 1.0(100%)].")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Bolts}Bolts")]
        public float BoltsWeightMultiplier { get; set; } = 1.0f;
        #endregion

        //~ Thrown
        #region Thrown
        [SettingPropertyBool("{=KPM_TWME}Thrown Multipliers Enabled", IsToggle = true, Order = 1, RequireRestart = false,
            HintText = "{=KPM_TWMEH}Enables Thrown Multipliers")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Thrown}Thrown")]
        public bool ThrownMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KPM_TSSM}Thrown Stack Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KPM_SSMH}Multiply stack sizes by the multiplier [Native: 1.0(100%)].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Thrown}Thrown")]
        public float ThrownMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KPM_TPM}Thrown Price Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KPM_PMH}Multiply price by the multiplier [Native: 1.0(100%)].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Thrown}Thrown")]
        public float ThrownValueMultiplier { get; set; } = 1.0f;

        //[SettingPropertyFloatingInteger("{=KPM_WM}Weight Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
        //    HintText = "{=KPM_WMH}Multiply item weight by the multiplier [Native: 1.0(100%)].")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Thrown}Thrown")]
        public float ThrownWeightMultiplier { get; set; } = 1.0f;

        //[SettingPropertyBool("{=KPM_MTWFE}Thrown Mission stack Fix Enabled", Order = 1, RequireRestart = false,
        //    HintText = "{KPM_MTWFEH}Enables a temp fix for thrown weapon stack sizes in missions")]
        //[SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Thrown}Thrown")]
        public bool ThrownMissionFixMultiplierEnabled { get; set; } = false;
        #endregion

        #region Bullets
        [SettingPropertyBool("{=KPM_BWME}Bullets Multipliers Enabled", IsToggle = true, Order = 1, RequireRestart = true,
            HintText = "{=KPM_BWMEH}Enables Bullets Multipliers")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Bullets}Bullets")]
        public bool BulletsMultiplierEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KPM_SSM}Stack Size Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KPM_SSMH}Multiply stack sizes by the multiplier [Native: 1.0(100%)].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Bullets}Bullets")]
        public float BulletsMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KPM_PM}Price Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KPM_PMH}Multiply price by the multiplier [Native: 1.0(100%)].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Bullets}Bullets")]
        public float BulletsValueMultiplier { get; set; } = 1.0f;

        /*[SettingPropertyFloatingInteger("{=KPM_WM}Weight Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "{=KPM_WMH}Multiply item weight by the multiplier [Native: 1.0(100%)].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KPM_Bullets}Bullets")]*/
        public float BulletsWeightMultiplier { get; set; } = 1.0f;

        /*
        [SettingPropertyBool("{=KPM_MTWFE}Thrown Mission stack Fix Enabled", Order = 1, RequireRestart = false,
            HintText = "{KPM_MTWFEH}Enables a temp fix for thrown weapon stack sizes in missions")]
        [SettingPropertyGroup("{=KPM_WorkAround}Work around")]
        public bool BulletsMissionFixMultiplierEnabled { get; set; } = false;*/
        #endregion


        //~ Trade Goods
        #region TradeGoods
        [SettingPropertyBool("{=KTMCM_TGM}Trade Goods Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CTradeGoods}Trade Goods")]
        public bool MCMTradeGoodsModifiers { get; set; } = true;

        //~ Weight
        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_Cweight}Weight", 0.1f, 5.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CTradeGoods}Trade Goods" + "/" + "{=KTMCM_Cweight}weight")]
        public float ItemTradeGoodsWeightMultiplier { get; set; } = 1.0f;
        #endregion

        //~ Price
        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_CPrice}Price", 0.1f, 5.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_ItemTweaks}Item Tweaks" + "/" + "{=KTMCM_CTradeGoods}Trade Goods" + "/" + "{=KTMCM_CPrice}Price")]
        public float ItemTradeGoodsPriceMultiplier { get; set; } = 1.0f;
        #endregion
        #endregion //~ TradeGoods
        #endregion //~ End Items

        //~ ItemLocks 
        #region ItemLocks
        [SettingPropertyBool("{=KTMCM_AL}Item Auto Locks " + "*", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ALH}Allows for auto locking horses, food , and smithing materials.")]
        [SettingPropertyGroup("{=KTMCM_CAutoLocks}Auto Locks")]
        public bool MCMAutoLocks { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_ALHS}Horses", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ALHSH}Auto lock horses except lame horses.")]
        [SettingPropertyGroup("{=KTMCM_CAutoLocks}Auto Locks")]
        public bool autoLockHorses { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_ALF}Food", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ALFH}Auto lock all food.")]
        [SettingPropertyGroup("{=KTMCM_CAutoLocks}Auto Locks")]
        public bool autoLockFood { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_ALIB1}Crude Iron", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ALIB1H}Auto lock Crude Iron.")]
        [SettingPropertyGroup("{=KTMCM_CAutoLocks}Auto Locks")]
        public bool autoLockIronBar1 { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_ALIB2}Wrought Iron", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ALIB2H}Auto lock Wrought Iron.")]
        [SettingPropertyGroup("{=KTMCM_CAutoLocks}Auto Locks")]
        public bool autoLockIronBar2 { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_ALIB3}Iron", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ALIB3H}Auto lock Iron.")]
        [SettingPropertyGroup("{=KTMCM_CAutoLocks}Auto Locks")]
        public bool autoLockIronBar3 { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_ALIB4}Steel", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ALIB4H}Auto lock Steel.")]
        [SettingPropertyGroup("{=KTMCM_CAutoLocks}Auto Locks")]
        public bool autoLockIronBar4 { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_ALIB5}Fine Steel", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ALIB5H}Auto lock Fine Steel.")]
        [SettingPropertyGroup("{=KTMCM_CAutoLocks}Auto Locks")]
        public bool autoLockIronBar5 { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_ALIB6}Thamaskene Steel", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ALIB6H}Auto lock Thamaskene Steel.")]
        [SettingPropertyGroup("{=KTMCM_CAutoLocks}Auto Locks")]
        public bool autoLockIronBar6 { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_ALIO}Iron Ore ", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ALIOH}Auto lock Iron Ore .")]
        [SettingPropertyGroup("{=KTMCM_CAutoLocks}Auto Locks")]
        public bool autoLockIronOre { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_ALSO}Silver Ore", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ALSOH}Auto lock Silver Ore.")]
        [SettingPropertyGroup("{=KTMCM_CAutoLocks}Auto Locks")]
        public bool autoLockSilverOre { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_ALHW}Hardwood", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ALHWH}Auto lock Hardwood.")]
        [SettingPropertyGroup("{=KTMCM_CAutoLocks}Auto Locks")]
        public bool autoLockHardwood { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_ALCC}Charcoal", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ALCCH}Auto lock Charcoal.")]
        [SettingPropertyGroup("{=KTMCM_CAutoLocks}Auto Locks")]
        public bool autoLockCharcol { get; set; } = false;


        #endregion

        //~ Killing Bandits Raises Relationships
        #region Killing Bandits Raises Relationships
        [SettingPropertyBool("{=KTMCM_CKBRR}Killing Bandits Raises Relationships " + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_CKBRRH}Edit killing kandits raises relationships settings.")]
        [SettingPropertyGroup("{=KTMCM_CKBRR}Killing Bandits Raises Relationships")]
        public bool MCMKillingBanditsEnabled { get; set; } = false;


        [SettingPropertyInteger("{=KTMCM_RBKBGB}Groups for bonus", 1, 50, "0 Parties", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKBGBH}Number of bandit groups you must destroy before you gain relation.")]
        [SettingPropertyGroup("{=KTMCM_CKBRR}Killing Bandits Raises Relationships" + "/" + "{=KTMCM_CBandits}Bandits")]
        public int GroupsOfBandits { get; set; } = 1;

        [SettingPropertyInteger("{=KTMCM_RBKBRI}Relationship Increase", 0, 50, "0 Points", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKBRIH}The base value that your relationship will increase by when it increases.")]
        [SettingPropertyGroup("{=KTMCM_CKBRR}Killing Bandits Raises Relationships" + "/" + "{=KTMCM_CBandits}Bandits")]
        public int RelationshipIncrease { get; set; } = 0;

        [SettingPropertyInteger("{=KTMCM_RBKBR}Radius", 500, 5000, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKBRH}This is the size of the radius inside which villages and towns will be affected by the relationship increase.")]
        [SettingPropertyGroup("{=KTMCM_CKBRR}Killing Bandits Raises Relationships" + "/" + "{=KTMCM_CBandits}Bandits")]
        public int Radius { get; set; } = 500;

        [SettingPropertyBool("{=KTMCM_RBKBSB}Size Bonus", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKBSBH}Enable Size bonus modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CKBRR}Killing Bandits Raises Relationships" + "/" + "{=KTMCM_CBandits}Bandits")]
        public bool SizeBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_RBKBSBM}Size bonus Modifier", 0.0f, 1.0f, RequireRestart = false,
            HintText = "{=KTMCM_RBKBSBMH}Multiply the Size Bonus by the number of bandits you have killed since you last gained relationship. this will then be multiplied by the base Relationship Increase to give your final increase value.")]
        [SettingPropertyGroup("{=KTMCM_CKBRR}Killing Bandits Raises Relationships" + "/" + "{=KTMCM_CBandits}Bandits")]
        public float SizeBonus { get; set; } = 0.05f;

        [SettingPropertyBool("{=KTMCM_RBKBOPP}Only Bandits with Prisoners", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKBOPPH}Enable relationship bonuses only for bandit parties with prisoners.")]
        [SettingPropertyGroup("{=KTMCM_CKBRR}Killing Bandits Raises Relationships" + "/" + "{=KTMCM_CBandits}Bandits")]
        public bool PrisonersOnly { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_RBKBBP}Bandits Parties", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKBBPH}Enable relationship bonuses for bandit parties.")]
        [SettingPropertyGroup("{=KTMCM_CKBRR}Killing Bandits Raises Relationships" + "/" + "{=KTMCM_CBandits}Bandits")]
        public bool IncludeBandits { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_RBKBOP}Outlaw Parties", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKBOPH}Enable relationship bonuses for outlaw parties.")]
        [SettingPropertyGroup("{=KTMCM_CKBRR}Killing Bandits Raises Relationships" + "/" + "{=KTMCM_CBandits}Bandits")]
        public bool IncludeOutlaws { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_RBKMGP}Mafia Parties", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKMGPH}Enable relationship bonuses for mafia parties.")]
        [SettingPropertyGroup("{=KTMCM_CKBRR}Killing Bandits Raises Relationships" + "/" + "{=KTMCM_CBandits}Bandits")]
        public bool IncludeMafia { get; set; } = false;

        [SettingPropertyBool("{=Titan_Settings_000008}Report Relation Gain", Order = 0, RequireRestart = false,
            HintText = "{=Titan_Settings_000008_desc}Set whether or not to report the relations gained from killing bandits to the player.")]
        [SettingPropertyGroup("{=KTMCM_CKBRR}Killing Bandits Raises Relationships" + "/" + "{=KTMCM_CBandits}Bandits")]
        public bool KillingBanditsRelationReportEnabled { get; set; } = true;

        #endregion //~ Killing Bandits Raises Relationships

        //~ Kingdom Tweaks
        #region Kingdom Tweaks
        [SettingPropertyBool("{=BT_Settings_005000}Kingdom Tweaks " + "*", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_KTH}Enable modifying kingdom tweak variables..")]
        [SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks")]
        public bool MCMKingdomTweaksEnabled { get; set; } = true;

        //~ Lord Bartering
        #region Lord Bartering
        [SettingPropertyBool("{=BT_Settings_005100}Lord Bartering " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_005100_Desc}Enables tweaks which affect bartering (Marriage, Factions Joining You, etc.)"),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005100}Lord Bartering")]
        public bool BarterablesTweaksEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_005101}Faction Joining Barter Adjustment", 0.01f, 2f, "0%", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_005101_Desc}Adjust the % cost of swaying a faction to join your kingdom. Native value is 100% (no change)."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005100}Lord Bartering")]
        public float BarterablesJoinKingdomAsClanAdjustment { get; set; } = 1;

        [SettingPropertyBool("{=BT_Settings_005102}Relationship favored Faction Joining Barter", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_005102_Desc}An alternate formula for calculating cost of swaying a faction to join your kingdom, with more emphasis on relationsip. [The higher your relationship to the lord, the cheaper the barter]."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005100}Lord Bartering")]
        public bool BarterablesJoinKingdomAsClanAltFormulaEnabled { get; set; } = false;
        #endregion //~ Lord Bartering

        //~ Balancing Tweaks
        #region Balancing Tweaks
        [SettingPropertyBool("{=BT_Settings_005200}Faction Balancing " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_005200_Desc}Enables tweaks which affect the balancing of kingdoms."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing", GroupOrder = 2)]
        public bool KingdomBalanceStrengthEnabled { get; set; } = false;

        //[SettingPropertyBool("{=BT_Settings_005236}Faction Balancing Harmony " + "*", Order = 1, RequireRestart = true, 
        //    HintText = "{=BT_Settings_005200_Desc}Enables faction balancing of kingdoms via hormony patches instead of model."),
        //    SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing", GroupOrder = 2)]
        public bool KingdomBalanceStrengthHarmonyEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms", Order = 2, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_005201_Desc}Enables tweaks which affect the balancing of kingdoms in vanilla game."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public bool KingdomBalanceStrengthVanEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_005202}Vlandia Balancing Strength", -0.5f, 0.5f, "0%", Order = 9, RequireRestart = false,
            HintText = "{=BT_Settings_005202_Desc}Balancing strength for vlandia. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float VlandiaBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005203}Sturgia Balancing Strength", -0.5f, 0.5f, "0%", Order = 8, RequireRestart = false,
            HintText = "{=BT_Settings_005203_Desc}Balancing strength for sturgia. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float SturgiaBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005204}Battania Balancing Strength", -0.5f, 0.5f, "0%", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_005204_Desc}Balancing strength for battania. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float BattaniaBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005205}Northern Empire Balancing Strength", -0.5f, 0.5f, "0%", Order = 5, RequireRestart = false,
            HintText = "{=BT_Settings_005205_Desc}Balancing strength for northern empire. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float Empire_N_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005206}Southern Empire Balancing Strength", -0.5f, 0.5f, "0%", Order = 6, RequireRestart = false,
            HintText = "{=BT_Settings_005206_Desc}Balancing strength for southern empire. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float Empire_S_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005207}Western Empire Balancing Strength", -0.5f, 0.5f, "0%", Order = 7, RequireRestart = false,
            HintText = "{=BT_Settings_005207_Desc}Balancing strength for western empire. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float Empire_W_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005208}Aserai Balancing Strength", -0.5f, 0.5f, "0%", Order = 1, RequireRestart = false,
            HintText = "{=BT_Settings_005208_Desc}Balancing strength for aserai. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float AseraiBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005209}Khuzait Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_005209_Desc}Balancing strength for khuzait. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float KhuzaitBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005210}Player Kingdom Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_005210_Desc}Balancing strength for player kingdom. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float PlayerBoost { get; set; } = 0.00f;

        [SettingPropertyBool("{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms", Order = 3, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_005211_Desc}Enables tweaks which affect the balancing of kingdoms in the mod Calradia Expanded Kingdoms."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms", GroupOrder = 2)]
        public bool KingdomBalanceStrengthCEKEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_005212}Nordlings Balancing Strength", -0.5f, 0.5f, "0%", Order = 9, RequireRestart = false,
            HintText = "{=BT_Settings_005212_Desc}Balancing strength for nordlings. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float NordlingsBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005213}Vagir Balancing Strength", -0.5f, 0.5f, "0%", Order = 8, RequireRestart = false,
            HintText = "{=BT_Settings_005213_Desc}Balancing strength for vagir. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float VagirBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005214}Royalist Vlandia Balancing Strength", -0.5f, 0.5f, "0%", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_005214_Desc}Balancing strength for royalist vlandia. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float RoyalistVlandiaBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005215}Apolssaly Balancing Strength", -0.5f, 0.5f, "0%", Order = 5, RequireRestart = false,
            HintText = "{=BT_Settings_005215_Desc}Balancing strength for apolssaly. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float ApolssalyBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005216}Lyrion Balancing Strength", -0.5f, 0.5f, "0%", Order = 6, RequireRestart = false,
            HintText = "{=BT_Settings_005216_Desc}Balancing strength for lyrion. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float LyrionBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005217}Khergit Balancing Strength", -0.5f, 0.5f, "0%", Order = 7, RequireRestart = false,
            HintText = "{=BT_Settings_005217_Desc}Balancing strength for khergit. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float RebelKhuzaitBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005218}Paleician Balancing Strength", -0.5f, 0.5f, "0%", Order = 1, RequireRestart = false,
            HintText = "{=BT_Settings_005218_Desc}Balancing strength for paleician. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float PaleicianBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005219}Ariorum Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_005219_Desc}Balancing strength for ariorum. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float AriorumBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005220}Calradian Empire Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_005220_Desc}Balancing strength for calradian empire. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Empire_S_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005221}Dryatican Republic Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_005221_Desc}Balancing strength for dryatican republic. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Empire_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005222}Battania Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_005222_Desc}Balancing strength for battania. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Battania_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005223}Cortanian Vlandia Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_005223_Desc}Balancing strength for cortanian vlandia. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Vlandia_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005224}Sturgia Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_005224_Desc}Balancing strength for sturgia. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Sturgia_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005225}Khuzait Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_005225_Desc}Balancing strength for khuzait. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Khuzait_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005226}Aserai Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_005226_Desc}Balancing strength for aserai. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Aserai_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005227}Western Empire Balancing Strength", -0.5f, 0.5f, "0%", Order = 7, RequireRestart = false,
            HintText = "{=BT_Settings_005227_Desc}Balancing strength for western empire. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Empire_W_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005228}Player Kingdom Balancing Strength", -0.5f, 0.5f, "0%", Order = 7, RequireRestart = false,
            HintText = "{=BT_Settings_005228_Desc}Balancing strength for player kingdom. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Player_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyBool("{=BT_Settings_005229}Party Sizes Balancing", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_005229_Desc}Modifier for max party sizes (1.0 x Balancing Strength)."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing")]
        public bool BalancingPartySizeTweaksEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_005230}Party Count Limit Balancing", Order = 5, RequireRestart = false,
            HintText = "{=BT_Settings_005230_Desc}Modifier for max party limit (1.0 x Balancing Strength)."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing")]
        public bool BalancingPartyLimitTweaksEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_005231}Village Food Production Balancing", Order = 6, RequireRestart = false,
            HintText = "{=BT_Settings_005231_Desc}Modifier for daily production of food goods in villages (1.0 x Balancing Strength)."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing")]
        public bool BalancingFoodTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_005232}Faster Recruitment Balancing", Order = 7, RequireRestart = false,
            HintText = "{=BT_Settings_005232_Desc}Flat % bonus chance of new recruits to spawn in settlements (0.75 x Balancing Strength)."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing")]
        public bool BalancingTimeRecruitsTweaksEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_005233}Taxation Efficiency Balancing", Order = 8, RequireRestart = false,
            HintText = "{=BT_Settings_005233_Desc}Modifier for tax income from prosperity in towns and castles and trade tax income in villages (1.25 x Balancing Strength)."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing")]
        public bool BalancingTaxTweaksEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_005234}Wage costs Balancing", Order = 9, RequireRestart = false,
            HintText = "{=BT_Settings_005234_Desc}Modifier for troop and garrison wages (1.0 x Balancing Strength)."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing")]
        public bool BalancingWagesTweaksEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_005235}Quality of Recruitment Balancing", Order = 10, RequireRestart = false,
            HintText = "{=BT_Settings_005235_Desc}Increases the chance for upgraded recruits (1.0 x Balancing Strength). No effect if balancing strength is lower than 0% (no decrease in chance for upgrades)."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing")]
        public bool BalancingUpgradeTroopsTweaksEnabled { get; set; } = false;
        #endregion //~ Balancing Tweaks
        #endregion //~ Kingdom Tweaks

        //~ Miscellaneous
        #region Miscellaneous

        [SettingPropertyBool("{=BT_Settings_009001}Disable Quest Troops Affecting Morale" + "*", Order = 1, RequireRestart = true,
            HintText = "{=BT_Settings_009001_Desc}When enabled, quest troops such as Borrowed Troop in your party are ignored when party morale is calculated."),
            SettingPropertyGroup("{=BT_Settings_009000}Misc", GroupOrder = 99)]
        public bool QuestCharactersIgnorePartySize { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_009002}Show Number of Days of Food" + "*", Order = 2, RequireRestart = true,
            HintText = "{=BT_Settings_009002_Desc}Changes the number showing how much food you have to instead show how many days' worth of food you have. (Bottom right of campaign map UI)."),
            SettingPropertyGroup("{=BT_Settings_009000}Misc")]
        public bool ShowFoodDaysRemaining { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_009003}Campaign Speed Fast Forward", 2, 32, Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_009003_Desc}Sets the campaign speed in fast forward mode. Vanilla is 4."),
            SettingPropertyGroup("{=BT_Settings_009000}Misc")]
        public int CampaignSpeed { get; set; } = 4;

        [SettingPropertyBool("{=BT_Settings_003103}Enable Unlimited Wanderers Patch" + "*", Order = 0, RequireRestart = true,
            HintText = "{=BT_Settings_003103_Desc}Removes the soft cap on the maximum number of potential companions who can spawn. Native limits the # of wanderers to ~25. This will remove that limit. Note: Requires a new campaign to take effect, as the cap is set when a new game is generated. Credit to Bleinz for his UnlimitedWanderers mod.")]
        [SettingPropertyGroup("{=BT_Settings_009000}Misc")]
        public bool UnlimitedWanderersPatch { get; set; } = false;

        /* Disable in 1.5.7.2 until we understand changes to the main quest.
        [SettingPropertyBool("Enable Auto-Extension of the 'Stop the Conspiracy' Quest", RequireRestart = false, HintText = "Automatically extends the timer of the 'Stop the Conspiracy' quest as TW hasn't finished it yet.")]
        public bool TweakedConspiracyQuestTimerEnabled { get; set; } = true;
        */
        #endregion

        //~ Party Speeds
        #region Kaoses Party Speeds
    [SettingPropertyFloatingInteger("{=KPS_MSL}Minimum Speed limit", 0.1f, 3.5f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_MSLH}Set the lowest speed allowed for any party, if a parties speed would fall below this it will changed to the limit. [Native: 1.0f]")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_Gloabal}Global", GroupOrder = 3)]
        public float KaosesmininumSpeedAmount { get; set; } = 1.0f;

        //~ New Dynamic Decrease
        #region New Dynamic Decrease
        [SettingPropertyBool("{=KPS_DSME}Dynamic Fleeing Speed Modifiers Enabled", IsToggle = true, Order = 1, RequireRestart = true,
            HintText = "{=KPS_DSMEH}Enables the new method to reduce a parties speed so long as they are fleeing.")]
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_DynFlee}Dynamic Fleeing", GroupOrder = 1)]
        public bool KaosesDynamicSpeedModifiersEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KPS_SMPH}Speed Modifier Per Hour/Reduction", -2.00f, 0.00f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_SMPHH}Modify Fleeing parties speed by the specified amount.")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_DynFlee}Dynamic Fleeing")]
        public float DynamicFleeingSpeedReductionAmount { get; set; } = -0.10f;

        [SettingPropertyInteger("{=KPS_HPC}Hours Per Change", 1, 24, "0 Hours", RequireRestart = false,
             HintText = "{=KPS_HPCH}Set how many hours between speed reductions", Order = 3)]
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_DynFlee}Dynamic Fleeing")]
        public int DynamicFleeingSpeedReductionHours { get; set; } = 1;

        #endregion //~ New Dynamic Decrease

        //~ Old Static
        #region Old Static
        #region PartySpeed
        [SettingPropertyBool("{=KPS_SSME}Static Speed Modifiers Enabled", IsToggle = true, Order = 1, RequireRestart = true,
            HintText = "{=KPS_SSMEH}Enables static Kaoses party speed Modifiers")]
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change", GroupOrder = 2)]
        public bool KaosesStaticSpeedModifiersEnabled { get; set; } = false;

        #region PartySpeed Looters
        [SettingPropertyFloatingInteger("{=KPS_SSLM}Looter Speed Modifier", -3.5f, 3.5f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_SSLMH}Modify Looters parties speed by the specified amount.")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KTMCM_CBandits}Bandits")]
        public float LooterSpeedReductionAmount { get; set; } = 0.0f;
        #endregion PartySpeed Looters

        #region PartySpeed Steppe
        [SettingPropertyFloatingInteger("{=KPS_SSMS}Steppe Bandits Speed Modifier", -3.5f, 3.5f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_SSMSH}Modify Steppe parties speed by the specified amount.")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KTMCM_CBandits}Bandits")]
        public float SteppeSpeedReductionAmount { get; set; } = 0.0f;
        #endregion PartySpeed Steppe

        #region PartySpeed Dessert
        [SettingPropertyFloatingInteger("{=KPS_SSD}Desert Bandits Speed Modifier", -3.5f, 3.5f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_SSDH}Modify Desert parties speed by the specified amount.")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KTMCM_CBandits}Bandits")]
        public float DesertSpeedReductionAmount { get; set; } = 0.0f;
        #endregion PartySpeed Dessert

        #region PartySpeed Mountain
        [SettingPropertyFloatingInteger("{=KPS_SSM}Mountain Bandits Speed Modifier", -3.5f, 3.5f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_SSMH}Modify Mountain parties speed by the specified amount.")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KTMCM_CBandits}Bandits")]
        public float MountainSpeedReductionAmount { get; set; } = 0.0f;
        #endregion PartySpeed Mountain

        #region PartySpeed Forest
        [SettingPropertyFloatingInteger("{=KPS_SSF}Forest Bandits Speed Modifier", -3.5f, 3.5f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_SSFH}Modify Forest parties speed by the specified amount.")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KTMCM_CBandits}Bandits")]
        public float ForestSpeedReductionAmount { get; set; } = 0.0f;
        #endregion PartySpeed Forest

        #region PartySpeed SeaRaiders
        [SettingPropertyFloatingInteger("{=KPS_SSSR}SeaRaiders Speed Modifier", -3.5f, 3.5f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_SSSRH}Modify SeaRaiders parties speed by the specified amount.")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KTMCM_CBandits}Bandits")]
        public float SeaRaiderSpeedReductionAmount { get; set; } = 0.0f;
        #endregion PartySpeed SeaRaiders

        #region PartySpeed Caravans
        [SettingPropertyBool("{=KPS_SSCE}Caravan Modifiers Enabled", IsToggle = true, Order = 1, RequireRestart = false,
            HintText = "{=KPS_SSCEH}Enables Caravan party speed Modifiers")]
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KPS_Caravan}Caravan")]
        public bool CaravanSpeedReductiontEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KPS_SSC}Caravan Speed Modifier", -3.5f, 3.5f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_SSCH}Modify Caravan parties speed by the specified amount.")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KPS_Caravan}Caravan")]
        public float CaravanSpeedReductionAmount { get; set; } = 0.0f;
        #endregion PartySpeed Caravans

        #region PartySpeed EliteCaravans
        [SettingPropertyFloatingInteger("{=KPS_SSCEE}Elite Caravan Speed Modifier", -3.5f, 3.5f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_SSCEEH}Modify Elite Caravan parties speed by the specified amount.")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KPS_Caravan}Caravan")]
        public float EliteCaravanSpeedReductionAmount { get; set; } = 0.0f;
        #endregion PartySpeed EliteCaravans

        #region PartySpeed Villagers
        [SettingPropertyBool("{=KPS_SSVE}Villagers Modifiers Enabled", IsToggle = true, Order = 1, RequireRestart = false,
            HintText = "{=KPS_SSVEH}Enables Villagers party speed Modifiers")]
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KPS_Villagers}Villagers")]
        public bool VillagerSpeedReductiontEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KPS_SSV}Villager Speed Modifier", -3.5f, 3.5f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_SSVH}Modify Villagers parties speed by the specified amount.")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KPS_Villagers}Villagers")]
        public float VillagerSpeedReductionAmount { get; set; } = 0.0f;
        #endregion PartySpeed Villagers

        #region PartySpeed Player
        [SettingPropertyBool("{=KPS_SSPE}Player Modifiers", IsToggle = true, Order = 1, RequireRestart = false,
            HintText = "{=KPS_SSPEH}Enables Player party speed Modifiers")]
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KTMCM_CPlayer}Player")]
        public bool PlayerSpeedReductiontEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KPS_SSP}Player Speed Modifier", -3.5f, 3.5f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_SSPH}Modify Player parties speed by the specified amount.")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KTMCM_CPlayer}Player")]
        public float PlayerSpeedReductionAmount { get; set; } = 0.0f;
        #endregion PartySpeed Player

        #region PartySpeed Player Clan Members
        [SettingPropertyFloatingInteger("{=KPS_SSPC}Player Clan Speed Modifier", -3.5f, 3.5f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_SSPCH}Modify Player Clan parties speed by the specified amount.")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KTMCM_CPlayer}Player")]
        public float PlayerClanSpeedReductionAmount { get; set; } = 0.0f;
        #endregion PartySpeed PlayerCompanions

        #region PartySpeed Kingdom
        [SettingPropertyBool("{=KPS_SSKE}Kingdom Modifiers", IsToggle = true, Order = 1, RequireRestart = false,
            HintText = "{=KPS_SSKEH}Enables Kingdom party speed Modifiers")]
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KPS_Kingdom}Kingdom")]
        public bool KingdomSpeedReductiontEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KPS_SSK}Kingdom Speed Modifier", -3.5f, 3.5f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_SSKH}Modify Kingdom parties speed by the specified amount.")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KPS_Kingdom}Kingdom")]
        public float KingdomSpeedReductionAmount { get; set; } = 0.0f;
        #endregion PartySpeed Kingdom

        #region PartySpeed MinorFactions
        [SettingPropertyFloatingInteger("{=KPS_SSKOE}Other Kingdom Speed Modifier", -3.5f, 3.5f, Order = 2, RequireRestart = false,
            HintText = "{=KPS_SSKOEH}Modify Other Kingdom parties speed by the specified amount.")] //, "#0%"
        [SettingPropertyGroup("{=KPS_PartySpeeds}Party Speeds" + "/" + "{=KPS_StaticChange}Static Change" + "/" + "{=KPS_Kingdom}Kingdom")]
        public float OtherKingdomSpeedReductionAmount { get; set; } = 0.0f;
        #endregion PartySpeed MinorFactions
        #endregion PartySpeed
        #endregion //~ Old Static
        #endregion //~ Kaoses Party Speeds

        //~ Party Tweaks
        #region Party Tweaks
        [SettingPropertyBool("{=BT_Settings_006000}Party Tweaks", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PTH}Enable modifying party tweak variables..")]
        [SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks")]
        public bool MCMPartyTweaksEnabled { get; set; } = true;

        //~ Caravan Tweaks
        #region Caravan Tweaks
        [SettingPropertyBool("{=BT_Settings_006200}Player Caravan Party Size" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_006200_Desc}Applies a configured value to your caravan party size."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006200}Player Caravan Party Size", GroupOrder = 2)]
        public bool PlayerCaravanPartySizeTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_006201}Player Caravan Party Size Amount", 30, 100, "0 Troops", Order = 2,
            HintText = "{=BT_Settings_006201_Desc}Native: 30. Be aware that bigger parties are also slower parties."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006200}Player Caravan Party Size")]
        public int PlayerCaravanPartySize { get; set; } = 30;
        #endregion //~ Caravan Tweaks

        //~ Party Size Tweaks
        #region Party Size Tweaks

        [SettingPropertyBool("{=BT_Settings_006300}Party Size " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_006300_Desc}Applies a bonues to you and AI lord's party size based on leadership and steward skills."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size", GroupOrder = 3)]
        public bool PartySizeTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_006301}Leadership Bonus " + "*", Order = 2, IsToggle = true, RequireRestart = false,
            HintText = "{=BT_Settings_006301_Desc}Applies a bonus equal to the set percentage of your leadership skill to your party size."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "/" + "{=BT_Settings_006301}Leadership Bonus")]
        public bool LeadershipPartySizeBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_006302}Leadership Bonus Player", 0f, 2f, "0%", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_006302_Desc}Applies a bonus equal to the set percentage of your leadership skill to your party size."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "/" + "{=BT_Settings_006301}Leadership Bonus")]
        public float LeadershipPartySizeBonus { get; set; } = 0.0f;

        [SettingPropertyBool("{=BT_Settings_006303}Steward Bonus " + "*", Order = 4, IsToggle = true, RequireRestart = true,
            HintText = "{=BT_Settings_006303_Desc}Applies a bonus equal to the set percentage of your steward skill to your party size."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "/" + "{=BT_Settings_006303}Steward Bonus", GroupOrder = 2)]
        public bool StewardPartySizeBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_006304}Steward Bonus Player", 0f, 2f, "0%", Order = 5, RequireRestart = false,
            HintText = "{=BT_Settings_006304_Desc}Applies a bonus equal to the set percentage of your steward skill to your party size."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "/" + "{=BT_Settings_006303}Steward Bonus")]
        public float StewardPartySizeBonus { get; set; } = 0f;

        [SettingPropertyFloatingInteger("{=BT_Settings_006305}Party Size Relation Player-AI", 0f, 2f, "0%", Order = 6, RequireRestart = false,
            HintText = "{=BT_Settings_006305_Desc}The percentage of the party size bonus set for the player to also apply for ai lords. 0% results in no bonus for ai. You may also want to increase food production amounts (Village Production, bigger demand)."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size")]
        public float PartySizeTweakAIFactor { get; set; } = 0f;

        [SettingPropertyBool("{=Titan_Settings_000002}Enable Party Multipliers " + "*", Order = 7, IsToggle = true, RequireRestart = true,
            HintText = "{=Titan_Settings_000002_desc}Multipliers that increase the size of Bandit, Villager, Caravan, and Militias. Tick the box to enable and configure. Restart Required."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "/" + "{=Titan_Settings_000001}Party Size Multipliers Group", GroupOrder = 5)]
        public bool PartySizeMultipliersEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=Titan_Settings_000003}Bandit Multiplier", 0.10f, 20f, "0%", Order = 8, RequireRestart = false,
            HintText = "{=Titan_Settings_000003_desc}Multiply bandit party sizes by this amount."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "/" + "{=Titan_Settings_000001}Party Size Multipliers Group")]
        public float PartySizeBanditMultiplier { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=Titan_Settings_000004}Villager Multiplier", 0.10f, 20f, "0%", Order = 9, RequireRestart = false,
           HintText = "{=Titan_Settings_000004_desc}Multiply villager party Sizes by this amount."),
           SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "/" + "{=Titan_Settings_000001}Party Size Multipliers Group")]
        public float PartySizeVillagerMultiplier { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=Titan_Settings_000005}Caravan Multiplier", 0.10f, 20f, "0%", Order = 10, RequireRestart = false,
           HintText = "{=Titan_Settings_000005_desc}Multiply caravan party Sizes by this amount."),
           SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "/" + "{=Titan_Settings_000001}Party Size Multipliers Group")]
        public float PartySizeCarvanMultiplier { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=Titan_Settings_000006}Militia Multiplier", 0.10f, 20f, "0%", Order = 10, RequireRestart = false,
           HintText = "{=Titan_Settings_000006_desc}Multiply Militia party Sizes by this amount."),
           SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "/" + "{=Titan_Settings_000001}Party Size Multipliers Group")]
        public float PartySizeMilitiaMultiplier { get; set; } = 1f;
        #endregion //~ Party Size Tweaks

        //~ Party Wage Tweaks
        #region Party Wage Tweaks
        [SettingPropertyBool("{=BT_Settings_006400}Wages " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_006400_Desc}Allows you to reduce/increase wages for various groups."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006400}Wages", GroupOrder = 4)]
        public bool PartyWageTweaksEnabled { get; set; } = false;

        //[SettingPropertyBool("{=BT_Settings_006405}Wage Harmony Patches " + "*", Order = 1, RequireRestart = true, IsToggle = true,
        //    HintText = "{=BT_Settings_006405_Desc}Enables Harmony Patches instead of model."),
        //    SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006400}Wages", GroupOrder = 4)]
        public bool PartyWageTweaksHarmonyEnabled { get; set; } = false;


        [SettingPropertyFloatingInteger("{=BT_Settings_006401}Party Wage Adjustment", 0f, 5f, "0%", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_006401_Desc}Adjusts party wages to a % of native value. Native is 100%."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006400}Wages")]
        public float PartyWagePercent { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=BT_Settings_006402}Garrison Wage Adjustment", 0f, 5f, "0%", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_006402_Desc}Adjusts garrison wages to a % of native value. Native is 100%."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006400}Wages")]
        public float GarrisonWagePercent { get; set; } = 1.0f;

        [SettingPropertyBool("{=BT_Settings_006403}Also Apply Wage Tweaks to Your Faction", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_006403_Desc}Applies the wage modifiers to your {=KTMCM_CClan}Clan/faction parties as well."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006400}Wages")]
        public bool ApplyWageTweakToFaction { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_006404}Also Apply Wage Tweaks to AI Lords", Order = 5, RequireRestart = false,
            HintText = "{=BT_Settings_006404_Desc}Applies the wage modifiers to ai lord parties as well."),
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006400}Wages")]
        public bool ApplyWageTweakToAI { get; set; } = false;
        #endregion //~ Party Wage Tweaks

        //~ MobilePartyFoodConsumption
        #region MobilePartyFoodConsumption
        [SettingPropertyBool("{=KTMCM_XPMMPF}Party Food Consumption Modifiers " + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_XPMMPFH}Enable Party food consumption modifiers.")]
        [SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=KTMCM_CFood}Food Consumption")]
        public bool PartyFoodConsumptionEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMMPFM}Party Food Consumption Multiplier", 0f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMMPFMH}Multiply Party food consumption by the multiplier [Native : 1.0[100%]]. allows increasing or decreasing daily consumption of food")]
        [SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=KTMCM_CFood}Food Consumption")]
        public float PartyFoodConsumptionMultiplier { get; set; } = 1.0f;
        #endregion //~ MobilePartyFoodConsumption
        #endregion //~ Party Tweaks

        //~ Pregnancy 
        #region Pregnancy
        [SettingPropertyBool("{=BT_Settings_002400}Pregnancy Tweaks " + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_PRMH}Enables modifying Pregnancy variables.")]
        [SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks")]
        public bool MCMPregnancyModifiers { get; set; } = false;

        //~ Duration
        #region Duration
        [SettingPropertyBool("{=KTMCM_PRMDD}Pregnancy Duration Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PRMDDH}Enables Pregnancy Duration modifier.")]
        [SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=KTMCM_CDuration}Duration")]
        public bool PregnancyDurationModifiers { get; set; } = false;

        [SettingPropertyInteger("{=KTMCM_PRMDDN}Pregnancy Duration", 1, 300, "0 Days", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PRMDDNH}Pregnancy Duration in days [Native : 36].")]
        [SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=KTMCM_CDuration}Duration")]
        public int PregnancyDurationValue { get; set; } = 36;
        #endregion //~ Duration

        //~ MortalityProbabilityInLabor
        #region MortalityProbabilityInLabor
        [SettingPropertyBool("{=BT_Settings_002402}Disable Maternal Mortality", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_002402_Desc}Disables the chance of mothers dying when giving birth."),
            SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=KTMCM_CLaborMortality}Labor Mortality")]
        public bool NoMaternalMortalityTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_PRMMPL}Labor Mortality Chance Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PRMMPLH}Enables Mortality In Labor Chance modifier.")]
        [SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=KTMCM_CLaborMortality}Labor Mortality")]
        public bool PregnancyLaborMortalityChanceModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_PRMMPLC}Labor Mortality Chance", 0.000f, 1.000f, RequireRestart = false,
            HintText = "{=KTMCM_PRMMPLCH}Mortality In Labor Chance [Native : 0.015].")]
        [SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=KTMCM_CLaborMortality}Labor Mortality")]
        public float PregnancyLaborMortalityChanceValue { get; set; } = 0.015f;
        #endregion //~ MortalityProbabilityInLabor

        //~ StillbirthProbability
        #region StillbirthProbability
        [SettingPropertyBool("{=BT_Settings_002401}Disable Stillbirths", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_002401_Desc}Disables the chance of children dying when born."),
            SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=KTMCM_CStillbirth}Stillbirth")]
        public bool NoStillbirthsTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_PRMSB}Stillbirth Chance Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PRMSBH}Enables Stillbirth Chance modifier.")]
        [SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=KTMCM_CStillbirth}Stillbirth")]
        public bool PregnancyStillbirthChanceModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_PRMSBC}Stillbirth Chance", 0.01f, 1.00f, RequireRestart = false,
            HintText = "{=KTMCM_PRMSBCH}Stillbirth Chance  [Native : 0.01].")]
        [SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=KTMCM_CStillbirth}Stillbirth")]
        public float PregnancyStillbirthChanceValue { get; set; } = 0.01f;
        #endregion //~ StillbirthProbability

        //~ DeliveringFemaleOffspringProbability
        #region DeliveringFemaleOffspringProbability
        [SettingPropertyBool("{=KTMCM_PRMFO}Female Child Chance Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PRMFOH}Enables Female Child Chance modifier.")]
        [SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=KTMCM_CFemaleChild}Female Child")]
        public bool PregnancyFemaleOffspringChanceModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_PRMFOC}Female Child Chance", 0.00f, 1.00f, RequireRestart = false,
            HintText = "{=KTMCM_PRMFOCH}Female Child Chance  [Native : 0.51].")]
        [SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=KTMCM_CFemaleChild}Female Child")]
        public float PregnancyFemaleOffspringChanceValue { get; set; } = 0.51f;
        #endregion //~ DeliveringFemaleOffspringProbability

        //~ DeliveringTwinsProbability
        #region DeliveringTwinsProbability
        [SettingPropertyBool("{=KTMCM_PMTO}Twins Chance Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PMTOH}Enables Twins Chance modifier.")]
        [SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=KTMCM_CTwins}Twins")]
        public bool PregnancyTwinsChanceModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_PMTOC}Twins Chance", 0.00f, 1.00f, RequireRestart = false, //"#0%"
            HintText = "{=KTMCM_PMTOCH}Twins Chance  [Native : 0.03].")]
        [SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=KTMCM_CTwins}Twins")]
        public float PregnancyTwinsChanceValue { get; set; } = 0.03f;
        #endregion //~ DeliveringTwinsProbability 

        //~ Chance Tweaks
        #region Chance Tweaks
        [SettingPropertyBool("{=BT_Settings_002409}Pregnancy Chance Tweaks " + "*", Order = 7, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_002409_Desc}Enabling this will completely override the daily pregnancy check. All settings below will be applied!"),
            SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=BT_Settings_002409}Pregnancy Chance Tweaks", GroupOrder = 4)]
        public bool DailyChancePregnancyTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_002410}Player is Infertile", Order = 1, RequireRestart = false, HintText = "{=BT_Settings_002410_Desc}Native: disabled. If enabled, the player will not be able to have children."),
            SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=BT_Settings_002409}Pregnancy Chance Tweaks")]
        public bool PlayerCharacterInfertileEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_002411}Min Pregnancy Age", 0, 125, "0 Years", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_002411_Desc}Native: 18. Minimum age that someone can get pregnant."),
            SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=BT_Settings_002409}Pregnancy Chance Tweaks")]
        public int MinPregnancyAge { get; set; } = 18;

        [SettingPropertyInteger("{=BT_Settings_002412}Max Pregnancy Age", 0, 125, "0 Years", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_002412_Desc}Native: 45. Maximum age that someone can get pregnant."),
            SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=BT_Settings_002409}Pregnancy Chance Tweaks")]
        public int MaxPregnancyAge { get; set; } = 45;

        [SettingPropertyFloatingInteger("{=BT_Settings_002413}Clan Fertility Bonus", 1f, 10f, "0%", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_002413_Desc}Adds modifier to your clan members to become pregnant. 100% = No Bonus, 200% = 2x chance. Note: May not do much after ~6-8 kids due to the base pregnancy calculations."),
            SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=BT_Settings_002409}Pregnancy Chance Tweaks")]
        public float ClanFertilityBonus { get; set; } = 1f;

        [SettingPropertyInteger("{=BT_Settings_002414}Max Children", 0, 100, "0 Children", Order = 5, RequireRestart = false,
            HintText = "{=BT_Settings_002414_Desc}Default: 5. Maximum number of children that someone can have."),
            SettingPropertyGroup("{=BT_Settings_002400}Pregnancy Tweaks" + "/" + "{=BT_Settings_002409}Pregnancy Chance Tweaks")]
        public int MaxChildren { get; set; } = 5;
        #endregion //~ Chance Tweaks
        #endregion //~ Pregnancy 

        //~ Prisoner Tweaks
        #region Prisoner Tweaks
        [SettingPropertyBool("{=BT_Settings_007000}Prisoner Tweaks", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PRTH}Enable modifying prisoner tweak variables..")]
        [SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks")]
        public bool MCMPrisonerTweaksEnabled { get; set; } = true;

        //~ Imprisonment Time Tweaks
        #region Imprisonment Time Tweaks
        [SettingPropertyBool("{=BT_Settings_007100}Imprisonment Time " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_007100_Desc}Adds a minimum amount of time before lords can attempt to escape imprisonment."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007100}Imprisonment Time")]
        public bool PrisonerImprisonmentTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_007101}Player Prisoners Only", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_007101_Desc}Whether the tweak should be applied only to prisoners held by the player."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007100}Imprisonment Time")]
        public bool PrisonerImprisonmentPlayerOnly { get; set; } = true;

        [SettingPropertyInteger("{=BT_Settings_007102}Minimum Days of Imprisonment", 0, 180, "0 Days", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_007102_Desc}The minimum number of days a lord will remain imprisoned before they can attempt to escape."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007100}Imprisonment Time")]
        public int MinimumDaysOfImprisonment { get; set; } = 0;

        [SettingPropertyBool("{=BT_Settings_007103}Enable Missing Prisoner Hero Fix" + "*", Order = 4, RequireRestart = true,
            HintText = "{=BT_Settings_007103_Desc}Will attempt to detect and release prisoner Heroes who may be bugged and do not respawn. Will trigger 3 days after the Minimum Days of Imprisonment setting."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007100}Imprisonment Time")]
        public bool EnableMissingHeroFix { get; set; } = false;

        #endregion //~ Imprisonment Time Tweaks

        //~ Party Prisoner Size Tweak
        #region Party Prisoner Size Tweak

        [SettingPropertyBool("{=BT_Settings_007200}Prisoner Size Bonus " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_007200_Desc}Enables a % bonus to your party's maximum prisoner size."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007200}Prisoner Size Bonus", GroupOrder = 2)]
        public bool PrisonerSizeTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_007201}Prisoner Size Bonus", 0f, 5f, "0%", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_007201_Desc}Adds a % bonues to your party's maximum prisoner size. Native is 0%."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007200}Prisoner Size Bonus")]
        public float PrisonerSizeTweakPercent { get; set; } = 0;

        [SettingPropertyBool("{=BT_Settings_007202}Also apply to AI", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_007202_Desc}Wether the prisoner size bonus should apply to AI Lords."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007200}Prisoner Size Bonus")]
        public bool PrisonerSizeTweakAI { get; set; } = false;

        #endregion //~ Party Prisoner Size Tweak

        //~ Prisoner Confirmity Tweaks
        #region Prisoner Confirmity Tweaks

        [SettingPropertyBool("{=BT_Settings_007300}Prisoner Confirmity " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_007300_Desc}Modifies the conformity rate of the base game, speeding the rate at which prisoners can be recruited."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007300}Prisoner Confirmity", GroupOrder = 3)]
        public bool PrisonerConformityTweaksEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_007301}Prisoner Confirmity Bonus", 0f, 10f, "0%", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_007301_Desc}Adds a % bonues to the conformity generated each hour. Native is 0%."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007300}Prisoner Confirmity")]
        public float PrisonerConformityTweakBonus { get; set; } = 0;

        [SettingPropertyBool("{=BT_Settings_007302}Apply Prisoner Confirmity Tweaks to Clan", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_007302_Desc}Applies Prisoner Conformity Tweaks to all clan parties as well."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007300}Prisoner Confirmity")]
        public bool PrisonerConformityTweaksApplyToClan { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_007303}Apply Prisoner Confirmity Tweaks to AI", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_007303_Desc}Applies Prisoner Conformity Tweaks to all parties, including AI lords as well."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007300}Prisoner Confirmity")]
        public bool PrisonerConformityTweaksApplyToAi { get; set; } = false;

        #endregion //~ Prisoner Confirmity Tweaks

        //~ Prisoner Selling Price
        #region Prisoner Selling Price
        [SettingPropertyBool("{=KT_PPE}Prisoner Price Tweaks Enabled", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KT_PPEH}Enable prisoner price tweaks.")]
        [SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=KT_PP}Prisoner Price")]
        public bool PrisonerPriceTweaksEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KT_PPM}Prisoner Price Multiplier", 0f, 10f, "#0%", Order = 2,
            HintText = "{=KT_PPMH}Multiply the prisoner selling price [Native: 100%]."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=KT_PP}Prisoner Price")]
        public float PrisonerPriceMultiplier { get; set; } = 1.0f;

        #endregion
        #endregion //~ Prisoner Tweaks

        //~ Settlement Tweaks
        #region Settlement Tweaks
        [SettingPropertyBool("{=BT_Settings_008000}Settlement Tweaks", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_STTH}Enable modifying settlement tweak variables..")]
        [SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks")]
        public bool MCMSettlementTweaksEnabled { get; set; } = true;

        //~ Settlement Culture Transformation
        #region Settlement Culture Transformation
        [SettingPropertyBool("{=BT_Settings_008100}Settlement Culture Transformation " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008100_Desc}Changes the culture of settlement in relation to the owner clan. On deactivation cultures revert back. The last town of a culture will not be changed!"),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008100}Settlement Culture Transformation")]
        public bool EnableCultureChanger { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_008101}Change To Culture Of Kingdom Faction Instead" + "*", Order = 1, RequireRestart = true, IsToggle = false,
            HintText = "{=BT_Settings_008101_Desc}Instead of changing the culture to its owner-clan culture, change to its kingdom culture."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008100}Settlement Culture Transformation")]
        public bool ChangeToKingdomCulture { get; set; } = false;

        [SettingPropertyDropdown("{=BT_Settings_008102}Override Culture For Player Clan" + "*", Order = 3, RequireRestart = true,
            HintText = "{=BT_Settings_008102_Desc}Overrides the culture to change to for player clan owned settlements."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008100}Settlement Culture Transformation")]
        public DropdownDefault<string> PlayerCultureOverride { get; } = new(new string[]
        {
            "No Override",
            "battania",
            "vlandia",
            "empire",
            "sturgia",
            "aserai",
            "khuzait",
            "CALRADIA EXPANDED KINGDOMS ONLY:",
            "nordling",
            "vagir",
            "rhodok",
            "apolssalian",
            "lyrion",
            "khergit",
            "paleician",
            "republic",
            "ariorum"
        }, 0);

        [SettingPropertyInteger("{=BT_Settings_008103}Weeks for Settlement Culture Change", 1, 52, "0 Weeks", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_008103_Desc}After how many weeks the culture of a settlement changes to its owner's culture (and produces recruits of the new culturegroup)."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008100}Settlement Culture Transformation")]
        public int TimeToChanceCulture { get; set; } = 10;
        #endregion //~ Settlement Culture Transformation

        //~ Settlement Tweaks - Disable Troop Donations
        #region Settlement Tweaks - Disable Troop Donations
        [SettingPropertyBool("{=BT_Settings_008200}Disable Troop Donations" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008200_Desc}Disables your clan parties from donating troops to clan owned settlements."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008200}Disable Troop Donations", GroupOrder = 2)]
        public bool DisableTroopDonationPatchEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_008201}Disable Troop Donations - Any Settlement", Order = 1, RequireRestart = false,
            HintText = "{=BT_Settings_008201_Desc}Additionally disables your clan parties from donating troops to ANY settlement."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008200}Disable Troop Donations")]
        public bool DisableTroopDonationAnyEnabled { get; set; } = false;
        #endregion //~ Settlement Tweaks - Disable Troop Donations

        //~ Settlement Tweaks - Production Tweaks
        #region Settlement Tweaks - Production Tweaks
        [SettingPropertyBool("{=BT_Settings_008300}Village Productivity" + "*", Order = 1, IsToggle = true, RequireRestart = true,
            HintText = "{=BT_Settings_008300_Desc}Enables Tweaks for increased productivity in villages."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008300}Village Productivity", GroupOrder = 3)]
        public bool ProductionTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008301}Village Production: Food", 1f, 3f, "0%", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_008301_Desc}Modifies the daily production of food goods in villages."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008300}Village Productivity")]
        public float ProductionFoodTweakAmount { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008302}Village Production: Other goods", 1f, 3f, "0%", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_008302_Desc}Modifies the daily production of other goods in villages."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008300}Village Productivity")]
        public float ProductionOtherTweakAmount { get; set; } = 1f;

        #endregion //~ Settlement Tweaks - Production Tweaks

        //~ Settlement Tweaks - Buildings
        #region Settlement Tweaks - Buildings

        //~ Settlement Tweaks - Buildings - Castle
        #region Settlement Tweaks - Buildings - Castle

        //~ Settlement Tweaks - Buildings - Castle - Training Fields
        #region Settlement Tweaks - Buildings - Castle - Training Fields
        [SettingPropertyBool("{=BT_Settings_008402}Castle Training Field " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008402_Desc}Changes the amount of experience the training fields provides for each level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008402}Castle Training Field")]
        public bool CastleTrainingFieldsBonusEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008403}Castle Training Fields Experience Level 1" + "*", 1, 100, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008403_Desc}Native value is 1. Changes the amount of experience the training fields provides at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008402}Castle Training Field")]
        public int CastleTrainingFieldsXpAmountLevel1 { get; set; } = 1;

        [SettingPropertyInteger("{=BT_Settings_008404}Castle Training Fields Experience Level 2" + "*", 2, 200, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008404_Desc}Native value is 2. Changes the amount of experience the training fields provides at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008402}Castle Training Field")]
        public int CastleTrainingFieldsXpAmountLevel2 { get; set; } = 2;

        [SettingPropertyInteger("{=BT_Settings_008405}Castle Training Fields Experience Level 3" + "*", 3, 300, RequireRestart = true, Order = 4,
            HintText = "{=BT_Settings_008405_Desc}Native value is 3. Changes the amount of experience the training fields provides at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008402}Castle Training Field")]
        public int CastleTrainingFieldsXpAmountLevel3 { get; set; } = 3;
        #endregion //~ Settlement Tweaks - Buildings - Castle - Training Fields

        //~ Settlement Tweaks - Buildings - Castle - Granary 
        #region Settlement Tweaks - Buildings - Castle - Granary 
        [SettingPropertyBool("{=BT_Settings_008406}Castle Granary " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008406_Desc}Changes the amount of food storage the castle granary provides per level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008406}Castle Granary", GroupOrder = 2)]
        public bool CastleGranaryBonusEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008407}Castle Granary Food Storage Level 1" + "*", 100, 1000, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008407_Desc}Native value is 100. Changes the amount of food storage the castle granary provides at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008406}Castle Granary")]
        public int CastleGranaryStorageAmountLevel1 { get; set; } = 100;

        [SettingPropertyInteger("{=BT_Settings_008408}Castle Granary Food Storage Level 2" + "*", 200, 2000, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008408_Desc}Native value is 200. Changes the amount of food storage the castle granary provides at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008406}Castle Granary")]
        public int CastleGranaryStorageAmountLevel2 { get; set; } = 200;

        [SettingPropertyInteger("{=BT_Settings_008409}Castle Granary Food Storage Level 3" + "*", 300, 3000, RequireRestart = true, Order = 4,
            HintText = "{=BT_Settings_008409_Desc}Native value is 300. Changes the amount of food storage the castle granary provides at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008406}Castle Granary")]
        public int CastleGranaryStorageAmountLevel3 { get; set; } = 300;
        #endregion //~ Settlement Tweaks - Buildings - Castle - Granary 

        //~ Settlement Tweaks - Buildings - Castle - Gardens
        #region Settlement Tweaks - Buildings - Castle - Gardens
        [SettingPropertyBool("{=BT_Settings_008410}Castle Gardens " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008410_Desc}Changes the amount of food the castle gardens produce per level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008410}Castle Gardens", GroupOrder = 3)]
        public bool CastleGardensBonusEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008411}Castle Garden Food Production Level 1" + "*", 5, 50, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008411_Desc}Native value is 5. Changes the amount of food the castle gardens produce at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008410}Castle Gardens")]
        public int CastleGardensFoodProductionAmountLevel1 { get; set; } = 5;

        [SettingPropertyInteger("{=BT_Settings_008412}Castle Garden Food Production Level 2" + "*", 10, 100, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008412_Desc}Native value is 10. Changes the amount of food the castle gardens produce at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008410}Castle Gardens")]
        public int CastleGardensFoodProductionAmountLevel2 { get; set; } = 10;

        [SettingPropertyInteger("{=BT_Settings_008413}Castle Garden Food Production Level 3" + "*", 15, 150, RequireRestart = true, Order = 4,
            HintText = "{=BT_Settings_008413_Desc}Native value is 15. Changes the amount of food the castle gardens produce at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008410}Castle Gardens")]
        public int CastleGardensFoodProductionAmountLevel3 { get; set; } = 15;
        #endregion //~ Settlement Tweaks - Buildings - Castle - Gardens

        //~ Settlement Tweaks - Buildings - Castle - Militia Barracks
        #region Settlement Tweaks - Buildings - Castle - Militia Barracks
        [SettingPropertyBool("{=BT_Settings_008414}Castle Militia Barracks " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008414_Desc}Changes the militia production that the castle militia barracks provides per level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008414}Castle Militia Barracks", GroupOrder = 4)]
        public bool CastleMilitiaBarracksBonusEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008415}Castle Militia Barracks Production Level 1" + "*", 1, 10, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008415_Desc}Native value is 1. Changes the militia production that the castle militia barracks provides at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008414}Castle Militia Barracks")]
        public int CastleMilitiaBarracksAmountLevel1 { get; set; } = 1;

        [SettingPropertyInteger("{=BT_Settings_008416}Castle Militia Barracks Production Level 2" + "*", 2, 20, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008416_Desc}Native value is 2. Changes the militia production that the castle militia barracks provides at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008414}Castle Militia Barracks")]
        public int CastleMilitiaBarracksAmountLevel2 { get; set; } = 2;

        [SettingPropertyInteger("{=BT_Settings_008417}Castle Militia Barracks Production Level 3" + "*", 3, 30, RequireRestart = true, Order = 4,
            HintText = "{=BT_Settings_008417_Desc}Native value is 3. Changes the militia production that the castle militia barracks provides at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008414}Castle Militia Barracks")]
        public int CastleMilitiaBarracksAmountLevel3 { get; set; } = 3;
        #endregion //~ Settlement Tweaks - Buildings - Castle - Militia Barracks

        #endregion //~ Settlement Tweaks - Buildings - Castle

        //~ Settlement Tweaks - Buildings - Town
        #region Settlement Tweaks - Buildings - Town 

        //~ Settlement Tweaks - Buildings - Town - Training Fields
        #region Settlement Tweaks - Buildings - Town - Training Fields
        [SettingPropertyBool("{=BT_Settings_008419}Town Training Fields " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008419_Desc}Changes the amount of experience the training fields provides for each level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008419}Town Training Fields")]
        public bool TownTrainingFieldsBonusEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008420}Town Training Fields Experience Level 1" + "*", 30, 300, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008420_Desc}Native value is 30. Changes the amount of experience the training fields provides at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008419}Town Training Fields")]
        public int TownTrainingFieldsXpAmountLevel1 { get; set; } = 30;

        [SettingPropertyInteger("{=BT_Settings_008421}Town Training Fields Experience Level 2" + "*", 60, 600, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008421_Desc}Native value is 60. Changes the amount of experience the training fields provides at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008419}Town Training Fields")]
        public int TownTrainingFieldsXpAmountLevel2 { get; set; } = 60;

        [SettingPropertyInteger("{=BT_Settings_008422}Town Training Fields Experience Level 3" + "*", 100, 1000, RequireRestart = true, Order = 4,
            HintText = "{=BT_Settings_008422_Desc}Native value is 100. Changes the amount of experience the training fields provides at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008419}Town Training Fields")]
        public int TownTrainingFieldsXpAmountLevel3 { get; set; } = 100;
        #endregion //~ Settlement Tweaks - Buildings - Town - Training Fields

        //~ Settlement Tweaks - Buildings - Town - Granary
        #region Settlement Tweaks - Buildings - Town - Granary
        [SettingPropertyBool("{=BT_Settings_008423}Town Granary " + "*", RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008423_Desc}Changes the amount of food storage the town granary provides per level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008423}Town Granary", GroupOrder = 2)]
        public bool TownGranaryBonusEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008424}Town Granary Food Storage Level 1" + "*", 200, 2000, RequireRestart = true, Order = 1,
            HintText = "{=BT_Settings_008424_Desc}Native value is 200. Changes the amount of food storage the town granary provides at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008423}Town Granary")]
        public int TownGranaryStorageAmountLevel1 { get; set; } = 200;

        [SettingPropertyInteger("{=BT_Settings_008425}Town Granary Food Storage Level 2" + "*", 400, 4000, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008425_Desc}Native value is 400. Changes the amount of food storage the town granary provides at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008423}Town Granary")]
        public int TownGranaryStorageAmountLevel2 { get; set; } = 400;

        [SettingPropertyInteger("{=BT_Settings_008426}Town Granary Food Storage Level 3" + "*", 600, 6000, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008426_Desc}Native value is 600. Changes the amount of food storage the town granary provides at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008423}Town Granary")]
        public int TownGranaryStorageAmountLevel3 { get; set; } = 600;
        #endregion //~ Settlement Tweaks - Buildings - Town - Granary

        //~ Settlement Tweaks - Settlement Buildings Tweaks - Town Buildings Tweaks - Orchards Tweak
        #region Settlement Tweaks - Settlement Buildings Tweaks - Town Buildings Tweaks - Orchards Tweak
        [SettingPropertyBool("{=BT_Settings_008427}Town Orchards " + "*", Order = 1, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_008427_Desc}Changes the amount of food the town orchards produce per level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008427}Town Orchards", GroupOrder = 3)]
        public bool TownOrchardsBonusEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008428}Town Orchard Food Production Level 1" + "*", 10, 100, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008428_Desc}Native value is 10. Changes the amount of food the town orchards produce at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008427}Town Orchards")]
        public int TownOrchardsFoodProductionAmountLevel1 { get; set; } = 10;

        [SettingPropertyInteger("{=BT_Settings_008429}Town Orchard Food Production Level 2" + "*", 20, 200, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008429_Desc}Native value is 20. Changes the amount of food the town orchards produce at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008427}Town Orchards")]
        public int TownOrchardsFoodProductionAmountLevel2 { get; set; } = 20;

        [SettingPropertyInteger("{=BT_Settings_008430}Town Orchard Food Production Level 3" + "*", 30, 300, RequireRestart = true, Order = 4,
            HintText = "{=BT_Settings_008430_Desc}Native value is 30. Changes the amount of food the town orchards produce at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008427}Town Orchards")]
        public int TownOrchardsFoodProductionAmountLevel3 { get; set; } = 30;
        #endregion //~ Settlement Tweaks - Settlement Buildings Tweaks - Town Buildings Tweaks - Orchards Tweak

        //~ Settlement Tweaks - Settlement Buildings Tweaks - Town Buildings Tweaks - Militia Barracks Tweak
        #region Settlement Tweaks - Settlement Buildings Tweaks - Town Buildings Tweaks - Militia Barracks Tweak
        [SettingPropertyBool("{=BT_Settings_008431}Town Militia Barracks " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008431_Desc}Changes the militia production that the town militia barracks provides per level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008431}Town Militia Barracks", GroupOrder = 4)]
        public bool TownMilitiaBarracksBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008432}Town Militia Barracks Production Level 1" + "*", .5f, 15, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008432_Desc}Native value is .5. Changes the militia production that the town militia barracks provides at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008431}Town Militia Barracks")]
        public float TownMilitiaBarracksAmountLevel1 { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008433}Town Militia Barracks Production Level 2" + "*", 1f, 20f, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008433_Desc}Native value is 1. Changes the militia production that the town militia barracks provides at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008431}Town Militia Barracks")]
        public float TownMilitiaBarracksAmountLevel2 { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008434}Town Militia Barracks Production Level 3" + "*", 1.5f, 30f, RequireRestart = true, Order = 4,
            HintText = "{=BT_Settings_008434_Desc}Native value is 1.5. Changes the militia production that the town militia barracks provides at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008431}Town Militia Barracks")]
        public float TownMilitiaBarracksAmountLevel3 { get; set; } = 1.5f;

        #endregion //~ Settlement Tweaks - Settlement Buildings Tweaks - Town Buildings Tweaks - Militia Barracks Tweak
        #endregion //~ //~ Settlement Tweaks - Buildings - Town
        #endregion //~ Settlement Tweaks - Buildings

        //~ Settlement Tweaks - Settlement Food
        #region Settlement Tweaks - Settlement Food
        [SettingPropertyBool("{=BT_Settings_008500}Settlement Food " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008500_Desc}Enables tweaks which provide bonuses to food production in towns and castles."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008500}Settlement Food", GroupOrder = 4)]
        public bool SettlementFoodBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008501}Castle Food Modifier", 1f, 10f, "0%", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_008501_Desc}Native value is 100%. Adds a modifier to food production in castles."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008500}Settlement Food")]
        public float CastleFoodBonus { get; set; } = 0f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008502}Town Food Modifier", 1f, 10f, "0%", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_008502_Desc}Native value is 100%. Adds a modifier to food production in towns."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008500}Settlement Food")]
        public float TownFoodBonus { get; set; } = 0f;

        //~ Settlement Tweaks - Settlement Food Bonus - Food Loss from Prosperity Tweak
        #region Settlement Tweaks - Settlement Food Bonus - Food Loss from Prosperity Tweak
        [SettingPropertyBool("{=BT_Settings_008503}Food Loss From Prosperity", Order = 1, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_008503_Desc}Allows you to adjust the loss to food production received from settlement prosperity."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008500}Settlement Food" + "/" + "{=BT_Settings_008503}Food Loss from Prosperity")]
        public bool SettlementProsperityFoodMalusTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008504}Prosperity Food Loss Divisor", 50, 400, RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008504_Desc}Native value is 50. The prosperity of the settlement is divided by this value to calculate the loss. Increasing this value will decrease the amount of food lost."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008500}Settlement Food" + "/" + "{=BT_Settings_008503}Food Loss from Prosperity")]
        public int SettlementProsperityFoodMalusDivisor { get; set; } = 50;
        #endregion  //~ Settlement Tweaks - Settlement Food Bonus - Food Loss from Prosperity Tweak
        #endregion //~ Settlement Tweaks - Settlement Food

        //~ Settlement Tweaks - Normal Militia
        #region Settlement Tweaks - Normal Militia

        [SettingPropertyBool("{=BT_Settings_008600}Normal Militia " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008600_Desc}Grants a flat bonus to militia growth and rate of retirement in towns and castles."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008600}Normal Militia", GroupOrder = 5)]
        public bool SettlementMilitiaBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008601}Castle Militia Growth Bonus", 0f, 50f, "0.0 Militia/Day", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008601_Desc}Native value is 0. Adds a flat bonus on how many militia gets recruited each day in castles."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008600}Normal Militia")]
        public float CastleMilitiaBonusFlat { get; set; } = 0f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008602}Town Militia Growth Bonus", 0f, 50f, "0.0 Militia/Day", RequireRestart = false, Order = 3,
            HintText = "{=BT_Settings_008602_Desc}Native value is 0. Adds a flat bonus on how many militia gets recruited each day in towns."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008600}Normal Militia")]
        public float TownMilitiaBonusFlat { get; set; } = 0f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008603}Castle Militia Retirement Modifier", 0f, 0.25f, "0.0%/Day", RequireRestart = false, Order = 3,
            HintText = "{=BT_Settings_008603_Desc}Native value is 2.5%. Modifies the percentage of your militia retiring each dayin castles."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008600}Normal Militia")]
        public float CastleMilitiaRetirementModifier { get; set; } = 0.025f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008604}Town Militia Retirement Modifier", 0f, 0.25f, "0.0%/Day", RequireRestart = false, Order = 3,
            HintText = "{=BT_Settings_008604_Desc}Native value is 2.5%. Modifies the percentage of your militia retiring each dayin town."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008600}Normal Militia")]
        public float TownMilitiaRetirementModifier { get; set; } = 0.025f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008605}Village Militia Retirement Modifier", 0f, 0.25f, "0.0%/Day", RequireRestart = false, Order = 4,
            HintText = "{=BT_Settings_008605_Desc}Native value is 2.5%. Modifies the percentage of your militia retiring each dayin town."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008600}Normal Militia")]
        public float VillageMilitiaRetirementModifier { get; set; } = 0.025f;
        #endregion //~ Settlement Tweaks - Normal Militia

        //~ Settlement Tweaks - Militia Bonus Tweaks - Elite Militia
        #region Settlement Tweaks - Militia Bonus Tweaks - Elite Militia

        [SettingPropertyBool("{=BT_Settings_008700}Elite Militia " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008700_Desc}Adds a bonus to the chance that militia spawning in towns and castles are elite."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008700}Elite Militia", GroupOrder = 6)]
        public bool SettlementMilitiaEliteSpawnRateBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008701}Elite Melee Militia Spawn Chance", 0.01f, 1f, "0%", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008701_Desc}Native value is 10%. Sets the chance that the militia spawning in towns and castles are elite melee troops."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008700}Elite Militia")]
        public float SettlementEliteMeleeSpawnRateBonus { get; set; } = 0.1f;
        /*

                [SettingPropertyFloatingInteger("{=BT_Settings_008702}Elite Ranged Militia Spawn Chance", 0.01f, 1f, "0%", RequireRestart = false, Order = 3,
                    HintText = "{=BT_Settings_008702_Desc}Native value is 10%. Sets the chance that the militia spawning in towns and castles are elite ranged troops."),
                    SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008700}Elite Militia")]
                public float SettlementEliteRangedSpawnRateBonus { get; set; } = 0.1f;*/
        #endregion//~ Settlement Tweaks - Militia Bonus Tweaks - Elite Militia
        #endregion //~ Settlement

        //~ Tournaments
        #region Tournaments
        [SettingPropertyBool("{=BT_Settings_008800}Tournaments", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_TTH}Enable modifying tournaments tweak variables.")]
        [SettingPropertyGroup("{=BT_Settings_008800}Tournaments")]
        public bool MCMTournamentsEnabled { get; set; } = true;

        //~ Renown Reward
        #region Renown Reward
        [SettingPropertyBool("{=BT_Settings_008801}Renown Reward " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008801_Desc}Sets the amount of renown awarded when you win a tournament."),
            SettingPropertyGroup("{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008801}Renown Reward")]
        public bool TournamentRenownIncreaseEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008802}Tournament Renown Reward", 1, 50, "0 Renown", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008802_Desc}Native value is 3. Increases the amount of renown awarded when you win a tournament."),
            SettingPropertyGroup("{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008801}Renown Reward")]
        public int TournamentRenownAmount { get; set; } = 3;
        #endregion //~ Renown Reward

        //~ Gold Reward
        #region Gold Reward
        [SettingPropertyBool("{=BT_Settings_008803}Gold Reward " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008803_Desc}Adds the set amount of gold to the rewards when you win a tournament."),
            SettingPropertyGroup("{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008803}Gold Reward", GroupOrder = 2)]
        public bool TournamentGoldRewardEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008804}Tournament Gold Reward", 0, 5000, "0 Denar", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008804_Desc}Native value is 0. Adds the set amount of gold to the rewards when you win a tournament."),
            SettingPropertyGroup("{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008803}Gold Reward")]
        public int TournamentGoldRewardAmount { get; set; } = 0;
        #endregion //~ Gold Reward

        //~ Maximum Bet
        #region Maximum Bet 
        [SettingPropertyBool("{=BT_Settings_008805}Maximum Bet " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008805_Desc}Sets the maximum amount of gold that you can bet per round in tournaments."),
            SettingPropertyGroup("{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008805}Maximum Bet", GroupOrder = 3)]
        public bool TournamentMaxBetAmountTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008806}Maximum Bet Amount", 0, 4000, "0 Denar", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008806_Desc}Native value is 150. Sets the maximum amount of gold that you can bet per round in tournaments."),
            SettingPropertyGroup("{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008805}Maximum Bet")]
        public int TournamentMaxBetAmount { get; set; } = 150;
        #endregion //~ Maximum Bet

        //~ Hero Experience
        #region Hero Experience
        [SettingPropertyBool("{=BT_Settings_008807}Hero Experience Modifier " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008807_Desc}Overrides the native multiplier value for experience gain in tournaments for hero characters."),
            SettingPropertyGroup("{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008807}Hero Experience Modifier", GroupOrder = 4)]
        public bool TournamentHeroExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008808}Tournament Hero Experience Modifier", 0.33f, 10f, "0%", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008808_Desc}Native value is 33%. Sets the modifier applied to experience gained in tournaments by hero characters. 100% = full real-world experience."),
            SettingPropertyGroup("{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008807}Hero Experience Modifier")]
        public float TournamentHeroExperienceMultiplier { get; set; } = 0.33f;
        #endregion //~ Hero Experience

        //~ Arena Hero Experience
        #region Arena Hero Experience
        [SettingPropertyBool("{=BT_Settings_008809}Arena Hero Experience Modifier " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008809_Desc}Overrides the native multiplier value for experience gain in arena fights for hero characters."),
            SettingPropertyGroup("{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008809}Arena Experience Modifier", GroupOrder = 5)]
        public bool ArenaHeroExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008810}Arena Hero Experience Modifier", 0.06f, 5f, "0%", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_008810_Desc}Native value is 6%. Sets the modifier applied to experience gain in arena fights for hero characters. 100% = full real-world experience."),
            SettingPropertyGroup("{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008809}Arena Experience Modifier")]
        public float ArenaHeroExperienceMultiplier { get; set; } = 0.06f;
        #endregion //~ Arena Hero Experience

        //~ Minimum Betting Odds
        #region Minimum Betting Odds
        [SettingPropertyBool("{=BT_Settings_008811}Minimum Betting Odds " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008811_Desc}Allows you to set the minimum betting odds in tournaments."),
            SettingPropertyGroup("{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008811}Minimum Betting Odds", GroupOrder = 6)]
        public bool MinimumBettingOddsTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008812}Minimum Betting Odds", 1.1f, 10f, RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008812_Desc}Native: 1.1. The minimum odds for tournament bets, higher means more yield for your bets, if won."),
            SettingPropertyGroup("{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008811}Minimum Betting Odds")]
        public float MinimumBettingOdds { get; set; } = 1.1f;

        #endregion //~ Minimum Betting Odds
        #endregion //~ Tournaments

        //~ Workshops
        #region Workshops
        [SettingPropertyBool("{=BT_Settings_008900}Workshops", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_WSTH}Enable modifying workshops tweak variables..")]
        [SettingPropertyGroup("{=BT_Settings_008900}Workshops")]
        public bool MCMWorkshopsEnabled { get; set; } = true;

        //~ Workshop Limit
        #region Workshop Limit
        [SettingPropertyBool("{=BT_Settings_008901}Workshop Count Limit " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008901_Desc}Sets the base maximum number of workshops you can have and the limit increase gained per clan tier."),
            SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008901}Workshop Count Limit")]
        public bool MaxWorkshopCountTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008902}Base Workshop Count Limit", 0, 159, "0 Workshops", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008902_Desc}Native value is 1. Sets the base maximum number of workshops you can have."),
            SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008901}Workshop Count Limit")]
        public int BaseWorkshopCount { get; set; } = 1;

        [SettingPropertyInteger("{=BT_Settings_008903}Bonus Workshops Per Clan Tier", 0, 50, "0 Shops/Tier", RequireRestart = false, Order = 3,
            HintText = "{=BT_Settings_008903_Desc}Native value is 1. Sets the base maximum number of workshops you can have and the limit increase gained per clan tier."),
            SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008901}Workshop Count Limit")]
        public int BonusWorkshopsPerClanTier { get; set; } = 1;
        #endregion //~ Workshop Limit

        //~ Workshop Cost Tweak
        #region Workshop Cost Tweak
        [SettingPropertyBool("{=BT_Settings_008904}Workshop Buy Cost " + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008904_Desc}Sets the base value used to calculate the cost of workshops. Reduce to reduce cost of workshops."),
            SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008904}Workshop Buy Cost", GroupOrder = 2)]
        public bool WorkshopBuyingCostTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008905}Workshop Base Cost", 0, 15000, "0 Denar", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008905_Desc}Native value is 10,000. Sets the base value used to calculate the cost of workshops (+ workshop type base cost + 0.5 x town prosperity). Reduce to reduce cost of workshops."),
            SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008904}Workshop Buy Cost")]
        public int WorkshopBaseCost { get; set; } = 10000;
        #endregion //~ Workshop Cost Tweak

        //~ Workshop Effectivness
        #region Workshop Effectivness
        [SettingPropertyBool("{=BT_Settings_008906}Workshop Effectivness " + "*", RequireRestart = true, IsToggle = true, Order = 2,
            HintText = "{=BT_Settings_008906_Desc}Increases effectivness of workshops by decreasing its daily expenses."),
            SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008906}Workshop Effectivness", GroupOrder = 3)]
        public bool WorkshopEffectivnessEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008907}Workshop Daily Cost Modifier", 0f, 1f, "0%", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008907_Desc}Native value is 100%. Increases effectivness of workshops by decreasing its daily expenses."),
            SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008906}Workshop Effectivness")]
        public float WorkshopEffectivnessv2Factor { get; set; } = 1f;
        #endregion //~ Workshop Effectivness

        //~ Workshop SellPrices
        #region Workshop SellPrices
        [SettingPropertyBool("{=BT_Settings_008908}Workshop Products Sell Prices " + "*", RequireRestart = true, IsToggle = true, Order = 2,
            HintText = "{=BT_Settings_008908_Desc}Alters the selling prices for products of workshops."),
            SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008908}Workshop Products Sell Prices", GroupOrder = 4)]
        public bool EnableWorkshopSellTweak { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008909}Workshop Products Sell Prices", 0.01f, 5f, "0%", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008909_Desc}Native value is 100%. Alters the selling prices for products of workshops. Increase for better profits."),
            SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008908}Workshop Products Sell Prices")]
        public float WorkshopSellTweak { get; set; } = 1f;
        #endregion //~ Workshop SellPrices

        //~ Workshop Buy Prices
        #region Workshop Buy Prices
        [SettingPropertyBool("{=BT_Settings_008910}Workshop Products Buy Prices " + "*", RequireRestart = true, IsToggle = true, Order = 2,
            HintText = "{=BT_Settings_008910_Desc}Alters the buying prices for input items of workshops."),
            SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008910}Workshop Products Buy Prices", GroupOrder = 5)]
        public bool EnableWorkshopBuyTweak { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008911}Workshop Products Buy Prices", 0.01f, 5f, "0%", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008911_Desc}Native value is 100%. Alters the buying prices for input items of workshops. Decrease for better profits."),
            SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008910}Workshop Products Buy Prices")]
        public float WorkshopBuyTweak { get; set; } = 1f;
        #endregion //~ Workshop Buy Prices

        //~ Bankruptcy
        #region Bankruptcy
        [SettingPropertyBool("{=KTMCM_CLMWSBRTM}Bankruptcy Modifier " + "*", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=}Enables Bankruptcy Modifiers.")]
        [SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=KTMCM_CBankruptcy}Bankruptcy")]
        public bool WorkShopBankruptcyModifiers { get; set; } = false;

        [SettingPropertyInteger("{=KTMCM_CLMWSBRDSM}Days to save", 1, 10, "0 Days", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_CLMWSBRDSMH}Days For Player to Save Workshop From Bankruptcy [Native : 3].")]
        [SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=KTMCM_CBankruptcy}Bankruptcy")]
        public int WorkShopBankruptcyValue { get; set; } = 3;
        #endregion //~ Bankruptcy
        
        //~ Workshop Ownership Changes
        #region Workshop Ownership Changes
        [SettingPropertyBool("{=BT_Settings_008921}Workshops Kept on War Declarations", Order = 0, RequireRestart = true,
            HintText = "{=BT_Settings_008922}Allows Player to Keep Their Workshops When a Kingdom Declares War [Native : false].")]
        [SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008920}Workshop Ownership Changes")]
        public bool KeepWorkshopsOnWarDeclaration { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_008923}Workshops Kept on Bankruptcy", Order = 0, RequireRestart = true,
            HintText = "{=BT_Settings_008924}Allows Player to Keep Their Workshops on Bankruptcy [Native : false].")]
        [SettingPropertyGroup("{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008920}Workshop Ownership Changes")]
        public bool KeepWorkshopsOnBankruptcy { get; set; } = false;
        #endregion //~ Workshop Ownership Changes
        #endregion //~ Workshops

        //~ XP Tweaks
        #region XP Tweaks
        [SettingPropertyBool("{=KTMCM_CXPTweaks}XP Tweaks " + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_XPMH}Enable modifying XP tweak variables.")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks")]
        public bool MCMSkillsXp { get; set; } = false;

        //~ Skills
        #region Skills
        [SettingPropertyBool("{=KTMCM_XPMSM}Skill XP Modifiers " + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_XPMSMH}Enable Skill Xp modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills")]
        public bool SkillXpEnabled { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_XPMSMP}Player Skill Modifiers", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_XPMSMPH}Enable Player to use Skill Xp modifiers")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills")]
        public bool SkillXpUseForPlayer { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_XPMSMPC}Player Clan Skill Modifiers", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_XPMSMPCH}Enable Player Clan to use Skill Xp modifiers")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills")]
        public bool SkillXpUseForPlayerClan { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_XPMSMAI}AI Skill Modifiers", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_XPMSMAIH}Enable AI to use Xp modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills")]
        public bool SkillXpUseForAI { get; set; } = false;

        //~ IndividualMultipliers
        #region IndividualMultipliers
        [SettingPropertyBool("{=KTMCM_XPMSMID}Individual Skill Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDH}Enable Individual Skill Xp modifiers. Use this or global they don't work at same time")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public bool SkillXpUseIndividualMultiplers { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDA}Athletics Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDAH}Multiply Athletics skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierAthletics { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDB}Bow Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDBH}Multiply Bow skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierBow { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDC}Charm Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDCH}Multiply Charm skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierCharm { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDCS}Crafting Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDCSH}Multiply Crafting skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierCrafting { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDCB}Crossbow Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDCBH}Multiply Crossbow skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierCrossbow { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDE}Engineering Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDEH}Multiply Engineering skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierEngineering { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDL}Leadership Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDLH}Multiply Leadership skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierLeadership { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDM}Medicine Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDMH}Multiply Medicine skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierMedicine { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDOH}OneHanded Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDOHH}Multiply OneHanded skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierOneHanded { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDP}Polearm Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDPH}Multiply Polearm skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierPolearm { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDR}Riding Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDRH}Multiply Riding skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierRiding { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDRG}Roguery Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDRGH}Multiply Roguery skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierRoguery { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDS}Scouting Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDSH}Multiply Scouting skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierScouting { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDST}Steward Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDSTH}Multiply Steward skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierSteward { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDT}Tactics Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDTH}Multiply Tactics skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierTactics { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDTR}Throwing Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDTRH}Multiply Throwing skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierThrowing { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDTS}Trade Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDTSH}Multiply Trade skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierTrade { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDTHW}TwoHanded Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDTHWH}Multiply TwoHanded skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierTwoHanded { get; set; } = 1.0f;
        #endregion //~ IndividualMultipliers

        //~ Global
        #region Global
        [SettingPropertyBool("{=KTMCM_XPMSMGS}Global Skill Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_XPMSMGSH}Enable Global Skill Xp modifiers. Use this or global they don't work at same time")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CGlobal}Global")]
        public bool SkillXpUseGlobalMultipler { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMGSM}Global Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMSMGSMH}Multiply skills exp gains by the Global multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CSkills}Skills" + "/" + "{=KTMCM_CGlobal}Global")]
        public float SkillsXpGlobalMultiplier { get; set; } = 1.0f;
        #endregion //~ Global
        #endregion //~ Skills

        //~ LearningRateMultipliers
        #region LearningRateMultipliers
        [SettingPropertyBool("{=KTMCM_XPMLR}Learning Rate Enable " + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_XPMLRH}Enable Learning Rate modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CLearning}Learning Rate")]
        public bool LearningRateEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMLRM}Learning Rate Multiplier", 0.1f, 100.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMLRMH}Multiply Learning Rate by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CLearning}Learning Rate")]
        public float LearningRateMultiplier { get; set; } = 1.0f;

        //~ MinimumLearningRate
        #region MinimumLearningRate
        [SettingPropertyFloatingInteger("{=KTMCM_XPMMLR}Minimum Learning Rate", 0.0f, 16.0f, "#0.00", RequireRestart = true,
          HintText = "{=KTMCM_XPMMLRH}Sets the minimum learning rate [Native : 0.0].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CLearning}Learning Rate")]
        public float MinimumLearningRate { get; set; } = 0.0f;
        #endregion //~ LearningLimitMultipliers
        #endregion //~ LearningRateMultipliers

        //~ LearningLimitMultipliers
        #region LearningLimitMultipliers
        [SettingPropertyBool("{=KTMCM_XPMLL}Learning Limit Enable " + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=KTMCM_XPMLLH}Enable Learning Limit modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CLearningLimit}Learning Limit")]
        public bool LearningLimitEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMLLM}Learning Limit Multiplier", 0.1f, 2.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_XPMLLMH}Multiply Learning limit by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=KTMCM_CLearningLimit}Learning Limit")]
        public float LearningLimitMultiplier { get; set; } = 1.0f;
        #endregion //~ LearningLimitMultipliers

        //~ Troop Daily Experience Tweak
        #region Troop Daily Experience Tweak
        [SettingPropertyBool("{=BT_Settings_006500}Daily Troop Experience" + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=BT_Settings_006500_Desc}Gives each troop roster (stack) in a party an amount of experience each day based upon the leader's Leadership skill. By default only applies to the player.")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=BT_Settings_006500}Daily Troop Experience")]//{=BT_Settings_006000}Party Tweaks
        public bool DailyTroopExperienceTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_006501}Percentage of Leadership", 0.0f, 50.0f, "#0%", RequireRestart = false,
            HintText = "{=BT_Settings_006501_Desc}The percentage of the leader's Leadership skill to be given as experience to each of their troop rosters. With 100 leadership and a setting of 1000% each troop type stack will get 1.000 xp daily.")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=BT_Settings_006500}Daily Troop Experience")]
        public float LeadershipPercentageForDailyExperienceGain { get; set; } = 0f;

        [SettingPropertyInteger("{=BT_Settings_006502}Starting from Level Of Leadership", 1, 200, Order = 0, RequireRestart = false,
            HintText = "{=BT_Settings_006502_Desc}The Leadership level required to start giving experience to troop rosters. With this setting at 20, daily training of your troop stacks will start from leadership 20 onwards (but be calculated with the full 20 skillpoints).")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=BT_Settings_006500}Daily Troop Experience")]
        public int DailyTroopExperienceRequiredLeadershipLevel { get; set; } = 30;

        [SettingPropertyBool("{=BT_Settings_006503}Apply to Player's Clan Members", Order = 0, RequireRestart = true,
            HintText = "{=BT_Settings_006503_Desc}Applies the daily troop experience gain to members of the player's clan also.")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=BT_Settings_006500}Daily Troop Experience")]
        public bool DailyTroopExperienceApplyToPlayerClanMembers { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_006504}Apply to all NPC Lords", Order = 0, RequireRestart = true,
            HintText = "{=BT_Settings_006504_Desc}Applies the daily troop experience gain to all NPC lords.")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=BT_Settings_006500}Daily Troop Experience")]
        public bool DailyTroopExperienceApplyToAllNPC { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_006505}Display Message", Order = 0, RequireRestart = true,
            HintText = "{=BT_Settings_006505_Desc}Displays a message showing the amount of experience granted.")]
        [SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=BT_Settings_006500}Daily Troop Experience")]
        public bool DisplayMessageDailyExperienceGain { get; set; } = false;
        #endregion //~ Troop Daily Experience Tweak

        //~ Troop Experience Tweaks
        #region Troop Experience Tweaks
        [SettingPropertyBool("{=BT_Settings_000400}Troop Experience" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_000400_Desc}Tweaks for experience gain of troops in battles and simulations."),
            SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=BT_Settings_000400}Troop Experience", GroupOrder = 4)]
        public bool TroopExperienceTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_000401}Troop Battle Experience", Order = 2, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_000401_Desc}Modifies the amount of experience that ALL troops receive during battles (Note: Only troops, not heroes)."),
            SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=BT_Settings_000400}Troop Experience" + "/" + "{=BT_Settings_000401}Troop Battle Experience")]
        public bool TroopBattleExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_000402}Troop Battle Experience Amount", .01f, 10f, "0%", RequireRestart = false, Order = 3,
            HintText = "{=BT_Settings_000402_Desc}Native value is 100%. Modifies the amount of experience that ALL troops receive during fought battles (Note: Only troops, not heroes. Does not apply to simulated battles.)."),
            SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=BT_Settings_000400}Troop Experience" + "/" + "{=BT_Settings_000401}Troop Battle Experience")]
        public float TroopBattleExperienceMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("{=BT_Settings_000403}Troop Simulation Experience", Order = 4, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_000403_Desc}Modifies the experience gained from simulated battles. This is applied to all fights (including NPC fights) on the campaign map."),
            SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=BT_Settings_000400}Troop Experience" + "/" + "{=BT_Settings_000403}Troop Simulation Experience", GroupOrder = 2)]
        public bool TroopBattleSimulationExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_000404}Troop Simulation Experience Amount", .01f, 10f, "0%", RequireRestart = false, Order = 5,
            HintText = "{=BT_Settings_000404_Desc}Native value is 90%. Provides a multiplier to experience gained from simulated battles. This is applied to all simulated fights on the campaign map."),
            SettingPropertyGroup("{=KTMCM_CXPTweaks}XP Tweaks" + "/" + "{=BT_Settings_000400}Troop Experience" + "/" + "{=BT_Settings_000403}Troop Simulation Experience")]
        public float TroopBattleSimulationExperienceMultiplier { get; set; } = 0.9f;
        #endregion //~ Troop Experience Tweaks
        #endregion //~ XP Tweaks

        //~ MobilePartyViewDistance
        #region MobilePartyViewDistance

        //~ NOT TO BE ENABLED WILL BREAK THE GAME TESTIJNHG ONLY

        /*
                [SettingPropertyBool("{=KTMCM_XPMMPF}Party View Distance Modifiers " + "*", IsToggle = true, Order = 0, RequireRestart = true,
                    HintText = "{=KTMCM_XPMMPFH}Enable Party View Distance modifiers.")]
                [SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=KTMCM_CFood}View Distance")]*/
        public bool MobilePartyViewDistanceEnabled { get; set; } = false;

        /*
                [SettingPropertyFloatingInteger("{=KTMCM_XPMMPFM}View Distance Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
                    HintText = "{=KTMCM_XPMMPFMH}Multiply Party View Distance by the multiplier [Native : 1.0[100%]]. allows increasing or decreasing View Distance")]
                [SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=KTMCM_CFood}View Distance")]*/
        public float MobilePartyViewDistanceMultiplier { get; set; } = 1.0f;
        #endregion //~ MobilePartyViewDistance


        //~ Presets
        #region Presets
        public override IDictionary<string, Func<BaseSettings>> GetAvailablePresets()
        {
            IDictionary<string, Func<BaseSettings>>? basePresets = base.GetAvailablePresets(); // include the 'Default' preset that MCM provides

            basePresets.Add("native all off", () => new MCMSettings()
            {
                //~ Age Tweaks
                AgeTweaksEnabled = false,
                BecomeInfantAge = 3,
                BecomeChildAge = 6,
                BecomeTeenagerAge = 14,
                HeroComesOfAge = 18,
                BecomeOldAge = 47,
                MaxAge = 125,


                //~ ArmyManagement
                MCMArmy = false,
                //~ Cohesion
                armyCohesionMultipliers = false,
                armyCohesionBaseChange = -2,
                armyDisableCohesionLossClanOnlyParties = false,
                armyApplyMultiplerToClanOnlyParties = false,
                armyCohesionLossMultiplier = 1.0f,

                //~ Battle Tweaks 
                battleTweaksEnabled = false,

                //~ Hideout Tweaks
                HideoutBattleTroopLimitTweakEnabled = false,
                HideoutBattleTroopLimit = 10,
                ContinueHideoutBattleOnPlayerDeath = false,
                ContinueHideoutBattleOnPlayerLoseDuel = false,

                //~ Battle Size Tweak
                BattleSizeTweakEnabled = false,
                BattleSize = 1000,

                //~ Siege Tweaks
                SiegeTweaksEnabled = false,
                SiegeConstructionProgressPerDayMultiplier = 1f,
                SiegeCollateralDamageCasualties = 0,
                SiegeDestructionCasualties = 0,

                //~ Weapon Cut Through Tweaks
                SliceThroughEnabled = false,
                TwoHandedWeaponsSliceThroughEnabled = false,
                SingleHandedWeaponsSliceThroughEnabled = false,

                //~ Battle Rewards
                MCMBattleRewardModifiers = false,
                BattleRewardApplyToAI = false,

                //~ Relationship Gain
                BattleRewardsRelationShipGainModifiers = false,
                BattleRewardsRelationShipGainMultiplier = 1.0f,

                //~ Renown Gain
                BattleRewardsRenownGainModifiers = false,
                BattleRewardsRenownGainMultiplier = 1.0f,

                //~ Influence Gain
                BattleRewardsInfluenceGainModifiers = false,
                BattleRewardsInfluenceGainMultiplier = 1.0f,

                //~ Morale Gain
                BattleRewardsMoraleGainModifiers = false,
                BattleRewardsMoraleGainMultiplier = 1.0f,

                //~ Gold loss after defeat
                BattleRewardsGoldLossModifiers = false,
                BattleRewardsGoldLossMultiplier = 1.0f,

                //~ Campaign Tweaks
                CampaignTweaksEnabled = false,

                //~ Difficulty Settings
                DifficultyTweakEnabled = false,
                DamageToPlayerTweakEnabled = false,
                DamageToPlayerMultiplier = 0.67f,
                //DamageToFriendsTweakEnabled = false,
                //DamageToFriendsMultiplier = 0.67f,
                DamageToTroopsTweakEnabled = false,
                DamageToTroopsMultiplier = 0.67f,
                CombatAIDifficultyTweakEnabled = false,
                CombatAIDifficultyMultiplier = 0.67f,
                PlayerMapMovementSpeedBonusTweakEnabled = false,
                PlayerMapMovementSpeedBonusMultiplier = 0.5f,


                //~ Clan *AI Tweaks need Checking
                MCMClanModifiers = false,

                //~ Party Limit
                ClanAdditionalPartyLimitEnabled = false,
                ClanPlayerPartiesLimitEnabled = false,

                //~ Limits Player
                ClanPlayerBasePartiesLimit = 1,
                ClanPlayerPartiesBonusPerClanTier = 0.5f,
                ClanAIPartiesLimitTweakEnabled = false,

                //~ AI Parties Limits
                ClanAIBaseClanPartiesLimit = 1,
                ClanAIPartiesBonusPerClanTier = 0.5f,
                ClanAIMinorClanPartiesLimitTweakEnabled = false,
                AICustomSpawnPartiesLimitTweakEnabled = false,

                //~ CS Parties
                BaseAICustomSpawnPartiesLimit = 0,
                ClanCSPartiesBonusPerClanTier = 0.5f,


                //~ Companion Limit
                ClanCompanionLimitEnabled = false,
                ClanAdditionalCompanionLimit = 0,
                ClanCompanionBaseLimit = 3,


                //~ Crafting Tweaks
                MCMSmithingModifiers = false, // Activates the Model Override
                MCMSmithingHarmoneyPatches = false, // Activates the Model Override

                //~ Stamina Tweaks
                CraftingStaminaTweakEnabled = false,
                MaxCraftingStaminaMultiplier = 1.0f,

                //~ Stamina Gains
                CraftingStaminaGainAmount = 5,
                CraftingStaminaGainOutsideSettlementMultiplier = 0f,

                //~ Energy Cost Multipliers
                SmithingEnergySmithingValue = 1.0f,
                SmithingEnergySmeltingValue = 1.0f,
                SmithingEnergyRefiningValue = 1.0f,
                SmithingEnergyDisable = false,

                //~ BT Smelting Tweaks
                SmeltingTweakEnabled = false,
                PreventSmeltingLockedItems = false,
                AutoLearnSmeltedParts = false,


                //~ Xp Modifiers

                SmithingXpModifiers = false,
                SmithingRefiningXpValue = 1.0f,
                SmithingSmeltingXpValue = 1.0f,
                SmithingSmithingXpValue = 1.0f,

                //~ CharacterDevelopment
                MCMCharacterDevlopmentModifiers = false,

                //~ Levels Per Attribute
                CharacterLevelsPerAttributeModifiers = false,
                CharacterLevelsPerAttributeValue = 4,

                //~ Focus Per Level
                CharacterFocusPerLevelModifiers = false,
                CharacterFocusPerLevelValue = 1,


                //~ Items
                MCMItemModifiers = false,

                //~ Armor
                MCMArmorModifiers = false,

                //~ Weight
                ItemArmorWeightModifiers = false,
                ItemArmorTier1WeightMultiplier = 1.0f,
                ItemArmorTier2WeightMultiplier = 1.0f,
                ItemArmorTier3WeightMultiplier = 1.0f,
                ItemArmorTier4WeightMultiplier = 1.0f,
                ItemArmorTier5WeightMultiplier = 1.0f,
                ItemArmorTier6WeightMultiplier = 1.0f,

                //~ Price
                ItemArmorValueModifiers = false,
                ItemArmorTier1PriceMultiplier = 1.0f,
                ItemArmorTier2PriceMultiplier = 1.0f,
                ItemArmorTier3PriceMultiplier = 1.0f,
                ItemArmorTier4PriceMultiplier = 1.0f,
                ItemArmorTier5PriceMultiplier = 1.0f,
                ItemArmorTier6PriceMultiplier = 1.0f,


                //~ Food
                MCMFoodModifiers = false,

                //~ Weight
                ItemFoodWeightMorale0Multiplier = 1.0f,
                ItemFoodWeightMorale1Multiplier = 1.0f,
                ItemFoodWeightMorale2Multiplier = 1.0f,
                ItemFoodWeightMorale3Multiplier = 1.0f,

                //~ Price
                ItemFoodPriceMorale0Multiplier = 1.0f,
                ItemFoodPriceMorale1Multiplier = 1.0f,
                ItemFoodPriceMorale2Multiplier = 1.0f,
                ItemFoodPriceMorale3Multiplier = 1.0f,


                //~ Melee Weapons
                MCMMeleeWeaponModifiers = false,

                //~ Weight
                ItemMeleeWeaponWeightModifiers = false,
                ItemMeleeWeaponTier1WeightMultiplier = 1.0f,
                ItemMeleeWeaponTier2WeightMultiplier = 1.0f,
                ItemMeleeWeaponTier3WeightMultiplier = 1.0f,
                ItemMeleeWeaponTier4WeightMultiplier = 1.0f,
                ItemMeleeWeaponTier5WeightMultiplier = 1.0f,
                ItemMeleeWeaponTier6WeightMultiplier = 1.0f,

                //~ Price
                ItemMeleeWeaponValueModifiers = false,
                ItemMeleeWeaponTier1PriceMultiplier = 1.0f,
                ItemMeleeWeaponTier2PriceMultiplier = 1.0f,
                ItemMeleeWeaponTier3PriceMultiplier = 1.0f,
                ItemMeleeWeaponTier4PriceMultiplier = 1.0f,
                ItemMeleeWeaponTier5PriceMultiplier = 1.0f,
                ItemMeleeWeaponTier6PriceMultiplier = 1.0f,


                //~ Ranged Weapons
                MCMRagedWeaponsModifiers = false,

                //~ Weight
                ItemRangedWeaponsWeightModifiers = false,
                ItemRangedWeaponsTier1WeightMultiplier = 1.0f,
                ItemRangedWeaponsTier2WeightMultiplier = 1.0f,
                ItemRangedWeaponsTier3WeightMultiplier = 1.0f,
                ItemRangedWeaponsTier4WeightMultiplier = 1.0f,
                ItemRangedWeaponsTier5WeightMultiplier = 1.0f,
                ItemRangedWeaponsTier6WeightMultiplier = 1.0f,

                //~ Price
                ItemRangedWeaponsValueModifiers = false,
                ItemRangedWeaponsTier1PriceMultiplier = 1.0f,
                ItemRangedWeaponsTier2PriceMultiplier = 1.0f,
                ItemRangedWeaponsTier3PriceMultiplier = 1.0f,
                ItemRangedWeaponsTier4PriceMultiplier = 1.0f,
                ItemRangedWeaponsTier5PriceMultiplier = 1.0f,
                ItemRangedWeaponsTier6PriceMultiplier = 1.0f,

                //~ Trade Goods
                MCMTradeGoodsModifiers = false,

                //~ Weight
                ItemTradeGoodsWeightMultiplier = 1.0f,

                //~ Price
                ItemTradeGoodsPriceMultiplier = 1.0f,


                //~ ItemLocks 
                MCMAutoLocks = false,
                autoLockHorses = false,
                autoLockFood = false,
                autoLockIronBar1 = false,
                autoLockIronBar2 = false,
                autoLockIronBar3 = false,
                autoLockIronBar4 = false,
                autoLockIronBar5 = false,
                autoLockIronBar6 = false,
                autoLockIronOre = false,
                autoLockSilverOre = false,
                autoLockHardwood = false,
                autoLockCharcol = false,



                //~ Killing Bandits Raises Relationships
                MCMKillingBanditsEnabled = false,
                GroupsOfBandits = 1,
                RelationshipIncrease = 1,
                Radius = 1000,
                SizeBonusEnabled = false,
                SizeBonus = 0.05f,
                PrisonersOnly = false,
                IncludeBandits = false,
                IncludeOutlaws = false,
                IncludeMafia = false,


                //~ Kingdom Tweaks
                MCMKingdomTweaksEnabled = false,

                //~ Lord Bartering
                BarterablesTweaksEnabled = false,
                BarterablesJoinKingdomAsClanAdjustment = 1,
                BarterablesJoinKingdomAsClanAltFormulaEnabled = false,

                //~ Balancing Tweaks
                KingdomBalanceStrengthEnabled = false,
                KingdomBalanceStrengthVanEnabled = false,
                VlandiaBoost = 0.00f,
                SturgiaBoost = 0.00f,
                BattaniaBoost = 0.00f,
                Empire_N_Boost = 0.00f,
                Empire_S_Boost = 0.00f,
                Empire_W_Boost = 0.00f,
                AseraiBoost = 0.00f,
                KhuzaitBoost = 0.00f,
                PlayerBoost = 0.00f,
                KingdomBalanceStrengthCEKEnabled = false,
                NordlingsBoost = 0.00f,
                VagirBoost = 0.00f,
                RoyalistVlandiaBoost = 0.00f,
                ApolssalyBoost = 0.00f,
                LyrionBoost = 0.00f,
                RebelKhuzaitBoost = 0.00f,
                PaleicianBoost = 0.00f,
                AriorumBoost = 0.00f,
                Empire_S_CEK_Boost = 0.00f,
                Empire_CEK_Boost = 0.00f,
                Battania_CEK_Boost = 0.00f,
                Vlandia_CEK_Boost = 0.00f,
                Sturgia_CEK_Boost = 0.00f,
                Khuzait_CEK_Boost = 0.00f,
                Aserai_CEK_Boost = 0.00f,
                Empire_W_CEK_Boost = 0.00f,
                Player_CEK_Boost = 0.00f,
                BalancingPartySizeTweaksEnabled = false,
                BalancingPartyLimitTweaksEnabled = false,
                BalancingFoodTweakEnabled = false,
                BalancingTimeRecruitsTweaksEnabled = false,
                BalancingTaxTweaksEnabled = false,
                BalancingWagesTweaksEnabled = false,
                BalancingUpgradeTroopsTweaksEnabled = false,


                //~ Miscellaneous
                QuestCharactersIgnorePartySize = false,
                ShowFoodDaysRemaining = false,
                CampaignSpeed = 4,
                UnlimitedWanderersPatch = false,

                //~ Party Tweaks
                MCMPartyTweaksEnabled = false,

                //~ Caravan Tweaks
                PlayerCaravanPartySizeTweakEnabled = false,
                PlayerCaravanPartySize = 30,

                //~ Party Size Tweaks
                PartySizeTweakEnabled = false,
                LeadershipPartySizeBonusEnabled = false,
                LeadershipPartySizeBonus = 0.0f,
                StewardPartySizeBonusEnabled = false,
                StewardPartySizeBonus = 0f,
                PartySizeTweakAIFactor = 0f,
                PartySizeMultipliersEnabled = false,
                PartySizeBanditMultiplier = 1f,
                PartySizeVillagerMultiplier = 1f,
                PartySizeCarvanMultiplier = 1f,
                PartySizeMilitiaMultiplier = 1f,

                //~ Party Wage Tweaks
                PartyWageTweaksEnabled = false,
                PartyWagePercent = 1.0f,
                GarrisonWagePercent = 1.0f,
                ApplyWageTweakToFaction = false,
                ApplyWageTweakToAI = false,

                //~ MobilePartyFoodConsumption
                PartyFoodConsumptionEnabled = false,
                PartyFoodConsumptionMultiplier = 1.0f,


                //~ Pregnancy 
                MCMPregnancyModifiers = false,

                //~ Duration
                PregnancyDurationModifiers = false,
                PregnancyDurationValue = 36,


                //~ MortalityProbabilityInLabor
                NoMaternalMortalityTweakEnabled = false,
                PregnancyLaborMortalityChanceModifiers = false,
                PregnancyLaborMortalityChanceValue = 0.015f,

                //~ StillbirthProbability
                NoStillbirthsTweakEnabled = false,
                PregnancyStillbirthChanceModifiers = false,
                PregnancyStillbirthChanceValue = 0.01f,


                //~ DeliveringFemaleOffspringProbability
                PregnancyFemaleOffspringChanceModifiers = false,
                PregnancyFemaleOffspringChanceValue = 0.51f,

                //~ DeliveringTwinsProbability
                PregnancyTwinsChanceModifiers = false,
                PregnancyTwinsChanceValue = 0.03f,

                //~ Chance Tweaks
                DailyChancePregnancyTweakEnabled = false,
                PlayerCharacterInfertileEnabled = false,
                MinPregnancyAge = 18,
                MaxPregnancyAge = 45,
                ClanFertilityBonus = 1f,
                MaxChildren = 5,


                //~ Prisoner Tweaks
                MCMPrisonerTweaksEnabled = false,

                //~ Imprisonment Time Tweaks
                PrisonerImprisonmentTweakEnabled = false,
                PrisonerImprisonmentPlayerOnly = false,
                MinimumDaysOfImprisonment = 0,
                EnableMissingHeroFix = false,


                //~ Party Prisoner Size Tweak
                PrisonerSizeTweakEnabled = false,
                PrisonerSizeTweakPercent = 0,
                PrisonerSizeTweakAI = false,


                //~ Prisoner Conformity Tweaks
                PrisonerConformityTweaksEnabled = false,
                PrisonerConformityTweakBonus = 0,
                PrisonerConformityTweaksApplyToClan = false,
                PrisonerConformityTweaksApplyToAi = false,


                //~ Settlement Tweaks
                MCMSettlementTweaksEnabled = false,

                //~ Settlement Culture Transformation
                EnableCultureChanger = false,
                ChangeToKingdomCulture = false,
                TimeToChanceCulture = 10,

                //~ Settlement Tweaks - Disable Troop Donations
                DisableTroopDonationPatchEnabled = false,
                DisableTroopDonationAnyEnabled = false,

                //~ Settlement Tweaks - Production Tweaks
                ProductionTweakEnabled = false,
                ProductionFoodTweakAmount = 1f,
                ProductionOtherTweakAmount = 1f,


                CastleTrainingFieldsBonusEnabled = false,
                CastleTrainingFieldsXpAmountLevel1 = 1,
                CastleTrainingFieldsXpAmountLevel2 = 2,
                CastleTrainingFieldsXpAmountLevel3 = 3,

                //~ Settlement Tweaks - Buildings - Castle - Granary 
                CastleGranaryBonusEnabled = false,
                CastleGranaryStorageAmountLevel1 = 100,
                CastleGranaryStorageAmountLevel2 = 200,
                CastleGranaryStorageAmountLevel3 = 300,

                //~ Settlement Tweaks - Buildings - Castle - Gardens
                CastleGardensBonusEnabled = false,
                CastleGardensFoodProductionAmountLevel1 = 5,
                CastleGardensFoodProductionAmountLevel2 = 10,
                CastleGardensFoodProductionAmountLevel3 = 15,

                //~ Settlement Tweaks - Buildings - Castle - Militia Barracks
                CastleMilitiaBarracksBonusEnabled = false,
                CastleMilitiaBarracksAmountLevel1 = 1,
                CastleMilitiaBarracksAmountLevel2 = 2,
                CastleMilitiaBarracksAmountLevel3 = 3,


                //~ Settlement Tweaks - Buildings - Town

                //~ Settlement Tweaks - Buildings - Town - Training Fields
                TownTrainingFieldsBonusEnabled = false,
                TownTrainingFieldsXpAmountLevel1 = 30,
                TownTrainingFieldsXpAmountLevel2 = 60,
                TownTrainingFieldsXpAmountLevel3 = 100,

                //~ Settlement Tweaks - Buildings - Town - Granary
                TownGranaryBonusEnabled = false,
                TownGranaryStorageAmountLevel1 = 200,
                TownGranaryStorageAmountLevel2 = 400,
                TownGranaryStorageAmountLevel3 = 600,

                //~ Settlement Tweaks - Settlement Buildings Tweaks - Town Buildings Tweaks - Orchards Tweak
                TownOrchardsBonusEnabled = false,
                TownOrchardsFoodProductionAmountLevel1 = 10,
                TownOrchardsFoodProductionAmountLevel2 = 20,
                TownOrchardsFoodProductionAmountLevel3 = 30,

                //~ Settlement Tweaks - Settlement Buildings Tweaks - Town Buildings Tweaks - Militia Barracks Tweak
                TownMilitiaBarracksBonusEnabled = false,
                TownMilitiaBarracksAmountLevel1 = 0.5f,
                TownMilitiaBarracksAmountLevel2 = 1.0f,
                TownMilitiaBarracksAmountLevel3 = 1.5f,


                //~ Settlement Tweaks - Settlement Food
                SettlementFoodBonusEnabled = false,
                CastleFoodBonus = 0f,
                TownFoodBonus = 0f,

                //~ Settlement Tweaks - Settlement Food Bonus - Food Loss from Prosperity Tweak
                SettlementProsperityFoodMalusTweakEnabled = false,
                SettlementProsperityFoodMalusDivisor = 50,

                //~ Settlement Tweaks - Normal Militia
                SettlementMilitiaBonusEnabled = false,
                CastleMilitiaBonusFlat = 0f,
                TownMilitiaBonusFlat = 0f,
                CastleMilitiaRetirementModifier = 0.025f,
                TownMilitiaRetirementModifier = 0.025f,
                VillageMilitiaRetirementModifier = 0.025f,

                //~ Settlement Tweaks - Militia Bonus Tweaks - Elite Militia
                SettlementMilitiaEliteSpawnRateBonusEnabled = false,
                SettlementEliteMeleeSpawnRateBonus = 0.1f,
                //SettlementEliteRangedSpawnRateBonus = 0.1f,

                //~ Tournaments
                MCMTournamentsEnabled = false,

                //~ Renown Reward
                TournamentRenownIncreaseEnabled = false,
                TournamentRenownAmount = 3,

                //~ Gold Reward
                TournamentGoldRewardEnabled = false,
                TournamentGoldRewardAmount = 0,

                //~ Maximum Bet
                TournamentMaxBetAmountTweakEnabled = false,
                TournamentMaxBetAmount = 150,

                //~ Hero Experience
                TournamentHeroExperienceMultiplierEnabled = false,
                TournamentHeroExperienceMultiplier = 0.33f,

                //~ Arena Hero Experience
                ArenaHeroExperienceMultiplierEnabled = false,
                ArenaHeroExperienceMultiplier = 0.06f,

                //~ Minimum Betting Odds
                MinimumBettingOddsTweakEnabled = false,
                MinimumBettingOdds = 1.1f,

                //~ Workshops
                MCMWorkshopsEnabled = false,

                //~ Workshop Limit
                MaxWorkshopCountTweakEnabled = false,
                BaseWorkshopCount = 1,
                BonusWorkshopsPerClanTier = 1,

                //~ Workshop Cost Tweak
                WorkshopBuyingCostTweakEnabled = false,
                WorkshopBaseCost = 10000,

                //~ Workshop Effectivness
                WorkshopEffectivnessEnabled = false,
                WorkshopEffectivnessv2Factor = 1f,

                //~ Workshop SellPrices
                EnableWorkshopSellTweak = false,
                WorkshopSellTweak = 1f,

                //~ Workshop Buy Prices
                EnableWorkshopBuyTweak = false,
                WorkshopBuyTweak = 1f,


                //~ Bankruptcy
                WorkShopBankruptcyModifiers = false,
                WorkShopBankruptcyValue = 3,

                //~ XP Tweaks
                MCMSkillsXp = false,

                //~ Skills
                SkillXpEnabled = false,
                SkillXpUseForPlayer = false,
                SkillXpUseForPlayerClan = false,
                SkillXpUseForAI = false,

                //~ IndividualMultipliers
                SkillXpUseIndividualMultiplers = false,
                SkillsXPMultiplierAthletics = 1.0f,
                SkillsXPMultiplierBow = 1.0f,
                SkillsXPMultiplierCharm = 1.0f,
                SkillsXPMultiplierCrafting = 1.0f,
                SkillsXPMultiplierCrossbow = 1.0f,
                SkillsXPMultiplierEngineering = 1.0f,
                SkillsXPMultiplierLeadership = 1.0f,
                SkillsXPMultiplierMedicine = 1.0f,
                SkillsXPMultiplierOneHanded = 1.0f,
                SkillsXPMultiplierPolearm = 1.0f,
                SkillsXPMultiplierRiding = 1.0f,
                SkillsXPMultiplierRoguery = 1.0f,
                SkillsXPMultiplierScouting = 1.0f,
                SkillsXPMultiplierSteward = 1.0f,
                SkillsXPMultiplierTactics = 1.0f,
                SkillsXPMultiplierThrowing = 1.0f,
                SkillsXPMultiplierTrade = 1.0f,
                SkillsXPMultiplierTwoHanded = 1.0f,

                //~ Global
                SkillXpUseGlobalMultipler = false,
                SkillsXpGlobalMultiplier = 1.0f,


                //~ LearningRateMultipliers
                LearningRateEnabled = false,
                LearningRateMultiplier = 1.0f,

                //~ Troop Daily Experience Tweak
                DailyTroopExperienceTweakEnabled = false,
                LeadershipPercentageForDailyExperienceGain = 0f,
                DailyTroopExperienceRequiredLeadershipLevel = 30,
                DailyTroopExperienceApplyToPlayerClanMembers = false,
                DailyTroopExperienceApplyToAllNPC = false,
                DisplayMessageDailyExperienceGain = false,

                //~ Troop Experience Tweaks
                TroopExperienceTweakEnabled = false,
                TroopBattleExperienceMultiplierEnabled = false,
                TroopBattleExperienceMultiplier = 1.0f,
                TroopBattleSimulationExperienceMultiplierEnabled = false,
                TroopBattleSimulationExperienceMultiplier = 0.9f,

                KaosesDynamicSpeedModifiersEnabled = false,
                DynamicFleeingSpeedReductionAmount = 0.0f,
                DynamicFleeingSpeedReductionHours = 1,
                KaosesStaticSpeedModifiersEnabled = false,
                KaosesmininumSpeedAmount = 1.0f,
                LooterSpeedReductionAmount = 0.0f,
                SteppeSpeedReductionAmount = 0.0f,
                DesertSpeedReductionAmount = 0.0f,
                MountainSpeedReductionAmount = 0.0f,
                ForestSpeedReductionAmount = 0.0f,
                SeaRaiderSpeedReductionAmount = 0.0f,
                CaravanSpeedReductiontEnabled = false,
                CaravanSpeedReductionAmount = 0.0f,
                EliteCaravanSpeedReductionAmount = 0.0f,
                VillagerSpeedReductiontEnabled = false,
                VillagerSpeedReductionAmount = -0.5f,
                PlayerSpeedReductiontEnabled = false,
                PlayerSpeedReductionAmount = 0.0f,
                PlayerClanSpeedReductionAmount = 0.0f,
                KingdomSpeedReductiontEnabled = false,
                KingdomSpeedReductionAmount = 0.0f,
                OtherKingdomSpeedReductionAmount = 0.0f,

                ArrowMultipliersEnabled = false,
                ArrowMultiplier = 1.0f,
                ArrowValueMultiplier = 1.0f,
                BoltsMultipliersEnabled = false,
                BoltsMultiplier = 1.0f,
                BoltsValueMultiplier = 1.0f,
                BoltsWeightMultiplier = 1.0f,
                ThrownMultiplierEnabled = false,
                ThrownMultiplier = 1.0f,
                ThrownValueMultiplier = 1.0f,
                ThrownWeightMultiplier = 1.0f,
                ThrownMissionFixMultiplierEnabled = false


            });

            basePresets.Add("native all on", () => new MCMSettings()
            {

                AgeTweaksEnabled = true,
                BecomeInfantAge = 3,
                BecomeChildAge = 6,
                BecomeTeenagerAge = 14,
                HeroComesOfAge = 18,
                BecomeOldAge = 47,
                MaxAge = 125,


                //~ ArmyManagement
                MCMArmy = true,
                //~ Cohesion
                armyCohesionMultipliers = true,
                armyCohesionBaseChange = -2,
                armyDisableCohesionLossClanOnlyParties = true,
                armyApplyMultiplerToClanOnlyParties = true,
                armyCohesionLossMultiplier = 1.0f,

                //~ Battle Tweaks 
                battleTweaksEnabled = true,

                //~ Hideout Tweaks
                HideoutBattleTroopLimitTweakEnabled = true,
                HideoutBattleTroopLimit = 10,
                ContinueHideoutBattleOnPlayerDeath = true,
                ContinueHideoutBattleOnPlayerLoseDuel = true,

                //~ Battle Size Tweak
                BattleSizeTweakEnabled = true,
                BattleSize = 1000,

                //~ Siege Tweaks
                SiegeTweaksEnabled = true,
                SiegeConstructionProgressPerDayMultiplier = 1f,
                SiegeCollateralDamageCasualties = 0,
                SiegeDestructionCasualties = 0,

                //~ Weapon Cut Through Tweaks
                SliceThroughEnabled = true,
                TwoHandedWeaponsSliceThroughEnabled = true,
                SingleHandedWeaponsSliceThroughEnabled = true,

                //~ Battle Rewards
                MCMBattleRewardModifiers = true,
                BattleRewardApplyToAI = true,

                //~ Relationship Gain
                BattleRewardsRelationShipGainModifiers = true,
                BattleRewardsRelationShipGainMultiplier = 1.0f,

                //~ Renown Gain
                BattleRewardsRenownGainModifiers = true,
                BattleRewardsRenownGainMultiplier = 1.0f,

                //~ Influence Gain
                BattleRewardsInfluenceGainModifiers = true,
                BattleRewardsInfluenceGainMultiplier = 1.0f,

                //~ Morale Gain
                BattleRewardsMoraleGainModifiers = true,
                BattleRewardsMoraleGainMultiplier = 1.0f,

                //~ Gold loss after defeat
                BattleRewardsGoldLossModifiers = true,
                BattleRewardsGoldLossMultiplier = 1.0f,

                //~ Campaign Tweaks
                CampaignTweaksEnabled = true,

                //~ Difficulty Settings
                DifficultyTweakEnabled = true,
                DamageToPlayerTweakEnabled = true,
                DamageToPlayerMultiplier = 0.35f,
                //DamageToFriendsTweakEnabled = true,
                //DamageToFriendsMultiplier = 0.35f,
                DamageToTroopsTweakEnabled = true,
                DamageToTroopsMultiplier = 0.35f,
                CombatAIDifficultyTweakEnabled = true,
                CombatAIDifficultyMultiplier = 0.35f,
                PlayerMapMovementSpeedBonusTweakEnabled = true,
                PlayerMapMovementSpeedBonusMultiplier = 0.5f,


                //~ Clan *AI Tweaks need Checking
                MCMClanModifiers = true,

                //~ Party Limit
                ClanAdditionalPartyLimitEnabled = true,
                ClanPlayerPartiesLimitEnabled = true,

                //~ Limits Player
                ClanPlayerBasePartiesLimit = 1,
                ClanPlayerPartiesBonusPerClanTier = 0.5f,
                ClanAIPartiesLimitTweakEnabled = true,

                //~ AI Parties Limits
                ClanAIBaseClanPartiesLimit = 1,
                ClanAIPartiesBonusPerClanTier = 0.5f,
                ClanAIMinorClanPartiesLimitTweakEnabled = true,
                AICustomSpawnPartiesLimitTweakEnabled = true,

                //~ CS Parties
                BaseAICustomSpawnPartiesLimit = 0,
                ClanCSPartiesBonusPerClanTier = 0.5f,


                //~ Companion Limit
                ClanCompanionLimitEnabled = true,
                ClanAdditionalCompanionLimit = 0,
                ClanCompanionBaseLimit = 3,


                //~ Crafting Tweaks
                MCMSmithingModifiers = true, // Activates the Model Override
                MCMSmithingHarmoneyPatches = false, // Activates the Model Override

                //~ Stamina Tweaks
                CraftingStaminaTweakEnabled = true,
                MaxCraftingStaminaMultiplier = 1.0f,

                //~ Stamina Gains
                CraftingStaminaGainAmount = 5,
                CraftingStaminaGainOutsideSettlementMultiplier = 0f,

                //~ Energy Cost Multipliers
                SmithingEnergySmithingValue = 1.0f,
                SmithingEnergySmeltingValue = 1.0f,
                SmithingEnergyRefiningValue = 1.0f,
                SmithingEnergyDisable = true,

                //~ BT Smelting Tweaks
                SmeltingTweakEnabled = true,
                PreventSmeltingLockedItems = false,
                AutoLearnSmeltedParts = true,


                //~ Xp Modifiers

                SmithingXpModifiers = true,
                SmithingRefiningXpValue = 1.0f,
                SmithingSmeltingXpValue = 1.0f,
                SmithingSmithingXpValue = 1.0f,

                //~ CharacterDevelopment
                MCMCharacterDevlopmentModifiers = true,

                //~ Levels Per Attribute
                CharacterLevelsPerAttributeModifiers = true,
                CharacterLevelsPerAttributeValue = 4,

                //~ Focus Per Level
                CharacterFocusPerLevelModifiers = true,
                CharacterFocusPerLevelValue = 1,


                //~ Items
                MCMItemModifiers = true,

                //~ Armor
                MCMArmorModifiers = true,

                //~ Weight
                ItemArmorWeightModifiers = true,
                ItemArmorTier1WeightMultiplier = 1.0f,
                ItemArmorTier2WeightMultiplier = 1.0f,
                ItemArmorTier3WeightMultiplier = 1.0f,
                ItemArmorTier4WeightMultiplier = 1.0f,
                ItemArmorTier5WeightMultiplier = 1.0f,
                ItemArmorTier6WeightMultiplier = 1.0f,

                //~ Price
                ItemArmorValueModifiers = true,
                ItemArmorTier1PriceMultiplier = 1.0f,
                ItemArmorTier2PriceMultiplier = 1.0f,
                ItemArmorTier3PriceMultiplier = 1.0f,
                ItemArmorTier4PriceMultiplier = 1.0f,
                ItemArmorTier5PriceMultiplier = 1.0f,
                ItemArmorTier6PriceMultiplier = 1.0f,


                //~ Food
                MCMFoodModifiers = true,

                //~ Weight
                ItemFoodWeightMorale0Multiplier = 1.0f,
                ItemFoodWeightMorale1Multiplier = 1.0f,
                ItemFoodWeightMorale2Multiplier = 1.0f,
                ItemFoodWeightMorale3Multiplier = 1.0f,

                //~ Price
                ItemFoodPriceMorale0Multiplier = 1.0f,
                ItemFoodPriceMorale1Multiplier = 1.0f,
                ItemFoodPriceMorale2Multiplier = 1.0f,
                ItemFoodPriceMorale3Multiplier = 1.0f,


                //~ Melee Weapons
                MCMMeleeWeaponModifiers = true,

                //~ Weight
                ItemMeleeWeaponWeightModifiers = false,
                ItemMeleeWeaponTier1WeightMultiplier = 1.0f,
                ItemMeleeWeaponTier2WeightMultiplier = 1.0f,
                ItemMeleeWeaponTier3WeightMultiplier = 1.0f,
                ItemMeleeWeaponTier4WeightMultiplier = 1.0f,
                ItemMeleeWeaponTier5WeightMultiplier = 1.0f,
                ItemMeleeWeaponTier6WeightMultiplier = 1.0f,

                //~ Price
                ItemMeleeWeaponValueModifiers = true,
                ItemMeleeWeaponTier1PriceMultiplier = 1.0f,
                ItemMeleeWeaponTier2PriceMultiplier = 1.0f,
                ItemMeleeWeaponTier3PriceMultiplier = 1.0f,
                ItemMeleeWeaponTier4PriceMultiplier = 1.0f,
                ItemMeleeWeaponTier5PriceMultiplier = 1.0f,
                ItemMeleeWeaponTier6PriceMultiplier = 1.0f,


                //~ Ranged Weapons
                MCMRagedWeaponsModifiers = true,

                //~ Weight
                ItemRangedWeaponsWeightModifiers = false,
                ItemRangedWeaponsTier1WeightMultiplier = 1.0f,
                ItemRangedWeaponsTier2WeightMultiplier = 1.0f,
                ItemRangedWeaponsTier3WeightMultiplier = 1.0f,
                ItemRangedWeaponsTier4WeightMultiplier = 1.0f,
                ItemRangedWeaponsTier5WeightMultiplier = 1.0f,
                ItemRangedWeaponsTier6WeightMultiplier = 1.0f,

                //~ Price
                ItemRangedWeaponsValueModifiers = true,
                ItemRangedWeaponsTier1PriceMultiplier = 1.0f,
                ItemRangedWeaponsTier2PriceMultiplier = 1.0f,
                ItemRangedWeaponsTier3PriceMultiplier = 1.0f,
                ItemRangedWeaponsTier4PriceMultiplier = 1.0f,
                ItemRangedWeaponsTier5PriceMultiplier = 1.0f,
                ItemRangedWeaponsTier6PriceMultiplier = 1.0f,

                //~ Trade Goods
                MCMTradeGoodsModifiers = true,

                //~ Weight
                ItemTradeGoodsWeightMultiplier = 1.0f,

                //~ Price
                ItemTradeGoodsPriceMultiplier = 1.0f,


                //~ ItemLocks 
                MCMAutoLocks = true,
                autoLockHorses = true,
                autoLockFood = true,
                autoLockIronBar1 = true,
                autoLockIronBar2 = true,
                autoLockIronBar3 = true,
                autoLockIronBar4 = true,
                autoLockIronBar5 = true,
                autoLockIronBar6 = true,
                autoLockIronOre = true,
                autoLockSilverOre = true,
                autoLockHardwood = true,
                autoLockCharcol = true,



                //~ Killing Bandits Raises Relationships
                MCMKillingBanditsEnabled = true,
                GroupsOfBandits = 1,
                RelationshipIncrease = 1,
                Radius = 1000,
                SizeBonusEnabled = true,
                SizeBonus = 0.00f,
                PrisonersOnly = true,
                IncludeBandits = true,
                IncludeOutlaws = true,
                IncludeMafia = true,


                //~ Kingdom Tweaks
                MCMKingdomTweaksEnabled = true,

                //~ Lord Bartering
                BarterablesTweaksEnabled = true,
                BarterablesJoinKingdomAsClanAdjustment = 1,
                BarterablesJoinKingdomAsClanAltFormulaEnabled = true,

                //~ Balancing Tweaks
                KingdomBalanceStrengthEnabled = true,
                KingdomBalanceStrengthVanEnabled = true,
                VlandiaBoost = 0.00f,
                SturgiaBoost = 0.00f,
                BattaniaBoost = 0.00f,
                Empire_N_Boost = 0.00f,
                Empire_S_Boost = 0.00f,
                Empire_W_Boost = 0.00f,
                AseraiBoost = 0.00f,
                KhuzaitBoost = 0.00f,
                PlayerBoost = 0.00f,
                KingdomBalanceStrengthCEKEnabled = true,
                NordlingsBoost = 0.00f,
                VagirBoost = 0.00f,
                RoyalistVlandiaBoost = 0.00f,
                ApolssalyBoost = 0.00f,
                LyrionBoost = 0.00f,
                RebelKhuzaitBoost = 0.00f,
                PaleicianBoost = 0.00f,
                AriorumBoost = 0.00f,
                Empire_S_CEK_Boost = 0.00f,
                Empire_CEK_Boost = 0.00f,
                Battania_CEK_Boost = 0.00f,
                Vlandia_CEK_Boost = 0.00f,
                Sturgia_CEK_Boost = 0.00f,
                Khuzait_CEK_Boost = 0.00f,
                Aserai_CEK_Boost = 0.00f,
                Empire_W_CEK_Boost = 0.00f,
                Player_CEK_Boost = 0.00f,
                BalancingPartySizeTweaksEnabled = true,
                BalancingPartyLimitTweaksEnabled = true,
                BalancingFoodTweakEnabled = true,
                BalancingTimeRecruitsTweaksEnabled = true,
                BalancingTaxTweaksEnabled = true,
                BalancingWagesTweaksEnabled = true,
                BalancingUpgradeTroopsTweaksEnabled = true,


                //~ Miscellaneous
                QuestCharactersIgnorePartySize = true,
                ShowFoodDaysRemaining = false,
                CampaignSpeed = 4,
                UnlimitedWanderersPatch = false,

                //~ Party Tweaks
                MCMPartyTweaksEnabled = true,

                //~ Caravan Tweaks
                PlayerCaravanPartySizeTweakEnabled = true,
                PlayerCaravanPartySize = 30,

                //~ Party Size Tweaks
                PartySizeTweakEnabled = true,
                LeadershipPartySizeBonusEnabled = true,
                LeadershipPartySizeBonus = 0.0f,
                StewardPartySizeBonusEnabled = true,
                StewardPartySizeBonus = 0f,
                PartySizeTweakAIFactor = 0f,
                PartySizeMultipliersEnabled = true,
                PartySizeBanditMultiplier = 1f,
                PartySizeVillagerMultiplier = 1f,
                PartySizeCarvanMultiplier = 1f,
                PartySizeMilitiaMultiplier = 1f,

                //~ Party Wage Tweaks
                PartyWageTweaksEnabled = true,
                PartyWagePercent = 1.0f,
                GarrisonWagePercent = 1.0f,
                ApplyWageTweakToFaction = true,
                ApplyWageTweakToAI = true,

                //~ MobilePartyFoodConsumption
                PartyFoodConsumptionEnabled = true,
                PartyFoodConsumptionMultiplier = 1.0f,


                //~ Pregnancy 
                MCMPregnancyModifiers = true,

                //~ Duration
                PregnancyDurationModifiers = true,
                PregnancyDurationValue = 36,


                //~ MortalityProbabilityInLabor
                NoMaternalMortalityTweakEnabled = true,
                PregnancyLaborMortalityChanceModifiers = true,
                PregnancyLaborMortalityChanceValue = 0.015f,

                //~ StillbirthProbability
                NoStillbirthsTweakEnabled = true,
                PregnancyStillbirthChanceModifiers = true,
                PregnancyStillbirthChanceValue = 0.01f,


                //~ DeliveringFemaleOffspringProbability
                PregnancyFemaleOffspringChanceModifiers = true,
                PregnancyFemaleOffspringChanceValue = 0.51f,

                //~ DeliveringTwinsProbability
                PregnancyTwinsChanceModifiers = true,
                PregnancyTwinsChanceValue = 0.03f,

                //~ Chance Tweaks
                DailyChancePregnancyTweakEnabled = true,
                PlayerCharacterInfertileEnabled = true,
                MinPregnancyAge = 18,
                MaxPregnancyAge = 45,
                ClanFertilityBonus = 1f,
                MaxChildren = 5,


                //~ Prisoner Tweaks
                MCMPrisonerTweaksEnabled = true,

                //~ Imprisonment Time Tweaks
                PrisonerImprisonmentTweakEnabled = true,
                PrisonerImprisonmentPlayerOnly = true,
                MinimumDaysOfImprisonment = 0,
                EnableMissingHeroFix = true,


                //~ Party Prisoner Size Tweak
                PrisonerSizeTweakEnabled = true,
                PrisonerSizeTweakPercent = 0,
                PrisonerSizeTweakAI = true,


                //~ Prisoner Confirmity Tweaks
                PrisonerConformityTweaksEnabled = true,
                PrisonerConformityTweakBonus = 0,
                PrisonerConformityTweaksApplyToClan = true,
                PrisonerConformityTweaksApplyToAi = true,


                //~ Settlement Tweaks
                MCMSettlementTweaksEnabled = true,

                //~ Settlement Culture Transformation
                EnableCultureChanger = false,
                ChangeToKingdomCulture = false,
                TimeToChanceCulture = 10,

                //~ Settlement Tweaks - Disable Troop Donations
                DisableTroopDonationPatchEnabled = false,
                DisableTroopDonationAnyEnabled = false,

                //~ Settlement Tweaks - Production Tweaks
                ProductionTweakEnabled = true,
                ProductionFoodTweakAmount = 1f,
                ProductionOtherTweakAmount = 1f,


                CastleTrainingFieldsBonusEnabled = false,
                CastleTrainingFieldsXpAmountLevel1 = 1,
                CastleTrainingFieldsXpAmountLevel2 = 2,
                CastleTrainingFieldsXpAmountLevel3 = 3,

                //~ Settlement Tweaks - Buildings - Castle - Granary 
                CastleGranaryBonusEnabled = false,
                CastleGranaryStorageAmountLevel1 = 100,
                CastleGranaryStorageAmountLevel2 = 200,
                CastleGranaryStorageAmountLevel3 = 300,

                //~ Settlement Tweaks - Buildings - Castle - Gardens
                CastleGardensBonusEnabled = false,
                CastleGardensFoodProductionAmountLevel1 = 5,
                CastleGardensFoodProductionAmountLevel2 = 10,
                CastleGardensFoodProductionAmountLevel3 = 15,

                //~ Settlement Tweaks - Buildings - Castle - Militia Barracks
                CastleMilitiaBarracksBonusEnabled = false,
                CastleMilitiaBarracksAmountLevel1 = 1,
                CastleMilitiaBarracksAmountLevel2 = 2,
                CastleMilitiaBarracksAmountLevel3 = 3,


                //~ Settlement Tweaks - Buildings - Town

                //~ Settlement Tweaks - Buildings - Town - Training Fields
                TownTrainingFieldsBonusEnabled = false,
                TownTrainingFieldsXpAmountLevel1 = 30,
                TownTrainingFieldsXpAmountLevel2 = 60,
                TownTrainingFieldsXpAmountLevel3 = 100,

                //~ Settlement Tweaks - Buildings - Town - Granary
                TownGranaryBonusEnabled = false,
                TownGranaryStorageAmountLevel1 = 200,
                TownGranaryStorageAmountLevel2 = 400,
                TownGranaryStorageAmountLevel3 = 600,

                //~ Settlement Tweaks - Settlement Buildings Tweaks - Town Buildings Tweaks - Orchards Tweak
                TownOrchardsBonusEnabled = false,
                TownOrchardsFoodProductionAmountLevel1 = 10,
                TownOrchardsFoodProductionAmountLevel2 = 20,
                TownOrchardsFoodProductionAmountLevel3 = 30,

                //~ Settlement Tweaks - Settlement Buildings Tweaks - Town Buildings Tweaks - Militia Barracks Tweak
                TownMilitiaBarracksBonusEnabled = false,
                TownMilitiaBarracksAmountLevel1 = 0.5f,
                TownMilitiaBarracksAmountLevel2 = 1.0f,
                TownMilitiaBarracksAmountLevel3 = 1.5f,


                //~ Settlement Tweaks - Settlement Food
                SettlementFoodBonusEnabled = false,
                CastleFoodBonus = 0f,
                TownFoodBonus = 0f,

                //~ Settlement Tweaks - Settlement Food Bonus - Food Loss from Prosperity Tweak
                SettlementProsperityFoodMalusTweakEnabled = false,
                SettlementProsperityFoodMalusDivisor = 50,

                //~ Settlement Tweaks - Normal Militia
                SettlementMilitiaBonusEnabled = true,
                CastleMilitiaBonusFlat = 0f,
                TownMilitiaBonusFlat = 0f,
                CastleMilitiaRetirementModifier = 0.025f,
                TownMilitiaRetirementModifier = 0.025f,
                VillageMilitiaRetirementModifier = 0.025f,

                //~ Settlement Tweaks - Militia Bonus Tweaks - Elite Militia
                SettlementMilitiaEliteSpawnRateBonusEnabled = true,
                SettlementEliteMeleeSpawnRateBonus = 0.1f,
                //SettlementEliteRangedSpawnRateBonus = 0.1f,

                //~ Tournaments
                MCMTournamentsEnabled = true,

                //~ Renown Reward
                TournamentRenownIncreaseEnabled = true,
                TournamentRenownAmount = 3,

                //~ Gold Reward
                TournamentGoldRewardEnabled = true,
                TournamentGoldRewardAmount = 0,

                //~ Maximum Bet
                TournamentMaxBetAmountTweakEnabled = true,
                TournamentMaxBetAmount = 150,

                //~ Hero Experience
                TournamentHeroExperienceMultiplierEnabled = true,
                TournamentHeroExperienceMultiplier = 0.33f,

                //~ Arena Hero Experience
                ArenaHeroExperienceMultiplierEnabled = true,
                ArenaHeroExperienceMultiplier = 0.06f,

                //~ Minimum Betting Odds
                MinimumBettingOddsTweakEnabled = true,
                MinimumBettingOdds = 1.1f,

                //~ Workshops
                MCMWorkshopsEnabled = true,

                //~ Workshop Limit
                MaxWorkshopCountTweakEnabled = true,
                BaseWorkshopCount = 1,
                BonusWorkshopsPerClanTier = 1,

                //~ Workshop Cost Tweak
                WorkshopBuyingCostTweakEnabled = true,
                WorkshopBaseCost = 10000,

                //~ Workshop Effectivness
                WorkshopEffectivnessEnabled = true,
                WorkshopEffectivnessv2Factor = 1f,

                //~ Workshop SellPrices
                EnableWorkshopSellTweak = true,
                WorkshopSellTweak = 1f,

                //~ Workshop Buy Prices
                EnableWorkshopBuyTweak = true,
                WorkshopBuyTweak = 1f,


                //~ Bankruptcy
                WorkShopBankruptcyModifiers = true,
                WorkShopBankruptcyValue = 3,

                //~ XP Tweaks
                MCMSkillsXp = true,

                //~ Skills
                SkillXpEnabled = true,
                SkillXpUseForPlayer = true,
                SkillXpUseForPlayerClan = true,
                SkillXpUseForAI = true,

                //~ IndividualMultipliers
                SkillXpUseIndividualMultiplers = true,
                SkillsXPMultiplierAthletics = 1.0f,
                SkillsXPMultiplierBow = 1.0f,
                SkillsXPMultiplierCharm = 1.0f,
                SkillsXPMultiplierCrafting = 1.0f,
                SkillsXPMultiplierCrossbow = 1.0f,
                SkillsXPMultiplierEngineering = 1.0f,
                SkillsXPMultiplierLeadership = 1.0f,
                SkillsXPMultiplierMedicine = 1.0f,
                SkillsXPMultiplierOneHanded = 1.0f,
                SkillsXPMultiplierPolearm = 1.0f,
                SkillsXPMultiplierRiding = 1.0f,
                SkillsXPMultiplierRoguery = 1.0f,
                SkillsXPMultiplierScouting = 1.0f,
                SkillsXPMultiplierSteward = 1.0f,
                SkillsXPMultiplierTactics = 1.0f,
                SkillsXPMultiplierThrowing = 1.0f,
                SkillsXPMultiplierTrade = 1.0f,
                SkillsXPMultiplierTwoHanded = 1.0f,

                //~ Global
                SkillXpUseGlobalMultipler = false,
                SkillsXpGlobalMultiplier = 1.0f,


                //~ LearningRateMultipliers
                LearningRateEnabled = true,
                LearningRateMultiplier = 1.0f,

                //~ Troop Daily Experience Tweak
                DailyTroopExperienceTweakEnabled = true,
                LeadershipPercentageForDailyExperienceGain = 0f,
                DailyTroopExperienceRequiredLeadershipLevel = 30,
                DailyTroopExperienceApplyToPlayerClanMembers = true,
                DailyTroopExperienceApplyToAllNPC = true,
                DisplayMessageDailyExperienceGain = true,

                //~ Troop Experience Tweaks
                TroopExperienceTweakEnabled = true,
                TroopBattleExperienceMultiplierEnabled = true,
                TroopBattleExperienceMultiplier = 1.0f,
                TroopBattleSimulationExperienceMultiplierEnabled = true,
                TroopBattleSimulationExperienceMultiplier = 0.9f,

                KaosesDynamicSpeedModifiersEnabled = true,
                DynamicFleeingSpeedReductionAmount = 0.0f,
                DynamicFleeingSpeedReductionHours = 1,
                KaosesStaticSpeedModifiersEnabled = true,
                KaosesmininumSpeedAmount = 1.0f,
                LooterSpeedReductionAmount = 0.0f,
                SteppeSpeedReductionAmount = 0.0f,
                DesertSpeedReductionAmount = 0.0f,
                MountainSpeedReductionAmount = 0.0f,
                ForestSpeedReductionAmount = 0.0f,
                SeaRaiderSpeedReductionAmount = 0.0f,
                CaravanSpeedReductiontEnabled = true,
                CaravanSpeedReductionAmount = 0.0f,
                EliteCaravanSpeedReductionAmount = 0.0f,
                VillagerSpeedReductiontEnabled = true,
                VillagerSpeedReductionAmount = -0.5f,
                PlayerSpeedReductiontEnabled = true,
                PlayerSpeedReductionAmount = 0.0f,
                PlayerClanSpeedReductionAmount = 0.0f,
                KingdomSpeedReductiontEnabled = true,
                KingdomSpeedReductionAmount = 0.0f,
                OtherKingdomSpeedReductionAmount = 0.0f,

                ArrowMultipliersEnabled = true,
                ArrowMultiplier = 1.0f,
                ArrowValueMultiplier = 1.0f,
                BoltsMultipliersEnabled = true,
                BoltsMultiplier = 1.0f,
                BoltsValueMultiplier = 1.0f,
                BoltsWeightMultiplier = 1.0f,
                ThrownMultiplierEnabled = true,
                ThrownMultiplier = 1.0f,
                ThrownValueMultiplier = 1.0f,
                ThrownWeightMultiplier = 1.0f,
                ThrownMissionFixMultiplierEnabled = false


            });

            /*
            basePresets.Add("True", () => new MCMSettings()
            {
                Property1 = true,
                Property2 = true
            });
            */

            basePresets.Add("Bannerlord Tweaks defaults 1.5.7.2", () => new MCMSettings()
            {
                #region Preset Restore defaults from 1.5.7.2
                QuestCharactersIgnorePartySize = false,
                ShowFoodDaysRemaining = false,
                MinimumDaysOfImprisonment = 10,
                PrisonerImprisonmentTweakEnabled = false,
                PrisonerImprisonmentPlayerOnly = true,
                EnableMissingHeroFix = true,
                PrisonerSizeTweakEnabled = false,
                PrisonerSizeTweakPercent = 0f,
                PrisonerConformityTweaksApplyToAi = false,
                PrisonerConformityTweaksApplyToClan = false,
                PrisonerConformityTweaksEnabled = false,
                PrisonerConformityTweakBonus = 0f,
                CastleMilitiaBonusFlat = 1.25f,
                TownMilitiaBonusFlat = 2.5f,
                SettlementMilitiaBonusEnabled = false,
                SettlementEliteMeleeSpawnRateBonus = 0.15f,
                //SettlementEliteRangedSpawnRateBonus = 0.1f,
                SettlementMilitiaEliteSpawnRateBonusEnabled = true,
                CastleFoodBonus = 2.0f,
                TownFoodBonus = 4.0f,
                SettlementFoodBonusEnabled = true,
                SettlementProsperityFoodMalusDivisor = 100,
                SettlementProsperityFoodMalusTweakEnabled = true,
                DisableTroopDonationPatchEnabled = false,
                DisableTroopDonationAnyEnabled = false,
                BaseWorkshopCount = 2,
                BonusWorkshopsPerClanTier = 1,
                MaxWorkshopCountTweakEnabled = true,
                WorkshopBaseCost = 10000,
                WorkshopBuyingCostTweakEnabled = false,
                TournamentHeroExperienceMultiplier = 0.25f,
                TournamentHeroExperienceMultiplierEnabled = false,
                TournamentRenownAmount = 3,
                TournamentRenownIncreaseEnabled = true,
                MinimumBettingOdds = 1.1f,
                MinimumBettingOddsTweakEnabled = false,
                TournamentMaxBetAmount = 500,
                TournamentMaxBetAmountTweakEnabled = true,
                TournamentGoldRewardAmount = 500,
                TournamentGoldRewardEnabled = true,
                ArenaHeroExperienceMultiplier = 0.06f,
                ArenaHeroExperienceMultiplierEnabled = false,
                TownTrainingFieldsXpAmountLevel1 = 30,
                TownTrainingFieldsXpAmountLevel2 = 60,
                TownTrainingFieldsXpAmountLevel3 = 100,
                TownTrainingFieldsBonusEnabled = true,
                TownOrchardsFoodProductionAmountLevel1 = 10,
                TownOrchardsFoodProductionAmountLevel2 = 20,
                TownOrchardsFoodProductionAmountLevel3 = 30,
                TownOrchardsBonusEnabled = true,
                TownMilitiaBarracksAmountLevel1 = 0.5f,
                TownMilitiaBarracksAmountLevel2 = 1.0f,
                TownMilitiaBarracksAmountLevel3 = 1.5f,
                TownMilitiaBarracksBonusEnabled = true,
                TownGranaryStorageAmountLevel1 = 200,
                TownGranaryStorageAmountLevel2 = 400,
                TownGranaryStorageAmountLevel3 = 600,
                TownGranaryBonusEnabled = true,
                CastleTrainingFieldsXpAmountLevel1 = 1,
                CastleTrainingFieldsXpAmountLevel2 = 2,
                CastleTrainingFieldsXpAmountLevel3 = 3,
                CastleTrainingFieldsBonusEnabled = true,
                CastleMilitiaBarracksAmountLevel1 = 1,
                CastleMilitiaBarracksAmountLevel2 = 2,
                CastleMilitiaBarracksAmountLevel3 = 3,
                CastleMilitiaBarracksBonusEnabled = true,
                CastleGranaryStorageAmountLevel1 = 100,
                CastleGranaryStorageAmountLevel2 = 200,
                CastleGranaryStorageAmountLevel3 = 300,
                CastleGranaryBonusEnabled = true,
                CastleGardensFoodProductionAmountLevel1 = 5,
                CastleGardensFoodProductionAmountLevel2 = 10,
                CastleGardensFoodProductionAmountLevel3 = 15,
                CastleGardensBonusEnabled = true,
                ApplyWageTweakToFaction = false,
                PartyWageTweaksEnabled = false,
                PartyWagePercent = 1.0f,
                GarrisonWagePercent = 1.0f,
                LeadershipPercentageForDailyExperienceGain = 0.5f,
                DailyTroopExperienceRequiredLeadershipLevel = 30,
                DailyTroopExperienceApplyToPlayerClanMembers = false,
                DailyTroopExperienceApplyToAllNPC = false,
                DisplayMessageDailyExperienceGain = false,
                DailyTroopExperienceTweakEnabled = false,
                LeadershipPartySizeBonus = 0.3f,
                StewardPartySizeBonus = 0.3f,
                LeadershipPartySizeBonusEnabled = true,
                PartySizeTweakEnabled = true,
                StewardPartySizeBonusEnabled = true,
                PlayerCaravanPartySize = 30,
                PlayerCaravanPartySizeTweakEnabled = false,
                //ClanArmyLosesNoCohesionEnabled = false,
                BarterablesTweaksEnabled = true,
                BarterablesJoinKingdomAsClanAdjustment = 100,
                BarterablesJoinKingdomAsClanAltFormulaEnabled = false,
                AutoLearnSmeltedParts = true,
                PreventSmeltingLockedItems = false,
                CraftingStaminaGainAmount = 10,
                CraftingStaminaGainOutsideSettlementMultiplier = 1.0f,
                MaxCraftingStaminaMultiplier = 1.0f,
                CraftingStaminaTweakEnabled = true,
                //IgnoreCraftingStamina = false,

                /*
                                CompanionBaseLimit = 3,
                                CompanionLimitBonusPerClanTier = 3,
                                CompanionLimitTweakEnabled = false,
                                UnlimitedWanderersPatch = false,
                                BaseClanPartiesLimit = 1,
                                ClanPartiesBonusPerClanTier = 0.5f,
                                ClanPartiesLimitTweakEnabled = false,
                                BaseAICustomSpawnPartiesLimit = 1,
                                AICustomSpawnPartiesLimitTweakEnabled = false,
                                BaseAIClanPartiesLimit = 1,
                                AIMinorClanPartiesLimitTweakEnabled = false,
                                AIClanPartiesLimitTweakEnabled = false,*/

                NoMaternalMortalityTweakEnabled = false,
                NoStillbirthsTweakEnabled = false,
                /*
                                TwinsProbability = 0.03f,
                                TwinsProbabilityTweakEnabled = false,
                                PregnancyDuration = 36,
                                PregnancyDurationTweakEnabled = false,*/
                MaxPregnancyAge = 45,
                MinPregnancyAge = 18,
                DailyChancePregnancyTweakEnabled = false,
                PlayerCharacterInfertileEnabled = false,
                MaxChildren = 5,
                ClanFertilityBonus = 1.1f,
                /*
                                FemaleOffspringProbability = 0.51f,
                                FemaleOffspringProbabilityTweakEnabled = false,*/
                /*
                                CompanionSkillExperienceMultiplier = 1.0f,
                                HeroSkillExperienceMultiplier = 1.0f,
                                CompanionSkillExperienceMultiplierEnabled = false,
                                HeroSkillExperienceMultiplierEnabled = false,
                                SkillExperienceMultipliersEnabled = false,
                                SkillBonusEngineering = 1.0f,
                                SkillBonusLeadership = 1.0f,
                                SkillBonusMedicine = 1.0f,
                                SkillBonusRiding = 1.0f,
                                SkillBonusRoguery = 1.0f,
                                SkillBonusScouting = 1.0f,
                                SkillBonusTrade = 1.0f,
                                PerSkillBonusEnabled = false,*/
                //FocusPointsPerLevel = 1,
                //AttributePointRequiredLevel = 4,
                //AttributeFocusPointTweakEnabled = false,
                AgeTweaksEnabled = false,
                BecomeChildAge = 6,
                BecomeInfantAge = 3,
                BecomeTeenagerAge = 14,
                HeroComesOfAge = 18,
                BecomeOldAge = 47,
                MaxAge = 125,
                DifficultyTweakEnabled = false,
                PlayerMapMovementSpeedBonusMultiplier = 0.0f,
                PlayerMapMovementSpeedBonusTweakEnabled = false,
                DamageToTroopsMultiplier = 1.0f,
                DamageToTroopsTweakEnabled = false,
                DamageToPlayerMultiplier = 1.0f,
                DamageToPlayerTweakEnabled = false,
                //DamageToFriendsMultiplier = 1.0f,
                //DamageToFriendsTweakEnabled = false,
                CombatAIDifficultyMultiplier = 0.96f,
                CombatAIDifficultyTweakEnabled = false,
                BattleSize = 1000,
                BattleSizeTweakEnabled = false,
                SingleHandedWeaponsSliceThroughEnabled = false,
                TwoHandedWeaponsSliceThroughEnabled = false,
                TroopBattleExperienceMultiplier = 1.0f,
                TroopBattleExperienceMultiplierEnabled = false,
                TroopBattleSimulationExperienceMultiplier = 1.0f,
                TroopBattleSimulationExperienceMultiplierEnabled = false,
                SiegeTweaksEnabled = false,
                SiegeConstructionProgressPerDayMultiplier = 0.8f,
                SiegeCollateralDamageCasualties = 1,
                SiegeDestructionCasualties = 0,
                HideoutBattleTroopLimit = 90,
                ContinueHideoutBattleOnPlayerLoseDuel = true,
                ContinueHideoutBattleOnPlayerDeath = false,
                HideoutBattleTroopLimitTweakEnabled = true,
                /*
                                BattleInfluenceMultiplier = 1.0f,
                                BattleRenownMultiplier = 2.0f,
                                BattleRewardTweaksEnabled = true,*/

                BattleRewardApplyToAI = true,

                #endregion
            });
            return basePresets;
        }
        #endregion


        public MCMSettings()
        {
            PropertyChanged += MCMSettings_PropertyChanged;
        }

        private void MCMSettings_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Debug))
            {
                BattleRewardShowDebug = false;
                ArmyDebug = false;
                BattleRewardsDebug = false;
                ClanDebugging = false;
                ItemDebugMode = false;
                PregnancyDebug = false;
                XpModifiersDebug = false;
                TournamentDebug = false;
                PrisonersDebug = false;
                SettlementsDebug = false;
                WandererLocationDebug = false;
                PartySizeLimitsDebug = false;
                WorkshopsDebug = false;
                CraftingDebug = false;
                LearningDebug = false;
                BattleSizeDebug = false;
                CultureChangeDebug = false;
                LogToFile = false;
            }
        }
    }
}
