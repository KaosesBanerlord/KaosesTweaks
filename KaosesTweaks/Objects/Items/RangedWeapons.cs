using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects
{
    public class RangedWeapons : ItemModifiersBase
    {

        public RangedWeapons(ItemObject itemObject) :
            base(itemObject)
        {
            if (_settings.ItemDebugMode)
            {
                //IM.MessageDebug("RangedWeapons : ObjectsBase");
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
            if (_settings.ItemRangedWeaponsValueModifiers && _settings.MCMRagedWeaponsModifiers)
            {
                SetItemsValue((int)(_item.Value * multiplerPrice), multiplerPrice);
            }
            if (_settings.ItemRangedWeaponsWeightModifiers && _settings.MCMRagedWeaponsModifiers)
            {
                if (_item.Type != ItemObject.ItemTypeEnum.Thrown)
                {
                    //SetItemsWeight(_item.Weight * multiplerWeight, multiplerWeight);
                }

            }
        }

        protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight)
        {

            if (_item.Tier == ItemObject.ItemTiers.Tier1)
            {
                multiplierPrice = _settings.ItemRangedWeaponsTier1PriceMultiplier;
                multiplierWeight = _settings.ItemRangedWeaponsTier1WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier2)
            {
                multiplierPrice = _settings.ItemRangedWeaponsTier2PriceMultiplier;
                multiplierWeight = _settings.ItemRangedWeaponsTier2WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier3)
            {
                multiplierPrice = _settings.ItemRangedWeaponsTier3PriceMultiplier;
                multiplierWeight = _settings.ItemRangedWeaponsTier3WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier4)
            {
                multiplierPrice = _settings.ItemRangedWeaponsTier4PriceMultiplier;
                multiplierWeight = _settings.ItemRangedWeaponsTier4WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier5)
            {
                multiplierPrice = _settings.ItemRangedWeaponsTier5PriceMultiplier;
                multiplierWeight = _settings.ItemRangedWeaponsTier5WeightMultiplier;
            }
            else if (_item.Tier == ItemObject.ItemTiers.Tier6)
            {
                multiplierPrice = _settings.ItemRangedWeaponsTier6PriceMultiplier;
                multiplierWeight = _settings.ItemRangedWeaponsTier6WeightMultiplier;
            }
        }
    }
}
