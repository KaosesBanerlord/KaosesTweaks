using KaosesCommon.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects.Items
{

    public class Food : ItemModifiersBase
    {

        public Food(ItemObject itemObject) :
            base(itemObject)
        {
            if (_settings.ItemDebugMode && _settings.MCMFoodModifiers)
            {
                IM.MessageDebug("Food : ObjectsBase");
            }
            TweakValues();
        }

        protected void TweakValues()
        {
            if (_settings.ItemDebugMode && _settings.MCMFoodModifiers)
            {
                //IM.MessageDebug("String ID: " + _item.StringId.ToString() + "  Tier: " + _item.Tier.ToString() + "  IsCivilian: " + _item.IsCivilian.ToString() + "  ");
            }
            float multiplerPrice = 1.0f;
            float multiplerWeight = 1.0f;
            GetMultiplierValues(ref multiplerPrice, ref multiplerWeight);
            if (_settings.MCMFoodModifiers)
            {
                SetItemsValue((int)(_item.Value * multiplerPrice), multiplerPrice, _settings.MCMFoodModifiers);
                SetItemsWeight(_item.Weight * multiplerWeight, multiplerWeight, _settings.MCMFoodModifiers);
            }
        }

        protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight)
        {
            if (_item.HasFoodComponent)
            {
                TradeItemComponent tc = _item.FoodComponent;
                if (tc.MoraleBonus == 0)
                {
                    multiplierPrice = Factory.Settings.ItemFoodPriceMorale0Multiplier;
                    multiplierWeight = Factory.Settings.ItemFoodWeightMorale0Multiplier;
                }
                else if (tc.MoraleBonus == 1)
                {
                    multiplierPrice = Factory.Settings.ItemFoodPriceMorale1Multiplier;
                    multiplierWeight = Factory.Settings.ItemFoodWeightMorale1Multiplier;
                }
                else if (tc.MoraleBonus == 2)
                {
                    multiplierPrice = Factory.Settings.ItemFoodPriceMorale2Multiplier;
                    multiplierWeight = Factory.Settings.ItemFoodWeightMorale2Multiplier;
                }
                else if (tc.MoraleBonus == 3)
                {
                    multiplierPrice = Factory.Settings.ItemFoodPriceMorale3Multiplier;
                    multiplierWeight = Factory.Settings.ItemFoodWeightMorale3Multiplier;
                }
            }

        }
    }
}
