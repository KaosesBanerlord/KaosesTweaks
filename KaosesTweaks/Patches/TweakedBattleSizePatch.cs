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
    /*    [HarmonyPatch(typeof(MissionAgentSpawnLogic), MethodType.Constructor, new Type[] { typeof(IMissionTroopSupplier[]), typeof(BattleSideEnum), typeof(Mission.BattleSizeType) })]
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
                }
                return;
            }

            static bool Prepare() => Factory.Settings is { } settings && settings.BattleSizeTweakEnabled && !settings.BattleSizeTweakExEnabled;
        }*/
    /*
        private static int[] _battleSizes = new int[7]
        {
          200,
          300,
          400,
          500,
          600,
          800,
          1000
        };
        private static int[] _siegeBattleSizes = new int[7]
        {
          150,
          230,
          320,
          425,
          540,
          625,
          1000
        };
        private static int[] _sallyOutBattleSizes = new int[7]
        {
          150,
          200,
          240,
          280,
          320,
          360,
          400
        };
        private static int[] _reinforcementWaveCounts = new int[4]
        {
          3,
          4,
          5,
          0
        };

        [BannerlordConfig.ConfigPropertyInt(new int[] { 0, 1, 2, 3, 4, 5, 6 }, false)]
        public static int BattleSize
        {
            get => BannerlordConfig._battleSize;
            set => BannerlordConfig._battleSize = value;
        }

        [BannerlordConfig.ConfigPropertyInt(new int[] { 0, 1, 2, 3 }, false)]
        public static int ReinforcementWaveCount { get; set; } = 3;

        public static int GetRealBattleSize() => BannerlordConfig._battleSizes[BannerlordConfig.BattleSize];
        public static int GetRealBattleSizeForSiege() => BannerlordConfig._siegeBattleSizes[BannerlordConfig.BattleSize];
        public static int GetReinforcementWaveCount() => BannerlordConfig._reinforcementWaveCounts[BannerlordConfig.ReinforcementWaveCount];
        public static int GetRealBattleSizeForSallyOut() => BannerlordConfig._sallyOutBattleSizes[BannerlordConfig.BattleSize];
    */

    [HarmonyPatch(typeof(BannerlordConfig), "GetRealBattleSize")]
    public class BattleSizePatch_GetRealBattleSize
    {
        static void Postfix(ref int __result)
        {
            if (Factory.Settings is { } settings)
            {
                if (settings.IsBattleSizeDebug)
                {
                    IM.MessageError("GetRealBattleSize: Battle size original: " + __result + " has been set to " + settings.BattleSize + ".");
                }
                __result = settings.BattleSize;
            }
        }
        static bool Prepare() => Factory.Settings is { } settings && settings.BattleSizeTweakEnabled;
    }

    [HarmonyPatch(typeof(BannerlordConfig), "GetRealBattleSizeForSiege")]
    public class BattleSizePatch_GetRealBattleSizeForSiege
    {
        static void Postfix(ref int __result)
        {
            if (Factory.Settings is { } settings)
            {
                if (settings.IsBattleSizeDebug)
                {
                    IM.MessageError("GetRealBattleSizeForSiege: Battle size original: " + __result + " has been set to " + settings.BattleSize + ".");
                }
                __result = settings.BattleSize;

            }
        }
        static bool Prepare() => Factory.Settings is { } settings && settings.BattleSizeTweakEnabled;
    }
    [HarmonyPatch(typeof(BannerlordConfig), "GetRealBattleSizeForSallyOut")]
    public class BattleSizePatch_GetRealBattleSizeForSallyOut
    {
        static void Postfix(ref int __result)
        {
            if (Factory.Settings is { } settings)
            {
                if (settings.IsBattleSizeDebug)
                {
                    IM.MessageError("GetRealBattleSizeForSallyOut: Battle size original: " + __result + " has been set to " + settings.BattleSize / 2 + ".");
                }
                __result = settings.BattleSize / 2;

            }
        }
        static bool Prepare() => Factory.Settings is { } settings && settings.BattleSizeTweakEnabled;
    }

}

