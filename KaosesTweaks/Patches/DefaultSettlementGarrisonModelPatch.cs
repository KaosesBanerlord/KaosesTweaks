using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;

namespace KaosesTweaks.Patches
{

    [HarmonyPatch]
    class DefaultSettlementGarrisonModelPatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(AccessTools.TypeByName("DefaultSettlementGarrisonModel"), "FindNumberOfTroopsToLeaveToGarrison", new Type[]
            {
                typeof(MobileParty),
                typeof(Settlement)
            }, null);
        }

        private static void Postfix(MobileParty mobileParty, Settlement settlement, ref int __result)
        {
            if (settlement == null || mobileParty == null) return;

            if (MCMSettings.Instance is { } settings && mobileParty.LeaderHero.Clan == Clan.PlayerClan)
            {
                bool DisableDonationClan = settlement.OwnerClan == Clan.PlayerClan && settings.DisableTroopDonationPatchEnabled;
                bool DisableForAnySettlement = settings.DisableTroopDonationAnyEnabled;

                if (DisableDonationClan || DisableForAnySettlement)
                {
                    if (Statics._settings.SettlementsDebug)
                    {
                        IM.MessageDebug("FindNumberOfTroopsToLeaveToGarrison: IS DISABLED");
                    }
                    __result = 0;
                }
            }
        }
        static bool Prepare() => MCMSettings.Instance is { } settings && settings.DisableTroopDonationPatchEnabled;
    }
}
