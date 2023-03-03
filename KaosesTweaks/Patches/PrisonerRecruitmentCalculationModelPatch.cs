using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesCommon.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using KaosesTweaks.Objects;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPrisonerRecruitmentCalculationModel), "GetConformityChangePerHour")]
    class PrisonerRecruitmentCalculationModelPatch
    {
        static void Postfix(PartyBase party, CharacterObject troopToBoost, ref int __result)
        {
            if (Factory.Settings is { } settings && settings.PrisonerConformityTweaksEnabled && !(party.LeaderHero is null))
            {
                float num;
                if (party.LeaderHero == Hero.MainHero ||
                  (!(party.Owner is null) && party.Owner.Clan == Hero.MainHero.Clan && settings.PrisonerConformityTweaksApplyToClan) ||
                  settings.PrisonerConformityTweaksApplyToAi)
                {
                    if (Factory.Settings.IsPrisonersDebug)
                    {
                        IM.MessageDebug("Prisoner ConformityTweak: original: " + __result.ToString() + "   Multiplier: " + (1 + settings.PrisonerConformityTweakBonus).ToString());
                    }
                    num = __result * (1 + settings.PrisonerConformityTweakBonus);
                    if (Factory.Settings.IsPrisonersDebug)
                    {
                        IM.MessageDebug("Prisoner num Final: " + num.ToString());
                    }
                    party.MobileParty.EffectiveQuartermaster.AddSkillXp(DefaultSkills.Charm, num * .05f);
                    __result = MathF.Round(num);
                }
            }

            // Add Tier-Specific Boosts?
        }

        static bool Prepare() => Factory.Settings is { } settings && settings.PrisonerConformityTweaksEnabled;
    }
}
