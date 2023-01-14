using KaosesCommon.Utils;
using KaosesTweaks.Objects;
using KaosesTweaks.Settings;

namespace KaosesTweaks
{
    /// <summary>
    /// Internal class to initialize the mod settings class from MCM and to set the IM and Logger variables 
    /// </summary>
    internal class Init
    {
        public Init()
        {
            /// Load the Settings Object
            Config settings = Factory.Settings;

            ///
            /// Set IM variable values
            ///
            IM.logToFile = settings.LogToFile;
            IM.IsDebug = settings.Debug;
            IM.ModVersion = Factory.ModVersion;
            IM.PrePrend = SubModule.ModuleId;

            ///
            /// Set Logger variable values
            ///
            Logger.ModuleId = SubModule.ModuleId;
            Logger._modulepath = SubModule.modulePath;

            ///Uncomment to have a this prepended to log lines
            //Logger.PrePrend = SubModule.ModuleFolder;

            /// Uncomment to have date time added to log lines
            //Logger.addDateTimeToLog = true;

            /// Uncomment to override the log file path
            //Logger.LogFilePath = "c:\\BannerLord\\KaosesCommon\\logfile.text";
        }
    }
}
