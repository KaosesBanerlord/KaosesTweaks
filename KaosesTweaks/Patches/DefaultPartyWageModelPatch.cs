using HarmonyLib;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Localization;
using System;
using KaosesTweaks.Utils;

namespace KaosesTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultPartyWageModel), "GetTotalWage")]
    public class DefaultPartyWageModelPatch
    {

        public static void Postfix(ref ExplainedNumber __result, MobileParty mobileParty, bool includeDescriptions = false)
        {

            try
            {
                if (MCMSettings.Instance is { } settings && settings.PartyWageTweaksEnabled && mobileParty != null)
                {
                    float orig_result = __result.ResultNumber;
                    if (!mobileParty.IsGarrison && (mobileParty.IsMainParty
                        || (mobileParty.Party.MapFaction == Hero.MainHero.MapFaction && settings.ApplyWageTweakToFaction)
                        || settings.ApplyWageTweakToAI))
                    {
                        float num = settings.PartyWagePercent;
                        num = orig_result * num - orig_result;
                        __result.Add(num, new TextObject("BT Party Wage Tweak", null));//
                    }
                    if (mobileParty.IsGarrison && (mobileParty.CurrentSettlement.OwnerClan == Clan.PlayerClan ||
                        (mobileParty.Party.MapFaction == Hero.MainHero.MapFaction && settings.ApplyWageTweakToFaction)
                        || settings.ApplyWageTweakToAI))
                    {
                        float num2 = settings.GarrisonWagePercent;
                        num2 = orig_result * num2 - orig_result;
                        __result.Add(num2, new TextObject("BT Garrison Wage Tweak", null));//
                    }
                }

                if (MCMSettings.Instance is { } settings2 && settings2.BalancingWagesTweaksEnabled &&
                    settings2.KingdomBalanceStrengthEnabled && mobileParty != null &&
                    mobileParty.LeaderHero != null && mobileParty.LeaderHero.Clan.Kingdom != null)
                {
                    float num = 0f;
                    if (settings2.KingdomBalanceStrengthVanEnabled)
                    {
                        num = mobileParty.LeaderHero.Clan.Kingdom.StringId switch
                        {
                            "vlandia" => settings2.VlandiaBoost,
                            "battania" => settings2.BattaniaBoost,
                            "empire" => settings2.Empire_N_Boost,
                            "empire_s" => settings2.Empire_S_Boost,
                            "empire_w" => settings2.Empire_W_Boost,
                            "sturgia" => settings2.SturgiaBoost,
                            "khuzait" => settings2.KhuzaitBoost,
                            "aserai" => settings2.AseraiBoost,
                            _ => 0f
                        };
                    }
                    if (settings2.KingdomBalanceStrengthCEKEnabled)
                    {
                        num = mobileParty.LeaderHero.Clan.Kingdom.StringId switch
                        {
                            "nordlings" => settings2.NordlingsBoost,
                            "vagir" => settings2.VagirBoost,
                            "royalist_vlandia" => settings2.RoyalistVlandiaBoost,
                            "apolssaly" => settings2.ApolssalyBoost,
                            "lyrion" => settings2.LyrionBoost,
                            "rebel_khuzait" => settings2.RebelKhuzaitBoost,
                            "paleician" => settings2.PaleicianBoost,
                            "ariorum" => settings2.AriorumBoost,
                            "vlandia" => settings2.Vlandia_CEK_Boost,
                            "battania" => settings2.Battania_CEK_Boost,
                            "empire" => settings2.Empire_CEK_Boost,
                            "empire_s" => settings2.Empire_S_CEK_Boost,
                            "empire_w" => settings2.Empire_W_CEK_Boost,
                            "sturgia" => settings2.Sturgia_CEK_Boost,
                            "khuzait" => settings2.Khuzait_CEK_Boost,
                            "aserai" => settings2.Aserai_CEK_Boost,
                            _ => 0f
                        };
                    }
                    if (num == 0f && mobileParty.LeaderHero.Clan.Kingdom.Leader == Hero.MainHero) num = (settings2.KingdomBalanceStrengthCEKEnabled) ? settings2.Player_CEK_Boost : settings2.PlayerBoost;
                    num = __result.ResultNumber * -num;
                    __result.Add(num, new TextObject("BT Balancing Tweak", null));
                }
            }
            catch (Exception ex)
            {
                IM.ShowError("GetWagePostFix", "Exception GEtWage", ex);
            }
        }

        public static bool Prepare() => MCMSettings.Instance is { } settings && ((settings.PartyWageTweaksEnabled && settings.PartyWageTweaksHarmonyEnabled) || (settings.KingdomBalanceStrengthEnabled && settings.KingdomBalanceStrengthHarmonyEnabled));



    }







}
