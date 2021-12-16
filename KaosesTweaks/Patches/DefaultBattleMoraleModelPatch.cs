using HarmonyLib;
using KaosesTweaks.Settings;
using SandBox;
using System;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMaxMoraleChangeDueToAgentIncapacitated")]
    class CalculateMaxMoraleChangeDueToAgentIncapacitatedPatch
    {
        static void Postfix(ref ValueTuple<float, float> __result)
        {
            if (MCMSettings.Instance is { } settings)
            {

                __result = new ValueTuple<float, float>(__result.Item1 * settings.BattleMoralTweaksMultiplier, __result.Item2 * settings.BattleMoralTweaksMultiplier);
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
    }

    [HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMaxMoraleChangeDueToAgentPanicked")]
    class CalculateMaxMoraleChangeDueToAgentPanickedPatch
    {
        static void Postfix(ref ValueTuple<float, float> __result)
        {
            if (MCMSettings.Instance is { } settings)
            {

                __result = new ValueTuple<float, float>(__result.Item1 * settings.BattleMoralTweaksMultiplier, __result.Item2 * settings.BattleMoralTweaksMultiplier);
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
    }

    [HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMoraleChangeToCharacter")]
    class CalculateMoraleChangeToCharacterPatch
    {
        static void Postfix(ref float __result)
        {
            if (MCMSettings.Instance is { } settings)
            {

                __result *= settings.BattleMoralTweaksMultiplier;
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
    }
}
