using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace KaosesTweaks.Models
{
    class BTSettlementMilitiaModel : DefaultSettlementMilitiaModel
    {
        public override void CalculateMilitiaSpawnRate(Settlement settlement, out float meleeTroopRate, out float rangedTroopRate, out float meleeEliteTroopRate, out float rangedEliteTroopRate)
        {
            base.CalculateMilitiaSpawnRate(settlement, out meleeTroopRate, out rangedTroopRate);
            meleeTroopRate = 0.5f;
            rangedTroopRate = 1f - meleeTroopRate;
        }

        // Token: 0x06002D27 RID: 11559 RVA: 0x000B22B0 File Offset: 0x000B04B0
        public override float CalculateEliteMilitiaSpawnChance(Settlement settlement)
        {
            float num = 0f;
            Hero? hero = null;
            if (settlement.IsFortification && settlement.Town.Governor != null)
            {
                hero = settlement.Town.Governor;
            }
            else if (settlement.IsVillage && settlement.Village.TradeBound.Town.Governor != null)
            {
                hero = settlement.Village.TradeBound.Town.Governor;
            }
            if (hero != null && hero.GetPerkValue(DefaultPerks.Leadership.CitizenMilitia))
            {
                num += DefaultPerks.Leadership.CitizenMilitia.PrimaryBonus * 0.01f;
            }
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