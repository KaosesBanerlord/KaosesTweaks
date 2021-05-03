using HarmonyLib;
using System;
using System.Collections;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using KaosesTweaks.BTTweaks;
using KaosesTweaks.Settings;

namespace KaosesTweaks.Patches
{
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

    [HarmonyPatch(typeof(CraftingCampaignBehavior), "GetMaxHeroCraftingStamina")]
    public class GetMaxHeroCraftingStaminaPatch
    {
        static bool Prefix(CraftingCampaignBehavior __instance, ref int __result)
        {
            __result = MCMSettings.Instance is { } settings ? settings.MaxCraftingStamina : 100;
            return false;
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

                if (!(MCMSettings.Instance is null) && curCraftingStamina < MCMSettings.Instance.MaxCraftingStamina)
                {
                    int staminaGainAmount = MCMSettings.Instance.CraftingStaminaGainAmount;

                    if (MCMSettings.Instance.CraftingStaminaGainOutsideSettlementMultiplier < 1 && hero.PartyBelongedTo?.CurrentSettlement == null)
                        staminaGainAmount = (int)Math.Ceiling(staminaGainAmount * MCMSettings.Instance.CraftingStaminaGainOutsideSettlementMultiplier);

                    int diff = MCMSettings.Instance.MaxCraftingStamina - curCraftingStamina;
                    if (diff < staminaGainAmount)
                        staminaGainAmount = diff;

                    __instance.SetHeroCraftingStamina(hero, Math.Min(MCMSettings.Instance.MaxCraftingStamina, curCraftingStamina + staminaGainAmount));
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
    
}
