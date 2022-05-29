using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using TaleWorlds.CampaignSystem.GameComponents;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultBanditDensityModel), "GetPlayerMaximumTroopCountForHideoutMission")]
    public class BTTroopCountForHidoutMission
    {
        public static bool Prefix(ref int __result)
        {
            if (MCMSettings.Instance.HideoutBattleTroopLimitTweakEnabled)
            {
                if (MCMSettings.Instance.BattleSizeDebug)
                {
                    IM.MessageDebug($"Hideout Battle Troop Limit Tweak: original: {__result}");
                }
                __result = Math.Min(MCMSettings.Instance.HideoutBattleTroopLimit, 90);
                if (MCMSettings.Instance.BattleSizeDebug)
                {
                    IM.MessageDebug($"Hideout Battle Troop Limit Tweak: modified: {__result}");
                }
                return false;
            }
            return true;

        }

        //static bool Prepare() => MCMSettings.Instance is { } settings && settings.HideoutBattleTroopLimitTweakEnabled;
    }
}