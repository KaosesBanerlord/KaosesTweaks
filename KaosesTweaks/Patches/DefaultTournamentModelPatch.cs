using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesCommon.Utils;
using TaleWorlds.CampaignSystem.GameComponents;
using KaosesTweaks.Objects;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
    class DefaultTournamentModelPatch
    {
        static bool Prefix(ref int __result)
        {
            if (!(Factory.Settings is null))
            {
                __result = Factory.Settings.TournamentRenownAmount;
                if (Factory.Settings.TournamentDebug)
                {
                    IM.MessageDebug("Patches TournamentRenownAmount Tweak: " + Factory.Settings.TournamentRenownAmount.ToString());
                }
                return false;
            }
            return true;
        }

        static bool Prepare() => Factory.Settings is { } settings && settings.TournamentRenownIncreaseEnabled;
    }
}
