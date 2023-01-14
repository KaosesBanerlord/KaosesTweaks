using KaosesCommon.Utils;
using KaosesTweaks.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;


namespace KaosesTweaks.Event
{

    class PlayerBattleEndEventListener
    {
        private int BanditGroupCounter { get; set; }
        private int BanditDeathCounter { get; set; }

        public PlayerBattleEndEventListener()
        {
            BanditGroupCounter = Factory.Settings.GroupsOfBandits;
            BanditDeathCounter = 0;
            Logger.Lm("Killing Bandits : PlayerBattleEndEventListener Called" + "");
        }

        public void IncreaseLocalRelationsAfterBanditFight(MapEvent m)
        {
            Logger.Lm("Killing Bandits : IncreaseLocalRelationsAfterBanditFight Called" + "");
            TroopRoster rosterReceivingLootShare;
            int mainPartSideInt = (int)PartyBase.MainParty.Side;
            rosterReceivingLootShare = PlayerEncounter.Current.RosterToReceiveLootMembers;
            //PartyBase partyReceivingLootShare = m.GetPartyReceivingLootShare(PartyBase.MainParty);

            MapEventSide banditSide;

            if (m.DefeatedSide == BattleSideEnum.Attacker)
            {
                banditSide = m.AttackerSide;
            }
            else
            {
                banditSide = m.DefenderSide;
            }
            if (!((int)m.DefeatedSide == -1 || (int)m.DefeatedSide == 2))
            {
                if (IsDefeatedBanditLike(m) && (rosterReceivingLootShare.TotalHealthyCount > 0 || !Factory.Settings.PrisonersOnly))
                {
                    BanditDeathCounter += banditSide.Casualties;
                    //IM.ColorGreenMessage("BanditDeathCounter: " + BanditDeathCounter.ToString());
                    if (BanditGroupCounter == 1)
                    {
                        IncreaseLocalRelations(m);
                        ResetBanditDeathCounter();
                    }
                    BanditGroupCounterUpdate();
                }
            }
        }

        private void IncreaseLocalRelations(MapEvent m)
        {
            float FinalRelationshipIncrease = Factory.Settings.RelationshipIncrease;
            if (Factory.Settings.SizeBonusEnabled)
            {
                FinalRelationshipIncrease = Factory.Settings.RelationshipIncrease * BanditDeathCounter * Factory.Settings.SizeBonus;
                if (Factory.Settings.KillingBanditsDebug)
                {
                    IM.MessageDebug("Killing Bandits: SizeBonusEnabled: " + FinalRelationshipIncrease.ToString());
                }
            }
            int FinalRelationshipIncreaseInt = (int)Math.Floor(FinalRelationshipIncrease);
            if (Factory.Settings.KillingBanditsDebug)
            {
                IM.MessageDebug("Killing Bandits: IncreaseLocalRelations: " + "Base Change: " + Factory.Settings.RelationshipIncrease.ToString() + "Final Change: " + FinalRelationshipIncreaseInt.ToString());
            }
            FinalRelationshipIncreaseInt = FinalRelationshipIncreaseInt < 1 ? 1 : FinalRelationshipIncreaseInt;
            if (Factory.Settings.KillingBanditsRelationReportEnabled)
                IM.MessageGreen("Final Relationship Increase: " + FinalRelationshipIncreaseInt.ToString());

            List<Settlement> list = new List<Settlement>();
            foreach (Settlement settlement in Settlement.All)
            {
                if ((settlement.IsVillage || settlement.IsTown) && settlement.Position2D.DistanceSquared(m.Position) <= Factory.Settings.Radius)
                {
                    list.Add(settlement);
                }
            }
            foreach (Settlement settlement2 in list)
            {
                if (settlement2.Notables.Any<Hero>())
                {
                    Hero h = settlement2.Notables.GetRandomElement<Hero>();
                    ChangeRelationAction.ApplyPlayerRelation(h, relation: FinalRelationshipIncreaseInt, affectRelatives: true, showQuickNotification: false);
                }
            }
            if (Factory.Settings.KillingBanditsRelationReportEnabled)
                IM.MessageGreen("Your relationship increased with nearby notables. " + FinalRelationshipIncreaseInt.ToString());
        }

        private void BanditGroupCounterUpdate()
        {
            BanditGroupCounter--;
            if (BanditGroupCounter == 0)
            {
                BanditGroupCounter = Factory.Settings.GroupsOfBandits;
            }
            if (Factory.Settings.KillingBanditsDebug)
            {
                IM.MessageDebug("Killing Bandits : BanditGroupCounterUpdate: " + BanditGroupCounter.ToString());
            }
        }

        private void ResetBanditDeathCounter()
        {
            BanditDeathCounter = 0;
        }

        private bool IsDefeatedBanditLike(MapEvent m)
        {
            try
            {
                if (m.GetLeaderParty(m.DefeatedSide).MapFaction.IsBanditFaction && Factory.Settings.IncludeBandits)
                {
                    return true;
                }

                if (m.GetLeaderParty(m.DefeatedSide).MapFaction.IsOutlaw && Factory.Settings.IncludeOutlaws)
                {
                    return true;
                }

                if (m.GetLeaderParty(m.DefeatedSide).Owner.Clan.IsMafia && Factory.Settings.IncludeMafia)
                {
                    return true;
                }
            }

            catch (Exception ex)
            {
                //Avoids crash for parties without an owner set	
            }
            return false;
        }
    }
}
