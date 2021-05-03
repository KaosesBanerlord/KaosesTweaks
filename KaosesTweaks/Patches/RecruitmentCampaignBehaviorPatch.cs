using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using System.Linq;
using TaleWorlds.Core;
using Helpers;
using KaosesTweaks.Settings;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(RecruitmentCampaignBehavior), "UpdateVolunteersOfNotables")]
    class RecruitmentCampaignBehaviorPatch
    {
        static void Postfix(bool initialRunning)
        {
            if (MCMSettings.Instance is { } settings && settings.BalancingUpgradeTroopsTweaksEnabled)
            {
                foreach (Settlement settlement in from settlement in Campaign.Current.Settlements where settlement.OwnerClan != null && settlement.OwnerClan.Kingdom != null && ((settlement.IsTown && !settlement.Town.InRebelliousState) || (settlement.IsVillage && !settlement.Village.Bound.Town.InRebelliousState)) select settlement)
                {
                    float num = 0f;
                    if (settings.KingdomBalanceStrengthVanEnabled)
                    {
                        num = settlement.OwnerClan.Kingdom.StringId switch
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
                        num = settlement.OwnerClan.Kingdom.StringId switch
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
                    if (num == 0f && settlement.OwnerClan.Kingdom.Leader == Hero.MainHero) num = (settings.KingdomBalanceStrengthCEKEnabled) ? settings.Player_CEK_Boost : settings.PlayerBoost;

                    foreach (Hero hero in settlement.Notables)
                    {
                        if (hero.CanHaveRecruits)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                if (hero.VolunteerTypes[i] != null && MBRandom.RandomFloat < (num * 0.5) && hero.VolunteerTypes[i].UpgradeTargets != null && hero.VolunteerTypes[i].Level < 20)
                                {
                                    CultureObject cultureObject = (hero.CurrentSettlement != null) ? hero.CurrentSettlement.Culture : hero.Clan.Culture;
                                    CharacterObject basicTroop = cultureObject.BasicTroop;
                                    if (hero.VolunteerTypes[i] == basicTroop && HeroHelper.HeroShouldGiveEliteTroop(hero))
                                    {
                                        hero.VolunteerTypes[i] = cultureObject.EliteBasicTroop;
                                    }
                                    else
                                    {
                                        hero.VolunteerTypes[i] = hero.VolunteerTypes[i].UpgradeTargets[MBRandom.RandomInt(hero.VolunteerTypes[i].UpgradeTargets.Length)];
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }
        static bool Prepare() => MCMSettings.Instance is { } settings && settings.KingdomBalanceStrengthEnabled;
    }
}
