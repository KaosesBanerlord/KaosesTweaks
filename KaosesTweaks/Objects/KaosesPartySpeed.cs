using KaosesTweaks;
using KaosesTweaks.Common;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;

namespace KaosesPartySpeeds.Objects
{
    public class KaosesPartySpeed
    {
        protected MobileParty _mobileParty;
        protected bool HasModifiedSpeed = false;
        protected float ModifiedSpeed = 0.0f;
        protected TextObject Message = new TextObject(null, null);

        /* Kaoses Custom Text Explainers*/
        protected readonly TextObject _slowMessage = new TextObject("{=Kaoses1ZiDIanZ}Kaoses Bandits", null);
        protected readonly TextObject _slowPlayerMessage = new TextObject("{=Kaoses1ZiDIa2Z}Kaoses Player", null);
        protected readonly TextObject _slowPlayerClanMessage = new TextObject("{=Kaoses1ZiDIa6Z}Player Clan", null);
        protected readonly TextObject _slowMinorMessage = new TextObject("{=Kaoses1ZiDIa4Z}Kaoses Minor", null);
        protected readonly TextObject _slowKingdomMessage = new TextObject("{=Kaoses1ZiDIa3Z}Kaoses Kingdom", null);
        protected readonly TextObject _slowVillagerMessage = new TextObject("{=Kaoses1ZiDIa4Z}Kaoses Villagers", null);
        protected readonly TextObject _slowCaravansMessage = new TextObject("{=Kaoses1ZiDIa5Z}Kaoses Caravans", null);
        /* Kaoses Custom Text Explainers*/

        public KaosesPartySpeed(MobileParty mobileParty)
        {
            _mobileParty = mobileParty;
            calculatePartySpeed();
        }

        private void calculatePartySpeed()
        {
            if (MCMSettings.Instance is { } settings && settings.KaosesStaticSpeedModifiersEnabled)
            {
                if (_mobileParty.StringId.Contains("looter") && settings.LooterSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = settings.LooterSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("caravan") && settings.CaravanSpeedReductiontEnabled)
                {
                    if (_mobileParty.StringId.Contains("elite") && settings.EliteCaravanSpeedReductionAmount != 0.0f)
                    {
                        ModifiedSpeed = settings.EliteCaravanSpeedReductionAmount;
                        Message = _slowCaravansMessage;
                        HasModifiedSpeed = true;
                    }
                    else if (settings.CaravanSpeedReductionAmount != 0.0f)
                    {
                        ModifiedSpeed = settings.CaravanSpeedReductionAmount;
                        Message = _slowCaravansMessage;
                        HasModifiedSpeed = true;
                    }
                }
                if (_mobileParty.StringId.Contains("desert") && settings.DesertSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = settings.DesertSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("forest") && settings.ForestSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = settings.ForestSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("mountain") && settings.MountainSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = settings.MountainSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("raider") && settings.SeaRaiderSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = settings.SeaRaiderSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("steppe") && settings.SteppeSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = settings.SteppeSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("villager") && settings.VillagerSpeedReductiontEnabled
                    && settings.VillagerSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = settings.VillagerSpeedReductionAmount;
                    Message = _slowVillagerMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("lord_") && settings.KingdomSpeedReductiontEnabled
                    && settings.KingdomSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = settings.KingdomSpeedReductionAmount;
                    Message = _slowKingdomMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("troops_of") && settings.OtherKingdomSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = settings.OtherKingdomSpeedReductionAmount;
                    Message = _slowMinorMessage;
                    HasModifiedSpeed = true;
                }

                if (_mobileParty.IsMainParty && settings.PlayerSpeedReductiontEnabled && settings.PlayerSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = settings.PlayerSpeedReductionAmount;
                    Message = _slowPlayerMessage;
                    HasModifiedSpeed = true;

                }

                if (!_mobileParty.IsMainParty && !_mobileParty.StringId.Contains("player_")
                    && !_mobileParty.StringId.Contains("militias_") && !_mobileParty.StringId.Contains("garrison_"))
                {
                    if (!HasModifiedSpeed && _mobileParty.LeaderHero != null)
                    {
                        if (Kaoses.IsPlayerClan(_mobileParty) && settings.PlayerSpeedReductiontEnabled && settings.PlayerClanSpeedReductionAmount != 0.0f)
                        {
                            ModifiedSpeed = settings.PlayerClanSpeedReductionAmount;
                            Message = _slowPlayerClanMessage;
                            HasModifiedSpeed = true;
                            //Logger.Lm("IsPlayerClan new speed:" + finalSpeed.ResultNumber.ToString());
                        }
                        else if (settings.OtherKingdomSpeedReductionAmount != 0.0f)
                        {
                            ModifiedSpeed = settings.OtherKingdomSpeedReductionAmount;
                            Message = _slowMinorMessage;
                            HasModifiedSpeed = true;
                        }
                    }
                }

            }


        }

        public bool HasPartyModifiedSpeed()
        {
            return HasModifiedSpeed;
        }

        public float ModifiedPartySpeed()
        {
            return ModifiedSpeed;
        }

        public TextObject ExplainationMessage()
        {
            return Message;
        }


        public static void GetDynamicSpeedChange(MobileParty mobileParty, ref ExplainedNumber finalSpeed)
        {

            if (MCMSettings.Instance is { } settings && settings.KaosesDynamicSpeedModifiersEnabled)
            {
                float reduction = 0f;
                if (mobileParty.ShortTermBehavior == AiBehavior.FleeToPoint)
                {
                    if (SubModule.FleeingParties != null && SubModule.FleeingParties.ContainsKey(mobileParty))
                    {
                        CampaignTime oldTime = SubModule.FleeingParties[mobileParty];
                        if (CampaignTime.Now.ToHours > oldTime.ToHours && SubModule.FleeingHours != null && SubModule.FleeingSpeedReduction != null)
                        {
                            int fleeingHours = SubModule.FleeingHours[mobileParty];
                            reduction = settings.DynamicFleeingSpeedReductionAmount * fleeingHours;
                            SubModule.FleeingHours[mobileParty] = fleeingHours + 1;
                            SubModule.FleeingParties[mobileParty] = CampaignTime.HoursFromNow(settings.DynamicFleeingSpeedReductionHours);
                            SubModule.FleeingSpeedReduction[mobileParty] = reduction;
                        }
                        else
                        {
                            if (SubModule.FleeingSpeedReduction != null && SubModule.FleeingSpeedReduction.ContainsKey(mobileParty))
                            {
                                reduction = SubModule.FleeingSpeedReduction[mobileParty];
                            }
                        }
                    }
                    else
                    {
                        if (SubModule.FleeingParties != null && SubModule.FleeingHours != null && SubModule.FleeingSpeedReduction != null)
                        {
                            SubModule.FleeingParties.Add(mobileParty, CampaignTime.HoursFromNow(settings.DynamicFleeingSpeedReductionHours));
                            SubModule.FleeingHours.Add(mobileParty, 1);
                            SubModule.FleeingSpeedReduction.Add(mobileParty, 0.0f);
                        }
                    }
                    if (reduction != 0)
                    {
                        finalSpeed.Add(reduction, null);
                    }
                }
                else
                {
                    if (SubModule.FleeingParties != null && SubModule.FleeingParties.ContainsKey(mobileParty))
                    {
                        CampaignTime oldTime = SubModule.FleeingParties[mobileParty];
                        if (CampaignTime.Now.ToHours > oldTime.ToHours)
                        {
                            SubModule.FleeingParties.Remove(mobileParty);
                            if (SubModule.FleeingHours != null && SubModule.FleeingHours.ContainsKey(mobileParty))
                            {
                                SubModule.FleeingHours.Remove(mobileParty);
                            }
                            if (SubModule.FleeingSpeedReduction != null && SubModule.FleeingSpeedReduction.ContainsKey(mobileParty))
                            {
                                SubModule.FleeingSpeedReduction.Remove(mobileParty);
                            }
                        }
                    }
                }
            }
        }



    }
}
