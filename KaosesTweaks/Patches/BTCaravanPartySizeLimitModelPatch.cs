using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyMemberSizeLimit")]
    class BTCaravanPartySizeLimitModelPatch
    {
        static void Postfix(MobileParty party, ref ExplainedNumber __result)
        {
            if (party.IsCaravan && party.Party?.Owner != null && party.Party.Owner == Hero.MainHero && MCMSettings.Instance is { } settings)
            {
                float num = settings.PlayerCaravanPartySize;
                float num2 = __result.ResultNumber;
                float num3 = num - num2;
                IM.MessageDebug("Caravan PartySize Tweak: "+ num3.ToString());
                __result.Add((int)Math.Ceiling(num3), null);
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.PlayerCaravanPartySizeTweakEnabled;
    }
}
