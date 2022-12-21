using Bannerlord.BUTR.Shared.Helpers;
using KaosesTweaks.Utils;
using System.IO;
using System.Linq;
using TaleWorlds.Engine;

namespace KaosesTweaks.Settings
{
    class ConfigLoader
    {

        public static void LoadConfig()
        {
            BuildVariables();
            //LoadModConfigFile();
            ChechMCMProvider();
            if (Statics._settings is null)
            {
                IM.MessageError("Failed to load any config provider");
            }
            IM.logToFile = Statics._settings.LogToFile;
            IM.Debug = Statics._settings.Debug;
            IM.PrePrend = Statics.PrePrend;
            Logging.PrePrend = Statics.PrePrend;
        }
        private static void BuildVariables()
        {
            IsMCMLoaded();
            CheckMcmConfig();
            //CheckModConfig();
        }


        private static void LoadModConfigFile()
        {
            /*
                        Settings.Instance = new Settings();
                        if (Settings.Instance is not null)
                        {
                            IM.MessageDebug("Settings.Instance is not null");
                            if (File.Exists(Statics.ConfigFilePath))
                            {
                                IM.MessageDebug("Config file exists " + Statics.ConfigFilePath.ToString());
                                Settings config = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Statics.ConfigFilePath));
                                Settings.Instance = config;
                            }

                            if (Settings.Instance.LoadMCMConfigFile && Statics.MCMConfigFileExists)
                            {
                                Settings config = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Statics.MCMConfigFile));
                                Settings.Instance = config;
                            }
                        }
                        Statics._settings = Settings.Instance;*/
        }


        private static void ChechMCMProvider()
        {
            if (Statics.MCMModuleLoaded)
            {
                if (MCMSettings.Instance is not null)
                {
                    Statics._settings = MCMSettings.Instance;
                    IM.MessageDebug("using MCM");
                    //IM.MessageDebug("Not Using config settings");
                }
                else
                {
                    //IM.MessageError("Problem loading MCM config");
                }
            }
            else
            {
                IM.MessageError("MCM Module is not loaded");
            }
        }

        private static void IsMCMLoaded()
        {
            System.Collections.Generic.List<string>? modnames = Utilities.GetModulesNames().ToList();
            if (modnames.Contains("Bannerlord.MBOptionScreen"))// && !overrideSettings
            {
                Statics.MCMModuleLoaded = true;
                IM.MessageDebug("MCM Module is loaded");
            }
        }

        private static void CheckMcmConfig()
        {
            string RootFolder = System.IO.Path.Combine(FSIOHelper.GetConfigPath(), "ModSettings/Global/" + Statics.ModuleFolder);
            if (Directory.Exists(RootFolder))
            {
                Statics.MCMConfigFolder = RootFolder;
                string fileLoc = System.IO.Path.Combine(RootFolder, Statics.ModuleFolder + ".json");
                if (File.Exists(fileLoc))
                {
                    Statics.MCMConfigFileExists = true;
                    Statics.MCMConfigFile = fileLoc;
                    IM.MessageDebug("MCM Module Config file found");
                }
            }
        }

        private static void CheckModConfig()
        {
            if (File.Exists(Statics.ConfigFilePath))
            {
                Statics.ModConfigFileExists = true;
                //IM.MessageDebug("Config File FOUND");
            }
        }

    }
}
