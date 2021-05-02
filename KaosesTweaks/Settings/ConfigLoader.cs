using KaosesTweaks.Utils;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using TaleWorlds.Engine;

namespace KaosesTweaks.Settings
{
    class ConfigLoader
    {
        public static string logPreppend { get; set; } = "KaosesTweaks: ";

        public static void LoadConfig()
        {
            BuildVariables();
            LoadModConfigFile();
            ChechMCMProvider();
            if (Statics._settings is null)
            {
                Ux.MessageError("Failed to load any config provider");
            }
            Ux.logToFile = Statics._settings.LogToFile;
            Ux.Debug = Statics._settings.Debug;
        }

        private static void LoadModConfigFile()
        {
            Settings.Instance = new Settings();
            if (Settings.Instance is not null)
            {
                Ux.MessageDebug(logPreppend + "Settings.Instance is not null");
                if (File.Exists(Statics.ConfigFilePath))
                {
                    Ux.MessageDebug(logPreppend + "Config file exists " + Statics.ConfigFilePath.ToString());
                    Settings config = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Statics.ConfigFilePath));
                    Settings.Instance = config;
                }

                if (Settings.Instance.LoadMCMConfigFile && Statics.MCMConfigFileExists)
                {
                    Settings config = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Statics.MCMConfigFile));
                    Settings.Instance = config;
                }
            }
            Statics._settings = Settings.Instance;

        }
        private static void ChechMCMProvider()
        {
            if (Statics.MCMModuleLoaded)
            {
                if (MCMSettings.Instance is not null)
                {
                    Statics._settings = MCMSettings.Instance;
                    Ux.MessageDebug(logPreppend + "using MCM");
                    Ux.MessageDebug(logPreppend + "Not Using config settings");
                }
                else
                {
                    Ux.MessageError(logPreppend + "Problem loading MCM config");
                }
            }
            else
            {
                Ux.MessageDebug(logPreppend + "MCM Module is not loaded");
            }
        }

        private static void BuildVariables()
        {
            IsMCMLoaded();
            CheckMcmConfig();
            CheckModConfig();
        }

        private static void IsMCMLoaded(bool overrideSettings = true)
        {
            var modnames = Utilities.GetModulesNames().ToList();
            //if (modnames.Contains("ModLib") && !overrideSettings)
            if (modnames.Contains("Bannerlord.MBOptionScreen"))// && !overrideSettings
            {
                Statics.MCMModuleLoaded = true;
                Ux.MessageDebug(logPreppend + "MCM Module is loaded");
            }
        }

        private static void CheckMcmConfig()
        {
            string RootFolder = System.IO.Path.Combine(Utilities.GetConfigsPath(), "ModSettings/Global/" + Statics.ModuleFolder);
            if (System.IO.Directory.Exists(RootFolder))
            {
                Statics.MCMConfigFolder = RootFolder;
                string fileLoc = System.IO.Path.Combine(RootFolder, Statics.ModuleFolder + ".json");
                if (System.IO.File.Exists(fileLoc))
                {
                    Statics.MCMConfigFileExists = true;
                    Statics.MCMConfigFile = fileLoc;
                    Ux.MessageDebug(logPreppend + "MCM Module Config file found");
                }
            }
        }
        private static void CheckModConfig()
        {
            if (File.Exists(Statics.ConfigFilePath))
            {
                Statics.ModConfigFileExists = true;
                Ux.MessageDebug(logPreppend + "Config File FOUND");
            }
        }

    }
}
