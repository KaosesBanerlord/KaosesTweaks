using KaosesTweaks.Utils;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace KaosesTweaks.Models
{
    class KaosesClanTierModel : DefaultClanTierModel
    {
        // Token: 0x06002C13 RID: 11283 RVA: 0x000AAD60 File Offset: 0x000A8F60
        public override ValueTuple<ExplainedNumber, bool> HasUpcomingTier(Clan clan, bool includeDescriptions = false)
        {
            bool flag = clan.Tier < MaxClanTier;
            ExplainedNumber item = new ExplainedNumber(0f, includeDescriptions, null);
            if (flag)
            {
                int num = GetPartyLimitForTier(clan, clan.Tier + 1) - GetPartyLimitForTier(clan, clan.Tier);
                if (num != 0)
                {
                    item.Add((float)num, _partyLimitBonusText, null);
                }
                int num2 = GetCompanionLimitFromTier(clan.Tier + 1) - GetCompanionLimitFromTier(clan.Tier);
                if (num2 != 0)
                {
                    item.Add((float)num2, _companionLimitBonusText, null);
                }
                int num3 = Campaign.Current.Models.PartySizeLimitModel.GetTierPartySizeEffect(clan.Tier + 1) - Campaign.Current.Models.PartySizeLimitModel.GetTierPartySizeEffect(clan.Tier);
                if (num3 > 0)
                {
                    item.Add((float)num3, _additionalCurrentPartySizeBonus, null);
                }
                if (clan.Tier + 1 == MercenaryEligibleTier)
                {
                    item.Add(1f, _mercenaryEligibleText, null);
                }
                if (clan.Tier + 1 == VassalEligibleTier)
                {
                    item.Add(1f, _vassalEligibleText, null);
                }
                if (clan.Tier + 1 == KingdomEligibleTier)
                {
                    item.Add(1f, _kingdomEligibleText, null);
                }
            }
            return new ValueTuple<ExplainedNumber, bool>(item, flag);
        }

        // Token: 0x06002C15 RID: 11285 RVA: 0x000AAEB8 File Offset: 0x000A90B8
        public override int GetPartyLimitForTier(Clan clan, int clanTierToCheck)
        {
            ExplainedNumber explainedNumber = new ExplainedNumber(0f, false, null);
            int AdditionalParties = 0;
            if (Statics._settings.ClanAdditionalPartyLimitForTierModifiers)
            {
                AdditionalParties += Statics._settings.ClanAdditionalPartyLimitForTierValue;
            }

            if (!clan.IsMinorFaction || clan == Clan.PlayerClan)
            {
                if (clanTierToCheck < 3)
                {
                    int basePartySize = 1 + AdditionalParties;
                    explainedNumber.Add((float)basePartySize, null, null);
                }
                else if (clanTierToCheck <= 5)
                {
                    int basePartySize = 2 + AdditionalParties;
                    explainedNumber.Add((float)basePartySize, null, null);
                }
                else
                {
                    int basePartySize = 4 + AdditionalParties; //NATIVE: int basePartySize = 3 + AdditionalParties;
                    explainedNumber.Add((float)basePartySize, null, null);
                }
            }
            else
            {
                explainedNumber.Add(MathF.Clamp((float)clanTierToCheck, 1f, 4f), null, null);
            }
            AddPartyLimitPerkEffects(clan, ref explainedNumber);
            return (int)Math.Ceiling(explainedNumber.ResultNumber);
            //return MathF.Round(explainedNumber.ResultNumber);
        }

        // Token: 0x06002C17 RID: 11287 RVA: 0x000AAF7C File Offset: 0x000A917C
        public override int GetCompanionLimit(Clan clan)
        {
            int num = GetCompanionLimitFromTier(clan.Tier);
            if (clan.Leader.GetPerkValue(DefaultPerks.Leadership.WePledgeOurSwords))
            {
                num += (int)DefaultPerks.Leadership.WePledgeOurSwords.PrimaryBonus;
            }
            return num;
        }

        // Token: 0x06002C18 RID: 11288 RVA: 0x000AAFB7 File Offset: 0x000A91B7
        private int GetCompanionLimitFromTier(int clanTier)
        {
            int AdditionalCompanions = 0;
            if (Statics._settings.ClanAdditionaCompanionLimitForTierModifiers)
            {
                AdditionalCompanions += Statics._settings.ClanAdditionalCompanionLimitForTierValue;
            }
            int compLimit = (clanTier + 3);
            //int compLimit = ((clanTier * 2) + 3);
            int CompanionLimit = compLimit + AdditionalCompanions;

            //Ux.MessageDebug("Clan Companion limit  Additional :" + AdditionalCompanions.ToString() + "  native: " + compLimit.ToString() + "  New Limit: "+ CompanionLimit.ToString());
            return CompanionLimit;
        }













        // Token: 0x17000B19 RID: 2841
        // (get) Token: 0x06002C0F RID: 11279 RVA: 0x000AAC6F File Offset: 0x000A8E6F
        private int KingdomEligibleTier
        {
            get
            {
                return Campaign.Current.Models.KingdomCreationModel.MinimumClanTierToCreateKingdom;
            }
        }

        // Token: 0x06002C16 RID: 11286 RVA: 0x000AAF42 File Offset: 0x000A9142
        private void AddPartyLimitPerkEffects(Clan clan, ref ExplainedNumber result)
        {
            if (clan.Leader != null && clan.Leader.GetPerkValue(DefaultPerks.Leadership.TalentMagnet))
            {
                result.Add(DefaultPerks.Leadership.TalentMagnet.SecondaryBonus, DefaultPerks.Leadership.TalentMagnet.Name, null);
            }
        }

        // Token: 0x04000EC5 RID: 3781
        private readonly TextObject _partyLimitBonusText = GameTexts.FindText("str_clan_tier_party_limit_bonus", null);

        // Token: 0x04000EC6 RID: 3782
        private readonly TextObject _companionLimitBonusText = GameTexts.FindText("str_clan_tier_companion_limit_bonus", null);

        // Token: 0x04000EC7 RID: 3783
        private readonly TextObject _mercenaryEligibleText = GameTexts.FindText("str_clan_tier_mercenary_eligible", null);

        // Token: 0x04000EC8 RID: 3784
        private readonly TextObject _vassalEligibleText = GameTexts.FindText("str_clan_tier_vassal_eligible", null);

        // Token: 0x04000EC9 RID: 3785
        private readonly TextObject _additionalCurrentPartySizeBonus = GameTexts.FindText("str_clan_tier_party_size_bonus", null);

        // Token: 0x04000ECA RID: 3786
        private readonly TextObject _kingdomEligibleText = GameTexts.FindText("str_clan_tier_kingdom_eligible", null);
    }
}
