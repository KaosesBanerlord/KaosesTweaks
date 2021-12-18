using KaosesTweaks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine;

namespace KaosesTweaks.Common
{
    class Kaoses
    {
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

        public static bool IsPlayer(Hero hero)
        {
            bool isPlayer = false;

            if (hero != null)
            {
                if (Hero.MainHero == hero)
                {
                    isPlayer = true;
                }
            }
            return isPlayer;
        }

        /// <summary>
        /// Checks if the hero is a Lord/Lady or Wanderer and is not the player
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public static bool IsLord(Hero hero)
        {
            /*
            IM.MessageDebug("IsLord: "
                + "Name: " + hero.CharacterObject.Name.ToString() +"\r\n"
                + "Occupation: " + hero.CharacterObject.Occupation.ToString() +"\r\n"
                + "IsHero: " + hero.CharacterObject.IsHero.ToString() +"\r\n"
                //+ "IsBasicTroop: " + hero.CharacterObject.IsBasicTroop.ToString() +"\r\n"
                + "result" + ((hero.CharacterObject.Occupation == Occupation.Lord || hero.CharacterObject.Occupation == Occupation.Lady || hero.CharacterObject.Occupation == Occupation.Wanderer) && !hero.IsHumanPlayerCharacter).ToString() +"\r\n"
                );
            */
            return (hero.CharacterObject.Occupation == Occupation.Mercenary || hero.CharacterObject.Occupation == Occupation.Lord || hero.CharacterObject.Occupation == Occupation.GangLeader || hero.CharacterObject.Occupation == Occupation.Wanderer) && !hero.IsHumanPlayerCharacter;

            /*
                Kaoses Tweaks : IsLordName: Nadea the Wanderer
                Occupation: Wanderer
                IsHero: True
                IsBasicTroop: False

                Kaoses Tweaks : IsLordName: Ira
                Occupation: Lord
                IsHero: True
                IsBasicTroop: False
             */
        }

        public static bool IsPlayerLord(Hero hero)
        {
            //hero.CharacterObject.IsHero
            /*
                        IM.MessageDebug("IsLord: "
                            + "Name: " + hero.CharacterObject.Name.ToString() + "\r\n"
                            + "Occupation: " + hero.CharacterObject.Occupation.ToString() + "\r\n"
                            + "IsHero: " + hero.CharacterObject.IsHero.ToString() + "\r\n"
                            + "IsPlayerClan: " + IsPlayerClan(hero).ToString() + "\r\n"
                            //+ "IsBasicTroop: " + hero.CharacterObject.IsBasicTroop.ToString() +"\r\n"
                            + "result" + ((hero.CharacterObject.Occupation == Occupation.Lord || hero.CharacterObject.Occupation == Occupation.Lady || hero.CharacterObject.Occupation == Occupation.Wanderer) && !hero.IsHumanPlayerCharacter && IsPlayerClan(hero)).ToString() + "\r\n"
                            );*/
            return (hero.CharacterObject.Occupation == Occupation.Mercenary || hero.CharacterObject.Occupation == Occupation.Lord || hero.CharacterObject.Occupation == Occupation.GangLeader || hero.CharacterObject.Occupation == Occupation.Wanderer) && !hero.IsHumanPlayerCharacter;
        }

        public static bool IsMCMLoaded()
        {
            bool loaded = false;
            List<string>? modnames = Utilities.GetModulesNames().ToList();
            if (modnames.Contains("Bannerlord.MBOptionScreen"))// && !overrideSettings
            {
                Statics.MCMModuleLoaded = true;
                loaded = true;
                IM.MessageDebug("MCM Module is loaded");
            }
            return loaded;
        }

        public static bool IsHarmonyLoaded()
        {
            bool loaded = false;
            List<string>? modnames = Utilities.GetModulesNames().ToList();
            //if (modnames.Contains("ModLib") && !overrideSettings)
            if (modnames.Contains("Bannerlord.Harmony"))// && !overrideSettings
            {
                loaded = true;
                IM.MessageDebug("Harmony Module is loaded");
            }
            else
            {
                IM.MessageError("Requires Harmony please install the Harmony mod");
            }
            return loaded;
        }


    }
}
