using KaosesTweaks.Utils;
using StoryMode.StoryModePhases;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
/*
 1.5.7.2 - Disable until we understand main quest changes.
 */
namespace KaosesTweaks.Behaviors
{
    class BTConspiracyQuestTimerTweak
    {
        public static void Apply(Campaign campaign)
        {
            BTConspiracyQuestTimerTweak? obj = new BTConspiracyQuestTimerTweak();
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
                        IM.ColorGreenMessage("Extending Stop the Conspiracy quest by 1 year.");
                        questBase.ChangeQuestDueTime(CampaignTime.YearsFromNow(1f));
                        IM.ColorGreenMessage("New quest deadline: " + questBase.QuestDueTime.ToString());
                    }
                    bool flag3 = questBase.StringId.StartsWith("conspiracy_quest_") && questBase.QuestDueTime < CampaignTime.DaysFromNow(7f);
                    if (flag3)
                    {
                        questBase.ChangeQuestDueTime(CampaignTime.WeeksFromNow(3f));
                        IM.ColorGreenMessage("BT Extend Conspiracy Tweak: Extended conspiracy quest.");
                        float cStrngth = SecondPhase.Instance.ConspiracyStrength;
                        if (cStrngth > 1000 && cStrngth > 250)
                        {
                            SecondPhase.Instance.DecreaseConspiracyStrength(150);
                            IM.ColorGreenMessage("BT Extend Conspiracy Tweak: Reduced conspiracy strength.");
                        }

                    }
                }
            }
        }
    }
}
