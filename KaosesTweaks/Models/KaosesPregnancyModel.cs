using Helpers;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace KaosesTweaks.Models
{
    public class KaosesPregnancyModel : DefaultPregnancyModel
    {

        // Token: 0x17000B21 RID: 2849
        // (get) Token: 0x06002C6A RID: 11370 RVA: 0x000AC6C0 File Offset: 0x000AA8C0
        public override float PregnancyDurationInDays
        {
            get
            {
                if (Statics._settings.PregnancyDurationModifiers)
                {
                    if (Statics._settings.PregnancyDebug)
                    {
                        IM.MessageDebug("New PregnancyDurationValue: " + Statics._settings.PregnancyDurationValue.ToString());
                    }
                    return Statics._settings.PregnancyDurationValue;
                }
                return 36f;
            }
        }

        // Token: 0x17000B22 RID: 2850
        // (get) Token: 0x06002C6B RID: 11371 RVA: 0x000AC6C7 File Offset: 0x000AA8C7
        public override float MaternalMortalityProbabilityInLabor
        {
            get
            {
                if (Statics._settings.PregnancyLaborMortalityChanceModifiers)
                {
                    if (Statics._settings.PregnancyDebug)
                    {
                        //IM.MessageDebug("New MortalityProbabilityInLabor: " + Statics._settings.PregnancyLaborMortalityChanceValue.ToString());
                    }
                    return Statics._settings.PregnancyLaborMortalityChanceValue;
                }
                else if (Statics._settings.NoMaternalMortalityTweakEnabled)
                {
                    if (Statics._settings.PregnancyDebug)
                    {
                        //IM.MessageDebug("New MortalityProbabilityInLabor: DISABLED");
                    }
                    return 0.0f;
                }
                return 0.015f;
            }
        }

        // Token: 0x17000B23 RID: 2851
        // (get) Token: 0x06002C6C RID: 11372 RVA: 0x000AC6CE File Offset: 0x000AA8CE
        public override float StillbirthProbability
        {
            get
            {
                if (Statics._settings.PregnancyStillbirthChanceModifiers)
                {
                    if (Statics._settings.PregnancyDebug)
                    {
                        //IM.MessageDebug("New StillbirthProbability: " + Statics._settings.PregnancyStillbirthChanceValue.ToString());
                    }
                    return Statics._settings.PregnancyStillbirthChanceValue;
                }
                else if (Statics._settings.NoStillbirthsTweakEnabled)
                {
                    if (Statics._settings.PregnancyDebug)
                    {
                        //IM.MessageDebug("New StillbirthProbability: DISABLED");
                    }
                    return 0.0f;
                }
                return 0.01f;
            }
        }

        // Token: 0x17000B24 RID: 2852
        // (get) Token: 0x06002C6D RID: 11373 RVA: 0x000AC6D5 File Offset: 0x000AA8D5
        public override float DeliveringFemaleOffspringProbability
        {
            get
            {
                if (Statics._settings.PregnancyFemaleOffspringChanceModifiers)
                {
                    if (Statics._settings.PregnancyDebug)
                    {
                        //IM.MessageDebug("New FemaleOffspring Probability: " + Statics._settings.PregnancyFemaleOffspringChanceValue.ToString());
                    }
                    return Statics._settings.PregnancyFemaleOffspringChanceValue;
                }
                return 0.51f;
            }
        }

        // Token: 0x17000B25 RID: 2853
        // (get) Token: 0x06002C6E RID: 11374 RVA: 0x000AC6DC File Offset: 0x000AA8DC
        public override float DeliveringTwinsProbability
        {
            get
            {
                if (Statics._settings.PregnancyTwinsChanceModifiers)
                {
                    if (Statics._settings.PregnancyDebug)
                    {
                        //IM.MessageDebug("New Twins Probability: " + Statics._settings.PregnancyTwinsChanceValue.ToString());
                    }
                    return Statics._settings.PregnancyTwinsChanceValue;
                }
                return 0.03f;
            }
        }


        public override float GetDailyChanceOfPregnancyForHero(Hero hero)
        {
            if (hero == null) throw new ArgumentNullException(nameof(hero));

            if (MCMSettings.Instance is { } settings && hero != null)
            {
                if (!settings.DailyChancePregnancyTweakEnabled)
                    return base.GetDailyChanceOfPregnancyForHero(hero);

                float num = 0f;
                if (settings.PlayerCharacterInfertileEnabled && HeroIsMainOrSpouseOfMain(hero))
                {
                    if (Statics._settings.PregnancyDebug)
                    {
                        //IM.MessageDebug("PlayerCharacterInfertileEnabled: " + num.ToString());
                    }
                    return num;
                }

                if (hero.Children != null && hero.Children.Any() && hero.Children.Count >= MCMSettings.Instance.MaxChildren)
                {
                    if (Statics._settings.PregnancyDebug)
                    {
                        //IM.MessageDebug("New hero.Children.Count >= MCMSettings.Instance.MaxChildren: " + num.ToString());
                    }
                    return num;
                }

                if (hero != null && hero.Spouse != null && IsHeroAgeSuitableForPregnancy(hero))
                {
                    ExplainedNumber bonuses = new ExplainedNumber(1f, false);
                    PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Medicine.PerfectHealth, hero.Clan.Leader.CharacterObject, true, ref bonuses);
                    num = (float)((6.9 - ((double)hero.Age - settings.MinPregnancyAge) * 0.2) * 0.02) / ((hero.Children!.Count + 1) * 0.2f) * bonuses.ResultNumber;
                    if (Statics._settings.PregnancyDebug)
                    {
                        //IM.MessageDebug("Pregnancy Chance: " + num.ToString());
                    }
                }

                if (hero!.Clan == Hero.MainHero.Clan)
                {
                    num *= settings.ClanFertilityBonus;
                    if (Statics._settings.PregnancyDebug)
                    {
                        //IM.MessageDebug("ClanFertilityBonus: " + num.ToString());
                    }
                }
                return num;
            }
            return base.GetDailyChanceOfPregnancyForHero(hero);
        }

        private bool IsHeroAgeSuitableForPregnancy(Hero hero)
        {
            if (!hero.IsFemale)
                return true;

            return (double)hero.Age >= MCMSettings.Instance!.MinPregnancyAge && (double)hero.Age <= MCMSettings.Instance!.MaxPregnancyAge;
        }

        private bool HeroIsMainOrSpouseOfMain(Hero hero)
        {
            if (hero == Hero.MainHero)
                return true;

            if (hero.Spouse == Hero.MainHero)
                return true;

            return false;
        }


        // Token: 0x04000ED4 RID: 3796
        private const int MinPregnancyAge = 18;

        // Token: 0x04000ED5 RID: 3797
        private const int MaxPregnancyAge = 45;

    }
}
