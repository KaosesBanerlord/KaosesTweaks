using HarmonyLib;
using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Core;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(MissionAgentSpawnLogic), MethodType.Constructor, new Type[] { typeof(IMissionTroopSupplier[]), typeof(BattleSideEnum) })]
    public class TweakedBattleSizePatch2
    {
        static void Postfix(MissionAgentSpawnLogic __instance, ref int ____battleSize)
        {

            if (MCMSettings.Instance is { } settings && settings.BattleSize > 0)
            {
                ____battleSize = settings.BattleSize;
                IM.ColorGreenMessage("Max Battle Size Modified to: " + settings.BattleSize);
            }

            return;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.BattleSizeTweakEnabled;
    }
}
