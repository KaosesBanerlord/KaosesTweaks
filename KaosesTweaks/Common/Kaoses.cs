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


        public static bool IsMCMLoaded()
        {
            bool loaded = false;
            var modnames = Utilities.GetModulesNames().ToList();
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
            var modnames = Utilities.GetModulesNames().ToList();
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




        // Token: 0x06002FC2 RID: 12226 RVA: 0x000C94C8 File Offset: 0x000C76C8
        public static bool KaosesHeroPregnancyCheckCondition(Hero hero)
        {
            if (Statics._settings.PregnancyDebug)
            {
                IM.MessageDebug("HeroComesOfAge: " + Campaign.Current.Models.AgeModel.HeroComesOfAge + "  Direct: " + Statics._settings.HeroComesOfAge);
                IM.MessageDebug("New PregnancyDurationValue: " + Statics._settings.PregnancyDurationValue.ToString());
            }
            //return hero.IsFemale && hero.IsAlive && hero.Age > (float)Campaign.Current.Models.AgeModel.HeroComesOfAge && (hero.Clan == null || !hero.Clan.IsRebelClan) && !CampaignOptions.IsLifeDeathCycleDisabled;
            return hero.IsFemale && hero.IsAlive && hero.Age > (float)Campaign.Current.Models.AgeModel.HeroComesOfAge && hero.Clan == null && !CampaignOptions.IsLifeDeathCycleDisabled;
        }


    }
}
