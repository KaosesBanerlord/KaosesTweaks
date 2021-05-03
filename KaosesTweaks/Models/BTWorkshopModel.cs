using KaosesTweaks.Settings;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Library;

namespace KaosesTweaks.Models
{
    class BTWorkshopModel : DefaultWorkshopModel
    {
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
                return (int)MathF.Round(base.GetDailyExpense(level) * (settings.WorkshopEffectivnessv2Factor));
            else
                return base.GetDailyExpense(level);
        }
    }
}
