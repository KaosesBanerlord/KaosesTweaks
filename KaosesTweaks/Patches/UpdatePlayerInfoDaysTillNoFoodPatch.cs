using HarmonyLib;
using KaosesTweaks.Objects;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapBar;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(MapInfoVM), "UpdatePlayerInfo")]
    class UpdatePlayerInfoDaysTillNoFoodPatch
    {
        private static void Postfix(MapInfoVM __instance)
        {
            __instance.TotalFood = MobileParty.MainParty.GetNumDaysForFoodToLast() + 1;
        }

        static bool Prepare() => Factory.Settings is { } settings && settings.ShowFoodDaysRemaining;
    }
}
