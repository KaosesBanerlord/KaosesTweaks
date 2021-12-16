using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core;

namespace KaosesTweaks.Event
{
    class PlayerBattleEndEventListener
    {
        private int BanditGroupCounter { get; set; }
        private int BanditDeathCounter { get; set; }

        public PlayerBattleEndEventListener()
        {
            if (MCMSettings.Instance is { } settings)
                BanditGroupCounter = settings.GroupsOfBandits;
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
                if (MCMSettings.Instance is { } settings && (IsDefeatedBanditLike(m) && (rosterReceivingLootShare.TotalHealthyCount > 0 || !settings.PrisonersOnly)))
                {
                    BanditDeathCounter += banditSide.Casualties;
                    //IM.ColorGreenMessage("BanditDeathCounter: " + BanditDeathCounter.ToString());
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
            if (MCMSettings.Instance is { } settings)
            {
                float FinalRelationshipIncrease = settings.RelationshipIncrease;
                if (settings.SizeBonusEnabled)
                {
                    FinalRelationshipIncrease = settings.RelationshipIncrease * this.BanditDeathCounter * settings.SizeBonus;
                    if (settings.KillingBanditsDebug)
                    {
                        IM.MessageDebug("Killing Bandits: SizeBonusEnabled: " + FinalRelationshipIncrease.ToString());
                    }
                }
                int FinalRelationshipIncreaseInt = (int)Math.Floor(FinalRelationshipIncrease);
                if (settings.KillingBanditsDebug)
                {
                    IM.MessageDebug("Killing Bandits: IncreaseLocalRelations: " + "Base Change: " + settings.RelationshipIncrease.ToString() + "Final Change: " + FinalRelationshipIncreaseInt.ToString());
                }

                FinalRelationshipIncreaseInt = FinalRelationshipIncreaseInt < 1 ? 1 : FinalRelationshipIncreaseInt;
                IM.ColorGreenMessage("Final Relationship Increase: " + FinalRelationshipIncreaseInt.ToString());

                List<Settlement> list = new List<Settlement>();
                foreach (Settlement settlement in Settlement.All)
                {
                    if ((settlement.IsVillage || settlement.IsTown) && settlement.Position2D.DistanceSquared(m.Position) <= settings.Radius)
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
                IM.ColorGreenMessage("Your relationship increased with nearby notables. " + FinalRelationshipIncreaseInt.ToString());
            }
        }

        private void BanditGroupCounterUpdate()
        {
            if (MCMSettings.Instance is { } settings)
            {
                BanditGroupCounter--;
                if (BanditGroupCounter == 0)
                {
                    BanditGroupCounter = settings.GroupsOfBandits;
                }
                if (settings.KillingBanditsDebug)
                {
                    IM.MessageDebug("Killing Bandits : BanditGroupCounterUpdate: " + BanditGroupCounter.ToString());
                }
            }
        }

        private void ResetBanditDeathCounter()
        {
            this.BanditDeathCounter = 0;
        }

        private bool IsDefeatedBanditLike(MapEvent m)
        {
            if (MCMSettings.Instance is { } settings && m.GetLeaderParty(m.DefeatedSide) != null)
            {
                return
                (m.GetLeaderParty(m.DefeatedSide).MapFaction.IsBanditFaction && settings.IncludeBandits) ||
                (m.GetLeaderParty(m.DefeatedSide).MapFaction.IsOutlaw && settings.IncludeOutlaws) ||
                (m.GetLeaderParty(m.DefeatedSide).Owner.Clan.IsMafia && settings.IncludeMafia);
            }
            return false;
        }
    }
}
