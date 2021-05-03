using HarmonyLib;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;

namespace KaosesTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultVillageProductionCalculatorModel), "CalculateDailyFoodProductionAmount")]
    class BTCalculateDailyFoodProductionAmountPatch
    {
        static void Postfix(Village village, ref float __result)
        {
            if (village != null && MCMSettings.Instance is { } settings && settings.ProductionTweakEnabled)
            {
                __result = (__result * settings.ProductionFoodTweakEnabled);
            }
            if (village != null && MCMSettings.Instance is { } settings2 && settings2.BalancingFoodTweakEnabled && settings2.KingdomBalanceStrengthEnabled && village.Settlement.OwnerClan.Kingdom != null)
            {
                float num = 0f;
                if (settings2.KingdomBalanceStrengthVanEnabled)
                {
                    num = village.Settlement.OwnerClan.Kingdom.StringId switch
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
                    num = village.Settlement.OwnerClan.Kingdom.StringId switch
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
                if (num == 0f && village.Settlement.OwnerClan.Kingdom.Leader == Hero.MainHero) num = (settings2.KingdomBalanceStrengthCEKEnabled) ? settings2.Player_CEK_Boost : settings2.PlayerBoost;
                __result += (__result * num);
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && (settings.ProductionTweakEnabled || settings.KingdomBalanceStrengthEnabled);
    }

    [HarmonyPatch(typeof(DefaultVillageProductionCalculatorModel), "CalculateDailyProductionAmount")]
    public class CalculateDailyProductionAmountPatch
    {
        static void Postfix(Village village, ItemObject item, ref float __result)
        {
            if ((MCMSettings.Instance is { } settings && settings.ProductionTweakEnabled))
            {
                __result *= settings.ProductionOtherTweakEnabled;
            }
        }
        static bool Prepare() => MCMSettings.Instance is { } settings && settings.ProductionTweakEnabled;
    }
}
