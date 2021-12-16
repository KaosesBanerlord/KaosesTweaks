using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyMemberSizeLimit")]
    public class DefaultPartySizeLimitModelPatch
    {
        static void Postfix(MobileParty party, ref ExplainedNumber __result)
        {
            if (!(party is null) && !(party.LeaderHero is null) && MCMSettings.Instance is { } settings)
            {
                float num;
                if (settings.LeadershipPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Leadership) * settings.LeadershipPartySizeBonus * ((party.LeaderHero == Hero.MainHero) ? 1 : settings.PartySizeTweakAIFactor));

                    if (settings.PartySizeLimitsDebug)
                    {
                        IM.MessageDebug("BT Leadership PartySizeBonus : " + num.ToString());
                    }
                    __result.Add((float)num, new TextObject("BT Leadership bonus"));
                }

                if (settings.StewardPartySizeBonusEnabled && party.LeaderHero == Hero.MainHero)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Steward) * settings.StewardPartySizeBonus * ((party.LeaderHero == Hero.MainHero) ? 1 : settings.PartySizeTweakAIFactor));
                    if (settings.PartySizeLimitsDebug)
                    {
                        IM.MessageDebug("BT Steward PartySizeBonus : " + num.ToString());
                    }
                    __result.Add((float)num, new TextObject("BT Steward bonus"));
                }
                if (settings.BalancingPartySizeTweaksEnabled && settings.KingdomBalanceStrengthEnabled && party.LeaderHero.Clan.Kingdom != null)
                {
                    float num2 = 0f;
                    if (settings.KingdomBalanceStrengthVanEnabled)
                    {
                        num2 = party.LeaderHero.Clan.Kingdom.StringId switch
                        {
                            "vlandia" => settings.VlandiaBoost,
                            "battania" => settings.BattaniaBoost,
                            "empire" => settings.Empire_N_Boost,
                            "empire_s" => settings.Empire_S_Boost,
                            "empire_w" => settings.Empire_W_Boost,
                            "sturgia" => settings.SturgiaBoost,
                            "khuzait" => settings.KhuzaitBoost,
                            "aserai" => settings.AseraiBoost,
                            _ => 0f
                        };
                    }

                    if (settings.KingdomBalanceStrengthCEKEnabled)
                    {
                        num2 = party.LeaderHero.Clan.Kingdom.StringId switch
                        {
                            "nordlings" => settings.NordlingsBoost,
                            "vagir" => settings.VagirBoost,
                            "royalist_vlandia" => settings.RoyalistVlandiaBoost,
                            "apolssaly" => settings.ApolssalyBoost,
                            "lyrion" => settings.LyrionBoost,
                            "rebel_khuzait" => settings.RebelKhuzaitBoost,
                            "paleician" => settings.PaleicianBoost,
                            "ariorum" => settings.AriorumBoost,
                            "vlandia" => settings.Vlandia_CEK_Boost,
                            "battania" => settings.Battania_CEK_Boost,
                            "empire" => settings.Empire_CEK_Boost,
                            "empire_s" => settings.Empire_S_CEK_Boost,
                            "empire_w" => settings.Empire_W_CEK_Boost,
                            "sturgia" => settings.Sturgia_CEK_Boost,
                            "khuzait" => settings.Khuzait_CEK_Boost,
                            "aserai" => settings.Aserai_CEK_Boost,
                            _ => 0f
                        };
                    }

                    if (num2 == 0f && party.LeaderHero.Clan.Kingdom.Leader == Hero.MainHero) num2 = (settings.KingdomBalanceStrengthCEKEnabled) ? settings.Player_CEK_Boost : settings.PlayerBoost;
                    if (settings.PartySizeLimitsDebug)
                    {
                        IM.MessageDebug("BT Balancing Tweak: " + num2.ToString());
                    }
                    __result.Add((float)__result.ResultNumber * num2, new TextObject("BT Balancing Tweak"));
                }
            }

            if (party != null && party.IsCaravan && party.Party?.Owner != null && party.Party.Owner == Hero.MainHero && MCMSettings.Instance is { } settings2)
            {
                float num = settings2.PlayerCaravanPartySize;
                float num2 = __result.ResultNumber;
                float num3 = num - num2;
                if (MCMSettings.Instance is { } setting && setting.PartySizeLimitsDebug)
                {
                    IM.MessageDebug("Caravan PartySize Tweak: " + num3.ToString());
                }
                __result.Add((int)Math.Ceiling(num3), null);
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && (settings.PartySizeTweakEnabled || settings.KingdomBalanceStrengthEnabled || settings.PlayerCaravanPartySizeTweakEnabled);
    }

    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "GetPartyPrisonerSizeLimit")]
    public class DefaultPrisonerSizeLimitModelPatch
    {
        private static void Postfix(PartyBase party, ref ExplainedNumber __result)
        {
            if (party.LeaderHero != null)// && party.LeaderHero == Hero.MainHero
            {
                if (MCMSettings.Instance is { } settings && settings.PrisonerSizeTweakEnabled)
                {
                    double num = (int)Math.Ceiling(__result.ResultNumber * settings.PrisonerSizeTweakPercent);
                    if (settings.PrisonersDebug)
                    {
                        IM.MessageDebug("Prisoner SizeTweak: " + num.ToString() + "   Multiplier: " + settings.PrisonerSizeTweakPercent.ToString());
                        IM.MessageDebug("Prisoner __result: " + __result.ResultNumber.ToString() + "   num: " + num.ToString());
                    }

                    if ((settings.PrisonerSizeTweakAI && party.LeaderHero != Hero.MainHero) || party.LeaderHero == Hero.MainHero)
                    {
                        __result.Add((float)num, new TextObject("BT Prisoner Limit Bonus"));
                        if (settings.PrisonersDebug)
                        {
                            IM.MessageDebug("Prisoner result Final: " + __result.ResultNumber.ToString());
                        }
                    }
                }
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.PrisonerSizeTweakEnabled;
    }
}
