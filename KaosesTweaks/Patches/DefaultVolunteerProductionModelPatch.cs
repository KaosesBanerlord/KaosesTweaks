using HarmonyLib;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultVolunteerProductionModel), "GetDailyVolunteerProductionProbability")]
    class DefaultVolunteerProductionModelPatch
    {
        static void Postfix(Hero hero, int index, Settlement settlement, ref float __result)
        {
            if (MCMSettings.Instance is { } settings && settings.BalancingTimeRecruitsTweaksEnabled && hero.CurrentSettlement != null && hero.CurrentSettlement.OwnerClan.Kingdom != null)
            {
                float num = 0f;
                if (settings.KingdomBalanceStrengthVanEnabled)
                {
                    num = hero.CurrentSettlement.OwnerClan.Kingdom.StringId switch
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
                    num = hero.CurrentSettlement.OwnerClan.Kingdom.StringId switch
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
                if (num == 0f && hero.CurrentSettlement.OwnerClan.Kingdom.Leader == Hero.MainHero) num = settings.KingdomBalanceStrengthCEKEnabled ? settings.Player_CEK_Boost : settings.PlayerBoost;
                __result += num * 0.75f;
            }
        }
        static bool Prepare() => MCMSettings.Instance is { } settings && settings.KingdomBalanceStrengthEnabled;
    }
}
