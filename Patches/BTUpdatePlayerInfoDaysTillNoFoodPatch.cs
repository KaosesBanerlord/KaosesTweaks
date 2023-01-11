using HarmonyLib;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapBar;

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
