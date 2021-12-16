using KaosesTweaks.Settings;
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
            if (MCMSettings.Instance is { } settings && _item.HasFoodComponent)
            {
                TradeItemComponent tc = _item.FoodComponent;
                if (tc.MoraleBonus == 0)
                {
                    multiplierPrice = settings.ItemFoodPriceMorale0Multiplier;
                    multiplierWeight = settings.ItemFoodWeightMorale0Multiplier;
                }
                else if (tc.MoraleBonus == 1)
                {
                    multiplierPrice = settings.ItemFoodPriceMorale1Multiplier;
                    multiplierWeight = settings.ItemFoodWeightMorale1Multiplier;
                }
                else if (tc.MoraleBonus == 2)
                {
                    multiplierPrice = settings.ItemFoodPriceMorale2Multiplier;
                    multiplierWeight = settings.ItemFoodWeightMorale2Multiplier;
                }
                else if (tc.MoraleBonus == 3)
                {
                    multiplierPrice = settings.ItemFoodPriceMorale3Multiplier;
                    multiplierWeight = settings.ItemFoodWeightMorale3Multiplier;
                }
            }

        }
    }
}
