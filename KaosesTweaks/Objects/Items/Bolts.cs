using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects.Items
{
    class Bolts : ItemModifiersBase
    {
        public Bolts(ItemObject itemObject) :
            base(itemObject)
        {
            if (_settings.ItemDebugMode)
            {
                IM.MessageDebug("Bolts : ObjectsBase");
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
            float multiplerStack = 1.0f;
            GetMultiplierValues(ref multiplerPrice, ref multiplerWeight, ref multiplerStack);
            if (MCMSettings.Instance is { } settings && settings.BoltsMultipliersEnabled)
            {
                SetItemsValue((int)(_item.Value * multiplerPrice), multiplerPrice);
                //SetItemsWeight((int)(_item.Value * multiplerPrice), multiplerPrice);
                SetItemsStack(multiplerStack);
            }
        }

        protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight, ref float multiplierStack)
        {
            if (MCMSettings.Instance is { } settings)
            {
                multiplierPrice = settings.BoltsValueMultiplier;
                //multiplierWeight = settings.BoltsWeightMultiplier;
                multiplierStack = settings.BoltsMultiplier;
            }
        }
    }
}
