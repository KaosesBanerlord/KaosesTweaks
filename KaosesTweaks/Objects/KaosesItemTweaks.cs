using KaosesTweaks.Objects;
using KaosesTweaks.Objects.Items;
using TaleWorlds.Core;
using TaleWorlds.Library;
using static TaleWorlds.Core.ItemObject;

namespace KaosesTweaks.Models
{
    public class KaosesItemTweaks
    {
        MBReadOnlyList<ItemObject> _ItemsList;

        public KaosesItemTweaks(MBReadOnlyList<ItemObject> ItemsList)
        {
            _ItemsList = ItemsList;
            TweakItemValues();
        }

        /*
            @TODO need to update the sub classed objects to be more like two handed weapons 
        also need to make code us the WeaponComponentData WeaponClass for better detection of weapon type
                 
        WeaponComponentData weaponComponent = item.PrimaryWeapon;
        //weaponComponent.WeaponClass == WeaponClass.
        //IM.MessageDebug("StringId: " + item.StringId.ToString() + "  Type:" + item.ItemType.ToString());
        if (weaponComponent != null)
        {
            //IM.MessageDebug("weaponComponent.WeaponClass:" + weaponComponent.WeaponClass.ToString());
        }                
            */
        protected void TweakItemValues()
        {
            for (int i = 0; i < _ItemsList.Count; i++)
            {

                ItemObject? item = _ItemsList[i];
                if (item.IsTradeGood && !item.IsFood)
                {
                    new TradeGoods(item);
                }
                else if (item.IsFood)
                {
                    new Food(item);
                }

                if (item.ItemType == ItemTypeEnum.BodyArmor || item.ItemType == ItemTypeEnum.Cape ||
                   item.ItemType == ItemTypeEnum.ChestArmor || item.ItemType == ItemTypeEnum.HandArmor
                   || item.ItemType == ItemTypeEnum.HeadArmor || item.ItemType == ItemTypeEnum.LegArmor
                   || item.ItemType == ItemTypeEnum.Shield || item.ItemType == ItemTypeEnum.HorseHarness
                   )
                {
                    new Armor(item);
                }
                else if (item.ItemType == ItemTypeEnum.Bow || item.ItemType == ItemTypeEnum.Crossbow || item.ItemType == ItemTypeEnum.Musket
                    || item.ItemType == ItemTypeEnum.Pistol)//|| item.ItemType == ItemTypeEnum.Thrown
                {
                    new RangedWeapons(item);
                }
                else if (item.ItemType == ItemTypeEnum.OneHandedWeapon || item.ItemType == ItemTypeEnum.Polearm
                    || item.ItemType == ItemTypeEnum.TwoHandedWeapon)
                {
                    new MeleeWeapons(item);
                }
                else if (item.ItemType == ItemTypeEnum.Arrows)
                {
                    new Arrows(item);
                }
                else if (item.ItemType == ItemTypeEnum.Bolts)
                {
                    new Bolts(item);
                }
                else if (item.ItemType == ItemTypeEnum.Thrown)
                {
                    new Thrown(item);
                }
                else if (item.ItemType == ItemTypeEnum.Bullets)
                {
                    new Bullets(item);
                }

            }
        }

    }
}
