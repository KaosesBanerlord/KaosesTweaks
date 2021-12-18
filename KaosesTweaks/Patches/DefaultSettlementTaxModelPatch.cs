using HarmonyLib;
using KaosesTweaks.Settings;
using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch]
    class DefaultSettlementTaxModelPatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(AccessTools.TypeByName("DefaultSettlementTaxModel"), "CalculateTownTax", new Type[]
            {
                typeof(Town),
                typeof(bool)
            }, null);
        }

        private static void Postfix(Town town, bool includeDescriptions, ref ExplainedNumber __result)
        {
            if (town == null) return;

            if (MCMSettings.Instance is { } settings && settings.BalancingTaxTweaksEnabled && town.Settlement.OwnerClan.Kingdom != null)
            {
                float num = 0f;
                if (settings.KingdomBalanceStrengthVanEnabled)
                {
                    num = town.Settlement.OwnerClan.Kingdom.StringId switch
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
                    num = town.Settlement.OwnerClan.Kingdom.StringId switch
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
                if (num == 0f && town.Settlement.OwnerClan.Kingdom.Leader == Hero.MainHero) num = settings.KingdomBalanceStrengthCEKEnabled ? settings.Player_CEK_Boost : settings.PlayerBoost;
                float prosperity = town.Prosperity;
                float num2 = 1f;
                if (town.Settlement.OwnerClan.Kingdom != null && town.Settlement.OwnerClan.Kingdom.ActivePolicies.Contains(DefaultPolicies.CouncilOfTheCommons))
                {
                    num2 -= 0.05f;
                }
                float num3 = 0.25f;
                float value = prosperity * num3 * num2 * num * 1.25f;
                __result.Add(value, new TextObject("BT Balancing Tax Tweak"));
            }
        }
        static bool Prepare() => MCMSettings.Instance is { } settings && settings.KingdomBalanceStrengthEnabled;
    }

    [HarmonyPatch]
    public class CalculateVillageTaxFromIncomePatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(AccessTools.TypeByName("DefaultSettlementTaxModel"), "CalculateVillageTaxFromIncome", new Type[]
            {
                typeof(Village),
                typeof(int)
            }, null);
        }

        private static void Postfix(Village village, int marketIncome, ref int __result)
        {
            if (MCMSettings.Instance is { } settings && settings.BalancingTaxTweaksEnabled && village.Settlement.OwnerClan.Kingdom != null)
            {
                float num = 0f;
                if (settings.KingdomBalanceStrengthVanEnabled)
                {
                    num = village.Settlement.OwnerClan.Kingdom.StringId switch
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
                    num = village.Settlement.OwnerClan.Kingdom.StringId switch
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
                if (num == 0f && village.Settlement.OwnerClan.Kingdom.Leader == Hero.MainHero) num = settings.KingdomBalanceStrengthCEKEnabled ? settings.Player_CEK_Boost : settings.PlayerBoost;
                //float oldresult = __result;
                //float newresult = oldresult * (1 + num);
                float newresult = __result * (1 + (num * 1.25f));
                //if (num > 0) DebugHelpers.DebugMessage("Tradeincome for " +village.Name+ " got a boost from " + oldresult + " to " + (int)newresult+"!");
                //if (num > 0) DebugHelpers.DebugMessage("Boost was " + num + "and Kingdom was " + village.Settlement.OwnerClan.Kingdom.StringId +" !");
                __result = (int)newresult;
            }
        }
        static bool Prepare() => MCMSettings.Instance is { } settings && settings.KingdomBalanceStrengthEnabled;
    }
}
