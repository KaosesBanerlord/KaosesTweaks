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
            if (KTSettings.Instance.HideoutBattleTroopLimitTweakEnabled)
            {
                if (KTSettings.Instance.BattleSizeDebug)
                {
                    IM.MessageDebug($"Hideout Battle Troop Limit Tweak: original: {__result}");
                }
                __result = Math.Min(KTSettings.Instance.HideoutBattleTroopLimit, 90);
                if (KTSettings.Instance.BattleSizeDebug)
                {
                    IM.MessageDebug($"Hideout Battle Troop Limit Tweak: modified: {__result}");
                }
                return false;
            }
            return true;

        }

        //static bool Prepare() => KTSettings.Instance is { } settings && settings.HideoutBattleTroopLimitTweakEnabled;
    }
}