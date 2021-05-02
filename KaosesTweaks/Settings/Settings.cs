using Bannerlord.BUTR.Shared.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace KaosesTweaks.Settings
{
    public class Settings : ISettingsProviderInterface
    {
        //private readonly ISettingsProviderInterface _provider;
        public static Settings? Instance;

        public string Id => Statics.InstanceID;
        string modName = Statics.DisplayName;
        public string DisplayName => TextObjectHelper.Create("{=testModDisplayName}" + modName + " {VERSION}", new Dictionary<string, TextObject>()
        {
            { "VERSION", TextObjectHelper.Create(typeof(MCMSettings).Assembly.GetName().Version?.ToString(3) ?? "")! }
        })!.ToString();

        public bool Debug { get; set; } = false;
        public bool LoadMCMConfigFile { get; set; } = false;
        public bool LogToFile { get; set; } = false;
        public string ModDisplayName { get { return DisplayName; } }


        ///~ Mod Specific settings 
        #region Items

        #region Item BodyArmor
        public bool BodyArmorWeightModifiers { get; set; } = true;
        public float BodyArmorTier1WeightMultiplier { get; set; } = 0.2f;
        public float BodyArmorTier2WeightMultiplier { get; set; } = 0.3f;
        public float BodyArmorTier3WeightMultiplier { get; set; } = 0.4f;
        public float BodyArmorTier4WeightMultiplier { get; set; } = 0.5f;
        public float BodyArmorTier5WeightMultiplier { get; set; } = 0.6f;
        public float BodyArmorTier6WeightMultiplier { get; set; } = 0.7f;
        public bool BodyArmorValueModifiers { get; set; } = true;
        public float BodyArmorTier1PriceMultiplier { get; set; } = 1.0f;
        public float BodyArmorTier2PriceMultiplier { get; set; } = 1.0f;
        public float BodyArmorTier3PriceMultiplier { get; set; } = 1.0f;
        public float BodyArmorTier4PriceMultiplier { get; set; } = 1.0f;
        public float BodyArmorTier5PriceMultiplier { get; set; } = 1.0f;
        public float BodyArmorTier6PriceMultiplier { get; set; } = 1.0f;

        #endregion Item BodyArmor

        #region Item Bow
        public bool BowWeightModifiers { get; set; } = true;
        public float BowTier1WeightMultiplier { get; set; } = 1.0f;
        public float BowTier2WeightMultiplier { get; set; } = 1.0f;
        public float BowTier3WeightMultiplier { get; set; } = 1.0f;
        public float BowTier4WeightMultiplier { get; set; } = 1.0f;
        public float BowTier5WeightMultiplier { get; set; } = 1.0f;
        public float BowTier6WeightMultiplier { get; set; } = 1.0f;
        public bool BowValueModifiers { get; set; } = true;
        public float BowTier1PriceMultiplier { get; set; } = 1.0f;
        public float BowTier2PriceMultiplier { get; set; } = 1.0f;
        public float BowTier3PriceMultiplier { get; set; } = 1.0f;
        public float BowTier4PriceMultiplier { get; set; } = 1.0f;
        public float BowTier5PriceMultiplier { get; set; } = 1.0f;
        public float BowTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Bow


        #region Item Cape
        public bool CapeWeightModifiers { get; set; } = true;
        public float CapeTier1WeightMultiplier { get; set; } = 1.0f;
        public float CapeTier2WeightMultiplier { get; set; } = 1.0f;
        public float CapeTier3WeightMultiplier { get; set; } = 1.0f;
        public float CapeTier4WeightMultiplier { get; set; } = 1.0f;
        public float CapeTier5WeightMultiplier { get; set; } = 1.0f;
        public float CapeTier6WeightMultiplier { get; set; } = 1.0f;
        public bool CapeValueModifiers { get; set; } = true;
        public float CapeTier1PriceMultiplier { get; set; } = 1.0f;
        public float CapeTier2PriceMultiplier { get; set; } = 1.0f;
        public float CapeTier3PriceMultiplier { get; set; } = 1.0f;
        public float CapeTier4PriceMultiplier { get; set; } = 1.0f;
        public float CapeTier5PriceMultiplier { get; set; } = 1.0f;
        public float CapeTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Cape

        #region Item ChestArmor
        public bool ChestArmorWeightModifiers { get; set; } = true;
        public float ChestArmorTier1WeightMultiplier { get; set; } = 1.0f;
        public float ChestArmorTier2WeightMultiplier { get; set; } = 1.0f;
        public float ChestArmorTier3WeightMultiplier { get; set; } = 1.0f;
        public float ChestArmorTier4WeightMultiplier { get; set; } = 1.0f;
        public float ChestArmorTier5WeightMultiplier { get; set; } = 1.0f;
        public float ChestArmorTier6WeightMultiplier { get; set; } = 1.0f;
        public bool ChestArmorValueModifiers { get; set; } = true;
        public float ChestArmorTier1PriceMultiplier { get; set; } = 1.0f;
        public float ChestArmorTier2PriceMultiplier { get; set; } = 1.0f;
        public float ChestArmorTier3PriceMultiplier { get; set; } = 1.0f;
        public float ChestArmorTier4PriceMultiplier { get; set; } = 1.0f;
        public float ChestArmorTier5PriceMultiplier { get; set; } = 1.0f;
        public float ChestArmorTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item ChestArmor

        #region Item Crossbow
        public bool CrossbowWeightModifiers { get; set; } = true;
        public float CrossbowTier1WeightMultiplier { get; set; } = 1.0f;
        public float CrossbowTier2WeightMultiplier { get; set; } = 1.0f;
        public float CrossbowTier3WeightMultiplier { get; set; } = 1.0f;
        public float CrossbowTier4WeightMultiplier { get; set; } = 1.0f;
        public float CrossbowTier5WeightMultiplier { get; set; } = 1.0f;
        public float CrossbowTier6WeightMultiplier { get; set; } = 1.0f;
        public bool CrossbowValueModifiers { get; set; } = true;
        public float CrossbowTier1PriceMultiplier { get; set; } = 1.0f;
        public float CrossbowTier2PriceMultiplier { get; set; } = 1.0f;
        public float CrossbowTier3PriceMultiplier { get; set; } = 1.0f;
        public float CrossbowTier4PriceMultiplier { get; set; } = 1.0f;
        public float CrossbowTier5PriceMultiplier { get; set; } = 1.0f;
        public float CrossbowTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Crossbow

        #region Item HandArmor
        public bool HandArmorWeightModifiers { get; set; } = true;
        public float HandArmorTier1WeightMultiplier { get; set; } = 1.0f;
        public float HandArmorTier2WeightMultiplier { get; set; } = 1.0f;
        public float HandArmorTier3WeightMultiplier { get; set; } = 1.0f;
        public float HandArmorTier4WeightMultiplier { get; set; } = 1.0f;
        public float HandArmorTier5WeightMultiplier { get; set; } = 1.0f;
        public float HandArmorTier6WeightMultiplier { get; set; } = 1.0f;
        public bool HandArmorValueModifiers { get; set; } = true;
        public float HandArmorTier1PriceMultiplier { get; set; } = 1.0f;
        public float HandArmorTier2PriceMultiplier { get; set; } = 1.0f;
        public float HandArmorTier3PriceMultiplier { get; set; } = 1.0f;
        public float HandArmorTier4PriceMultiplier { get; set; } = 1.0f;
        public float HandArmorTier5PriceMultiplier { get; set; } = 1.0f;
        public float HandArmorTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item HandArmor

        #region Item HeadArmor
        public bool HeadArmorWeightModifiers { get; set; } = true;
        public float HeadArmorTier1WeightMultiplier { get; set; } = 1.0f;
        public float HeadArmorTier2WeightMultiplier { get; set; } = 1.0f;
        public float HeadArmorTier3WeightMultiplier { get; set; } = 1.0f;
        public float HeadArmorTier4WeightMultiplier { get; set; } = 1.0f;
        public float HeadArmorTier5WeightMultiplier { get; set; } = 1.0f;
        public float HeadArmorTier6WeightMultiplier { get; set; } = 1.0f;
        public bool HeadArmorValueModifiers { get; set; } = true;
        public float HeadArmorTier1PriceMultiplier { get; set; } = 1.0f;
        public float HeadArmorTier2PriceMultiplier { get; set; } = 1.0f;
        public float HeadArmorTier3PriceMultiplier { get; set; } = 1.0f;
        public float HeadArmorTier4PriceMultiplier { get; set; } = 1.0f;
        public float HeadArmorTier5PriceMultiplier { get; set; } = 1.0f;
        public float HeadArmorTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item HeadArmor


        #region Item Horse
        public bool HorseWeightModifiers { get; set; } = true;
        public float HorseTier1WeightMultiplier { get; set; } = 1.0f;
        public float HorseTier2WeightMultiplier { get; set; } = 1.0f;
        public float HorseTier3WeightMultiplier { get; set; } = 1.0f;
        public float HorseTier4WeightMultiplier { get; set; } = 1.0f;
        public float HorseTier5WeightMultiplier { get; set; } = 1.0f;
        public float HorseTier6WeightMultiplier { get; set; } = 1.0f;
        public bool HorseValueModifiers { get; set; } = true;
        public float HorseTier1PriceMultiplier { get; set; } = 1.0f;
        public float HorseTier2PriceMultiplier { get; set; } = 1.0f;
        public float HorseTier3PriceMultiplier { get; set; } = 1.0f;
        public float HorseTier4PriceMultiplier { get; set; } = 1.0f;
        public float HorseTier5PriceMultiplier { get; set; } = 1.0f;
        public float HorseTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Horse


        #region Item HorseHarness
        public bool HorseHarnessWeightModifiers { get; set; } = true;
        public float HorseHarnessTier1WeightMultiplier { get; set; } = 1.0f;
        public float HorseHarnessTier2WeightMultiplier { get; set; } = 1.0f;
        public float HorseHarnessTier3WeightMultiplier { get; set; } = 1.0f;
        public float HorseHarnessTier4WeightMultiplier { get; set; } = 1.0f;
        public float HorseHarnessTier5WeightMultiplier { get; set; } = 1.0f;
        public float HorseHarnessTier6WeightMultiplier { get; set; } = 1.0f;
        public bool HorseHarnessValueModifiers { get; set; } = true;
        public float HorseHarnessTier1PriceMultiplier { get; set; } = 1.0f;
        public float HorseHarnessTier2PriceMultiplier { get; set; } = 1.0f;
        public float HorseHarnessTier3PriceMultiplier { get; set; } = 1.0f;
        public float HorseHarnessTier4PriceMultiplier { get; set; } = 1.0f;
        public float HorseHarnessTier5PriceMultiplier { get; set; } = 1.0f;
        public float HorseHarnessTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item HorseHarness


        #region Item LegArmor
        public bool LegArmorWeightModifiers { get; set; } = true;
        public float LegArmorTier1WeightMultiplier { get; set; } = 1.0f;
        public float LegArmorTier2WeightMultiplier { get; set; } = 1.0f;
        public float LegArmorTier3WeightMultiplier { get; set; } = 1.0f;
        public float LegArmorTier4WeightMultiplier { get; set; } = 1.0f;
        public float LegArmorTier5WeightMultiplier { get; set; } = 1.0f;
        public float LegArmorTier6WeightMultiplier { get; set; } = 1.0f;
        public bool LegArmorValueModifiers { get; set; } = true;
        public float LegArmorTier1PriceMultiplier { get; set; } = 1.0f;
        public float LegArmorTier2PriceMultiplier { get; set; } = 1.0f;
        public float LegArmorTier3PriceMultiplier { get; set; } = 1.0f;
        public float LegArmorTier4PriceMultiplier { get; set; } = 1.0f;
        public float LegArmorTier5PriceMultiplier { get; set; } = 1.0f;
        public float LegArmorTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item LegArmor


        #region Item Musket
        public bool MusketWeightModifiers { get; set; } = true;
        public float MusketTier1WeightMultiplier { get; set; } = 1.0f;
        public float MusketTier2WeightMultiplier { get; set; } = 1.0f;
        public float MusketTier3WeightMultiplier { get; set; } = 1.0f;
        public float MusketTier4WeightMultiplier { get; set; } = 1.0f;
        public float MusketTier5WeightMultiplier { get; set; } = 1.0f;
        public float MusketTier6WeightMultiplier { get; set; } = 1.0f;
        public bool MusketValueModifiers { get; set; } = true;
        public float MusketTier1PriceMultiplier { get; set; } = 1.0f;
        public float MusketTier2PriceMultiplier { get; set; } = 1.0f;
        public float MusketTier3PriceMultiplier { get; set; } = 1.0f;
        public float MusketTier4PriceMultiplier { get; set; } = 1.0f;
        public float MusketTier5PriceMultiplier { get; set; } = 1.0f;
        public float MusketTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Musket


        #region Item OneHandedWeapon
        public bool OneHandedWeaponWeightModifiers { get; set; } = true;
        public float OneHandedWeaponTier1WeightMultiplier { get; set; } = 1.0f;
        public float OneHandedWeaponTier2WeightMultiplier { get; set; } = 1.0f;
        public float OneHandedWeaponTier3WeightMultiplier { get; set; } = 1.0f;
        public float OneHandedWeaponTier4WeightMultiplier { get; set; } = 1.0f;
        public float OneHandedWeaponTier5WeightMultiplier { get; set; } = 1.0f;
        public float OneHandedWeaponTier6WeightMultiplier { get; set; } = 1.0f;
        public bool OneHandedWeaponValueModifiers { get; set; } = true;
        public float OneHandedWeaponTier1PriceMultiplier { get; set; } = 1.0f;
        public float OneHandedWeaponTier2PriceMultiplier { get; set; } = 1.0f;
        public float OneHandedWeaponTier3PriceMultiplier { get; set; } = 1.0f;
        public float OneHandedWeaponTier4PriceMultiplier { get; set; } = 1.0f;
        public float OneHandedWeaponTier5PriceMultiplier { get; set; } = 1.0f;
        public float OneHandedWeaponTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item OneHandedWeapon


        #region Item Pistol
        public bool PistolWeightModifiers { get; set; } = true;
        public float PistolTier1WeightMultiplier { get; set; } = 1.0f;
        public float PistolTier2WeightMultiplier { get; set; } = 1.0f;
        public float PistolTier3WeightMultiplier { get; set; } = 1.0f;
        public float PistolTier4WeightMultiplier { get; set; } = 1.0f;
        public float PistolTier5WeightMultiplier { get; set; } = 1.0f;
        public float PistolTier6WeightMultiplier { get; set; } = 1.0f;
        public bool PistolValueModifiers { get; set; } = true;
        public float PistolTier1PriceMultiplier { get; set; } = 1.0f;
        public float PistolTier2PriceMultiplier { get; set; } = 1.0f;
        public float PistolTier3PriceMultiplier { get; set; } = 1.0f;
        public float PistolTier4PriceMultiplier { get; set; } = 1.0f;
        public float PistolTier5PriceMultiplier { get; set; } = 1.0f;
        public float PistolTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Pistol


        #region Item Polearm
        public bool PolearmWeightModifiers { get; set; } = true;
        public float PolearmTier1WeightMultiplier { get; set; } = 1.0f;
        public float PolearmTier2WeightMultiplier { get; set; } = 1.0f;
        public float PolearmTier3WeightMultiplier { get; set; } = 1.0f;
        public float PolearmTier4WeightMultiplier { get; set; } = 1.0f;
        public float PolearmTier5WeightMultiplier { get; set; } = 1.0f;
        public float PolearmTier6WeightMultiplier { get; set; } = 1.0f;
        public bool PolearmValueModifiers { get; set; } = true;
        public float PolearmTier1PriceMultiplier { get; set; } = 1.0f;
        public float PolearmTier2PriceMultiplier { get; set; } = 1.0f;
        public float PolearmTier3PriceMultiplier { get; set; } = 1.0f;
        public float PolearmTier4PriceMultiplier { get; set; } = 1.0f;
        public float PolearmTier5PriceMultiplier { get; set; } = 1.0f;
        public float PolearmTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Polearm


        #region Item Shield
        public bool ShieldWeightModifiers { get; set; } = true;
        public float ShieldTier1WeightMultiplier { get; set; } = 1.0f;
        public float ShieldTier2WeightMultiplier { get; set; } = 1.0f;
        public float ShieldTier3WeightMultiplier { get; set; } = 1.0f;
        public float ShieldTier4WeightMultiplier { get; set; } = 1.0f;
        public float ShieldTier5WeightMultiplier { get; set; } = 1.0f;
        public float ShieldTier6WeightMultiplier { get; set; } = 1.0f;
        public bool ShieldValueModifiers { get; set; } = true;
        public float ShieldTier1PriceMultiplier { get; set; } = 1.0f;
        public float ShieldTier2PriceMultiplier { get; set; } = 1.0f;
        public float ShieldTier3PriceMultiplier { get; set; } = 1.0f;
        public float ShieldTier4PriceMultiplier { get; set; } = 1.0f;
        public float ShieldTier5PriceMultiplier { get; set; } = 1.0f;
        public float ShieldTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Shield


        #region Item TwoHandedWeapon
        public bool TwoHandedWeaponWeightModifiers { get; set; } = true;
        public float TwoHandedWeaponTier1WeightMultiplier { get; set; } = 1.0f;
        public float TwoHandedWeaponTier2WeightMultiplier { get; set; } = 1.0f;
        public float TwoHandedWeaponTier3WeightMultiplier { get; set; } = 1.0f;
        public float TwoHandedWeaponTier4WeightMultiplier { get; set; } = 1.0f;
        public float TwoHandedWeaponTier5WeightMultiplier { get; set; } = 1.0f;
        public float TwoHandedWeaponTier6WeightMultiplier { get; set; } = 1.0f;
        public bool TwoHandedWeaponValueModifiers { get; set; } = true;
        public float TwoHandedWeaponTier1PriceMultiplier { get; set; } = 1.0f;
        public float TwoHandedWeaponTier2PriceMultiplier { get; set; } = 1.0f;
        public float TwoHandedWeaponTier3PriceMultiplier { get; set; } = 1.0f;
        public float TwoHandedWeaponTier4PriceMultiplier { get; set; } = 1.0f;
        public float TwoHandedWeaponTier5PriceMultiplier { get; set; } = 1.0f;
        public float TwoHandedWeaponTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item TwoHandedWeapon

        #endregion Items





        #region Battle Rewards

        #region  Battle Rewards RelationshipGain
        public bool BattleRewardsRelationShipGainModifiers { get; set; } = true;
        public float BattleRewardsRelationShipGainMultiplier { get; set; } = 1.0f;
        #endregion  Battle Rewards RelationshipGain

        #region  Battle Rewards Renown
        public bool BattleRewardsRenownGainModifiers { get; set; } = true;
        public float BattleRewardsRenownGainMultiplier { get; set; } = 1.0f;
        #endregion  Battle Rewards Renown

        #region  Battle Rewards InfluenceGain
        public bool BattleRewardsInfluenceGainModifiers { get; set; } = true;
        public float BattleRewardsInfluenceGainMultiplier { get; set; } = 1.0f;
        #endregion  Battle Rewards InfluenceGain

        #region  Battle Rewards MoraleGain
        public bool BattleRewardsMoraleGainModifiers { get; set; } = true;
        public float BattleRewardsMoraleGainMultiplier { get; set; } = 1.0f;
        #endregion  Battle Rewards MoraleGain

        #region  Battle Rewards GoldLossAfterDefeat
        public bool BattleRewardsGoldLossModifiers { get; set; } = true;
        public float BattleRewardsGoldLossMultiplier { get; set; } = 1.0f;
        #endregion  Battle Rewards GoldLossAfterDefeat


        #endregion  Battle Rewards




        #region Clan


        #region Clan Party Limit
        public bool ClanAdditionalPartyLimitForTierModifiers { get; set; } = false;
        public int ClanAdditionalPartyLimitForTierValue { get; set; } = 0;
        #endregion Clan Party Limit


        #region Clan Companion Limit
        public bool ClanAdditionaCompanionLimitForTierModifiers { get; set; } = false;
        public int ClanAdditionalCompanionLimitForTierValue { get; set; } = 0;
        #endregion Clan Companion Limit


        #endregion Clan


        #region Workshops

        #region Workshops Save Bankruptcy
        public bool WorkShopBankruptcyModifiers { get; set; } = false;
        public int WorkShopBankruptcyValue { get; set; } = 3;
        #endregion Save


        #region WorkShop Limit Player
        public bool WorkShopMaxWorkshopCountForPlayerModifiers { get; set; } = false;
        public int WorkShopMaxWorkshopCountForPlayerValue { get; set; } = 1;

        #endregion WorkShop Limit Player

        #endregion Workshops



        #region Character Development

        #region Character Development LevelsPerAttributePoint
        public bool CharacterLevelsPerAttributeModifiers { get; set; } = false;
        public int CharacterLevelsPerAttributeValue { get; set; } = 4;
        #endregion Character Development LevelsPerAttributePoint


        #region Character Development FocusPointsPerLevel
        public bool CharacterFocusPerLevelModifiers { get; set; } = false;
        public int CharacterFocusPerLevelValue { get; set; } = 1;
        #endregion Character Development FocusPointsPerLevel

        #endregion Character Development






        #region Pregnancy

        #region Pregnancy Duration
        public bool PregnancyDurationModifiers { get; set; } = false;
        public int PregnancyDurationValue { get; set; } = 36;
        #endregion Pregnancy Duration

        #region Pregnancy MortalityProbabilityInLabor
        public bool PregnancyLaborMortalityChanceModifiers { get; set; } = false;
        public float PregnancyLaborMortalityChanceValue { get; set; } = 0.015f;
        #endregion Pregnancy MortalityProbabilityInLabor

        #region Pregnancy StillbirthProbability
        public bool PregnancyStillbirthChanceModifiers { get; set; } = false;
        public float PregnancyStillbirthChanceValue { get; set; } = 0.01f;
        #endregion PregnancyStillbirthProbability

        #region Pregnancy DeliveringFemaleOffspringProbability
        public bool PregnancyFemaleOffspringChanceModifiers { get; set; } = false;
        public float PregnancyFemaleOffspringChanceValue { get; set; } = 0.51f;
        #endregion PregnancyDeliveringFemaleOffspringProbability

        #region Pregnancy DeliveringTwinsProbability
        public bool PregnancyTwinsChanceModifiers { get; set; } = false;
        public float PregnancyTwinsChanceValue { get; set; } = 0.03f;
        #endregion PregnancyDeliveringTwinsProbability


        #endregion Pregnancy





        #region Smithing


        #region  Smithing RefiningXp
        public bool SmithingRefiningXpModifiers { get; set; } = false;
        public float SmithingRefiningXpValue { get; set; } = 1.0f;
        #endregion  Smithing RefiningXp 

        #region  Smithing  XpForSmelting
        public bool SmithingSmeltingXpModifiers { get; set; } = false;
        public float SmithingSmeltingXpValue { get; set; } = 1.0f;
        #endregion  Smithing XpForSmelting

        #region  Smithing XpForSmithing
        public bool SmithingSmithingXpModifiers { get; set; } = false;
        public float SmithingSmithingXpValue { get; set; } = 1.0f;
        #endregion  Smithing XpForSmithing

        #region  Smithing Energy Disable
        public bool SmithingEnergyDisable { get; set; } = false;

        #endregion  Smithing Energy Disable


        #region  Smithing EnergyCostForRefining
        public bool SmithingEnergyRefiningModifiers { get; set; } = false;
        public float SmithingEnergyRefiningValue { get; set; } = 1.0f;
        #endregion  Smithing EnergyCostForRefining

        #region  Smithing EnergyCostForSmithing
        public bool SmithingEnergySmithingModifiers { get; set; } = false;
        public float SmithingEnergySmithingValue { get; set; } = 1.0f;
        #endregion  Smithing EnergyCostForSmithing

        #region  Smithing EnergyCostForSmelting
        public bool SmithingEnergySmeltingModifiers { get; set; } = false;
        public float SmithingEnergySmeltingValue { get; set; } = 1.0f;
        #endregion  Smithing EnergyCostForSmelting


        #endregion  Smithing




        #region ItemLocks

        public bool autoLockHorses { get; set; } = false;
        public bool autoLockFood { get; set; } = false;
        public bool autoLockIronBar1 { get; set; } = false;
        public bool autoLockIronBar2 { get; set; } = false;
        public bool autoLockIronBar3 { get; set; } = false;
        public bool autoLockIronBar4 { get; set; } = false;
        public bool autoLockIronBar5 { get; set; } = false;
        public bool autoLockIronBar6 { get; set; } = false;
        public bool autoLockIronOre { get; set; } = false;
        public bool autoLockSilverOre { get; set; } = false;
        public bool autoLockHardwood { get; set; } = false;
        public bool autoLockCharcol { get; set; } = false;


        #endregion ItemLocks


        #region ArmyManagement

        #region ArmyManagement Cohesion
        public bool armyCohesionMultipliers { get; set; } = false;
        public int armyCohesionBaseChange { get; set; } = -2;
        public bool armyDisableCohesionLossClanOnlyParties { get; set; } = false;
        public bool armyApplyMultiplerToClanOnlyParties { get; set; } = false;
        public float armyCohesionLossMultiplier { get; set; } = 1.0f;

        #endregion ArmyManagement Cohesion


        #endregion ArmyManagement





        #region Relations


        #region Relations KillingBandits
        public bool relationsKillingBanditsEnabled { get; set; } = false;
        public int GroupsOfBandits { get; set; } = 1;
        public int RelationshipIncrease { get; set; } = 1;
        public int Radius { get; set; } = 1000;
        public bool SizeBonusEnabled { get; set; } = false;
        public float SizeBonus { get; set; } = 0.05f;
        public bool PrisonersOnly { get; set; } = false;
        public bool IncludeBandits { get; set; } = false;
        public bool IncludeOutlaws { get; set; } = false;
        public bool IncludeMafia { get; set; } = false;

        #endregion Relations KillingBandits


        #endregion Relations




        #region SkillsXpMultipliers
        public bool SkillXpEnabled { get; set; } = false;

        public bool SkillXpUseForPlayer { get; set; } = false;
        public bool SkillXpUseForPlayerClan { get; set; } = false;
        public bool SkillXpUseForAI { get; set; } = false;

        public bool SkillXpUseIndividualMultiplers { get; set; } = false;
        #region SkillsXpMultipliers SpecificMultipliers
        public float SkillsXPMultiplierAthletics { get; set; } = 1.0f;
        public float SkillsXPMultiplierBow { get; set; } = 1.0f;
        public float SkillsXPMultiplierCharm { get; set; } = 1.0f;
        public float SkillsXPMultiplierCrafting { get; set; } = 1.0f;
        public float SkillsXPMultiplierCrossbow { get; set; } = 1.0f;
        public float SkillsXPMultiplierEngineering { get; set; } = 1.0f;
        public float SkillsXPMultiplierLeadership { get; set; } = 1.0f;
        public float SkillsXPMultiplierMedicine { get; set; } = 1.0f;
        public float SkillsXPMultiplierOneHanded { get; set; } = 1.0f;
        public float SkillsXPMultiplierPolearm { get; set; } = 1.0f;
        public float SkillsXPMultiplierRiding { get; set; } = 1.0f;
        public float SkillsXPMultiplierRoguery { get; set; } = 1.0f;
        public float SkillsXPMultiplierScouting { get; set; } = 1.0f;
        public float SkillsXPMultiplierSteward { get; set; } = 1.0f;
        public float SkillsXPMultiplierTactics { get; set; } = 1.0f;
        public float SkillsXPMultiplierThrowing { get; set; } = 1.0f;
        public float SkillsXPMultiplierTrade { get; set; } = 1.0f;
        public float SkillsXPMultiplierTwoHanded { get; set; } = 1.0f;
        #endregion SkillsXpMultipliers SpecificMultipliers

        public bool SkillXpUseGlobalMultipler { get; set; } = false;
        #region SkillsXpMultipliers Global
        public float SkillsXpGlobalMultiplier { get; set; } = 1.0f;
        #endregion SkillsXpMultipliers Global

        #endregion SkillsXpMultipliers





        #region LearningRateMultipliers
        public bool LearningRateEnabled { get; set; } = false;
        public float LearningRateMultiplier { get; set; } = 1.0f;

        #endregion LearningRate Multipliers


        #region MobileParty Food Consumption
        public bool PartyFoodConsumptionEnabled { get; set; } = false;
        public float PartyFoodConsumptionMultiplier { get; set; } = 1.0f;
        #endregion  MobileParty Food Consumption



    }
}
