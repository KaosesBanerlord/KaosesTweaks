using KaosesTweaks.Utils;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace KaosesTweaks.Models
{
    class KaosesArmyManagementCalculationModel : DefaultArmyManagementCalculationModel
    {

        /*

                // Token: 0x17000B39 RID: 2873
                // (get) Token: 0x06002D6A RID: 11626 RVA: 0x000B4F54 File Offset: 0x000B3154
                public override int CohesionThresholdForDispersion
                {
                    get
                    {
                        return 10;
                    }
                }

                // Token: 0x06002D6B RID: 11627 RVA: 0x000B4F58 File Offset: 0x000B3158
                public override float DailyBeingAtArmyInfluenceAward(MobileParty armyMemberParty)
                {
                    return (armyMemberParty.Party.TotalStrength + 20f) / 200f;
                }

                // Token: 0x06002D6C RID: 11628 RVA: 0x000B4F74 File Offset: 0x000B3174
                public override int CalculatePartyInfluenceCost(MobileParty armyLeaderParty, MobileParty party)
                {
                    if (armyLeaderParty.LeaderHero != null && party.LeaderHero != null && armyLeaderParty.LeaderHero.Clan == party.LeaderHero.Clan)
                    {
                        return 0;
                    }
                    float num = (float)armyLeaderParty.LeaderHero.GetRelation(party.LeaderHero);
                    float partySizeScore = GetPartySizeScore(party);
                    float num2 = (float)MBMath.Round(party.Party.TotalStrength);
                    double num3 = (num < 0f) ? (1.0 + Math.Sqrt((double)Math.Abs(Math.Max(-100f, num))) / 10.0) : (1.0 - Math.Sqrt((double)Math.Abs(Math.Min(100f, num))) / 20.0);
                    float num4 = 0.5f + num2 / 100f;
                    float num5 = 0.5f + 1f * (1f - (partySizeScore - _minimumPartySizeScoreNeeded) / (1f - _minimumPartySizeScoreNeeded));
                    double num6 = 1.0 + 1.0 * Math.Pow((double)(Campaign.Current.Models.MapDistanceModel.GetDistance(armyLeaderParty, party) / Campaign.MapDiagonal), 0.6700000166893005);
                    double num7 = (party.LeaderHero != null) ? ((double)(0.75f + 0.5f * ((float)party.LeaderHero.RandomValueRarelyChanging / 100f))) : 1.0;
                    float num8 = 1f;
                    float num9 = 1f;
                    if (armyLeaderParty.LeaderHero != null && armyLeaderParty.LeaderHero.Clan.Kingdom != null)
                    {
                        if (armyLeaderParty.LeaderHero.Clan.Tier >= 5 && armyLeaderParty.LeaderHero.Clan.Kingdom.ActivePolicies.Contains(DefaultPolicies.Marshals))
                        {
                            num8 -= 0.1f;
                        }
                        if (armyLeaderParty.LeaderHero.Clan.Kingdom.ActivePolicies.Contains(DefaultPolicies.RoyalCommissions))
                        {
                            if (armyLeaderParty.LeaderHero == armyLeaderParty.LeaderHero.Clan.Kingdom.Leader)
                            {
                                num8 -= 0.3f;
                            }
                            else
                            {
                                num8 += 0.1f;
                            }
                        }
                        if (party.LeaderHero != null && armyLeaderParty.LeaderHero.Clan.Kingdom != null)
                        {
                            if (party.LeaderHero.Clan.Kingdom.ActivePolicies.Contains(DefaultPolicies.LordsPrivyCouncil) && party.LeaderHero.Clan.Tier <= 4)
                            {
                                num8 += 0.2f;
                            }
                            if (party.LeaderHero.Clan.Kingdom.ActivePolicies.Contains(DefaultPolicies.Senate) && party.LeaderHero.Clan.Tier <= 2)
                            {
                                num8 += 0.1f;
                            }
                        }
                        if (armyLeaderParty.LeaderHero.GetPerkValue(DefaultPerks.Leadership.InspiringLeader))
                        {
                            num9 += DefaultPerks.Leadership.InspiringLeader.PrimaryBonus;
                        }
                        if (armyLeaderParty.LeaderHero.GetPerkValue(DefaultPerks.Tactics.CallToArms))
                        {
                            num9 += DefaultPerks.Tactics.CallToArms.SecondaryBonus;
                        }
                    }
                    return (int)((double)0.65f * num3 * (double)num4 * num7 * num6 * (double)num5 * (double)num8 * (double)num9 * 20.0);
                }

                // Token: 0x06002D6D RID: 11629 RVA: 0x000B52A8 File Offset: 0x000B34A8
                public override List<MobileParty> GetMobilePartiesToCallToArmy(MobileParty leaderParty)
                {
                    List<MobileParty> list = new List<MobileParty>();
                    bool flag = false;
                    bool flag2 = false;
                    if (leaderParty.LeaderHero != null)
                    {
                        foreach (Settlement settlement in leaderParty.MapFaction.Settlements)
                        {
                            if (settlement.IsFortification && settlement.SiegeEvent != null)
                            {
                                flag = true;
                                if (settlement.OwnerClan == leaderParty.LeaderHero.Clan)
                                {
                                    flag2 = true;
                                }
                            }
                        }
                    }
                    int val = (leaderParty.MapFaction.IsKingdomFaction && (Kingdom)leaderParty.MapFaction != null) ? ((Kingdom)leaderParty.MapFaction).Armies.Count<Army>() : 0;
                    double num = ((double)(0.55f - (float)Math.Min(2, val) * 0.05f) - ((Hero.MainHero.MapFaction == leaderParty.MapFaction) ? 0.05 : 0.0)) * (1.0 - 0.5 * Math.Sqrt((double)Math.Min(leaderParty.LeaderHero.Clan.Influence, 900f)) / Math.Sqrt(900.0));
                    num *= (double)(flag2 ? 1.25f : 1f);
                    num *= (double)(flag ? 1.125f : 1f);
                    num *= (double)(0.85f + 0.15f * ((float)leaderParty.LeaderHero.RandomValueRarelyChanging / 99f));
                    double num2 = (double)Math.Min(leaderParty.LeaderHero.Clan.Influence, 900f) * Math.Min(1.0, num);
                    Dictionary<MobileParty, float> dictionary = new Dictionary<MobileParty, float>();
                    foreach (Hero hero in leaderParty.MapFaction.Heroes)
                    {
                        float num3 = (hero.PartyBelongedTo != null) ? hero.PartyBelongedTo.PartySizeRatio : 1f;
                        if (hero != leaderParty.LeaderHero && hero != Hero.MainHero && hero.PartyBelongedTo != null && hero.PartyBelongedTo != MobileParty.MainParty && hero != hero.MapFaction.Leader && hero.PartyBelongedTo.IsLordParty && !hero.PartyBelongedTo.Ai.DoNotMakeNewDecisions && hero.PartyBelongedTo.IsLordParty && (hero.CurrentSettlement == null || hero.CurrentSettlement.SiegeEvent == null) && !hero.PartyBelongedTo.IsDisbanding && num3 > 0.4f && hero.PartyBelongedTo.Army == null)
                        {
                            MobileParty partyBelongedTo = hero.PartyBelongedTo;
                            if (partyBelongedTo.MapEvent == null && partyBelongedTo.BesiegedSettlement == null && !dictionary.ContainsKey(partyBelongedTo))
                            {
                                int num4 = Campaign.Current.Models.ArmyManagementCalculationModel.CalculatePartyInfluenceCost(leaderParty, partyBelongedTo);
                                float totalStrength = partyBelongedTo.Party.TotalStrength;
                                float num5 = 1f - (float)(partyBelongedTo.Party.MemberRoster.TotalWounded / partyBelongedTo.Party.MemberRoster.TotalManCount);
                                float value = totalStrength / ((float)num4 + 0.1f) * num5;
                                dictionary.Add(partyBelongedTo, value);
                            }
                        }
                    }
                    bool flag3 = false;
                    do
                    {
                        flag3 = false;
                        float num6 = 0.01f;
                        MobileParty mobileParty = null;
                        foreach (KeyValuePair<MobileParty, float> keyValuePair in dictionary)
                        {
                            if (keyValuePair.Value > num6)
                            {
                                mobileParty = keyValuePair.Key;
                                num6 = keyValuePair.Value;
                                flag3 = true;
                            }
                        }
                        if (mobileParty != null && mobileParty.BesiegedSettlement == null)
                        {
                            int num7 = Campaign.Current.Models.ArmyManagementCalculationModel.CalculatePartyInfluenceCost(leaderParty, mobileParty);
                            dictionary[mobileParty] = 0f;
                            if (num2 > (double)num7)
                            {
                                num2 -= (double)num7;
                                list.Add(mobileParty);
                            }
                        }
                    }
                    while (flag3);
                    return list;
                }

                // Token: 0x06002D6E RID: 11630 RVA: 0x000B5714 File Offset: 0x000B3914
                public override int CalculateTotalInfluenceCost(Army army, float percentage)
                {
                    int num = 0;
                    foreach (MobileParty party in from p in army.Parties
                                                  where !p.IsMainParty
                                                  select p)
                    {
                        num += CalculatePartyInfluenceCost(army.LeaderParty, party);
                    }
                    ExplainedNumber explainedNumber = new ExplainedNumber((float)num, false, null);
                    if (army.LeaderParty.MapFaction.IsKingdomFaction && ((Kingdom)army.LeaderParty.MapFaction).ActivePolicies.Contains(DefaultPolicies.RoyalCommissions))
                    {
                        explainedNumber.AddFactor(-0.3f, null);
                    }
                    if (army.LeaderParty.Leader.GetPerkValue(DefaultPerks.Tactics.Encirclement))
                    {
                        explainedNumber.AddFactor(DefaultPerks.Tactics.Encirclement.SecondaryBonus, null);
                    }
                    return (int)Math.Ceiling((double)(explainedNumber.ResultNumber * percentage) / 100.0);
                }

                // Token: 0x06002D6F RID: 11631 RVA: 0x000B5820 File Offset: 0x000B3A20
                public override float GetPartySizeScore(MobileParty party)
                {
                    return Math.Min(1f, party.PartySizeRatio);
                }*/

        // Token: 0x06002D70 RID: 11632 RVA: 0x000B5834 File Offset: 0x000B3A34
        public override ExplainedNumber CalculateCohesionChange(Army army, bool includeDescriptions = false)
        {
            //~ KT
            float baseChange = Statics._settings.armyCohesionBaseChange;
            bool IsClanOnlyarmy = IsClanOnlyArmy(army);
            if (IsClanOnlyarmy && Statics._settings.armyDisableCohesionLossClanOnlyParties)
            {
                baseChange = 0;
            }
            ExplainedNumber result = new ExplainedNumber(baseChange, includeDescriptions, null);
            KaosesCalculateCohesionChangeInternal(army, IsClanOnlyarmy, ref result);
            //~ KT
            MobileParty leaderParty = army.LeaderParty;
            SiegeEvent siegeEvent = (leaderParty != null) ? leaderParty.SiegeEvent : null;
            if (siegeEvent != null && siegeEvent.BesiegerCamp.IsBesiegerSideParty(army.LeaderParty)
                && army.LeaderParty.HasPerk(DefaultPerks.Engineering.CampBuilding, false))
            {
                result.AddFactor(DefaultPerks.Engineering.CampBuilding.PrimaryBonus, DefaultPerks.Engineering.CampBuilding.Name);
            }
            return result;
        }

        // Token: 0x06002D71 RID: 11633 RVA: 0x000B58B0 File Offset: 0x000B3AB0
        private void KaosesCalculateCohesionChangeInternal(Army army, bool IsClanOnly, ref ExplainedNumber cohesionChange)
        {
            int starvingParties = 0;
            int lowMoraleParties = 0;
            int lowHealthyTroopsParties = 0;
            int num4 = 0;
            bool armyIsClanOnly = IsClanOnly;

            foreach (MobileParty mobileParty in army.Parties)
            {
                if (mobileParty != army.LeaderParty)
                {
                    if (mobileParty.Party.IsStarving)
                    {
                        starvingParties++;
                    }
                    if (mobileParty.Morale <= 25f)
                    {
                        lowMoraleParties++;
                    }
                    if (mobileParty.Party.NumberOfHealthyMembers <= 10)
                    {
                        lowHealthyTroopsParties++;
                    }
                    /* Idea fro rework*/
                    //float injuredRatio = mobileParty.MemberRoster.TotalRegulars / mobileParty.MemberRoster.TotalWoundedRegulars;
                    /* the more injured to troops more cohesion loss*/
                    num4++;
                }
            }
            float starvingCohesion = (starvingParties + 1) / 2;
            float lowMoraleCohesion = (lowMoraleParties + 1) / 2;
            float lowHealthyTroops = (lowHealthyTroopsParties + 1) / 2;

            if (Statics._settings.armyCohesionMultipliers)
            {
                if (Statics._settings.ArmyDebug)
                {
                    IM.MessageDebug("KAOSES Cohesion Settings:"
                        + "  army.LeaderParty: " + army.LeaderParty.StringId.ToString()
                        + "  armyDisableCohesionLossClanOnlyParties: " + Statics._settings.armyDisableCohesionLossClanOnlyParties.ToString()
                        + "  armyApplyMultiplerToClanOnlyParties: " + Statics._settings.armyApplyMultiplerToClanOnlyParties.ToString()
                        + "  armyIsClanOnly: " + armyIsClanOnly.ToString());
                }

                if (Statics._settings.armyDisableCohesionLossClanOnlyParties && armyIsClanOnly)
                {
                    starvingCohesion = 0;
                    lowMoraleCohesion = 0;
                    lowHealthyTroops = 0;
                }
                else if (Statics._settings.armyApplyMultiplerToClanOnlyParties && armyIsClanOnly)
                {
                    if (Statics._settings.ArmyDebug)
                    {
                        IM.MessageDebug("Only clan multipliers:   starvingCohesion: " + starvingCohesion.ToString()
                                                                                               + "  lowMoraleCohesion: " + lowMoraleCohesion.ToString()
                                                                                               + " lowHealthyTroops: " + lowHealthyTroops.ToString());
                    }
                    starvingCohesion *= Statics._settings.armyCohesionLossMultiplier;
                    lowMoraleCohesion *= Statics._settings.armyCohesionLossMultiplier;
                    lowHealthyTroops *= Statics._settings.armyCohesionLossMultiplier;
                }
                else if (!Statics._settings.armyApplyMultiplerToClanOnlyParties)
                {
                    if (Statics._settings.ArmyDebug)
                    {
                        IM.MessageDebug("Multipliers applied to all:   starvingCohesion: " + starvingCohesion.ToString()
                                                                + "  lowMoraleCohesion: " + lowMoraleCohesion.ToString()
                                                                + " lowHealthyTroops: " + lowHealthyTroops.ToString());
                    }
                    starvingCohesion *= Statics._settings.armyCohesionLossMultiplier;
                    lowMoraleCohesion *= Statics._settings.armyCohesionLossMultiplier;
                    lowHealthyTroops *= Statics._settings.armyCohesionLossMultiplier;
                }
            }


            //cohesionChange.Add((float)(-(float)num4), _numberOfPartiesText, null);
            if (starvingCohesion > 0)
            {
                cohesionChange.Add((float)-(float)starvingCohesion, _numberOfStarvingPartiesText, null);
            }
            if (lowMoraleCohesion > 0)
            {
                cohesionChange.Add((float)-(float)lowMoraleCohesion, _numberOfLowMoralePartiesText, null);
            }
            if (lowHealthyTroops > 0)
            {
                cohesionChange.Add((float)-(float)lowHealthyTroops, _numberOfLessMemberPartiesText, null);
            }


            if (army.LeaderParty.HasPerk(DefaultPerks.Tactics.HordeLeader, true))
            {
                cohesionChange.AddFactor(DefaultPerks.Tactics.HordeLeader.SecondaryBonus * 0.01f, DefaultPerks.Tactics.HordeLeader.Name);
            }

            if (Statics._settings.ArmyDebug)
            {
                IM.MessageDebug("Final Cohesion :"
                                + " base change: " + Statics._settings.armyCohesionBaseChange.ToString()
                                + " starvingCohesion: " + starvingCohesion.ToString()
                                + " lowMoraleCohesion: " + lowMoraleCohesion.ToString()
                                + " lowHealthyTroops: " + lowHealthyTroops.ToString()
                                + " result: " + cohesionChange.ResultNumber.ToString());
            }
        }

        //~ KT
        protected bool IsClanOnlyArmy(Army army)
        {
            bool result = true;
            Clan armyClan = army.LeaderParty.ActualClan;
            foreach (MobileParty mobileParty in army.Parties)
            {
                if (mobileParty != army.LeaderParty)
                {
                    if (mobileParty.ActualClan != army.LeaderParty.ActualClan)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        //~ Copied Methods To Make Model Override work
        #region Copied Methods To Make Model Override work
        // Token: 0x06002D72 RID: 11634 RVA: 0x000B59C8 File Offset: 0x000B3BC8
        public override int CalculateNewCohesion(Army army, PartyBase newParty, int calculatedCohesion, int sign)
        {
            if (army == null)
            {
                return calculatedCohesion;
            }
            sign = Math.Sign(sign);
            int num = (sign == 1) ? (army.Parties.Count - 1) : army.Parties.Count;
            int num2 = MathF.Ceiling((float)(calculatedCohesion * num + 100 * sign)) / (num + sign);
            if (num2 > 100)
            {
                return 100;
            }
            if (num2 >= 0)
            {
                return num2;
            }
            return 0;
        }

        // Token: 0x06002D73 RID: 11635 RVA: 0x000B5A27 File Offset: 0x000B3C27
        public override int GetCohesionBoostInfluenceCost(Army army, int percentageToBoost = 100)
        {
            return CalculateTotalInfluenceCost(army, percentageToBoost);
        }

        // Token: 0x06002D74 RID: 11636 RVA: 0x000B5A32 File Offset: 0x000B3C32
        public override int GetCohesionBoostGoldCost(Army army, float percentageToBoost = 100f)
        {
            return CalculateTotalInfluenceCost(army, percentageToBoost) * 40;
        }

        // Token: 0x06002D75 RID: 11637 RVA: 0x000B5A3F File Offset: 0x000B3C3F
        public override int GetPartyRelation(Hero hero)
        {
            if (hero == null)
            {
                return -101;
            }
            if (hero == Hero.MainHero)
            {
                return 101;
            }
            return Hero.MainHero.GetRelation(hero);
        }

        // Token: 0x06002D76 RID: 11638 RVA: 0x000B5A5D File Offset: 0x000B3C5D
        public override int GetPartyStrength(PartyBase party)
        {
            return MathF.Round(party.TotalStrength);
        }

        // Token: 0x06002D77 RID: 11639 RVA: 0x000B5A6A File Offset: 0x000B3C6A
        public override bool CheckPartyEligibility(MobileParty party)
        {
            return party.Army == null && GetPartySizeScore(party) > _minimumPartySizeScoreNeeded && party.MapEvent == null;
        }

        // Token: 0x04000F62 RID: 3938
        private static readonly TextObject _numberOfPartiesText = GameTexts.FindText("str_number_of_parties", null);

        // Token: 0x04000F63 RID: 3939
        private static readonly TextObject _numberOfStarvingPartiesText = GameTexts.FindText("str_number_of_starving_parties", null);

        // Token: 0x04000F64 RID: 3940
        private static readonly TextObject _numberOfLowMoralePartiesText = GameTexts.FindText("str_number_of_low_morale_parties", null);

        // Token: 0x04000F65 RID: 3941
        private static readonly TextObject _numberOfLessMemberPartiesText = GameTexts.FindText("str_number_of_less_member_parties", null);

        // Token: 0x04000F66 RID: 3942
        private float _minimumPartySizeScoreNeeded = 0.4f;

        // Token: 0x04000F67 RID: 3943
        public const int InfluenceValuePerGold = 40;

        // Token: 0x04000F68 RID: 3944
        public const int AverageCallToArmyCost = 20;
        #endregion

    }
}
