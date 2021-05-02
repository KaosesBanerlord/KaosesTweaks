using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects
{
    public class HeadArmor : ObjectsBase
    {

        public HeadArmor(ItemObject itemObject) :
            base(itemObject)
        {
            TweakValues();
        }

        protected void TweakValues()
        {
            //Logging.Lm("String ID: "+_item.StringId.ToString()+ "  Tier: "+_item.Tier.ToString()+ "  IsCivilian: "+ _item.IsCivilian.ToString()+"  ");
            float multiplePriceValue = 1.0f;
            float multipleWeightValue = 1.0f;

            if (_settings.HeadArmorValueModifiers)
            {
                if (_item.Tier == ItemObject.ItemTiers.Tier1)
                {
                    multiplePriceValue = _item.Value * _settings.HeadArmorTier1PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier2)
                {
                    multiplePriceValue = _item.Value * _settings.HeadArmorTier2PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier3)
                {
                    multiplePriceValue = _item.Value * _settings.HeadArmorTier3PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier4)
                {
                    multiplePriceValue = _item.Value * _settings.HeadArmorTier4PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier5)
                {
                    multiplePriceValue = _item.Value * _settings.HeadArmorTier5PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier6)
                {
                    multiplePriceValue = _item.Value * _settings.HeadArmorTier6PriceMultiplier;
                }
                //DebugValue(_item, multipleValue, _settings.HeadArmorValueMultiplier);
                typeof(ItemObject).GetProperty("Value").SetValue(_item, (int)multiplePriceValue);
            }
            if (_settings.HeadArmorWeightModifiers)
            {
                if (_item.Tier == ItemObject.ItemTiers.Tier1)
                {
                    multipleWeightValue = _item.Weight * _settings.HeadArmorTier1WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier2)
                {
                    multipleWeightValue = _item.Weight * _settings.HeadArmorTier2WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier3)
                {
                    multipleWeightValue = _item.Weight * _settings.HeadArmorTier3WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier4)
                {
                    multipleWeightValue = _item.Weight * _settings.HeadArmorTier4WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier5)
                {
                    multipleWeightValue = _item.Weight * _settings.HeadArmorTier5WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier6)
                {
                    multipleWeightValue = _item.Weight * _settings.HeadArmorTier6WeightMultiplier;
                }
                //DebugWeight(_item, multipleValue, _settings.HeadArmorWeightMultiplier);
                typeof(ItemObject).GetProperty("Weight").SetValue(_item, (float)multipleWeightValue);
            }

        }


    }
}
