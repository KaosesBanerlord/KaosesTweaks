using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects
{
    public class TradeGoods : ItemModifiersBase
    {

        public TradeGoods(ItemObject itemObject) :
            base(itemObject)
        {
            if (_settings.ItemDebugMode)
            {
                //IM.MessageDebug("TradeGoods : ObjectsBase");
            }
            TweakValues();
        }

        protected void TweakValues()
        {
            if (_settings.ItemDebugMode)
            {
                IM.MessageDebug("String ID: " + _item.StringId.ToString() + "  Tier: " + _item.Tier.ToString() + "  IsCivilian: " + _item.IsCivilian.ToString() + "  ");
            }
            if (_settings.MCMTradeGoodsModifiers)
            {
                SetItemsValue((int)(_item.Value * _settings.ItemTradeGoodsPriceMultiplier), _settings.ItemTradeGoodsPriceMultiplier);
                SetItemsWeight(_item.Weight * _settings.ItemTradeGoodsWeightMultiplier, _settings.ItemTradeGoodsWeightMultiplier);
            }
        }
    }
}
