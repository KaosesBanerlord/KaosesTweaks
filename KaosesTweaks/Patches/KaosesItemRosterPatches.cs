using HarmonyLib;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using static TaleWorlds.Core.ItemObject;
using KaosesTweaks.Objects;
using System;
using KaosesTweaks.Utils;

namespace KaosesTweaks.Patches
{
    //~ BT Tweaks
    [HarmonyPatch(typeof(ItemRoster), "AddToCounts")]
    [HarmonyPatch(new Type[]
        {
        typeof(ItemObject),
        typeof(int)
        })]
    public class AddToCountsPatch
    {
        static bool Prefix(ItemRoster __instance, ItemObject item, int number, int __result)
        {
/*
            if (Statics._settings.MCMItemModifiers)
            {
                if (number == 0)
                {
                    __result = - 1;
                }
                if (Statics._settings.ItemDebugMode)
                {
                    IM.MessageDebug($"AddToCounts patch called IsCraftedByPlayer: {item.IsCraftedByPlayer} IsCraftedWeapon:{item.IsCraftedWeapon} ");
                }
                if (item.IsCraftedByPlayer && item.IsCraftedWeapon)
                {
                    if (item.ItemType == ItemTypeEnum.Bow || item.ItemType == ItemTypeEnum.Crossbow || item.ItemType == ItemTypeEnum.Musket
                       || item.ItemType == ItemTypeEnum.Pistol || item.ItemType == ItemTypeEnum.Thrown)
                    {
                        new RangedWeapons(item);
                    }
                    else if (item.ItemType == ItemTypeEnum.OneHandedWeapon || item.ItemType == ItemTypeEnum.Polearm
                        || item.ItemType == ItemTypeEnum.TwoHandedWeapon)
                    {
                        IM.MessageDebug($"IS MELEE WEAPON DO NEW MELEE ITEM MODIFICATION");
                        new MeleeWeapons(item);
                    }
                    __result = __instance.AddToCounts(new EquipmentElement(item, null), number);
                    return false;
                }
            }*/
            return true;
        }
        static bool Prepare() => MCMSettings.Instance is { } settings && settings.MCMItemModifiers;
    }
}
