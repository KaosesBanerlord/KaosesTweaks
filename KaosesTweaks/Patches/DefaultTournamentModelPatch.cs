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
            if (!(KTSettings.Instance is null))
            {
                __result = KTSettings.Instance.TournamentRenownAmount;
                if (Statics._settings.TournamentDebug)
                {
                    IM.MessageDebug("Patches TournamentRenownAmount Tweak: " + KTSettings.Instance.TournamentRenownAmount.ToString());
                }
                return false;
            }
            return true;
        }

        static bool Prepare() => KTSettings.Instance is { } settings && settings.TournamentRenownIncreaseEnabled;
    }
}
