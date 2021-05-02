using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects
{
    public class TwoHandedWeapon : ObjectsBase
    {

        public TwoHandedWeapon(ItemObject itemObject) :
            base(itemObject)
        {
            //Logging.Lm("TwoHandedWeapon : ObjectsBase");
            TweakValues();
        }

        protected void TweakValues()
        {
            //Logging.Lm("String ID: "+_item.StringId.ToString()+ "  Tier: "+_item.Tier.ToString()+ "  IsCivilian: "+ _item.IsCivilian.ToString()+"  ");
            float multiplePriceValue = 1.0f;
            float multipleWeightValue = 1.0f;

            if (_settings.TwoHandedWeaponValueModifiers)
            {
                if (_item.Tier == ItemObject.ItemTiers.Tier1)
                {
                    multiplePriceValue = _item.Value * _settings.TwoHandedWeaponTier1PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier2)
                {
                    multiplePriceValue = _item.Value * _settings.TwoHandedWeaponTier2PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier3)
                {
                    multiplePriceValue = _item.Value * _settings.TwoHandedWeaponTier3PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier4)
                {
                    multiplePriceValue = _item.Value * _settings.TwoHandedWeaponTier4PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier5)
                {
                    multiplePriceValue = _item.Value * _settings.TwoHandedWeaponTier5PriceMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier6)
                {
                    multiplePriceValue = _item.Value * _settings.TwoHandedWeaponTier6PriceMultiplier;
                }
                //DebugValue(_item, multipleValue, _settings.TwoHandedWeaponValueMultiplier);
                typeof(ItemObject).GetProperty("Value").SetValue(_item, (int)multiplePriceValue);
            }
            if (_settings.TwoHandedWeaponWeightModifiers)
            {
                if (_item.Tier == ItemObject.ItemTiers.Tier1)
                {
                    multipleWeightValue = _item.Weight * _settings.TwoHandedWeaponTier1WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier2)
                {
                    multipleWeightValue = _item.Weight * _settings.TwoHandedWeaponTier2WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier3)
                {
                    multipleWeightValue = _item.Weight * _settings.TwoHandedWeaponTier3WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier4)
                {
                    multipleWeightValue = _item.Weight * _settings.TwoHandedWeaponTier4WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier5)
                {
                    multipleWeightValue = _item.Weight * _settings.TwoHandedWeaponTier5WeightMultiplier;
                }
                else if (_item.Tier == ItemObject.ItemTiers.Tier6)
                {
                    multipleWeightValue = _item.Weight * _settings.TwoHandedWeaponTier6WeightMultiplier;
                }
                //DebugWeight(_item, multipleValue, _settings.TwoHandedWeaponWeightMultiplier);
                typeof(ItemObject).GetProperty("Weight").SetValue(_item, (float)multipleWeightValue);
            }

            /*
                        if (_settings.TwoHandedWeaponValueModifiers)
                        {
                            float multipleValue = _item.Value * _settings.TwoHandedWeaponValueMultiplier;
                            //DebugValue(_item, multipleValue, _settings.TwoHandedWeaponValueMultiplier);
                            typeof(ItemObject).GetProperty("Value").SetValue(_item, (int)multipleValue);
                        }
                        if (_settings.TwoHandedWeaponWeightModifiers)
                        {
                            float multipleValue = _item.Weight * _settings.TwoHandedWeaponWeightMultiplier;
                            //DebugWeight(_item, multipleValue, _settings.TwoHandedWeaponWeightMultiplier);
                            typeof(ItemObject).GetProperty("Weight").SetValue(_item, (float)multipleValue);
                        }*/
        }


    }
}
