using Helpers;
using KaosesTweaks.Utils;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;

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
            double originalRelationShipGain = 0.75 + Math.Pow((double)(playerPartyContributionRate * 1.3f * (num2 + renownValue)), 0.6700000166893005);
            double modifiedRelationShipGain;
            IM.MessageDebug("GetPlayerGainedRelationAmount: playerPartyContributionRate: " + playerPartyContributionRate.ToString() + "  num: " + num.ToString() + "  num2: " + num2.ToString() + "  renownValue: " + renownValue.ToString());
            if (Statics._settings.BattleRewardsRelationShipGainModifiers)
            {
                modifiedRelationShipGain = originalRelationShipGain * Statics._settings.BattleRewardsRelationShipGainMultiplier;
            }
            else
            {
                modifiedRelationShipGain = originalRelationShipGain;
            }
            IM.MessageDebug("Original RelationShipGain : "+ originalRelationShipGain.ToString() + "   Modified Gain : "+ modifiedRelationShipGain.ToString()  + " Using Multiplier : " + Statics._settings.BattleRewardsRelationShipGainMultiplier.ToString());
            return (int)modifiedRelationShipGain;
        }

        // Token: 0x06002D87 RID: 11655 RVA: 0x000B5C58 File Offset: 0x000B3E58
        public override float CalculateRenownGain(PartyBase party, float renownValueOfBattle, float contributionShare, ref ExplainedNumber result)
        {
            float originalRenownGain = renownValueOfBattle * contributionShare;
            float modifiedRenownGain;
            if (Statics._settings.BattleRewardsRenownGainModifiers)
            {
                modifiedRenownGain = originalRenownGain * Statics._settings.BattleRewardsRenownGainMultiplier;
            }
            else
            {
                modifiedRenownGain = originalRenownGain;
            }
            IM.MessageDebug("Original Renown Gain : " + originalRenownGain.ToString() + "   Modified Gain : " + modifiedRenownGain.ToString()+ " Using Multiplier : " + Statics._settings.BattleRewardsRenownGainMultiplier.ToString());
            result.Add(modifiedRenownGain, null, null);

            if (party.IsMobile)
            {
                if (party.MobileParty.HasPerk(DefaultPerks.Charm.ShowYourScars, false))
                {
                    PerkHelper.AddPerkBonusForParty(DefaultPerks.Charm.ShowYourScars, party.MobileParty, true, ref result);
                }
                if (party.MobileParty.HasPerk(DefaultPerks.Throwing.LongReach, true))
                {
                    PerkHelper.AddPerkBonusForParty(DefaultPerks.Throwing.LongReach, party.MobileParty, false, ref result);
                }
                PerkObject famousCommander = DefaultPerks.Leadership.FamousCommander;
                MobileParty mobileParty = party.MobileParty;
                PerkHelper.AddPerkBonusForCharacter(famousCommander, (mobileParty != null) ? mobileParty.Leader : null, true, ref result);
            }
            return result.ResultNumber;
        }

        // Token: 0x06002D88 RID: 11656 RVA: 0x000B5CEB File Offset: 0x000B3EEB
        public override float CalculateInfluenceGain(PartyBase party, float influenceValueOfBattle, float contributionShare, ref ExplainedNumber result)
        {
            float originalInfluenceGain = influenceValueOfBattle * contributionShare;
            float modifiedInfluenceGain;
            if (Statics._settings.BattleRewardsInfluenceGainModifiers)
            {
                modifiedInfluenceGain = originalInfluenceGain * Statics._settings.BattleRewardsInfluenceGainMultiplier;
            }
            else
            {
                modifiedInfluenceGain = originalInfluenceGain;
            }
            IM.MessageDebug("Original Influence Gain : " + originalInfluenceGain.ToString() + "   Modified Gain : " + modifiedInfluenceGain.ToString()+ " Using Multiplier : " + Statics._settings.BattleRewardsInfluenceGainMultiplier.ToString());
            
            result.Add(party.MapFaction.IsKingdomFaction ? (modifiedInfluenceGain) : 0f, null, null);
            return result.ResultNumber;
        }

        // Token: 0x06002D89 RID: 11657 RVA: 0x000B5D14 File Offset: 0x000B3F14
        public override float CalculateMoraleGainVictory(PartyBase party, float renownValueOfBattle, float contributionShare, ref ExplainedNumber result)
        {
            float originalMoraleGain = 0.5f + renownValueOfBattle * contributionShare * 0.5f;
            float modifiedMoraleGain;
            if (Statics._settings.BattleRewardsMoraleGainModifiers)
            {
                modifiedMoraleGain = originalMoraleGain * Statics._settings.BattleRewardsMoraleGainMultiplier;
            }
            else
            {
                modifiedMoraleGain = originalMoraleGain;
            }
            IM.MessageDebug("Original Morale Gain : " + originalMoraleGain.ToString() + "   Modified Gain : " + modifiedMoraleGain.ToString()+ " Using Multiplier : " + Statics._settings.BattleRewardsMoraleGainMultiplier.ToString());

            result.Add(modifiedMoraleGain, null, null);
            if (party.IsMobile && party.MobileParty.HasPerk(DefaultPerks.Throwing.LongReach, true))
            {
                PerkHelper.AddPerkBonusForParty(DefaultPerks.Throwing.LongReach, party.MobileParty, false, ref result);
            }
            if (party.IsMobile && party.MobileParty.HasPerk(DefaultPerks.Leadership.CitizenMilitia, true))
            {
                PerkHelper.AddPerkBonusForParty(DefaultPerks.Leadership.CitizenMilitia, party.MobileParty, false, ref result);
            }
            return result.ResultNumber;
        }

        // Token: 0x06002D8A RID: 11658 RVA: 0x000B5D9C File Offset: 0x000B3F9C
        public override int CalculateGoldLossAfterDefeat(Hero partyLeaderHero)
        {
            float num = (float)partyLeaderHero.Gold * 0.05f;
            if (num > 10000f)
            {
                num = 10000f;
            }
            float originalGoldLoss = num;
            float modifiedGoldLoss;
            if (Statics._settings.BattleRewardsGoldLossModifiers)
            {
                modifiedGoldLoss = originalGoldLoss * Statics._settings.BattleRewardsGoldLossMultiplier;
            }
            else
            {
                modifiedGoldLoss = originalGoldLoss;
            }
            IM.MessageDebug("Original Gold Lost on defeat : " + originalGoldLoss.ToString() + "   Modified Gain : " + modifiedGoldLoss.ToString()+ " Using Multiplier : " + Statics._settings.BattleRewardsGoldLossMultiplier.ToString());
            return (int)modifiedGoldLoss;
        }

        // Token: 0x06002D8B RID: 11659 RVA: 0x000B5DC8 File Offset: 0x000B3FC8
        public override EquipmentElement GetLootedItemFromTroop(CharacterObject character, float targetValue)
        {
            Equipment randomElement = character.AllEquipments.GetRandomElement<Equipment>();
            return GetRandomItem(randomElement, targetValue);
        }

        // Token: 0x06002D8C RID: 11660 RVA: 0x000B5DEC File Offset: 0x000B3FEC
        private EquipmentElement GetRandomItem(Equipment equipment, float targetValue = 0f)
        {
            int num = 0;
            for (int i = 0; i < 12; i++)
            {
                if (equipment[i].Item != null && !equipment[i].Item.NotMerchandise)
                {
                    _indices[num] = i;
                    num++;
                }
            }
            for (int j = 0; j < num - 1; j++)
            {
                int num2 = j;
                int value = equipment[_indices[j]].Item.Value;
                for (int k = j + 1; k < num; k++)
                {
                    if (equipment[_indices[k]].Item.Value > value)
                    {
                        num2 = k;
                        value = equipment[_indices[k]].Item.Value;
                    }
                }
                int num3 = _indices[j];
                _indices[j] = _indices[num2];
                _indices[num2] = num3;
            }
            if (num > 0)
            {
                for (int l = 0; l < num; l++)
                {
                    int index = _indices[l];
                    EquipmentElement result = equipment[index];
                    if (result.Item != null && !equipment[index].Item.NotMerchandise)
                    {
                        float num4 = (float)result.Item.Value + 0.1f;
                        if (num4 > 1.3f * targetValue || num4 < 0.8f * targetValue)
                        {
                            result = GetEquipmentWithModifier(result.Item, targetValue / num4);
                            num4 = (float)result.ItemValue;
                        }
                        float num5 = targetValue / (Math.Max(targetValue, num4) * (float)(num - l));
                        if (MBRandom.RandomFloat < num5)
                        {
                            return result;
                        }
                    }
                }
            }
            return default(EquipmentElement);
        }

        // Token: 0x06002D8D RID: 11661 RVA: 0x000B5FA4 File Offset: 0x000B41A4
        public EquipmentElement GetEquipmentWithModifier(ItemObject item, float targetValueFactor)
        {
            ItemModifierGroup itemModifierGroup;
            if (!item.HasHorseComponent)
            {
                if (!item.HasArmorComponent)
                {
                    WeaponComponent weaponComponent = item.WeaponComponent;
                    itemModifierGroup = ((weaponComponent != null) ? weaponComponent.ItemModifierGroup : null);
                }
                else
                {
                    itemModifierGroup = item.ArmorComponent.ItemModifierGroup;
                }
            }
            else
            {
                itemModifierGroup = item.HorseComponent.ItemModifierGroup;
            }
            ItemModifierGroup itemModifierGroup2 = itemModifierGroup;
            ItemModifier itemModifier = null;
            if (itemModifierGroup2 != null)
            {
                itemModifier = itemModifierGroup2.GetItemModifierWithTarget(targetValueFactor);
                if (itemModifier != null)
                {
                    float num = (itemModifier.PriceMultiplier < targetValueFactor) ? (itemModifier.PriceMultiplier / targetValueFactor) : (targetValueFactor / itemModifier.PriceMultiplier);
                    if (((1f < targetValueFactor) ? (1f / targetValueFactor) : targetValueFactor) > num)
                    {
                        itemModifier = null;
                    }
                }
            }
            return new EquipmentElement(item, itemModifier);
        }

        // Token: 0x06002D8E RID: 11662 RVA: 0x000B6038 File Offset: 0x000B4238
        public override float GetPartySavePrisonerAsMemberShareProbability(PartyBase winnerParty, float lootAmount)
        {
            float result = lootAmount;
            if (winnerParty.IsMobile && winnerParty.MobileParty.IsBandit && winnerParty.MobileParty.CurrentSettlement != null 
                && winnerParty.MobileParty.CurrentSettlement.IsHideout())
            {
                result = 0f;
            }
            return result;
        }

        // Token: 0x04000F69 RID: 3945
        private static int[] _indices = new int[12];
    }
}
