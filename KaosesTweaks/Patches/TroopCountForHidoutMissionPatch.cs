using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesCommon.Utils;
using System;
using TaleWorlds.CampaignSystem.GameComponents;
using KaosesTweaks.Objects;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultBanditDensityModel), "GetPlayerMaximumTroopCountForHideoutMission")]
    public class TroopCountForHidoutMissionPatch
    {
        public static bool Prefix(ref int __result)
        {
            if (Factory.Settings.HideoutBattleTroopLimitTweakEnabled)
            {
                if (Factory.Settings.BattleSizeDebug)
                {
                    IM.MessageDebug($"Hideout Battle Troop Limit Tweak: original: {__result}");
                }
                __result = Math.Min(Factory.Settings.HideoutBattleTroopLimit, 90);
                if (Factory.Settings.BattleSizeDebug)
                {
                    IM.MessageDebug($"Hideout Battle Troop Limit Tweak: modified: {__result}");
                }
                return false;
            }
            return true;

        }

        static bool Prepare() => Factory.Settings is { } settings && settings.HideoutBattleTroopLimitTweakEnabled;
    }
}