using KaosesTweaks.Settings;
using KaosesCommon.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Library;
using System.Runtime;
using KaosesTweaks.Objects;

namespace KaosesTweaks.Tweaks
{

    class DailyTroopExperienceTweak
    {
        public static void Apply(Campaign campaign)
        {
            DailyTroopExperienceTweak? obj = new DailyTroopExperienceTweak();
            CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(obj, (MobileParty mp) => { obj.DailyTick(mp); });
        }

        private void DailyTick(MobileParty party)
        {
            if (party.LeaderHero != null)
            {
                int count = party.MemberRoster.TotalManCount;
                if (party.LeaderHero == Hero.MainHero || Factory.Settings is { } settings && settings.DailyTroopExperienceApplyToAllNPC
                    || Factory.Settings is { } settings2 && settings2.DailyTroopExperienceApplyToPlayerClanMembers && party.LeaderHero.Clan == Clan.PlayerClan)
                {
                    int experienceAmount = ExperienceAmount(party.LeaderHero);
                    if (experienceAmount > 0)
                    {
                        int num = 0;
                        foreach (TroopRosterElement troop in party.MemberRoster.GetTroopRoster())
                        {
                            if (!troop.Character.IsHero)
                            {
                                party.MemberRoster.AddXpToTroop(experienceAmount, troop.Character);
                                num++;
                            }
                        }

                        if (Factory.Settings is { } settings3 && settings3.DisplayMessageDailyExperienceGain)
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
            if (Factory.Settings != null)
            {
                if (Factory.Settings.XpModifiersDebug)
                {
                    IM.MessageDebug("leadership: " + leadership.ToString() + " RequiredLeadershipLevel: " + Factory.Settings.DailyTroopExperienceRequiredLeadershipLevel.ToString());
                }
                if (leadership >= Factory.Settings.DailyTroopExperienceRequiredLeadershipLevel)
                {
                    if (Factory.Settings.XpModifiersDebug)
                    {
                        IM.MessageDebug("DailyExperienceGain : " + (Factory.Settings.LeadershipPercentageForDailyExperienceGain * leadership).ToString());
                    }
                    return (int)(Factory.Settings.LeadershipPercentageForDailyExperienceGain * leadership);
                }
            }
            return 0;
        }
    }
}
