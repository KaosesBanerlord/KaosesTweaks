using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects
{
    public class MeleeWeapons : ItemModifiersBase
    {
        public MeleeWeapons(ItemObject itemObject) :
            base(itemObject)
        {
            if (_settings.ItemDebugMode)
            {
                //IM.MessageDebug("MeleeWeapons : ObjectsBase");
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
            if (_settings.ItemMeleeWeaponValueModifiers && _settings.MCMMeleeWeaponModifiers)
            {
                SetItemsValue((int)(_item.Value * multiplerPrice), multiplerPrice);
            }
            if (_settings.ItemMeleeWeaponWeightModifiers && _settings.MCMMeleeWeaponModifiers)
            {
                //SetItemsWeight(_item.Weight * multiplerWeight, multiplerWeight);
            }
        }

        protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight)
        {

            if (_item.Tier == ItemObject.ItemTiers.Tier1)
            {
                multiplierPrice = _settings.ItemMeleeWeaponTier1PriceMultiplier;
                multiplierWeight = _settings.ItemMeleeWeaponTier1WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier2)
            {
                multiplierPrice = _settings.ItemMeleeWeaponTier2PriceMultiplier;
                multiplierWeight = _settings.ItemMeleeWeaponTier2WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier3)
            {
                multiplierPrice = _settings.ItemMeleeWeaponTier3PriceMultiplier;
                multiplierWeight = _settings.ItemMeleeWeaponTier3WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier4)
            {
                multiplierPrice = _settings.ItemMeleeWeaponTier4PriceMultiplier;
                multiplierWeight = _settings.ItemMeleeWeaponTier4WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier5)
            {
                multiplierPrice = _settings.ItemMeleeWeaponTier5PriceMultiplier;
                multiplierWeight = _settings.ItemMeleeWeaponTier5WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier6)
            {
                multiplierPrice = _settings.ItemMeleeWeaponTier6PriceMultiplier;
                multiplierWeight = _settings.ItemMeleeWeaponTier6WeightMultiplier;
            }
        }
    }
}
