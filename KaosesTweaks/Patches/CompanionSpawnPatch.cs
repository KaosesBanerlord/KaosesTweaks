using HarmonyLib;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem.CampaignBehaviors;

namespace KaosesTweaks.Patches
{
    /*
    [HarmonyPatch(typeof(NotablesCampaignBehavior), "SpawnNotablesIfNeeded")]
    class SpawnNotablesIfNeededPatch
    {
        private static bool Prefix(ref int ____randomCompanionSpawnFrequencyInWeeks)
        {
            //____randomCompanionSpawnFrequencyInWeeks = MCMSettings.Instance.CompanionSpawnInterval;
            return true;
        }
        static bool Prepare() => MCMSettings.Instance is { } settings && settings.CompanionSpawnInterval != 6;
    }
    */
}
