using HarmonyLib;
using Helpers;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace KaosesTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateRenownGain")]
    class KTBattleRewardsRenownGainPatch
    {
        private static bool Prefix(PartyBase party, float renownValueOfBattle, float contributionShare, ref ExplainedNumber __result)
        {
            if (MCMSettings.Instance.BattleRewardsRenownGainModifiers)
            {
                float originalRenownGain = renownValueOfBattle * contributionShare;
                float modifiedRenownGain = originalRenownGain * Statics._settings.BattleRewardsRenownGainMultiplier;
                __result = new ExplainedNumber(modifiedRenownGain, true, new TextObject("KT Renown Tweak", null));
                if (party.IsMobile)
                {
                    if (party.MobileParty.HasPerk(DefaultPerks.Charm.Warlord, false))
                    {
                        PerkHelper.AddPerkBonusForParty(DefaultPerks.Charm.Warlord, party.MobileParty, true, ref __result);
                    }
                    if (party.MobileParty.HasPerk(DefaultPerks.Throwing.LongReach, true))
                    {
                        PerkHelper.AddPerkBonusForParty(DefaultPerks.Throwing.LongReach, party.MobileParty, false, ref __result);
                    }
                    PerkObject famousCommander = DefaultPerks.Leadership.FamousCommander;
                    MobileParty mobileParty = party.MobileParty;
                    PerkHelper.AddPerkBonusForCharacter(famousCommander, (mobileParty != null) ? mobileParty.LeaderHero.CharacterObject : null, true, ref __result);
                }
                if (party.LeaderHero == Hero.MainHero && MCMSettings.Instance.BattleRewardShowDebug)
                {
                    IM.DebugMessage("Harmony Patch Renown Value = " +
                                                (float)Math.Round(renownValueOfBattle, 2) +
                                                "| Your share = " + (float)Math.Round((double)renownValueOfBattle * contributionShare, 2) +
                                                "(" + (float)Math.Round((double)contributionShare * 100f, 1) + "%)" +
                                                //"\nPerkBonus = " + (float)Math.Round((double)result.ResultNumber - result.BaseNumber, 2) +
                                                //"(" + (float)Math.Round((double)(result.ResultNumber / result.BaseNumber - 1f) * 100f, 1) + "%)" +
                                                //"\nSum = " + (float)Math.Round((double)result.ResultNumber, 2) +
                                                "\nBT Tweak = " + (float)Math.Round(modifiedRenownGain, 2) +
                                                //+ "(" + BTTweak + (float)Math.Round((double)battleRenownMultiplier * 100f, 1) + "%)" +
                                                "\n\n");
                }
                return false;
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.MCMBattleRewardModifiers && settings.BattleRewardModifiersPatchOnly && settings.BattleRewardsRenownGainModifiers;
    }

    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateInfluenceGain")]
    class KTBattleRewardsInfluenceGainPatch
    {
        private static bool Prefix(PartyBase party, float influenceValueOfBattle, float contributionShare, ref ExplainedNumber __result)
        {
            if (MCMSettings.Instance.BattleRewardsInfluenceGainModifiers)
            {
                //~ KT
                float originalInfluenceGain = influenceValueOfBattle * contributionShare;
                float modifiedInfluenceGain = originalInfluenceGain * Statics._settings.BattleRewardsInfluenceGainMultiplier;
                __result = new ExplainedNumber(party.MapFaction.IsKingdomFaction ? modifiedInfluenceGain : 0f, true, new TextObject("KT influence Tweak", null));
                //~ KT

                if (party.LeaderHero == Hero.MainHero && MCMSettings.Instance.BattleRewardShowDebug)
                {
                    IM.DebugMessage("Harmony Patch Influence Value = " +
                                                (float)Math.Round(influenceValueOfBattle, 2) +
                                                "| Your share = " + (float)Math.Round((double)influenceValueOfBattle * contributionShare, 2) +
                                                "(" + (float)Math.Round((double)contributionShare * 100f, 1) + "%)" +
                                                //"\nPerkBonus = " + (float)Math.Round((double)result.ResultNumber - result.BaseNumber, 2) +
                                                //"(" + (float)Math.Round((double)(result.ResultNumber / result.BaseNumber - 1f) * 100f, 1) + "%)" +
                                                //"\nSum = " + (float)Math.Round((double)result.ResultNumber, 2) +
                                                "\nBT Tweak = " + (float)Math.Round(modifiedInfluenceGain, 2) +
                                                //+ "(" + BTTweak + (float)Math.Round((double)battleRenownMultiplier * 100f, 1) + "%)" +
                                                "\n\n");
                }
                return false;
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.MCMBattleRewardModifiers && settings.BattleRewardModifiersPatchOnly && settings.BattleRewardsInfluenceGainModifiers;
    }

    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateMoraleGainVictory")]
    class KTBattleRewardsMoraleGainPatch
    {
        private static bool Prefix(PartyBase party, float renownValueOfBattle, float contributionShare, ref ExplainedNumber __result)
        {
            if (MCMSettings.Instance.BattleRewardsMoraleGainModifiers)
            {
                float originalMoraleGain = 0.5f + renownValueOfBattle * contributionShare * 0.5f;
                float modifiedMoraleGain = originalMoraleGain * Statics._settings.BattleRewardsMoraleGainMultiplier;
                __result = new ExplainedNumber(modifiedMoraleGain, true, new TextObject("KT Morale Tweak", null));

                if (party.IsMobile && party.MobileParty.HasPerk(DefaultPerks.Throwing.LongReach, true))
                {
                    PerkHelper.AddPerkBonusForParty(DefaultPerks.Throwing.LongReach, party.MobileParty, false, ref __result);
                }
                if (party.IsMobile && party.MobileParty.HasPerk(DefaultPerks.Leadership.CitizenMilitia, true))
                {
                    PerkHelper.AddPerkBonusForParty(DefaultPerks.Leadership.CitizenMilitia, party.MobileParty, false, ref __result);
                }

                if (party.LeaderHero == Hero.MainHero && MCMSettings.Instance.BattleRewardShowDebug)
                {
                    IM.DebugMessage("Harmony Patch Morale Value = " +
                                                (float)Math.Round(renownValueOfBattle, 2) +
                                                "| Your share = " + (float)Math.Round((double)renownValueOfBattle * contributionShare, 2) +
                                                "(" + (float)Math.Round((double)contributionShare * 100f, 1) + "%)" +
                                                //"\nPerkBonus = " + (float)Math.Round((double)result.ResultNumber - result.BaseNumber, 2) +
                                                //"(" + (float)Math.Round((double)(result.ResultNumber / result.BaseNumber - 1f) * 100f, 1) + "%)" +
                                                //"\nSum = " + (float)Math.Round((double)result.ResultNumber, 2) +
                                                "\nBT Tweak = " + (float)Math.Round(modifiedMoraleGain, 2) +
                                                //+ "(" + BTTweak + (float)Math.Round((double)battleRenownMultiplier * 100f, 1) + "%)" +
                                                "\n\n");
                }
                return false;
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.MCMBattleRewardModifiers && settings.BattleRewardModifiersPatchOnly && settings.BattleRewardsMoraleGainModifiers;
    }

    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateGoldLossAfterDefeat")]
    class KTBattleRewardsGoldLossPatch
    {
        private static bool Prefix(Hero partyLeaderHero, ref int __result)
        {
            if (MCMSettings.Instance.MCMBattleRewardModifiers && MCMSettings.Instance.BattleRewardsGoldLossModifiers)
            {
                float originalGoldLoss = partyLeaderHero.Gold * 0.05f;
                if (originalGoldLoss > 10000f)
                {
                    originalGoldLoss = 10000f;
                }
                float modifiedGoldLoss = originalGoldLoss * Statics._settings.BattleRewardsGoldLossMultiplier;

                if (partyLeaderHero == Hero.MainHero && MCMSettings.Instance.BattleRewardShowDebug)
                {
                    IM.DebugMessage("Harmony Patch Gold Loss = " +
                                                (float)Math.Round(originalGoldLoss, 2) +
                                                "\nBT Tweak = " + (float)Math.Round(modifiedGoldLoss, 2) +
                                                "\n\n");
                }
                __result = (int)modifiedGoldLoss;
                return false;
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.MCMBattleRewardModifiers && settings.BattleRewardModifiersPatchOnly && settings.BattleRewardsGoldLossModifiers;
    }

    [HarmonyPatch(typeof(DefaultBattleRewardModel), "GetPlayerGainedRelationAmount")]
    class KTBattleRewardsGainedRelationAmountPatch
    {
        private static bool Prefix(MapEvent mapEvent, Hero hero, ref int __result)
        {
            if (MCMSettings.Instance.BattleRewardsRelationShipGainModifiers)
            {
                MapEventSide mapEventSide = mapEvent.AttackerSide.IsMainPartyAmongParties() ? mapEvent.AttackerSide : mapEvent.DefenderSide;
                float playerPartyContributionRate = mapEventSide.GetPlayerPartyContributionRate();
                float num = (mapEvent.StrengthOfSide[(int)PartyBase.MainParty.Side] - PlayerEncounter.Current.PlayerPartyInitialStrength) / mapEvent.StrengthOfSide[(int)PartyBase.MainParty.OpponentSide];
                float num2 = (num < 1f) ? (1f + (1f - num)) : ((num < 3f) ? (0.5f * (3f - num)) : 0f);
                float renownValue = mapEvent.GetRenownValue((mapEventSide == mapEvent.AttackerSide) ? BattleSideEnum.Attacker : BattleSideEnum.Defender);
                //~ KT
                double relationShipGain = GetPlayerGainedRelationAmount(0.75 + Math.Pow(playerPartyContributionRate * 1.3f * (num2 + renownValue), 0.6700000166893005));
                __result = (int)relationShipGain;
                //~ KT

                return false;
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.MCMBattleRewardModifiers && settings.BattleRewardModifiersPatchOnly && settings.BattleRewardsRelationShipGainModifiers;

        //~ KT
        public static int GetPlayerGainedRelationAmount(double relationShipGain)
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


    }




}
