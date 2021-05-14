using HarmonyLib;
using KaosesTweaks.Settings;
using System;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultBanditDensityModel), "GetPlayerMaximumTroopCountForHideoutMission")]
    public class BTTroopCountForHidoutMission
    {
        public static bool Prefix(ref int __result)
        {
            if (MCMSettings.Instance.HideoutBattleTroopLimitTweakEnabled)
            {
                __result = Math.Min(MCMSettings.Instance.HideoutBattleTroopLimit, 90);
                return false;
            }
            return true;

        }

        //static bool Prepare() => MCMSettings.Instance is { } settings && settings.HideoutBattleTroopLimitTweakEnabled;
    }
}