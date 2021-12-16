using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
    class DefaultTournamentModelPatch
    {
        static bool Prefix(ref int __result)
        {
            if (MCMSettings.Instance is { } settings)
            {
                __result = settings.TournamentRenownAmount;

                if (settings.TournamentDebug)
                {
                    IM.MessageDebug("Patches TournamentRenownAmount Tweak: " + settings.TournamentRenownAmount.ToString());
                }
                return false;
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.TournamentRenownIncreaseEnabled;
    }
}
