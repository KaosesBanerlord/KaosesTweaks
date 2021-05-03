using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine;

namespace KaosesTweaks
{
    public static class Statics
    {
        public static ISettingsProviderInterface? _settings;
        public const string ModuleFolder = "KaosesTweaks";
        public const string InstanceID = ModuleFolder;
        public const string DisplayName = "Kaoses Tweaks";
        public const string Version = "0.0.1";
        public const string FormatType = "json";//json2
        public const string logPath = @"..\\..\\Modules\\" + ModuleFolder + "\\KaosLog.txt";
        public const string ConfigFilePath = @"..\\..\\Modules\\" + ModuleFolder + "\\config.json";

        public static string PrePrend { get; set; } = DisplayName + " : ";
        public const string HarmonyId = "KaosesPoliticalWars" + ".harmony";

        public const bool UsesHarmony = false;

        public static bool Debug { get; set; } = false;
        public static bool LogToFile { get; set; } = false;

        public static string? MCMConfigFolder { get; set; }
        public static string? MCMConfigFile { get; set; }
        public static bool MCMConfigFileExists { get; set; } = false;
        public static bool MCMModuleLoaded { get; set; } = false;
        public static bool ModConfigFileExists { get; set; } = false;


    }
}