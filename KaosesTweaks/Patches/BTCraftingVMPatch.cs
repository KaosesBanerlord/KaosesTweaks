using HarmonyLib;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem.ViewModelCollection;

namespace KaosesTweaks.Patches
{
    class BTCraftingVMPatch
    {
        [HarmonyPatch(typeof(CraftingVM), "HaveEnergy")]
        static bool Prefix(ref bool __result)
        {
            __result = true;
            return false;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.IgnoreCraftingStamina;
    }
}
