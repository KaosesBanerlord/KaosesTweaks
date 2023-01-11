using KaosesTweaks;
using KaosesTweaks.Common;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Localization;

namespace KaosesPartySpeeds.Objects
{
    public class KaosesPartySpeed
    {
        protected MobileParty _mobileParty;
        protected bool HasModifiedSpeed = false;
        protected float ModifiedSpeed = 0.0f;
        protected TextObject Message = new TextObject(null, null);
        private bool _debug = false;
        private bool _enabled = false;
        //public KTSettings Statics._settings;

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
            _debug = Statics._settings.PartySpeedDebug;
            _enabled = Statics._settings.KaosesStaticSpeedModifiersEnabled;
            if (_debug)
            {
                // IM.MessageDebug("Debugging Party Speeds ");
            }
            calculatePartySpeed();
        }

        private void calculatePartySpeed()
        {
            if (_enabled)
            {
                if (_mobileParty.StringId.Contains("looter") && Statics._settings.LooterSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.LooterSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                    if (_debug){IM.MessageDebug("DPS: Modifying looters Speed: " + Statics._settings.LooterSpeedReductionAmount);}
                }
                if (_mobileParty.StringId.Contains("caravan") && Statics._settings.CaravanSpeedReductiontEnabled)
                {
                    if (_mobileParty.StringId.Contains("elite") && Statics._settings.EliteCaravanSpeedReductionAmount != 0.0f)
                    {
                        ModifiedSpeed = Statics._settings.EliteCaravanSpeedReductionAmount;
                        Message = _slowCaravansMessage;
                        HasModifiedSpeed = true;
                        if (_debug){IM.MessageDebug("DPS: Modifying elite caravan Speed: " + Statics._settings.EliteCaravanSpeedReductionAmount);}
                    }
                    else if (Statics._settings.CaravanSpeedReductionAmount != 0.0f)
                    {
                        ModifiedSpeed = Statics._settings.CaravanSpeedReductionAmount;
                        Message = _slowCaravansMessage;
                        HasModifiedSpeed = true;
                        if (_debug){IM.MessageDebug("DPS: Modifying caravan Speed: " + Statics._settings.CaravanSpeedReductionAmount);}
                    }
                }
                if (_mobileParty.StringId.Contains("desert") && Statics._settings.DesertSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.DesertSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                    if (_debug){IM.MessageDebug("DPS: Modifying desert bandits Speed: " + Statics._settings.DesertSpeedReductionAmount);}
                }
                if (_mobileParty.StringId.Contains("forest") && Statics._settings.ForestSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.ForestSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                    if (_debug){IM.MessageDebug("DPS: Modifying forest bandits Speed: " + Statics._settings.ForestSpeedReductionAmount);}
                }
                if (_mobileParty.StringId.Contains("mountain") && Statics._settings.MountainSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.MountainSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                    if (_debug){IM.MessageDebug("DPS: Modifying mountain bandits Speed: " + Statics._settings.MountainSpeedReductionAmount);}
                }
                if (_mobileParty.StringId.Contains("raider") && Statics._settings.SeaRaiderSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.SeaRaiderSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                    if (_debug){IM.MessageDebug("DPS: Modifying sea raider bandits Speed: " + Statics._settings.SeaRaiderSpeedReductionAmount);}
                }
                if (_mobileParty.StringId.Contains("steppe") && Statics._settings.SteppeSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.SteppeSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                    if (_debug){IM.MessageDebug("DPS: Modifying steppe bandits Speed: " + Statics._settings.SteppeSpeedReductionAmount);}
                }
                if (_mobileParty.StringId.Contains("villager") && Statics._settings.VillagerSpeedReductiontEnabled
                    && Statics._settings.VillagerSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.VillagerSpeedReductionAmount;
                    Message = _slowVillagerMessage;
                    HasModifiedSpeed = true;
                    if (_debug){IM.MessageDebug("DPS: Modifying villager Speed: " + Statics._settings.VillagerSpeedReductiontEnabled);}
                }
                if (_mobileParty.StringId.Contains("lord_") && Statics._settings.KingdomSpeedReductiontEnabled
                    && Statics._settings.KingdomSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.KingdomSpeedReductionAmount;
                    Message = _slowKingdomMessage;
                    HasModifiedSpeed = true;
                    if (_debug){IM.MessageDebug("DPS: Modifying lord_ Speed: " + Statics._settings.KingdomSpeedReductiontEnabled);}
                }
                if (_mobileParty.StringId.Contains("troops_of") && Statics._settings.OtherKingdomSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.OtherKingdomSpeedReductionAmount;
                    Message = _slowMinorMessage;
                    HasModifiedSpeed = true;
                    if (_debug){IM.MessageDebug("DPS: Modifying troops_of Speed: " + Statics._settings.OtherKingdomSpeedReductionAmount);}
                }

                if (_mobileParty.IsMainParty && Statics._settings.PlayerSpeedReductiontEnabled && Statics._settings.PlayerSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.PlayerSpeedReductionAmount;
                    Message = _slowPlayerMessage;
                    HasModifiedSpeed = true;
                    if (_debug){IM.MessageDebug("DPS: Modifying player Speed: " + Statics._settings.PlayerSpeedReductionAmount);}

                }

                if (!_mobileParty.IsMainParty && !_mobileParty.StringId.Contains("player_")
                    && !_mobileParty.StringId.Contains("militias_") && !_mobileParty.StringId.Contains("garrison_"))
                {
                    if (!HasModifiedSpeed && _mobileParty.LeaderHero != null)
                    {
                        if (Kaoses.IsPlayerClan(_mobileParty) && Statics._settings.PlayerSpeedReductiontEnabled && Statics._settings.PlayerClanSpeedReductionAmount != 0.0f)
                        {
                            ModifiedSpeed = Statics._settings.PlayerClanSpeedReductionAmount;
                            Message = _slowPlayerClanMessage;
                            HasModifiedSpeed = true;
                            if (_debug){IM.MessageDebug("DPS: Modifying PlayerClan Speed: " + Statics._settings.PlayerClanSpeedReductionAmount);}
                            //Logger.Lm("IsPlayerClan new speed:" + finalSpeed.ResultNumber.ToString());
                        }
                        else if (Statics._settings.OtherKingdomSpeedReductionAmount != 0.0f)
                        {
                            ModifiedSpeed = Statics._settings.OtherKingdomSpeedReductionAmount;
                            Message = _slowMinorMessage;
                            HasModifiedSpeed = true;
                            if (_debug){IM.MessageDebug("DPS: Modifying other Speed: " + Statics._settings.OtherKingdomSpeedReductionAmount);}
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

            //if (SubModule.FleeingParties != null && SubModule.FleeingHours != null && SubModule.FleeingSpeedReduction != null && Statics._settings != null)
            //{

            if (Statics._settings.KaosesDynamicSpeedModifiersEnabled)
            {
                if (Statics._settings.DynamicPartySpeedDebug)
                {
                    //IM.MessageDebug("Debug Dynamic party Speeds");
                }
                float reduction = 0f;
                if (mobileParty.ShortTermBehavior == AiBehavior.FleeToPoint)
                {
                    if (SubModule.FleeingParties.ContainsKey(mobileParty))
                    {
                        CampaignTime oldTime = SubModule.FleeingParties[mobileParty];
                        if (CampaignTime.Now.ToHours > oldTime.ToHours)
                        {
                            int fleeingHours = SubModule.FleeingHours[mobileParty];
                            reduction = Statics._settings.DynamicFleeingSpeedReductionAmount * fleeingHours;
                            SubModule.FleeingHours[mobileParty] = fleeingHours + 1;
                            SubModule.FleeingParties[mobileParty] = CampaignTime.HoursFromNow(Statics._settings.DynamicFleeingSpeedReductionHours);
                            SubModule.FleeingSpeedReduction[mobileParty] = reduction;
                            if (Statics._settings.DynamicPartySpeedDebug)
                            {
                                IM.MessageDebug("DDPS: Reducing party speed due to fleeing amount: " + reduction);
                            }
                        }
                        else
                        {
                            if (SubModule.FleeingSpeedReduction.ContainsKey(mobileParty))
                            {
                                reduction = SubModule.FleeingSpeedReduction[mobileParty];
                                if (Statics._settings.DynamicPartySpeedDebug)
                                {
                                    IM.MessageDebug("DDPS: Base reduction of speed for fleeing party");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (mobileParty != null)
                        {
                            int DynSpeedReduction = Statics._settings.DynamicFleeingSpeedReductionHours;
                            CampaignTime hoursFromNow = CampaignTime.HoursFromNow(DynSpeedReduction);

                            if (DynSpeedReduction == null)
                            {
                                IM.MessageDebug("DDPS: DynSpeedReduction IS NULL");
                            }
                            else
                            {
                                IM.MessageDebug("DDPS: DynSpeedReduction: " + DynSpeedReduction);
                            }
                            if (hoursFromNow == null)
                            {
                                IM.MessageDebug("DDPS: hoursFromNow IS NULL");
                            }
                            else
                            {
                                IM.MessageDebug("DDPS: hoursFromNow: " + hoursFromNow.ToString());
                            }



                            SubModule.FleeingParties.Add(mobileParty, hoursFromNow);
                            SubModule.FleeingHours.Add(mobileParty, 1);
                            float speedReductionNew = 0.0f;
                            if (SubModule.FleeingSpeedReduction.ContainsKey(mobileParty))
                            {
                                SubModule.FleeingSpeedReduction[mobileParty] = speedReductionNew;
                            }
                            else
                            {
                                if (SubModule.FleeingSpeedReduction == null)
                                {
                                    IM.MessageDebug("DDPS: SubModule.FleeingSpeedReduction DICTIONARY IS NULL WTF ");
                                }
                                if (mobileParty == null)
                                {
                                    IM.MessageDebug("DDPS: mobileParty IS NULL WTF ");
                                }

                                SubModule.FleeingSpeedReduction.Add(mobileParty, speedReductionNew);

                            }
                            if (Statics._settings.DynamicPartySpeedDebug)
                            {
                                IM.MessageDebug("DDPS: Adding new party to fleeing list");
                            }
                        }
                        else
                        {
                            IM.DebugMessage("mobile Party Is NOT VALID");
                            throw new Exception("mobile party is null");
                        }

                    }
                    if (reduction != 0)
                    {
                        finalSpeed.Add(reduction, null);
                        if (Statics._settings.DynamicPartySpeedDebug)
                        {
                            IM.MessageDebug("DDPS: Adding final reduction amount");
                        }
                    }
                }
                else
                {
                    if (SubModule.FleeingParties.ContainsKey(mobileParty))
                    {
                        CampaignTime oldTime = SubModule.FleeingParties[mobileParty];
                        if (CampaignTime.Now.ToHours > oldTime.ToHours)
                        {
                            SubModule.FleeingParties.Remove(mobileParty);
                            if (SubModule.FleeingHours.ContainsKey(mobileParty))
                            {
                                SubModule.FleeingHours.Remove(mobileParty);
                            }
                            if (SubModule.FleeingSpeedReduction.ContainsKey(mobileParty))
                            {
                                SubModule.FleeingSpeedReduction.Remove(mobileParty);
                            }
                        }
                        if (Statics._settings.DynamicPartySpeedDebug)
                        {
                            IM.MessageDebug("DDPS: Removing Fleeing party from list");
                        }
                    }
                }
            }
            //}

        }



    }
}
