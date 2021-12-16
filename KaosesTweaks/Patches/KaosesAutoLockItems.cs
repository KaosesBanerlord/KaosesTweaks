using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;

namespace KaosesTweaks.Patches
{
    public class KaosesAutoLockItems
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
                if (__instance.InventorySide == InventoryLogic.InventorySide.PlayerInventory && Statics._settings.MCMAutoLocks)
                {
                    bool isHorse = __instance.ItemType == EquipmentIndex.Horse;
                    if (isHorse && !__instance.StringId.Contains("lame") && Statics._settings.autoLockHorses)
                    {
                        __instance.IsLocked = true;
                    }
                    bool isFood = __instance.ItemRosterElement.EquipmentElement.Item.IsFood;
                    if (isFood && Statics._settings.autoLockFood)
                    {
                        __instance.IsLocked = true;
                    }
                    if (__instance.IsCivilianItem && !isFood)
                    {
                        if (__instance.StringId == "ironIngot1" && Statics._settings.autoLockIronBar1)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "ironIngot2" && Statics._settings.autoLockIronBar2)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "ironIngot3" && Statics._settings.autoLockIronBar3)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "ironIngot4" && Statics._settings.autoLockIronBar4)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "ironIngot5" && Statics._settings.autoLockIronBar5)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "ironIngot6" && Statics._settings.autoLockIronBar6)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "iron" && Statics._settings.autoLockIronOre)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "silver" && Statics._settings.autoLockSilverOre)
                        {
                            __instance.IsLocked = true;
                        }

                        if (__instance.StringId == "hardwood" && Statics._settings.autoLockHardwood)
                        {
                            __instance.IsLocked = true;
                        }


                        if (__instance.StringId == "charcoal" && Statics._settings.autoLockCharcol)
                        {
                            __instance.IsLocked = true;
                        }
                    }
                }
            }
        }
    }
}
