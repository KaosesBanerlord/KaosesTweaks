using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;

namespace KaosesTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultRansomValueCalculationModel), "PrisonerRansomValue")]
    class PrisonerRansomValuePatch
    {
        private static void Postfix(CharacterObject prisoner, Hero sellerHero, ref int __result)
        {
            if (KTSettings.Instance.PrisonerPriceTweaksEnabled)
            {
                float tmp = __result * KTSettings.Instance.PrisonerPriceMultiplier;
                __result = (int)tmp;
            }
        }

        static bool Prepare() => KTSettings.Instance is { } settings && settings.PrisonerPriceTweaksEnabled;
    }

}
