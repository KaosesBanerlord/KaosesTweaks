using HarmonyLib;
using KaosesTweaks.Objects;
using KaosesTweaks.Settings;
using System;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.Localization;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultBuildingTypes), "InitializeAll")]
    class BuildingTypesPatch
    {
        static void Postfix(BuildingType ____buildingCastleTrainingFields, BuildingType ____buildingCastleGranary, BuildingType ____buildingCastleGardens,
            BuildingType ____buildingCastleMilitiaBarracks, BuildingType ____buildingSettlementTrainingFields, BuildingType ____buildingSettlementGranary,
            BuildingType ____buildingSettlementOrchard, BuildingType ____buildingSettlementMilitiaBarracks)
        {
            if (Factory.Settings is null) { return; }
            //Castle
            #region Training Fields
            if (Factory.Settings.CastleTrainingFieldsBonusEnabled)
            {
                ____buildingCastleTrainingFields?.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
                    new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
                    new int[3] { 500, 1000, 1500 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Experience,
                            Factory.Settings.CastleTrainingFieldsXpAmountLevel1,
                            Factory.Settings.CastleTrainingFieldsXpAmountLevel2,
                            Factory.Settings.CastleTrainingFieldsXpAmountLevel3
                        )
                    });
            }
            #endregion
            #region Granary
            if (Factory.Settings.CastleGranaryBonusEnabled)
            {
                ____buildingCastleGranary?.Initialize(new TextObject("{=PstO2f5I}Granary"),
                    new TextObject("{=iazij7fO}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
                    new int[3] { 1000, 1500, 2000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Foodstock,
                            Factory.Settings.CastleGranaryStorageAmountLevel1,
                            Factory.Settings.CastleGranaryStorageAmountLevel2,
                            Factory.Settings.CastleGranaryStorageAmountLevel3
                        )
                    });
            }
            #endregion
            #region Gardens
            if (Factory.Settings.CastleGardensBonusEnabled)
            {
                ____buildingCastleGardens?.Initialize(new TextObject("{=yT6XN4Mr}Gardens"),
                    new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege.", null),
                    new int[] { 500, 750, 1000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.FoodProduction,
                            Factory.Settings.CastleGardensFoodProductionAmountLevel1,
                            Factory.Settings.CastleGardensFoodProductionAmountLevel2,
                            Factory.Settings.CastleGardensFoodProductionAmountLevel3
                        )
                    });
            }
            #endregion
            #region Militia Barracks
            if (Factory.Settings.CastleMilitiaBarracksBonusEnabled)
            {
                ____buildingCastleMilitiaBarracks?.Initialize(new TextObject("{=l91xAgmU}Militia Grounds"),
                    new TextObject("{=YRrx8bAK}Provides battle training for citizens and recruit them into militia, each level increases daily militia recruitment."),
                    new int[3] { 500, 750, 1000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Militia,
                            Factory.Settings.CastleMilitiaBarracksAmountLevel1,
                            Factory.Settings.CastleMilitiaBarracksAmountLevel2,
                            Factory.Settings.CastleMilitiaBarracksAmountLevel3
                        )
                    });
            }
            #endregion

            //Town
            #region Training Fields
            if (Factory.Settings.TownTrainingFieldsBonusEnabled)
            {
                ____buildingSettlementTrainingFields?.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
                    new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                        BuildingEffectEnum.Experience,
                        Factory.Settings.TownTrainingFieldsXpAmountLevel1,
                        Factory.Settings.TownTrainingFieldsXpAmountLevel2,
                        Factory.Settings.TownTrainingFieldsXpAmountLevel3)
                    });
            }
            #endregion
            #region Granary
            if (Factory.Settings.TownGranaryBonusEnabled)
            {
                ____buildingSettlementGranary?.Initialize(new TextObject("{=PstO2f5I}Granary"),
                    new TextObject("{=aK23T43P}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
                    new int[3] { 1000, 1500, 2000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum,float,float,float>(
                            BuildingEffectEnum.Foodstock,
                            Factory.Settings.TownGranaryStorageAmountLevel1,
                            Factory.Settings.TownGranaryStorageAmountLevel2,
                            Factory.Settings.TownGranaryStorageAmountLevel3)
                    });
            }
            #endregion
            #region Orchards
            if (Factory.Settings.TownOrchardsBonusEnabled)
            {
                ____buildingSettlementOrchard?.Initialize(new TextObject("{=AkbiPIij}Orchards"),
                    new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                    new Tuple<BuildingEffectEnum,float,float,float>(
                        BuildingEffectEnum.FoodProduction,
                        Factory.Settings.TownOrchardsFoodProductionAmountLevel1,
                        Factory.Settings.TownOrchardsFoodProductionAmountLevel2,
                        Factory.Settings.TownOrchardsFoodProductionAmountLevel3)
                    });
            }
            #endregion
            #region Militia Barracks
            if (Factory.Settings.TownMilitiaBarracksBonusEnabled)
            {
                ____buildingSettlementMilitiaBarracks?.Initialize(new TextObject("{=l91xAgmU}Militia Grounds"),
                    new TextObject("{=RliyRJKl}Provides battle training for citizens and recruit them into militia. Increases daily militia recruitment."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Militia,
                            Factory.Settings.TownMilitiaBarracksAmountLevel1,
                            Factory.Settings.TownMilitiaBarracksAmountLevel2,
                            Factory.Settings.TownMilitiaBarracksAmountLevel3)
                    });
            }
            #endregion
        }

        static bool Prepare()
        {
            if (Factory.Settings == null) { return false; }
            return Factory.Settings.CastleGranaryBonusEnabled || Factory.Settings.CastleGardensBonusEnabled ||
                Factory.Settings.CastleTrainingFieldsBonusEnabled || Factory.Settings.CastleMilitiaBarracksBonusEnabled ||
                Factory.Settings.TownGranaryBonusEnabled || Factory.Settings.TownOrchardsBonusEnabled ||
                Factory.Settings.TownTrainingFieldsBonusEnabled || Factory.Settings.TownMilitiaBarracksBonusEnabled;
        }
    }
}
