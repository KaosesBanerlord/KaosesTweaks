using KaosesCommon.Utils;
using StoryMode.StoryModePhases;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
/*
 1.5.7.2 - Disable until we understand main quest changes.
 */
namespace KaosesTweaks.Behaviors
{

    class ConspiracyQuestTimerTweak
    {
        public static void Apply(Campaign campaign)
        {
            ConspiracyQuestTimerTweak? obj = new ConspiracyQuestTimerTweak();
            CampaignEvents.DailyTickEvent.AddNonSerializedListener(obj, new Action(obj.ExtendDeadline));
        }

        private void ExtendDeadline()
        {
            if (Campaign.Current != null && Campaign.Current.QuestManager != null)
            {
                foreach (QuestBase questBase in Campaign.Current.QuestManager.Quests)
                {
                    bool flag2 = questBase.GetName().ToString().StartsWith("stop_conspiracy_") && questBase.QuestDueTime < CampaignTime.DaysFromNow(5f);
                    if (flag2)
                    {
                        IM.MessageGreen("Extending Stop the Conspiracy quest by 1 year.");
                        questBase.ChangeQuestDueTime(CampaignTime.YearsFromNow(1f));
                        IM.MessageGreen("New quest deadline: " + questBase.QuestDueTime.ToString());
                    }
                    bool flag3 = questBase.StringId.StartsWith("conspiracy_quest_") && questBase.QuestDueTime < CampaignTime.DaysFromNow(7f);
                    if (flag3)
                    {
                        questBase.ChangeQuestDueTime(CampaignTime.WeeksFromNow(3f));
                        IM.MessageGreen("BT Extend Conspiracy Tweak: Extended conspiracy quest.");
                        float cStrngth = SecondPhase.Instance.ConspiracyStrength;
                        if (cStrngth > 1000 && cStrngth > 250)
                        {
                            SecondPhase.Instance.DecreaseConspiracyStrength(150);
                            IM.MessageGreen("BT Extend Conspiracy Tweak: Reduced conspiracy strength.");
                        }

                    }
                }
            }
        }
    }
}
