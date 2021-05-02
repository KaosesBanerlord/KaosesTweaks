using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects
{
    public class Horse : ObjectsBase
    {

        public Horse(ItemObject itemObject) :
            base(itemObject)
        {
            //Logging.Lm("Horse : ObjectsBase");
            TweakValues();
        }

        protected void TweakValues()
        {
            //Logging.Lm("KaosesTweaks.Objects.horse String ID: " + _item.StringId.ToString()+ "  Tier: "+_item.Tier.ToString()+ "  IsCivilian: "+ _item.IsCivilian.ToString()+"  ");
            float multiplePriceValue = 1.0f;
            float priceMultiplier = 1.0f;
            float multipleWeightValue = 1.0f;
            float weightMultiplier = 1.0f;

            /*
                Pureblood Old Price: 41220  New Price: 320 using multiplier: 0.8
                Pureblood  tier: Tier6 Old Price: 41220  New Price: 320 using multiplier: 0.8
                Pureblood Old Weight: 400  New Weight: 815.556 using multiplier: 2.03889
                Pureblood  tier: Tier6 Old Weight: 400  New Weight: 815.556 using multiplier: 2.03889
                KaosesTweaks.Objects.horse String ID: mule_unmountable  Tier: Tier1  IsCivilian: True  
             */

            if (_settings.HorseValueModifiers)
            {
                if (_item.Tier == ItemObject.ItemTiers.Tier1)
                {
                    priceMultiplier = _settings.HorseTier1PriceMultiplier;
                    multiplePriceValue = _item.Value * _settings.HorseTier1PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier2)
                {
                    priceMultiplier = _settings.HorseTier2PriceMultiplier;
                    multiplePriceValue = _item.Value * _settings.HorseTier2PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier3)
                {
                    priceMultiplier = _settings.HorseTier3PriceMultiplier;
                    multiplePriceValue = _item.Value * _settings.HorseTier3PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier4)
                {
                    priceMultiplier = _settings.HorseTier4PriceMultiplier;
                    multiplePriceValue = _item.Value * _settings.HorseTier4PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier5)
                {
                    priceMultiplier = _settings.HorseTier5PriceMultiplier;
                    multiplePriceValue = _item.Value * _settings.HorseTier5PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier6)
                {
                    priceMultiplier = _settings.HorseTier6PriceMultiplier;
                    multiplePriceValue = _item.Value * _settings.HorseTier6PriceMultiplier;
                }
                //DebugValue(_item, multiplePriceValue, priceMultiplier);
                typeof(ItemObject).GetProperty("Value").SetValue(_item, (int)multiplePriceValue);
            }
            if (_settings.HorseWeightModifiers)
            {
                if (_item.Tier == ItemObject.ItemTiers.Tier1)
                {
                    weightMultiplier = _settings.HorseTier1WeightMultiplier;
                    multipleWeightValue = _item.Weight * _settings.HorseTier1WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier2)
                {
                    weightMultiplier = _settings.HorseTier2WeightMultiplier;
                    multipleWeightValue = _item.Weight * _settings.HorseTier2WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier3)
                {
                    weightMultiplier = _settings.HorseTier3WeightMultiplier;
                    multipleWeightValue = _item.Weight * _settings.HorseTier3WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier4)
                {
                    weightMultiplier = _settings.HorseTier4WeightMultiplier;
                    multipleWeightValue = _item.Weight * _settings.HorseTier4WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier5)
                {
                    weightMultiplier = _settings.HorseTier5WeightMultiplier;
                    multipleWeightValue = _item.Weight * _settings.HorseTier5WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier6)
                {
                    weightMultiplier = _settings.HorseTier6WeightMultiplier;
                    multipleWeightValue = _item.Weight * _settings.HorseTier6WeightMultiplier;
                }
                //DebugWeight(_item, multipleWeightValue, weightMultiplier);
                typeof(ItemObject).GetProperty("Weight").SetValue(_item, (float)multipleWeightValue);
            }

        }


    }
}
