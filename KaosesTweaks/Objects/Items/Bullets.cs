using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects.Items
{
    class Bullets : ItemModifiersBase
    {
        public Bullets(ItemObject itemObject) :
            base(itemObject)
        {
            if (_settings.ItemDebugMode)
            {
                IM.MessageDebug("Bullets : ObjectsBase");
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
            if (MCMSettings.Instance is { } settings && settings.BulletsMultiplierEnabled)
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
                multiplierPrice = settings.BulletsValueMultiplier;
                //multiplierWeight = settings.BoltsWeightMultiplier;
                multiplierStack = settings.BulletsMultiplier;
            }
        }
    }
}
