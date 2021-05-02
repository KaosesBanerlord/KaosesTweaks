using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects
{
    public class Crossbow : ObjectsBase
    {

        public Crossbow(ItemObject itemObject) :
            base(itemObject)
        {
            //Logging.Lm("Crossbow : ObjectsBase");
            TweakValues();
        }

        protected void TweakValues()
        {
            //Logging.Lm("String ID: "+_item.StringId.ToString()+ "  Tier: "+_item.Tier.ToString()+ "  IsCivilian: "+ _item.IsCivilian.ToString()+"  ");
            float multiplePriceValue = 1.0f;
            float multipleWeightValue = 1.0f;

            if (_settings.CrossbowValueModifiers)
            {
                if (_item.Tier == ItemObject.ItemTiers.Tier1)
                {
                    multiplePriceValue = _item.Value * _settings.CrossbowTier1PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier2)
                {
                    multiplePriceValue = _item.Value * _settings.CrossbowTier2PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier3)
                {
                    multiplePriceValue = _item.Value * _settings.CrossbowTier3PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier4)
                {
                    multiplePriceValue = _item.Value * _settings.CrossbowTier4PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier5)
                {
                    multiplePriceValue = _item.Value * _settings.CrossbowTier5PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier6)
                {
                    multiplePriceValue = _item.Value * _settings.CrossbowTier6PriceMultiplier;
                }
                //DebugValue(_item, multipleValue, _settings.CrossbowValueMultiplier);
                typeof(ItemObject).GetProperty("Value").SetValue(_item, (int)multiplePriceValue);
            }
            if (_settings.CrossbowWeightModifiers)
            {
                if (_item.Tier == ItemObject.ItemTiers.Tier1)
                {
                    multipleWeightValue = _item.Weight * _settings.CrossbowTier1WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier2)
                {
                    multipleWeightValue = _item.Weight * _settings.CrossbowTier2WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier3)
                {
                    multipleWeightValue = _item.Weight * _settings.CrossbowTier3WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier4)
                {
                    multipleWeightValue = _item.Weight * _settings.CrossbowTier4WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier5)
                {
                    multipleWeightValue = _item.Weight * _settings.CrossbowTier5WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier6)
                {
                    multipleWeightValue = _item.Weight * _settings.CrossbowTier6WeightMultiplier;
                }
                //DebugWeight(_item, multipleValue, _settings.CrossbowWeightMultiplier);
                typeof(ItemObject).GetProperty("Weight").SetValue(_item, (float)multipleWeightValue);
            }

        }


    }
}
