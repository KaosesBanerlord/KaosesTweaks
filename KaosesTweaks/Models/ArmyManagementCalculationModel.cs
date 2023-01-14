using Helpers;
using KaosesCommon.Utils;
using KaosesTweaks.Objects;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace KaosesTweaks.Models
{

    class ArmyManagementCalculationModel : DefaultArmyManagementCalculationModel
    {


        public override ExplainedNumber CalculateDailyCohesionChange(
          Army army,
          bool includeDescriptions = false)
        {
            //~ KT
            float baseChange = Factory.Settings.armyCohesionBaseChange;
            bool IsClanOnlyarmy = IsClanOnlyArmy(army);
            if (IsClanOnlyarmy && Factory.Settings.armyDisableCohesionLossClanOnlyParties)
            {
                baseChange = 0;
            }
            ExplainedNumber cohesionChange = new ExplainedNumber(baseChange, includeDescriptions);
            KaosesCalculateCohesionChangeInternal(army, ref cohesionChange, IsClanOnlyarmy);
            //~ KT

            if (army.LeaderParty.HasPerk(DefaultPerks.Tactics.HordeLeader, true))
                cohesionChange.AddFactor(DefaultPerks.Tactics.HordeLeader.SecondaryBonus * 0.01f, DefaultPerks.Tactics.HordeLeader.Name);
            SiegeEvent siegeEvent = army.LeaderParty.SiegeEvent;
            if (siegeEvent != null && siegeEvent.BesiegerCamp.IsBesiegerSideParty(army.LeaderParty) && army.LeaderParty.HasPerk(DefaultPerks.Engineering.CampBuilding))
                cohesionChange.AddFactor(DefaultPerks.Engineering.CampBuilding.PrimaryBonus, DefaultPerks.Engineering.CampBuilding.Name);
            if (PartyBaseHelper.HasFeat(army.LeaderParty?.Party, DefaultCulturalFeats.SturgianArmyCohesionFeat))
                cohesionChange.AddFactor(DefaultCulturalFeats.SturgianArmyCohesionFeat.EffectBonus, GameTexts.FindText("str_culture"));
            return cohesionChange;
        }


        private void KaosesCalculateCohesionChangeInternal(Army army, ref ExplainedNumber cohesionChange, bool IsClanOnly = false)
        {
            int _numberOfStarvingParties = 0;//numberOfStarvingParties
            int _numberOfLowMoraleParties = 0; //numberOfLowMoraleParties
            int _numberOfLessMemberParties = 0; //numberOfLessMemberParties NumberOfHealthyMembers
            int _numberOfParties = 0; //numberOfParties
            float _finalNumberOfStarvingParties = (float)-((_numberOfStarvingParties + 1) / 2);
            float _finalNumberOfLowMoralePartiess = (float)-((_numberOfLowMoraleParties + 1) / 2);
            float _finalNumberOfLessMemberParties = (float)-((_numberOfLessMemberParties + 1) / 2);
            float _finalNumberOfParties = _numberOfParties;

            foreach (MobileParty andAttachedParty in army.LeaderPartyAndAttachedParties)
            {
                if (andAttachedParty != army.LeaderParty)
                {
                    if (andAttachedParty.Party.IsStarving)
                        ++_numberOfStarvingParties;
                    if ((double)andAttachedParty.Morale <= 25.0)
                        ++_numberOfLowMoraleParties;
                    if (andAttachedParty.Party.NumberOfHealthyMembers <= 10)
                        ++_numberOfLessMemberParties;
                    ++_numberOfParties;
                }
            }
            if (Factory.Settings.armyCohesionMultipliers)
            {
                if (Factory.Settings.ArmyDebug)
                {
                    IM.MessageDebug("KAOSES Cohesion Settings:"
                        + "  army.LeaderParty: " + army.LeaderParty.StringId.ToString()
                        + "  armyDisableCohesionLossClanOnlyParties: " + Factory.Settings.armyDisableCohesionLossClanOnlyParties.ToString()
                        + "  armyApplyMultiplerToClanOnlyParties: " + Factory.Settings.armyApplyMultiplerToClanOnlyParties.ToString()
                        + "  armyIsClanOnly: " + IsClanOnly.ToString());
                }
                if (Factory.Settings.armyDisableCohesionLossClanOnlyParties && IsClanOnly)
                {
                    return;
                }
                else if (Factory.Settings.armyApplyMultiplerToClanOnlyParties && IsClanOnly)
                {
                    if (Factory.Settings.ArmyDebug)
                    {
                        IM.MessageDebug("Only clan multipliers:   starvingCohesion: " + _numberOfStarvingParties.ToString()
                                                                                               + "  lowMoraleCohesion: " + _numberOfLowMoraleParties.ToString()
                                                                                               + " lowHealthyTroops: " + _numberOfLessMemberParties.ToString());
                    }
                    _finalNumberOfStarvingParties = _numberOfLessMemberParties * Factory.Settings.armyCohesionLossMultiplier;
                    _finalNumberOfLowMoralePartiess = _numberOfLowMoraleParties * Factory.Settings.armyCohesionLossMultiplier;
                    _finalNumberOfLessMemberParties = _numberOfLessMemberParties * Factory.Settings.armyCohesionLossMultiplier;
                    _finalNumberOfParties = _numberOfParties * Factory.Settings.armyCohesionLossMultiplier;
                }
                else if (!Factory.Settings.armyApplyMultiplerToClanOnlyParties)
                {
                    if (Factory.Settings.ArmyDebug)
                    {
                        IM.MessageDebug("Multipliers applied to all:   starvingCohesion: " + _numberOfLessMemberParties.ToString()
                                                                + "  lowMoraleCohesion: " + _numberOfLowMoraleParties.ToString()
                                                                + " lowHealthyTroops: " + _numberOfLessMemberParties.ToString());
                    }
                    _finalNumberOfStarvingParties = _numberOfLessMemberParties * Factory.Settings.armyCohesionLossMultiplier;
                    _finalNumberOfLowMoralePartiess = _numberOfLowMoraleParties * Factory.Settings.armyCohesionLossMultiplier;
                    _finalNumberOfLessMemberParties = _numberOfLessMemberParties * Factory.Settings.armyCohesionLossMultiplier;
                    _finalNumberOfParties = _numberOfParties * Factory.Settings.armyCohesionLossMultiplier;
                }
            }
            cohesionChange.Add(_finalNumberOfParties, ArmyManagementCalculationModel._numberOfPartiesText);
            cohesionChange.Add(_finalNumberOfStarvingParties, ArmyManagementCalculationModel._numberOfStarvingPartiesText);
            cohesionChange.Add(_finalNumberOfLowMoralePartiess, ArmyManagementCalculationModel._numberOfLowMoralePartiesText);
            cohesionChange.Add(_finalNumberOfLessMemberParties, ArmyManagementCalculationModel._numberOfLessMemberPartiesText);

            if (Factory.Settings.ArmyDebug)
            {
                IM.MessageDebug("Final Cohesion :"
                                + " base change: " + Factory.Settings.armyCohesionBaseChange.ToString()
                                + " starvingCohesion: " + _numberOfStarvingParties.ToString()
                                + " lowMoraleCohesion: " + _numberOfLowMoraleParties.ToString()
                                + " lowHealthyTroops: " + _numberOfLessMemberParties.ToString()
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

        public override int CalculateNewCohesion(
          Army army,
          PartyBase newParty,
          int calculatedCohesion,
          int sign)
        {
            if (army == null)
                return calculatedCohesion;
            sign = MathF.Sign(sign);
            int num1 = sign == 1 ? army.Parties.Count - 1 : army.Parties.Count;
            int num2 = (calculatedCohesion * num1 + 100 * sign) / (num1 + sign);
            if (num2 > 100)
                return 100;
            return num2 >= 0 ? num2 : 0;
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
        // public const int InfluenceValuePerGold = 40;

        // Token: 0x04000F68 RID: 3944
        // public const int AverageCallToArmyCost = 20;
        #endregion

    }
}
