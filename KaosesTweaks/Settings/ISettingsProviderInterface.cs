
namespace KaosesTweaks.Settings
{
    public interface ISettingsProviderInterface
    {
        bool Debug { get; set; }
        bool LoadMCMConfigFile { get; set; }
        bool LogToFile { get; set; }
        string ModDisplayName { get; }


        ///~ Mod Specific settings 
        #region Items

        #region Item BodyArmor
        public bool BodyArmorWeightModifiers { get; set; }
        public float BodyArmorTier1WeightMultiplier { get; set; }
        public float BodyArmorTier2WeightMultiplier { get; set; }
        public float BodyArmorTier3WeightMultiplier { get; set; }
        public float BodyArmorTier4WeightMultiplier { get; set; }
        public float BodyArmorTier5WeightMultiplier { get; set; }
        public float BodyArmorTier6WeightMultiplier { get; set; }
        public bool BodyArmorValueModifiers { get; set; }
        public float BodyArmorTier1PriceMultiplier { get; set; }
        public float BodyArmorTier2PriceMultiplier { get; set; }
        public float BodyArmorTier3PriceMultiplier { get; set; }
        public float BodyArmorTier4PriceMultiplier { get; set; }
        public float BodyArmorTier5PriceMultiplier { get; set; }
        public float BodyArmorTier6PriceMultiplier { get; set; }
        #endregion Item BodyArmor

        #region Item Bow
        public bool BowWeightModifiers { get; set; }
        public float BowTier1WeightMultiplier { get; set; }
        public float BowTier2WeightMultiplier { get; set; }
        public float BowTier3WeightMultiplier { get; set; }
        public float BowTier4WeightMultiplier { get; set; }
        public float BowTier5WeightMultiplier { get; set; }
        public float BowTier6WeightMultiplier { get; set; }
        public bool BowValueModifiers { get; set; }
        public float BowTier1PriceMultiplier { get; set; }
        public float BowTier2PriceMultiplier { get; set; }
        public float BowTier3PriceMultiplier { get; set; }
        public float BowTier4PriceMultiplier { get; set; }
        public float BowTier5PriceMultiplier { get; set; }
        public float BowTier6PriceMultiplier { get; set; }
        #endregion Item Bow

        #region Item Cape
        public bool CapeWeightModifiers { get; set; }
        public float CapeTier1WeightMultiplier { get; set; }
        public float CapeTier2WeightMultiplier { get; set; }
        public float CapeTier3WeightMultiplier { get; set; }
        public float CapeTier4WeightMultiplier { get; set; }
        public float CapeTier5WeightMultiplier { get; set; }
        public float CapeTier6WeightMultiplier { get; set; }
        public bool CapeValueModifiers { get; set; }
        public float CapeTier1PriceMultiplier { get; set; }
        public float CapeTier2PriceMultiplier { get; set; }
        public float CapeTier3PriceMultiplier { get; set; }
        public float CapeTier4PriceMultiplier { get; set; }
        public float CapeTier5PriceMultiplier { get; set; }
        public float CapeTier6PriceMultiplier { get; set; }
        #endregion Item Cape

        #region Item ChestArmor
        public bool ChestArmorWeightModifiers { get; set; }
        public float ChestArmorTier1WeightMultiplier { get; set; }
        public float ChestArmorTier2WeightMultiplier { get; set; }
        public float ChestArmorTier3WeightMultiplier { get; set; }
        public float ChestArmorTier4WeightMultiplier { get; set; }
        public float ChestArmorTier5WeightMultiplier { get; set; }
        public float ChestArmorTier6WeightMultiplier { get; set; }
        public bool ChestArmorValueModifiers { get; set; }
        public float ChestArmorTier1PriceMultiplier { get; set; }
        public float ChestArmorTier2PriceMultiplier { get; set; }
        public float ChestArmorTier3PriceMultiplier { get; set; }
        public float ChestArmorTier4PriceMultiplier { get; set; }
        public float ChestArmorTier5PriceMultiplier { get; set; }
        public float ChestArmorTier6PriceMultiplier { get; set; }
        #endregion Item ChestArmor

        #region Item Crossbow
        public bool CrossbowWeightModifiers { get; set; }
        public float CrossbowTier1WeightMultiplier { get; set; }
        public float CrossbowTier2WeightMultiplier { get; set; }
        public float CrossbowTier3WeightMultiplier { get; set; }
        public float CrossbowTier4WeightMultiplier { get; set; }
        public float CrossbowTier5WeightMultiplier { get; set; }
        public float CrossbowTier6WeightMultiplier { get; set; }
        public bool CrossbowValueModifiers { get; set; }
        public float CrossbowTier1PriceMultiplier { get; set; }
        public float CrossbowTier2PriceMultiplier { get; set; }
        public float CrossbowTier3PriceMultiplier { get; set; }
        public float CrossbowTier4PriceMultiplier { get; set; }
        public float CrossbowTier5PriceMultiplier { get; set; }
        public float CrossbowTier6PriceMultiplier { get; set; }
        #endregion Item Crossbow

        #region Item HandArmor
        public bool HandArmorWeightModifiers { get; set; }
        public float HandArmorTier1WeightMultiplier { get; set; }
        public float HandArmorTier2WeightMultiplier { get; set; }
        public float HandArmorTier3WeightMultiplier { get; set; }
        public float HandArmorTier4WeightMultiplier { get; set; }
        public float HandArmorTier5WeightMultiplier { get; set; }
        public float HandArmorTier6WeightMultiplier { get; set; }
        public bool HandArmorValueModifiers { get; set; }
        public float HandArmorTier1PriceMultiplier { get; set; }
        public float HandArmorTier2PriceMultiplier { get; set; }
        public float HandArmorTier3PriceMultiplier { get; set; }
        public float HandArmorTier4PriceMultiplier { get; set; }
        public float HandArmorTier5PriceMultiplier { get; set; }
        public float HandArmorTier6PriceMultiplier { get; set; }
        #endregion Item HandArmor

        #region Item HeadArmor
        public bool HeadArmorWeightModifiers { get; set; }
        public float HeadArmorTier1WeightMultiplier { get; set; }
        public float HeadArmorTier2WeightMultiplier { get; set; }
        public float HeadArmorTier3WeightMultiplier { get; set; }
        public float HeadArmorTier4WeightMultiplier { get; set; }
        public float HeadArmorTier5WeightMultiplier { get; set; }
        public float HeadArmorTier6WeightMultiplier { get; set; }
        public bool HeadArmorValueModifiers { get; set; }
        public float HeadArmorTier1PriceMultiplier { get; set; }
        public float HeadArmorTier2PriceMultiplier { get; set; }
        public float HeadArmorTier3PriceMultiplier { get; set; }
        public float HeadArmorTier4PriceMultiplier { get; set; }
        public float HeadArmorTier5PriceMultiplier { get; set; }
        public float HeadArmorTier6PriceMultiplier { get; set; }
        #endregion Item HeadArmor


        #region Item Horse
        public bool HorseWeightModifiers { get; set; }
        public float HorseTier1WeightMultiplier { get; set; }
        public float HorseTier2WeightMultiplier { get; set; }
        public float HorseTier3WeightMultiplier { get; set; }
        public float HorseTier4WeightMultiplier { get; set; }
        public float HorseTier5WeightMultiplier { get; set; }
        public float HorseTier6WeightMultiplier { get; set; }
        public bool HorseValueModifiers { get; set; }
        public float HorseTier1PriceMultiplier { get; set; }
        public float HorseTier2PriceMultiplier { get; set; }
        public float HorseTier3PriceMultiplier { get; set; }
        public float HorseTier4PriceMultiplier { get; set; }
        public float HorseTier5PriceMultiplier { get; set; }
        public float HorseTier6PriceMultiplier { get; set; }
        #endregion Item Horse

        #region Item HorseHarness
        public bool HorseHarnessWeightModifiers { get; set; }
        public float HorseHarnessTier1WeightMultiplier { get; set; }
        public float HorseHarnessTier2WeightMultiplier { get; set; }
        public float HorseHarnessTier3WeightMultiplier { get; set; }
        public float HorseHarnessTier4WeightMultiplier { get; set; }
        public float HorseHarnessTier5WeightMultiplier { get; set; }
        public float HorseHarnessTier6WeightMultiplier { get; set; }
        public bool HorseHarnessValueModifiers { get; set; }
        public float HorseHarnessTier1PriceMultiplier { get; set; }
        public float HorseHarnessTier2PriceMultiplier { get; set; }
        public float HorseHarnessTier3PriceMultiplier { get; set; }
        public float HorseHarnessTier4PriceMultiplier { get; set; }
        public float HorseHarnessTier5PriceMultiplier { get; set; }
        public float HorseHarnessTier6PriceMultiplier { get; set; }
        #endregion Item HorseHarness


        #region Item LegArmor
        public bool LegArmorWeightModifiers { get; set; }
        public float LegArmorTier1WeightMultiplier { get; set; }
        public float LegArmorTier2WeightMultiplier { get; set; }
        public float LegArmorTier3WeightMultiplier { get; set; }
        public float LegArmorTier4WeightMultiplier { get; set; }
        public float LegArmorTier5WeightMultiplier { get; set; }
        public float LegArmorTier6WeightMultiplier { get; set; }
        public bool LegArmorValueModifiers { get; set; }
        public float LegArmorTier1PriceMultiplier { get; set; }
        public float LegArmorTier2PriceMultiplier { get; set; }
        public float LegArmorTier3PriceMultiplier { get; set; }
        public float LegArmorTier4PriceMultiplier { get; set; }
        public float LegArmorTier5PriceMultiplier { get; set; }
        public float LegArmorTier6PriceMultiplier { get; set; }
        #endregion Item LegArmor


        #region Item Musket
        public bool MusketWeightModifiers { get; set; }
        public float MusketTier1WeightMultiplier { get; set; }
        public float MusketTier2WeightMultiplier { get; set; }
        public float MusketTier3WeightMultiplier { get; set; }
        public float MusketTier4WeightMultiplier { get; set; }
        public float MusketTier5WeightMultiplier { get; set; }
        public float MusketTier6WeightMultiplier { get; set; }
        public bool MusketValueModifiers { get; set; }
        public float MusketTier1PriceMultiplier { get; set; }
        public float MusketTier2PriceMultiplier { get; set; }
        public float MusketTier3PriceMultiplier { get; set; }
        public float MusketTier4PriceMultiplier { get; set; }
        public float MusketTier5PriceMultiplier { get; set; }
        public float MusketTier6PriceMultiplier { get; set; }
        #endregion Item Musket


        #region Item OneHandedWeapon
        public bool OneHandedWeaponWeightModifiers { get; set; }
        public float OneHandedWeaponTier1WeightMultiplier { get; set; }
        public float OneHandedWeaponTier2WeightMultiplier { get; set; }
        public float OneHandedWeaponTier3WeightMultiplier { get; set; }
        public float OneHandedWeaponTier4WeightMultiplier { get; set; }
        public float OneHandedWeaponTier5WeightMultiplier { get; set; }
        public float OneHandedWeaponTier6WeightMultiplier { get; set; }
        public bool OneHandedWeaponValueModifiers { get; set; }
        public float OneHandedWeaponTier1PriceMultiplier { get; set; }
        public float OneHandedWeaponTier2PriceMultiplier { get; set; }
        public float OneHandedWeaponTier3PriceMultiplier { get; set; }
        public float OneHandedWeaponTier4PriceMultiplier { get; set; }
        public float OneHandedWeaponTier5PriceMultiplier { get; set; }
        public float OneHandedWeaponTier6PriceMultiplier { get; set; }
        #endregion Item OneHandedWeapon


        #region Item Pistol
        public bool PistolWeightModifiers { get; set; }
        public float PistolTier1WeightMultiplier { get; set; }
        public float PistolTier2WeightMultiplier { get; set; }
        public float PistolTier3WeightMultiplier { get; set; }
        public float PistolTier4WeightMultiplier { get; set; }
        public float PistolTier5WeightMultiplier { get; set; }
        public float PistolTier6WeightMultiplier { get; set; }
        public bool PistolValueModifiers { get; set; }
        public float PistolTier1PriceMultiplier { get; set; }
        public float PistolTier2PriceMultiplier { get; set; }
        public float PistolTier3PriceMultiplier { get; set; }
        public float PistolTier4PriceMultiplier { get; set; }
        public float PistolTier5PriceMultiplier { get; set; }
        public float PistolTier6PriceMultiplier { get; set; }
        #endregion Item Pistol


        #region Item Polearm
        public bool PolearmWeightModifiers { get; set; }
        public float PolearmTier1WeightMultiplier { get; set; }
        public float PolearmTier2WeightMultiplier { get; set; }
        public float PolearmTier3WeightMultiplier { get; set; }
        public float PolearmTier4WeightMultiplier { get; set; }
        public float PolearmTier5WeightMultiplier { get; set; }
        public float PolearmTier6WeightMultiplier { get; set; }
        public bool PolearmValueModifiers { get; set; }
        public float PolearmTier1PriceMultiplier { get; set; }
        public float PolearmTier2PriceMultiplier { get; set; }
        public float PolearmTier3PriceMultiplier { get; set; }
        public float PolearmTier4PriceMultiplier { get; set; }
        public float PolearmTier5PriceMultiplier { get; set; }
        public float PolearmTier6PriceMultiplier { get; set; }
        #endregion Item Polearm


        #region Item Shield
        public bool ShieldWeightModifiers { get; set; }
        public float ShieldTier1WeightMultiplier { get; set; }
        public float ShieldTier2WeightMultiplier { get; set; }
        public float ShieldTier3WeightMultiplier { get; set; }
        public float ShieldTier4WeightMultiplier { get; set; }
        public float ShieldTier5WeightMultiplier { get; set; }
        public float ShieldTier6WeightMultiplier { get; set; }
        public bool ShieldValueModifiers { get; set; }
        public float ShieldTier1PriceMultiplier { get; set; }
        public float ShieldTier2PriceMultiplier { get; set; }
        public float ShieldTier3PriceMultiplier { get; set; }
        public float ShieldTier4PriceMultiplier { get; set; }
        public float ShieldTier5PriceMultiplier { get; set; }
        public float ShieldTier6PriceMultiplier { get; set; }
        #endregion Item Shield

        #region Item TwoHandedWeapon
        public bool TwoHandedWeaponWeightModifiers { get; set; }
        public float TwoHandedWeaponTier1WeightMultiplier { get; set; }
        public float TwoHandedWeaponTier2WeightMultiplier { get; set; }
        public float TwoHandedWeaponTier3WeightMultiplier { get; set; }
        public float TwoHandedWeaponTier4WeightMultiplier { get; set; }
        public float TwoHandedWeaponTier5WeightMultiplier { get; set; }
        public float TwoHandedWeaponTier6WeightMultiplier { get; set; }
        public bool TwoHandedWeaponValueModifiers { get; set; }
        public float TwoHandedWeaponTier1PriceMultiplier { get; set; }
        public float TwoHandedWeaponTier2PriceMultiplier { get; set; }
        public float TwoHandedWeaponTier3PriceMultiplier { get; set; }
        public float TwoHandedWeaponTier4PriceMultiplier { get; set; }
        public float TwoHandedWeaponTier5PriceMultiplier { get; set; }
        public float TwoHandedWeaponTier6PriceMultiplier { get; set; }
        #endregion Item TwoHandedWeapon


        #endregion Items


        #region Battle Rewards

        #region  Battle Rewards RelationshipGain
        public bool BattleRewardsRelationShipGainModifiers { get; set; }
        public float BattleRewardsRelationShipGainMultiplier { get; set; }
        #endregion  Battle Rewards RelationshipGain

        #region  Battle Rewards Renown
        public bool BattleRewardsRenownGainModifiers { get; set; }
        public float BattleRewardsRenownGainMultiplier { get; set; }
        #endregion  Battle Rewards Renown

        #region  Battle Rewards InfluenceGain
        public bool BattleRewardsInfluenceGainModifiers { get; set; }
        public float BattleRewardsInfluenceGainMultiplier { get; set; }
        #endregion  Battle Rewards InfluenceGain

        #region  Battle Rewards MoraleGain
        public bool BattleRewardsMoraleGainModifiers { get; set; }
        public float BattleRewardsMoraleGainMultiplier { get; set; }
        #endregion  Battle Rewards MoraleGain

        #region  Battle Rewards GoldLossAfterDefeat
        public bool BattleRewardsGoldLossModifiers { get; set; }
        public float BattleRewardsGoldLossMultiplier { get; set; }
        #endregion  Battle Rewards GoldLossAfterDefeat


        #endregion  Battle Rewards


        #region Clan


        #region Clan Party Limit
        public bool ClanAdditionalPartyLimitForTierModifiers { get; set; }
        public int ClanAdditionalPartyLimitForTierValue { get; set; }
        #endregion Clan Party Limit

        #region Clan Companion Limit
        public bool ClanAdditionaCompanionLimitForTierModifiers { get; set; }
        public int ClanAdditionalCompanionLimitForTierValue { get; set; }
        #endregion Clan Companion Limit

        #endregion Clan


        #region Workshops

        #region Workshops Save Bankruptcy
        public bool WorkShopBankruptcyModifiers { get; set; }
        public int WorkShopBankruptcyValue { get; set; }
        #endregion Save


        #region WorkShop Limit Player
        public bool WorkShopMaxWorkshopCountForPlayerModifiers { get; set; }
        public int WorkShopMaxWorkshopCountForPlayerValue { get; set; }

        #endregion WorkShop Limit Player

        #endregion Workshops





        #region Character Development

/*
        #region Character Development Start Attributes
        public bool CharacterAttributesAtStartModifiers { get; set; }
        public int CharacterAttributesAtStartValue { get; set; }
        #endregion Character Development Start Attributes*/


        #region Character Development LevelsPerAttributePoint
        public bool CharacterLevelsPerAttributeModifiers { get; set; }
        public int CharacterLevelsPerAttributeValue { get; set; }
        #endregion Character Development LevelsPerAttributePoint



/*
        #region Character Development FocusPointsPerLevel
        public bool CharacterFocusAtStartModifiers { get; set; }
        public int CharacterFocusAtStartValue { get; set; }
        #endregion Character Development FocusPointsPerLevel*/



        #region Character Development FocusPointsPerLevel
        public bool CharacterFocusPerLevelModifiers { get; set; }
        public int CharacterFocusPerLevelValue { get; set; }
        #endregion Character Development FocusPointsPerLevel

        #endregion Character Development




        #region Pregnancy

        #region Pregnancy Duration
        public bool PregnancyDurationModifiers { get; set; }
        public int PregnancyDurationValue { get; set; }
        #endregion Pregnancy Duration

        #region Pregnancy MortalityProbabilityInLabor
        public bool PregnancyLaborMortalityChanceModifiers { get; set; }
        public float PregnancyLaborMortalityChanceValue { get; set; }
        #endregion Pregnancy MortalityProbabilityInLabor

        #region Pregnancy StillbirthProbability
        public bool PregnancyStillbirthChanceModifiers { get; set; }
        public float PregnancyStillbirthChanceValue { get; set; }
        #endregion PregnancyStillbirthProbability

        #region Pregnancy DeliveringFemaleOffspringProbability
        public bool PregnancyFemaleOffspringChanceModifiers { get; set; }
        public float PregnancyFemaleOffspringChanceValue { get; set; }
        #endregion PregnancyDeliveringFemaleOffspringProbability

        #region Pregnancy DeliveringTwinsProbability
        public bool PregnancyTwinsChanceModifiers { get; set; }
        public float PregnancyTwinsChanceValue { get; set; }
        #endregion PregnancyDeliveringTwinsProbability


        #endregion Pregnancy





        #region Smithing


        #region  Smithing RefiningXp
        public bool SmithingRefiningXpModifiers { get; set; }
        public float SmithingRefiningXpValue { get; set; }
        #endregion  Smithing RefiningXp 

        #region  Smithing  XpForSmelting
        public bool SmithingSmeltingXpModifiers { get; set; }
        public float SmithingSmeltingXpValue { get; set; }
        #endregion  Smithing XpForSmelting

        #region  Smithing XpForSmithing
        public bool SmithingSmithingXpModifiers { get; set; }
        public float SmithingSmithingXpValue { get; set; }
        #endregion  Smithing XpForSmithing

        #region  Smithing Energy Disable
        public bool SmithingEnergyDisable { get; set; }

        #endregion  Smithing Energy Disable


        #region  Smithing EnergyCostForRefining
        public bool SmithingEnergyRefiningModifiers { get; set; }
        public float SmithingEnergyRefiningValue { get; set; }
        #endregion  Smithing EnergyCostForRefining

        #region  Smithing EnergyCostForSmithing
        public bool SmithingEnergySmithingModifiers { get; set; }
        public float SmithingEnergySmithingValue { get; set; }
        #endregion  Smithing EnergyCostForSmithing

        #region  Smithing EnergyCostForSmelting
        public bool SmithingEnergySmeltingModifiers { get; set; }
        public float SmithingEnergySmeltingValue { get; set; }
        #endregion  Smithing EnergyCostForSmelting


        #endregion  Smithing




        #region ItemLocks

        public bool autoLockHorses { get; set; }
        public bool autoLockFood { get; set; }
        public bool autoLockIronBar1 { get; set; }
        public bool autoLockIronBar2 { get; set; }
        public bool autoLockIronBar3 { get; set; }
        public bool autoLockIronBar4 { get; set; }
        public bool autoLockIronBar5 { get; set; }
        public bool autoLockIronBar6 { get; set; }
        public bool autoLockIronOre { get; set; }
        public bool autoLockSilverOre { get; set; }
        public bool autoLockHardwood { get; set; }
        public bool autoLockCharcol { get; set; }


        //public bool autoLockBetterGear { get; set; }

        #endregion ItemLocks



        #region ArmyManagement

        #region ArmyManagement Cohesion
        public bool armyCohesionMultipliers { get; set; }
        public int armyCohesionBaseChange { get; set; }
        public bool armyDisableCohesionLossClanOnlyParties { get; set; }
        public bool armyApplyMultiplerToClanOnlyParties { get; set; }
        public float armyCohesionLossMultiplier { get; set; }

        #endregion ArmyManagement Cohesion


        #endregion ArmyManagement


        #region Relations


        #region Relations KillingBandits
        public bool relationsKillingBanditsEnabled { get; set; }
        public int GroupsOfBandits { get; set; }
        public int RelationshipIncrease { get; set; }
        public int Radius { get; set; }
        public bool SizeBonusEnabled { get; set; }
        public float SizeBonus { get; set; }
        public bool PrisonersOnly { get; set; }
        public bool IncludeBandits { get; set; }
        public bool IncludeOutlaws { get; set; }
        public bool IncludeMafia { get; set; }

        #endregion Relations KillingBandits


        #endregion Relations


        #region SkillsXpMultipliers
        public bool SkillXpEnabled { get; set; }

        public bool SkillXpUseForPlayer { get; set; }
        public bool SkillXpUseForPlayerClan { get; set; }
        public bool SkillXpUseForAI { get; set; }

        public bool SkillXpUseIndividualMultiplers { get; set; }
        #region SkillsXpMultipliers SpecificMultipliers
        public float SkillsXPMultiplierAthletics { get; set; }
        public float SkillsXPMultiplierBow { get; set; }
        public float SkillsXPMultiplierCharm { get; set; }
        public float SkillsXPMultiplierCrafting { get; set; }
        public float SkillsXPMultiplierCrossbow { get; set; }
        public float SkillsXPMultiplierEngineering { get; set; }
        public float SkillsXPMultiplierLeadership { get; set; }
        public float SkillsXPMultiplierMedicine { get; set; }
        public float SkillsXPMultiplierOneHanded { get; set; }
        public float SkillsXPMultiplierPolearm { get; set; }
        public float SkillsXPMultiplierRiding { get; set; }
        public float SkillsXPMultiplierRoguery { get; set; }
        public float SkillsXPMultiplierScouting { get; set; }
        public float SkillsXPMultiplierSteward { get; set; }
        public float SkillsXPMultiplierTactics { get; set; }
        public float SkillsXPMultiplierThrowing { get; set; }
        public float SkillsXPMultiplierTrade { get; set; }
        public float SkillsXPMultiplierTwoHanded { get; set; }
        #endregion SkillsXpMultipliers SpecificMultipliers

        public bool SkillXpUseGlobalMultipler { get; set; }
        #region SkillsXpMultipliers Global
        public float SkillsXpGlobalMultiplier { get; set; }
        #endregion SkillsXpMultipliers Global

        #endregion SkillsXpMultipliers




        #region LearningRateMultipliers
        public bool LearningRateEnabled { get; set; }
        public float LearningRateMultiplier { get; set; }

        #endregion LearningRate Multipliers



        #region MobileParty Food Consumption
        public bool PartyFoodConsumptionEnabled { get; set; }
        public float PartyFoodConsumptionMultiplier { get; set; }
        #endregion  MobileParty Food Consumption









    }
}
