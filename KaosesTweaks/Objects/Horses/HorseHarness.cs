using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects
{
    public class HorseHarness : ObjectsBase
    {

        public HorseHarness(ItemObject itemObject) :
            base(itemObject)
        {
            //Logging.Lm("HorseHarness : ObjectsBase");
            TweakValues();
        }

        protected void TweakValues()
        {
            //Logging.Lm("String ID: "+_item.StringId.ToString()+ "  Tier: "+_item.Tier.ToString()+ "  IsCivilian: "+ _item.IsCivilian.ToString()+"  ");
            float multiplePriceValue = 1.0f;
            float multipleWeightValue = 1.0f;

            if (_settings.HorseHarnessValueModifiers)
            {
                if (_item.Tier == ItemObject.ItemTiers.Tier1)
                {
                    multiplePriceValue = _item.Value * _settings.HorseHarnessTier1PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier2)
                {
                    multiplePriceValue = _item.Value * _settings.HorseHarnessTier2PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier3)
                {
                    multiplePriceValue = _item.Value * _settings.HorseHarnessTier3PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier4)
                {
                    multiplePriceValue = _item.Value * _settings.HorseHarnessTier4PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier5)
                {
                    multiplePriceValue = _item.Value * _settings.HorseHarnessTier5PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier6)
                {
                    multiplePriceValue = _item.Value * _settings.HorseHarnessTier6PriceMultiplier;
                }
                //DebugValue(_item, multipleValue, _settings.HorseHarnessValueMultiplier);
                typeof(ItemObject).GetProperty("Value").SetValue(_item, (int)multiplePriceValue);
            }
            if (_settings.HorseHarnessWeightModifiers)
            {
                if (_item.Tier == ItemObject.ItemTiers.Tier1)
                {
                    multipleWeightValue = _item.Weight * _settings.HorseHarnessTier1WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier2)
                {
                    multipleWeightValue = _item.Weight * _settings.HorseHarnessTier2WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier3)
                {
                    multipleWeightValue = _item.Weight * _settings.HorseHarnessTier3WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier4)
                {
                    multipleWeightValue = _item.Weight * _settings.HorseHarnessTier4WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier5)
                {
                    multipleWeightValue = _item.Weight * _settings.HorseHarnessTier5WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier6)
                {
                    multipleWeightValue = _item.Weight * _settings.HorseHarnessTier6WeightMultiplier;
                }
                //DebugWeight(_item, multipleValue, _settings.HorseHarnessWeightMultiplier);
                typeof(ItemObject).GetProperty("Weight").SetValue(_item, (float)multipleWeightValue);
            }

        }


    }
}
