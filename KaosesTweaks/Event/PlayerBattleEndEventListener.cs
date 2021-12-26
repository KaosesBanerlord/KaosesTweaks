using KaosesTweaks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace KaosesTweaks.Event
{
    class PlayerBattleEndEventListener
    {
        private int BanditGroupCounter { get; set; }
        private int BanditDeathCounter { get; set; }

        public PlayerBattleEndEventListener()
        {
            BanditGroupCounter = Statics._settings.GroupsOfBandits;
            BanditDeathCounter = 0;
            Logging.Lm("Killing Bandits : PlayerBattleEndEventListener Called" + "");
        }

        public void IncreaseLocalRelationsAfterBanditFight(MapEvent m)
        {
            Logging.Lm("Killing Bandits : IncreaseLocalRelationsAfterBanditFight Called" + "");
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
                if (IsDefeatedBanditLike(m) && (rosterReceivingLootShare.TotalHealthyCount > 0 || !Statics._settings.PrisonersOnly))
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
            float FinalRelationshipIncrease = Statics._settings.RelationshipIncrease;
            if (Statics._settings.SizeBonusEnabled)
            {
                FinalRelationshipIncrease = Statics._settings.RelationshipIncrease * BanditDeathCounter * Statics._settings.SizeBonus;
                if (Statics._settings.KillingBanditsDebug)
                {
                    IM.MessageDebug("Killing Bandits: SizeBonusEnabled: " + FinalRelationshipIncrease.ToString());
                }
            }
            int FinalRelationshipIncreaseInt = (int)Math.Floor(FinalRelationshipIncrease);
            if (Statics._settings.KillingBanditsDebug)
            {
                IM.MessageDebug("Killing Bandits: IncreaseLocalRelations: " + "Base Change: " + Statics._settings.RelationshipIncrease.ToString() + "Final Change: " + FinalRelationshipIncreaseInt.ToString());
            }
            FinalRelationshipIncreaseInt = FinalRelationshipIncreaseInt < 1 ? 1 : FinalRelationshipIncreaseInt;
            if (Statics._settings.KillingBanditsRelationReportEnabled)
                IM.ColorGreenMessage("Final Relationship Increase: " + FinalRelationshipIncreaseInt.ToString());

            List<Settlement> list = new List<Settlement>();
            foreach (Settlement settlement in Settlement.All)
            {
                if ((settlement.IsVillage || settlement.IsTown) && settlement.Position2D.DistanceSquared(m.Position) <= Statics._settings.Radius)
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
            if (Statics._settings.KillingBanditsRelationReportEnabled)
                IM.ColorGreenMessage("Your relationship increased with nearby notables. " + FinalRelationshipIncreaseInt.ToString());
        }

        private void BanditGroupCounterUpdate()
        {
            BanditGroupCounter--;
            if (BanditGroupCounter == 0)
            {
                BanditGroupCounter = Statics._settings.GroupsOfBandits;
            }
            if (Statics._settings.KillingBanditsDebug)
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
                if (m.GetLeaderParty(m.DefeatedSide).MapFaction.IsBanditFaction && Statics._settings.IncludeBandits)
                {
                    return true;
                }

                if (m.GetLeaderParty(m.DefeatedSide).MapFaction.IsOutlaw && Statics._settings.IncludeOutlaws)
                {
                    return true;
                }

                if (m.GetLeaderParty(m.DefeatedSide).Owner.Clan.IsMafia && Statics._settings.IncludeMafia)
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
