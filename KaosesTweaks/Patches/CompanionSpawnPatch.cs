using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

namespace KaosesTweaks.Patches
{
  [HarmonyPatch(typeof(UrbanCharactersCampaignBehavior), "WeeklyTick")]
  class CompanionSpawnPatch
  {
    private static bool Prefix(ref int ____randomCompanionSpawnFrequencyInWeeks)
    {
      ____randomCompanionSpawnFrequencyInWeeks = MCMSettings.Instance.CompanionSpawnInterval;
      return true;
    }
    static bool Prepare() => MCMSettings.Instance is { } settings && settings.CompanionSpawnInterval != 6;
  }
}
