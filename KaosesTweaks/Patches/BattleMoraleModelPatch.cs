using HarmonyLib;
using KaosesTweaks.Objects;
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
            if (!(Factory.Settings is null))
            {

                __result = new ValueTuple<float, float>(__result.Item1 * Factory.Settings.BattleMoralTweaksMultiplier, __result.Item2 * Factory.Settings.BattleMoralTweaksMultiplier);
            }
        }

        static bool Prepare() => Factory.Settings is { } settings && settings.BattleMoralTweaksEnabled;
    }

    [HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMaxMoraleChangeDueToAgentPanicked")]
    class CalculateMaxMoraleChangeDueToAgentPanickedPatch
    {
        static void Postfix(ref ValueTuple<float, float> __result)
        {
            if (!(Factory.Settings is null))
            {

                __result = new ValueTuple<float, float>(__result.Item1 * Factory.Settings.BattleMoralTweaksMultiplier, __result.Item2 * Factory.Settings.BattleMoralTweaksMultiplier);
            }
        }

        static bool Prepare() => Factory.Settings is { } settings && settings.BattleMoralTweaksEnabled;
    }

    [HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMoraleChangeToCharacter")]
    class CalculateMoraleChangeToCharacterPatch
    {
        static void Postfix(ref float __result)
        {
            if (!(Factory.Settings is null))
            {

                __result *= Factory.Settings.BattleMoralTweaksMultiplier;
            }
        }

        static bool Prepare() => Factory.Settings is { } settings && settings.BattleMoralTweaksEnabled;
    }
}
