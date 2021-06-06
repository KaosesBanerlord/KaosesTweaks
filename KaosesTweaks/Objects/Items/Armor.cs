using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects
{
    public class Armor : ItemModifiersBase
    {

        public Armor(ItemObject itemObject) :
            base(itemObject)
        {
            if (_settings.ItemDebugMode)
            {
                //IM.MessageDebug("Armor : ObjectsBase");
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
            if (_settings.ItemArmorValueModifiers && _settings.MCMArmorModifiers)
            {
                SetItemsValue((int)(_item.Value * multiplerPrice), multiplerPrice);
            }
            if (_settings.ItemArmorWeightModifiers && _settings.MCMArmorModifiers)
            {
                SetItemsWeight(_item.Weight * multiplerWeight, multiplerWeight);
            }
        }

        protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight)
        {

            if (_item.Tier == ItemObject.ItemTiers.Tier1)
            {
                multiplierPrice = _settings.ItemArmorTier1PriceMultiplier;
                multiplierWeight = _settings.ItemArmorTier1WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier2)
            {
                multiplierPrice = _settings.ItemArmorTier2PriceMultiplier;
                multiplierWeight = _settings.ItemArmorTier2WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier3)
            {
                multiplierPrice = _settings.ItemArmorTier3PriceMultiplier;
                multiplierWeight = _settings.ItemArmorTier3WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier4)
            {
                multiplierPrice = _settings.ItemArmorTier4PriceMultiplier;
                multiplierWeight = _settings.ItemArmorTier4WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier5)
            {
                multiplierPrice = _settings.ItemArmorTier5PriceMultiplier;
                multiplierWeight = _settings.ItemArmorTier5WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier6)
            {
                multiplierPrice = _settings.ItemArmorTier6PriceMultiplier;
                multiplierWeight = _settings.ItemArmorTier6WeightMultiplier;
            }
        }
    }
}
