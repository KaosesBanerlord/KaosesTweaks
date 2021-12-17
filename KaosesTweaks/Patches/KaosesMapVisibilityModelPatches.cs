using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map;

namespace KaosesTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultMapVisibilityModel), "GetPartySpottingRange")]
    class GetPartySpottingRangePatch
    {
        private static void Postfix(MobileParty party, bool includeDescriptions, ref ExplainedNumber __result)
        {
            /*
                        Logging.Lm($"GetPartySpottingRange Postfix Called \n__result.ResultNumber: {__result.ResultNumber}");
                           float existingView = __result.ResultNumber;
                        existingView *= Statics._settings.MobilePartyViewDistanceMultiplier;
                        __result.Add(existingView - __result.ResultNumber);
                        Logging.Lm(
                            $"\nStatics._settings.MobilePartyViewDistanceMultiplier: {Statics._settings.MobilePartyViewDistanceMultiplier}" +
                            $"existingView: {existingView}\n" +
                            $"existingView - __result.ResultNumber: {existingView - __result.ResultNumber}\n" +
                            $"__result.ResultNumber: {__result.ResultNumber}\n" 
                            );*/
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.MobilePartyViewDistanceEnabled;
    }

}
