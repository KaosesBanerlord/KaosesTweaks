using HarmonyLib;
using KaosesTweaks.Objects;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;

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
                        existingView *= Factory.Settings.MobilePartyViewDistanceMultiplier;
                        __result.Add(existingView - __result.ResultNumber);
                        Logging.Lm(
                            $"\nFactory.Settings.MobilePartyViewDistanceMultiplier: {Factory.Settings.MobilePartyViewDistanceMultiplier}" +
                            $"existingView: {existingView}\n" +
                            $"existingView - __result.ResultNumber: {existingView - __result.ResultNumber}\n" +
                            $"__result.ResultNumber: {__result.ResultNumber}\n" 
                            );*/
        }

        static bool Prepare() => Factory.Settings is { } settings && settings.MobilePartyViewDistanceEnabled;
    }

}
