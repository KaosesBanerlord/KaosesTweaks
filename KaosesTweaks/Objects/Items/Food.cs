using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects
{
    public class Food : ItemModifiersBase
    {

        public Food(ItemObject itemObject) :
            base(itemObject)
        {
            if (_settings.ItemDebugMode)
            {
                //IM.MessageDebug("Food : ObjectsBase");
            }
            TweakValues();
        }

        protected void TweakValues()
        {
            if (_settings.ItemDebugMode)
            {
                IM.MessageDebug("String ID: " + _item.StringId.ToString() + "  Tier: " + _item.Tier.ToString() + "  IsCivilian: " + _item.IsCivilian.ToString() + "  ");
            }
            float multiplerPrice = 1.0f;
            float multiplerWeight = 1.0f;
            GetMultiplierValues(ref multiplerPrice, ref multiplerWeight);
            if (_settings.MCMFoodModifiers)
            {
                SetItemsValue((int)(_item.Value * multiplerPrice), multiplerPrice);
                SetItemsWeight(_item.Weight * multiplerWeight, multiplerWeight);
            }
        }

        protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight)
        {
            if (_item.HasFoodComponent)
            {
                TradeItemComponent tc = _item.FoodComponent;
                if (tc.MoraleBonus == 0)
                {
                    multiplierPrice = Statics._settings.ItemFoodPriceMorale0Multiplier;
                    multiplierWeight = Statics._settings.ItemFoodWeightMorale0Multiplier;
                }
                else if (tc.MoraleBonus == 1)
                {
                    multiplierPrice = Statics._settings.ItemFoodPriceMorale1Multiplier;
                    multiplierWeight = Statics._settings.ItemFoodWeightMorale1Multiplier;
                }
                else if (tc.MoraleBonus == 2)
                {
                    multiplierPrice = Statics._settings.ItemFoodPriceMorale2Multiplier;
                    multiplierWeight = Statics._settings.ItemFoodWeightMorale2Multiplier;
                }
                else if (tc.MoraleBonus == 3)
                {
                    multiplierPrice = Statics._settings.ItemFoodPriceMorale3Multiplier;
                    multiplierWeight = Statics._settings.ItemFoodWeightMorale3Multiplier;
                }
            }

        }
    }
}
