using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace KaosesTweaks.Models
{
    class BTSettlementMilitiaModel : DefaultSettlementMilitiaModel
    {
        public override void CalculateMilitiaSpawnRate(Settlement settlement, out float meleeTroopRate, out float rangedTroopRate, out float meleeEliteTroopRate, out float rangedEliteTroopRate)
        {
            base.CalculateMilitiaSpawnRate(settlement, out meleeTroopRate, out rangedTroopRate, out float _meleeEliteTroopRate, out float _rangedEliteTroopRate);

            if (MCMSettings.Instance is { } settings && settings.SettlementMilitiaEliteSpawnRateBonusEnabled)
            {
                _meleeEliteTroopRate = settings.SettlementEliteMeleeSpawnRateBonus;
                _rangedEliteTroopRate = settings.SettlementEliteRangedSpawnRateBonus;
            }
            meleeEliteTroopRate = _meleeEliteTroopRate;
            rangedEliteTroopRate = _rangedEliteTroopRate;
        }
    }
}