using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPrisonerRecruitmentCalculationModel), "GetConformityChangePerHour")]
    class BTPrisonerRecruitmentCalculationModelPatch
    {
        static void Postfix(PartyBase party, CharacterObject troopToBoost, ref int __result)
        {
            if (MCMSettings.Instance is { } settings && settings.PrisonerConformityTweaksEnabled && !(party.LeaderHero is null))
            {
                float num;
                if (party.LeaderHero == Hero.MainHero ||
                  (!(party.Owner is null) && party.Owner.Clan == Hero.MainHero.Clan && settings.PrisonerConformityTweaksApplyToClan) ||
                  settings.PrisonerConformityTweaksApplyToAi)
                {
                    if (Statics._settings.PrisonersDebug)
                    {
                        IM.MessageDebug("Prisoner ConformityTweak: original: " + __result.ToString() + "   Multiplier: " + (1 + settings.PrisonerConformityTweakBonus).ToString());
                    }
                    num = __result * (1 + settings.PrisonerConformityTweakBonus);
                    if (Statics._settings.PrisonersDebug)
                    {
                        IM.MessageDebug("Prisoner num Final: " + num.ToString());
                    }
                    party.MobileParty.EffectiveQuartermaster.AddSkillXp(DefaultSkills.Charm, num * .05f);
                    __result = MathF.Round(num);
                }
            }

            // Add Tier-Specific Boosts?
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.PrisonerConformityTweaksEnabled;
    }
}
