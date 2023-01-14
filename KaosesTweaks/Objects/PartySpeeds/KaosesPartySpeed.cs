using KaosesCommon;
using KaosesCommon.Helpers;
using KaosesCommon.Utils;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Localization;

namespace KaosesTweaks.Objects.PartySpeeds
{

    public class KaosesPartySpeed
    {
        protected MobileParty _mobileParty;
        protected static Config? _settings;

        protected bool HasModifiedSpeed = false;
        protected float ModifiedSpeed = 0.0f;
        protected TextObject Message = new TextObject(null, null);
        private bool _debug = false;
        private bool _enabled = false;

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
            _settings = Factory.Settings;
            _debug = Factory.Settings.Debug;
            _enabled = Factory.Settings.KaosesStaticSpeedModifiersEnabled;
            //calculateNewPartySpeed();
        }

        public void CalculateNewPartySpeed(ref ExplainedNumber finalSpeed)
        {

            if (_enabled)
            {
                if (_mobileParty.StringId.Contains("looter") && Factory.Settings.LooterSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Factory.Settings.LooterSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                    if (_debug && Factory.Settings.PartySpeedDebug) { IM.MessageDebug("DPS: Modifying looters Speed: " + Factory.Settings.LooterSpeedReductionAmount); }
                }
                if (_mobileParty.StringId.Contains("caravan") && Factory.Settings.CaravanSpeedReductiontEnabled)
                {
                    if (_mobileParty.StringId.Contains("elite") && Factory.Settings.EliteCaravanSpeedReductionAmount != 0.0f)
                    {
                        ModifiedSpeed = Factory.Settings.EliteCaravanSpeedReductionAmount;
                        Message = _slowCaravansMessage;
                        HasModifiedSpeed = true;
                        if (_debug && Factory.Settings.PartySpeedDebug) { IM.MessageDebug("DPS: Modifying elite caravan Speed: " + Factory.Settings.EliteCaravanSpeedReductionAmount); }
                    }
                    else if (Factory.Settings.CaravanSpeedReductionAmount != 0.0f)
                    {
                        ModifiedSpeed = Factory.Settings.CaravanSpeedReductionAmount;
                        Message = _slowCaravansMessage;
                        HasModifiedSpeed = true;
                        if (_debug && Factory.Settings.PartySpeedDebug) { IM.MessageDebug("DPS: Modifying caravan Speed: " + Factory.Settings.CaravanSpeedReductionAmount); }
                    }
                }
                if (_mobileParty.StringId.Contains("desert") && Factory.Settings.DesertSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Factory.Settings.DesertSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                    if (_debug && Factory.Settings.PartySpeedDebug) { IM.MessageDebug("DPS: Modifying desert bandits Speed: " + Factory.Settings.DesertSpeedReductionAmount); }
                }
                if (_mobileParty.StringId.Contains("forest") && Factory.Settings.ForestSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Factory.Settings.ForestSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                    if (_debug && Factory.Settings.PartySpeedDebug) { IM.MessageDebug("DPS: Modifying forest bandits Speed: " + Factory.Settings.ForestSpeedReductionAmount); }
                }
                if (_mobileParty.StringId.Contains("mountain") && Factory.Settings.MountainSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Factory.Settings.MountainSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                    if (_debug && Factory.Settings.PartySpeedDebug) { IM.MessageDebug("DPS: Modifying mountain bandits Speed: " + Factory.Settings.MountainSpeedReductionAmount); }
                }
                if (_mobileParty.StringId.Contains("raider") && Factory.Settings.SeaRaiderSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Factory.Settings.SeaRaiderSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                    if (_debug && Factory.Settings.PartySpeedDebug) { IM.MessageDebug("DPS: Modifying sea raider bandits Speed: " + Factory.Settings.SeaRaiderSpeedReductionAmount); }
                }
                if (_mobileParty.StringId.Contains("steppe") && Factory.Settings.SteppeSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Factory.Settings.SteppeSpeedReductionAmount;
                    Message = _slowMessage;
                    HasModifiedSpeed = true;
                    if (_debug && Factory.Settings.PartySpeedDebug) { IM.MessageDebug("DPS: Modifying steppe bandits Speed: " + Factory.Settings.SteppeSpeedReductionAmount); }
                }
                if (_mobileParty.StringId.Contains("villager") && Factory.Settings.VillagerSpeedReductiontEnabled
                    && Factory.Settings.VillagerSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Factory.Settings.VillagerSpeedReductionAmount;
                    Message = _slowVillagerMessage;
                    HasModifiedSpeed = true;
                    if (_debug && Factory.Settings.PartySpeedDebug) { IM.MessageDebug("DPS: Modifying villager Speed: " + Factory.Settings.VillagerSpeedReductiontEnabled); }
                }
                if (_mobileParty.StringId.Contains("lord_") && Factory.Settings.KingdomSpeedReductiontEnabled
                    && Factory.Settings.KingdomSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Factory.Settings.KingdomSpeedReductionAmount;
                    Message = _slowKingdomMessage;
                    HasModifiedSpeed = true;
                    if (_debug && Factory.Settings.PartySpeedDebug) { IM.MessageDebug("DPS: Modifying lord_ Speed: " + Factory.Settings.KingdomSpeedReductiontEnabled); }
                }
                if (_mobileParty.StringId.Contains("troops_of") && Factory.Settings.OtherKingdomSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Factory.Settings.OtherKingdomSpeedReductionAmount;
                    Message = _slowMinorMessage;
                    HasModifiedSpeed = true;
                    if (_debug && Factory.Settings.PartySpeedDebug) { IM.MessageDebug("DPS: Modifying troops_of Speed: " + Factory.Settings.OtherKingdomSpeedReductionAmount); }
                }

                if (_mobileParty.IsMainParty && Factory.Settings.PlayerSpeedReductiontEnabled && Factory.Settings.PlayerSpeedReductionAmount != 0.0f)
                {
                    ModifiedSpeed = Factory.Settings.PlayerSpeedReductionAmount;
                    Message = _slowPlayerMessage;
                    HasModifiedSpeed = true;
                    if (_debug && Factory.Settings.PartySpeedDebug) { IM.MessageDebug("DPS: Modifying player Speed: " + Factory.Settings.PlayerSpeedReductionAmount); }

                }

                if (!_mobileParty.IsMainParty && !_mobileParty.StringId.Contains("player_")
                    && !_mobileParty.StringId.Contains("militias_") && !_mobileParty.StringId.Contains("garrison_"))
                {
                    if (!HasModifiedSpeed && _mobileParty.LeaderHero != null)
                    {
                        if (KFaction.IsPlayerClan(_mobileParty) && Factory.Settings.PlayerSpeedReductiontEnabled && Factory.Settings.PlayerClanSpeedReductionAmount != 0.0f)
                        {
                            ModifiedSpeed = Factory.Settings.PlayerClanSpeedReductionAmount;
                            Message = _slowPlayerClanMessage;
                            HasModifiedSpeed = true;
                            if (_debug && Factory.Settings.PartySpeedDebug) { IM.MessageDebug("DPS: Modifying PlayerClan Speed: " + Factory.Settings.PlayerClanSpeedReductionAmount); }
                            //Logger.Lm("IsPlayerClan new speed:" + finalSpeed.ResultNumber.ToString());
                        }
                        else if (Factory.Settings.OtherKingdomSpeedReductionAmount != 0.0f)
                        {
                            ModifiedSpeed = Factory.Settings.OtherKingdomSpeedReductionAmount;
                            Message = _slowMinorMessage;
                            HasModifiedSpeed = true;
                            if (_debug && Factory.Settings.PartySpeedDebug) { IM.MessageDebug("DPS: Modifying other Speed: " + Factory.Settings.OtherKingdomSpeedReductionAmount); }
                        }
                    }
                }

            }

            if (HasPartyModifiedSpeed())
            {
                finalSpeed.Add(ModifiedPartySpeed(), ExplainationMessage());
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

        private void DebugMessage(string message, bool additionalCheck = true)
        {
            if (_debug && additionalCheck)
            {
                IM.MessageDebug(message);
            }
        }

    }
}
