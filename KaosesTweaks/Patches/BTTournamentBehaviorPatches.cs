using HarmonyLib;
using KaosesTweaks.Settings;
using SandBox.TournamentMissions.Missions;
using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace KaosesTweaks.Patches
{

    [HarmonyPatch(typeof(TournamentBehavior), "get_Winner")]
    public class OnPlayerWinTournamentPatch
    {
        private static PropertyInfo? overallExpectedDenars = null;

        static void Prefix(TournamentBehavior __instance)
        {
            if (MCMSettings.Instance is { } settings)
            {
                overallExpectedDenars?.SetValue(__instance, __instance.OverallExpectedDenars + settings.TournamentGoldRewardAmount);
            }
        }

        static bool Prepare()
        {
            if (MCMSettings.Instance is { } settings && settings.TournamentGoldRewardEnabled)
            {
                overallExpectedDenars = typeof(TournamentBehavior).GetProperty("OverallExpectedDenars", BindingFlags.Public | BindingFlags.Instance);
                return true;
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(TournamentBehavior), "GetExpectedDenarsForBet")]
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

    [HarmonyPatch(typeof(TournamentBehavior), "GetMaximumBet")]
    public class GetMaximimBetPatch
    {
        static void Postfix(ref int __result)
        {
            if (MCMSettings.Instance is { } settings)
            {
                int num = settings.TournamentMaxBetAmount;

                if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
                {
                    num *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
                }

                __result = Math.Min(num, Hero.MainHero.Gold);
            }
        }

        static bool Prepare()
        {
            return MCMSettings.Instance is { } settings && settings.TournamentMaxBetAmountTweakEnabled;
        }
    }
}
