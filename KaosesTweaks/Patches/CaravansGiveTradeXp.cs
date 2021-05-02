using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment.Managers;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace KaosesTweaks.Patches
{
    class CaravansGiveTradeXp
    {
        // Token: 0x02000002 RID: 2
        [HarmonyPatch(typeof(DefaultClanFinanceModel), "AddIncomeFromParties")]
        internal class CaravansGiveTradeXpPatch
        {
            // Token: 0x06000001 RID: 1 RVA: 0x00002048 File Offset: 0x00000248
            private static void Prefix(Clan clan, ref ExplainedNumber goldChange, bool applyWithdrawals = false)
            {
/*
                foreach (MobileParty mobileParty in clan.AllParties)
                {
                    if (mobileParty.IsActive && mobileParty.IsCaravan)
                    {
                        int num = (int)((double)(mobileParty.PartyTradeGold - 10000) / 10.0);
                        if (num > 0 && applyWithdrawals)
                        {
                            SkillLevelingManager.OnTradeProfitMade(clan.Leader, num);
                            if (mobileParty.LeaderHero != null)
                            {
                                SkillLevelingManager.OnTradeProfitMade(mobileParty.LeaderHero, num / 2);
                            }
                        }
                    }
                }*/
            }
        }




/*
        // Token: 0x02000002 RID: 2
        internal class CaravanTradeXPBehaviour : CampaignBehaviorBase
        {
            // Token: 0x06000001 RID: 1 RVA: 0x00002048 File Offset: 0x00000248
            public override void RegisterEvents()
            {
                CampaignEvents.OnCaravanTransactionCompletedEvent.AddNonSerializedListener(this, new Action<MobileParty, Town, List<ValueTuple<ItemObject, int>>>(this.onCaravanTransactionCompleted));
            }

            // Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
            private void onCaravanTransactionCompleted(MobileParty sellerparty, Town transactiontown, List<ValueTuple<ItemObject, int>> merchandize)
            {
                if (sellerparty.IsCaravan)
                {
                    int sumofsolditems = 0;
                    foreach (ValueTuple<ItemObject, int> tuple in merchandize)
                    {
                        sumofsolditems += tuple.Item1.Value;
                    }
                    if (sellerparty.Leader.HeroObject != null)
                    {
                        sellerparty.Leader.HeroObject.AddSkillXp(DefaultSkills.Trade, (float)sumofsolditems * 0.3f);
                        if (sellerparty.Leader.HeroObject.Clan == Hero.MainHero.Clan)
                        {
                            Hero.MainHero.AddSkillXp(DefaultSkills.Trade, (float)sumofsolditems * 0.3f);
                        }
                    }
                }
            }

            // Token: 0x06000003 RID: 3 RVA: 0x00002124 File Offset: 0x00000324
            public override void SyncData(IDataStore dataStore)
            {
            }
        }*/




        /*
        namespace TaleWorlds.CampaignSystem.SandBox.GameComponents
            {
                // Token: 0x020002CE RID: 718
                public class DefaultClanFinanceModel : ClanFinanceModel
                {*/
/*

        // Token: 0x06002BED RID: 11245 RVA: 0x000A9594 File Offset: 0x000A7794
        private void AddIncomeFromParties(Clan clan, ref ExplainedNumber goldChange, bool applyWithdrawals = false)
        {
            foreach (Hero hero in clan.Lords)
            {
                foreach (CaravanPartyComponent caravanPartyComponent in hero.OwnedCaravans)
                {
                    this.AddIncomeFromParty(caravanPartyComponent.MobileParty, clan, ref goldChange, applyWithdrawals);
                }
            }
            foreach (Hero hero2 in clan.Companions)
            {
                foreach (CaravanPartyComponent caravanPartyComponent2 in hero2.OwnedCaravans)
                {
                    this.AddIncomeFromParty(caravanPartyComponent2.MobileParty, clan, ref goldChange, applyWithdrawals);
                }
            }
            foreach (WarPartyComponent warPartyComponent in clan.WarPartyComponents)
            {
                this.AddIncomeFromParty(warPartyComponent.MobileParty, clan, ref goldChange, applyWithdrawals);
            }
        }

        // Token: 0x06002BEE RID: 11246 RVA: 0x000A96F8 File Offset: 0x000A78F8
        private void AddIncomeFromParty(MobileParty party, Clan clan, ref ExplainedNumber goldChange, bool applyWithdrawals)
        {
            if (party.IsActive && party.LeaderHero != clan.Leader && (party.IsLordParty || party.IsGarrison || party.IsCaravan))
            {
                int num = (party.IsLordParty && party.LeaderHero != null) ? party.LeaderHero.Gold : party.PartyTradeGold;
                if (num > 10000)
                {
                    int num2 = (num - 10000) / 10;
                    goldChange.Add((float)num2, party.IsCaravan ? DefaultClanFinanceModel._caravanIncomeStr : DefaultClanFinanceModel._partyIncomeStr, (party.IsCaravan && party.LeaderHero != null) ? party.LeaderHero.Name : party.Name);
                    if (applyWithdrawals)
                    {
                        this.RemovePartyGold(party, num2);
                        if (party.LeaderHero != null && num2 > 0)
                        {
                            SkillLevelingManager.OnTradeProfitMade(party.LeaderHero, num2);
                        }
                        Hero owner = party.Party.Owner;
                        bool flag;
                        if (owner == null)
                        {
                            flag = (null != null);
                        }
                        else
                        {
                            Clan clan2 = owner.Clan;
                            flag = (((clan2 != null) ? clan2.Leader : null) != null);
                        }
                        if (flag && party.IsCaravan && party.Party.Owner.Clan.Leader.GetPerkValue(DefaultPerks.Trade.GreatInvestor))
                        {
                            party.Party.Owner.Clan.AddRenown(DefaultPerks.Trade.GreatInvestor.PrimaryBonus, true);
                        }
                    }
                }
            }
        }*/











    }

}
