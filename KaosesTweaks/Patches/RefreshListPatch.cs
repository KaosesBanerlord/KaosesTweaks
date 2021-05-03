using HarmonyLib;
using KaosesTweaks.Settings;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ViewModelCollection.Craft.Smelting;
using TaleWorlds.Core;
using TaleWorlds.Library;
using KaosesTweaks.BTTweaks;


namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(SmeltingVM), "RefreshList")]
    class RefreshListPatch
    {
        private static void Postfix(SmeltingVM __instance, ItemRoster ____playerItemRoster)
        {

            if (MCMSettings.Instance is { } settings && settings.PreventSmeltingLockedItems)
            {
                List<string> locked_items = Campaign.Current.GetCampaignBehavior<IInventoryLockTracker>().GetLocks().ToList<string>();

                bool isLocked(ItemRosterElement item)
                {
                    string text = item.EquipmentElement.Item.StringId;
                    if (item.EquipmentElement.ItemModifier != null)
                    {
                        text += item.EquipmentElement.ItemModifier.StringId;
                    }
                    return locked_items.Contains(text);
                }

                MBBindingList<SmeltingItemVM> filteredList = new MBBindingList<SmeltingItemVM>();

                foreach (SmeltingItemVM sItem in __instance.SmeltableItemList)
                {
                    if (!____playerItemRoster.Any(rItem =>
                        sItem.EquipmentElement.Item == rItem.EquipmentElement.Item && isLocked(rItem)
                    ))
                    {
                        filteredList.Add(sItem);
                    }
                }

                __instance.SmeltableItemList = filteredList;

                if (__instance.SmeltableItemList.Count == 0)
                {
                    __instance.CurrentSelectedItem = null;
                }
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.SmeltingTweakEnabled;
    }

    [HarmonyPatch(typeof(SmeltingVM), "RefreshList")]
    [HarmonyPriority(Priority.VeryLow)]
    public class RefreshListRenamePatch
    {
        private static void Postfix(SmeltingVM __instance, ItemRoster ____playerItemRoster)
        {
            if (MCMSettings.Instance is { } settings && settings.AutoLearnSmeltedParts)
            {
                foreach (SmeltingItemVM item in __instance.SmeltableItemList)
                {
                    int count = SmeltingHelper.GetNewPartsFromSmelting(item.EquipmentElement.Item).Count();
                    if (count > 0)
                    {
                        string parts = count == 1 ? "part" : "parts";
                        item.Name = $"{item.Name} ({count} new {parts})";
                    }
                }
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.SmeltingTweakEnabled;
    }
}
