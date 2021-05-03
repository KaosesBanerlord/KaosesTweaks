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
    public class MCMSettings : AttributeGlobalSettings<MCMSettings> //, ISettingsProviderInterface
    {
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

        //[SettingPropertyBool("{=debug}Debug", RequireRestart = false, HintText = "{=}{=debug_desc}Displays mod developer debug information and logs them to the file")]
        public bool Debug { get; set; } = false;

        //[SettingPropertyBool("{=debuglog}Log to file", RequireRestart = false, HintText = "{=}{=debuglog_desc}Log information messages to the log file as well as errors and debug")]
        public bool LogToFile { get; set; } = false;

        public bool LoadMCMConfigFile { get; set; } = false;
        public string ModDisplayName { get { return DisplayName; } }



        ///~ Mod Specific settings 


        [SettingPropertyBool("{=KTMCM_ItemModifiers}Item Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=}{=KTMCM_ItemModifiersHint}Enables modifying Item weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items")]
        public bool MCMItemModifiers { get; set; } = true;

        #region Items

        [SettingPropertyBool("{=KTMCM_BAM}BodyArmor Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor")]
        public bool MCMBodyArmorModifiers { get; set; } = true;
        #region BodyArmor


        [SettingPropertyBool("{=KTMCM_BAW}BodyArmor Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor/{=KTMCM_Cweight}weight")]
        public bool BodyArmorWeightModifiers { get; set; } = true;

        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor/{=KTMCM_Cweight}weight")]
        public float BodyArmorTier1WeightMultiplier { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor/{=KTMCM_Cweight}weight")]
        public float BodyArmorTier2WeightMultiplier { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor/{=KTMCM_Cweight}weight")]
        public float BodyArmorTier3WeightMultiplier { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor/{=KTMCM_Cweight}weight")]
        public float BodyArmorTier4WeightMultiplier { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor/{=KTMCM_Cweight}weight")]
        public float BodyArmorTier5WeightMultiplier { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor/{=KTMCM_Cweight}weight")]
        public float BodyArmorTier6WeightMultiplier { get; set; } = 0.5f;

        #endregion

        [SettingPropertyBool("{=KTMCM_BAP}BodyArmor Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor/{=KTMCM_CPrice}Price")]
        public bool BodyArmorValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor/{=KTMCM_CPrice}Price")]
        public float BodyArmorTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor/{=KTMCM_CPrice}Price")]
        public float BodyArmorTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor/{=KTMCM_CPrice}Price")]
        public float BodyArmorTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor/{=KTMCM_CPrice}Price")]
        public float BodyArmorTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor/{=KTMCM_CPrice}Price")]
        public float BodyArmorTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBodyArmor}BodyArmor/{=KTMCM_CPrice}Price")]
        public float BodyArmorTier6PriceMultiplier { get; set; } = 1.0f;
        
        #endregion

        #endregion


        [SettingPropertyBool("{=KTMCM_BM}Bow Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow")]
        public bool MCMBowModifiers { get; set; } = true;
        #region Bow
        [SettingPropertyBool("{=KTMCM_BMW}Bow Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow/{=KTMCM_Cweight}weight")]
        public bool BowWeightModifiers { get; set; } = true;
        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow/{=KTMCM_Cweight}weight")]
        public float BowTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow/{=KTMCM_Cweight}weight")]
        public float BowTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow/{=KTMCM_Cweight}weight")]
        public float BowTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow/{=KTMCM_Cweight}weight")]
        public float BowTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow/{=KTMCM_Cweight}weight")]
        public float BowTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow/{=KTMCM_Cweight}weight")]
        public float BowTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion



        [SettingPropertyBool("{=KTMCM_BMP}Bow Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow/{=KTMCM_CPrice}Price")]
        public bool BowValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow/{=KTMCM_CPrice}Price")]
        public float BowTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow/{=KTMCM_CPrice}Price")]
        public float BowTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow/{=KTMCM_CPrice}Price")]
        public float BowTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow/{=KTMCM_CPrice}Price")]
        public float BowTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow/{=KTMCM_CPrice}Price")]
        public float BowTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CBow}Bow/{=KTMCM_CPrice}Price")]
        public float BowTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion

        #endregion


        [SettingPropertyBool("{=KTMCM_CM}Cape Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape")]
        public bool MCMCapeModifiers { get; set; } = true;
        #region Cape


        [SettingPropertyBool("{=KTMCM_CMW}Cape Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape/{=KTMCM_Cweight}weight")]
        public bool CapeWeightModifiers { get; set; } = true;

        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape/{=KTMCM_Cweight}weight")]
        public float CapeTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape/{=KTMCM_Cweight}weight")]
        public float CapeTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape/{=KTMCM_Cweight}weight")]
        public float CapeTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
             HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape/{=KTMCM_Cweight}weight")]
        public float CapeTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape/{=KTMCM_Cweight}weight")]
        public float CapeTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape/{=KTMCM_Cweight}weight")]
        public float CapeTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion


        [SettingPropertyBool("{=KTMCM_CMP}Cape Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape/{=KTMCM_CPrice}Price")]
        public bool CapeValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape/{=KTMCM_CPrice}Price")]
        public float CapeTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape/{=KTMCM_CPrice}Price")]
        public float CapeTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape/{=KTMCM_CPrice}Price")]
        public float CapeTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape/{=KTMCM_CPrice}Price")]
        public float CapeTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape/{=KTMCM_CPrice}Price")]
        public float CapeTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCape}Cape/{=KTMCM_CPrice}Price")]
        public float CapeTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion



        #endregion


        [SettingPropertyBool("{=KTMCM_CAM}ChestArmor Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor")]
        public bool MCMChestArmorModifiers { get; set; } = true;
        #region ChestArmor
        [SettingPropertyBool("{=KTMCM_CAMW}ChestArmor Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor/{=KTMCM_Cweight}weight")]
        public bool ChestArmorWeightModifiers { get; set; } = true;

        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor/{=KTMCM_Cweight}weight")]
        public float ChestArmorTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor/{=KTMCM_Cweight}weight")]
        public float ChestArmorTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor/{=KTMCM_Cweight}weight")]
        public float ChestArmorTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor/{=KTMCM_Cweight}weight")]
        public float ChestArmorTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor/{=KTMCM_Cweight}weight")]
        public float ChestArmorTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor/{=KTMCM_Cweight}weight")]
        public float ChestArmorTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion




        [SettingPropertyBool("{=KTMCM_CAMP}ChestArmor Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor/{=KTMCM_CPrice}Price")]
        public bool ChestArmorValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor/{=KTMCM_CPrice}Price")]
        public float ChestArmorTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor/{=KTMCM_CPrice}Price")]
        public float ChestArmorTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor/{=KTMCM_CPrice}Price")]
        public float ChestArmorTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor/{=KTMCM_CPrice}Price")]
        public float ChestArmorTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor/{=KTMCM_CPrice}Price")]
        public float ChestArmorTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CChestArmor}ChestArmor/{=KTMCM_CPrice}Price")]
        public float ChestArmorTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion


        #endregion


        [SettingPropertyBool("{=KTMCM_CBM}Crossbow Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow")]
        public bool MCMCrossbowModifiers { get; set; } = true;
        #region Crossbow
        [SettingPropertyBool("{=KTMCM_CBMW}Crossbow Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow/{=KTMCM_Cweight}weight")]
        public bool CrossbowWeightModifiers { get; set; } = true;

        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow/{=KTMCM_Cweight}weight")]
        public float CrossbowTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow/{=KTMCM_Cweight}weight")]
        public float CrossbowTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow/{=KTMCM_Cweight}weight")]
        public float CrossbowTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow/{=KTMCM_Cweight}weight")]
        public float CrossbowTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow/{=KTMCM_Cweight}weight")]
        public float CrossbowTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow/{=KTMCM_Cweight}weight")]
        public float CrossbowTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion


        [SettingPropertyBool("{=KTMCM_CBMP}Crossbow Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow/{=KTMCM_CPrice}Price")]
        public bool CrossbowValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow/{=KTMCM_CPrice}Price")]
        public float CrossbowTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow/{=KTMCM_CPrice}Price")]
        public float CrossbowTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow/{=KTMCM_CPrice}Price")]
        public float CrossbowTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow/{=KTMCM_CPrice}Price")]
        public float CrossbowTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow/{=KTMCM_CPrice}Price")]
        public float CrossbowTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CCrossbow}Crossbow/{=KTMCM_CPrice}Price")]
        public float CrossbowTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion


        #endregion


        [SettingPropertyBool("{=KTMCM_HAM}HandArmor Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor")]
        public bool MCMHandArmorModifiers { get; set; } = true;
        #region HandArmor
        [SettingPropertyBool("{=KTMCM_HAMW}HandArmor Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor/{=KTMCM_Cweight}weight")]
        public bool HandArmorWeightModifiers { get; set; } = true;


        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor/{=KTMCM_Cweight}weight")]
        public float HandArmorTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor/{=KTMCM_Cweight}weight")]
        public float HandArmorTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor/{=KTMCM_Cweight}weight")]
        public float HandArmorTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor/{=KTMCM_Cweight}weight")]
        public float HandArmorTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor/{=KTMCM_Cweight}weight")]
        public float HandArmorTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor/{=KTMCM_Cweight}weight")]
        public float HandArmorTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion



        [SettingPropertyBool("{=KTMCM_HAMP}HandArmor Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor/{=KTMCM_CPrice}Price")]
        public bool HandArmorValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor/{=KTMCM_CPrice}Price")]
        public float HandArmorTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
             HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor/{=KTMCM_CPrice}Price")]
        public float HandArmorTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor/{=KTMCM_CPrice}Price")]
        public float HandArmorTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor/{=KTMCM_CPrice}Price")]
        public float HandArmorTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor/{=KTMCM_CPrice}Price")]
        public float HandArmorTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHandArmor}HandArmor/{=KTMCM_CPrice}Price")]
        public float HandArmorTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion


        #endregion


        [SettingPropertyBool("{=KTMCM_HDABM}HeadArmor Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor")]
        public bool MCMHeadArmorModifiers { get; set; } = true;
        #region HeadArmor
        [SettingPropertyBool("{=KTMCM_HDAMW}HeadArmor Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor/{=KTMCM_Cweight}weight")]
        public bool HeadArmorWeightModifiers { get; set; } = true;

        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor/{=KTMCM_Cweight}weight")]
        public float HeadArmorTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor/{=KTMCM_Cweight}weight")]
        public float HeadArmorTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor/{=KTMCM_Cweight}weight")]
        public float HeadArmorTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor/{=KTMCM_Cweight}weight")]
        public float HeadArmorTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor/{=KTMCM_Cweight}weight")]
        public float HeadArmorTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor/{=KTMCM_Cweight}weight")]
        public float HeadArmorTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion


        [SettingPropertyBool("{=KTMCM_HDAMP}HeadArmor Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor/{=KTMCM_CPrice}Price")]
        public bool HeadArmorValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor/{=KTMCM_CPrice}Price")]
        public float HeadArmorTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor/{=KTMCM_CPrice}Price")]
        public float HeadArmorTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor/{=KTMCM_CPrice}Price")]
        public float HeadArmorTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor/{=KTMCM_CPrice}Price")]
        public float HeadArmorTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor/{=KTMCM_CPrice}Price")]
        public float HeadArmorTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHeadArmor}HeadArmor/{=KTMCM_CPrice}Price")]
        public float HeadArmorTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion



        #endregion


        [SettingPropertyBool("{=KTMCM_HM}Horse Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse")]
        public bool MCMHorseModifiers { get; set; } = true;
        #region Horse
        [SettingPropertyBool("{=KTMCM_HMW}Horse Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse/{=KTMCM_Cweight}weight")]
        public bool HorseWeightModifiers { get; set; } = true;

        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse/{=KTMCM_Cweight}weight")]
        public float HorseTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse/{=KTMCM_Cweight}weight")]
        public float HorseTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse/{=KTMCM_Cweight}weight")]
        public float HorseTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse/{=KTMCM_Cweight}weight")]
        public float HorseTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse/{=KTMCM_Cweight}weight")]
        public float HorseTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse/{=KTMCM_Cweight}weight")]
        public float HorseTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion



        [SettingPropertyBool("{=KTMCM_HMP}Horse Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse/{=KTMCM_CPrice}Price")]
        public bool HorseValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse/{=KTMCM_CPrice}Price")]
        public float HorseTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse/{=KTMCM_CPrice}Price")]
        public float HorseTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse/{=KTMCM_CPrice}Price")]
        public float HorseTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse/{=KTMCM_CPrice}Price")]
        public float HorseTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse/{=KTMCM_CPrice}Price")]
        public float HorseTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorse}Horse/{=KTMCM_CPrice}Price")]
        public float HorseTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion


        #endregion



        [SettingPropertyBool("{=KTMCM_HHM}HorseHarness Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness")]
        public bool MCMHorseHarnessModifiers { get; set; } = true;
        #region HorseHarness
        [SettingPropertyBool("{=KTMCM_HHMW}HorseHarness Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness/{=KTMCM_Cweight}weight")]
        public bool HorseHarnessWeightModifiers { get; set; } = true;

        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness/{=KTMCM_Cweight}weight")]
        public float HorseHarnessTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness/{=KTMCM_Cweight}weight")]
        public float HorseHarnessTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness/{=KTMCM_Cweight}weight")]
        public float HorseHarnessTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness/{=KTMCM_Cweight}weight")]
        public float HorseHarnessTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness/{=KTMCM_Cweight}weight")]
        public float HorseHarnessTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness/{=KTMCM_Cweight}weight")]
        public float HorseHarnessTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion


        [SettingPropertyBool("{=KTMCM_HHMP}HorseHarness Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness/{=KTMCM_CPrice}Price")]
        public bool HorseHarnessValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness/{=KTMCM_CPrice}Price")]
        public float HorseHarnessTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness/{=KTMCM_CPrice}Price")]
        public float HorseHarnessTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness/{=KTMCM_CPrice}Price")]
        public float HorseHarnessTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness/{=KTMCM_CPrice}Price")]
        public float HorseHarnessTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness/{=KTMCM_CPrice}Price")]
        public float HorseHarnessTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CHorseHarness}HorseHarness/{=KTMCM_CPrice}Price")]
        public float HorseHarnessTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion


        #endregion


        [SettingPropertyBool("{=KTMCM_LAM}LegArmor Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor")]
        public bool MCMLegArmorModifiers { get; set; } = true;
        #region LegArmor
        [SettingPropertyBool("{=KTMCM_LAMW}LegArmor Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor/{=KTMCM_Cweight}weight")]
        public bool LegArmorWeightModifiers { get; set; } = true;

        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor/{=KTMCM_Cweight}weight")]
        public float LegArmorTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor/{=KTMCM_Cweight}weight")]
        public float LegArmorTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor/{=KTMCM_Cweight}weight")]
        public float LegArmorTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor/{=KTMCM_Cweight}weight")]
        public float LegArmorTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor/{=KTMCM_Cweight}weight")]
        public float LegArmorTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor/{=KTMCM_Cweight}weight")]
        public float LegArmorTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion


        [SettingPropertyBool("{=KTMCM_LAMP}LegArmor Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor/{=KTMCM_CPrice}Price")]
        public bool LegArmorValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor/{=KTMCM_CPrice}Price")]
        public float LegArmorTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor/{=KTMCM_CPrice}Price")]
        public float LegArmorTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor/{=KTMCM_CPrice}Price")]
        public float LegArmorTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor/{=KTMCM_CPrice}Price")]
        public float LegArmorTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor/{=KTMCM_CPrice}Price")]
        public float LegArmorTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CLegArmor}LegArmor/{=KTMCM_CPrice}Price")]
        public float LegArmorTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion


        #endregion


        [SettingPropertyBool("{=KTMCM_MM}Musket Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket")]
        public bool MCMMusketModifiers { get; set; } = true;
        #region Musket
        [SettingPropertyBool("{=KTMCM_MMW}Musket Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket/{=KTMCM_Cweight}weight")]
        public bool MusketWeightModifiers { get; set; } = true;

        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket/{=KTMCM_Cweight}weight")]
        public float MusketTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket/{=KTMCM_Cweight}weight")]
        public float MusketTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket/{=KTMCM_Cweight}weight")]
        public float MusketTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket/{=KTMCM_Cweight}weight")]
        public float MusketTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket/{=KTMCM_Cweight}weight")]
        public float MusketTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket/{=KTMCM_Cweight}weight")]
        public float MusketTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion


        [SettingPropertyBool("{=KTMCM_MMP}Musket Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket/{=KTMCM_CPrice}Price")]
        public bool MusketValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket/{=KTMCM_CPrice}Price")]
        public float MusketTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket/{=KTMCM_CPrice}Price")]
        public float MusketTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket/{=KTMCM_CPrice}Price")]
        public float MusketTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket/{=KTMCM_CPrice}Price")]
        public float MusketTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket/{=KTMCM_CPrice}Price")]
        public float MusketTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CMusket}Musket/{=KTMCM_CPrice}Price")]
        public float MusketTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion




        #endregion


        [SettingPropertyBool("{=KTMCM_OHBM}OneHandedWeapon Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon")]
        public bool MCMOneHandedWeaponModifiers { get; set; } = true;
        #region OneHandedWeapon
        [SettingPropertyBool("{=KTMCM_OHMW}OneHandedWeapon Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon/{=KTMCM_Cweight}weight")]
        public bool OneHandedWeaponWeightModifiers { get; set; } = true;

        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon/{=KTMCM_Cweight}weight")]
        public float OneHandedWeaponTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon/{=KTMCM_Cweight}weight")]
        public float OneHandedWeaponTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon/{=KTMCM_Cweight}weight")]
        public float OneHandedWeaponTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon/{=KTMCM_Cweight}weight")]
        public float OneHandedWeaponTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon/{=KTMCM_Cweight}weight")]
        public float OneHandedWeaponTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon/{=KTMCM_Cweight}weight")]
        public float OneHandedWeaponTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion


        [SettingPropertyBool("{=KTMCM_OHMP}OneHandedWeapon Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon/{=KTMCM_CPrice}Price")]
        public bool OneHandedWeaponValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon/{=KTMCM_CPrice}Price")]
        public float OneHandedWeaponTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon/{=KTMCM_CPrice}Price")]
        public float OneHandedWeaponTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon/{=KTMCM_CPrice}Price")]
        public float OneHandedWeaponTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon/{=KTMCM_CPrice}Price")]
        public float OneHandedWeaponTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon/{=KTMCM_CPrice}Price")]
        public float OneHandedWeaponTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_COneHandedWeapon}OneHandedWeapon/{=KTMCM_CPrice}Price")]
        public float OneHandedWeaponTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion


        #endregion


        [SettingPropertyBool("{=KTMCM_PM}Pistol Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol")]
        public bool MCMPistolModifiers { get; set; } = true;
        #region Pistol
        [SettingPropertyBool("{=KTMCM_PMW}Pistol Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol/{=KTMCM_Cweight}weight")]
        public bool PistolWeightModifiers { get; set; } = true;

        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol/{=KTMCM_Cweight}weight")]
        public float PistolTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol/{=KTMCM_Cweight}weight")]
        public float PistolTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol/{=KTMCM_Cweight}weight")]
        public float PistolTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol/{=KTMCM_Cweight}weight")]
        public float PistolTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol/{=KTMCM_Cweight}weight")]
        public float PistolTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol/{=KTMCM_Cweight}weight")]
        public float PistolTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion


        [SettingPropertyBool("{=KTMCM_PMP}Pistol Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol/{=KTMCM_CPrice}Price")]
        public bool PistolValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol/{=KTMCM_CPrice}Price")]
        public float PistolTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol/{=KTMCM_CPrice}Price")]
        public float PistolTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol/{=KTMCM_CPrice}Price")]
        public float PistolTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol/{=KTMCM_CPrice}Price")]
        public float PistolTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol/{=KTMCM_CPrice}Price")]
        public float PistolTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPistol}Pistol/{=KTMCM_CPrice}Price")]
        public float PistolTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion



        #endregion


        [SettingPropertyBool("{=KTMCM_PAM}Polearm Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm")]
        public bool MCMPolearmModifiers { get; set; } = true;
        #region Polearm
        [SettingPropertyBool("{=KTMCM_PAMW}Polearm Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm/{=KTMCM_Cweight}weight")]
        public bool PolearmWeightModifiers { get; set; } = true;

        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm/{=KTMCM_Cweight}weight")]
        public float PolearmTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm/{=KTMCM_Cweight}weight")]
        public float PolearmTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm/{=KTMCM_Cweight}weight")]
        public float PolearmTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm/{=KTMCM_Cweight}weight")]
        public float PolearmTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm/{=KTMCM_Cweight}weight")]
        public float PolearmTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm/{=KTMCM_Cweight}weight")]
        public float PolearmTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion


        [SettingPropertyBool("{=KTMCM_PAMP}Polearm Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm/{=KTMCM_CPrice}Price")]
        public bool PolearmValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm/{=KTMCM_CPrice}Price")]
        public float PolearmTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm/{=KTMCM_CPrice}Price")]
        public float PolearmTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm/{=KTMCM_CPrice}Price")]
        public float PolearmTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm/{=KTMCM_CPrice}Price")]
        public float PolearmTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm/{=KTMCM_CPrice}Price")]
        public float PolearmTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CPolearm}Polearm/{=KTMCM_CPrice}Price")]
        public float PolearmTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion



        #endregion


        [SettingPropertyBool("{=KTMCM_SHM}Shield Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield")]
        public bool MCMShieldModifiers { get; set; } = true;
        #region Shield
        [SettingPropertyBool("{=KTMCM_SHMW}Shield Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield/{=KTMCM_Cweight}weight")]
        public bool ShieldWeightModifiers { get; set; } = true;

        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield/{=KTMCM_Cweight}weight")]
        public float ShieldTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield/{=KTMCM_Cweight}weight")]
        public float ShieldTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield/{=KTMCM_Cweight}weight")]
        public float ShieldTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield/{=KTMCM_Cweight}weight")]
        public float ShieldTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield/{=KTMCM_Cweight}weight")]
        public float ShieldTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield/{=KTMCM_Cweight}weight")]
        public float ShieldTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion


        [SettingPropertyBool("{=KTMCM_SHMP}Shield Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield/{=KTMCM_CPrice}Price")]
        public bool ShieldValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield/{=KTMCM_CPrice}Price")]
        public float ShieldTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield/{=KTMCM_CPrice}Price")]
        public float ShieldTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield/{=KTMCM_CPrice}Price")]
        public float ShieldTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield/{=KTMCM_CPrice}Price")]
        public float ShieldTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield/{=KTMCM_CPrice}Price")]
        public float ShieldTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CShield}Shield/{=KTMCM_CPrice}Price")]
        public float ShieldTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion



        #endregion


        [SettingPropertyBool("{=KTMCM_THM}TwoHandedWeapon Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMH}Enables modifying the weight and price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon")]
        public bool MCMTwoHandedWeaponModifiers { get; set; } = true;
        #region TwoHandedWeapon
        [SettingPropertyBool("{=KTMCM_THMW}TwoHandedWeapon Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMWH}Enables modifying items weight.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon/{=KTMCM_Cweight}weight")]
        public bool TwoHandedWeaponWeightModifiers { get; set; } = true;

        #region Weight
        [SettingPropertyFloatingInteger("{=KTMCM_WeightT1}Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT1H}Multiply Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon/{=KTMCM_Cweight}weight")]
        public float TwoHandedWeaponTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT2}Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT2H}Multiply Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon/{=KTMCM_Cweight}weight")]
        public float TwoHandedWeaponTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT3}Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT3H}Multiply Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon/{=KTMCM_Cweight}weight")]
        public float TwoHandedWeaponTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT4}Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT4H}Multiply Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon/{=KTMCM_Cweight}weight")]
        public float TwoHandedWeaponTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT5}Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT5H}Multiply Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon/{=KTMCM_Cweight}weight")]
        public float TwoHandedWeaponTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_WeightT6}Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMWT6H}Multiply Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon/{=KTMCM_Cweight}weight")]
        public float TwoHandedWeaponTier6WeightMultiplier { get; set; } = 1.0f;
        #endregion


        [SettingPropertyBool("{=KTMCM_THMP}TwoHandedWeapon Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ITMPH}Enables modifying items price.")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon/{=KTMCM_CPrice}Price")]
        public bool TwoHandedWeaponValueModifiers { get; set; } = true;

        #region Price
        [SettingPropertyFloatingInteger("{=KTMCM_PriceT1}Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT1H}Multiply Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon/{=KTMCM_CPrice}Price")]
        public float TwoHandedWeaponTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT2}Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
             HintText = "{=KTMCM_ITMPT2H}Multiply Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon/{=KTMCM_CPrice}Price")]
        public float TwoHandedWeaponTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT3}Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT3H}Multiply Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon/{=KTMCM_CPrice}Price")]
        public float TwoHandedWeaponTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT4}Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT4H}Multiply Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon/{=KTMCM_CPrice}Price")]
        public float TwoHandedWeaponTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT5}Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT5H}Multiply Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon/{=KTMCM_CPrice}Price")]
        public float TwoHandedWeaponTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_PriceT6}Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_ITMPT6H}Multiply Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CItems}Items/{=KTMCM_CTwoHandedWeapon}TwoHandedWeapon/{=KTMCM_CPrice}Price")]
        public float TwoHandedWeaponTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion


        #endregion


        //End Items
        #endregion


        [SettingPropertyBool("{=KTMCM_BRM}Battle Reward Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_BRMH}Enables modifying battle rewards.")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards")]
        public bool MCMBattleRewardModifiers { get; set; } = true;
        #region Battle Rewards
        #region RelationshipGain
        [SettingPropertyBool("{=KTMCM_BRMRG}Relationship Gain", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_BRMRGH}Enables modifying Relationship gain.")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards/{=KTMCM_CRelationShipGain}RelationShip Gain")]
        public bool BattleRewardsRelationShipGainModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KTMCM_BRMRGM}Relationship gain Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_BRMRGMH}Multiply Relationship gain  by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards/{=KTMCM_CRelationShipGain}RelationShip Gain")]
        public float BattleRewardsRelationShipGainMultiplier { get; set; } = 1.0f;
        #endregion

        #region  RenownGain
        [SettingPropertyBool("{=KTMCM_BRMRNG}Renown Gain", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_BRMRNGH}Enables modifying Renown gain.")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards/{=KTMCM_CRenownGain}Renown Gain")]
        public bool BattleRewardsRenownGainModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KTMCM_BRMRNGM}Renown gain Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_BRMRNGMH}Multiply Renown gain  by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards/{=KTMCM_CRenownGain}Renown Gain")]
        public float BattleRewardsRenownGainMultiplier { get; set; } = 1.0f;
        #endregion

        #region  InfluenceGain
        [SettingPropertyBool("{=KTMCM_BRMIG}Influence Gain", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_BRMIGH}Enables modifying Influence gain.")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards/{=KTMCM_CInfluenceGain}Influence Gain")]
        public bool BattleRewardsInfluenceGainModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KTMCM_BRMIGM}Influence gain Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_BRMIGMH}Multiply Influence gain  by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards/{=KTMCM_CInfluenceGain}Influence Gain")]
        public float BattleRewardsInfluenceGainMultiplier { get; set; } = 1.0f;
        #endregion  

        #region  MoraleGain
        [SettingPropertyBool("{=KTMCM_BRMMG}Morale Gain", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_BRMMGH}Enables modifying Morale gain.")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards/{=KTMCM_CMoraleGain}Morale Gain")]
        public bool BattleRewardsMoraleGainModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KTMCM_BRMMGM}Morale gain Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_BRMMGMH}Multiply Morale gain  by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards/{=KTMCM_CMoraleGain}Morale Gain")]
        public float BattleRewardsMoraleGainMultiplier { get; set; } = 1.0f;
        #endregion  

        #region  GoldLossAfterDefeat
        [SettingPropertyBool("{=KTMCM_BRMGL}Gold Loss", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_BRMGLH}Enables modifying Gold Loss on defeat .")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards/{=KTMCM_CGoldLoss}Gold Loss")]
        public bool BattleRewardsGoldLossModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("{=KTMCM_BRMGLM}Gold Loss Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_BRMGLMH}Multiply Gold Loss on defeat by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CBattleReward}Battle Rewards/{=KTMCM_CGoldLoss}Gold Loss")]
        public float BattleRewardsGoldLossMultiplier { get; set; } = 1.0f;
        #endregion  
        #endregion  



        [SettingPropertyBool("{=KTMCM_CLM}Clan Modifiers *", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_CLMH}Enables modifying clan variables.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks")]
        public bool MCMClanModifiers { get; set; } = false;

        #region Clan

        [SettingPropertyBool("{=KTMCM_CLMBPL}Party Limits", IsToggle = true, RequireRestart = true,//, Order = 3
            HintText = "{=KTMCM_CLMBPLH}Enables additional Party limit per clan tier .")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=BT_Settings_003200}Party Limits")]
        public bool ClanAdditionalPartyLimitEnabled { get; set; } = false;

        #region PartyLimit
        [SettingPropertyBool("{=BT_Settings_003201}Player Parties Limit", IsToggle = true, RequireRestart = false,
            HintText = "{=BT_Settings_003201_Desc}Changes the base number of parties you can field and the bonus gained per clan tier.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=BT_Settings_003200}Party Limits/{=KTMCM_CPlayer}Player", GroupOrder = 1)]
        public bool ClanPlayerPartiesLimitEnabled { get; set; } = false;
        #region Clan Tweaks - Limits Player
        [SettingPropertyInteger("{=BT_Settings_003202}Base Parties Limit", 0, 10, "0 Parties", Order = 0, RequireRestart = false,
            HintText = "{=BT_Settings_003202_Desc}This is the number of parties you can field at the start.[Native: 1].")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=BT_Settings_003200}Party Limits/{=KTMCM_CPlayer}Player")]
        public int ClanPlayerBasePartiesLimit{ get; set; } = 1;

        [SettingPropertyFloatingInteger("{=BT_Settings_003203}Parties Bonus Per Clan Tier", 0.0f, 6.0f, "0.0 Parties/Clan Tier", RequireRestart = false,
            HintText = "{=BT_Settings_003203_Desc}Native has a calculation for this: 1 party for under tier 3, 2 parties for under tier 5, 3 parties for over tier 5. This setting is multiplied by your clan tier. A value of 0.5 will equate to 1 extra party per 2 clan tiers, which eqautes to the same as native.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=BT_Settings_003200}Party Limits/{=KTMCM_CPlayer}Player")]
        public float ClanPlayerPartiesBonusPerClanTier { get; set; } = 0.5f;
        #endregion

        [SettingPropertyBool("{=BT_Settings_003204}AI Lord Parties Limit", IsToggle = true, RequireRestart = false,
            HintText = "{=BT_Settings_003204_Desc}Changes the base number of parties AI Lords can field.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=BT_Settings_003200}Party Limits/{=BT_Settings_003204}AI Lord Parties Limit", GroupOrder=2)]
        public bool ClanAIPartiesLimitTweakEnabled { get; set; } = false;
        #region Clan Tweaks - AI Lord Parties Limmit
        [SettingPropertyInteger("{=BT_Settings_003205}Add to AI Lord Parties Limit", 0, 10, "0 Parties", Order = 0, RequireRestart = false,
            HintText = "{=BT_Settings_003205_Desc}This adds to the the base number of parties AI Lords can field. [Native is 1 for Tier 3 and below, 2 at T4, 3 at T5 and up.] Minor Factions are not included unless the option below is also enabled.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=BT_Settings_003200}Party Limits/{=BT_Settings_003204}AI Lord Parties Limit")]
        public int ClanAIBaseClanPartiesLimit { get; set; } = 0;

        [SettingPropertyFloatingInteger("{=BT_Settings_003203}Parties Bonus Per Clan Tier", 0.0f, 6.0f, "0.0 Parties/Clan Tier", RequireRestart = false,
            HintText = "{=BT_Settings_003203_Desc}Native has a calculation for this: 1 party for under tier 3, 2 parties for under tier 5, 3 parties for over tier 5. This setting is multiplied by your clan tier. A value of 0.5 will equate to 1 extra party per 2 clan tiers, which eqautes to the same as native.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=BT_Settings_003200}Party Limits/{=BT_Settings_003204}AI Lord Parties Limit")]
        public float ClanAIPartiesBonusPerClanTier { get; set; } = 0.5f;

        [SettingPropertyBool("{=BT_Settings_003206}Also Adjust Minor Factions", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_003206_Desc}Changes the base number of parties AI Minor Faction Lords can field. [Native is 1-4, depending on Clan tier.]"),
            SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=BT_Settings_003200}Party Limits/{=BT_Settings_003204}AI Lord Parties Limit")]
        public bool ClanAIMinorClanPartiesLimitTweakEnabled { get; set; } = false;

        #endregion

        [SettingPropertyBool("{=BT_Settings_003207}Custom Spawn Parties Limit", IsToggle = true, RequireRestart = false,
            HintText = "{=BT_Settings_003207_Desc}Changes the base number of parties Custom Spawn lords can field.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=BT_Settings_003200}Party Limits/{=BT_Settings_003207}Custom Spawns Parties Limit", GroupOrder = 3)]
        public bool AICustomSpawnPartiesLimitTweakEnabled { get; set; } = false;
        #region Clan Tweaks - Custom Spawns Parties Tweak

        [SettingPropertyInteger("{=BT_Settings_003208}Add to Custom Spawn Parties Limit", 0, 10, "0 Parties", Order = 0, RequireRestart = false,
            HintText = "{=BT_Settings_003208_Desc}This adds to the the base number of parties Custom Lords can field. [Native is 1 for Tier 3 and below, 2 at T4, 3 at T5 and up.].")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=BT_Settings_003200}Party Limits/{=BT_Settings_003207}Custom Spawns Parties Limit")]
        public int BaseAICustomSpawnPartiesLimit { get; set; } = 0;

        [SettingPropertyFloatingInteger("{=BT_Settings_003203}Parties Bonus Per Clan Tier", 0.0f, 6.0f, "0.0 Parties/Clan Tier", RequireRestart = false,
            HintText = "{=BT_Settings_003203_Desc}Native has a calculation for this: 1 party for under tier 3, 2 parties for under tier 5, 3 parties for over tier 5. This setting is multiplied by your clan tier. A value of 0.5 will equate to 1 extra party per 2 clan tiers, which eqautes to the same as native.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=BT_Settings_003200}Party Limits/{=BT_Settings_003207}Custom Spawns Parties Limit")]
        public float ClanCSPartiesBonusPerClanTier { get; set; } = 0.5f;

        #endregion

        #endregion

        #region CompanionLimit
        [SettingPropertyBool("{=KTMCM_CLMCL}Bonus Companion Limit", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_CLMCLH}Enables additional Companion limit per clan tier .")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=KTMCM_CCompanionLimit}Companion Limit")]
        public bool ClanCompanionLimitEnabled { get; set; } = false;

        [SettingPropertyInteger("{=KTMCM_CLMCLBC}Bonus Companions", 0, 10, "0 Companions", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_CLMCLBCH}Additional Companion limit per clan tier [Native: 0].")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=KTMCM_CCompanionLimit}Companion Limit")]
        public int ClanAdditionalCompanionLimit { get; set; } = 0;

        [SettingPropertyInteger("{=BT_Settings_003101}Base Companion Limit", 1, 20, "0 Companions", Order = 0, RequireRestart = false,
            HintText = "{=BT_Settings_003101_Desc}Sets the base companion limit. [Native: 3].")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=KTMCM_CCompanionLimit}Companion Limit")]

        public int ClanCompanionBaseLimit { get; set; } = 3;

        [SettingPropertyBool("{=BT_Settings_003103}Enable Unlimited Wanderers Patch" + "*", Order = 0, RequireRestart = true,
            HintText = "{=BT_Settings_003103_Desc}Removes the soft cap on the maximum number of potential companions who can spawn. Native limits the # of wanderers to ~25. This will remove that limit. Note: Requires a new campaign to take effect, as the cap is set when a new game is generated. Credit to Bleinz for his UnlimitedWanderers mod.")]
        [SettingPropertyGroup("{=BT_Settings_003000}Clan Tweaks/{=KTMCM_CCompanionLimit}Companion Limit")]
        public bool UnlimitedWanderersPatch { get; set; } = false;
        #endregion
        #endregion


        [SettingPropertyBool("{=KTMCM_CLMWS}WorkShop Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_CLMWSH}Enables modifying WorkShop variables.")]
        [SettingPropertyGroup("{=KTMCM_CWorkShop}WorkShop")]
        public bool MCMWorkShopModifiers { get; set; } = false;
        #region Workshops
        #region Bankruptcy

        [SettingPropertyBool("{=KTMCM_CLMWSBRTM}Bankruptcy Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=}Enables Bankruptcy Modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CWorkShop}WorkShop/{=KTMCM_CBankruptcy}Bankruptcy")]
        public bool WorkShopBankruptcyModifiers { get; set; } = false;

        [SettingPropertyInteger("{=KTMCM_CLMWSBRDSM}Days to save", 1, 10, "0 Days", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_CLMWSBRDSMH}Days For Player to Save Workshop From Bankruptcy [Native : 3].")]
        [SettingPropertyGroup("{=KTMCM_CWorkShop}WorkShop/{=KTMCM_CBankruptcy}Bankruptcy")]
        public int WorkShopBankruptcyValue { get; set; } = 3;
        #endregion 

        #region Limit

        [SettingPropertyBool("{=KTMCM_CLMWSL}Enable Limit Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_CLMWSLH}Enable Limit Modifier for workshop limit for player.")]
        [SettingPropertyGroup("{=KTMCM_CWorkShop}WorkShop/{=KTMCM_CMaxLimit}Max Limit")]
        public bool WorkShopMaxWorkshopCountForPlayerModifiers { get; set; } = false;

        [SettingPropertyInteger("{=KTMCM_CLMWSLN}Max number of Limit", 1, 10, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_CLMWSLNH}Maximum number of workshops for player native is the multiplier number times clan tier [Native : 1].")]
        [SettingPropertyGroup("{=KTMCM_CWorkShop}WorkShop/{=KTMCM_CMaxLimit}Max Limit")]
        public int WorkShopMaxWorkshopCountForPlayerValue { get; set; } = 1;

        #endregion 
        #endregion



        [SettingPropertyBool("{=KTMCM_PCM}Character Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PCMH}Enables modifying Character variables.")]
        [SettingPropertyGroup("{=KTMCM_CCharacter}Character")]
        public bool MCMCharacterDevlopmentModifiers { get; set; } = false;
        #region CharacterDevelopment
        #region LevelsPerAttributePoint
        [SettingPropertyBool("{=KTMCM_PCMLPA}Levels Per Attribute modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PCMLPAH}Enable Number of Levels Per Attribute modifier")]
        [SettingPropertyGroup("{=KTMCM_CCharacter}Character/{=KTMCM_CAttributes}Attributes/{=KTMCM_CLevel}Level")]
        public bool CharacterLevelsPerAttributeModifiers { get; set; } = false;

        [SettingPropertyInteger("{=KTMCM_PCMLPAN}Levels for Attribute", 1, 20, "0 Levels", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PCMLPANH}Number of Levels to acquire an attribute point [Native : 4].")]
        [SettingPropertyGroup("{=KTMCM_CCharacter}Character/{=KTMCM_CAttributes}Attributes/{=KTMCM_CLevel}Level")]
        public int CharacterLevelsPerAttributeValue { get; set; } = 4;
        #endregion 

        #region FocusPointsPerLevel
        [SettingPropertyBool("{=KTMCM_PCMFP}Focus points modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PCMFPH}Enables Focus points per level modifier.")]
        [SettingPropertyGroup("{=KTMCM_CCharacter}Character/{=KTMCM_CFocus}Focus/{=KTMCM_CLevel}Level")]
        public bool CharacterFocusPerLevelModifiers { get; set; } = false;

        [SettingPropertyInteger("{=KTMCM_PCMFPPL}Focus points per level", 1, 20, "0 Focus", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PCMFPPLH}Number of Focus points per level [Native : 1].")]
        [SettingPropertyGroup("{=KTMCM_CCharacter}Character/{=KTMCM_CFocus}Focus/{=KTMCM_CLevel}Level")]
        public int CharacterFocusPerLevelValue { get; set; } = 1;
        #endregion 
        #endregion CharacterDevelopment




        [SettingPropertyBool("{=KTMCM_PM}Pregnancy Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PMH}Enables modifying Pregnancy variables.")]
        [SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy")]
        public bool MCMPregnancyModifiers { get; set; } = false;
        #region Pregnancy

        [SettingPropertyBool("{=KTMCM_PMDD}Pregnancy Duration Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PMDDH}Enables Pregnancy Duration modifier.")]
        [SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/{=KTMCM_CDuration}Duration")]
        public bool PregnancyDurationModifiers { get; set; } = false;

        #region Duration
        [SettingPropertyInteger("{=KTMCM_PMDDN}Pregnancy Duration", 1, 300, "0 Days", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PMDDNH}Pregnancy Duration in days [Native : 36].")]
        [SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/{=KTMCM_CDuration}Duration")]
        public int PregnancyDurationValue { get; set; } = 36;
        #endregion

        [SettingPropertyBool("{=BT_Settings_002402}Disable Maternal Mortality", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_002402_Desc}Disables the chance of mothers dying when giving birth."),
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002400}Pregnancy Tweaks" + "*")]
        public bool NoMaternalMortalityTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_PMMPL}Labor Mortality Chance Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PMMPLH}Enables Mortality In Labor Chance modifier.")]
        [SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/{=KTMCM_CLaborMortality}Labor Mortality")]
        public bool PregnancyLaborMortalityChanceModifiers { get; set; } = false;

        #region MortalityProbabilityInLabor
        [SettingPropertyFloatingInteger("{=KTMCM_PMMPLC}Labor Mortality Chance", 0.000f, 1.000f, RequireRestart = false, 
            HintText = "{=KTMCM_PMMPLCH}Mortality In Labor Chance [Native : 0.015].")]
        [SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/{=KTMCM_CLaborMortality}Labor Mortality")]
        public float PregnancyLaborMortalityChanceValue { get; set; } = 0.015f;
        #endregion

        [SettingPropertyBool("{=BT_Settings_002401}Disable Stillbirths", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_002401_Desc}Disables the chance of children dying when born."),
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002400}Pregnancy Tweaks" + "*")]
        public bool NoStillbirthsTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_PMSB}Stillbirth Chance Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PMSBH}Enables Stillbirth Chance modifier.")]
        [SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/{=KTMCM_CStillbirth}Stillbirth")]
        public bool PregnancyStillbirthChanceModifiers { get; set; } = false;

        #region StillbirthProbability
        [SettingPropertyFloatingInteger("{=KTMCM_PMSBC}Stillbirth Chance", 0.01f, 1.00f, RequireRestart = false,
            HintText = "{=KTMCM_PMSBCH}Stillbirth Chance  [Native : 0.01].")]
        [SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/{=KTMCM_CStillbirth}Stillbirth")]
        public float PregnancyStillbirthChanceValue { get; set; } = 0.01f;
        #endregion

        [SettingPropertyBool("{=KTMCM_PMFO}Female Child Chance Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PMFOH}Enables Female Child Chance modifier.")]
        [SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/{=KTMCM_CFemaleChild}Female Child")]
        public bool PregnancyFemaleOffspringChanceModifiers { get; set; } = false;

        #region DeliveringFemaleOffspringProbability
        [SettingPropertyFloatingInteger("{=KTMCM_PMFOC}Female Child Chance", 0.00f, 1.00f, RequireRestart = false,
            HintText = "{=KTMCM_PMFOCH}Female Child Chance  [Native : 0.51].")]
        [SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/{=KTMCM_CFemaleChild}Female Child")]
        public float PregnancyFemaleOffspringChanceValue { get; set; } = 0.51f;
        #endregion

        [SettingPropertyBool("{=KTMCM_PMTO}Twins Chance Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_PMTOH}Enables Twins Chance modifier.")]
        [SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/{=KTMCM_CTwins}Twins")]
        public bool PregnancyTwinsChanceModifiers { get; set; } = false;

        #region DeliveringTwinsProbability
        [SettingPropertyFloatingInteger("{=KTMCM_PMTOC}Twins Chance", 0.00f, 1.00f, RequireRestart = false, //"#0%"
            HintText = "{=KTMCM_PMTOCH}Twins Chance  [Native : 0.03].")]
        [SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/{=KTMCM_CTwins}Twins")]
        public float PregnancyTwinsChanceValue { get; set; } = 0.03f;
        #endregion 



        [SettingPropertyBool("{=BT_Settings_002409}Pregnancy Chance Tweaks", Order = 7, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_002409_Desc}Enabling this will completely override the daily pregnancy check. All settings below will be applied!"),
            SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/" + "{=BT_Settings_002409}Pregnancy Chance Tweaks", GroupOrder = 4)]
        public bool DailyChancePregnancyTweakEnabled { get; set; } = false;



        [SettingPropertyBool("{=BT_Settings_002410}Player is Infertile", Order = 1, RequireRestart = false, HintText = "{=BT_Settings_002410_Desc}Native: disabled. If enabled, the player will not be able to have children."),
            SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/" + "{=BT_Settings_002409}Pregnancy Chance Tweaks")]
        public bool PlayerCharacterInfertileEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_002411}Min Pregnancy Age", 0, 125, "0 Years", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_002411_Desc}Native: 18. Minimum age that someone can get pregnant."),
            SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/" + "{=BT_Settings_002409}Pregnancy Chance Tweaks")]
        public int MinPregnancyAge { get; set; } = 18;

        [SettingPropertyInteger("{=BT_Settings_002412}Max Pregnancy Age", 0, 125, "0 Years", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_002412_Desc}Native: 45. Maximum age that someone can get pregnant."),
            SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/" + "{=BT_Settings_002409}Pregnancy Chance Tweaks")]
        public int MaxPregnancyAge { get; set; } = 45;

        [SettingPropertyFloatingInteger("{=BT_Settings_002413}Clan Fertility Bonus", 1f, 10f, "0%", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_002413_Desc}Adds modifier to your clan members to become pregnant. 100% = No Bonus, 200% = 2x chance. Note: May not do much after ~6-8 kids due to the base pregnancy calculations."),
            SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/" + "{=BT_Settings_002409}Pregnancy Chance Tweaks")]
        public float ClanFertilityBonus { get; set; } = 1f;

        [SettingPropertyInteger("{=BT_Settings_002414}Max Children", 0, 100, "0 Children", Order = 5, RequireRestart = false,
            HintText = "{=BT_Settings_002414_Desc}Default: 5. Maximum number of children that someone can have."),
            SettingPropertyGroup("{=KTMCM_CPregnancy}Pregnancy/" + "{=BT_Settings_002409}Pregnancy Chance Tweaks")]
        public int MaxChildren { get; set; } = 5;




        #endregion 





        [SettingPropertyBool("{=KTMCM_SM}Smithing Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_SMH}Enables modifying Smithing variables.")]
        [SettingPropertyGroup("{=KTMCM_CCrafting}Crafting")]
        public bool MCMSmithingModifiers { get; set; } = false;
        #region Smithing
        #region RefiningXp
        [SettingPropertyBool("{=KTMCM_SMRXP}Refining Xp", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_SMRXPH}Enable Refining Xp multiplier.")]
        [SettingPropertyGroup("{=KTMCM_CCrafting}Crafting/{=KTMCM_CXP}XP/{=KTMCM_CRefining}Refining")]
        public bool SmithingRefiningXpModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_SMRXPM}Refining Xp", 0.1f, 5.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_SMRXPMH}Multiply Refining Xp by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CCrafting}Crafting/{=KTMCM_CXP}XP/{=KTMCM_CRefining}Refining")]
        public float SmithingRefiningXpValue { get; set; } = 1.0f;
        #endregion  

        #region XpForSmelting
        [SettingPropertyBool("{=KTMCM_SMSXP}Smelting Xp", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_SMSXPH}Enable Smelting Xp multiplier.")]
        [SettingPropertyGroup("{=KTMCM_CCrafting}Crafting/{=KTMCM_CXP}XP/{=KTMCM_CSmelting}Smelting")]
        public bool SmithingSmeltingXpModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_SMSXPM}Smelting Xp", 0.1f, 5.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_SMSXPMH}Multiply Smelting Xp by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CCrafting}Crafting/{=KTMCM_CXP}XP/{=KTMCM_CSmelting}Smelting")]
        public float SmithingSmeltingXpValue { get; set; } = 1.0f;
        #endregion 

        #region XpForSmithing
        [SettingPropertyBool("{=KTMCM_SMCXP}Crafting Xp", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_SMCXPH}Enable Crafting Xp multiplier.")]
        [SettingPropertyGroup("{=KTMCM_CCrafting}Crafting/{=KTMCM_CXP}XP/{=KTMCM_CSmithing}Smithing")]
        public bool SmithingSmithingXpModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_SMCXPM}Smithing Xp", 0.1f, 5.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_SMCXPMH}Multiply Smithing Xp by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CCrafting}Crafting/{=KTMCM_CXP}XP/{=KTMCM_CSmithing}Smithing")]
        public float SmithingSmithingXpValue { get; set; } = 1.0f;
        #endregion 

        #region EnergyDisable
        [SettingPropertyBool("{=KTMCM_SMSE}Energy Disable", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_SMSEH}Disable the energy for crafting and refining tasks [Native : false]")]
        [SettingPropertyGroup("{=KTMCM_CCrafting}Crafting/{=KTMCM_CEnergy}Energy/{=KTMCM_CDisable}Disable")]
        public bool SmithingEnergyDisable { get; set; } = false;

        #endregion 

        #region EnergyCostForRefining
        [SettingPropertyBool("{=KTMCM_SMECR}Energy Refining", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_SMECRH}Enable modify the energy used for refining.")]
        [SettingPropertyGroup("{=KTMCM_CCrafting}Crafting/{=KTMCM_CEnergy}Energy/{=KTMCM_CRefining}Refining")]
        public bool SmithingEnergyRefiningModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_SMECRM}Energy Refining", 0.1f, 1.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_SMECRMH}Multiply the energy used for refining by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Smithing/{=KTMCM_CEnergy}Energy/{=KTMCM_CRefining}Refining")]
        public float SmithingEnergyRefiningValue { get; set; } = 1.0f;
        #endregion 

        #region EnergyCostForSmithing
        [SettingPropertyBool("{=KTMCM_SMECC}Energy Crafting", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_SMECCH}Enable modify the energy used for Crafting.")]
        [SettingPropertyGroup("{=KTMCM_CCrafting}Crafting/{=KTMCM_CEnergy}Energy/{=KTMCM_CCrafting}Crafting")]
        public bool SmithingEnergySmithingModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_SMECCM}Energy Crafting", 0.1f, 1.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_SMECCMH}Multiply the energy used for Crafting by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CCrafting}Crafting/{=KTMCM_CEnergy}Energy/{=KTMCM_CCrafting}Crafting")]
        public float SmithingEnergySmithingValue { get; set; } = 1.0f;
        #endregion 

        #region EnergyCostForSmelting
        [SettingPropertyBool("{=KTMCM_SMECS}Energy Smelting", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_SMECSH}Enable modify the energy used for Smelting.")]
        [SettingPropertyGroup("{=KTMCM_CCrafting}Crafting/{=KTMCM_CEnergy}Energy/{=KTMCM_CSmelting}Smelting")]
        public bool SmithingEnergySmeltingModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("{=}Energy Smithing", 0.1f, 1.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_SMECSMH}Multiply the energy used for Smelting by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CCrafting}Crafting/{=KTMCM_CEnergy}Energy/{=KTMCM_CSmelting}Smelting")]
        public float SmithingEnergySmeltingValue { get; set; } = 1.0f;
        #endregion 
        #endregion  



        [SettingPropertyBool("{=KTMCM_AL}Item Auto Locks", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_ALH}Allows for auto locking horses, food , and smithing materials.")]
        [SettingPropertyGroup("{=KTMCM_CAutoLocks}Auto Locks")]
        public bool MCMAutoLocks { get; set; } = false;
        #region ItemLocks

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




        [SettingPropertyBool("{=KTMCM_AM}Army Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_AMH}Enable army modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CArmy}Army")]
        public bool MCMArmy { get; set; } = false;
        #region ArmyManagement
        #region Cohesion

        [SettingPropertyBool("{=KTMCM_AMCM}Cohesion Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_AMSMH}Allows Cohesion modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CArmy}Army/{=KTMCM_CCohesion}Cohesion")]
        public bool armyCohesionMultipliers { get; set; } = false;

        [SettingPropertyInteger("{=KTMCM_AMCMBC}Base Cohesion Change", -5, 5, "0 Cohesion", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_AMCMBCH}Base Cohesion Change for armies [Native : -2].")]
        [SettingPropertyGroup("{=KTMCM_CArmy}Army/{=KTMCM_CCohesion}Cohesion")]
        public int armyCohesionBaseChange { get; set; } = -2;

        [SettingPropertyBool("{=KTMCM_AMCMDCOO}Disable Clan Only other modifiers", Order = 0, RequireRestart = false, 
            HintText = "{=}Disable other cohesion loss except base for armies that are made up of a single clans parties only.")]
        [SettingPropertyGroup("{=KTMCM_CArmy}Army/{=KTMCM_CCohesion}Cohesion")]
        public bool armyDisableCohesionLossClanOnlyParties { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_AMCMCO}Clan Only Use Multiplier", Order = 0, RequireRestart = false, 
            HintText = "{=KTMCM_AMCMCOH}Apply the cohesion multiplier to armies that are made up of a single clans parties only.")]
        [SettingPropertyGroup("{=KTMCM_CArmy}Army/{=KTMCM_CCohesion}Cohesion")]
        public bool armyApplyMultiplerToClanOnlyParties { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_AMCHMM}Cohesion Multiplier", 0.1f, 3.0f, "#0%", RequireRestart = false,
            HintText = "{=KTMCM_AMCHMMH}Multiply the cohesion loss by this multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CArmy}Army/{=KTMCM_CCohesion}Cohesion")]
        public float armyCohesionLossMultiplier { get; set; } = 1.0f;

        #endregion
        #endregion ArmyManagement




        [SettingPropertyBool("{=}Relation Building Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=}Relation Building modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CRelationBuilding}Relation Building")]
        public bool MCMRelationBuilding { get; set; } = false;
        #region Relations

        #region KillingBandits
        [SettingPropertyBool("{=KTMCM_RB}Killing Bandits Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBH}Relation Building Killing Bandits modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CRelationBuilding}Relation Building/{=KTMCM_CBandits}Bandits")]
        public bool relationsKillingBanditsEnabled { get; set; } = false;

        [SettingPropertyInteger("{=KTMCM_RBKBGB}Groups for bonus", 1, 50, "0 Parties", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKBGBH}Number of bandit groups you must destroy before you gain relation.")]
        [SettingPropertyGroup("{=KTMCM_CRelationBuilding}Relation Building/{=KTMCM_CBandits}Bandits")]
        public int GroupsOfBandits { get; set; } = 1;

        [SettingPropertyInteger("{=KTMCM_RBKBRI}Relationship Increase", 1, 50, "0 Points", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKBRIH}The base value that your relationship will increase by when it increases.")]
        [SettingPropertyGroup("{=KTMCM_CRelationBuilding}Relation Building/{=KTMCM_CBandits}Bandits")]
        public int RelationshipIncrease { get; set; } = 1;

        [SettingPropertyInteger("{=KTMCM_RBKBR}Radius", 500, 5000, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKBRH}This is the size of the radius inside which villages and towns will be affected by the relationship increase.")]
        [SettingPropertyGroup("{=KTMCM_CRelationBuilding}Relation Building/{=KTMCM_CBandits}Bandits")]
        public int Radius { get; set; } = 1000;

        [SettingPropertyBool("{=KTMCM_RBKBSB}Size Bonus", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKBSBH}Enable Size bonus modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CRelationBuilding}Relation Building/{=KTMCM_CBandits}Bandits")]
        public bool SizeBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_RBKBSBM}Size bonus Modifier", 0.1f, 1.0f, RequireRestart = false,
            HintText = "{=KTMCM_RBKBSBMH}Multiply the Size Bonus by the number of bandits you have killed since you last gained relationship. this will then be multiplied by the base Relationship Increase to give your final increase value.")]
        [SettingPropertyGroup("{=KTMCM_CRelationBuilding}Relation Building/{=KTMCM_CBandits}Bandits")]
        public float SizeBonus { get; set; } = 0.05f;

        [SettingPropertyBool("{=KTMCM_RBKBOPP}Only Bandits with Prisoners", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKBOPPH}Enable relationship bonuses only for bandit parties with prisoners.")]
        [SettingPropertyGroup("{=KTMCM_CRelationBuilding}Relation Building/{=KTMCM_CBandits}Bandits")]
        public bool PrisonersOnly { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_RBKBBP}Bandits Parties", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKBBPH}Enable relationship bonuses for bandit parties.")]
        [SettingPropertyGroup("{=KTMCM_CRelationBuilding}Relation Building/{=KTMCM_CBandits}Bandits")]
        public bool IncludeBandits { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_RBKBOP}Outlaw Parties", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKBOPH}Enable relationship bonuses for outlaw parties.")]
        [SettingPropertyGroup("{=KTMCM_CRelationBuilding}Relation Building/{=KTMCM_CBandits}Bandits")]
        public bool IncludeOutlaws { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_RBKMGP}Mafia Parties", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_RBKMGPH}Enable relationship bonuses for mafia parties.")]
        [SettingPropertyGroup("{=KTMCM_CRelationBuilding}Relation Building/{=KTMCM_CBandits}Bandits")]
        public bool IncludeMafia { get; set; } = false;

        #endregion

        #endregion








        [SettingPropertyBool("{=KTMCM_XPM}Xp Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_XPMH}Enable Xp modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers")]
        public bool MCMSkillsXp { get; set; } = false;

        #region XpMultipliers
        [SettingPropertyBool("{=KTMCM_XPMSM}Skill Xp Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_XPMSMH}Enable Skill Xp modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills")]
        public bool SkillXpEnabled { get; set; } = false;
        #region Skills
        [SettingPropertyBool("{=KTMCM_XPMSMP}Player Skill Modifiers", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_XPMSMPH}Enable Player to use Skill Xp modifiers")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills")]
        public bool SkillXpUseForPlayer { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_XPMSMPC}Player Clan Skill Modifiers", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_XPMSMPCH}Enable Player Clan to use Skill Xp modifiers")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills")]
        public bool SkillXpUseForPlayerClan { get; set; } = false;

        [SettingPropertyBool("{=KTMCM_XPMSMAI}AI Skill Modifiers", Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_XPMSMAIH}Enable AI to use Xp modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills")]
        public bool SkillXpUseForAI { get; set; } = false;



        [SettingPropertyBool("{=KTMCM_XPMSMID}Individual Skill Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_XPMSMIDH}Enable Individual Skill Xp modifiers. Use this or global they don't work at same time")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public bool SkillXpUseIndividualMultiplers { get; set; } = false;

        #region IndividualMultipliers
        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDA}Athletics Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDAH}Multiply Athletics skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierAthletics { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDB}Bow Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDBH}Multiply Bow skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierBow { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDC}Charm Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDCH}Multiply Charm skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierCharm { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDCS}Crafting Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDCSH}Multiply Crafting skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierCrafting { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDCB}Crossbow Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDCBH}Multiply Crossbow skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierCrossbow { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDE}Engineering Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDEH}Multiply Engineering skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierEngineering { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDL}Leadership Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDLH}Multiply Leadership skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierLeadership { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDM}Medicine Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDMH}Multiply Medicine skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierMedicine { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDOH}OneHanded Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDOHH}Multiply OneHanded skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierOneHanded { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDP}Polearm Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDPH}Multiply Polearm skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierPolearm { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDR}Riding Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDRH}Multiply Riding skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierRiding { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDRG}Roguery Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDRGH}Multiply Roguery skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierRoguery { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDS}Scouting Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDSH}Multiply Scouting skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierScouting { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDST}Steward Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDSTH}Multiply Steward skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierSteward { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDT}Tactics Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDTH}Multiply Tactics skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierTactics { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDTR}Throwing Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDTRH}Multiply Throwing skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierThrowing { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDTS}Trade Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDTSH}Multiply Trade skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierTrade { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMIDTHW}TwoHanded Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMIDTHWH}Multiply TwoHanded skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CIndividual}Individual")]
        public float SkillsXPMultiplierTwoHanded { get; set; } = 1.0f;

        #endregion

        [SettingPropertyBool("{=KTMCM_XPMSMGS}Global Skill Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_XPMSMGSH}Enable Global Skill Xp modifiers. Use this or global they don't work at same time")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CGlobal}Global")]
        public bool SkillXpUseGlobalMultipler { get; set; } = false;
        #region Global

        [SettingPropertyFloatingInteger("{=KTMCM_XPMSMGSM}Global Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMSMGSMH}Multiply skills exp gains by the Global multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=KTMCM_CSkills}Skills/{=KTMCM_CGlobal}Global")]
        public float SkillsXpGlobalMultiplier { get; set; } = 1.0f;
        #endregion

        #endregion


        [SettingPropertyBool("{=BT_Settings_006500}Daily Troop Experience" + "*", IsToggle = true, Order = 0, RequireRestart = true,
            HintText = "{=BT_Settings_006500_Desc}Gives each troop roster (stack) in a party an amount of experience each day based upon the leader's Leadership skill. By default only applies to the player.")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=BT_Settings_006500}Daily Troop Experience")]//{=BT_Settings_006000}Party Tweaks
        public bool DailyTroopExperienceTweakEnabled { get; set; } = false;

        #region Party Tweaks - Troop Daily Experience Tweak
        [SettingPropertyFloatingInteger("{=BT_Settings_006501}Percentage of Leadership", 0.0f, 50.0f, "#0%", RequireRestart = false,
            HintText = "{=BT_Settings_006501_Desc}The percentage of the leader's Leadership skill to be given as experience to each of their troop rosters. With 100 leadership and a setting of 1000% each troop type stack will get 1.000 xp daily.")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=BT_Settings_006500}Daily Troop Experience")]
        public float LeadershipPercentageForDailyExperienceGain { get; set; } = 0f;

        [SettingPropertyInteger("{=BT_Settings_006502}Starting from Level Of Leadership", 1, 200 , Order = 0, RequireRestart = false,
            HintText = "{=BT_Settings_006502_Desc}The Leadership level required to start giving experience to troop rosters. With this setting at 20, daily training of your troop stacks will start from leadership 20 onwards (but be calculated with the full 20 skillpoints).")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=BT_Settings_006500}Daily Troop Experience")]
        public int DailyTroopExperienceRequiredLeadershipLevel { get; set; } = 30;

        [SettingPropertyBool("{=BT_Settings_006503}Apply to Player's Clan Members", Order = 0, RequireRestart = true,
            HintText = "{=BT_Settings_006503_Desc}Applies the daily troop experience gain to members of the player's clan also.")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=BT_Settings_006500}Daily Troop Experience")]
        public bool DailyTroopExperienceApplyToPlayerClanMembers { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_006504}Apply to all NPC Lords", Order = 0, RequireRestart = true,
            HintText = "{=BT_Settings_006504_Desc}Applies the daily troop experience gain to all NPC lords.")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=BT_Settings_006500}Daily Troop Experience")]
        public bool DailyTroopExperienceApplyToAllNPC { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_006505}Display Message", Order = 0, RequireRestart = true,
            HintText = "{=BT_Settings_006505_Desc}Displays a message showing the amount of experience granted.")]
        [SettingPropertyGroup("{=KTMCM_CXpModifiers}Xp Modifiers/{=BT_Settings_006500}Daily Troop Experience")]
        public bool DisplayMessageDailyExperienceGain { get; set; } = false;

        #endregion


        #endregion




        /*

                [SettingPropertyBool("Learning Rate Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
                    HintText = "{=}Enable Learning Rate modifiers.")]
                [SettingPropertyGroup("Learning Rate")]
                public bool MCMLearningRate { get; set; } = false;
        */

        #region LearningRateMultipliers
        [SettingPropertyBool("{=KTMCM_XPMLR}Learning Rate Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_XPMLRH}Enable Learning Rate modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CLearning}Learning Rate")]
        public bool LearningRateEnabled { get; set; } = false;


        [SettingPropertyFloatingInteger("{=KTMCM_XPMLRM}Global Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMLRMH}Multiply Learning Rate by the Global multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CLearning}Learning Rate")]
        public float LearningRateMultiplier { get; set; } = 1.0f;

        #endregion



        #region MobilePartyFoodConsumption
        [SettingPropertyBool("{=KTMCM_XPMMPF}Party Food Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "{=KTMCM_XPMMPFH}Enable Party food consumption modifiers.")]
        [SettingPropertyGroup("{=KTMCM_CParty}Party/{=KTMCM_CFood}Food")]
        public bool PartyFoodConsumptionEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=KTMCM_XPMMPFM}Party Food Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, 
            HintText = "{=KTMCM_XPMMPFMH}Multiply Party food consumption by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("{=KTMCM_CParty}Party/{=KTMCM_CFood}Food")]
        public float PartyFoodConsumptionMultiplier { get; set; } = 1.0f;
        #endregion


        //~ BT Keep
        #region Prisoner Tweaks #9

        #region Imprisonmewnt Time Tweaks

        [SettingPropertyBool("{=BT_Settings_007100}Imprisonment Time" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_007100_Desc}Adds a minimum amount of time before lords can attempt to escape imprisonment."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007100}Imprisonment Time" + "*", GroupOrder = 1)]
        public bool PrisonerImprisonmentTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_007101}Player Prisoners Only", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_007101_Desc}Whether the tweak should be applied only to prisoners held by the player."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007100}Imprisonment Time" + "*")]
        public bool PrisonerImprisonmentPlayerOnly { get; set; } = true;

        [SettingPropertyInteger("{=BT_Settings_007102}Minimum Days of Imprisonment", 0, 180, "0 Days", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_007102_Desc}The minimum number of days a lord will remain imprisoned before they can attempt to escape."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007100}Imprisonment Time" + "*")]
        public int MinimumDaysOfImprisonment { get; set; } = 0;

        [SettingPropertyBool("{=BT_Settings_007103}Enable Missing Prisoner Hero Fix" + "*", Order = 4, RequireRestart = true,
            HintText = "{=BT_Settings_007103_Desc}Will attempt to detect and release prisoner Heroes who may be bugged and do not respawn. Will trigger 3 days after the Minimum Days of Imprisonment setting."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007100}Imprisonment Time" + "*")]
        public bool EnableMissingHeroFix { get; set; } = false;

        #endregion

        #region Prisoner Size Tweak

        [SettingPropertyBool("{=BT_Settings_007200}Prisoner Size Bonus" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_007200_Desc}Enables a % bonus to your party's maximum prisoner size."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007200}Prisoner Size Bonus" + "*", GroupOrder = 2)]
        public bool PrisonerSizeTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_007201}Prisoner Size Bonus", 0f, 5f, "0%", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_007201_Desc}Adds a % bonues to your party's maximum prisoner size. Native is 0%."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007200}Prisoner Size Bonus" + "*")]
        public float PrisonerSizeTweakPercent { get; set; } = 0;

        [SettingPropertyBool("{=BT_Settings_007202}Also apply to AI", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_007202_Desc}Wether the prisoner size bonus should apply to AI Lords."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007200}Prisoner Size Bonus" + "*")]
        public bool PrisonerSizeTweakAI { get; set; } = false;

        #endregion

        #region Prisoner Confirmity Tweaks

        [SettingPropertyBool("{=BT_Settings_007300}Prisoner Confirmity" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_007300_Desc}Modifies the conformity rate of the base game, speeding the rate at which prisoners can be recruited."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007300}Prisoner Confirmity" + "*", GroupOrder = 3)]
        public bool PrisonerConformityTweaksEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_007301}Prisoner Confirmity Bonus", 0f, 10f, "0%", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_007301_Desc}Adds a % bonues to the conformity generated each hour. Native is 0%."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007300}Prisoner Confirmity" + "*")]
        public float PrisonerConformityTweakBonus { get; set; } = 0;

        [SettingPropertyBool("{=BT_Settings_007302}Apply Prisoner Confirmity Tweaks to Clan", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_007302_Desc}Applies Prisoner Conformity Tweaks to all clan parties as well."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007300}Prisoner Confirmity" + "*")]
        public bool PrisonerConformityTweaksApplyToClan { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_007303}Apply Prisoner Confirmity Tweaks to AI", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_007303_Desc}Applies Prisoner Conformity Tweaks to all parties, including AI lords as well."),
            SettingPropertyGroup("{=BT_Settings_007000}Prisoner Tweaks" + "/" + "{=BT_Settings_007300}Prisoner Confirmity" + "*")]
        public bool PrisonerConformityTweaksApplyToAi { get; set; } = false;

        #endregion

        #endregion



        #region Campaign Tweaks #2

        #region Campaign Tweaks - Battle Size Tweak

        [SettingPropertyBool("{=BT_Settings_001100}Battle Size Tweak" + "*", Order = 1, IsToggle = true, RequireRestart = true,
            HintText = "{=BT_Settings_001100_Desc}Allows you to set the battle size limit outside of native values. WARNING: Setting this above 1000 can cause performance degradation and crashes."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001100}Battle Size Tweak" + "*", GroupOrder = 1)]
        public bool BattleSizeTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_001101}Battle Size Limit", 2, 1800, "0 Soldiers", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_001101_Desc}Sets the limit for number of troops on a battlefield, ignoring what is in Bannerlord Options. WARNING: Will crash if all troops + their horses exceed 2000."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001100}Battle Size Tweak" + "*")]
        public int BattleSize { get; set; } = 1000;

        #endregion

        #region Campaign Tweaks - Difficulty Settings

        [SettingPropertyBool("{=BT_Settings_001200}Difficulty Tweaks" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_001200_Desc}Allows you to change the multiplier for several difficulty settings."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "*", GroupOrder = 2)]
        public bool DifficultyTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_001201}Damage to Player Tweak", Order = 2, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_001201_Desc}Allows you to change the multiplier for damage the player receives."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "*/" + "{=BT_Settings_001201}Damage to Player Tweak", GroupOrder = 1)]
        public bool DamageToPlayerTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_001202}Damage to Player Tweak Amount", 0.1f, 5.0f, "0%", RequireRestart = false, Order = 3,
            HintText = "{=BT_Settings_001202_Desc}Native values: Very Easy: 30%, Easy: 67%, Realistic: 100%. This value is used to calculate the damage player receives."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "*/" + "{=BT_Settings_001201}Damage to Player Tweak")]
        public float DamageToPlayerMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("{=BT_Settings_001203}Damage to Friends Tweak", Order = 4, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_001203_Desc}Allows you to change the damage the player's friends receive."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "*/" + "{=BT_Settings_001203}Damage to Friends Tweak", GroupOrder = 2)]
        public bool DamageToFriendsTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_001204}Damage to Friends Tweak Amount", 0.1f, 5.0f, "0%", RequireRestart = false, Order = 5,
            HintText = "{=BT_Settings_001204_Desc}Native values: Very Easy: 30%, Easy: 67%, Realistic: 100%. This value is used to calculate the damage the player's friends receive."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "*/" + "{=BT_Settings_001203}Damage to Friends Tweak")]
        public float DamageToFriendsMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("{=BT_Settings_001205}Damage to Player's Troops Tweak", Order = 6, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_001205_Desc}Allows you to change the multiplier for damage the player's troops receive."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "*/" + "{=BT_Settings_001205}Damage to Player's Troops Tweak", GroupOrder = 3)]
        public bool DamageToTroopsTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_001206}Damage to Troops Tweak Amount", 0.1f, 5.0f, "0%", RequireRestart = false, Order = 7,
            HintText = "{=BT_Settings_001206_Desc}Native values: Very Easy: 30%, Easy: 67%, Realistic: 100%. This value is used to calculate the damage to the player's troops."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "*/" + "{=BT_Settings_001205}Damage to Player's Troops Tweak")]
        public float DamageToTroopsMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("{=BT_Settings_001207}Combat AI Difficulty Tweak", Order = 8, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_001207_Desc}Allows you to change the AI combat difficulty."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "*/" + "{=BT_Settings_001207}Combat AI Difficulty Tweak", GroupOrder = 4)]
        public bool CombatAIDifficultyTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_001208}Combat AI Difficulty Tweak Amount", 0.1f, 1.0f, "0%", RequireRestart = false, Order = 9,
            HintText = "{=BT_Settings_001208_Desc}Native values: Very Easy: 10%, Easy: 32%, Realistic: 96%. This value is used to calculate AI combat difficulty."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "*/" + "{=BT_Settings_001207}Combat AI Difficulty Tweak")]
        public float CombatAIDifficultyMultiplier { get; set; } = 0.96f;

        [SettingPropertyBool("{=BT_Settings_001209}Player Map Movement Speed Tweak", Order = 10, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_001209_Desc}Allows you to change the bonus map movement speed multiplier the player receives."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "*/" + "{=BT_Settings_001209}Player Map Movement Speed Tweak", GroupOrder = 5)]
        public bool PlayerMapMovementSpeedBonusTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_001210}Player Map Movement Tweak Amount", 0.0f, 2.0f, "0%", RequireRestart = false, Order = 11,
            HintText = "{=BT_Settings_001210_Desc}Native values: Very Easy: 10%, Easy: 5%, Realistic: 0%. This value is used to calculate player's map movement speed."),
            SettingPropertyGroup("{=BT_Settings_001000}Campaign Tweaks" + "/" + "{=BT_Settings_001200}Difficulty Tweaks" + "*/" + "{=BT_Settings_001209}Player Map Movement Speed Tweak")]
        public float PlayerMapMovementSpeedBonusMultiplier { get; set; } = 0.0f;

        #endregion

        #endregion


        #region Settlement Tweaks #10

        #region Settlement Tweaks - Settlement culture

        [SettingPropertyBool("{=BT_Settings_008100}Settlement Culture Transformation" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008100_Desc}Changes the culture of settlement in relation to the owner clan. On deactivation cultures revert back. The last town of a culture will not be changed!"),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008100}Settlement Culture Transformation" + "*", GroupOrder = 1)]
        public bool EnableCultureChanger { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_008101}Change To Culture Of Kingdom Faction Instead" + "*", Order = 1, RequireRestart = true, IsToggle = false,
            HintText = "{=BT_Settings_008101_Desc}Instead of changing the culture to its owner-clan culture, change to its kingdom culture."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008100}Settlement Culture Transformation" + "*")]
        public bool ChangeToKingdomCulture { get; set; } = false;

        [SettingPropertyDropdown("{=BT_Settings_008102}Override Culture For Player Clan" + "*", Order = 3, RequireRestart = true,
            HintText = "{=BT_Settings_008102_Desc}Overrides the culture to change to for player clan owned settlements."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008100}Settlement Culture Transformation" + "*")]
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
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008100}Settlement Culture Transformation" + "*")]
        public int TimeToChanceCulture { get; set; } = 10;

        #endregion


        #region Settlement Tweaks - Disable Troop Donations

        [SettingPropertyBool("{=BT_Settings_008200}Disable Troop Donations" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008200_Desc}Disables your clan parties from donating troops to clan owned settlements."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008200}Disable Troop Donations" + "*", GroupOrder = 2)]
        public bool DisableTroopDonationPatchEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_008201}Disable Troop Donations - Any Settlement", Order = 1, RequireRestart = false,
            HintText = "{=BT_Settings_008201_Desc}Additionally disables your clan parties from donating troops to ANY settlement."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008200}Disable Troop Donations" + "*")]
        public bool DisableTroopDonationAnyEnabled { get; set; } = false;

        #endregion

        #region Settlement Tweaks - Production Tweaks

        [SettingPropertyBool("{=BT_Settings_008300}Village Productivity" + "*", Order = 1, IsToggle = true, RequireRestart = true,
            HintText = "{=BT_Settings_008300_Desc}Enables Tweaks for increased productivity in villages."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008300}Village Productivity" + "*", GroupOrder = 3)]
        public bool ProductionTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008301}Village Production: Food", 1f, 3f, "0%", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_008301_Desc}Modifies the daily production of food goods in villages."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008300}Village Productivity" + "*")]
        public float ProductionFoodTweakEnabled { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008302}Village Production: Other goods", 1f, 3f, "0%", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_008302_Desc}Modifies the daily production of other goods in villages."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008300}Village Productivity" + "*")]
        public float ProductionOtherTweakEnabled { get; set; } = 1f;

        #endregion Settlement Tweaks - Disable Troop Donations

        #region Settlement Tweaks - Buildings

        #region Settlement Tweaks - Buildings - Castle

        #region Settlement Tweaks - Buildings - Castle - Training Fields

        [SettingPropertyBool("{=BT_Settings_008402}Castle Training Field" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008402_Desc}Changes the amount of experience the training fields provides for each level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008402}Castle Training Field" + "*", GroupOrder = 1)]
        public bool CastleTrainingFieldsBonusEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008403}Castle Training Fields Experience Level 1" + "*", 1, 100, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008403_Desc}Native value is 1. Changes the amount of experience the training fields provides at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008402}Castle Training Field" + "*")]
        public int CastleTrainingFieldsXpAmountLevel1 { get; set; } = 1;

        [SettingPropertyInteger("{=BT_Settings_008404}Castle Training Fields Experience Level 2" + "*", 2, 200, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008404_Desc}Native value is 2. Changes the amount of experience the training fields provides at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008402}Castle Training Field" + "*")]
        public int CastleTrainingFieldsXpAmountLevel2 { get; set; } = 2;

        [SettingPropertyInteger("{=BT_Settings_008405}Castle Training Fields Experience Level 3" + "*", 3, 300, RequireRestart = true, Order = 4,
            HintText = "{=BT_Settings_008405_Desc}Native value is 3. Changes the amount of experience the training fields provides at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008402}Castle Training Field" + "*")]
        public int CastleTrainingFieldsXpAmountLevel3 { get; set; } = 3;

        #endregion

        #region Settlement Tweaks - Buildings - Castle - Granary 

        [SettingPropertyBool("{=BT_Settings_008406}Castle Granary" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008406_Desc}Changes the amount of food storage the castle granary provides per level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008406}Castle Granary" + "*", GroupOrder = 2)]
        public bool CastleGranaryBonusEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008407}Castle Granary Food Storage Level 1" + "*", 100, 1000, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008407_Desc}Native value is 100. Changes the amount of food storage the castle granary provides at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008406}Castle Granary" + "*")]
        public int CastleGranaryStorageAmountLevel1 { get; set; } = 100;

        [SettingPropertyInteger("{=BT_Settings_008408}Castle Granary Food Storage Level 2" + "*", 200, 2000, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008408_Desc}Native value is 200. Changes the amount of food storage the castle granary provides at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008406}Castle Granary" + "*")]
        public int CastleGranaryStorageAmountLevel2 { get; set; } = 200;

        [SettingPropertyInteger("{=BT_Settings_008409}Castle Granary Food Storage Level 3" + "*", 300, 3000, RequireRestart = true, Order = 4,
            HintText = "{=BT_Settings_008409_Desc}Native value is 300. Changes the amount of food storage the castle granary provides at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008406}Castle Granary" + "*")]
        public int CastleGranaryStorageAmountLevel3 { get; set; } = 300;

        #endregion

        #region Settlement Tweaks - Buildings - Castle - Gardens

        [SettingPropertyBool("{=BT_Settings_008410}Castle Gardens" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008410_Desc}Changes the amount of food the castle gardens produce per level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008410}Castle Gardens" + "*", GroupOrder = 3)]
        public bool CastleGardensBonusEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008411}Castle Garden Food Production Level 1" + "*", 5, 50, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008411_Desc}Native value is 5. Changes the amount of food the castle gardens produce at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008410}Castle Gardens" + "*")]
        public int CastleGardensFoodProductionAmountLevel1 { get; set; } = 5;

        [SettingPropertyInteger("{=BT_Settings_008412}Castle Garden Food Production Level 2" + "*", 10, 100, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008412_Desc}Native value is 10. Changes the amount of food the castle gardens produce at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008410}Castle Gardens" + "*")]
        public int CastleGardensFoodProductionAmountLevel2 { get; set; } = 10;

        [SettingPropertyInteger("{=BT_Settings_008413}Castle Garden Food Production Level 3" + "*", 15, 150, RequireRestart = true, Order = 4,
            HintText = "{=BT_Settings_008413_Desc}Native value is 15. Changes the amount of food the castle gardens produce at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008410}Castle Gardens" + "*")]
        public int CastleGardensFoodProductionAmountLevel3 { get; set; } = 15;

        #endregion

        #region Settlement Tweaks - Buildings - Castle - Militia Barracks

        [SettingPropertyBool("{=BT_Settings_008414}Castle Militia Barracks" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008414_Desc}Changes the militia production that the castle militia barracks provides per level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008414}Castle Militia Barracks" + "*", GroupOrder = 4)]
        public bool CastleMilitiaBarracksBonusEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008415}Castle Militia Barracks Production Level 1" + "*", 1, 10, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008415_Desc}Native value is 1. Changes the militia production that the castle militia barracks provides at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008414}Castle Militia Barracks" + "*")]
        public int CastleMilitiaBarracksAmountLevel1 { get; set; } = 1;

        [SettingPropertyInteger("{=BT_Settings_008416}Castle Militia Barracks Production Level 2" + "*", 2, 20, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008416_Desc}Native value is 2. Changes the militia production that the castle militia barracks provides at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008414}Castle Militia Barracks" + "*")]
        public int CastleMilitiaBarracksAmountLevel2 { get; set; } = 2;

        [SettingPropertyInteger("{=BT_Settings_008417}Castle Militia Barracks Production Level 3" + "*", 3, 30, RequireRestart = true, Order = 4,
            HintText = "{=BT_Settings_008417_Desc}Native value is 3. Changes the militia production that the castle militia barracks provides at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008401}Castle" + "/" + "{=BT_Settings_008414}Castle Militia Barracks" + "*")]
        public int CastleMilitiaBarracksAmountLevel3 { get; set; } = 3;

        #endregion

        #endregion

        #region Settlement Tweaks - Buildings - Town 

        #region Settlement Tweaks - Buildings - Town - Training Fields

        [SettingPropertyBool("{=BT_Settings_008419}Town Training Fields" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008419_Desc}Changes the amount of experience the training fields provides for each level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008419}Town Training Fields" + "*", GroupOrder = 1)]
        public bool TownTrainingFieldsBonusEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008420}Town Training Fields Experience Level 1" + "*", 30, 300, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008420_Desc}Native value is 30. Changes the amount of experience the training fields provides at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008419}Town Training Fields" + "*")]
        public int TownTrainingFieldsXpAmountLevel1 { get; set; } = 30;

        [SettingPropertyInteger("{=BT_Settings_008421}Town Training Fields Experience Level 2" + "*", 60, 600, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008421_Desc}Native value is 60. Changes the amount of experience the training fields provides at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008419}Town Training Fields" + "*")]
        public int TownTrainingFieldsXpAmountLevel2 { get; set; } = 60;

        [SettingPropertyInteger("{=BT_Settings_008422}Town Training Fields Experience Level 3" + "*", 100, 1000, RequireRestart = true, Order = 4,
            HintText = "{=BT_Settings_008422_Desc}Native value is 100. Changes the amount of experience the training fields provides at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008419}Town Training Fields" + "*")]
        public int TownTrainingFieldsXpAmountLevel3 { get; set; } = 100;

        #endregion

        #region Settlement Tweaks - Buildings - Town - Granary

        [SettingPropertyBool("{=BT_Settings_008423}Town Granary" + "*", RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008423_Desc}Changes the amount of food storage the town granary provides per level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008423}Town Granary" + "*", GroupOrder = 2)]
        public bool TownGranaryBonusEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008424}Town Granary Food Storage Level 1" + "*", 200, 2000, RequireRestart = true, Order = 1,
            HintText = "{=BT_Settings_008424_Desc}Native value is 200. Changes the amount of food storage the town granary provides at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008423}Town Granary" + "*")]
        public int TownGranaryStorageAmountLevel1 { get; set; } = 200;

        [SettingPropertyInteger("{=BT_Settings_008425}Town Granary Food Storage Level 2" + "*", 400, 4000, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008425_Desc}Native value is 400. Changes the amount of food storage the town granary provides at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008423}Town Granary" + "*")]
        public int TownGranaryStorageAmountLevel2 { get; set; } = 400;

        [SettingPropertyInteger("{=BT_Settings_008426}Town Granary Food Storage Level 3" + "*", 600, 6000, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008426_Desc}Native value is 600. Changes the amount of food storage the town granary provides at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008423}Town Granary" + "*")]
        public int TownGranaryStorageAmountLevel3 { get; set; } = 600;

        #endregion

        #region Settlement Tweaks - Settlement Buildings Tweaks - Town Buildings Tweaks - Orchards Tweak

        [SettingPropertyBool("{=BT_Settings_008427}Town Orchards" + "*", Order = 1, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_008427_Desc}Changes the amount of food the town orchards produce per level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008427}Town Orchards" + "*", GroupOrder = 3)]
        public bool TownOrchardsBonusEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008428}Town Orchard Food Production Level 1" + "*", 10, 100, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008428_Desc}Native value is 10. Changes the amount of food the town orchards produce at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008427}Town Orchards" + "*")]
        public int TownOrchardsFoodProductionAmountLevel1 { get; set; } = 10;

        [SettingPropertyInteger("{=BT_Settings_008429}Town Orchard Food Production Level 2" + "*", 20, 200, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008429_Desc}Native value is 20. Changes the amount of food the town orchards produce at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008427}Town Orchards" + "*")]
        public int TownOrchardsFoodProductionAmountLevel2 { get; set; } = 20;

        [SettingPropertyInteger("{=BT_Settings_008430}Town Orchard Food Production Level 3" + "*", 30, 300, RequireRestart = true, Order = 4,
            HintText = "{=BT_Settings_008430_Desc}Native value is 30. Changes the amount of food the town orchards produce at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008427}Town Orchards" + "*")]
        public int TownOrchardsFoodProductionAmountLevel3 { get; set; } = 30;

        #endregion

        #region Settlement Tweaks - Settlement Buildings Tweaks - Town Buildings Tweaks - Militia Barracks Tweak

        [SettingPropertyBool("{=BT_Settings_008431}Town Militia Barracks" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008431_Desc}Changes the militia production that the town militia barracks provides per level."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008431}Town Militia Barracks" + "*", GroupOrder = 4)]
        public bool TownMilitiaBarracksBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008432}Town Militia Barracks Production Level 1" + "*", .5f, 15, RequireRestart = true, Order = 2,
            HintText = "{=BT_Settings_008432_Desc}Native value is .5. Changes the militia production that the town militia barracks provides at level 1."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008431}Town Militia Barracks" + "*")]
        public float TownMilitiaBarracksAmountLevel1 { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008433}Town Militia Barracks Production Level 2" + "*", 1f, 20f, RequireRestart = true, Order = 3,
            HintText = "{=BT_Settings_008433_Desc}Native value is 1. Changes the militia production that the town militia barracks provides at level 2."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008431}Town Militia Barracks" + "*")]
        public float TownMilitiaBarracksAmountLevel2 { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008434}Town Militia Barracks Production Level 3" + "*", 1.5f, 30f, RequireRestart = true, Order = 4,
            HintText = "{=BT_Settings_008434_Desc}Native value is 1.5. Changes the militia production that the town militia barracks provides at level 3."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008400}Buildings" + "/" + "{=BT_Settings_008418}Town" + "/" + "{=BT_Settings_008431}Town Militia Barracks" + "*")]
        public float TownMilitiaBarracksAmountLevel3 { get; set; } = 1.5f;

        #endregion

        #endregion

        #endregion

        #region Settlement Tweaks - Settlement Food

        [SettingPropertyBool("{=BT_Settings_008500}Settlement Food" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008500_Desc}Enables tweaks which provide bonuses to food production in towns and castles."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008500}Settlement Food" + "*", GroupOrder = 4)]
        public bool SettlementFoodBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008501}Castle Food Modifier", 1f, 10f, "0%", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_008501_Desc}Native value is 100%. Adds a modifier to food production in castles."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008500}Settlement Food" + "*")]
        public float CastleFoodBonus { get; set; } = 0f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008502}Town Food Modifier", 1f, 10f, "0%", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_008502_Desc}Native value is 100%. Adds a modifier to food production in towns."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008500}Settlement Food" + "*")]
        public float TownFoodBonus { get; set; } = 0f;

        #region Settlement Tweaks - Settlement Food Bonus - Food Loss from Prosperity Tweak

        [SettingPropertyBool("{=BT_Settings_008503}Food Loss From Prosperity", Order = 1, RequireRestart = false, IsToggle = true,
            HintText = "{=BT_Settings_008503_Desc}Allows you to adjust the loss to food production received from settlement prosperity."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008500}Settlement Food" + "*/" + "{=BT_Settings_008503}Food Loss from Prosperity", GroupOrder = 1)]
        public bool SettlementProsperityFoodMalusTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008504}Prosperity Food Loss Divisor", 50, 400, RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008504_Desc}Native value is 50. The prosperity of the settlement is divided by this value to calculate the loss. Increasing this value will decrease the amount of food lost."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008500}Settlement Food" + "*/" + "{=BT_Settings_008503}Food Loss from Prosperity")]
        public int SettlementProsperityFoodMalusDivisor { get; set; } = 50;

        #endregion

        #endregion

        #region Settlement Tweaks - Normal Militia

        [SettingPropertyBool("{=BT_Settings_008600}Normal Militia" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008600_Desc}Grants a flat bonus to militia growth and rate of retirement in towns and castles."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008600}Normal Militia" + "*", GroupOrder = 5)]
        public bool SettlementMilitiaBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008601}Castle Militia Growth Bonus", 0f, 50f, "0.0 Militia/Day", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008601_Desc}Native value is 0. Adds a flat bonus on how many militia gets recruited each day in castles."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008600}Normal Militia" + "*")]
        public float CastleMilitiaBonusFlat { get; set; } = 0f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008602}Town Militia Growth Bonus", 0f, 50f, "0.0 Militia/Day", RequireRestart = false, Order = 3,
            HintText = "{=BT_Settings_008602_Desc}Native value is 0. Adds a flat bonus on how many militia gets recruited each day in towns."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008600}Normal Militia" + "*")]
        public float TownMilitiaBonusFlat { get; set; } = 0f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008603}Castle Militia Retirement Modifier", 0f, 0.25f, "0.0%/Day", RequireRestart = false, Order = 3,
            HintText = "{=BT_Settings_008603_Desc}Native value is 2.5%. Modifies the percentage of your militia retiring each dayin castles."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008600}Normal Militia" + "*")]
        public float CastleMilitiaRetirementModifier { get; set; } = 0.025f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008604}Town Militia Retirement Modifier", 0f, 0.25f, "0.0%/Day", RequireRestart = false, Order = 3,
            HintText = "{=BT_Settings_008604_Desc}Native value is 2.5%. Modifies the percentage of your militia retiring each dayin town."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008600}Normal Militia" + "*")]
        public float TownMilitiaRetirementModifier { get; set; } = 0.025f;

        #endregion

        #region Settlement Tweaks - Militia Bonus Tweaks - Elite Militia

        [SettingPropertyBool("{=BT_Settings_008700}Elite Militia" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008700_Desc}Adds a bonus to the chance that militia spawning in towns and castles are elite."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008700}Elite Militia" + "*", GroupOrder = 6)]
        public bool SettlementMilitiaEliteSpawnRateBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008701}Elite Melee Militia Spawn Chance", 0.01f, 1f, "0%", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008701_Desc}Native value is 10%. Sets the chance that the militia spawning in towns and castles are elite melee troops."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008700}Elite Militia" + "*")]
        public float SettlementEliteMeleeSpawnRateBonus { get; set; } = 0.1f;

        [SettingPropertyFloatingInteger("{=BT_Settings_008702}Elite Ranged Militia Spawn Chance", 0.01f, 1f, "0%", RequireRestart = false, Order = 3,
            HintText = "{=BT_Settings_008702_Desc}Native value is 10%. Sets the chance that the militia spawning in towns and castles are elite ranged troops."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008700}Elite Militia" + "*")]
        public float SettlementEliteRangedSpawnRateBonus { get; set; } = 0.1f;

        #endregion

        #region Settlement Tweaks - Tournaments

        #region Settlement Tweaks - Tournaments - Renown Reward

        [SettingPropertyBool("{=BT_Settings_008801}Renown Reward" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008801_Desc}Sets the amount of renown awarded when you win a tournament."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008801}Renown Reward" + "*", GroupOrder = 1)]
        public bool TournamentRenownIncreaseEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008802}Tournament Renown Reward", 1, 50, "0 Renown", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008802_Desc}Native value is 3. Increases the amount of renown awarded when you win a tournament."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008801}Renown Reward" + "*")]
        public int TournamentRenownAmount { get; set; } = 3;

        #endregion

        #region Settlement Tweaks - Tournaments - Gold Reward

        [SettingPropertyBool("{=BT_Settings_008803}Gold Reward" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008803_Desc}Adds the set amount of gold to the rewards when you win a tournament."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008803}Gold Reward" + "*", GroupOrder = 2)]
        public bool TournamentGoldRewardEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008804}Tournament Gold Reward", 0, 5000, "0 Denar", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008804_Desc}Native value is 0. Adds the set amount of gold to the rewards when you win a tournament."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008803}Gold Reward" + "*")]
        public int TournamentGoldRewardAmount { get; set; } = 0;

        #endregion

        #region Settlement Tweaks - Tournaments - Maximum Bet

        [SettingPropertyBool("{=BT_Settings_008805}Maximum Bet" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008805_Desc}Sets the maximum amount of gold that you can bet per round in tournaments."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008805}Maximum Bet" + "*", GroupOrder = 3)]
        public bool TournamentMaxBetAmountTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008806}Maximum Bet Amount", 0, 4000, "0 Denar", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008806_Desc}Native value is 150. Sets the maximum amount of gold that you can bet per round in tournaments."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008805}Maximum Bet" + "*")]
        public int TournamentMaxBetAmount { get; set; } = 150;

        #endregion

        #region Settlement Tweaks - Tournaments - Hero Experience

        [SettingPropertyBool("{=BT_Settings_008807}Hero Experience Modifier" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008807_Desc}Overrides the native multiplier value for experience gain in tournaments for hero characters."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008807}Hero Experience Modifier" + "*", GroupOrder = 4)]
        public bool TournamentHeroExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008808}Tournament Hero Experience Modifier", 0.33f, 10f, "0%", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008808_Desc}Native value is 33%. Sets the modifier applied to experience gained in tournaments by hero characters. 100% = full real-world experience."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008807}Hero Experience Modifier" + "*")]
        public float TournamentHeroExperienceMultiplier { get; set; } = 0.33f;

        #endregion

        #region Settlement Tweaks - Tournaments - Arena Hero Experience

        [SettingPropertyBool("{=BT_Settings_008809}Arena Hero Experience Modifier" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008809_Desc}Overrides the native multiplier value for experience gain in arena fights for hero characters."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008809}Arena Experience Modifier" + "*", GroupOrder = 5)]
        public bool ArenaHeroExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008810}Arena Hero Experience Modifier", 0.06f, 5f, "0%", Order = 2, RequireRestart = false,
            HintText = "{=BT_Settings_008810_Desc}Native value is 6%. Sets the modifier applied to experience gain in arena fights for hero characters. 100% = full real-world experience."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008809}Arena Experience Modifier" + "*")]
        public float ArenaHeroExperienceMultiplier { get; set; } = 0.06f;

        #endregion

        #region Settlement Tweaks - Tournament Tweaks - Minimum Betting Odds

        [SettingPropertyBool("{=BT_Settings_008811}Minimum Betting Odds" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008811_Desc}Allows you to set the minimum betting odds in tournaments."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008811}Minimum Betting Odds" + "*", GroupOrder = 6)]
        public bool MinimumBettingOddsTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_008812}Minimum Betting Odds", 1.1f, 10f, RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008812_Desc}Native: 1.1. The minimum odds for tournament bets, higher means more yield for your bets, if won."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008800}Tournaments" + "/" + "{=BT_Settings_008811}Minimum Betting Odds" + "*")]
        public float MinimumBettingOdds { get; set; } = 1.1f;

        #endregion

        #endregion

        #region Settlement Tweaks - Workshops

        #region Settlement Tweaks - Workshops - Workshop Limit

        [SettingPropertyBool("{=BT_Settings_008901}Workshop Count Limit" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008901_Desc}Sets the base maximum number of workshops you can have and the limit increase gained per clan tier."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008901}Workshop Count Limit" + "*", GroupOrder = 1)]
        public bool MaxWorkshopCountTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008902}Base Workshop Count Limit", 0, 20, "0 Workshops", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008902_Desc}Native value is 1. Sets the base maximum number of workshops you can have."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008901}Workshop Count Limit" + "*")]
        public int BaseWorkshopCount { get; set; } = 1;

        [SettingPropertyInteger("{=BT_Settings_008903}Bonus Workshops Per Clan Tier", 0, 5, "0 Shops/Tier", RequireRestart = false, Order = 3,
            HintText = "{=BT_Settings_008903_Desc}Native value is 1. Sets the base maximum number of workshops you can have and the limit increase gained per clan tier."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008901}Workshop Count Limit" + "*")]
        public int BonusWorkshopsPerClanTier { get; set; } = 1;

        #endregion

        #region Settlement Tweaks - Workshops - Workshop Cost Tweak

        [SettingPropertyBool("{=BT_Settings_008904}Workshop Buy Cost" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_008904_Desc}Sets the base value used to calculate the cost of workshops. Reduce to reduce cost of workshops."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008904}Workshop Buy Cost" + "*", GroupOrder = 2)]
        public bool WorkshopBuyingCostTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_008905}Workshop Base Cost", 0, 15000, "0 Denar", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008905_Desc}Native value is 10,000. Sets the base value used to calculate the cost of workshops (+ workshop type base cost + 0.5 x town prosperity). Reduce to reduce cost of workshops."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008904}Workshop Buy Cost" + "*")]
        public int WorkshopBaseCost { get; set; } = 10000;

        #endregion

        #region Settlement Tweaks - Workshops - Workshop Effectivness

        [SettingPropertyBool("{=BT_Settings_008906}Workshop Effectivness" + "*", RequireRestart = true, IsToggle = true, Order = 2,
            HintText = "{=BT_Settings_008906_Desc}Increases effectivness of workshops by decreasing its daily expenses."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008906}Workshop Effectivness" + "*", GroupOrder = 3)]
        public bool WorkshopEffectivnessEnabled { get; set; } = false;
        [SettingPropertyFloatingInteger("{=BT_Settings_008907}Workshop Daily Cost Modifier", 0f, 1f, "0%", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008907_Desc}Native value is 100%. Increases effectivness of workshops by decreasing its daily expenses."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008906}Workshop Effectivness" + "*")]
        public float WorkshopEffectivnessv2Factor { get; set; } = 1f;

        #endregion

        #region Settlement Tweaks - Workshops - Workshop SellPrices

        [SettingPropertyBool("{=BT_Settings_008908}Workshop Products Sell Prices" + "*", RequireRestart = true, IsToggle = true, Order = 2,
            HintText = "{=BT_Settings_008908_Desc}Alters the selling prices for products of workshops."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008908}Workshop Products Sell Prices" + "*", GroupOrder = 4)]
        public bool EnableWorkshopSellTweak { get; set; } = false;
        [SettingPropertyFloatingInteger("{=BT_Settings_008909}Workshop Products Sell Prices", 0.01f, 5f, "0%", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008909_Desc}Native value is 100%. Alters the selling prices for products of workshops. Increase for better profits."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008908}Workshop Products Sell Prices" + "*")]
        public float WorkshopSellTweak { get; set; } = 1f;

        #endregion

        #region Settlement Tweaks - Workshops - Workshop Buy Prices

        [SettingPropertyBool("{=BT_Settings_008910}Workshop Products Buy Prices" + "*", RequireRestart = true, IsToggle = true, Order = 2,
            HintText = "{=BT_Settings_008910_Desc}Alters the buying prices for input items of workshops."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008910}Workshop Products Buy Prices" + "*", GroupOrder = 5)]
        public bool EnableWorkshopBuyTweak { get; set; } = false;
        [SettingPropertyFloatingInteger("{=BT_Settings_008911}Workshop Products Buy Prices", 0.01f, 5f, "0%", RequireRestart = false, Order = 2,
            HintText = "{=BT_Settings_008911_Desc}Native value is 100%. Alters the buying prices for input items of workshops. Decrease for better profits."),
            SettingPropertyGroup("{=BT_Settings_008000}Settlement Tweaks" + "/" + "{=BT_Settings_008900}Workshops" + "/" + "{=BT_Settings_008910}Workshop Products Buy Prices" + "*")]
        public float WorkshopBuyTweak { get; set; } = 1f;

        #endregion

        #endregion

        #endregion






        //~ BT Source

        #region Battle Tweaks #2

        #region Battle Tweaks - Battle Renown, Influence, Morale Tweaks

        [SettingPropertyBool("{=BT_Settings_000100}Battle Renown, Influence, Morale Tweaks" + "*", Order = 1, RequireRestart = true, IsToggle = true, 
            HintText = "{=BT_Settings_000100_Desc}Applies the set multiplier to renown, influence and morale gain from battles (applies to the player only)."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000100}Battle Renown, Influence, Morale Tweaks" + "*", GroupOrder = 1)]
        public bool BattleRewardTweaksEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_000101}Battle Renown Amount", 0.1f, 5f, "0%", Order = 2, RequireRestart = false, 
            HintText = "{=BT_Settings_000101_Desc}Native value is 100%. The amount of renown you receive from a battle is multiplied by this value."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000100}Battle Renown, Influence, Morale Tweaks" + "*")]
        public float BattleRenownMultiplier { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=BT_Settings_000102}Battle Influence Amount", 0.1f, 5f, "0%", RequireRestart = false, Order = 3, 
            HintText = "{=BT_Settings_000102_Desc}Native value is 100%. The amount of influence you receive from a battle is multiplied by this value."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000100}Battle Renown, Influence, Morale Tweaks" + "*")]
        public float BattleInfluenceMultiplier { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=BT_Settings_000103}Battle Morale Amount", 0.1f, 5f, "0%", RequireRestart = false, Order = 4,
            HintText = "{=BT_Settings_000103_Desc}Native value is 100%. The amount of morale you receive from a battle is multiplied by this value."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000100}Battle Renown, Influence, Morale Tweaks" + "*")]
        public float BattleMoraleMultiplier { get; set; } = 1f;

        [SettingPropertyBool("{=BT_Settings_000104}Also Apply To AI", Order = 5, RequireRestart = false, 
            HintText = "{=BT_Settings_000104_Desc}Applies the renown, influence and morale modifiers to AI parties."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000100}Battle Renown, Influence, Morale Tweaks" + "*")]
        public bool BattleRewardApplyToAI { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_000105}Show Calculation Message", Order = 6, RequireRestart = false, 
            HintText = "{=BT_Settings_000105_Desc}Shows detailed calculation for renown, influence and morale tweaks in message log."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000100}Battle Renown, Influence, Morale Tweaks" + "*")]
        public bool BattleRewardShowDebug { get; set; } = false;

        #endregion

        #region Battle Tweaks - Hideout Tweaks

        [SettingPropertyBool("{=BT_Settings_000200}Hideout Tweaks" + "*", Order = 1, RequireRestart = true, IsToggle = true, 
            HintText = "{=BT_Settings_000200_Desc}Changes game behavior inside hideout battles."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000200}Hideout Tweaks" + "*", GroupOrder = 2)]
        public bool HideoutBattleTroopLimitTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_000201}Hideout Battle Troop Limit", 5, 90, "0 Soldiers", Order = 2, RequireRestart = false, 
            HintText = "{=BT_Settings_000201_Desc}Native value is 9 or 10. Changes the maximum troop limit to the set value inside hideout battles. Cannot be higher than 90 because it causes bugs."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000200}Hideout Tweaks" + "*")]
        public int HideoutBattleTroopLimit { get; set; } = 10;

        [SettingPropertyBool("{=BT_Settings_000202}Continue Hideout Battle On Player Death" + "*", Order = 3, RequireRestart = true, 
            HintText = "{=BT_Settings_000202_Desc}Native value is false. If enabled, you will not automatically lose the hideout battle if you die. Your troops will charge and the boss duel will be disabled."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000200}Hideout Tweaks" + "*")]
        public bool ContinueHideoutBattleOnPlayerDeath { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_000203}Continue Battle On Losing Duel" + "*", Order = 4, RequireRestart = true, 
            HintText = "{=BT_Settings_000203_Desc}Native value is false. If enabled, your troops will rush to avenge you and finish everyone off."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000200}Hideout Tweaks" + "*")]
        public bool ContinueHideoutBattleOnPlayerLoseDuel { get; set; } = false;

        #endregion

        #region Battle Tweaks - Siege Tweaks

        [SettingPropertyBool("{=BT_Settings_000300}Siege Tweaks" + "*", Order = 1, RequireRestart = true, IsToggle = true, 
            HintText = "{=BT_Settings_000300_Desc}Tweaks for siege engine construction speed and collateral casulaties during sieges."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000300}Siege Tweaks" + "*", GroupOrder = 3)]
        public bool SiegeTweaksEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_000301}Siege Construction Progress Per Day Amount", 0.1f, 10.0f, "0%", RequireRestart = false, Order = 2, 
            HintText = "{=BT_Settings_000301_Desc}Native value is 100%. This tweak adds a modifier to the construction progress per day for sieges. A smaller number results in longer siege times."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000300}Siege Tweaks" + "*")]
        public float SiegeConstructionProgressPerDayMultiplier { get; set; } = 1f;

        [SettingPropertyInteger("{=BT_Settings_000302}Siege Collateral Damage Casualties", 0, 10, Order = 5, RequireRestart = false, 
            HintText = "{=BT_Settings_000302_Desc}Native value is 0. This tweak adds to the base value (1 or 2 with Crossbow Terror Perk) used to calculate collateral casualties during the campaign map siege stage."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000300}Siege Tweaks" + "*")]
        public int SiegeCollateralDamageCasualties { get; set; } = 0;

        [SettingPropertyInteger("{=BT_Settings_000303}Siege Destruction Casualties", 0, 10, Order = 6, RequireRestart = false, 
            HintText = "{=BT_Settings_000303_Desc}Native value is 0. This tweak adds to the base value (2) used to calculate destruction casualties during the campaign map siege stage."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000300}Siege Tweaks" + "*")]
        public int SiegeDestructionCasualties { get; set; } = 0;

        #endregion

        #region Battle Tweaks - Troop Experience Tweaks

        [SettingPropertyBool("{=BT_Settings_000400}Troop Experience" + "*", Order = 1, RequireRestart = true, IsToggle = true, 
            HintText = "{=BT_Settings_000400_Desc}Tweaks for experience gain of troops in battles and simulations."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000400}Troop Experience" + "*", GroupOrder = 4)]
        public bool TroopExperienceTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_000401}Troop Battle Experience", Order = 2, RequireRestart = false, IsToggle = true, 
            HintText = "{=BT_Settings_000401_Desc}Modifies the amount of experience that ALL troops receive during battles (Note: Only troops, not heroes)."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000400}Troop Experience" + "*/" + "{=BT_Settings_000401}Troop Battle Experience", GroupOrder = 1)]
        public bool TroopBattleExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_000402}Troop Battle Experience Amount", .01f, 6f, "0%", RequireRestart = false, Order = 3, 
            HintText = "{=BT_Settings_000402_Desc}Native value is 100%. Modifies the amount of experience that ALL troops receive during fought battles (Note: Only troops, not heroes. Does not apply to simulated battles.)."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000400}Troop Experience" + "*/" + "{=BT_Settings_000401}Troop Battle Experience")]
        public float TroopBattleExperienceMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("{=BT_Settings_000403}Troop Simulation Experience", Order = 4, RequireRestart = false, IsToggle = true, 
            HintText = "{=BT_Settings_000403_Desc}Modifies the experience gained from simulated battles. This is applied to all fights (including NPC fights) on the campaign map."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000400}Troop Experience" + "*/" + "{=BT_Settings_000403}Troop Simulation Experience", GroupOrder = 2)]
        public bool TroopBattleSimulationExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_000404}Troop Simulation Experience Amount", .01f, 8f, "0%", RequireRestart = false, Order = 5, 
            HintText = "{=BT_Settings_000404_Desc}Native value is 90%. Provides a multiplier to experience gained from simulated battles. This is applied to all simulated fights on the campaign map."), 
            SettingPropertyGroup("{=BT_Settings_000000}Battle Tweaks" + "/" + "{=BT_Settings_000400}Troop Experience" + "*/" + "{=BT_Settings_000403}Troop Simulation Experience")]
        public float TroopBattleSimulationExperienceMultiplier { get; set; } = 0.9f;

        #endregion

        #region Battle Tweaks - Weapon Cut Through Tweaks

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

        #endregion

        #endregion

        #region Character Tweaks #4

        #region Character Tweaks - Age Tweaks

        [SettingPropertyBool("{=BT_Settings_002100}Age Tweaks" + "*", Order = 1, RequireRestart = true, IsToggle = true, 
            HintText = "{=BT_Settings_002100_Desc}Enables the tweaking of character age behavior."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002100}Age Tweaks" + "*", GroupOrder = 1)]
        public bool AgeTweaksEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_002101}Become Infant Age", 0, 125, "0 Years", RequireRestart = false, Order = 2, 
            HintText = "{=BT_Settings_002101_Desc}Native: 3. Must be less than Become Child Age."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002100}Age Tweaks" + "*")]
        public int BecomeInfantAge { get; set; } = 3;

        [SettingPropertyInteger("{=BT_Settings_002102}Become Child Age", 0, 125, "0 Years", RequireRestart = false, Order = 3, 
            HintText = "{=BT_Settings_002102_Desc}Native: 6. Must be less than Become Teenager Age."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002100}Age Tweaks" + "*")]
        public int BecomeChildAge { get; set; } = 6;

        [SettingPropertyInteger("{=BT_Settings_002103}Become Teenager Age", 0, 125, "0 Years", RequireRestart = false, Order = 4, 
            HintText = "{=BT_Settings_002103_Desc}Native: 14. Must be less than Hero Comes Of Age."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002100}Age Tweaks" + "*")]
        public int BecomeTeenagerAge { get; set; } = 14;

        [SettingPropertyInteger("{=BT_Settings_002104}Hero Comes Of Age", 0, 125, "0 Years", RequireRestart = false, Order = 5, 
            HintText = "{=BT_Settings_002104_Desc}Native: 18. Must be less than Become Old Age."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002100}Age Tweaks" + "*")]
        public int HeroComesOfAge { get; set; } = 18;

        [SettingPropertyInteger("{=BT_Settings_002105}Become Old Age", 0, 125, "0 Years", RequireRestart = false, Order = 6, 
            HintText = "{=BT_Settings_002105_Desc}Native: 47. Must be less than Max Age."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002100}Age Tweaks" + "*")]
        public int BecomeOldAge { get; set; } = 47;

        [SettingPropertyInteger("{=BT_Settings_002106}Max Age", 0, 125, "0 Years", RequireRestart = false, Order = 7, 
            HintText = "{=BT_Settings_002106_Desc}Native: 125."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002100}Age Tweaks" + "*")]
        public int MaxAge { get; set; } = 125;

        #endregion

        //#region Character Tweaks - Attribute Focus Point Tweaks

/*
        [SettingPropertyBool("{=BT_Settings_002200}Attribute-Focus Points Tweaks" + "*", Order = 1, RequireRestart = true, IsToggle = true, 
            HintText = "{=BT_Settings_002200_Desc}Changes the values used to calculate how many Attribute and Focus points Heroes gain."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002200}Attribute-Focus Points Tweaks" + "*", GroupOrder = 2)]
        public bool AttributeFocusPointTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_002201}Levels per Attribute Point", 1, 5, "0 Level", Order = 2, RequireRestart = false, 
            HintText = "{=BT_Settings_002201_Desc}Native value is 4. How many levels you need to gain to receive an attribute point."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002200}Attribute-Focus Points Tweaks" + "*")]
        public int AttributePointRequiredLevel { get; set; } = 4;

        [SettingPropertyInteger("{=BT_Settings_002202}Focus Point Per Level", 1, 5, "0 Points", Order = 3, RequireRestart = false, 
            HintText = "{=BT_Settings_002202_Desc}Native value is 1. This is the amount of focus points earned per level."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002200}Attribute-Focus Points Tweaks" + "*")]
        public int FocusPointsPerLevel { get; set; } = 1;*/

        //#endregion

        #region Character Tweaks - Hero Skill Multiplier Tweaks

        [SettingPropertyBool("{=BT_Settings_002300}Hero Skill Experience" + "*", Order = 1, IsToggle = true, RequireRestart = true, 
            HintText = "{=BT_Settings_002300_Desc}Enable bonuses to the skill experience your hero and companions members gain."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002300}Hero Skill Experience" + "*", GroupOrder = 3)]
        public bool SkillExperienceMultipliersEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_002301}Player Skill Experience", Order = 2, RequireRestart = false, IsToggle = true, 
            HintText = "{=BT_Settings_002301_Desc}Applies a modifier to the amount of experience recieved for skills. Affects the player only. 100% = No Bonus."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002300}Hero Skill Experience" + "*/" + "{=BT_Settings_002301}Player Skill Experience", GroupOrder = 1)]
        public bool HeroSkillExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_002302}Player Skill Experience Amount", 1f, 5f, "0%", RequireRestart = false, 
            HintText = "{=BT_Settings_002302_Desc}Applies a modifier to the amount of experience recieved for skills. Affects the player only. 100% = No Bonus."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002300}Hero Skill Experience" + "*/" + "{=BT_Settings_002301}Player Skill Experience")]
        public float HeroSkillExperienceMultiplier { get; set; } = 1f;

        [SettingPropertyBool("{=BT_Settings_002303}Companion Skill Experience", Order = 3, RequireRestart = false, IsToggle = true, 
            HintText = "{=BT_Settings_002303_Desc}Applies a modifier to the amount of experience recieved for skills. Affects Compaions only. 100% = No Bonus."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002300}Hero Skill Experience" + "*/" + "{=BT_Settings_002303}Companion Skill Experience", GroupOrder = 2)]
        public bool CompanionSkillExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_002304}Companion Skill Experience Amount", 1f, 20f, "0%", RequireRestart = false, 
            HintText = "{=BT_Settings_002304_Desc}Applies a modifier to the amount of experience recieved for skills. Affects the Companion only. 100% = No Bonus."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002300}Hero Skill Experience" + "*/" + "{=BT_Settings_002303}Companion Skill Experience")]
        public float CompanionSkillExperienceMultiplier { get; set; } = 1f;


        #region Character Tweaks - Hero Skill Multiplier Tweaks - Enable Per-Skill Bonuses

        [SettingPropertyBool("{=BT_Settings_002305}Per Skill Bonuses", Order = 4, RequireRestart = false, IsToggle = true, 
            HintText = "{=BT_Settings_002305_Desc}Modifies the amount of experience recieved for specific skills before applying global experience modifier. Affects the player and companions only. 1.0 = No Bonus. 1.1 = 10%, etc."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002300}Hero Skill Experience" + "*/" + "{=BT_Settings_002305}Per-Skill Bonuses", GroupOrder = 3)]
        public bool PerSkillBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_002306}Per-Skill Experience Amount: Engineering", 1f, 10f, "0%", RequireRestart = false, 
            HintText = "{=BT_Settings_002306_Desc}Modifies the amount of Engineering experience recieved."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002300}Hero Skill Experience" + "*/" + "{=BT_Settings_002305}Per-Skill Bonuses")]
        public float SkillBonusEngineering { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=BT_Settings_002307}Per-Skill Experience Amount: Leadership", 1f, 10f, "0%", RequireRestart = false, 
            HintText = "{=BT_Settings_002307_Desc}Modifies the amount of Leadership experience recieved."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002300}Hero Skill Experience" + "*/" + "{=BT_Settings_002305}Per-Skill Bonuses")]
        public float SkillBonusLeadership { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=BT_Settings_002308}Per-Skill Experience Amount: Medicine", 1f, 10f, "0%", RequireRestart = false, 
            HintText = "{=BT_Settings_002308_Desc}Modifies the amount of Medicine experience recieved."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002300}Hero Skill Experience" + "*/" + "{=BT_Settings_002305}Per-Skill Bonuses")]
        public float SkillBonusMedicine { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=BT_Settings_002309}Per-Skill Experience Amount: Riding", 1f, 10f, "0%", RequireRestart = false, 
            HintText = "{=BT_Settings_002309_Desc}Modifies the amount of Riding experience recieved."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002300}Hero Skill Experience" + "*/" + "{=BT_Settings_002305}Per-Skill Bonuses")]
        public float SkillBonusRiding { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=BT_Settings_002310}Per-Skill Experience Amount: Roguery", 1f, 10f, "0%", RequireRestart = false, 
            HintText = "{=BT_Settings_002310_Desc}Modifies the amount of Roguery experience recieved."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002300}Hero Skill Experience" + "*/" + "{=BT_Settings_002305}Per-Skill Bonuses")]
        public float SkillBonusRoguery { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=BT_Settings_002311}Per-Skill Experience Amount: Scouting", 1f, 10f, "0%", RequireRestart = false, 
            HintText = "{=BT_Settings_002311_Desc}Modifies the amount of Scouting experience recieved."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002300}Hero Skill Experience" + "*/" + "{=BT_Settings_002305}Per-Skill Bonuses")]
        public float SkillBonusScouting { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=BT_Settings_002312}Per-Skill Experience Amount: Trade", 1f, 10f, "0%", RequireRestart = false, 
            HintText = "{=BT_Settings_002312_Desc}Modifies the amount of Trade experience recieved."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002300}Hero Skill Experience" + "*/" + "{=BT_Settings_002305}Per-Skill Bonuses")]
        public float SkillBonusTrade { get; set; } = 1f;

        [SettingPropertyFloatingInteger("{=BT_Settings_002313}Per-Skill Experience Amount: Smithing", 1f, 10f, "0%", RequireRestart = false, 
            HintText = "{=BT_Settings_002313_Desc}Modifies the amount of Smithing experience recieved."), 
            SettingPropertyGroup("{=BT_Settings_002000}Character Tweaks" + "/" + "{=BT_Settings_002300}Hero Skill Experience" + "*/" + "{=BT_Settings_002305}Per-Skill Bonuses")]
        public float SkillBonusSmithing { get; set; } = 1f;

        #endregion

        #endregion

        #endregion


        #region Crafting Tweaks #6

        #region Crafting Tweaks - Crafting Stamina Tweaks
        [SettingPropertyBool("{=BT_Settings_004100}Crafting Stamina" + "*", Order = 1, RequireRestart = true, IsToggle = true, 
            HintText = "{=BT_Settings_004100_Desc}Enables tweaks which affect crafting stamina."), 
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004100}Crafting Stamina" + "*", GroupOrder = 1)]
        public bool CraftingStaminaTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_004101}Max Crafting Stamina", 100, 1000, "0 Stamina", Order = 2, RequireRestart = false, 
            HintText = "{=BT_Settings_004101_Desc}Native value is 400. Sets the maximum crafting stamina value."), 
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004100}Crafting Stamina" + "*")]
        public int MaxCraftingStamina { get; set; } = 400;

        [SettingPropertyInteger("{=BT_Settings_004102}Crafting Stamina Gain", 0, 100, "0 Stamina/h", Order = 3, RequireRestart = false, 
            HintText = "{=BT_Settings_004102_Desc}Native value is 5. You gain this amount of crafting stamina per hour."), 
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004100}Crafting Stamina" + "*")]
        public int CraftingStaminaGainAmount { get; set; } = 5;

        [SettingPropertyFloatingInteger("{=BT_Settings_004103}Crafting Stamina Gain Outside Settlement", 0f, 1f, "0%", Order = 4, RequireRestart = false, 
            HintText = "{=BT_Settings_004103_Desc}Native value is 0%. In % of Crafting Stamina Gain. In native, you do not gain crafting stamina if you are not resting inside a settlement."), 
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004100}Crafting Stamina" + "*")]
        public float CraftingStaminaGainOutsideSettlementMultiplier { get; set; } = 0f;

        [SettingPropertyBool("{=BT_Settings_004104}Ignore Crafting Stamina", Order = 5, RequireRestart = false, 
            HintText = "{=BT_Settings_004104_Desc}Native value is false. This disables crafting stamina completely. You will still lose crafting stamina when you craft, but you will still be able to craft when you hit zero."), 
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004100}Crafting Stamina" + "*")]
        public bool IgnoreCraftingStamina { get; set; } = false;


        #endregion

        #region Crafting Tweaks - Smelting

        [SettingPropertyBool("{=BT_Settings_004200}Smelting" + "*", Order = 1, RequireRestart = true, IsToggle = true, 
            HintText = "{=BT_Settings_004200_Desc}Enables tweaks which affect smelting of weapons."), 
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004200}Smelting" + "*", GroupOrder = 2)]
        public bool SmeltingTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_004201}Hide Locked Weapons in Smelting Menu", Order = 1, RequireRestart = false, 
            HintText = "{=BT_Settings_004201_Desc}Native value is false. Prevent weapons that you have locked in your inventory from showing up in the smelting list to prevent accidental smelting."), 
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004200}Smelting" + "*")]
        public bool PreventSmeltingLockedItems { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_004202}Enable Unlocking Parts From Smelted Weapons", Order = 2, RequireRestart = false, 
            HintText = "{=BT_Settings_004202_Desc}Native value is false. Unlock the parts that a weapon is made of when you smelt it."), 
            SettingPropertyGroup("{=BT_Settings_004000}Crafting Tweaks" + "/" + "{=BT_Settings_004200}Smelting" + "*")]
        public bool AutoLearnSmeltedParts { get; set; } = false;

        #endregion

        #endregion

        #region Kingdom Tweaks #7


        #region Kingdom Tweaks - Lord Bartering

        [SettingPropertyBool("{=BT_Settings_005100}Lord Bartering" + "*", Order = 1, RequireRestart = true, IsToggle = true, 
            HintText = "{=BT_Settings_005100_Desc}Enables tweaks which affect bartering (Marriage, Factions Joining You, etc.)"), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005100}Lord Bartering" + "*", GroupOrder = 1)]
        public bool BarterablesTweaksEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_005101}Faction Joining Barter Adjustment", 0.01f, 2f, "0%", Order = 2, RequireRestart = false, 
            HintText = "{=BT_Settings_005101_Desc}Adjust the % cost of swaying a faction to join your kingdom. Native value is 100% (no change)."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005100}Lord Bartering" + "*")]
        public float BarterablesJoinKingdomAsClanAdjustment { get; set; } = 1;

        [SettingPropertyBool("{=BT_Settings_005102}Relationship favored Faction Joining Barter", Order = 3, RequireRestart = false, 
            HintText = "{=BT_Settings_005102_Desc}An alternate formula for calculating cost of swaying a faction to join your kingdom, with more emphasis on relationsip. [The higher your relationship to the lord, the cheaper the barter]."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005100}Lord Bartering" + "*")]
        public bool BarterablesJoinKingdomAsClanAltFormulaEnabled { get; set; } = false;

        #endregion




        #region Kingdom Tweaks - Balancing Tweaks

        [SettingPropertyBool("{=BT_Settings_005200}Faction Balancing" + "*", Order = 1, RequireRestart = true, IsToggle = true, 
            HintText = "{=BT_Settings_005200_Desc}Enables tweaks which affect the balancing of kingdoms."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*", GroupOrder = 2)]
        public bool KingdomBalanceStrengthEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms", Order = 2, RequireRestart = false, IsToggle = true, 
            HintText = "{=BT_Settings_005201_Desc}Enables tweaks which affect the balancing of kingdoms in vanilla game."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms", GroupOrder = 1)]
        public bool KingdomBalanceStrengthVanEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_005202}Vlandia Balancing Strength", -0.5f, 0.5f, "0%", Order = 9, RequireRestart = false, 
            HintText = "{=BT_Settings_005202_Desc}Balancing strength for vlandia. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float VlandiaBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005203}Sturgia Balancing Strength", -0.5f, 0.5f, "0%", Order = 8, RequireRestart = false, 
            HintText = "{=BT_Settings_005203_Desc}Balancing strength for sturgia. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float SturgiaBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005204}Battania Balancing Strength", -0.5f, 0.5f, "0%", Order = 2, RequireRestart = false, 
            HintText = "{=BT_Settings_005204_Desc}Balancing strength for battania. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float BattaniaBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005205}Northern Empire Balancing Strength", -0.5f, 0.5f, "0%", Order = 5, RequireRestart = false, 
            HintText = "{=BT_Settings_005205_Desc}Balancing strength for northern empire. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float Empire_N_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005206}Southern Empire Balancing Strength", -0.5f, 0.5f, "0%", Order = 6, RequireRestart = false, 
            HintText = "{=BT_Settings_005206_Desc}Balancing strength for southern empire. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float Empire_S_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005207}Western Empire Balancing Strength", -0.5f, 0.5f, "0%", Order = 7, RequireRestart = false, 
            HintText = "{=BT_Settings_005207_Desc}Balancing strength for western empire. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float Empire_W_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005208}Aserai Balancing Strength", -0.5f, 0.5f, "0%", Order = 1, RequireRestart = false, 
            HintText = "{=BT_Settings_005208_Desc}Balancing strength for aserai. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float AseraiBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005209}Khuzait Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false, 
            HintText = "{=BT_Settings_005209_Desc}Balancing strength for khuzait. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float KhuzaitBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005210}Player Kingdom Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false, 
            HintText = "{=BT_Settings_005210_Desc}Balancing strength for player kingdom. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005201}Balancing Modifiers For Vanilla Kingdoms")]
        public float PlayerBoost { get; set; } = 0.00f;



        [SettingPropertyBool("{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms", Order = 3, RequireRestart = false, IsToggle = true, 
            HintText = "{=BT_Settings_005211_Desc}Enables tweaks which affect the balancing of kingdoms in the mod Calradia Expanded Kingdoms."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms", GroupOrder = 2)]
        public bool KingdomBalanceStrengthCEKEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_005212}Nordlings Balancing Strength", -0.5f, 0.5f, "0%", Order = 9, RequireRestart = false, 
            HintText = "{=BT_Settings_005212_Desc}Balancing strength for nordlings. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float NordlingsBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005213}Vagir Balancing Strength", -0.5f, 0.5f, "0%", Order = 8, RequireRestart = false, 
            HintText = "{=BT_Settings_005213_Desc}Balancing strength for vagir. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float VagirBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005214}Royalist Vlandia Balancing Strength", -0.5f, 0.5f, "0%", Order = 2, RequireRestart = false, 
            HintText = "{=BT_Settings_005214_Desc}Balancing strength for royalist vlandia. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float RoyalistVlandiaBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005215}Apolssaly Balancing Strength", -0.5f, 0.5f, "0%", Order = 5, RequireRestart = false, 
            HintText = "{=BT_Settings_005215_Desc}Balancing strength for apolssaly. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float ApolssalyBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005216}Lyrion Balancing Strength", -0.5f, 0.5f, "0%", Order = 6, RequireRestart = false, 
            HintText = "{=BT_Settings_005216_Desc}Balancing strength for lyrion. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float LyrionBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005217}Khergit Balancing Strength", -0.5f, 0.5f, "0%", Order = 7, RequireRestart = false, 
            HintText = "{=BT_Settings_005217_Desc}Balancing strength for khergit. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float RebelKhuzaitBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005218}Paleician Balancing Strength", -0.5f, 0.5f, "0%", Order = 1, RequireRestart = false, 
            HintText = "{=BT_Settings_005218_Desc}Balancing strength for paleician. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float PaleicianBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005219}Ariorum Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false, 
            HintText = "{=BT_Settings_005219_Desc}Balancing strength for ariorum. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float AriorumBoost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005220}Calradian Empire Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false, 
            HintText = "{=BT_Settings_005220_Desc}Balancing strength for calradian empire. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Empire_S_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005221}Dryatican Republic Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false, 
            HintText = "{=BT_Settings_005221_Desc}Balancing strength for dryatican republic. Boosts or reduces the enabled balancing tweaks."),
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Empire_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005222}Battania Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false,
            HintText = "{=BT_Settings_005222_Desc}Balancing strength for battania. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Battania_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005223}Cortanian Vlandia Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false, 
            HintText = "{=BT_Settings_005223_Desc}Balancing strength for cortanian vlandia. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Vlandia_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005224}Sturgia Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false, 
            HintText = "{=BT_Settings_005224_Desc}Balancing strength for sturgia. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Sturgia_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005225}Khuzait Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false, 
            HintText = "{=BT_Settings_005225_Desc}Balancing strength for khuzait. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Khuzait_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005226}Aserai Balancing Strength", -0.5f, 0.5f, "0%", Order = 4, RequireRestart = false, 
            HintText = "{=BT_Settings_005226_Desc}Balancing strength for aserai. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Aserai_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005227}Western Empire Balancing Strength", -0.5f, 0.5f, "0%", Order = 7, RequireRestart = false, 
            HintText = "{=BT_Settings_005227_Desc}Balancing strength for western empire. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Empire_W_CEK_Boost { get; set; } = 0.00f;

        [SettingPropertyFloatingInteger("{=BT_Settings_005228}Player Kingdom Balancing Strength", -0.5f, 0.5f, "0%", Order = 7, RequireRestart = false, 
            HintText = "{=BT_Settings_005228_Desc}Balancing strength for player kingdom. Boosts or reduces the enabled balancing tweaks."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*/" + "{=BT_Settings_005211}Balancing Modifiers For Calradia Expanded Kingdoms")]
        public float Player_CEK_Boost { get; set; } = 0.00f;




        [SettingPropertyBool("{=BT_Settings_005229}Party Sizes Balancing", Order = 4, RequireRestart = false, 
            HintText = "{=BT_Settings_005229_Desc}Modifier for max party sizes (1.0 x Balancing Strength)."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*")]
        public bool BalancingPartySizeTweaksEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_005230}Party Count Limit Balancing", Order = 5, RequireRestart = false, 
            HintText = "{=BT_Settings_005230_Desc}Modifier for max party limit (1.0 x Balancing Strength)."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*")]
        public bool BalancingPartyLimitTweaksEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_005231}Village Food Production Balancing", Order = 6, RequireRestart = false,
            HintText = "{=BT_Settings_005231_Desc}Modifier for daily production of food goods in villages (1.0 x Balancing Strength)."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*")]
        public bool BalancingFoodTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_005232}Faster Recruitment Balancing", Order = 7, RequireRestart = false, 
            HintText = "{=BT_Settings_005232_Desc}Flat % bonus chance of new recruits to spawn in settlements (0.75 x Balancing Strength)."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*")]
        public bool BalancingTimeRecruitsTweaksEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_005233}Taxation Efficiency Balancing", Order = 8, RequireRestart = false, 
            HintText = "{=BT_Settings_005233_Desc}Modifier for tax income from prosperity in towns and castles and trade tax income in villages (1.25 x Balancing Strength)."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*")]
        public bool BalancingTaxTweaksEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_005234}Wage costs Balancing", Order = 9, RequireRestart = false, 
            HintText = "{=BT_Settings_005234_Desc}Modifier for troop and garrison wages (1.0 x Balancing Strength)."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*")]
        public bool BalancingWagesTweaksEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_005235}Quality of Recruitment Balancing", Order = 10, RequireRestart = false, 
            HintText = "{=BT_Settings_005235_Desc}Increases the chance for upgraded recruits (1.0 x Balancing Strength). No effect if balancing strength is lower than 0% (no decrease in chance for upgrades)."), 
            SettingPropertyGroup("{=BT_Settings_005000}Kingdom Tweaks" + "/" + "{=BT_Settings_005200}Faction Balancing" + "*")]
        public bool BalancingUpgradeTroopsTweaksEnabled { get; set; } = false;

        #endregion



        #endregion

        #region Party Tweaks #8

        #region Party Tweaks - Cohesion Tweaks

        [SettingPropertyBool("{=BT_Settings_006100}Cohesion Tweaks" + "*", Order = 1, RequireRestart = true, IsToggle = true, 
            HintText = "{=BT_Settings_006100_Desc}Enables tweaks affecting cohesion."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006100}Cohesion Tweaks" + "*", GroupOrder = 1)]
        public bool BTCohesionTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_006101}Cohesion Degradation Factor", 0f, 1f, "0%", Order = 2, RequireRestart = false, 
            HintText = "{=BT_Settings_006101_Desc}Modifier to how much cohesion should degrade over time. Vanilla is 100%, 0% is disabled."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006100}Cohesion Tweaks" + "*")]
        public float BTCohesionTweakv2 { get; set; } = 1f;

        [SettingPropertyBool("{=BT_Settings_006102}All-Clan Armies Lose No Cohesion", Order = 3, RequireRestart = false, 
            HintText = "{=BT_Settings_006102_Desc}Armies composed of only clan parties lose no cohesion."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006100}Cohesion Tweaks" + "*")]
        public bool ClanArmyLosesNoCohesionEnabled { get; set; } = false;

        #endregion

        #region Party Tweaks - Caravan Tweaks

        [SettingPropertyBool("{=BT_Settings_006200}Player Caravan Party Size" + "*", Order = 1, RequireRestart = true, IsToggle = true, 
            HintText = "{=BT_Settings_006200_Desc}Applies a configured value to your caravan party size."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006200}Player Caravan Party Size" + "*", GroupOrder = 2)]
        public bool PlayerCaravanPartySizeTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("{=BT_Settings_006201}Player Caravan Party Size Amount", 30, 100, "0 Troops", Order = 2, 
            HintText = "{=BT_Settings_006201_Desc}Native: 30. Be aware that bigger parties are also slower parties."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006200}Player Caravan Party Size" + "*")]
        public int PlayerCaravanPartySize { get; set; } = 30;

        #endregion

        #region Party Tweaks - Party Size Tweaks

        [SettingPropertyBool("{=BT_Settings_006300}Party Size" + "*", Order = 1, RequireRestart = true, IsToggle = true,
            HintText = "{=BT_Settings_006300_Desc}Applies a bonues to you and AI lord's party size based on leadership and steward skills."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "*", GroupOrder = 3)]
        public bool PartySizeTweakEnabled { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_006301}Leadership Bonus", Order = 2, IsToggle = true, RequireRestart = false, 
            HintText = "{=BT_Settings_006301_Desc}Applies a bonus equal to the set percentage of your leadership skill to your party size."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "*/" + "{=BT_Settings_006301}Leadership Bonus", GroupOrder = 1)]
        public bool LeadershipPartySizeBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_006302}Leadership Bonus Player", 0f, 2f, "0%", Order = 3, RequireRestart = false,
            HintText = "{=BT_Settings_006302_Desc}Applies a bonus equal to the set percentage of your leadership skill to your party size."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "*/" + "{=BT_Settings_006301}Leadership Bonus")]
        public float LeadershipPartySizeBonus { get; set; } = 0.0f;

        [SettingPropertyBool("{=BT_Settings_006303}Steward Bonus", Order = 4, IsToggle = true, RequireRestart = false, 
            HintText = "{=BT_Settings_006303_Desc}Applies a bonus equal to the set percentage of your steward skill to your party size."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "*/" + "{=BT_Settings_006303}Steward Bonus", GroupOrder = 2)]
        public bool StewardPartySizeBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_006304}Steward Bonus Player", 0f, 2f, "0%", Order = 5, RequireRestart = false, 
            HintText = "{=BT_Settings_006304_Desc}Applies a bonus equal to the set percentage of your steward skill to your party size."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "*/" + "{=BT_Settings_006303}Steward Bonus")]
        public float StewardPartySizeBonus { get; set; } = 0f;

        [SettingPropertyFloatingInteger("{=BT_Settings_006305}Party Size Relation Player-AI", 0f, 2f, "0%", Order = 6, RequireRestart = false, 
            HintText = "{=BT_Settings_006305_Desc}The percentage of the party size bonus set for the player to also apply for ai lords. 0% results in no bonus for ai. You may also want to increase food production amounts (Village Production, bigger demand)."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006300}Party Size" + "*")]
        public float PartySizeTweakAIFactor { get; set; } = 0f;

        #endregion

        #region Party Tweaks - Party Wage Tweaks

        [SettingPropertyBool("{=BT_Settings_006400}Wages" + "*", Order = 1, RequireRestart = true, IsToggle = true, 
            HintText = "{=BT_Settings_006400_Desc}Allows you to reduce/increase wages for various groups."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006400}Wages" + "*", GroupOrder = 4)]
        public bool PartyWageTweaksEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("{=BT_Settings_006401}Party Wage Adjustment", .05f, 5f, "0%", Order = 2, RequireRestart = false, 
            HintText = "{=BT_Settings_006401_Desc}Adjusts party wages to a % of native value. Native is 100%."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006400}Wages" + "*")]
        public float PartyWagePercent { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=BT_Settings_006402}Garrison Wage Adjustment", .05f, 5f, "0%", Order = 3, RequireRestart = false, 
            HintText = "{=BT_Settings_006402_Desc}Adjusts garrison wages to a % of native value. Native is 100%."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006400}Wages" + "*")]
        public float GarrisonWagePercent { get; set; } = 1.0f;

        [SettingPropertyBool("{=BT_Settings_006403}Also Apply Wage Tweaks to Your Faction", Order = 4, RequireRestart = false, 
            HintText = "{=BT_Settings_006403_Desc}Applies the wage modifiers to your {=KTMCM_CClan}Clan/faction parties as well."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006400}Wages" + "*")]
        public bool ApplyWageTweakToFaction { get; set; } = false;

        [SettingPropertyBool("{=BT_Settings_006404}Also Apply Wage Tweaks to AI Lords", Order = 5, RequireRestart = false, 
            HintText = "{=BT_Settings_006404_Desc}Applies the wage modifiers to ai lord parties as well."), 
            SettingPropertyGroup("{=BT_Settings_006000}Party Tweaks" + "/" + "{=BT_Settings_006400}Wages" + "*")]
        public bool ApplyWageTweakToAI { get; set; } = false;

        #endregion


        #endregion



        #region Miscellaneous #11

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

        /* Disable in 1.5.7.2 until we understand changes to the main quest.
        [SettingPropertyBool("Enable Auto-Extension of the 'Stop the Conspiracy' Quest", RequireRestart = false, HintText = "Automatically extends the timer of the 'Stop the Conspiracy' quest as TW hasn't finished it yet.")]
        public bool TweakedConspiracyQuestTimerEnabled { get; set; } = true;
        */
        #endregion





        public override IDictionary<string, Func<BaseSettings>> GetAvailablePresets()
        {
            var basePresets = base.GetAvailablePresets(); // include the 'Default' preset that MCM provides

            basePresets.Add("native", () => new MCMSettings()
            {
                Debug = false,
                LoadMCMConfigFile = false,
                LogToFile = false,
                MCMItemModifiers = false,

                MCMBodyArmorModifiers = true,
                BodyArmorWeightModifiers = true,
                BodyArmorTier1WeightMultiplier = 1.0f,
                BodyArmorTier2WeightMultiplier = 1.0f,
                BodyArmorTier3WeightMultiplier = 1.0f,
                BodyArmorTier4WeightMultiplier = 1.0f,
                BodyArmorTier5WeightMultiplier = 1.0f,
                BodyArmorTier6WeightMultiplier = 1.0f,
                BodyArmorValueModifiers = true,
                BodyArmorTier1PriceMultiplier = 1.0f,
                BodyArmorTier2PriceMultiplier = 1.0f,
                BodyArmorTier3PriceMultiplier = 1.0f,
                BodyArmorTier4PriceMultiplier = 1.0f,
                BodyArmorTier5PriceMultiplier = 1.0f,
                BodyArmorTier6PriceMultiplier = 1.0f,

                MCMBowModifiers = true,
                BowWeightModifiers = true,
                BowTier1WeightMultiplier = 1.0f,
                BowTier2WeightMultiplier = 1.0f,
                BowTier3WeightMultiplier = 1.0f,
                BowTier4WeightMultiplier = 1.0f,
                BowTier5WeightMultiplier = 1.0f,
                BowTier6WeightMultiplier = 1.0f,
                BowValueModifiers = true,
                BowTier1PriceMultiplier = 1.0f,
                BowTier2PriceMultiplier = 1.0f,
                BowTier3PriceMultiplier = 1.0f,
                BowTier4PriceMultiplier = 1.0f,
                BowTier5PriceMultiplier = 1.0f,
                BowTier6PriceMultiplier = 1.0f,

                MCMCapeModifiers = true,
                CapeWeightModifiers = true,
                CapeTier1WeightMultiplier = 1.0f,
                CapeTier2WeightMultiplier = 1.0f,
                CapeTier3WeightMultiplier = 1.0f,
                CapeTier4WeightMultiplier = 1.0f,
                CapeTier5WeightMultiplier = 1.0f,
                CapeTier6WeightMultiplier = 1.0f,
                CapeValueModifiers = true,
                CapeTier1PriceMultiplier = 1.0f,
                CapeTier2PriceMultiplier = 1.0f,
                CapeTier3PriceMultiplier = 1.0f,
                CapeTier4PriceMultiplier = 1.0f,
                CapeTier5PriceMultiplier = 1.0f,
                CapeTier6PriceMultiplier = 1.0f,

                MCMChestArmorModifiers = true,
                ChestArmorWeightModifiers = true,
                ChestArmorTier1WeightMultiplier = 1.0f,
                ChestArmorTier2WeightMultiplier = 1.0f,
                ChestArmorTier3WeightMultiplier = 1.0f,
                ChestArmorTier4WeightMultiplier = 1.0f,
                ChestArmorTier5WeightMultiplier = 1.0f,
                ChestArmorTier6WeightMultiplier = 1.0f,
                ChestArmorValueModifiers = true,
                ChestArmorTier1PriceMultiplier = 1.0f,
                ChestArmorTier2PriceMultiplier = 1.0f,
                ChestArmorTier3PriceMultiplier = 1.0f,
                ChestArmorTier4PriceMultiplier = 1.0f,
                ChestArmorTier5PriceMultiplier = 1.0f,
                ChestArmorTier6PriceMultiplier = 1.0f,

                MCMCrossbowModifiers = true,
                CrossbowWeightModifiers = true,
                CrossbowTier1WeightMultiplier = 1.0f,
                CrossbowTier2WeightMultiplier = 1.0f,
                CrossbowTier3WeightMultiplier = 1.0f,
                CrossbowTier4WeightMultiplier = 1.0f,
                CrossbowTier5WeightMultiplier = 1.0f,
                CrossbowTier6WeightMultiplier = 1.0f,
                CrossbowValueModifiers = true,
                CrossbowTier1PriceMultiplier = 1.0f,
                CrossbowTier2PriceMultiplier = 1.0f,
                CrossbowTier3PriceMultiplier = 1.0f,
                CrossbowTier4PriceMultiplier = 1.0f,
                CrossbowTier5PriceMultiplier = 1.0f,
                CrossbowTier6PriceMultiplier = 1.0f,

                MCMHandArmorModifiers = true,
                HandArmorWeightModifiers = true,
                HandArmorTier1WeightMultiplier = 1.0f,
                HandArmorTier2WeightMultiplier = 1.0f,
                HandArmorTier3WeightMultiplier = 1.0f,
                HandArmorTier4WeightMultiplier = 1.0f,
                HandArmorTier5WeightMultiplier = 1.0f,
                HandArmorTier6WeightMultiplier = 1.0f,
                HandArmorValueModifiers = true,
                HandArmorTier1PriceMultiplier = 1.0f,
                HandArmorTier2PriceMultiplier = 1.0f,
                HandArmorTier3PriceMultiplier = 1.0f,
                HandArmorTier4PriceMultiplier = 1.0f,
                HandArmorTier5PriceMultiplier = 1.0f,
                HandArmorTier6PriceMultiplier = 1.0f,

                MCMHeadArmorModifiers = true,
                HeadArmorWeightModifiers = true,
                HeadArmorTier1WeightMultiplier = 1.0f,
                HeadArmorTier2WeightMultiplier = 1.0f,
                HeadArmorTier3WeightMultiplier = 1.0f,
                HeadArmorTier4WeightMultiplier = 1.0f,
                HeadArmorTier5WeightMultiplier = 1.0f,
                HeadArmorTier6WeightMultiplier = 1.0f,
                HeadArmorValueModifiers = true,
                HeadArmorTier1PriceMultiplier = 1.0f,
                HeadArmorTier2PriceMultiplier = 1.0f,
                HeadArmorTier3PriceMultiplier = 1.0f,
                HeadArmorTier4PriceMultiplier = 1.0f,
                HeadArmorTier5PriceMultiplier = 1.0f,
                HeadArmorTier6PriceMultiplier = 1.0f,

                MCMHorseModifiers = true,
                HorseWeightModifiers = true,
                HorseTier1WeightMultiplier = 1.0f,
                HorseTier2WeightMultiplier = 1.0f,
                HorseTier3WeightMultiplier = 1.0f,
                HorseTier4WeightMultiplier = 1.0f,
                HorseTier5WeightMultiplier = 1.0f,
                HorseTier6WeightMultiplier = 1.0f,
                HorseValueModifiers = true,
                HorseTier1PriceMultiplier = 1.0f,
                HorseTier2PriceMultiplier = 1.0f,
                HorseTier3PriceMultiplier = 1.0f,
                HorseTier4PriceMultiplier = 1.0f,
                HorseTier5PriceMultiplier = 1.0f,
                HorseTier6PriceMultiplier = 1.0f,

                MCMHorseHarnessModifiers = true,
                HorseHarnessWeightModifiers = true,
                HorseHarnessTier1WeightMultiplier = 1.0f,
                HorseHarnessTier2WeightMultiplier = 1.0f,
                HorseHarnessTier3WeightMultiplier = 1.0f,
                HorseHarnessTier4WeightMultiplier = 1.0f,
                HorseHarnessTier5WeightMultiplier = 1.0f,
                HorseHarnessTier6WeightMultiplier = 1.0f,
                HorseHarnessValueModifiers = true,
                HorseHarnessTier1PriceMultiplier = 1.0f,
                HorseHarnessTier2PriceMultiplier = 1.0f,
                HorseHarnessTier3PriceMultiplier = 1.0f,
                HorseHarnessTier4PriceMultiplier = 1.0f,
                HorseHarnessTier5PriceMultiplier = 1.0f,
                HorseHarnessTier6PriceMultiplier = 1.0f,

                MCMLegArmorModifiers = true,
                LegArmorWeightModifiers = true,
                LegArmorTier1WeightMultiplier = 1.0f,
                LegArmorTier2WeightMultiplier = 1.0f,
                LegArmorTier3WeightMultiplier = 1.0f,
                LegArmorTier4WeightMultiplier = 1.0f,
                LegArmorTier5WeightMultiplier = 1.0f,
                LegArmorTier6WeightMultiplier = 1.0f,
                LegArmorValueModifiers = true,
                LegArmorTier1PriceMultiplier = 1.0f,
                LegArmorTier2PriceMultiplier = 1.0f,
                LegArmorTier3PriceMultiplier = 1.0f,
                LegArmorTier4PriceMultiplier = 1.0f,
                LegArmorTier5PriceMultiplier = 1.0f,
                LegArmorTier6PriceMultiplier = 1.0f,

                MCMMusketModifiers = true,
                MusketWeightModifiers = true,
                MusketTier1WeightMultiplier = 1.0f,
                MusketTier2WeightMultiplier = 1.0f,
                MusketTier3WeightMultiplier = 1.0f,
                MusketTier4WeightMultiplier = 1.0f,
                MusketTier5WeightMultiplier = 1.0f,
                MusketTier6WeightMultiplier = 1.0f,
                MusketValueModifiers = true,
                MusketTier1PriceMultiplier = 1.0f,
                MusketTier2PriceMultiplier = 1.0f,
                MusketTier3PriceMultiplier = 1.0f,
                MusketTier4PriceMultiplier = 1.0f,
                MusketTier5PriceMultiplier = 1.0f,
                MusketTier6PriceMultiplier = 1.0f,


                MCMOneHandedWeaponModifiers = true,
                OneHandedWeaponWeightModifiers = true,
                OneHandedWeaponTier1WeightMultiplier = 1.0f,
                OneHandedWeaponTier2WeightMultiplier = 1.0f,
                OneHandedWeaponTier3WeightMultiplier = 1.0f,
                OneHandedWeaponTier4WeightMultiplier = 1.0f,
                OneHandedWeaponTier5WeightMultiplier = 1.0f,
                OneHandedWeaponTier6WeightMultiplier = 1.0f,
                OneHandedWeaponValueModifiers = true,
                OneHandedWeaponTier1PriceMultiplier = 1.0f,
                OneHandedWeaponTier2PriceMultiplier = 1.0f,
                OneHandedWeaponTier3PriceMultiplier = 1.0f,
                OneHandedWeaponTier4PriceMultiplier = 1.0f,
                OneHandedWeaponTier5PriceMultiplier = 1.0f,
                OneHandedWeaponTier6PriceMultiplier = 1.0f,

                MCMPistolModifiers = true,
                PistolWeightModifiers = true,
                PistolTier1WeightMultiplier = 1.0f,
                PistolTier2WeightMultiplier = 1.0f,
                PistolTier3WeightMultiplier = 1.0f,
                PistolTier4WeightMultiplier = 1.0f,
                PistolTier5WeightMultiplier = 1.0f,
                PistolTier6WeightMultiplier = 1.0f,
                PistolValueModifiers = true,
                PistolTier1PriceMultiplier = 1.0f,
                PistolTier2PriceMultiplier = 1.0f,
                PistolTier3PriceMultiplier = 1.0f,
                PistolTier4PriceMultiplier = 1.0f,
                PistolTier5PriceMultiplier = 1.0f,
                PistolTier6PriceMultiplier = 1.0f,

                MCMPolearmModifiers = true,
                PolearmWeightModifiers = true,
                PolearmTier1WeightMultiplier = 1.0f,
                PolearmTier2WeightMultiplier = 1.0f,
                PolearmTier3WeightMultiplier = 1.0f,
                PolearmTier4WeightMultiplier = 1.0f,
                PolearmTier5WeightMultiplier = 1.0f,
                PolearmTier6WeightMultiplier = 1.0f,
                PolearmValueModifiers = true,
                PolearmTier1PriceMultiplier = 1.0f,
                PolearmTier2PriceMultiplier = 1.0f,
                PolearmTier3PriceMultiplier = 1.0f,
                PolearmTier4PriceMultiplier = 1.0f,
                PolearmTier5PriceMultiplier = 1.0f,
                PolearmTier6PriceMultiplier = 1.0f,

                MCMShieldModifiers = true,
                ShieldWeightModifiers = true,
                ShieldTier1WeightMultiplier = 1.0f,
                ShieldTier2WeightMultiplier = 1.0f,
                ShieldTier3WeightMultiplier = 1.0f,
                ShieldTier4WeightMultiplier = 1.0f,
                ShieldTier5WeightMultiplier = 1.0f,
                ShieldTier6WeightMultiplier = 1.0f,
                ShieldValueModifiers = true,

                ShieldTier1PriceMultiplier = 1.0f,
                ShieldTier2PriceMultiplier = 1.0f,
                ShieldTier3PriceMultiplier = 1.0f,
                ShieldTier4PriceMultiplier = 1.0f,
                ShieldTier5PriceMultiplier = 1.0f,
                ShieldTier6PriceMultiplier = 1.0f,

                MCMTwoHandedWeaponModifiers = true,
                TwoHandedWeaponWeightModifiers = true,
                TwoHandedWeaponTier1WeightMultiplier = 1.0f,
                TwoHandedWeaponTier2WeightMultiplier = 1.0f,
                TwoHandedWeaponTier3WeightMultiplier = 1.0f,
                TwoHandedWeaponTier4WeightMultiplier = 1.0f,
                TwoHandedWeaponTier5WeightMultiplier = 1.0f,
                TwoHandedWeaponTier6WeightMultiplier = 1.0f,
                TwoHandedWeaponValueModifiers = true,
                TwoHandedWeaponTier1PriceMultiplier = 1.0f,
                TwoHandedWeaponTier2PriceMultiplier = 1.0f,
                TwoHandedWeaponTier3PriceMultiplier = 1.0f,
                TwoHandedWeaponTier4PriceMultiplier = 1.0f,
                TwoHandedWeaponTier5PriceMultiplier = 1.0f,
                TwoHandedWeaponTier6PriceMultiplier = 1.0f,


                MCMBattleRewardModifiers = true,
                BattleRewardsRelationShipGainModifiers = true,
                BattleRewardsRelationShipGainMultiplier = 1.0f,
                BattleRewardsRenownGainModifiers = true,
                BattleRewardsRenownGainMultiplier = 1.0f,
                BattleRewardsInfluenceGainModifiers = true,
                BattleRewardsInfluenceGainMultiplier = 1.0f,
                BattleRewardsMoraleGainModifiers = true,
                BattleRewardsMoraleGainMultiplier = 1.0f,
                BattleRewardsGoldLossModifiers = true,
                BattleRewardsGoldLossMultiplier = 1.0f,


                //MCMClanModifiers = true,
                ClanAdditionalPartyLimitEnabled = true,
                ClanPlayerPartiesLimitEnabled = true,
                ClanPlayerBasePartiesLimit = 1,
                ClanPlayerPartiesBonusPerClanTier = 0.5f,
                ClanAIPartiesLimitTweakEnabled = true,
                ClanAIBaseClanPartiesLimit  = 1,
                ClanAIPartiesBonusPerClanTier = 0.5f,
                ClanAIMinorClanPartiesLimitTweakEnabled = false,
                AICustomSpawnPartiesLimitTweakEnabled = false,
                BaseAICustomSpawnPartiesLimit = 1,
                ClanCompanionLimitEnabled = true,
                ClanAdditionalCompanionLimit = 0,

                MCMWorkShopModifiers = true,
                WorkShopBankruptcyModifiers = true,
                WorkShopBankruptcyValue = 3,
                WorkShopMaxWorkshopCountForPlayerModifiers = true,
                WorkShopMaxWorkshopCountForPlayerValue = 1,

                MCMCharacterDevlopmentModifiers = true,
                CharacterLevelsPerAttributeModifiers = true,
                CharacterLevelsPerAttributeValue = 4,
                CharacterFocusPerLevelModifiers = true,
                CharacterFocusPerLevelValue = 4,


                MCMPregnancyModifiers = true,
                PregnancyDurationModifiers = true,
                PregnancyDurationValue = 36,
                PregnancyLaborMortalityChanceModifiers = true,
                PregnancyLaborMortalityChanceValue = 0.015f,
                PregnancyFemaleOffspringChanceModifiers = true,
                PregnancyFemaleOffspringChanceValue = 0.01f,

                MCMSmithingModifiers = true,
                SmithingRefiningXpModifiers = true,
                SmithingRefiningXpValue = 1.0f,
                SmithingSmeltingXpModifiers = true,
                SmithingSmeltingXpValue = 1.0f,
                SmithingSmithingXpModifiers = true,
                SmithingSmithingXpValue = 1.0f,
                SmithingEnergyDisable = true,
                SmithingEnergyRefiningModifiers = true,
                SmithingEnergyRefiningValue = 1.0f,
                SmithingEnergySmithingModifiers = true,
                SmithingEnergySmithingValue = 1.0f,
                SmithingEnergySmeltingModifiers = true,
                SmithingEnergySmeltingValue = 1.0f,


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


                MCMArmy = true,
                armyCohesionMultipliers = true,
                armyCohesionBaseChange = -2,
                armyDisableCohesionLossClanOnlyParties = true,
                armyApplyMultiplerToClanOnlyParties = true,
                armyCohesionLossMultiplier = 1.0f,


                MCMRelationBuilding = true,
                relationsKillingBanditsEnabled = true,
                GroupsOfBandits = 1,
                RelationshipIncrease = 1,
                Radius = 1000,
                SizeBonusEnabled = true,
                SizeBonus = 0.05f,
                PrisonersOnly = true,
                IncludeBandits = true,
                IncludeOutlaws = true,
                IncludeMafia = true,




                MCMSkillsXp = true,
                SkillXpEnabled = true,

                SkillXpUseForPlayer = true,
                SkillXpUseForPlayerClan = true,
                SkillXpUseForAI = true,

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

                SkillXpUseGlobalMultipler = true,
                SkillsXpGlobalMultiplier = 1.0f,



                LearningRateEnabled = true,
                LearningRateMultiplier = 1.0f,


                PartyFoodConsumptionEnabled = true,
                PartyFoodConsumptionMultiplier = 1.0f,

                //~ BT
                #region Preset Everything enabled with vanilla values
                QuestCharactersIgnorePartySize = true,
                ShowFoodDaysRemaining = true,
                CampaignSpeed = 4,
                SettlementMilitiaEliteSpawnRateBonusEnabled = true,
                SettlementEliteMeleeSpawnRateBonus = 0.1f,
                SettlementEliteRangedSpawnRateBonus = 0.1f,
                SettlementMilitiaBonusEnabled = true,
                CastleMilitiaBonusFlat = 0.0f,
                CastleMilitiaRetirementModifier = 0.025f,
                TownMilitiaBonusFlat = 0.0f,
                TownMilitiaRetirementModifier = 0.025f,
                SettlementFoodBonusEnabled = true,
                CastleFoodBonus = 1.0f,
                TownFoodBonus = 1.0f,
                SettlementProsperityFoodMalusTweakEnabled = true,
                SettlementProsperityFoodMalusDivisor = 50,
                ProductionTweakEnabled = true,
                ProductionFoodTweakEnabled = 1.0f,
                ProductionOtherTweakEnabled = 1.0f,
                DisableTroopDonationAnyEnabled = true,
                DisableTroopDonationPatchEnabled = true,
                ChangeToKingdomCulture = true,
                EnableCultureChanger = true,
                TimeToChanceCulture = 10,
                PlayerCultureOverride = { "No Override" },
                WorkshopBuyTweak = 1.0f,
                EnableWorkshopBuyTweak = true,
                WorkshopSellTweak = 1.0f,
                EnableWorkshopSellTweak = true,
                WorkshopEffectivnessv2Factor = 1.0f,
                WorkshopEffectivnessEnabled = true,
                WorkshopBuyingCostTweakEnabled = true,
                WorkshopBaseCost = 10000,
                MaxWorkshopCountTweakEnabled = true,
                BaseWorkshopCount = 1,
                BonusWorkshopsPerClanTier = 1,
                MinimumBettingOddsTweakEnabled = true,
                MinimumBettingOdds = 1.1f,
                ArenaHeroExperienceMultiplierEnabled = true,
                ArenaHeroExperienceMultiplier = 0.06f,
                TournamentHeroExperienceMultiplierEnabled = true,
                TournamentHeroExperienceMultiplier = 0.33f,
                TournamentMaxBetAmountTweakEnabled = true,
                TournamentMaxBetAmount = 150,
                TournamentGoldRewardEnabled = true,
                TournamentGoldRewardAmount = 0,
                TournamentRenownIncreaseEnabled = true,
                TournamentRenownAmount = 3,
                TownMilitiaBarracksBonusEnabled = true,
                TownMilitiaBarracksAmountLevel1 = 0.5f,
                TownMilitiaBarracksAmountLevel2 = 1.0f,
                TownMilitiaBarracksAmountLevel3 = 1.5f,
                TownOrchardsBonusEnabled = true,
                TownOrchardsFoodProductionAmountLevel1 = 10,
                TownOrchardsFoodProductionAmountLevel2 = 20,
                TownOrchardsFoodProductionAmountLevel3 = 30,
                TownGranaryBonusEnabled = true,
                TownGranaryStorageAmountLevel1 = 200,
                TownGranaryStorageAmountLevel2 = 400,
                TownGranaryStorageAmountLevel3 = 600,
                TownTrainingFieldsBonusEnabled = true,
                TownTrainingFieldsXpAmountLevel1 = 30,
                TownTrainingFieldsXpAmountLevel2 = 60,
                TownTrainingFieldsXpAmountLevel3 = 100,
                CastleMilitiaBarracksBonusEnabled = true,
                CastleMilitiaBarracksAmountLevel1 = 1,
                CastleMilitiaBarracksAmountLevel2 = 2,
                CastleMilitiaBarracksAmountLevel3 = 3,
                CastleGardensBonusEnabled = true,
                CastleGardensFoodProductionAmountLevel1 = 5,
                CastleGardensFoodProductionAmountLevel2 = 10,
                CastleGardensFoodProductionAmountLevel3 = 15,
                CastleGranaryBonusEnabled = true,
                CastleGranaryStorageAmountLevel1 = 100,
                CastleGranaryStorageAmountLevel2 = 200,
                CastleGranaryStorageAmountLevel3 = 300,
                CastleTrainingFieldsBonusEnabled = true,
                CastleTrainingFieldsXpAmountLevel1 = 1,
                CastleTrainingFieldsXpAmountLevel2 = 2,
                CastleTrainingFieldsXpAmountLevel3 = 3,
                PrisonerConformityTweaksEnabled = true,
                PrisonerConformityTweakBonus = 0.0f,
                PrisonerConformityTweaksApplyToClan = true,
                PrisonerConformityTweaksApplyToAi = true,
                PrisonerSizeTweakEnabled = true,
                PrisonerSizeTweakAI = true,
                PrisonerSizeTweakPercent = 0.0f,
                PrisonerImprisonmentTweakEnabled = true,
                PrisonerImprisonmentPlayerOnly = false,
                MinimumDaysOfImprisonment = 0,
                EnableMissingHeroFix = true,
                DailyTroopExperienceTweakEnabled = true,
                LeadershipPercentageForDailyExperienceGain = 0.01f,
                DailyTroopExperienceRequiredLeadershipLevel = 30,
                DailyTroopExperienceApplyToPlayerClanMembers = true,
                DailyTroopExperienceApplyToAllNPC = true,
                DisplayMessageDailyExperienceGain = true,
                PartyWageTweaksEnabled = true,
                PartyWagePercent = 1.0f,
                GarrisonWagePercent = 1.0f,
                ApplyWageTweakToFaction = true,
                ApplyWageTweakToAI = true,
                PartySizeTweakEnabled = true,
                PartySizeTweakAIFactor = 0.0f,
                StewardPartySizeBonusEnabled = true,
                StewardPartySizeBonus = 0.0f,
                LeadershipPartySizeBonusEnabled = true,
                LeadershipPartySizeBonus = 0.0f,
                PlayerCaravanPartySizeTweakEnabled = true,
                PlayerCaravanPartySize = 30,
                BTCohesionTweakEnabled = true,
                BTCohesionTweakv2 = 1.0f,
                ClanArmyLosesNoCohesionEnabled = true,
                KingdomBalanceStrengthEnabled = true,
                BalancingPartySizeTweaksEnabled = true,
                BalancingPartyLimitTweaksEnabled = true,
                BalancingFoodTweakEnabled = true,
                BalancingTimeRecruitsTweaksEnabled = true,
                BalancingTaxTweaksEnabled = true,
                BalancingWagesTweaksEnabled = true,
                BalancingUpgradeTroopsTweaksEnabled = true,
                PaleicianBoost = 0.0f,
                RoyalistVlandiaBoost = 0.0f,
                KingdomBalanceStrengthCEKEnabled = false,
                AriorumBoost = 0.0f,
                Aserai_CEK_Boost = 0.0f,
                Battania_CEK_Boost = 0.0f,
                Empire_S_CEK_Boost = 0.0f,
                Vlandia_CEK_Boost = 0.0f,
                Empire_CEK_Boost = 0.0f,
                Khuzait_CEK_Boost = 0.0f,
                Sturgia_CEK_Boost = 0.0f,
                ApolssalyBoost = 0.0f,
                LyrionBoost = 0.0f,
                RebelKhuzaitBoost = 0.0f,
                Player_CEK_Boost = 0.0f,
                Empire_W_CEK_Boost = 0.0f,
                VagirBoost = 0.0f,
                NordlingsBoost = 0.0f,
                AseraiBoost = 0.0f,
                KingdomBalanceStrengthVanEnabled = true,
                BattaniaBoost = 0.0f,
                KhuzaitBoost = 0.0f,
                PlayerBoost = 0.0f,
                Empire_N_Boost = 0.0f,
                Empire_S_Boost = 0.0f,
                Empire_W_Boost = 0.0f,
                SturgiaBoost = 0.0f,
                VlandiaBoost = 0.0f,
                BarterablesTweaksEnabled = true,
                BarterablesJoinKingdomAsClanAdjustment = 1.0f,
                BarterablesJoinKingdomAsClanAltFormulaEnabled = true,
                PreventSmeltingLockedItems = true,
                SmeltingTweakEnabled = true,
                AutoLearnSmeltedParts = true,
                CraftingStaminaTweakEnabled = true,
                MaxCraftingStamina = 400,
                CraftingStaminaGainAmount = 5,
                CraftingStaminaGainOutsideSettlementMultiplier = 0.0f,
                IgnoreCraftingStamina = true,



/*
                PartiesLimitTweakEnabled = true,
                AICustomSpawnPartiesLimitTweakEnabled = true,
                BaseAICustomSpawnPartiesLimit = 1,
                AIClanPartiesLimitTweakEnabled = true,
                BaseAIClanPartiesLimit = 1,
                AIMinorClanPartiesLimitTweakEnabled = true,
                ClanPartiesLimitTweakEnabled = true,
                BaseClanPartiesLimit = 1,
                ClanPartiesBonusPerClanTier = 0.5f,
                CompanionLimitTweakEnabled = true,
                CompanionBaseLimit = 3,
                CompanionLimitBonusPerClanTier = 1,
*/


                UnlimitedWanderersPatch = true,
                //PregnancyTweaksEnabled = true,
                NoStillbirthsTweakEnabled = true,
                NoMaternalMortalityTweakEnabled = true,
                PlayerCharacterInfertileEnabled = false,
                MinPregnancyAge = 18,
                MaxPregnancyAge = 45,
                ClanFertilityBonus = 1.0f,
                MaxChildren = 5,
                DailyChancePregnancyTweakEnabled = true,

/*
                TwinsProbability = 0.03f,
                TwinsProbabilityTweakEnabled = true,
                FemaleOffspringProbability = 0.51f,
                FemaleOffspringProbabilityTweakEnabled = true,
                PregnancyDuration = 36,
                PregnancyDurationTweakEnabled = true,*/

                SkillExperienceMultipliersEnabled = true,
                SkillBonusEngineering = 1.0f,
                SkillBonusLeadership = 1.0f,
                SkillBonusMedicine = 1.0f,
                SkillBonusRiding = 1.0f,
                SkillBonusRoguery = 1.0f,
                SkillBonusScouting = 1.0f,
                SkillBonusSmithing = 1.0f,
                SkillBonusTrade = 1.0f,
                PerSkillBonusEnabled = true,
                CompanionSkillExperienceMultiplier = 1.0f,
                CompanionSkillExperienceMultiplierEnabled = true,
                HeroSkillExperienceMultiplier = 1.0f,
                HeroSkillExperienceMultiplierEnabled = true,
                //AttributeFocusPointTweakEnabled = true,
                //AttributePointRequiredLevel = 4,
                //FocusPointsPerLevel = 1,
                AgeTweaksEnabled = true,
                BecomeInfantAge = 3,
                BecomeChildAge = 6,
                BecomeTeenagerAge = 14,
                HeroComesOfAge = 18,
                BecomeOldAge = 47,
                MaxAge = 125,
                DifficultyTweakEnabled = true,
                PlayerMapMovementSpeedBonusTweakEnabled = true,
                PlayerMapMovementSpeedBonusMultiplier = 0.0f,
                CombatAIDifficultyTweakEnabled = true,
                CombatAIDifficultyMultiplier = 0.96f,
                DamageToTroopsTweakEnabled = true,
                DamageToTroopsMultiplier = 1.0f,
                DamageToFriendsTweakEnabled = true,
                DamageToFriendsMultiplier = 1.0f,
                DamageToPlayerTweakEnabled = true,
                DamageToPlayerMultiplier = 1.0f,
                BattleSizeTweakEnabled = true,
                BattleSize = 1000,
                SliceThroughEnabled = true,
                TwoHandedWeaponsSliceThroughEnabled = true,
                SingleHandedWeaponsSliceThroughEnabled = true,
                TroopExperienceTweakEnabled = true,
                TroopBattleSimulationExperienceMultiplierEnabled = true,
                TroopBattleSimulationExperienceMultiplier = 0.9f,
                TroopBattleExperienceMultiplierEnabled = true,
                TroopBattleExperienceMultiplier = 1.0f,
                SiegeTweaksEnabled = true,
                SiegeConstructionProgressPerDayMultiplier = 1.0f,
                SiegeCollateralDamageCasualties = 0,
                SiegeDestructionCasualties = 0,
                HideoutBattleTroopLimitTweakEnabled = true,
                HideoutBattleTroopLimit = 10,
                ContinueHideoutBattleOnPlayerDeath = true,
                ContinueHideoutBattleOnPlayerLoseDuel = true,
                BattleRewardTweaksEnabled = true,
                BattleRenownMultiplier = 1.0f,
                BattleInfluenceMultiplier = 1.0f,
                BattleMoraleMultiplier = 1.0f,
                BattleRewardApplyToAI = true,
                BattleRewardShowDebug = true
                #endregion



            });

            basePresets.Add("Kaoses", () => new MCMSettings()
            {
                Debug = false,
                LoadMCMConfigFile = false,
                LogToFile = false,
                MCMItemModifiers = true,

                MCMBodyArmorModifiers = true,
                BodyArmorWeightModifiers = false,
                BodyArmorValueModifiers = true,
                BodyArmorTier1PriceMultiplier = 0.3f,
                BodyArmorTier2PriceMultiplier = 0.4f,
                BodyArmorTier3PriceMultiplier = 0.5f,
                BodyArmorTier4PriceMultiplier = 0.6f,
                BodyArmorTier5PriceMultiplier = 0.7f,
                BodyArmorTier6PriceMultiplier = 0.8f,

                MCMBowModifiers = true,
                BowWeightModifiers = false,
                BowValueModifiers = true,
                BowTier1PriceMultiplier = 0.3f,
                BowTier2PriceMultiplier = 0.4f,
                BowTier3PriceMultiplier = 0.5f,
                BowTier4PriceMultiplier = 0.6f,
                BowTier5PriceMultiplier = 0.7f,
                BowTier6PriceMultiplier = 0.8f,

                MCMCapeModifiers = true,
                CapeWeightModifiers = false,
                CapeValueModifiers = true,
                CapeTier1PriceMultiplier = 0.3f,
                CapeTier2PriceMultiplier = 0.4f,
                CapeTier3PriceMultiplier = 0.5f,
                CapeTier4PriceMultiplier = 0.6f,
                CapeTier5PriceMultiplier = 0.7f,
                CapeTier6PriceMultiplier = 0.8f,

                MCMChestArmorModifiers = true,
                ChestArmorWeightModifiers = false,
                ChestArmorValueModifiers = true,
                ChestArmorTier1PriceMultiplier = 0.3f,
                ChestArmorTier2PriceMultiplier = 0.4f,
                ChestArmorTier3PriceMultiplier = 0.5f,
                ChestArmorTier4PriceMultiplier = 0.6f,
                ChestArmorTier5PriceMultiplier = 0.7f,
                ChestArmorTier6PriceMultiplier = 0.8f,

                MCMCrossbowModifiers = true,
                CrossbowWeightModifiers = false,
                CrossbowValueModifiers = true,
                CrossbowTier1PriceMultiplier = 0.3f,
                CrossbowTier2PriceMultiplier = 0.4f,
                CrossbowTier3PriceMultiplier = 0.5f,
                CrossbowTier4PriceMultiplier = 0.6f,
                CrossbowTier5PriceMultiplier = 0.7f,
                CrossbowTier6PriceMultiplier = 0.8f,

                MCMHandArmorModifiers = true,
                HandArmorWeightModifiers = false,
                HandArmorValueModifiers = true,
                HandArmorTier1PriceMultiplier = 0.3f,
                HandArmorTier2PriceMultiplier = 0.4f,
                HandArmorTier3PriceMultiplier = 0.5f,
                HandArmorTier4PriceMultiplier = 0.6f,
                HandArmorTier5PriceMultiplier = 0.7f,
                HandArmorTier6PriceMultiplier = 0.8f,

                MCMHeadArmorModifiers = true,
                HeadArmorWeightModifiers = false,
                HeadArmorValueModifiers = true,
                HeadArmorTier1PriceMultiplier = 0.3f,
                HeadArmorTier2PriceMultiplier = 0.4f,
                HeadArmorTier3PriceMultiplier = 0.5f,
                HeadArmorTier4PriceMultiplier = 0.6f,
                HeadArmorTier5PriceMultiplier = 0.7f,
                HeadArmorTier6PriceMultiplier = 0.8f,

                MCMHorseModifiers = true,
                HorseWeightModifiers = false,
                HorseValueModifiers = true,
                HorseTier1PriceMultiplier = 0.3f,
                HorseTier2PriceMultiplier = 0.4f,
                HorseTier3PriceMultiplier = 0.5f,
                HorseTier4PriceMultiplier = 0.6f,
                HorseTier5PriceMultiplier = 0.7f,
                HorseTier6PriceMultiplier = 0.8f,

                MCMHorseHarnessModifiers = true,
                HorseHarnessWeightModifiers = false,
                HorseHarnessValueModifiers = true,
                HorseHarnessTier1PriceMultiplier = 0.3f,
                HorseHarnessTier2PriceMultiplier = 0.4f,
                HorseHarnessTier3PriceMultiplier = 0.5f,
                HorseHarnessTier4PriceMultiplier = 0.6f,
                HorseHarnessTier5PriceMultiplier = 0.7f,
                HorseHarnessTier6PriceMultiplier = 0.8f,

                MCMLegArmorModifiers = true,
                LegArmorWeightModifiers = false,
                LegArmorValueModifiers = true,
                LegArmorTier1PriceMultiplier = 0.3f,
                LegArmorTier2PriceMultiplier = 0.4f,
                LegArmorTier3PriceMultiplier = 0.5f,
                LegArmorTier4PriceMultiplier = 0.6f,
                LegArmorTier5PriceMultiplier = 0.7f,
                LegArmorTier6PriceMultiplier = 0.8f,

                MCMMusketModifiers = true,
                MusketWeightModifiers = false,
                MusketValueModifiers = true,
                MusketTier1PriceMultiplier = 0.4f,
                MusketTier2PriceMultiplier = 0.5f,
                MusketTier3PriceMultiplier = 0.6f,
                MusketTier4PriceMultiplier = 0.7f,
                MusketTier5PriceMultiplier = 0.8f,
                MusketTier6PriceMultiplier = 0.9f,

                MCMOneHandedWeaponModifiers = true,
                OneHandedWeaponWeightModifiers = false,
                OneHandedWeaponValueModifiers = true,
                OneHandedWeaponTier1PriceMultiplier = 0.3f,
                OneHandedWeaponTier2PriceMultiplier = 0.4f,
                OneHandedWeaponTier3PriceMultiplier = 0.5f,
                OneHandedWeaponTier4PriceMultiplier = 0.6f,
                OneHandedWeaponTier5PriceMultiplier = 0.7f,
                OneHandedWeaponTier6PriceMultiplier = 0.8f,

                MCMPistolModifiers = true,
                PistolWeightModifiers = false,
                PistolValueModifiers = true,
                PistolTier1PriceMultiplier = 0.4f,
                PistolTier2PriceMultiplier = 0.5f,
                PistolTier3PriceMultiplier = 0.6f,
                PistolTier4PriceMultiplier = 0.7f,
                PistolTier5PriceMultiplier = 0.8f,
                PistolTier6PriceMultiplier = 0.9f,

                MCMPolearmModifiers = true,
                PolearmWeightModifiers = false,
                PolearmValueModifiers = true,
                PolearmTier1PriceMultiplier = 0.3f,
                PolearmTier2PriceMultiplier = 0.4f,
                PolearmTier3PriceMultiplier = 0.5f,
                PolearmTier4PriceMultiplier = 0.6f,
                PolearmTier5PriceMultiplier = 0.7f,
                PolearmTier6PriceMultiplier = 0.8f,

                MCMShieldModifiers = true,
                ShieldWeightModifiers = false,
                ShieldValueModifiers = true,
                ShieldTier1PriceMultiplier = 0.3f,
                ShieldTier2PriceMultiplier = 0.4f,
                ShieldTier3PriceMultiplier = 0.5f,
                ShieldTier4PriceMultiplier = 0.6f,
                ShieldTier5PriceMultiplier = 0.7f,
                ShieldTier6PriceMultiplier = 0.8f,

                MCMTwoHandedWeaponModifiers = true,
                TwoHandedWeaponWeightModifiers = false,
                TwoHandedWeaponValueModifiers = true,
                TwoHandedWeaponTier1PriceMultiplier = 0.4f,
                TwoHandedWeaponTier2PriceMultiplier = 0.5f,
                TwoHandedWeaponTier3PriceMultiplier = 0.6f,
                TwoHandedWeaponTier4PriceMultiplier = 0.7f,
                TwoHandedWeaponTier5PriceMultiplier = 0.8f,
                TwoHandedWeaponTier6PriceMultiplier = 0.9f,

                MCMBattleRewardModifiers = true,
                BattleRewardsRelationShipGainModifiers = true,
                BattleRewardsRelationShipGainMultiplier = 1.5f,
                BattleRewardsRenownGainModifiers = true,
                BattleRewardsRenownGainMultiplier = 1.5f,
                BattleRewardsInfluenceGainModifiers = true,
                BattleRewardsInfluenceGainMultiplier = 1.7f,
                BattleRewardsMoraleGainModifiers = true,
                BattleRewardsMoraleGainMultiplier = 0.9f,
                BattleRewardsGoldLossModifiers = true,
                BattleRewardsGoldLossMultiplier = 1.4f,


                //MCMClanModifiers = true,
                ClanAdditionalPartyLimitEnabled = true,
                ClanPlayerPartiesLimitEnabled = true,
                ClanPlayerBasePartiesLimit = 1,
                ClanPlayerPartiesBonusPerClanTier = 0.5f,
                ClanAIPartiesLimitTweakEnabled = true,
                ClanAIBaseClanPartiesLimit = 1,
                ClanAIPartiesBonusPerClanTier = 0.5f,
                ClanAIMinorClanPartiesLimitTweakEnabled = false,
                AICustomSpawnPartiesLimitTweakEnabled = false,
                BaseAICustomSpawnPartiesLimit = 1,
                ClanCompanionLimitEnabled = true,
                ClanAdditionalCompanionLimit = 0,

                MCMWorkShopModifiers = true,
                WorkShopBankruptcyModifiers = true,
                WorkShopBankruptcyValue = 6,
                WorkShopMaxWorkshopCountForPlayerModifiers = true,
                WorkShopMaxWorkshopCountForPlayerValue = 2,

                MCMCharacterDevlopmentModifiers = true,
                CharacterLevelsPerAttributeModifiers = true,
                CharacterLevelsPerAttributeValue = 3,
                CharacterFocusPerLevelModifiers = true,
                CharacterFocusPerLevelValue = 2,


                MCMPregnancyModifiers = true,
                PregnancyDurationModifiers = true,
                PregnancyDurationValue = 36,
                PregnancyLaborMortalityChanceModifiers = true,
                PregnancyLaborMortalityChanceValue = 0.010f,
                PregnancyFemaleOffspringChanceModifiers = true,
                PregnancyFemaleOffspringChanceValue = 0.60f,

                MCMSmithingModifiers = true,
                SmithingRefiningXpModifiers = true,
                SmithingRefiningXpValue = 1.5f,
                SmithingSmeltingXpModifiers = true,
                SmithingSmeltingXpValue = 1.5f,
                SmithingSmithingXpModifiers = true,
                SmithingSmithingXpValue = 2.5f,
                SmithingEnergyDisable = true,
                SmithingEnergyRefiningModifiers = true,
                SmithingEnergyRefiningValue = 0.5f,
                SmithingEnergySmithingModifiers = true,
                SmithingEnergySmithingValue = 0.5f,
                SmithingEnergySmeltingModifiers = true,
                SmithingEnergySmeltingValue = 0.5f,



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


                MCMArmy = true,
                armyCohesionMultipliers = true,
                armyCohesionBaseChange = -2,
                armyDisableCohesionLossClanOnlyParties = false,
                armyApplyMultiplerToClanOnlyParties = true,
                armyCohesionLossMultiplier = 0.75f,


                MCMRelationBuilding = true,
                relationsKillingBanditsEnabled = true,
                GroupsOfBandits = 1,
                RelationshipIncrease = 1,
                Radius = 1500,
                SizeBonusEnabled = false,
                SizeBonus = 0.05f,
                PrisonersOnly = false,
                IncludeBandits = true,
                IncludeOutlaws = false,
                IncludeMafia = false,



                MCMSkillsXp = false,
                SkillXpEnabled = false,

                SkillXpUseForPlayer = false,
                SkillXpUseForPlayerClan = false,
                SkillXpUseForAI = false,

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

                SkillXpUseGlobalMultipler = false,
                SkillsXpGlobalMultiplier = 1.0f,



                LearningRateEnabled = false,
                LearningRateMultiplier = 1.0f,


                PartyFoodConsumptionEnabled = false,
                PartyFoodConsumptionMultiplier = 1.0f,



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
                SettlementEliteRangedSpawnRateBonus = 0.1f,
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
                ClanArmyLosesNoCohesionEnabled = false,
                BarterablesTweaksEnabled = true,
                BarterablesJoinKingdomAsClanAdjustment = 100,
                BarterablesJoinKingdomAsClanAltFormulaEnabled = false,
                AutoLearnSmeltedParts = true,
                PreventSmeltingLockedItems = false,
                CraftingStaminaGainAmount = 10,
                CraftingStaminaGainOutsideSettlementMultiplier = 1.0f,
                MaxCraftingStamina = 400,
                CraftingStaminaTweakEnabled = true,
                IgnoreCraftingStamina = false,

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
                PerSkillBonusEnabled = false,
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
                DamageToFriendsMultiplier = 1.0f,
                DamageToFriendsTweakEnabled = false,
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
                BattleInfluenceMultiplier = 1.0f,
                BattleRenownMultiplier = 2.0f,
                BattleRewardApplyToAI = true,
                BattleRewardTweaksEnabled = true
                #endregion
            });
            return basePresets;
        }

    }
}
