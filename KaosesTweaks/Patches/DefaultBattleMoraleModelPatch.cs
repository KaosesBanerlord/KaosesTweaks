using HarmonyLib;
using KaosesTweaks.Settings;
using SandBox.GameComponents;
using System;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMaxMoraleChangeDueToAgentIncapacitated")]
    class CalculateMaxMoraleChangeDueToAgentIncapacitatedPatch
    {
        static void Postfix(ref ValueTuple<float, float> __result)
        {
            if (!(KTSettings.Instance is null))
            {

                __result = new ValueTuple<float, float>(__result.Item1 * Statics._settings.BattleMoralTweaksMultiplier, __result.Item2 * Statics._settings.BattleMoralTweaksMultiplier);
            }
        }

        static bool Prepare() => KTSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
    }

    [HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMaxMoraleChangeDueToAgentPanicked")]
    class CalculateMaxMoraleChangeDueToAgentPanickedPatch
    {
        static void Postfix(ref ValueTuple<float, float> __result)
        {
            if (!(KTSettings.Instance is null))
            {

                __result = new ValueTuple<float, float>(__result.Item1 * Statics._settings.BattleMoralTweaksMultiplier, __result.Item2 * Statics._settings.BattleMoralTweaksMultiplier);
            }
        }

        static bool Prepare() => KTSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
    }

    [HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMoraleChangeToCharacter")]
    class CalculateMoraleChangeToCharacterPatch
    {
        static void Postfix(ref float __result)
        {
            if (!(KTSettings.Instance is null))
            {

                __result *= Statics._settings.BattleMoralTweaksMultiplier;
            }
        }

        static bool Prepare() => KTSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
    }
}
