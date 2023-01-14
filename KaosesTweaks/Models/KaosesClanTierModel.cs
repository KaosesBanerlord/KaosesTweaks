using KaosesTweaks.Objects;
using KaosesTweaks.Settings;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace KaosesTweaks.Models
{
    class KaosesClanTierModel : DefaultClanTierModel
    {

        private static readonly int[] TierLowerRenownLimits = new int[7]
        {
                      0,
                      50,
                      150,
                      350,
                      900,
                      2350,
                      6150
        };
        private readonly TextObject _partyLimitBonusText = GameTexts.FindText("str_clan_tier_party_limit_bonus");
        private readonly TextObject _companionLimitBonusText = GameTexts.FindText("str_clan_tier_companion_limit_bonus");
        private readonly TextObject _mercenaryEligibleText = GameTexts.FindText("str_clan_tier_mercenary_eligible");
        private readonly TextObject _vassalEligibleText = GameTexts.FindText("str_clan_tier_vassal_eligible");
        private readonly TextObject _additionalCurrentPartySizeBonus = GameTexts.FindText("str_clan_tier_party_size_bonus");
        private readonly TextObject _additionalWorkshopCountBonus = GameTexts.FindText("str_clan_tier_workshop_count_bonus");
        private readonly TextObject _kingdomEligibleText = GameTexts.FindText("str_clan_tier_kingdom_eligible");

        public override int MinClanTier => 0;

        public override int MaxClanTier => 6;

        public override int MercenaryEligibleTier => 1;

        public override int VassalEligibleTier => 2;

        public override int BannerEligibleTier => 0;

        public override int RebelClanStartingTier => 3;

        public override int CompanionToLordClanStartingTier => 2;

        private int KingdomEligibleTier => Campaign.Current.Models.KingdomCreationModel.MinimumClanTierToCreateKingdom;

        public override int CalculateInitialRenown(Clan clan)
        {
            int lowerRenownLimit = KaosesClanTierModel.TierLowerRenownLimits[clan.Tier];
            int num = clan.Tier >= this.MaxClanTier ? KaosesClanTierModel.TierLowerRenownLimits[this.MaxClanTier] + 1500 : KaosesClanTierModel.TierLowerRenownLimits[clan.Tier + 1];
            int maxValue = (int)((double)num - (double)(num - lowerRenownLimit) * 0.400000005960464);
            return MBRandom.RandomInt(lowerRenownLimit, maxValue);
        }

        public override int CalculateInitialInfluence(Clan clan) => (int)(150.0 + (double)MBRandom.RandomInt((int)((double)this.CalculateInitialRenown(clan) / 15.0)) + (double)MBRandom.RandomInt(MBRandom.RandomInt(MBRandom.RandomInt(400))));

        public override int CalculateTier(Clan clan)
        {
            int tier = this.MinClanTier;
            for (int index = this.MinClanTier + 1; index <= this.MaxClanTier; ++index)
            {
                if ((double)clan.Renown >= (double)KaosesClanTierModel.TierLowerRenownLimits[index])
                    tier = index;
            }
            return tier;
        }

        public override (ExplainedNumber, bool) HasUpcomingTier(
          Clan clan,
          out TextObject extraExplanation,
          bool includeDescriptions = false)
        {
            bool flag = clan.Tier < this.MaxClanTier;
            ExplainedNumber explainedNumber = new ExplainedNumber(includeDescriptions: includeDescriptions);
            extraExplanation = TextObject.Empty;
            if (flag)
            {
                int num1 = this.GetPartyLimitForTier(clan, clan.Tier + 1) - this.GetPartyLimitForTier(clan, clan.Tier);
                if (num1 != 0)
                    explainedNumber.Add((float)num1, this._partyLimitBonusText);
                int num2 = this.GetCompanionLimitFromTier(clan.Tier + 1) - this.GetCompanionLimitFromTier(clan.Tier);
                if (num2 != 0)
                    explainedNumber.Add((float)num2, this._companionLimitBonusText);
                int num3 = Campaign.Current.Models.PartySizeLimitModel.GetTierPartySizeEffect(clan.Tier + 1) - Campaign.Current.Models.PartySizeLimitModel.GetTierPartySizeEffect(clan.Tier);
                if (num3 > 0)
                    explainedNumber.Add((float)num3, this._additionalCurrentPartySizeBonus);
                int num4 = Campaign.Current.Models.WorkshopModel.GetMaxWorkshopCountForTier(clan.Tier + 1) - Campaign.Current.Models.WorkshopModel.GetMaxWorkshopCountForTier(clan.Tier);
                if (num4 > 0)
                    explainedNumber.Add((float)num4, this._additionalWorkshopCountBonus);
                if (clan.Tier + 1 == this.MercenaryEligibleTier)
                    extraExplanation = this._mercenaryEligibleText;
                else if (clan.Tier + 1 == this.VassalEligibleTier)
                    extraExplanation = this._vassalEligibleText;
                else if (clan.Tier + 1 == this.KingdomEligibleTier)
                    extraExplanation = this._kingdomEligibleText;
            }
            return (explainedNumber, flag);
        }

        public override int GetRequiredRenownForTier(int tier) => KaosesClanTierModel.TierLowerRenownLimits[tier];

        public override int GetPartyLimitForTier(Clan clan, int clanTierToCheck)
        {
            Config _settings = Factory.Settings;
            ExplainedNumber result = new ExplainedNumber();
            if (_settings.ClanAdditionalPartyLimitEnabled && clan == Clan.PlayerClan && _settings.ClanPlayerPartiesLimitEnabled)
            {
                result.Add((float)(_settings.ClanPlayerBasePartiesLimit + Math.Floor(clanTierToCheck * _settings.ClanPlayerPartiesBonusPerClanTier)), new TextObject("KT Player Clan Parties Tweak", null));
            }
            else if (_settings.ClanAIPartiesLimitTweakEnabled && clan.IsClan && !clan.StringId.Contains("_deserters"))
            {

                if (_settings.AICustomSpawnPartiesLimitTweakEnabled && clan.StringId.StartsWith("cs_"))
                {
                    result.Add((float)(_settings.BaseAICustomSpawnPartiesLimit + Math.Floor(clanTierToCheck * _settings.ClanCSPartiesBonusPerClanTier)), new TextObject("KT Custom Spawn Parties Tweak", null));

                }
                else if (_settings.ClanAIMinorClanPartiesLimitTweakEnabled && clan.IsMinorFaction && !clan.StringId.StartsWith("cs_"))
                {
                    result.Add((float)(base.GetPartyLimitForTier(clan, clanTierToCheck) + _settings.ClanAIBaseClanPartiesLimit), new TextObject("KT Minor Clan Parties Tweak", null));
                }
                else if (clan.IsClan)
                {
                    result.Add((float)(_settings.ClanAIBaseClanPartiesLimit + Math.Floor(clanTierToCheck * _settings.ClanAIPartiesBonusPerClanTier)), new TextObject("KT AI Clan Parties Tweak", null));
                }

            }
            else
            {
                result.Add((float)(base.GetPartyLimitForTier(clan, clanTierToCheck)), null);
            }
            this.AddPartyLimitPerkEffects(clan, ref result);
            return MathF.Round(result.ResultNumber);
        }


        private void AddPartyLimitPerkEffects(Clan clan, ref ExplainedNumber result)
        {
            if (clan.Leader == null || !clan.Leader.GetPerkValue(DefaultPerks.Leadership.TalentMagnet))
                return;
            result.Add(DefaultPerks.Leadership.TalentMagnet.SecondaryBonus, DefaultPerks.Leadership.TalentMagnet.Name);
        }

        public override int GetCompanionLimit(Clan clan)
        {
            int companionLimitFromTier = this.GetCompanionLimitFromTier(clan.Tier);
            if (clan.Leader.GetPerkValue(DefaultPerks.Leadership.WePledgeOurSwords))
                companionLimitFromTier += (int)DefaultPerks.Leadership.WePledgeOurSwords.PrimaryBonus;
            if (clan.Leader.GetPerkValue(DefaultPerks.Charm.Camaraderie))
                companionLimitFromTier += (int)DefaultPerks.Charm.Camaraderie.SecondaryBonus;
            return companionLimitFromTier;
        }

        // Token: 0x06002C18 RID: 11288 RVA: 0x000AAFB7 File Offset: 0x000A91B7
        private int GetCompanionLimitFromTier(int clanTier)
        {
            if (Factory.Settings.ClanCompanionLimitEnabled)
            {
                return clanTier + Factory.Settings.ClanCompanionBaseLimit;
            }
            return clanTier + 3;
        }














    }
}
