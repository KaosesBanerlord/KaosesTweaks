using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Library;

namespace KaosesTweaks.Models
{
    class KaosesBanditDensityModel : DefaultBanditDensityModel
    {

        // Token: 0x17000B3A RID: 2874
        // (get) Token: 0x06002D7C RID: 11644 RVA: 0x000B59C9 File Offset: 0x000B3BC9
        public override int NumberOfMaximumLooterParties
        {
            get
            {
                return 300;
            }
        }

        // Token: 0x17000B3B RID: 2875
        // (get) Token: 0x06002D7D RID: 11645 RVA: 0x000B59D0 File Offset: 0x000B3BD0
        public override int NumberOfMinimumBanditPartiesInAHideoutToInfestIt
        {
            get
            {
                return 2;
            }
        }

        // Token: 0x17000B3C RID: 2876
        // (get) Token: 0x06002D7E RID: 11646 RVA: 0x000B59D3 File Offset: 0x000B3BD3
        public override int NumberOfMaximumBanditPartiesInEachHideout
        {
            get
            {
                return 4;
            }
        }

        // Token: 0x17000B3D RID: 2877
        // (get) Token: 0x06002D7F RID: 11647 RVA: 0x000B59D6 File Offset: 0x000B3BD6
        public override int NumberOfMaximumBanditPartiesAroundEachHideout
        {
            get
            {
                return 8;
            }
        }

        // Token: 0x17000B3E RID: 2878
        // (get) Token: 0x06002D80 RID: 11648 RVA: 0x000B59D9 File Offset: 0x000B3BD9
        public override int NumberOfMaximumHideoutsAtEachBanditFaction
        {
            get
            {
                return 10;
            }
        }

        // Token: 0x17000B3F RID: 2879
        // (get) Token: 0x06002D81 RID: 11649 RVA: 0x000B59DD File Offset: 0x000B3BDD
        public override int NumberOfInitialHideoutsAtEachBanditFaction
        {
            get
            {
                return 3;
            }
        }

        // Token: 0x17000B40 RID: 2880
        // (get) Token: 0x06002D82 RID: 11650 RVA: 0x000B59E0 File Offset: 0x000B3BE0
        public override int NumberOfMinimumBanditTroopsInHideoutMission
        {
            get
            {
                return 10;
            }
        }

        // Token: 0x17000B41 RID: 2881
        // (get) Token: 0x06002D83 RID: 11651 RVA: 0x000B59E4 File Offset: 0x000B3BE4
        public override int NumberOfMaximumTroopCountForFirstFightInHideout
        {
            get
            {
                return MathF.Floor(6f * (2f + MiscHelper.GetGameProcess()));
            }
        }

        // Token: 0x17000B42 RID: 2882
        // (get) Token: 0x06002D84 RID: 11652 RVA: 0x000B59FC File Offset: 0x000B3BFC
        public override int NumberOfMaximumTroopCountForBossFightInHideout
        {
            get
            {
                return MathF.Floor(1f + 5f * (1f + MiscHelper.GetGameProcess()));
            }
        }

        // Token: 0x17000B43 RID: 2883
        // (get) Token: 0x06002D85 RID: 11653 RVA: 0x000B5A1A File Offset: 0x000B3C1A
        public override float SpawnPercentageForFirstFightInHideoutMission
        {
            get
            {
                return 0.75f;
            }
        }

        // Token: 0x06002D86 RID: 11654 RVA: 0x000B5A24 File Offset: 0x000B3C24
        public override int GetPlayerMaximumTroopCountForHideoutMission(MobileParty party)
        {
            float num = Statics._settings.HideoutBattleTroopLimit;
            if (party.HasPerk(DefaultPerks.Tactics.SmallUnitTactics, false))
            {
                num += DefaultPerks.Tactics.SmallUnitTactics.PrimaryBonus;
            }
            return MathF.Round(num);
        }
    }
}
