using HarmonyLib;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace KaosesTweaks.Patches
{
  [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningLimit")]
  public class LearningRatePatches
  {
    static void Postfix(ref ExplainedNumber __result)
    {
      __result.LimitMin(MCMSettings.Instance.MinimumLearningRate);
    }

    static bool Prepare() => MCMSettings.Instance is { } settings && settings.MinimumLearningRate != 0.0f;
  }
}
