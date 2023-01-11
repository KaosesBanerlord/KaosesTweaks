using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using TaleWorlds.CampaignSystem.GameComponents;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
    class DefaultTournamentModelPatch
    {
        static bool Prefix(ref int __result)
        {
            if (!(MCMSettings.Instance is null))
            {
                __result = MCMSettings.Instance.TournamentRenownAmount;
                if (Statics._settings.TournamentDebug)
                {
                    IM.MessageDebug("Patches TournamentRenownAmount Tweak: " + MCMSettings.Instance.TournamentRenownAmount.ToString());
                }
                return false;
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.TournamentRenownIncreaseEnabled;
    }
}
