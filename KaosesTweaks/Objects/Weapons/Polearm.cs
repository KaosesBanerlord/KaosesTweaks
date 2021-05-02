using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects
{
    public class Polearm : ObjectsBase
    {

        public Polearm(ItemObject itemObject) :
            base(itemObject)
        {
            //Logging.Lm("Polearm : ObjectsBase");
            TweakValues();
        }

        protected void TweakValues()
        {
            //Logging.Lm("String ID: "+_item.StringId.ToString()+ "  Tier: "+_item.Tier.ToString()+ "  IsCivilian: "+ _item.IsCivilian.ToString()+"  ");
            float multiplePriceValue = 1.0f;
            float multipleWeightValue = 1.0f;

            if (_settings.PolearmValueModifiers)
            {
                if (_item.Tier == ItemObject.ItemTiers.Tier1)
                {
                    multiplePriceValue = _item.Value * _settings.PolearmTier1PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier2)
                {
                    multiplePriceValue = _item.Value * _settings.PolearmTier2PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier3)
                {
                    multiplePriceValue = _item.Value * _settings.PolearmTier3PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier4)
                {
                    multiplePriceValue = _item.Value * _settings.PolearmTier4PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier5)
                {
                    multiplePriceValue = _item.Value * _settings.PolearmTier5PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier6)
                {
                    multiplePriceValue = _item.Value * _settings.PolearmTier6PriceMultiplier;
                }
                //DebugValue(_item, multipleValue, _settings.PolearmValueMultiplier);
                typeof(ItemObject).GetProperty("Value").SetValue(_item, (int)multiplePriceValue);
            }
            if (_settings.PolearmWeightModifiers)
            {
                if (_item.Tier == ItemObject.ItemTiers.Tier1)
                {
                    multipleWeightValue = _item.Weight * _settings.PolearmTier1WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier2)
                {
                    multipleWeightValue = _item.Weight * _settings.PolearmTier2WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier3)
                {
                    multipleWeightValue = _item.Weight * _settings.PolearmTier3WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier4)
                {
                    multipleWeightValue = _item.Weight * _settings.PolearmTier4WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier5)
                {
                    multipleWeightValue = _item.Weight * _settings.PolearmTier5WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier6)
                {
                    multipleWeightValue = _item.Weight * _settings.PolearmTier6WeightMultiplier;
                }
                //DebugWeight(_item, multipleValue, _settings.PolearmWeightMultiplier);
                typeof(ItemObject).GetProperty("Weight").SetValue(_item, (float)multipleWeightValue);
            }

        }


    }
}
