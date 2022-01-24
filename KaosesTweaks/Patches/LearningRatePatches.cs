using System;
using HarmonyLib;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Localization;

namespace KaosesTweaks.Patches
{
  [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningRate", new Type[]{typeof(int), typeof(int), typeof(int), typeof(int), typeof(TextObject), typeof(bool)})]
  public class LearningRatePatches
  {
    static void Postfix(ref ExplainedNumber __result)
    {
      __result.LimitMin(MCMSettings.Instance.MinimumLearningRate);
    }

    static bool Prepare() => MCMSettings.Instance is { } settings && settings.MinimumLearningRate != 0.0f;
  }
}
