using Helpers;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace KaosesTweaks.Models
{
    class BTCombatXpModel : DefaultCombatXpModel
    {
        public override void GetXpFromHit(CharacterObject attackerTroop, CharacterObject captain, CharacterObject attackedTroop, PartyBase party, int damage, bool isFatal, MissionTypeEnum missionType, out int xpAmount)
        {
            if (attackerTroop == null || attackedTroop == null)
            {
                xpAmount = 0;
                return;
            }

            int num = attackerTroop.MaxHitPoints();
            float troopPowerBasedOnContext;

            if (party != null && party.MapEvent != null)
            {
                troopPowerBasedOnContext = Campaign.Current.Models.MilitaryPowerModel.GetTroopPowerBasedOnContext(attackerTroop, party.MapEvent.EventType, party.Side, missionType == MissionTypeEnum.SimulationBattle);
            }
            else
            {
                troopPowerBasedOnContext = Campaign.Current.Models.MilitaryPowerModel.GetTroopPowerBasedOnContext(attackerTroop, MapEvent.BattleTypes.None, BattleSideEnum.None, false);
            }
            xpAmount = MathF.Round(0.4f * ((troopPowerBasedOnContext + 0.5f) * (Math.Min(damage, num) + (isFatal ? num : 0))));
            if (missionType == MissionTypeEnum.NoXp)
            {
                xpAmount = 0;
            }

            if (attackerTroop.IsHero && missionType == MissionTypeEnum.Tournament)
            {
                if (MCMSettings.Instance is { } settings && settings.TournamentHeroExperienceMultiplierEnabled)
                {
                    if (Statics._settings.TournamentDebug)
                    {
                        IM.MessageDebug("TournamentHeroXP : original : " + xpAmount.ToString() + " new: "
                            + MathF.Round(settings.TournamentHeroExperienceMultiplier * xpAmount).ToString() + "  multiplier: " + settings.TournamentHeroExperienceMultiplier.ToString());
                    }
                    xpAmount = MathF.Round(settings.TournamentHeroExperienceMultiplier * xpAmount);
                }
                else
                {
                    xpAmount = MathF.Round(xpAmount * 0.33f);
                }
            }

            else if (attackerTroop.IsHero && missionType == MissionTypeEnum.PracticeFight)
            {
                if (MCMSettings.Instance is { } settings && settings.ArenaHeroExperienceMultiplierEnabled)
                {
                    if (Statics._settings.TournamentDebug)
                    {
                        IM.MessageDebug("ArenaHeroXP : original : " + xpAmount.ToString() + " new: "
                            + MathF.Round(settings.ArenaHeroExperienceMultiplier * xpAmount).ToString() + "  multiplier: " + settings.ArenaHeroExperienceMultiplier.ToString());
                    }
                    xpAmount = MathF.Round(settings.ArenaHeroExperienceMultiplier * xpAmount);
                }
                else
                {
                    xpAmount = MathF.Round(xpAmount * 0.0625f);
                }
            }

            else if (missionType == MissionTypeEnum.Battle)
            {
                if (MCMSettings.Instance is { } settings && settings.TroopBattleExperienceMultiplierEnabled)
                {
                    if (Statics._settings.XpModifiersDebug)
                    {
                        IM.MessageDebug(" TroopBattleExperienceMultiplier Original: " + xpAmount.ToString() + " new XP amount: " + (xpAmount * settings.TroopBattleExperienceMultiplier).ToString() + "  multiplier: " + settings.TroopBattleExperienceMultiplier.ToString());
                    }
                    xpAmount = MathF.Round(xpAmount * settings.TroopBattleExperienceMultiplier);
                }
                else
                {
                    xpAmount = MathF.Round(xpAmount * 1f);
                }

            }

            else if (missionType == MissionTypeEnum.SimulationBattle)
            {
                if (MCMSettings.Instance is { } settings && settings.TroopBattleSimulationExperienceMultiplierEnabled)
                {
                    if (Statics._settings.XpModifiersDebug)
                    {
                        IM.MessageDebug("TroopBattleSimulationExperienceMultiplier original: " + xpAmount.ToString() + " new XP amount: " + (xpAmount * settings.TroopBattleSimulationExperienceMultiplier).ToString() + "  multiplier: " + settings.TroopBattleSimulationExperienceMultiplier.ToString());
                    }
                    xpAmount = MathF.Round(xpAmount * settings.TroopBattleSimulationExperienceMultiplier);
                }
                else
                {
                    xpAmount = MathF.Round(xpAmount * 0.9f);
                }

            }

            ExplainedNumber xpToGain = new(xpAmount, false, null);
            if (party != null)
            {
                if (party.IsMobile && party.MobileParty.LeaderHero != null)
                {
                    if (!attackerTroop.IsRanged && party.MobileParty.HasPerk(DefaultPerks.OneHanded.Trainer, true))
                    {
                        xpToGain.AddFactor(DefaultPerks.OneHanded.Trainer.SecondaryBonus * 0.01f, DefaultPerks.OneHanded.Trainer.Name);
                    }
                    if (attackerTroop.HasThrowingWeapon() && party.MobileParty.HasPerk(DefaultPerks.Throwing.Resourceful, true))
                    {
                        xpToGain.AddFactor(DefaultPerks.Throwing.Resourceful.SecondaryBonus * 0.01f, DefaultPerks.Throwing.Resourceful.Name);
                    }
                    if (attackerTroop.IsInfantry)
                    {
                        if (party.MobileParty.HasPerk(DefaultPerks.OneHanded.CorpsACorps, false))
                        {
                            xpToGain.AddFactor(DefaultPerks.OneHanded.CorpsACorps.PrimaryBonus * 0.01f, DefaultPerks.OneHanded.CorpsACorps.Name);
                        }
                        if (party.MobileParty.HasPerk(DefaultPerks.TwoHanded.BaptisedInBlood, true))
                        {
                            xpToGain.AddFactor(DefaultPerks.TwoHanded.BaptisedInBlood.SecondaryBonus * 0.01f, DefaultPerks.TwoHanded.BaptisedInBlood.Name);
                        }
                    }
                    if (party.MobileParty.HasPerk(DefaultPerks.OneHanded.LeadByExample, false))
                    {
                        xpToGain.AddFactor(DefaultPerks.OneHanded.LeadByExample.PrimaryBonus * 0.01f, DefaultPerks.OneHanded.LeadByExample.Name);
                    }
                    if (attackerTroop.IsRanged && party.MobileParty.HasPerk(DefaultPerks.Crossbow.MountedCrossbowman, true))
                    {
                        xpToGain.AddFactor(DefaultPerks.Crossbow.MountedCrossbowman.SecondaryBonus * 0.01f, DefaultPerks.Crossbow.MountedCrossbowman.Name);
                    }
                    if (attackerTroop.Culture.IsBandit && party.MobileParty.HasPerk(DefaultPerks.Roguery.NoRestForTheWicked, false))
                    {
                        xpToGain.AddFactor(DefaultPerks.Roguery.NoRestForTheWicked.PrimaryBonus * 0.01f, DefaultPerks.Roguery.NoRestForTheWicked.Name);
                    }
                }
                if (party.IsMobile && party.MobileParty.IsGarrison)
                {
                    Settlement currentSettlement = party.MobileParty.CurrentSettlement;
                    if (((currentSettlement != null) ? currentSettlement.Town.Governor : null) != null)
                    {
                        PerkHelper.AddPerkBonusForTown(DefaultPerks.TwoHanded.ArrowDeflection, party.MobileParty.CurrentSettlement.Town, ref xpToGain);
                        if (attackerTroop.IsMounted)
                        {
                            PerkHelper.AddPerkBonusForTown(DefaultPerks.Polearm.Guards, party.MobileParty.CurrentSettlement.Town, ref xpToGain);
                        }
                    }
                }
            }
            if (captain != null && captain.IsHero && captain.GetPerkValue(DefaultPerks.Leadership.InspiringLeader))
            {
                xpToGain.AddFactor(DefaultPerks.Leadership.InspiringLeader.SecondaryBonus, DefaultPerks.Leadership.InspiringLeader.Name);
            }
            xpAmount = MathF.Round(xpToGain.ResultNumber);
        }
    }
}
