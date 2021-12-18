using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace KaosesTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultRansomValueCalculationModel), "PrisonerRansomValue")]
    class PrisonerRansomValuePatch
    {
        private static void Postfix(CharacterObject prisoner, Hero sellerHero, ref int __result)
        {
            if (MCMSettings.Instance.PrisonerPriceTweaksEnabled)
            {
                float tmp = __result * MCMSettings.Instance.PrisonerPriceMultiplier;
                __result = (int)tmp;
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.PrisonerPriceTweaksEnabled;
    }

}
