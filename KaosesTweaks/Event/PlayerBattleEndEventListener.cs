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
            this.BanditGroupCounter = Statics._settings.GroupsOfBandits;
            this.BanditDeathCounter = 0;
        }

        public void IncreaseLocalRelationsAfterBanditFight(MapEvent m)
        {
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
                    InformationManager.DisplayMessage(new InformationMessage("BanditDeathCounter: " + BanditDeathCounter.ToString(), Color.FromUint(4282569842U)));
                    if (this.BanditGroupCounter == 1)
                    {
                        IncreaseLocalRelations(m);
                        this.ResetBanditDeathCounter();
                    }
                    this.BanditGroupCounterUpdate();
                }
            }
        }

        private void IncreaseLocalRelations(MapEvent m)
        {
            float FinalRelationshipIncrease = Statics._settings.RelationshipIncrease;
            if (Statics._settings.SizeBonusEnabled)
            {
                FinalRelationshipIncrease = Statics._settings.RelationshipIncrease * this.BanditDeathCounter * Statics._settings.SizeBonus;
                
                //IM.MessageDebug("Killing Bandits: SizeBonusEnabled: " + FinalRelationshipIncrease.ToString());
            }
            int FinalRelationshipIncreaseInt = (int)Math.Floor(FinalRelationshipIncrease);
            FinalRelationshipIncreaseInt = FinalRelationshipIncreaseInt < 1 ? 1 : FinalRelationshipIncreaseInt;
            InformationManager.DisplayMessage(new InformationMessage("Final Relationship Increase: " + FinalRelationshipIncreaseInt.ToString(), Color.FromUint(4282569842U)));

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
            InformationManager.DisplayMessage(new InformationMessage("Your relationship increased with nearby notables.", Color.FromUint(4282569842U)));
        }

        private void BanditGroupCounterUpdate()
        {
            this.BanditGroupCounter--;
            if (this.BanditGroupCounter == 0)
            {
                this.BanditGroupCounter = Statics._settings.GroupsOfBandits;
            }
        }

        private void ResetBanditDeathCounter()
        {
            this.BanditDeathCounter = 0;
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
