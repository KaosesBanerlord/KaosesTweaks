using KaosesTweaks;
using KaosesTweaks.Common;
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
            if (Statics._settings.KaosesStaticSpeedModifiersEnabled)
            {
                if (_mobileParty.StringId.Contains("looter") && Statics._settings.LooterSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.LooterSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("caravan") && Statics._settings.CaravanSpeedReductiontEnabled)
                {
                    if (_mobileParty.StringId.Contains("elite") && Statics._settings.EliteCaravanSpeedReductionAmount != 0.0f)
                    {
                        ModifiedSpeed = Statics._settings.EliteCaravanSpeedReductionAmount;
                        Message = _slowCaravansMessage;
                        HasModifiedSpeed = true;
                    }
                    else if (Statics._settings.CaravanSpeedReductionAmount != 0.0f)
                    {
                        ModifiedSpeed = Statics._settings.CaravanSpeedReductionAmount;
                        Message = _slowCaravansMessage;
                        HasModifiedSpeed = true;
                    }
                }
                if (_mobileParty.StringId.Contains("desert") && Statics._settings.DesertSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.DesertSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("forest") && Statics._settings.ForestSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.ForestSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("mountain") && Statics._settings.MountainSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.MountainSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("raider") && Statics._settings.SeaRaiderSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.SeaRaiderSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("steppe") && Statics._settings.SteppeSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.SteppeSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("villager") && Statics._settings.VillagerSpeedReductiontEnabled
                    && Statics._settings.VillagerSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.VillagerSpeedReductionAmount;
                    Message = _slowVillagerMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("lord_") && Statics._settings.KingdomSpeedReductiontEnabled
                    && Statics._settings.KingdomSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.KingdomSpeedReductionAmount;
                    Message = _slowKingdomMessage;
                    HasModifiedSpeed = true;
                }
                if (_mobileParty.StringId.Contains("troops_of") && Statics._settings.OtherKingdomSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.OtherKingdomSpeedReductionAmount;
                    Message = _slowMinorMessage;
                    HasModifiedSpeed = true;
                }

                if (_mobileParty.IsMainParty && Statics._settings.PlayerSpeedReductiontEnabled && Statics._settings.PlayerSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Statics._settings.PlayerSpeedReductionAmount;
                    Message = _slowPlayerMessage;
                    HasModifiedSpeed = true;

                }

                if (!_mobileParty.IsMainParty && !_mobileParty.StringId.Contains("player_")
                    && !_mobileParty.StringId.Contains("militias_") && !_mobileParty.StringId.Contains("garrison_"))
                {
                    if (!HasModifiedSpeed && !_mobileParty.IsLeaderless)
                    {
                        if (Kaoses.IsPlayerClan(_mobileParty) && Statics._settings.PlayerSpeedReductiontEnabled && Statics._settings.PlayerClanSpeedReductionAmount != 0.0f)
                        {
                            ModifiedSpeed = Statics._settings.PlayerClanSpeedReductionAmount;
                            Message = _slowPlayerClanMessage;
                            HasModifiedSpeed = true;
                            //Logger.Lm("IsPlayerClan new speed:" + finalSpeed.ResultNumber.ToString());
                        }
                        else if (Statics._settings.OtherKingdomSpeedReductionAmount != 0.0f)
                        {
                            ModifiedSpeed = Statics._settings.OtherKingdomSpeedReductionAmount;
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
    }
}