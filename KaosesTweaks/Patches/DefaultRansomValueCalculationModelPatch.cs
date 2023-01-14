using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesCommon.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using KaosesTweaks.Objects;

namespace KaosesTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultRansomValueCalculationModel), "PrisonerRansomValue")]
    class PrisonerRansomValuePatch
    {
        private static void Postfix(CharacterObject prisoner, Hero sellerHero, ref int __result)
        {
            if (Factory.Settings.PrisonerPriceTweaksEnabled)
            {
                float tmp = __result * Factory.Settings.PrisonerPriceMultiplier;
                __result = (int)tmp;
            }
        }

        static bool Prepare() => Factory.Settings is { } settings && settings.PrisonerPriceTweaksEnabled;
    }

}
