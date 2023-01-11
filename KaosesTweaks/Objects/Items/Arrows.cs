using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects.Items
{
    class Arrows : ItemModifiersBase
    {
        public Arrows(ItemObject itemObject) :
            base(itemObject)
        {
            if (_settings.ItemDebugMode && _settings.ArrowMultipliersEnabled)
            {
                IM.MessageDebug("Arrows : ObjectsBase");
            }
            TweakValues();
        }

        protected void TweakValues()
        {
            if (_settings.ItemDebugMode && _settings.ArrowMultipliersEnabled)
            {
                //IM.MessageDebug("String ID: " + _item.StringId.ToString() + "  Tier: " + _item.Tier.ToString() + "  IsCivilian: " + _item.IsCivilian.ToString() + "  ");
            }
            float multiplerPrice = 1.0f;
            float multiplerWeight = 1.0f;
            float multiplerStack = 1.0f;
            GetMultiplierValues(ref multiplerPrice, ref multiplerWeight, ref multiplerStack);
            if (_settings.ArrowMultipliersEnabled)
            {
                SetItemsValue((int)(_item.Value * multiplerPrice), multiplerPrice, _settings.ArrowMultipliersEnabled);
                //SetItemsWeight((int)(_item.Value * multiplerPrice), multiplerPrice, _settings.ArrowMultipliersEnabled);
                SetItemsStack(multiplerStack);
            }
        }

        protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight, ref float multiplierStack)
        {
            multiplierPrice = Statics._settings.ArrowValueMultiplier;
            //multiplierWeight = Statics._settings.ArrowWeightMultiplier;
            multiplierStack = Statics._settings.ArrowMultiplier;
        }
    }
}
