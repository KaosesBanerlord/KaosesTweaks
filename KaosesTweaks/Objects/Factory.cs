using KaosesCommon.Utils;
using KaosesTweaks.Objects.PartySpeeds;
using KaosesTweaks.Settings;
using System.Collections.Concurrent;
using System.Reflection;

namespace KaosesTweaks.Objects
{
    /// <summary>
    /// KaosesTweaks Factory Object
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// Variable to hold the MCM settings object
        /// </summary>
        private static Config? _settings = null;

        /// <summary>
        /// Bool indicates if MCM is a loaded mod
        /// </summary>
        public static bool MCMModuleLoaded { get; set; } = false;

        //~ KT Party Speeds
        /* KaosesPartySpeeds */
        private static ConcurrentDictionary<string, KaosesFleeingPartySpeed> _KaosesFleeingPartiesList = new ConcurrentDictionary<string, KaosesFleeingPartySpeed>();
        private static FleeingPartiesManager _fleeingPartiesdManager = new FleeingPartiesManager();
        //~ KT Party Speeds
        /* KaosesPartySpeeds */

        /// <summary>
        /// MCM Settings Object Instance
        /// </summary>
        public static Config Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = Config.Instance;
                    if (_settings is null)
                    {
                        IM.ShowMessageBox("Kaoses Tweaks Failed to load MCM config provider", "Kaoses Tweaks MCM Error");
                    }
                }
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }

        /// <summary>
        /// Mod version
        /// </summary>
        public static string ModVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Unused mod config file path
        /// </summary>
        private static string ConfigFilePath
        {

            get
            {
                return @"..\\..\\Modules\\" + SubModule.ModuleId + "\\config.json";
            }
            //set {  = value; }

        }

        //~ KT Party Speeds
        /* KaosesPartySpeeds */
        public static ConcurrentDictionary<string, KaosesFleeingPartySpeed> KaosesFleeingPartiesList
        {
            get
            {
                if (_KaosesFleeingPartiesList != null)
                {
                    return _KaosesFleeingPartiesList;
                }
                else
                {
                    _KaosesFleeingPartiesList = new ConcurrentDictionary<string, KaosesFleeingPartySpeed>();
                    return _KaosesFleeingPartiesList;
                }
            }
            set => _KaosesFleeingPartiesList = value;

        }

        public static FleeingPartiesManager FleeingPartiesMgr
        {
            get
            {
                if (_fleeingPartiesdManager != null)
                {
                    return _fleeingPartiesdManager;
                }
                else
                {
                    _fleeingPartiesdManager = new FleeingPartiesManager();
                    return _fleeingPartiesdManager;
                }
            }
            set => _fleeingPartiesdManager = value;

        }
        //~ KT Party Speeds
        /* KaosesPartySpeeds */
    }
}
