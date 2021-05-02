using KaosesTweaks.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace KaosesTweaks.Models
{
    public class KaosesWorkshopModel : DefaultWorkshopModel
    {

        // Token: 0x17000B27 RID: 2855
        // (get) Token: 0x06002CBA RID: 11450 RVA: 0x000AECEC File Offset: 0x000ACEEC
        public override int DaysForPlayerSaveWorkshopFromBankruptcy
        {
            get
            {
                if (Statics._settings.WorkShopBankruptcyModifiers)
                {
                    return Statics._settings.WorkShopBankruptcyValue;
                }
                return 3;
            }
        }

        // Token: 0x06002CBB RID: 11451 RVA: 0x000AECEF File Offset: 0x000ACEEF
        public override int GetInitialCapital(int level)
        {
            return level * 10000;
        }

        // Token: 0x06002CBC RID: 11452 RVA: 0x000AECF6 File Offset: 0x000ACEF6
        public override int GetDailyExpense(int level)
        {
            return level * 20;
        }


        // Token: 0x06002CBE RID: 11454 RVA: 0x000AED74 File Offset: 0x000ACF74
        public override int GetUpgradeCost(int currentLevel)
        {
            return currentLevel * 5000;
        }

        // Token: 0x06002CBF RID: 11455 RVA: 0x000AED7B File Offset: 0x000ACF7B
        public override int GetMaxWorkshopCountForPlayer()
        {
            int BonusLimit = 0;
            if (Statics._settings.WorkShopMaxWorkshopCountForPlayerModifiers)
            {
                BonusLimit = Statics._settings.WorkShopMaxWorkshopCountForPlayerValue;
                int newCal = (1 + BonusLimit) + Clan.PlayerClan.Tier;
            }
            return (1 + BonusLimit) + Clan.PlayerClan.Tier;
        }
    }
}
