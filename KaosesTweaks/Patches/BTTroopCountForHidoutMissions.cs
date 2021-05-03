using HarmonyLib;
using KaosesTweaks.Settings;
using System;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

namespace KaosesTweaks.Patches
{
    class BTTroopCountForHidoutMissions
    {
        [HarmonyPatch(typeof(DefaultBanditDensityModel), "GetPlayerMaximumTroopCountForHideoutMission")]
        static bool Prefix(ref int __result)
        {
            if (MCMSettings.Instance is null) return true;
            __result = Math.Min(MCMSettings.Instance.HideoutBattleTroopLimit, 90);
            return false;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.HideoutBattleTroopLimitTweakEnabled;
    }
}
