using KaosesCommon.Objects;
using KaosesCommon.Utils;
using KaosesTweaks.Objects;
using KaosesTweaks.Settings;

using CharacterConfig = KaosesCharacterTweaksCore.Settings.KaosesCharacterTweaksCoreConfig;
using CharacterFactory = KaosesCharacterTweaksCore.Objects.KaosesCharacterTweaksCoreFactory;
using BattleConfig = KaosesBattleTweaksCore.Settings.KaosesBattleTweaksCoreConfig;
using BattleFactory = KaosesBattleTweaksCore.Objects.KaosesBattleTweaksCoreFactory;
using CraftingConfig = KaosesCraftingTweaksCore.Settings.KaosesCraftingTweaksCoreConfig;
using CraftingFactory = KaosesCraftingTweaksCore.Objects.KaosesCraftingTweaksCoreFactory;
using AmmoConfig = KaosesMoreAmmoCore.Settings.KaosesMoreAmmoCoreConfig;
using AmmoFactory = KaosesMoreAmmoCore.Objects.KaosesMoreAmmoCoreFactory;
using SizeConfig = KaosesPartySizesCore.Settings.KaosesPartySizesCoreConfig;
using SizeFactory = KaosesPartySizesCore.Objects.KaosesPartySizesCoreFactory;
using SpeedsConfig = KaosesPartySpeedsCore.Settings.SpeedsCoreConfig;
using SpeedsFactory = KaosesPartySpeedsCore.Objects.SpeedsCoreFactory;
using SettlementConfig = KaosesSettlementTweaksCore.Settings.KaosesSettlementTweaksCoreConfig;
using SettlementFactory = KaosesSettlementTweaksCore.Objects.KaosesSettlementTweaksCoreFactory;
using TradeConfig = KaosesTradeGoodsCore.Settings.KaosesTradeGoodsCoreConfig;
using TradeFactory = KaosesTradeGoodsCore.Objects.KaosesTradeGoodsCoreFactory;
using WagesConfig = KaosesWagesCore.Settings.KaosesWagesCoreConfig;
using WagesFactory = KaosesWagesCore.Objects.KaosesWagesCoreFactory;
using WorkshopConfig = KaosesWorkshopTweaksCore.Settings.KaosesWorkshopTweaksCoreConfig;
using WorkshopFactory = KaosesWorkshopTweaksCore.Objects.KaosesWorkshopTweaksCoreFactory;
using KillingBanditsConfig = KillingBanditsRaisesRelationsCore.Settings.KillingBanditsRaisesRelationsCoreConfig;
using KillingBanditsFactory = KillingBanditsRaisesRelationsCore.Objects.KillingBanditsRaisesRelationsCoreFactory;

namespace KaosesTweaks
{
    /// <summary>
    /// Internal class to initialize the mod settings class from MCM and to set the IM and Logger variables 
    /// </summary>
    internal class Init
    {
        public Init()
        {
            /// Load the Settings Object
            Config settings = Factory.Settings;
            //ConfigOther settings2 = Factory.Settings2;
            //TempCoreConfig settings2 = Factory.SettingsCore;
            //TempCoreConfig settings2 = TempCoreFactory.Settings;
            //Factory.DConfig();


            ///
            /// Set IM variable values
            ///
            InfoMgr im = new InfoMgr(settings.IsDebug, settings.IsLogToFile, SubModule.ModuleId, SubModule.modulePath);
            im.PrePrend = SubModule.ModuleId;
            im.ModVersion = settings.versionTextObj.ToString();
            //im.LogFilePath = "c:\\BannerLord\\KaosesCommon\\logfile.text";
            //im.AddDateTimeToLog = true;
            Factory.IM = im;
            CharacterFactory.IM = im;
            BattleFactory.IM = im;
            CraftingFactory.IM = im;
            AmmoFactory.IM = im;
            SizeFactory.IM = im;
            SpeedsFactory.IM = im;
            SettlementFactory.IM = im;
            TradeFactory.IM = im;
            WagesFactory.IM = im;
            WorkshopFactory.IM = im;
            KillingBanditsFactory.IM = im;
        }
    }
}
