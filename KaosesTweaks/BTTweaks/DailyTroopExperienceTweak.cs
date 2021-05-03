using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace KaosesTweaks.BTTweaks
{
    class DailyTroopExperienceTweak
    {
        public static void Apply(Campaign campaign)
        {
            var obj = new DailyTroopExperienceTweak();
            CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(obj, (MobileParty mp) => { obj.DailyTick(mp); });
        }

        private void DailyTick(MobileParty party)
        {
            if (party.LeaderHero != null)
            {
                int count = party.MemberRoster.TotalManCount;
                if (party.LeaderHero == Hero.MainHero || MCMSettings.Instance is { } settings && settings.DailyTroopExperienceApplyToAllNPC 
                    || MCMSettings.Instance is { } settings2 && settings2.DailyTroopExperienceApplyToPlayerClanMembers && party.LeaderHero.Clan == Clan.PlayerClan)
                {
                    int experienceAmount = ExperienceAmount(party.LeaderHero);
                    if (experienceAmount > 0)
                    {
                        int num = 0;
                        foreach (var troop in party.MemberRoster.GetTroopRoster())
                        {
                            if (!troop.Character.IsHero)
                            {
                                party.MemberRoster.AddXpToTroop(experienceAmount, troop.Character);
                                num++;
                            }
                        }

                        if (MCMSettings.Instance is { } settings3 && settings3.DisplayMessageDailyExperienceGain)
                        {
                            string troops = count == 1 ? "troop roster (stack)" : "troop rosters (stacks)";
                            if (party.LeaderHero == Hero.MainHero && num > 0)
                                InformationManager.DisplayMessage(new InformationMessage($"Granted {experienceAmount} experience to {num} {troops}."));
                        }
                    }
                }
            }
        }

        private static int ExperienceAmount(Hero h)
        {
            int leadership = h.GetSkillValue(DefaultSkills.Leadership);
            if (MCMSettings.Instance != null)
            {
                if (leadership >= MCMSettings.Instance.DailyTroopExperienceRequiredLeadershipLevel)
                    return (int)(MCMSettings.Instance.LeadershipPercentageForDailyExperienceGain * leadership);
            }
            return 0;
        }
    }
}
