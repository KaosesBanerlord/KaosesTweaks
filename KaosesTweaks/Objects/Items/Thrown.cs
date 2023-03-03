using KaosesCommon.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects.Items
{

    class Thrown : ItemModifiersBase
    {
        public Thrown(ItemObject itemObject) :
            base(itemObject)
        {
            if (_settings.IsItemDebugMode && _settings.ThrownMultiplierEnabled)
            {
                IM.MessageDebug("Thrown : ObjectsBase");
            }
            TweakValues();
        }

        protected void TweakValues()
        {
            if (_settings.IsItemDebugMode && _settings.ThrownMultiplierEnabled)
            {
                //IM.MessageDebug("String ID: " + _item.StringId.ToString() + "  Tier: " + _item.Tier.ToString() + "  IsCivilian: " + _item.IsCivilian.ToString() + "  ");
            }
            float multiplerPrice = 1.0f;
            float multiplerWeight = 1.0f;
            float multiplerStack = 1.0f;
            GetMultiplierValues(ref multiplerPrice, ref multiplerWeight, ref multiplerStack);
            if (_settings.ThrownMultiplierEnabled)
            {
                SetItemsValue((int)(_item.Value * multiplerPrice), multiplerPrice, _settings.ThrownMultiplierEnabled);
                //SetItemsWeight((int)(_item.Value * multiplerPrice), multiplerPrice,  _settings.ThrownMultiplierEnabled);
                SetItemsStack(multiplerStack);
            }
        }

        protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight, ref float multiplierStack)
        {
            multiplierPrice = _settings.ThrownValueMultiplier;
            //multiplierWeight = Factory.Settings.ThrownWeightMultiplier;
            multiplierStack = Factory.Settings.ThrownMultiplier;
        }
    }
}
