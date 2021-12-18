using Helpers;
using KaosesTweaks.Utils;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace KaosesTweaks.Models
{
    class KaosesBattleRewardModel : DefaultBattleRewardModel
    {

        // Token: 0x06002D86 RID: 11654 RVA: 0x000B5B88 File Offset: 0x000B3D88
        public override int GetPlayerGainedRelationAmount(MapEvent mapEvent, Hero hero)
        {
            MapEventSide mapEventSide = mapEvent.AttackerSide.IsMainPartyAmongParties() ? mapEvent.AttackerSide : mapEvent.DefenderSide;
            float playerPartyContributionRate = mapEventSide.GetPlayerPartyContributionRate();
            float num = (mapEvent.StrengthOfSide[(int)PartyBase.MainParty.Side] - PlayerEncounter.Current.PlayerPartyInitialStrength) / mapEvent.StrengthOfSide[(int)PartyBase.MainParty.OpponentSide];
            float num2 = (num < 1f) ? (1f + (1f - num)) : ((num < 3f) ? (0.5f * (3f - num)) : 0f);
            float renownValue = mapEvent.GetRenownValue((mapEventSide == mapEvent.AttackerSide) ? BattleSideEnum.Attacker : BattleSideEnum.Defender);
            //~ KT
            double relationShipGain = GetPlayerGainedRelationAmount(0.75 + Math.Pow(playerPartyContributionRate * 1.3f * (num2 + renownValue), 0.6700000166893005));
            //return (int)(0.75 + Math.Pow((double)(playerPartyContributionRate * 1.3f * (num2 + renownValue)), 0.6700000166893005));
            return (int)relationShipGain;
            //~ KT
        }

        // Token: 0x06002D87 RID: 11655 RVA: 0x000B5C58 File Offset: 0x000B3E58
        public override ExplainedNumber CalculateRenownGain(PartyBase party, float renownValueOfBattle, float contributionShare)
        {
            //~ KT
            float renowGainBase = 0.0f;
            renowGainBase = GetModifiedRenownGain(renownValueOfBattle * contributionShare);
            //~ KT
            //ExplainedNumber result = new ExplainedNumber(renownValueOfBattle * contributionShare, true, null);
            ExplainedNumber result = new ExplainedNumber(renowGainBase, true, null);
            if (party.IsMobile)
            {
                if (party.MobileParty.HasPerk(DefaultPerks.Charm.Warlord, false))
                {
                    PerkHelper.AddPerkBonusForParty(DefaultPerks.Charm.Warlord, party.MobileParty, true, ref result);
                }
                if (party.MobileParty.HasPerk(DefaultPerks.Throwing.LongReach, true))
                {
                    PerkHelper.AddPerkBonusForParty(DefaultPerks.Throwing.LongReach, party.MobileParty, false, ref result);
                }
                PerkObject famousCommander = DefaultPerks.Leadership.FamousCommander;
                MobileParty mobileParty = party.MobileParty;
                PerkHelper.AddPerkBonusForCharacter(famousCommander, (mobileParty != null) ? mobileParty.LeaderHero.CharacterObject : null, true, ref result);
            }
            return result;
        }

        // Token: 0x06002D88 RID: 11656 RVA: 0x000B5CEB File Offset: 0x000B3EEB
        public override ExplainedNumber CalculateInfluenceGain(PartyBase party, float influenceValueOfBattle, float contributionShare)//, ref ExplainedNumber resul
        {
            //~ KT
            float influenceGainBase = 0.0f;
            influenceGainBase = GetModifiedInfluenceGain(party, influenceValueOfBattle * contributionShare);
            ExplainedNumber result = new ExplainedNumber(party.MapFaction.IsKingdomFaction ? influenceGainBase : 0f, true, null);
            //result.Add(party.MapFaction.IsKingdomFaction ? (influenceValueOfBattle * contributionShare) : 0f, null, null);
            //~ KT
            return result;
        }

        // Token: 0x06002D89 RID: 11657 RVA: 0x000B5D14 File Offset: 0x000B3F14
        public override ExplainedNumber CalculateMoraleGainVictory(PartyBase party, float renownValueOfBattle, float contributionShare)
        {
            //~ KT
            float moraleGainBase = 0.0f;
            moraleGainBase = GetModifiedMoraleGain((float)(0.5f + renownValueOfBattle * contributionShare * 0.5f));
            //result.Add(0.5f + renownValueOfBattle * contributionShare * 0.5f, null, null);
            //~ KT
            //ExplainedNumber result = new ExplainedNumber(0.5f + renownValueOfBattle * contributionShare * 0.5f, true, null);
            ExplainedNumber result = new ExplainedNumber(moraleGainBase, true, null);
            if (party.IsMobile && party.MobileParty.HasPerk(DefaultPerks.Throwing.LongReach, true))
            {
                PerkHelper.AddPerkBonusForParty(DefaultPerks.Throwing.LongReach, party.MobileParty, false, ref result);
            }
            if (party.IsMobile && party.MobileParty.HasPerk(DefaultPerks.Leadership.CitizenMilitia, true))
            {
                PerkHelper.AddPerkBonusForParty(DefaultPerks.Leadership.CitizenMilitia, party.MobileParty, false, ref result);
            }
            return result;
        }

        // Token: 0x06002D8A RID: 11658 RVA: 0x000B5D9C File Offset: 0x000B3F9C
        public override int CalculateGoldLossAfterDefeat(Hero partyLeaderHero)
        {
            float num = partyLeaderHero.Gold * 0.05f;
            if (num > 10000f)
            {
                num = 10000f;
            }
            //~ KT
            num = GetModifiedGoldLossAfterDefeat(num);
            //~ KT
            return (int)num;
        }


        //~ KT
        protected int GetPlayerGainedRelationAmount(double relationShipGain)
        {
            double modifiedRelationShipGain = relationShipGain;
            if (Statics._settings.BattleRewardsRelationShipGainModifiers)
            {
                modifiedRelationShipGain = relationShipGain * Statics._settings.BattleRewardsRelationShipGainMultiplier;
                if (Statics._settings.BattleRewardsDebug)
                {
                    IM.MessageDebug("Original RelationShipGain : " + relationShipGain.ToString() +
                    "   Modified Gain : " + modifiedRelationShipGain.ToString() +
                    " Using Multiplier : " + Statics._settings.BattleRewardsRelationShipGainMultiplier.ToString());
                }
            }
            return (int)modifiedRelationShipGain;
        }

        protected float GetModifiedRenownGain(float renownGain)//, ref ExplainedNumber result
        {
            float modifiedRenownGain = renownGain;
            if (Statics._settings.BattleRewardsRenownGainModifiers)
            {
                modifiedRenownGain = renownGain * Statics._settings.BattleRewardsRenownGainMultiplier;
                //result.Add(modifiedRenownGain, new TextObject("KT renown tweak", null), null);
                if (Statics._settings.BattleRewardsDebug)
                {
                    IM.MessageDebug("Original Renown Gain : " + renownGain.ToString() +
                        "   Modified Gain : " + modifiedRenownGain.ToString() +
                        " Using Multiplier : " + Statics._settings.BattleRewardsRenownGainMultiplier.ToString());
                }
            }
            return modifiedRenownGain;
        }

        protected float GetModifiedInfluenceGain(PartyBase party, float influenceGain)
        {
            float modifiedInfluenceGain = influenceGain;
            if (Statics._settings.BattleRewardsInfluenceGainModifiers)
            {
                modifiedInfluenceGain = influenceGain * Statics._settings.BattleRewardsInfluenceGainMultiplier;
                if (Statics._settings.BattleRewardsDebug)
                {
                    IM.MessageDebug("Original Influence Gain : " + influenceGain.ToString() +
                        "   Modified Gain : " + modifiedInfluenceGain.ToString() +
                        " Using Multiplier : " + Statics._settings.BattleRewardsInfluenceGainMultiplier.ToString());
                }
            }
            return modifiedInfluenceGain;
        }

        protected float GetModifiedMoraleGain(float moraleGain)
        {
            float modifiedMoraleGain = moraleGain;
            if (Statics._settings.BattleRewardsMoraleGainModifiers)
            {
                modifiedMoraleGain = moraleGain * Statics._settings.BattleRewardsMoraleGainMultiplier;
                if (Statics._settings.BattleRewardsDebug)
                {
                    IM.MessageDebug("Original Morale Gain : " + moraleGain.ToString() +
                        "   Modified Gain : " + modifiedMoraleGain.ToString() +
                        " Using Multiplier : " + Statics._settings.BattleRewardsMoraleGainMultiplier.ToString());
                }
            }
            return modifiedMoraleGain;
        }

        protected float GetModifiedGoldLossAfterDefeat(float originalGoldLoss)
        {
            float modifiedGoldLoss = originalGoldLoss;
            if (Statics._settings.BattleRewardsGoldLossModifiers)
            {
                modifiedGoldLoss = originalGoldLoss * Statics._settings.BattleRewardsGoldLossMultiplier;
                if (Statics._settings.BattleRewardsDebug)
                {
                    IM.MessageDebug("Original gold loss : " + originalGoldLoss.ToString() +
                        "   Modified loss : " + modifiedGoldLoss.ToString() +
                        " Using Multiplier : " + Statics._settings.BattleRewardsGoldLossMultiplier.ToString());
                }
            }
            return modifiedGoldLoss;
        }

        //~ Copied private methods and variables to allow Model override to work
        #region Copied private methods and variables to allow Modle override to work

        #endregion
    }
}
