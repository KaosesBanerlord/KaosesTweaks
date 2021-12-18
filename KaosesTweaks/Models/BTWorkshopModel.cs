using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Library;

namespace KaosesTweaks.Models
{
    class BTWorkshopModel : DefaultWorkshopModel
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

        public override int GetMaxWorkshopCountForPlayer()
        {
            if (MCMSettings.Instance is { } settings && settings.MaxWorkshopCountTweakEnabled)
                return settings.BaseWorkshopCount + Clan.PlayerClan.Tier * settings.BonusWorkshopsPerClanTier;
            else
                return base.GetMaxWorkshopCountForPlayer();
        }

        public override int GetBuyingCostForPlayer(Workshop workshop)
        {
            if (MCMSettings.Instance is { } settings && settings.WorkshopBuyingCostTweakEnabled && workshop != null)
                return workshop.WorkshopType.EquipmentCost + (int)workshop.Settlement.Prosperity / 2 + settings.WorkshopBaseCost;
            else
                return base.GetBuyingCostForPlayer(workshop);
        }
        public override int GetDailyExpense(int level)
        {
            if (MCMSettings.Instance is { } settings && settings.WorkshopEffectivnessEnabled)
                return MathF.Round(base.GetDailyExpense(level) * settings.WorkshopEffectivnessv2Factor);
            else
                return base.GetDailyExpense(level);
        }
    }
}
