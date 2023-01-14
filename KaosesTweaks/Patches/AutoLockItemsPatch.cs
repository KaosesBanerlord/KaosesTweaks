using HarmonyLib;
using KaosesTweaks.Objects;
using System;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Inventory;
using TaleWorlds.Core;

namespace KaosesTweaks.Patches
{
    public class AutoLockItemsPatch
    {

        // Token: 0x02000003 RID: 3
        [HarmonyPatch(typeof(SPItemVM))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[]
        {
        typeof(InventoryLogic),
        typeof(bool),
        typeof(bool),
        typeof(InventoryMode),
        typeof(ItemRosterElement),
        typeof(InventoryLogic.InventorySide),
        typeof(string),
        typeof(string),
        typeof(int),
        typeof(EquipmentIndex?)
        })]
        public class SPItemVMPatch
        {
            // Token: 0x06000004 RID: 4 RVA: 0x00002088 File Offset: 0x00000288
            public static void Postfix(SPItemVM __instance)
            {
                if (__instance.InventorySide == InventoryLogic.InventorySide.PlayerInventory && Factory.Settings.MCMAutoLocks)
                {
                    bool isHorse = __instance.ItemType == EquipmentIndex.Horse;
                    if (isHorse && !__instance.StringId.Contains("lame") && Factory.Settings.autoLockHorses)
                    {
                        __instance.IsLocked = true;
                    }
                    bool isFood = __instance.ItemRosterElement.EquipmentElement.Item.IsFood;
                    if (isFood && Factory.Settings.autoLockFood)
                    {
                        __instance.IsLocked = true;
                    }
                    if (__instance.IsCivilianItem && !isFood)
                    {
                        if (__instance.StringId == "ironIngot1" && Factory.Settings.autoLockIronBar1)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "ironIngot2" && Factory.Settings.autoLockIronBar2)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "ironIngot3" && Factory.Settings.autoLockIronBar3)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "ironIngot4" && Factory.Settings.autoLockIronBar4)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "ironIngot5" && Factory.Settings.autoLockIronBar5)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "ironIngot6" && Factory.Settings.autoLockIronBar6)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "iron" && Factory.Settings.autoLockIronOre)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "silver" && Factory.Settings.autoLockSilverOre)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "hardwood" && Factory.Settings.autoLockHardwood)
                        {
                            __instance.IsLocked = true;
                        }


                        if (__instance.StringId == "charcoal" && Factory.Settings.autoLockCharcol)
                        {
                            __instance.IsLocked = true;
                        }
                    }
                }
            }
        }
    }
}
