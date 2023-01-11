using KaosesTweaks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;

namespace KaosesTweaks.Objects
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

        public KaosesFleeingPartySpeed(string mobilePartyStringId)
        {
            _mobilePartyId = mobilePartyStringId;
            _speedReduction = Statics._settings.DynamicFleeingSpeedReductionAmount;
            _hoursBeforeReduction = Statics._settings.DynamicFleeingSpeedReductionHours;
            _enabled = Statics._settings.KaosesDynamicSpeedModifiersEnabled;
            _nextTimeApplyReduction = CampaignTime.HoursFromNow(_hoursBeforeReduction);
            _debug = Statics._settings.DynamicPartySpeedDebug;
        }

        public float GetSpeedReductionAmount(AiBehavior shortTermBehavior)
        {
            float reductionAmount = 0.0f;
            if (!_enabled) { return reductionAmount; }
            if (_debug){ IM.MessageDebug("KFPS: Debug Dynamic party Speeds");}

            if (CheckShouldApplySpeedRecution())
            {
                reductionAmount = _speedReduction * _hoursFleeing;
                _hoursFleeing = _hoursFleeing + _hoursBeforeReduction;

                if (_debug) { IM.MessageDebug("KFPS: Reducing " + _mobilePartyId + " speed due to fleeing for: " + _hoursFleeing + " hours so reducing by amount: " + reductionAmount); }
            }
            return reductionAmount;
        }

        private bool CheckShouldApplySpeedRecution()
        {
            bool value = false;
            if (CampaignTime.Now.ToHours > _nextTimeApplyReduction.ToHours)
            {
                value = true;
                _nextTimeApplyReduction = CampaignTime.HoursFromNow(_hoursBeforeReduction);
                if (_debug) { IM.MessageDebug("KFPS applySpeedRecution:- " + _mobilePartyId + " fleeing hours great enough to apply reduction, new time set "); }
            }
            return value;
        }


    }
}

