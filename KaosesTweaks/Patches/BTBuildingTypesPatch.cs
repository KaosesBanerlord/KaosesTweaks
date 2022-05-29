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
            if (MCMSettings.Instance is null) { return; }
            //Castle
            #region Training Fields
            if (MCMSettings.Instance.CastleTrainingFieldsBonusEnabled)
            {
                ____buildingCastleTrainingFields?.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
                    new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
                    new int[3] { 500, 1000, 1500 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Experience,
                            MCMSettings.Instance.CastleTrainingFieldsXpAmountLevel1,
                            MCMSettings.Instance.CastleTrainingFieldsXpAmountLevel2,
                            MCMSettings.Instance.CastleTrainingFieldsXpAmountLevel3
                        )
                    });
            }
            #endregion
            #region Granary
            if (MCMSettings.Instance.CastleGranaryBonusEnabled)
            {
                ____buildingCastleGranary?.Initialize(new TextObject("{=PstO2f5I}Granary"),
                    new TextObject("{=iazij7fO}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
                    new int[3] { 1000, 1500, 2000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Foodstock,
                            MCMSettings.Instance.CastleGranaryStorageAmountLevel1,
                            MCMSettings.Instance.CastleGranaryStorageAmountLevel2,
                            MCMSettings.Instance.CastleGranaryStorageAmountLevel3
                        )
                    });
            }
            #endregion
            #region Gardens
            if (MCMSettings.Instance.CastleGardensBonusEnabled)
            {
                ____buildingCastleGardens?.Initialize(new TextObject("{=yT6XN4Mr}Gardens"),
                    new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege.", null),
                    new int[] { 500, 750, 1000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.FoodProduction,
                            MCMSettings.Instance.CastleGardensFoodProductionAmountLevel1,
                            MCMSettings.Instance.CastleGardensFoodProductionAmountLevel2,
                            MCMSettings.Instance.CastleGardensFoodProductionAmountLevel3
                        )
                    });
            }
            #endregion
            #region Militia Barracks
            if (MCMSettings.Instance.CastleMilitiaBarracksBonusEnabled)
            {
                ____buildingCastleMilitiaBarracks?.Initialize(new TextObject("{=l91xAgmU}Militia Grounds"),
                    new TextObject("{=YRrx8bAK}Provides battle training for citizens and recruit them into militia, each level increases daily militia recruitment."),
                    new int[3] { 500, 750, 1000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Militia,
                            MCMSettings.Instance.CastleMilitiaBarracksAmountLevel1,
                            MCMSettings.Instance.CastleMilitiaBarracksAmountLevel2,
                            MCMSettings.Instance.CastleMilitiaBarracksAmountLevel3
                        )
                    });
            }
            #endregion

            //Town
            #region Training Fields
            if (MCMSettings.Instance.TownTrainingFieldsBonusEnabled)
            {
                ____buildingSettlementTrainingFields?.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
                    new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                        BuildingEffectEnum.Experience,
                        MCMSettings.Instance.TownTrainingFieldsXpAmountLevel1,
                        MCMSettings.Instance.TownTrainingFieldsXpAmountLevel2,
                        MCMSettings.Instance.TownTrainingFieldsXpAmountLevel3)
                    });
            }
            #endregion
            #region Granary
            if (MCMSettings.Instance.TownGranaryBonusEnabled)
            {
                ____buildingSettlementGranary?.Initialize(new TextObject("{=PstO2f5I}Granary"),
                    new TextObject("{=aK23T43P}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
                    new int[3] { 1000, 1500, 2000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum,float,float,float>(
                            BuildingEffectEnum.Foodstock,
                            MCMSettings.Instance.TownGranaryStorageAmountLevel1,
                            MCMSettings.Instance.TownGranaryStorageAmountLevel2,
                            MCMSettings.Instance.TownGranaryStorageAmountLevel3)
                    });
            }
            #endregion
            #region Orchards
            if (MCMSettings.Instance.TownOrchardsBonusEnabled)
            {
                ____buildingSettlementOrchard?.Initialize(new TextObject("{=AkbiPIij}Orchards"),
                    new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                    new Tuple<BuildingEffectEnum,float,float,float>(
                        BuildingEffectEnum.FoodProduction,
                        MCMSettings.Instance.TownOrchardsFoodProductionAmountLevel1,
                        MCMSettings.Instance.TownOrchardsFoodProductionAmountLevel2,
                        MCMSettings.Instance.TownOrchardsFoodProductionAmountLevel3)
                    });
            }
            #endregion
            #region Militia Barracks
            if (MCMSettings.Instance.TownMilitiaBarracksBonusEnabled)
            {
                ____buildingSettlementMilitiaBarracks?.Initialize(new TextObject("{=l91xAgmU}Militia Grounds"),
                    new TextObject("{=RliyRJKl}Provides battle training for citizens and recruit them into militia. Increases daily militia recruitment."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Militia,
                            MCMSettings.Instance.TownMilitiaBarracksAmountLevel1,
                            MCMSettings.Instance.TownMilitiaBarracksAmountLevel2,
                            MCMSettings.Instance.TownMilitiaBarracksAmountLevel3)
                    });
            }
            #endregion
        }

        static bool Prepare()
        {
            if (MCMSettings.Instance == null) { return false; }
            return MCMSettings.Instance.CastleGranaryBonusEnabled || MCMSettings.Instance.CastleGardensBonusEnabled ||
                MCMSettings.Instance.CastleTrainingFieldsBonusEnabled || MCMSettings.Instance.CastleMilitiaBarracksBonusEnabled ||
                MCMSettings.Instance.TownGranaryBonusEnabled || MCMSettings.Instance.TownOrchardsBonusEnabled ||
                MCMSettings.Instance.TownTrainingFieldsBonusEnabled || MCMSettings.Instance.TownMilitiaBarracksBonusEnabled;
        }
    }
}
