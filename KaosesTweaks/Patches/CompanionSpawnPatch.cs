using HarmonyLib;
using KaosesTweaks.Objects;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem.CampaignBehaviors;

namespace KaosesTweaks.Patches
{

    /*    [HarmonyPatch(typeof(NotablesCampaignBehavior), "SpawnNotablesIfNeeded")]
        class SpawnNotablesIfNeededPatch
        {
            private static bool Prefix(ref int ____randomCompanionSpawnFrequencyInWeeks)
            {
                //____randomCompanionSpawnFrequencyInWeeks = Factory.Settings.CompanionSpawnInterval;
                return true;
            }
            static bool Prepare() => Factory.Settings is { } settings && settings.CompanionSpawnInterval != 6;
        }
    */





}
