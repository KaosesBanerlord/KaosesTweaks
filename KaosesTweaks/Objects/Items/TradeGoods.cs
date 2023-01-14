using KaosesCommon.Utils;
using TaleWorlds.Core;


namespace KaosesTweaks.Objects.Items
{

    public class TradeGoods : ItemModifiersBase
    {

        public TradeGoods(ItemObject itemObject) :
            base(itemObject)
        {
            if (_settings.ItemDebugMode && _settings.MCMTradeGoodsModifiers)
            {
                IM.MessageDebug("TradeGoods : ObjectsBase");
            }
            TweakValues();
        }

        protected void TweakValues()
        {
            if (_settings.ItemDebugMode && _settings.MCMTradeGoodsModifiers)
            {
                //IM.MessageDebug("String ID: " + _item.StringId.ToString() + "  Tier: " + _item.Tier.ToString() + "  IsCivilian: " + _item.IsCivilian.ToString() + "  ");
            }
            if (_settings.MCMTradeGoodsModifiers)
            {
                SetItemsValue((int)(_item.Value * _settings.ItemTradeGoodsPriceMultiplier), _settings.ItemTradeGoodsPriceMultiplier, _settings.MCMTradeGoodsModifiers);
                SetItemsWeight(_item.Weight * _settings.ItemTradeGoodsWeightMultiplier, _settings.ItemTradeGoodsWeightMultiplier, _settings.MCMTradeGoodsModifiers);
            }


            //Debug Calls
            //if (_settings.ItemDebugMode && _settings.MCMTradeGoodsModifiers)
            //{
            //    IM.MessageDebug("String ID: " + _item.StringId.ToString() + "  Value: " + _item.Value + "  New Value: " + _item.Value * _settings.ItemTradeGoodsPriceMultiplier);
            //}
            //if (_settings.ItemDebugMode && _settings.ItemArmorWeightModifiers)
            //{
            //    IM.MessageDebug("String ID: " + _item.StringId.ToString() + "  Weight: " + _item.Weight + "  New Weight: " + _item.Weight * _settings.ItemTradeGoodsWeightMultiplier);
            //}
        }
    }
}
