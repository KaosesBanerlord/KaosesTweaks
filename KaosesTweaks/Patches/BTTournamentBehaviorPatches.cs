using HarmonyLib;
using KaosesTweaks.Settings;
using SandBox.TournamentMissions.Missions;
using System.Reflection;
using TaleWorlds.Library;

namespace KaosesTweaks.Patches
{

    [HarmonyPatch(typeof(TournamentBehavior), "OnPlayerWinTournament")]
    public class OnPlayerWinTournamentPatch
    {
        static bool Prefix(TournamentBehavior __instance)
        {
            if (MCMSettings.Instance is { } settings)
            {
                typeof(TournamentBehavior).GetProperty("OverallExpectedDenars").SetValue(__instance, __instance.OverallExpectedDenars + settings.TournamentGoldRewardAmount);
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.TournamentGoldRewardEnabled;
    }

    [HarmonyPatch(typeof(TournamentBehavior), "CalculateBet")]
    public class CalculateBetPatch
    {
        private static PropertyInfo? betOdd = null;

        static void Postfix(TournamentBehavior __instance)
        {
            if (MCMSettings.Instance is { } settings)
            {
                betOdd?.SetValue(__instance, MathF.Max((float)betOdd.GetValue(__instance), settings.MinimumBettingOdds, 0));
            }
        }

        static bool Prepare()
        {
            if (MCMSettings.Instance is { } settings && settings.MinimumBettingOddsTweakEnabled)
            {
                betOdd = typeof(TournamentBehavior).GetProperty(nameof(TournamentBehavior.BetOdd), BindingFlags.Public | BindingFlags.Instance);
                return true;
            }
            return false;
        }
    }
}
