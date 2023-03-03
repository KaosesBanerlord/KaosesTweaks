using KaosesCommon.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Localization;

namespace KaosesTweaks.Objects.PartySpeeds
{
    public class KaosesFleeingPartySpeed
    {
        public string _mobilePartyId = "";
        private CampaignTime _nextTimeApplyReduction;
        private int _hoursFleeing = 0;
        private float _speedReduction = 0.0f;
        private int _hoursBeforeReduction = 0;
        private bool _enabled = false;
        private bool _debug = false;
        private bool _debugFleeing = false;
        private readonly TextObject _slowMessage = new TextObject("{=ExhustionMessage}Exhustion penalty", null);

        public KaosesFleeingPartySpeed(string mobilePartyStringId)
        {
            _mobilePartyId = mobilePartyStringId;
            _speedReduction = Factory.Settings.DynamicFleeingSpeedReductionAmount;
            _hoursBeforeReduction = Factory.Settings.DynamicFleeingSpeedReductionHours;
            _enabled = Factory.Settings.KaosesDynamicSpeedModifiersEnabled;
            _nextTimeApplyReduction = CampaignTime.HoursFromNow(_hoursBeforeReduction);
            _debug = Factory.Settings.IsDebug;
            _debugFleeing = Factory.Settings.IsDebugDynamic;
        }

        public void CheckPartyToApplyFleeingPenalties(ref ExplainedNumber finalSpeed)//AiBehavior shortTermBehaviors
        {
            float reductionAmount = 0.0f;
            if (!_enabled) { return; }
            //DebugMessage("KFP: Debug Kaoses Fleeing Party Speeds");
            if (CheckShouldApplySpeedRecution())
            {
                reductionAmount = _speedReduction * _hoursFleeing;
                DebugMessage("KFP: Reducing " + _mobilePartyId + " Hours: " + _hoursFleeing + " baseReduction: " + _speedReduction + "  Final Reduction: " + reductionAmount);
            }
            finalSpeed.Add(reductionAmount, _slowMessage);
            //return reductionAmount;
        }

        private bool CheckShouldApplySpeedRecution()
        {
            bool value = false;
            if (CampaignTime.Now.ToHours > _nextTimeApplyReduction.ToHours)
            {
                value = true;
                _nextTimeApplyReduction = CampaignTime.HoursFromNow(_hoursBeforeReduction);
                _hoursFleeing = _hoursFleeing + _hoursBeforeReduction;
                //DebugMessage("KFP applySpeedRecution:- " + _mobilePartyId + " fleeing hours great enough to apply reduction, new time set ");
            }
            return value;
        }

        private void DebugMessage(string message)
        {
            if (_debug && _debugFleeing)
            {
                //IM.MessageDebug(message);
            }
        }



    }
}
