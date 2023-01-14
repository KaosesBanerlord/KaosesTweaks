using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesCommon.Utils;
using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using KaosesTweaks.Objects;

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
    [HarmonyPatch(typeof(MissionAgentSpawnLogic), MethodType.Constructor, new Type[] { typeof(IMissionTroopSupplier[]), typeof(BattleSideEnum), typeof(Mission.BattleSizeType) })]
    public class TweakedBattleSizePatch
    {
        static void Postfix(MissionAgentSpawnLogic __instance, ref int ____battleSize)
        {
            if (Factory.Settings.BattleSize > 0)
            {
                ____battleSize = Factory.Settings.BattleSize;
                if (Factory.Settings.BattleSizeDebug)
                {
                    IM.MessageGreen("Max Battle Size Modified to: " + Factory.Settings.BattleSize + "  original size: " + ____battleSize);
                }

            if (MCMSettings.Instance is { } settings && settings.BattleSize > 0)
            {
                ____battleSize = settings.BattleSize;
                IM.ColorGreenMessage("Max Battle Size Modified to: " + settings.BattleSize);
            }

            return;
        }

        static bool Prepare() => Factory.Settings is { } settings && settings.BattleSizeTweakEnabled && !settings.BattleSizeTweakExEnabled;
    }
}

