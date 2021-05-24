using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace KaosesTweaks.Models
{
    class BTSettlementMilitiaModel : DefaultSettlementMilitiaModel
    {
        // Token: 0x06002D28 RID: 11560 RVA: 0x000B2341 File Offset: 0x000B0541
        public override void CalculateMilitiaSpawnRate(Settlement settlement, out float meleeTroopRate, out float rangedTroopRate)
        {
            base.CalculateMilitiaSpawnRate(settlement, out meleeTroopRate, out rangedTroopRate);
            meleeTroopRate = 0.5f;
            rangedTroopRate = 1f - meleeTroopRate;
        }

        // Token: 0x06002D27 RID: 11559 RVA: 0x000B22B0 File Offset: 0x000B04B0
        public override float CalculateEliteMilitiaSpawnChance(Settlement settlement)
        {
            float num = 0f;
            Hero hero = null;
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

            if (MCMSettings.Instance is { } settings && settings.SettlementMilitiaEliteSpawnRateBonusEnabled)
            {
                num = settings.SettlementEliteMeleeSpawnRateBonus;
                //_rangedEliteTroopRate = settings.SettlementEliteRangedSpawnRateBonus;
            }
            //meleeEliteTroopRate = _meleeEliteTroopRate;
            //rangedEliteTroopRate = _rangedEliteTroopRate;
            return num;
        }

    }
}
