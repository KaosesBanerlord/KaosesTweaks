using HarmonyLib;
using KaosesTweaks.Objects;
using KaosesTweaks.Settings;
using TaleWorlds.Core;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(WeaponComponentData), "CanHitMultipleTargets", MethodType.Getter)]
    class WeaponComponentDataPatch
    {
        static void Postfix(ref bool __result, WeaponComponentData __instance)
        {
            if (Factory.Settings is { } settings)
            {
                bool twoHanded = settings.TwoHandedWeaponsSliceThroughEnabled && __instance.WeaponClass == WeaponClass.TwoHandedAxe ||
                    __instance.WeaponClass == WeaponClass.TwoHandedMace || __instance.WeaponClass == WeaponClass.TwoHandedPolearm ||
                    __instance.WeaponClass == WeaponClass.TwoHandedSword;

                bool oneHanded = settings.SingleHandedWeaponsSliceThroughEnabled && __instance.WeaponClass == WeaponClass.OneHandedSword ||
                    __instance.WeaponClass == WeaponClass.OneHandedPolearm || __instance.WeaponClass == WeaponClass.OneHandedAxe;

                bool all = settings.AllWeaponsSliceThroughEnabled;

                __result = twoHanded || oneHanded || all;
            }
        }

        static bool Prepare() => Factory.Settings is { } settings && settings.SliceThroughEnabled;
    }
}
