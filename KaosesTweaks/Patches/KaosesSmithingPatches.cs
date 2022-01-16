using HarmonyLib;
using KaosesTweaks.BTTweaks;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Craft.Smelting;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace KaosesTweaks.Patches
{
    //~ BT Tweaks
    [HarmonyPatch(typeof(CraftingCampaignBehavior), "DoSmelting")]
    public class DoSmeltingPatch
    {
        private static MethodInfo? openPartMethodInfo;

        static void Postfix(CraftingCampaignBehavior __instance, EquipmentElement equipmentElement)
        {
            ItemObject item = equipmentElement.Item;
            if (item == null) return;
            if (__instance == null) throw new ArgumentNullException(nameof(__instance), $"Tried to run postfix for {nameof(CraftingCampaignBehavior)}.DoSmelting but the instance was null.");
            if (openPartMethodInfo == null) GetMethodInfo();
            foreach (CraftingPiece piece in SmeltingHelper.GetNewPartsFromSmelting(item))
            {
                if (piece != null && piece.Name != null && openPartMethodInfo != null)
                    openPartMethodInfo.Invoke(__instance, new object[] { piece, true });
            }
        }

        static bool Prepare()
        {
            if (MCMSettings.Instance is { } settings)
            {
                if (settings.AutoLearnSmeltedParts)
                    GetMethodInfo();
                return settings.AutoLearnSmeltedParts;
            }
            else return false;
        }

        private static void GetMethodInfo()
        {
            openPartMethodInfo = typeof(CraftingCampaignBehavior).GetMethod("OpenPart", BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }

    [HarmonyPatch(typeof(CraftingCampaignBehavior), "OnSessionLaunched")]
    public class OnSessionLaunchedPatch
    {
        static void Postfix(CraftingCampaignBehavior __instance, CraftingPiece[] ____allCraftingParts, List<CraftingPiece> ____openedParts)
        {

            if (Statics._settings.craftingUnlockAllParts)
            {
                if (____allCraftingParts == null)
                {
                    ____allCraftingParts = (from x in CraftingPiece.All
                                            orderby x.Id
                                            select x).ToArray<CraftingPiece>();
                }
                int num = ____allCraftingParts.Length;
                int count = ____openedParts.Count;
                SmithingModel smithingModel = Campaign.Current.Models.SmithingModel;
                CraftingPiece[] array = (from x in ____allCraftingParts
                                         where !____openedParts.Contains(x)
                                         select x).ToArray<CraftingPiece>();
                if (Statics._settings.craftingUnlockAllParts)
                {
                    if (array.Length != 0 && count < num)
                    {
                        foreach (CraftingPiece craftingPiece in array)
                        {
                            if (!____openedParts.Contains(craftingPiece))
                            {
                                ____openedParts.Add(craftingPiece);
                            }
                        }
                        InformationManager.AddQuickInformation(new TextObject("{=p9F90bc0}KT All Smithing Parts Unlocked:", null), 0, null, "");

                    }
                }
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.craftingUnlockAllParts;
    }

    [HarmonyPatch(typeof(CraftingCampaignBehavior), "GetMaxHeroCraftingStamina")]
    public class GetMaxHeroCraftingStaminaPatch
    {
        static void Postfix(CraftingCampaignBehavior __instance, ref int __result)
        {
            __result = MCMSettings.Instance is { } settings ? MathF.Round(settings.MaxCraftingStaminaMultiplier * __result)  : __result;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.CraftingStaminaTweakEnabled;
    }

    [HarmonyPatch(typeof(CraftingCampaignBehavior), "HourlyTick")]
    public class HourlyTickPatch
    {
        private static FieldInfo? recordsInfo;

        static bool Prefix(CraftingCampaignBehavior __instance)
        {
            if (recordsInfo == null)
                GetRecordsInfo();

            if (recordsInfo == null || __instance == null) throw new ArgumentNullException(nameof(__instance), $"Tried to run postfix for {nameof(CraftingCampaignBehavior)}.HourlyTickPatch but the recordsInfo or __instance was null.");

            IDictionary records = (IDictionary)recordsInfo.GetValue(__instance);

            foreach (Hero hero in records.Keys)
            {
                int curCraftingStamina = __instance.GetHeroCraftingStamina(hero);

                if (!(MCMSettings.Instance is null) && curCraftingStamina < __instance.GetMaxHeroCraftingStamina(hero))
                {
                    int staminaGainAmount = MCMSettings.Instance.CraftingStaminaGainAmount;

                    if (MCMSettings.Instance.CraftingStaminaGainOutsideSettlementMultiplier < 1 && hero.PartyBelongedTo?.CurrentSettlement == null)
                        staminaGainAmount = (int)Math.Ceiling(staminaGainAmount * MCMSettings.Instance.CraftingStaminaGainOutsideSettlementMultiplier);

                    int diff = __instance.GetMaxHeroCraftingStamina(hero) - curCraftingStamina;
                    if (diff < staminaGainAmount)
                        staminaGainAmount = diff;

                    __instance.SetHeroCraftingStamina(hero, Math.Min(__instance.GetMaxHeroCraftingStamina(hero), curCraftingStamina + staminaGainAmount));
                }
            }
            return false;
        }

        static bool Prepare()
        {
            if (MCMSettings.Instance is { } settings)
            {
                if (settings.CraftingStaminaTweakEnabled)
                    GetRecordsInfo();
                return settings.CraftingStaminaTweakEnabled;
            }
            else return false;
        }

        private static void GetRecordsInfo()
        {
            recordsInfo = typeof(CraftingCampaignBehavior).GetField("_heroCraftingRecords", BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }

    class BTCraftingVMPatch
    {
        [HarmonyPatch(typeof(CraftingVM), "HaveEnergy")]
        static bool Prefix(ref bool __result)
        {
            __result = true;
            return false;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.SmithingEnergyDisable;
    }

    [HarmonyPatch(typeof(SmeltingVM), "RefreshList")]
    class RefreshListPatch
    {
        private static void Postfix(SmeltingVM __instance, ItemRoster ____playerItemRoster, Action ____updateValuesOnSelectItemAction)
        {

            if (MCMSettings.Instance is { } settings && settings.PreventSmeltingLockedItems)
            {
                List<string> locked_items = Campaign.Current.GetCampaignBehavior<ViewDataTrackerCampaignBehavior>().GetInventoryLocks().ToList<string>();

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
                __instance.SortController.SetListToControl(__instance.SmeltableItemList);

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


    //~ KT Tweaks

    //~ XP Tweaks
    [HarmonyPatch(typeof(DefaultSmithingModel), "GetSkillXpForRefining")]
    public class GetSkillXpForRefiningPatch
    {
        static bool Prefix(DefaultSmithingModel __instance, ref Crafting.RefiningFormula refineFormula, ref int __result)
        {
            if (Statics._settings.SmithingXpModifiers)
            {
                float baseXp = MathF.Round(0.3f * (__instance.GetCraftingMaterialItem(refineFormula.Output).Value * refineFormula.OutputCount));
                baseXp *= Statics._settings.SmithingRefiningXpValue;
                if (Statics._settings.CraftingDebug)
                {
                    IM.MessageDebug("GetSkillXpForRefining  base: " + MathF.Round(0.3f * (__instance.GetCraftingMaterialItem(refineFormula.Output).Value * refineFormula.OutputCount)).ToString() + "  new :" + baseXp.ToString());
                }
                __result = (int)baseXp;
                return false;
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance != null && MCMSettings.Instance.SmithingXpModifiers && MCMSettings.Instance.MCMSmithingHarmoneyPatches;
    }

    [HarmonyPatch(typeof(DefaultSmithingModel), "GetSkillXpForSmelting")]
    public class GetSkillXpForSmeltingPatch
    {
        static bool Prefix(ItemObject item, ref int __result)
        {
            if (Statics._settings.SmithingXpModifiers)
            {
                IM.MessageDebug("GetSkillXpForSmelting Patch called");
                float baseXp = MathF.Round(0.02f * item.Value);
                baseXp *= Statics._settings.SmithingSmeltingXpValue;
                IM.MessageDebug("GetSkillXpForSmelting  base: " + MathF.Round(0.02f * item.Value).ToString() + "  new :" + baseXp.ToString());
                __result = (int)baseXp;
                return false;
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance != null && MCMSettings.Instance.SmithingXpModifiers && MCMSettings.Instance.MCMSmithingHarmoneyPatches;
    }

    [HarmonyPatch(typeof(DefaultSmithingModel), "GetSkillXpForSmithing")]
    public class GetSkillXpForSmithingPatch
    {
        static bool Prefix(DefaultSmithingModel __instance, ItemObject item, ref int __result)
        {
            if (Statics._settings.SmithingXpModifiers)
            {
                float baseXp = MathF.Round(0.1f * item.Value);
                baseXp *= Statics._settings.SmithingSmithingXpValue;
                if (Statics._settings.CraftingDebug)
                {
                    IM.MessageDebug("GetSkillXpForSmithing  base: " + MathF.Round(0.1f * item.Value).ToString() + "  new :" + baseXp.ToString());
                }
                __result = (int)baseXp;
                return false;
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance != null && MCMSettings.Instance.SmithingXpModifiers && MCMSettings.Instance.MCMSmithingHarmoneyPatches;
    }

    //~ Energy Tweaks
    [HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForRefining")]
    public class GetEnergyCostForRefiningPatch
    {
        static bool Prefix(Hero hero, ref int __result)
        {
            if (Statics._settings.SmithingEnergyDisable || Statics._settings.CraftingStaminaTweakEnabled)
            {
                IM.MessageDebug("GetEnergyCostForRefining Patch called");
                int num = 6;
                if (Statics._settings.SmithingEnergyDisable)
                {
                    IM.MessageDebug("GetEnergyCostForRefining: DISABLED ");
                    __result = 0;
                    return false;
                }
                else //if (Statics._settings.CraftingStaminaTweakEnabled)
                {
                    float tmp = num * Statics._settings.SmithingEnergyRefiningValue;
                    IM.MessageDebug("GetEnergyCostForRefining Old : " + num.ToString() + " New : " + tmp.ToString());
                    num = (int)tmp;
                    if (hero.GetPerkValue(DefaultPerks.Crafting.PracticalRefiner))
                    {
                        num = (num + 1) / 2;
                    }
                    __result = num;
                    return false;
                }
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && (settings.SmithingEnergyDisable || settings.CraftingStaminaTweakEnabled) && MCMSettings.Instance.MCMSmithingHarmoneyPatches;
    }

    [HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForSmithing")]
    public class GetEnergyCostForSmithingPatch
    {
        static bool Prefix(ItemObject item, Hero hero, ref int __result)
        {
            if (Statics._settings.SmithingEnergyDisable || Statics._settings.CraftingStaminaTweakEnabled)
            {
                int.TryParse(item.Tier.ToString(), out int itemTier);
                int tier6 = 6;
                int num = 10 + tier6 * itemTier;
                if (Statics._settings.SmithingEnergyDisable)
                {
                    if (Statics._settings.CraftingDebug)
                    {
                        IM.MessageDebug("GetEnergyCostForSmithing: DISABLED ");
                    }
                    __result = 0;
                    return false;
                }
                else
                {
                    float tmp = num * Statics._settings.SmithingEnergySmithingValue;
                    if (Statics._settings.CraftingDebug)
                    {
                        IM.MessageDebug("GetEnergyCostForSmithing Old : " + num.ToString() + " New : " + tmp.ToString());
                    }
                    num = (int)tmp;
                    if (hero.GetPerkValue(DefaultPerks.Crafting.PracticalSmith))
                    {
                        num = (num + 1) / 2;
                    }
                    __result = num;
                    return false;
                }
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && (settings.SmithingEnergyDisable || settings.CraftingStaminaTweakEnabled) && MCMSettings.Instance.MCMSmithingHarmoneyPatches;
    }

    [HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForSmelting")]
    public class GetEnergyCostForSmeltingPatch
    {
        static bool Prefix(Hero hero, ref int __result)
        {

            if (Statics._settings.SmithingEnergyDisable || Statics._settings.CraftingStaminaTweakEnabled)
            {
                IM.MessageDebug("GetEnergyCostForSmelting Patch called");
                int num = 10;
                if (Statics._settings.SmithingEnergyDisable)
                {
                    if (Statics._settings.CraftingDebug)
                    {
                        IM.MessageDebug("GetEnergyCostForSmelting: DISABLED ");
                    }
                    __result = 0;
                    return false;
                }
                else
                {
                    float tmp = num * Statics._settings.SmithingEnergySmeltingValue;
                    if (Statics._settings.CraftingDebug)
                    {
                        IM.MessageDebug("GetEnergyCostForSmelting Old : " + num.ToString() + " New : " + tmp.ToString());
                    }
                    num = (int)tmp;
                    if (hero.GetPerkValue(DefaultPerks.Crafting.PracticalSmelter))
                    {
                        num = (num + 1) / 2;
                    }
                    __result = num;
                    return false;
                }
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && (settings.SmithingEnergyDisable || settings.CraftingStaminaTweakEnabled) && MCMSettings.Instance.MCMSmithingHarmoneyPatches;

    }













}
