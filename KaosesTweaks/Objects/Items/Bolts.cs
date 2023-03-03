using KaosesCommon.Utils;
using System.Runtime;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects.Items
{

    class Bolts : ItemModifiersBase
    {
        public Bolts(ItemObject itemObject) :
            base(itemObject)
        {
            if (_settings.IsItemDebugMode && _settings.BoltsMultipliersEnabled)
            {
                IM.MessageDebug("Bolts : ObjectsBase");
            }
            TweakValues();
        }

        protected void TweakValues()
        {
            if (_settings.IsItemDebugMode && _settings.BoltsMultipliersEnabled)
            {
                //IM.MessageDebug("String ID: " + _item.StringId.ToString() + "  Tier: " + _item.Tier.ToString() + "  IsCivilian: " + _item.IsCivilian.ToString() + "  ");
            }
            float multiplerPrice = 1.0f;
            float multiplerWeight = 1.0f;
            float multiplerStack = 1.0f;
            GetMultiplierValues(ref multiplerPrice, ref multiplerWeight, ref multiplerStack);
            if (_settings.BoltsMultipliersEnabled)
            {
                SetItemsValue((int)(_item.Value * multiplerPrice), multiplerPrice, _settings.BoltsMultipliersEnabled);
                //SetItemsWeight((int)(_item.Value * multiplerPrice), multiplerPrice, _settings.BoltsMultipliersEnabled);
                SetItemsStack(multiplerStack);
            }
        }

        protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight, ref float multiplierStack)
        {
            multiplierPrice = Factory.Settings.BoltsValueMultiplier;
            //multiplierWeight = Factory.Settings.BoltsWeightMultiplier;
            multiplierStack = Factory.Settings.BoltsMultiplier;
        }
    }
}
