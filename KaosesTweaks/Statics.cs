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
        public static string? MCMConfigFolder { get; set; }
        public static string? MCMConfigFile { get; set; }
        public static bool MCMConfigFileExists { get; set; } = false;
        public static bool MCMModuleLoaded { get; set; } = false;
        public static bool ModConfigFileExists { get; set; } = false;
        public static string prePrend { get; set; } = DisplayName + ": "; 
        public static bool Debug { get; set; } = false;

        public static bool LogToFile { get; set; } = false;


        public static bool IsMCMLoaded()
        {
            bool loaded = false;
            var modnames = Utilities.GetModulesNames().ToList();
            if (modnames.Contains("Bannerlord.MBOptionScreen"))// && !overrideSettings
            {
                Statics.MCMModuleLoaded = true;
                loaded = true;
                Ux.MessageDebug(prePrend + "MCM Module is loaded");
            }
            return loaded;
        }

        public static bool IsHarmonyLoaded()
        {
            bool loaded = false;
            var modnames = Utilities.GetModulesNames().ToList();
            //if (modnames.Contains("ModLib") && !overrideSettings)
            if (modnames.Contains("Bannerlord.Harmony"))// && !overrideSettings
            {
                loaded = true;
                Ux.MessageDebug(prePrend + "Harmony Module is loaded");
            }
            else
            {
                Ux.MessageError("Failure: " + Statics._settings.ModDisplayName + " Requires Harmony please install the Harmony mod");
            }
            return loaded;
        }


        public static bool IsPlayerClan(PartyBase party)
        {
            bool isSame = false;
            Hero hero = party.LeaderHero;
            if (hero != null)
            {
                Clan clan = hero.Clan;
                Clan playerClan = Clan.PlayerClan;
                if (clan == playerClan)
                {
                    isSame = true;
                }
            }
            return isSame;
        }
        public static bool IsPlayerClan(Hero hero)
        {
            bool isSame = false;
            if (hero != null)
            {
                Clan clan = hero.Clan;
                Clan playerClan = Clan.PlayerClan;
                if (clan == playerClan)
                {
                    isSame = true;
                }
            }
            return isSame;
        }

        public static bool IsPlayerClan(MobileParty mobileParty)
        {
            bool isPlayerClan = false;
            Clan clan;
            Clan playerClan;
            if (mobileParty.IsCaravan)
            {
                Hero hero = mobileParty.LeaderHero;
                if (hero != null)
                {
                    clan = hero.Clan;
                    playerClan = Clan.PlayerClan;
                    if (clan == playerClan)
                    {
                        isPlayerClan = true;
                    }
                }
            }
            else if (mobileParty.IsGarrison)
            {
                Settlement settlement = mobileParty.CurrentSettlement;
                clan = settlement.OwnerClan;
                playerClan = Clan.PlayerClan;
                if (clan == playerClan)
                {
                    isPlayerClan = true;
                }
            }
            return isPlayerClan;
        }

        public static bool isPlayer(Hero hero)
        {
            bool isPlayer = false;
            
            if (hero != null)
            {
                if (Hero.MainHero == hero)
                {
                    isPlayer = true;
                }
/*
                if (Clan.PlayerClan.Leader == hero)
                {
                    isPlayer = true;
                }*/
            }
            return isPlayer;
        }

        public static bool IsLord(Hero hero)
        {
            // == Occupation.Lord
            return hero.CharacterObject.IsHero;
        }

    }
}