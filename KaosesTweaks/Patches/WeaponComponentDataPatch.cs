using HarmonyLib;
using KaosesTweaks.Settings;
using TaleWorlds.Core;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(WeaponComponentData), "CanHitMultipleTargets", MethodType.Getter)]
    class WeaponComponentDataPatch
    {
        static void Postfix(ref bool __result, WeaponComponentData __instance)
        {
            if (MCMSettings.Instance is { } settings)
            {
                __result = (settings.TwoHandedWeaponsSliceThroughEnabled && __instance.WeaponClass == WeaponClass.TwoHandedAxe || __instance.WeaponClass == WeaponClass.TwoHandedMace ||
                    __instance.WeaponClass == WeaponClass.TwoHandedPolearm || __instance.WeaponClass == WeaponClass.TwoHandedSword) ||
                    (settings.SingleHandedWeaponsSliceThroughEnabled && __instance.WeaponClass == WeaponClass.OneHandedSword ||
                    __instance.WeaponClass == WeaponClass.OneHandedPolearm || __instance.WeaponClass == WeaponClass.OneHandedAxe);
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && (settings.SliceThroughEnabled);
    }
}
