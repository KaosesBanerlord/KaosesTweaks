using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using SandBox.TournamentMissions.Missions;
using SandBox.ViewModelCollection;
using SandBox.ViewModelCollection.Tournament;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;


namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(TournamentVM), "RefreshBetProperties")]
    public class RefreshBetPropertiesPatch
    {
        private static FieldInfo? bettedAmountFieldInfo = null;

        static void Postfix(TournamentVM __instance)
        {
            if (!(MCMSettings.Instance is { } settings)) return;

            if (bettedAmountFieldInfo == null) GetFieldInfo();
            int thisRoundBettedAmount = !(bettedAmountFieldInfo is null) ? (int)bettedAmountFieldInfo.GetValue(__instance) : 0;
            int num = settings.TournamentMaxBetAmount;
            if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
            {
                num *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
            }
            __instance.MaximumBetValue = Math.Min(num - thisRoundBettedAmount, Hero.MainHero.Gold);
        }

        static bool Prepare()
        {
            if (MCMSettings.Instance is { } settings && settings.TournamentMaxBetAmountTweakEnabled)
            {
                GetFieldInfo();
                return true;
            }
            return false;
        }

        private static void GetFieldInfo()
        {
            bettedAmountFieldInfo = typeof(TournamentVM).GetField("_thisRoundBettedAmount", BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }


    [HarmonyPatch(typeof(TournamentVM), "RefreshValues")]
    public class RefreshValuesPatch
    {
        static void Postfix(TournamentVM __instance)
        {
            int num = MCMSettings.Instance is { } settings ? settings.TournamentMaxBetAmount : __instance.MaximumBetValue;
            if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
            {
                num *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
            }
            GameTexts.SetVariable("MAX_AMOUNT", num);
            __instance.BetDescriptionText = GameTexts.FindText("str_tournament_bet_description").ToString();
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.TournamentMaxBetAmountTweakEnabled;
    }


    [HarmonyPatch(typeof(TournamentVM), "get_IsBetButtonEnabled")]
    public class IsBetButtonEnabledPatch
    {
        private static FieldInfo? bettedAmountFieldInfo = null;

        static bool Prefix(TournamentVM __instance, ref bool __result)
        {
            bool failed = false;
            try
            {
                if (bettedAmountFieldInfo == null) GetFieldInfo();
                bool result = false;
                if (__instance.IsTournamentIncomplete)
                {
                    int thisRoundBettedAmount = !(bettedAmountFieldInfo is null) ? (int)bettedAmountFieldInfo.GetValue(__instance) : 0;
                    bool flag = __instance.Tournament.CurrentMatch.Participants.Any((TournamentParticipant x) => x.Character == CharacterObject.PlayerCharacter);
                    int num = MCMSettings.Instance is { } settings ? settings.TournamentMaxBetAmount : __instance.MaximumBetValue;
                    if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
                    {
                        num *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
                    }
                    if (flag && thisRoundBettedAmount < num)
                        result = Hero.MainHero.Gold > 0;
                }
                __result = result;
            }
            catch (Exception ex)
            {
                failed = true;
                MessageBox.Show($"An error occurred while trying to get IsBetButtonEnabled. Reverting to original...\n\n{ex.ToStringFull()}");
            }
            return failed;
        }

        static bool Prepare()
        {
            if (MCMSettings.Instance is { } settings && settings.TournamentMaxBetAmountTweakEnabled)
            {
                GetFieldInfo();
                return true;
            }
            return false;
        }

        private static void GetFieldInfo()
        {
            bettedAmountFieldInfo = typeof(TournamentVM).GetField("_thisRoundBettedAmount", BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}
