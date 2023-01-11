using HarmonyLib;
using KaosesTweaks.Settings;
using System;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.Localization;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultBuildingTypes), "InitializeAll")]
    class BTBuildingTypesPatch
    {
        static void Postfix(BuildingType ____buildingCastleTrainingFields, BuildingType ____buildingCastleGranary, BuildingType ____buildingCastleGardens,
            BuildingType ____buildingCastleMilitiaBarracks, BuildingType ____buildingSettlementTrainingFields, BuildingType ____buildingSettlementGranary,
            BuildingType ____buildingSettlementOrchard, BuildingType ____buildingSettlementMilitiaBarracks)
        {
            if (KTSettings.Instance is null) { return; }
            //Castle
            #region Training Fields
            if (KTSettings.Instance.CastleTrainingFieldsBonusEnabled)
            {
                ____buildingCastleTrainingFields?.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
                    new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
                    new int[3] { 500, 1000, 1500 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Experience,
                            KTSettings.Instance.CastleTrainingFieldsXpAmountLevel1,
                            KTSettings.Instance.CastleTrainingFieldsXpAmountLevel2,
                            KTSettings.Instance.CastleTrainingFieldsXpAmountLevel3
                        )
                    });
            }
            #endregion
            #region Granary
            if (KTSettings.Instance.CastleGranaryBonusEnabled)
            {
                ____buildingCastleGranary?.Initialize(new TextObject("{=PstO2f5I}Granary"),
                    new TextObject("{=iazij7fO}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
                    new int[3] { 1000, 1500, 2000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Foodstock,
                            KTSettings.Instance.CastleGranaryStorageAmountLevel1,
                            KTSettings.Instance.CastleGranaryStorageAmountLevel2,
                            KTSettings.Instance.CastleGranaryStorageAmountLevel3
                        )
                    });
            }
            #endregion
            #region Gardens
            if (KTSettings.Instance.CastleGardensBonusEnabled)
            {
                ____buildingCastleGardens?.Initialize(new TextObject("{=yT6XN4Mr}Gardens"),
                    new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege.", null),
                    new int[] { 500, 750, 1000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.FoodProduction,
                            KTSettings.Instance.CastleGardensFoodProductionAmountLevel1,
                            KTSettings.Instance.CastleGardensFoodProductionAmountLevel2,
                            KTSettings.Instance.CastleGardensFoodProductionAmountLevel3
                        )
                    });
            }
            #endregion
            #region Militia Barracks
            if (KTSettings.Instance.CastleMilitiaBarracksBonusEnabled)
            {
                ____buildingCastleMilitiaBarracks?.Initialize(new TextObject("{=l91xAgmU}Militia Grounds"),
                    new TextObject("{=YRrx8bAK}Provides battle training for citizens and recruit them into militia, each level increases daily militia recruitment."),
                    new int[3] { 500, 750, 1000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Militia,
                            KTSettings.Instance.CastleMilitiaBarracksAmountLevel1,
                            KTSettings.Instance.CastleMilitiaBarracksAmountLevel2,
                            KTSettings.Instance.CastleMilitiaBarracksAmountLevel3
                        )
                    });
            }
            #endregion

            //Town
            #region Training Fields
            if (KTSettings.Instance.TownTrainingFieldsBonusEnabled)
            {
                ____buildingSettlementTrainingFields?.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
                    new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                        BuildingEffectEnum.Experience,
                        KTSettings.Instance.TownTrainingFieldsXpAmountLevel1,
                        KTSettings.Instance.TownTrainingFieldsXpAmountLevel2,
                        KTSettings.Instance.TownTrainingFieldsXpAmountLevel3)
                    });
            }
            #endregion
            #region Granary
            if (KTSettings.Instance.TownGranaryBonusEnabled)
            {
                ____buildingSettlementGranary?.Initialize(new TextObject("{=PstO2f5I}Granary"),
                    new TextObject("{=aK23T43P}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
                    new int[3] { 1000, 1500, 2000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum,float,float,float>(
                            BuildingEffectEnum.Foodstock,
                            KTSettings.Instance.TownGranaryStorageAmountLevel1,
                            KTSettings.Instance.TownGranaryStorageAmountLevel2,
                            KTSettings.Instance.TownGranaryStorageAmountLevel3)
                    });
            }
            #endregion
            #region Orchards
            if (KTSettings.Instance.TownOrchardsBonusEnabled)
            {
                ____buildingSettlementOrchard?.Initialize(new TextObject("{=AkbiPIij}Orchards"),
                    new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                    new Tuple<BuildingEffectEnum,float,float,float>(
                        BuildingEffectEnum.FoodProduction,
                        KTSettings.Instance.TownOrchardsFoodProductionAmountLevel1,
                        KTSettings.Instance.TownOrchardsFoodProductionAmountLevel2,
                        KTSettings.Instance.TownOrchardsFoodProductionAmountLevel3)
                    });
            }
            #endregion
            #region Militia Barracks
            if (KTSettings.Instance.TownMilitiaBarracksBonusEnabled)
            {
                ____buildingSettlementMilitiaBarracks?.Initialize(new TextObject("{=l91xAgmU}Militia Grounds"),
                    new TextObject("{=RliyRJKl}Provides battle training for citizens and recruit them into militia. Increases daily militia recruitment."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Militia,
                            KTSettings.Instance.TownMilitiaBarracksAmountLevel1,
                            KTSettings.Instance.TownMilitiaBarracksAmountLevel2,
                            KTSettings.Instance.TownMilitiaBarracksAmountLevel3)
                    });
            }
            #endregion
        }

        static bool Prepare()
        {
            if (KTSettings.Instance == null) { return false; }
            return KTSettings.Instance.CastleGranaryBonusEnabled || KTSettings.Instance.CastleGardensBonusEnabled ||
                KTSettings.Instance.CastleTrainingFieldsBonusEnabled || KTSettings.Instance.CastleMilitiaBarracksBonusEnabled ||
                KTSettings.Instance.TownGranaryBonusEnabled || KTSettings.Instance.TownOrchardsBonusEnabled ||
                KTSettings.Instance.TownTrainingFieldsBonusEnabled || KTSettings.Instance.TownMilitiaBarracksBonusEnabled;
        }
    }
}
