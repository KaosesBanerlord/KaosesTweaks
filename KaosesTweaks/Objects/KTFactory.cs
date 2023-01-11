using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;

namespace KaosesTweaks.Objects
{
    public static class KTFactory
    {
        /* KaosesPartySpeeds */
        //~ KT Party Speeds
        private static Dictionary<MobileParty, CampaignTime> _FleeingParties = new Dictionary<MobileParty, CampaignTime>();
        private static Dictionary<MobileParty, int> _FleeingHours = new Dictionary<MobileParty, int>();
        private static Dictionary<MobileParty, float> _FleeingSpeedReduction = new Dictionary<MobileParty, float>();
        private static Dictionary<string, KaosesFleeingPartySpeed> _KaosesFleeingSpeedReduction = new Dictionary<string, KaosesFleeingPartySpeed>();

        public static Dictionary<MobileParty, CampaignTime> FleeingParties
        {
            get
            {
                if (_FleeingParties != null)
                {
                    return _FleeingParties;
                }
                else
                {
                    _FleeingParties = new Dictionary<MobileParty, CampaignTime>();
                    return _FleeingParties;
                }
            }
            set => _FleeingParties = value;

        }
        public static Dictionary<MobileParty, int> FleeingHours
        {
            get
            {
                if (_FleeingHours != null)
                {
                    return _FleeingHours;
                }
                else
                {
                    _FleeingHours = new Dictionary<MobileParty, int>();
                    return _FleeingHours;
                }
            }
            set => _FleeingHours = value;

        }
        public static Dictionary<MobileParty, float> FleeingSpeedReduction
        {
            get
            {
                if (_FleeingSpeedReduction != null)
                {
                    return _FleeingSpeedReduction;
                }
                else
                {
                    _FleeingSpeedReduction = new Dictionary<MobileParty, float>();
                    return _FleeingSpeedReduction;
                }
            }
            set => _FleeingSpeedReduction = value;

        }



        public static Dictionary<string, KaosesFleeingPartySpeed> KaosesFleeingSpeedReduction
        {
            get
            {
                if (_KaosesFleeingSpeedReduction != null)
                {
                    return _KaosesFleeingSpeedReduction;
                }
                else
                {
                    _KaosesFleeingSpeedReduction = new Dictionary<string, KaosesFleeingPartySpeed>();
                    return _KaosesFleeingSpeedReduction;
                }
            }
            set => _KaosesFleeingSpeedReduction = value;

        }
        //~ KT Party Speeds
        /* KaosesPartySpeeds */

    }
}
