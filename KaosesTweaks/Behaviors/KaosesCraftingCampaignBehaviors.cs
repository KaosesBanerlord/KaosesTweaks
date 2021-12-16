using KaosesTweaks.Objects;
using KaosesTweaks.Objects.Items;
using KaosesTweaks.Utils;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using static TaleWorlds.Core.ItemObject;


namespace KaosesTweaks.Behaviors
{
    class KaosesCraftingCampaignBehaviors : CampaignBehaviorBase
    {
        //private readonly MbEvent<ItemObject, Crafting.OverrideData> _onNewItemCraftedEvent = new MbEvent<ItemObject, Crafting.OverrideData>();
        public override void RegisterEvents()
        {
            try
            {
                CampaignEvents.OnNewItemCraftedEvent.AddNonSerializedListener(this, OnNewItemCraftedEvent);
            }
            catch (Exception ex)
            {
                IM.MessageError("Kaoses Projectiles Fatal error on RegisterEvents" + ex.ToString());
            }
        }

        public override void SyncData(IDataStore dataStore)
        {

        }

        //private void OnNewItemCraftedEvent(ItemObject itemObject, Crafting.OverrideData overRideData)
        private void OnNewItemCraftedEvent(ItemObject itemObject, Crafting.OverrideData overrideData, bool isCraftingOrderItem)
        {
            if (itemObject != null)
            {
                if (itemObject.ItemType == ItemTypeEnum.Bow || itemObject.ItemType == ItemTypeEnum.Crossbow || itemObject.ItemType == ItemTypeEnum.Musket
                   || itemObject.ItemType == ItemTypeEnum.Pistol || itemObject.ItemType == ItemTypeEnum.Thrown)
                {
                    new RangedWeapons(itemObject);
                }
                else if (itemObject.ItemType == ItemTypeEnum.OneHandedWeapon || itemObject.ItemType == ItemTypeEnum.Polearm
                    || itemObject.ItemType == ItemTypeEnum.TwoHandedWeapon)
                {
                    IM.MessageDebug($"IS MELEE WEAPON DO NEW MELEE ITEM MODIFICATION");
                    new MeleeWeapons(itemObject);
                }
                else if (itemObject.ItemType == ItemTypeEnum.Thrown)
                {
                    IM.MessageDebug($"IS Thrown WEAPON DO NEW thrown ITEM MODIFICATION");
                    new Thrown(itemObject);
                }
            }
        }

    }
}
