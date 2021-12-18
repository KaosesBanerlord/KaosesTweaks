using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(MissionAgentSpawnLogic), MethodType.Constructor, new Type[] { typeof(IMissionTroopSupplier[]), typeof(BattleSideEnum), typeof(bool) })]
    public class TweakedBattleSizePatch
    {
        static void Postfix(MissionAgentSpawnLogic __instance, ref int ____battleSize)
        {

            if (MCMSettings.Instance is { } settings && settings.BattleSize > 0)
            {
                ____battleSize = settings.BattleSize;
                if (Statics._settings.BattleSizeDebug)
                {
                    IM.ColorGreenMessage("Max Battle Size Modified to: " + settings.BattleSize);
                }

            }

            return;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.BattleSizeTweakEnabled && !settings.BattleSizeTweakExEnabled;
    }
}
