using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using SandBox;
using System;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMaxMoraleChangeDueToAgentIncapacitated")]
    class CalculateMaxMoraleChangeDueToAgentIncapacitatedPatch
    {
        static void Postfix(ref ValueTuple<float, float> __result)
        {
            if (!(MCMSettings.Instance is null))
            {

                __result = new ValueTuple<float, float>(__result.Item1 * Statics._settings.BattleMoralTweaksMultiplier, __result.Item2 * Statics._settings.BattleMoralTweaksMultiplier);
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
    }

    [HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMaxMoraleChangeDueToAgentPanicked")]
    class CalculateMaxMoraleChangeDueToAgentPanickedPatch
    {
        static void Postfix(ref ValueTuple<float, float> __result)
        {
            if (!(MCMSettings.Instance is null))
            {

                __result = new ValueTuple<float, float>(__result.Item1 * Statics._settings.BattleMoralTweaksMultiplier, __result.Item2 * Statics._settings.BattleMoralTweaksMultiplier);
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
    }

    [HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMoraleChangeToCharacter")]
    class CalculateMoraleChangeToCharacterPatch
    {
        static void Postfix(ref float __result)
        {
            if (!(MCMSettings.Instance is null))
            {

                __result *= Statics._settings.BattleMoralTweaksMultiplier;
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
    }
}
