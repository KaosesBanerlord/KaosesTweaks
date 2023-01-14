using KaosesCommon.Utils;
using System.Runtime;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects.Items
{

    class Bullets : ItemModifiersBase
    {
        public Bullets(ItemObject itemObject) :
            base(itemObject)
        {
            if (_settings.ItemDebugMode && _settings.BulletsMultiplierEnabled)
            {
                IM.MessageDebug("Bullets : ObjectsBase");
            }
            TweakValues();
        }

        protected void TweakValues()
        {
            if (_settings.ItemDebugMode && _settings.BulletsMultiplierEnabled)
            {
                //IM.MessageDebug("String ID: " + _item.StringId.ToString() + "  Tier: " + _item.Tier.ToString() + "  IsCivilian: " + _item.IsCivilian.ToString() + "  ");
            }
            float multiplerPrice = 1.0f;
            float multiplerWeight = 1.0f;
            float multiplerStack = 1.0f;
            GetMultiplierValues(ref multiplerPrice, ref multiplerWeight, ref multiplerStack);
            if (_settings.BulletsMultiplierEnabled)
            {
                SetItemsValue((int)(_item.Value * multiplerPrice), multiplerPrice, _settings.BulletsMultiplierEnabled);
                //SetItemsWeight((int)(_item.Value * multiplerPrice), multiplerPrice, _settings.BulletsMultiplierEnabled);
                SetItemsStack(multiplerStack);
            }
        }

        protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight, ref float multiplierStack)
        {
            multiplierPrice = _settings.BulletsValueMultiplier;
            //multiplierWeight = Factory.Settings.BoltsWeightMultiplier;
            multiplierStack = _settings.BulletsMultiplier;
        }
    }
}
