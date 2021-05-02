using KaosesTweaks.Objects;
using KaosesTweaks.Utils;
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

        protected void TweakItemValues()
        {
            for (int i = 0; i < _ItemsList.Count; i++)
            {
                var item = _ItemsList[i];
                //Logging.Lm("StringId: " + item.StringId.ToString() + "  Type:" + item.ItemType.ToString());
                if (item.ItemType == ItemTypeEnum.Banner)
                {

                }
                else if (item.ItemType == ItemTypeEnum.BodyArmor)
                {
                    new BodyArmor(item);
                }
                else if (item.ItemType == ItemTypeEnum.Book)
                {
                    //new Book(item);
                }
                else if (item.ItemType == ItemTypeEnum.Bow)
                {
                    new Bow(item);
                }
                else if (item.ItemType == ItemTypeEnum.Cape)
                {
                    new Cape(item);
                }
                else if (item.ItemType == ItemTypeEnum.ChestArmor)
                {
                    new ChestArmor(item);
                }
                else if (item.ItemType == ItemTypeEnum.Crossbow)
                {
                    new Crossbow(item);
                }
                else if (item.ItemType == ItemTypeEnum.HandArmor)
                {
                    new HandArmor(item);
                }
                else if (item.ItemType == ItemTypeEnum.HeadArmor)
                {
                    new HeadArmor(item);
                }
                else if (item.ItemType == ItemTypeEnum.Horse)
                {
                    new Horse(item);
                }
                else if (item.ItemType == ItemTypeEnum.HorseHarness)
                {
                    new HorseHarness(item);
                }
                else if (item.ItemType == ItemTypeEnum.LegArmor)
                {
                    new LegArmor(item);
                }
                else if (item.ItemType == ItemTypeEnum.Musket)
                {
                    new Musket(item);
                }
                else if (item.ItemType == ItemTypeEnum.OneHandedWeapon)
                {
                    new OneHandedWeapon(item);
                }
                else if (item.ItemType == ItemTypeEnum.Pistol)
                {
                    new Pistol(item);
                }
                else if (item.ItemType == ItemTypeEnum.Polearm)
                {
                    new Polearm(item);
                }
                else if (item.ItemType == ItemTypeEnum.Shield)
                {
                    new Shield(item);
                }
                else if (item.ItemType == ItemTypeEnum.TwoHandedWeapon)
                {
                    new TwoHandedWeapon(item);
                }

            }
        }

    }
}
