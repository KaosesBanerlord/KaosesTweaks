using KaosesTweaks.Utils;
using TaleWorlds.Core;
using TaleWorlds.Library;
using static TaleWorlds.Core.ItemObject;

namespace KaosesTweaks.Items
{
    public static class TradeGoods
    {
        public static void processAnimalGoodsWeight(MBReadOnlyList<ItemObject> ItemsList)
        {
            for (int i = 0; i < ItemsList.Count; i++)
            {
                var item = ItemsList[i];
                if (item.ItemType == ItemTypeEnum.Animal)
                {
                    //float multipleValue = item.Weight * Statics._settings.weightAnimalMultiplier;
                    ////DebugWeight(item, multipleValue, Statics._settings.weightAnimalMultiplier);
                    //typeof(ItemObject).GetProperty("Weight").SetValue(item, multipleValue);
                }
            }
        }

        public static void processAnimalGoodsValue(MBReadOnlyList<ItemObject> ItemsList)
        {
            for (int i = 0; i < ItemsList.Count; i++)
            {
                var item = ItemsList[i];
                if (item.ItemType == ItemTypeEnum.Animal)
                {
                    //float multipleValue = item.Value * Statics._settings.valueAnimalMultiplier;
                    //int newValue = (int)multipleValue;
                    ////DebugValue(item, multipleValue, Statics._settings.valueAnimalMultiplier);
                    //typeof(ItemObject).GetProperty("Value").SetValue(item, newValue);
                }
            }
        }

        public static void processFoodGoodsWeight(MBReadOnlyList<ItemObject> ItemsList)
        {
            for (int i = 0; i < ItemsList.Count; i++)
            {
                var item = ItemsList[i];
                if (item.IsFood)
                {
                    //float multipleValue = item.Weight * Statics._settings.weightFoodMultiplier;
                    ////DebugWeight(item, multipleValue, Statics._settings.weightFoodMultiplier);
                    //typeof(ItemObject).GetProperty("Weight").SetValue(item, multipleValue);
                }
            }
        }

        public static void processFoodGoodsValue(MBReadOnlyList<ItemObject> ItemsList)
        {
            for (int i = 0; i < ItemsList.Count; i++)
            {
                var item = ItemsList[i];
                if (item.IsFood)
                {
                    //float multipleValue = item.Value * Statics._settings.valueFoodMultiplier;
                    //int newValue = (int)multipleValue;
                   // //DebugValue(item, multipleValue, Statics._settings.valueFoodMultiplier);
                    //typeof(ItemObject).GetProperty("Value").SetValue(item, newValue);
                }
            }
        }

        public static void processFoodGoodsWeightByMoral(MBReadOnlyList<ItemObject> ItemsList)
        {
            for (int i = 0; i < ItemsList.Count; i++)
            {
                var item = ItemsList[i];
                if (item.IsFood)
                {
                    float multiplier = 1.0f;
                    if (item.HasFoodComponent)
                    {
                        TradeItemComponent tc = item.FoodComponent;
                        if (tc.MoraleBonus == 0)
                        {
                            //multiplier = Statics._settings.weightFoodByMoral0Multiplier;
                        }
                        else if (tc.MoraleBonus == 1)
                        {
                            //multiplier = Statics._settings.weightFoodByMoral1Multiplier;
                        }
                        else if (tc.MoraleBonus == 2)
                        {
                            //multiplier = Statics._settings.weightFoodByMoral2Multiplier;
                        }
                        else if (tc.MoraleBonus == 3)
                        {
                            //multiplier = Statics._settings.weightFoodByMoral3Multiplier;
                        }
                        float multipleValue = item.Weight * multiplier;
                        //DebugWeight(item, multipleValue, multiplier);
                        typeof(ItemObject).GetProperty("Weight").SetValue(item, multipleValue);
                    }
                }
            }
        }

        public static void processFoodGoodsValueByMoral(MBReadOnlyList<ItemObject> ItemsList)
        {
            for (int i = 0; i < ItemsList.Count; i++)
            {
                var item = ItemsList[i];
                if (item.IsFood)
                {
                    float multiplier = 1.0f;
                    if (item.HasFoodComponent)
                    {
                        TradeItemComponent tc = item.FoodComponent;
                        if (tc.MoraleBonus == 0)
                        {
                            //multiplier = Statics._settings.valueFoodByMoral0Multiplier;
                        }
                        else if (tc.MoraleBonus == 1)
                        {
                            //multiplier = Statics._settings.valueFoodByMoral1Multiplier;
                        }
                        else if (tc.MoraleBonus == 2)
                        {
                            //multiplier = Statics._settings.valueFoodByMoral2Multiplier;
                        }
                        else if (tc.MoraleBonus == 3)
                        {
                            //multiplier = Statics._settings.valueFoodByMoral3Multiplier;
                        }
                        float multipleValue = item.Value * multiplier;
                        int newValue = (int)multipleValue;
                        //DebugValue(item, multipleValue, multiplier);
                        typeof(ItemObject).GetProperty("Value").SetValue(item, newValue);

                    }
                }
            }
        }

        public static void processTradeGoodsWeight(MBReadOnlyList<ItemObject> ItemsList)
        {
            for (int i = 0; i < ItemsList.Count; i++)
            {
                var item = ItemsList[i];
                if (!item.IsFood && item.ItemType != ItemTypeEnum.Animal && item.IsTradeGood)
                {
                    //float multipleValue = item.Weight * Statics._settings.weightGoodsMultiplier;
                    ////DebugWeight(item, multipleValue, Statics._settings.weightGoodsMultiplier);
                    //typeof(ItemObject).GetProperty("Weight").SetValue(item, multipleValue);
                }
            }
        }

        public static void processTradeGoodsValue(MBReadOnlyList<ItemObject> ItemsList)
        {
            for (int i = 0; i < ItemsList.Count; i++)
            {
                var item = ItemsList[i];
                if (!item.IsFood && item.ItemType != ItemTypeEnum.Animal && item.IsTradeGood)
                {
                    float multipleValue = 0.0f;
                    float multiplier = 1.0f;
                    int newValue = 0;
                    //multiplier = Statics._settings.valueGoodsMultiplier;
                    multipleValue = item.Value * multiplier;
                    newValue = (int)multipleValue;
                    ////DebugValue(item, multipleValue, Statics._settings.valueGoodsMultiplier);

                    typeof(ItemObject).GetProperty("Value").SetValue(item, newValue);


                }
            }
        }


        private static void DebugValue(ItemObject item, float newValue, float multiplier)
        {
            if (Statics._settings.Debug)
            {
                IM.MessageDebug(item.Name.ToString() + " Old Value: " + item.Value.ToString() + "  New Value: " + newValue.ToString() + " using multiplier: " + multiplier);
            }
            else if (Statics._settings.LogToFile)
            {
                //Logging.Lm(item.Name.ToString() + " Old Value: " + item.Value.ToString() + "  New Value: " + newValue.ToString() + " using multiplier: " + multiplier);
            }
        }
        private static void DebugWeight(ItemObject item, float newValue, float multiplier)
        {
            if (Statics._settings.Debug)
            {
                IM.MessageDebug(item.Name.ToString() + " Old Weight: " + item.Weight.ToString() + "  New Weight: " + newValue.ToString() + " using multiplier: " + multiplier);
            }
            else if (Statics._settings.LogToFile)
            {
                //Logging.Lm(item.Name.ToString() + " Old Weight: " + item.Weight.ToString() + "  New Weight: " + newValue.ToString() + " using multiplier: " + multiplier);
            }
        }

    }
}
