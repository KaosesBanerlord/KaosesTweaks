using HarmonyLib;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(MapInfoVM), "UpdatePlayerInfo")]
    class BTUpdatePlayerInfoDaysTillNoFoodPatch
    {
        private static void Postfix(MapInfoVM __instance)
        {
            __instance.TotalFood = MobileParty.MainParty.GetNumDaysForFoodToLast() + 1;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.ShowFoodDaysRemaining;
    }
}
