using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;

namespace KaosesTweaks.Behaviors
{
    public class KaosesWorkshopsBehavior : WorkshopsCampaignBehavior
    {

        // Token: 0x060034EF RID: 13551 RVA: 0x000F3394 File Offset: 0x000F1594
        public override void RegisterEvents()
        {

        }

        // Token: 0x06003510 RID: 13584 RVA: 0x000F48E8 File Offset: 0x000F2AE8
        public static void ProduceOutput(ItemObject outputItem, Town town, Workshop workshop, int count, bool doNotEffectCapital)
        {
            int itemPrice = town.GetItemPrice(outputItem, null, false);
            town.Owner.ItemRoster.AddToCounts(outputItem, count);
            if (Campaign.Current.GameStarted && !doNotEffectCapital)
            {
                int num = Math.Min(1000, itemPrice) * count;
                workshop.ChangeGold(num);
                town.ChangeGold(-num);
            }
            //CampaignEventDispatcher.Instance.OnItemProduced(outputItem, town.Owner.Settlement, count);
        }

    }
}
