using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;


/**
 * 
 * @UPDATE Replace with the below code over
 * 
 * 
 * 
 * namespace TaleWorlds.MountAndBlade
{
  public class MissionAgentSpawnLogic


namespace TaleWorlds.MountAndBlade
{
  public static class BannerlordConfig

 * 
 * 
 * 
 * 
*/

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(MissionAgentSpawnLogic), MethodType.Constructor, new Type[] { typeof(IMissionTroopSupplier[]), typeof(BattleSideEnum), typeof(Mission.BattleSizeType)})]
    public class TweakedBattleSizePatch
    {
        static void Postfix(MissionAgentSpawnLogic __instance, ref int ____battleSize)
        {
            if (Statics._settings.BattleSize > 0)
            {
                ____battleSize = Statics._settings.BattleSize;
                if (Statics._settings.BattleSizeDebug)
                {
                    IM.ColorGreenMessage("Max Battle Size Modified to: " + Statics._settings.BattleSize + "  original size: " + ____battleSize);
                }

            }

            return;
        }

        static bool Prepare() => KTSettings.Instance is { } settings && settings.BattleSizeTweakEnabled && !settings.BattleSizeTweakExEnabled;
    }
}

