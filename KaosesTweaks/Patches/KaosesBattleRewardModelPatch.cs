using HarmonyLib;
using Helpers;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Localization;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateRenownGain")]
    public class KTBattleRewardsRenownGain
    {
        public static bool PreFix(PartyBase party, float renownValueOfBattle, float contributionShare, ref ExplainedNumber result, ref float __result)
        {
            if (MCMSettings.Instance.MCMBattleRewardModifiers && MCMSettings.Instance.BattleRewardsRenownGainModifiers)
            {
                float originalRenownGain = renownValueOfBattle * contributionShare;
                float modifiedRenownGain = originalRenownGain * Statics._settings.BattleRewardsRenownGainMultiplier;
                result.Add(modifiedRenownGain, new TextObject("KT Renown Tweak", null), null);

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
                __result = result.ResultNumber;
                if (party.LeaderHero == Hero.MainHero && MCMSettings.Instance.BattleRewardShowDebug)
                {
                    IM.DebugMessage("Renown Value = " +
                                                (float)Math.Round((double)renownValueOfBattle, 2) +
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

        static bool Prepare() => MCMSettings.Instance is { } settings && (settings.MCMBattleRewardModifiers && settings.BattleRewardsRenownGainModifiers);
    }


    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateInfluenceGain")]
    public class KTBattleRewardsInfluenceGain
    {
        static bool PreFix(PartyBase party, float influenceValueOfBattle, float contributionShare, ref ExplainedNumber result, ref float __result)
        {
            if (MCMSettings.Instance.MCMBattleRewardModifiers && MCMSettings.Instance.BattleRewardsInfluenceGainModifiers)
            {

                float originalInfluenceGain = influenceValueOfBattle * contributionShare;
                float modifiedInfluenceGain = originalInfluenceGain * Statics._settings.BattleRewardsInfluenceGainMultiplier;
                //Ux.MessageDebug("Original Influence Gain : " + originalInfluenceGain.ToString() + "   Modified Gain : " + modifiedInfluenceGain.ToString()+ " Using Multiplier : " + Statics._settings.BattleRewardsInfluenceGainMultiplier.ToString());
                result.Add(party.MapFaction.IsKingdomFaction ? (modifiedInfluenceGain) : 0f, new TextObject("KT influence Tweak", null), null);
                if (party.LeaderHero == Hero.MainHero && MCMSettings.Instance.BattleRewardShowDebug)
                {
                    IM.DebugMessage("Influence Value = " +
                                                (float)Math.Round((double)influenceValueOfBattle, 2) +
                                                "| Your share = " + (float)Math.Round((double)influenceValueOfBattle * contributionShare, 2) +
                                                "(" + (float)Math.Round((double)contributionShare * 100f, 1) + "%)" +
                                                //"\nPerkBonus = " + (float)Math.Round((double)result.ResultNumber - result.BaseNumber, 2) +
                                                //"(" + (float)Math.Round((double)(result.ResultNumber / result.BaseNumber - 1f) * 100f, 1) + "%)" +
                                                //"\nSum = " + (float)Math.Round((double)result.ResultNumber, 2) +
                                                "\nBT Tweak = " + (float)Math.Round(modifiedInfluenceGain, 2) +
                                                //+ "(" + BTTweak + (float)Math.Round((double)battleRenownMultiplier * 100f, 1) + "%)" +
                                                "\n\n");
                }
                __result = result.ResultNumber;
                return false;
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && (settings.MCMBattleRewardModifiers && settings.BattleRewardsInfluenceGainModifiers);
    }


    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateMoraleGainVictory")]
    public class KTBattleRewardsMoraleGain
    {
        static bool PreFix(PartyBase party, float renownValueOfBattle, float contributionShare, ref ExplainedNumber result, ref float __result)
        {
            if (MCMSettings.Instance.MCMBattleRewardModifiers && MCMSettings.Instance.BattleRewardsMoraleGainModifiers)
            {
                float originalMoraleGain = 0.5f + renownValueOfBattle * contributionShare * 0.5f;
                float modifiedMoraleGain = originalMoraleGain * Statics._settings.BattleRewardsMoraleGainMultiplier;
                //Ux.MessageDebug("Original Morale Gain : " + originalMoraleGain.ToString() + "   Modified Gain : " + modifiedMoraleGain.ToString()+ " Using Multiplier : " + Statics._settings.BattleRewardsMoraleGainMultiplier.ToString());
                result.Add(modifiedMoraleGain, new TextObject("KT Morale Tweak", null), null);

                if (party.IsMobile && party.MobileParty.HasPerk(DefaultPerks.Throwing.LongReach, true))
                {
                    PerkHelper.AddPerkBonusForParty(DefaultPerks.Throwing.LongReach, party.MobileParty, false, ref result);
                }
                if (party.IsMobile && party.MobileParty.HasPerk(DefaultPerks.Leadership.CitizenMilitia, true))
                {
                    PerkHelper.AddPerkBonusForParty(DefaultPerks.Leadership.CitizenMilitia, party.MobileParty, false, ref result);
                }

                if (party.LeaderHero == Hero.MainHero && MCMSettings.Instance.BattleRewardShowDebug)
                {
                    IM.DebugMessage("Morale Value = " +
                                                (float)Math.Round((double)renownValueOfBattle, 2) +
                                                "| Your share = " + (float)Math.Round((double)renownValueOfBattle * contributionShare, 2) +
                                                "(" + (float)Math.Round((double)contributionShare * 100f, 1) + "%)" +
                                                //"\nPerkBonus = " + (float)Math.Round((double)result.ResultNumber - result.BaseNumber, 2) +
                                                //"(" + (float)Math.Round((double)(result.ResultNumber / result.BaseNumber - 1f) * 100f, 1) + "%)" +
                                                //"\nSum = " + (float)Math.Round((double)result.ResultNumber, 2) +
                                                "\nBT Tweak = " + (float)Math.Round(modifiedMoraleGain, 2) +
                                                //+ "(" + BTTweak + (float)Math.Round((double)battleRenownMultiplier * 100f, 1) + "%)" +
                                                "\n\n");
                }
                __result = result.ResultNumber;
                return false;
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && (settings.MCMBattleRewardModifiers && settings.BattleRewardsMoraleGainModifiers);
    }




    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateGoldLossAfterDefeat")]
    public class KTBattleRewardsGoldLoss
    {
        static bool PreFix(Hero partyLeaderHero, ref int __result)
        {
            if (MCMSettings.Instance.MCMBattleRewardModifiers && MCMSettings.Instance.BattleRewardsGoldLossModifiers)
            {
                float originalGoldLoss = (float)partyLeaderHero.Gold * 0.05f;
                if (originalGoldLoss > 10000f)
                {
                    originalGoldLoss = 10000f;
                }
                float modifiedGoldLoss = originalGoldLoss * Statics._settings.BattleRewardsGoldLossMultiplier;
                //Ux.MessageDebug("Original Gold Lost on defeat : " + originalGoldLoss.ToString() + "   Modified Gain : " + modifiedGoldLoss.ToString()+ " Using Multiplier : " + Statics._settings.BattleRewardsGoldLossMultiplier.ToString());
                if (partyLeaderHero == Hero.MainHero && MCMSettings.Instance.BattleRewardShowDebug)
                {
                    IM.DebugMessage("Gold Loss = " +
                                                (float)Math.Round((double)originalGoldLoss, 2) +
                                                "\nBT Tweak = " + (float)Math.Round(modifiedGoldLoss, 2) +
                                                "\n\n");
                }
                __result = (int)modifiedGoldLoss;
                return false;
            }
            return true;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && (settings.MCMBattleRewardModifiers && settings.BattleRewardsGoldLossModifiers);
    }


}
