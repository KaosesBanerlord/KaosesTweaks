using HarmonyLib;
using KaosesCommon.Utils;
using KaosesTweaks.Objects;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Settlements;
using static HarmonyLib.AccessTools;

namespace KaosesTweaks.Patches
{

    [HarmonyPatch(typeof(CompanionsCampaignBehavior))]
    [HarmonyPatch("_desiredTotalCompanionCount", MethodType.Getter)]
    class MapRoomFunctionality_get_desiredTotalCompanionCount_Patch
    {
        public static void Postfix(ref float __result, CompanionsCampaignBehavior __instance)
        {
            //__result = Factory.Settings.DesiredTotalCompanionCount;
            __result = 53;
        }
    }



    //[HarmonyReversePatch(HarmonyReversePatchType.Original)]
    [HarmonyPatch(typeof(CompanionsCampaignBehavior), "CreateCompanionAndAddToSettlement")]
    class CreateCompanionAndAddToSettlementPatch
    {

        private static void Postfix(CompanionsCampaignBehavior __instance)
        {
            //AccessTools.Field(typeof(CompanionsCampaignBehavior), "DesiredCompanionPerTown").SetValue(__instance, 2);
            //AccessTools.Property(typeof(CompanionsCampaignBehavior), "_desiredTotalCompanionCount").SetValue(__instance, 100);
            //AccessTools.Property(__instance.GetType(), "_desiredTotalCompanionCount").SetValue(__instance, 100);
            //Town.AllTowns.Count = 53
            //Town.AllTowns.Count * 0.6f = 31.8
            //___DesiredCompanionPerTown = 0.6f;
            //____desiredTotalCompanionCount = (float)Town.AllTowns.Count * 0.6f;

            //float tempFloat = (float)Town.AllTowns.Count * 0.6f;
            IM.MessageDebug("CreateCompanionAndAddToSettlement called");
            //return true;
        }


        //static bool Prepare() => Factory.Settings is { } settings && settings.UnlimitedWanderersPatch;
    }

    //[HarmonyReversePatch(HarmonyReversePatchType.Original)]
    /*    [HarmonyPatch(typeof(CompanionsCampaignBehavior), "CreateCompanionAndAddToSettlement")]
        class CreateCompanionAndAddToSettlementUnPatch
        {
            private static void Postfix(CompanionsCampaignBehavior __instance, Settlement settlement, Dictionary<CharacterObject, int> ____companionTemplates, int ____cachedCompanionCount)
            {
                IM.MessageDebug("___companionTemplates: " + ____companionTemplates.Count() + "  ____cachedCompanionCount: " + ____cachedCompanionCount.ToString());
                //return true;
            }

            static bool Prepare() => Factory.Settings is { } settings && !settings.UnlimitedWanderersPatch;
        }*/
}
