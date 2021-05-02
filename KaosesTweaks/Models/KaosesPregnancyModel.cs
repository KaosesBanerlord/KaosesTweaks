using KaosesTweaks.Utils;
using System;
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
                    //Logging.Lm("New Duration: "+ Statics._settings.PregnancyDurationValue.ToString());
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
                    //Logging.Lm("New MortalityProbabilityInLabor: " + Statics._settings.PregnancyLaborMortalityChanceValue.ToString());
                    return Statics._settings.PregnancyLaborMortalityChanceValue;
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
                    //Logging.Lm("New StillbirthProbability: " + Statics._settings.PregnancyStillbirthChanceValue.ToString());
                    return Statics._settings.PregnancyStillbirthChanceValue;
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
                    //Logging.Lm("New DeliveringFemaleOffspringProbability: " + Statics._settings.PregnancyFemaleOffspringChanceValue.ToString());
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
                    //Logging.Lm("New DeliveringTwinsProbability: " + Statics._settings.PregnancyTwinsChanceValue.ToString());
                    return Statics._settings.PregnancyTwinsChanceValue;
                }
                return 0.03f;
            }
        }

        // Token: 0x06002C6F RID: 11375 RVA: 0x000AC6E3 File Offset: 0x000AA8E3
        private bool IsHeroAgeSuitableForPregnancy(Hero hero)
        {
            return hero.Age >= 18f && hero.Age <= 45f;
        }

        // Token: 0x06002C70 RID: 11376 RVA: 0x000AC704 File Offset: 0x000AA904
        public override float GetDailyChanceOfPregnancyForHero(Hero hero)
        {
            float result = 0f;
            if (hero.Spouse != null && this.IsHeroAgeSuitableForPregnancy(hero))
            {
                result = (1.2f - (hero.Age - 18f) * 0.04f) / (float)Math.Pow((double)(hero.Children.Count + 1), 2.0) * 0.2f;
            }
            return result;
        }

        // Token: 0x04000ED4 RID: 3796
        private const int MinPregnancyAge = 18;

        // Token: 0x04000ED5 RID: 3797
        private const int MaxPregnancyAge = 45;

    }
}
