using Helpers;
using KaosesTweaks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace KaosesTweaks.Behaviors
{
    class KaosesPregnancyCampaignBehavior : PregnancyCampaignBehavior
    {
        // Token: 0x06002FBF RID: 12223 RVA: 0x000C9360 File Offset: 0x000C7560
        public override void RegisterEvents()
        {
            //CampaignEvents.OnNewGameCreatedEvent2.AddNonSerializedListener(this, new Action(this.OnAfterNewGameCreated));
            CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(OnHeroKilled));
            CampaignEvents.DailyTickHeroEvent.AddNonSerializedListener(this, new Action<Hero>(DailyTickHero));
            CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(GameLoaded));
            CampaignEvents.OnChildConceivedEvent.AddNonSerializedListener(this, new Action<Hero>(ChildConceived));

        }

        // Token: 0x06002FC0 RID: 12224 RVA: 0x000C93E0 File Offset: 0x000C75E0

        private void GameLoaded(CampaignGameStarter starter)
        {
            foreach (LogEntry logEntry in new List<LogEntry>(Campaign.Current.LogEntryHistory.GameActionLogs))
            {
                ChildbirthLogEntry childbirthLogEntry = logEntry as ChildbirthLogEntry;
                if (childbirthLogEntry != null && childbirthLogEntry.NeedsNewLogEntryForTwin && childbirthLogEntry.NewLogTwin != null)
                {
                    LogEntry.AddLogEntry(new ChildbirthLogEntry(childbirthLogEntry.Mother, childbirthLogEntry.NewLogTwin), childbirthLogEntry.GameTime);
                }
            }
        }

        // Token: 0x06002FC1 RID: 12225 RVA: 0x000C9470 File Offset: 0x000C7670
        private void DailyTickHero(Hero hero)
        {
            if (HeroPregnancyCheckCondition(hero))
            {
                if (hero.Age >= Campaign.Current.Models.AgeModel.HeroComesOfAge && hero.Spouse != null && hero.Spouse.IsAlive && !hero.IsPregnant)
                {
                    RefreshSpouseVisit(hero);
                }
                if (hero.IsPregnant)
                {
                    CheckOffspringsToDeliver(hero);
                }
            }
        }

        // Token: 0x06002FC2 RID: 12226 RVA: 0x000C94C8 File Offset: 0x000C76C8
        private bool HeroPregnancyCheckCondition(Hero hero)
        {
            return hero.IsFemale && hero.IsAlive && hero.Age >= Campaign.Current.Models.AgeModel.HeroComesOfAge && (hero.Clan == null || !hero.Clan.IsRebelClan) && !CampaignOptions.IsLifeDeathCycleDisabled;
        }

        // Token: 0x06002FC3 RID: 12227 RVA: 0x000C9524 File Offset: 0x000C7724
        private void CheckOffspringsToDeliver(Hero hero)
        {
            KaosesPregnancyCampaignBehavior.Pregnancy pregnancy = _heroPregnancies.Find((KaosesPregnancyCampaignBehavior.Pregnancy x) => x.Mother == hero);
            if (pregnancy == null)
            {
                hero.IsPregnant = false;
                return;
            }
            CheckOffspringsToDeliver(pregnancy);
        }

        // Token: 0x06002FC4 RID: 12228 RVA: 0x000C956D File Offset: 0x000C776D
        private void RefreshSpouseVisit(Hero hero)
        {
            if (CheckAreNearby(hero, hero.Spouse) && MBRandom.RandomFloat <= Campaign.Current.Models.PregnancyModel.GetDailyChanceOfPregnancyForHero(hero))
            {
                if (Statics._settings.PregnancyDebug)
                {
                    IM.MessageDebug("KaosesPregnancyCampaignBehavior:  MBRandom.RandomFloat <=" + MBRandom.RandomFloat.ToString() + " Hero Chance: " + Campaign.Current.Models.PregnancyModel.GetDailyChanceOfPregnancyForHero(hero).ToString());
                }
                MakePregnantAction.Apply(hero);
            }
        }

        // Token: 0x06002FC5 RID: 12229 RVA: 0x000C95A0 File Offset: 0x000C77A0
        private bool CheckAreNearby(Hero hero, Hero spouse)
        {
            Settlement settlement;
            MobileParty mobileParty;
            GetLocation(hero, out settlement, out mobileParty);
            Settlement settlement2;
            MobileParty mobileParty2;
            GetLocation(spouse, out settlement2, out mobileParty2);
            return (settlement != null && settlement == settlement2) || (hero.Clan != Hero.MainHero.Clan && MBRandom.RandomFloat < 0.2f);
        }

        // Token: 0x06002FC6 RID: 12230 RVA: 0x000C95ED File Offset: 0x000C77ED
        private void GetLocation(Hero hero, out Settlement heroSettlement, out MobileParty heroParty)
        {
            heroSettlement = hero.CurrentSettlement;
            heroParty = hero.PartyBelongedTo;
            MobileParty mobileParty = heroParty;
            if (((mobileParty != null) ? mobileParty.AttachedTo : null) != null)
            {
                heroParty = heroParty.AttachedTo;
            }
            if (heroSettlement == null)
            {
                MobileParty mobileParty2 = heroParty;
                heroSettlement = (mobileParty2 != null) ? mobileParty2.CurrentSettlement : null;
            }
        }

        // Token: 0x06002FC7 RID: 12231 RVA: 0x000C962C File Offset: 0x000C782C
        private void CheckOffspringsToDeliver(KaosesPregnancyCampaignBehavior.Pregnancy pregnancy)
        {
            PregnancyModel pregnancyModel = Campaign.Current.Models.PregnancyModel;
            if (!pregnancy.DueDate.IsFuture && pregnancy.Mother.IsAlive)
            {
                Hero mother = pregnancy.Mother;
                bool flag = MBRandom.RandomFloat <= pregnancyModel.DeliveringTwinsProbability;
                List<Hero> list = new List<Hero>();
                int num = flag ? 2 : 1;
                int num2 = 0;
                for (int i = 0; i < num; i++)
                {
                    if (MBRandom.RandomFloat > pregnancyModel.StillbirthProbability)
                    {
                        bool isOffspringFemale = MBRandom.RandomFloat <= pregnancyModel.DeliveringFemaleOffspringProbability;
                        Hero item = HeroCreator.DeliverOffSpring(mother, pregnancy.Father, isOffspringFemale, null);
                        list.Add(item);
                    }
                    else
                    {
                        TextObject textObject = new TextObject("{=pw4cUPEn}{MOTHER.LINK} has delivered stillborn.", null);
                        StringHelpers.SetCharacterProperties("MOTHER", mother.CharacterObject, textObject);
                        InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
                        num2++;
                    }
                }
                //CampaignEventDispatcher.Instance.OnGivenBirth(mother, list, num2);
                OnGivenBirth(mother, list, num2);

                mother.IsPregnant = false;
                _heroPregnancies.Remove(pregnancy);
                if (mother != Hero.MainHero && MBRandom.RandomFloat <= pregnancyModel.MaternalMortalityProbabilityInLabor)
                {
                    KillCharacterAction.ApplyInLabor(mother, true);
                }
            }
        }

        // Token: 0x06002FC8 RID: 12232 RVA: 0x000C9764 File Offset: 0x000C7964
        private void ChildConceived(Hero mother)
        {
            _heroPregnancies.Add(new KaosesPregnancyCampaignBehavior.Pregnancy(mother, mother.Spouse, CampaignTime.DaysFromNow(Campaign.Current.Models.PregnancyModel.PregnancyDurationInDays)));
        }

        // Token: 0x06002FCB RID: 12235 RVA: 0x000C97F8 File Offset: 0x000C79F8
        private void CreateYoungCharactersForFactions()
        {
            foreach (Hero hero2 in from hero in Hero.AllAliveHeroes.ToList<Hero>()
                                   where !hero.IsNotSpawned
                                   select hero)
            {
                if (hero2.IsFemale && hero2.Spouse != null && hero2.Clan != Clan.PlayerClan)
                {
                    int num = 0;
                    for (int i = 17; i > 0; i--)
                    {
                        if (MBRandom.RandomFloat < GetChanceOfChild(hero2, i) && num < 6)
                        {
                            HeroCreator.DeliverOffSpring(hero2, hero2.Spouse, MBRandom.RandomFloat <= Campaign.Current.Models.PregnancyModel.DeliveringFemaleOffspringProbability, null);
                            num++;
                        }
                    }
                }
            }
        }

        // Token: 0x06002FCC RID: 12236 RVA: 0x000C98D8 File Offset: 0x000C7AD8
        private float GetChanceOfChild(Hero mother, int yearsAgo)
        {
            int count = mother.Children.Count;
            float num = 48f;
            float num2 = mother.CharacterObject.Age - yearsAgo;
            foreach (Hero hero in mother.Children)
            {
                if (hero.CharacterObject.Age - yearsAgo < num)
                {
                    num = hero.CharacterObject.Age - yearsAgo;
                }
            }
            float result = 0f;
            if (num > 2f && num2 > 18f)
            {
                result = (num + 2f) * (42f - num2) / (count + 1) / 100f;
            }
            return result;
        }

        // Token: 0x06002FCD RID: 12237 RVA: 0x000C99A0 File Offset: 0x000C7BA0
        public override void SyncData(IDataStore dataStore)
        {
            dataStore.SyncData<List<KaosesPregnancyCampaignBehavior.Pregnancy>>("_heroPregnancies", ref _heroPregnancies);
        }

        // Token: 0x04001007 RID: 4103
        private List<KaosesPregnancyCampaignBehavior.Pregnancy> _heroPregnancies = new List<KaosesPregnancyCampaignBehavior.Pregnancy>();



        // Token: 0x06003126 RID: 12582 RVA: 0x000D5E04 File Offset: 0x000D4004
        private void OnGivenBirth(Hero mother, List<Hero> aliveOffsprings, int stillbornCount)
        {
            if (mother == Hero.MainHero || mother == Hero.MainHero.Spouse || mother.Clan == Clan.PlayerClan)
            {
                TextObject textObject;
                if (mother == Hero.MainHero)
                {
                    textObject = new TextObject("{=oIA9lkpc}You have given birth to {DELIVERED_CHILDREN}.", null);
                }
                else if (mother == Hero.MainHero.Spouse)
                {
                    textObject = new TextObject("{=TsbjAsxs}Your wife {MOTHER.NAME} has given birth to {DELIVERED_CHILDREN}.", null);
                }
                else
                {
                    textObject = new TextObject("{=LsDRCPp0}Your clan member {MOTHER.NAME} has given birth to {DELIVERED_CHILDREN}.", null);
                }
                if (stillbornCount == 2)
                {
                    textObject.SetTextVariable("DELIVERED_CHILDREN", new TextObject("{=Sn9a1Aba}two stillborn babies", null));
                }
                else if (stillbornCount == 1 && aliveOffsprings.Count == 0)
                {
                    textObject.SetTextVariable("DELIVERED_CHILDREN", new TextObject("{=qWLq2y84}a stillborn baby", null));
                }
                else if (stillbornCount == 1 && aliveOffsprings.Count == 1)
                {
                    textObject.SetTextVariable("DELIVERED_CHILDREN", new TextObject("{=vn13OyFV}one healthy and one stillborn baby", null));
                }
                else if (stillbornCount == 0 && aliveOffsprings.Count == 1)
                {
                    textObject.SetTextVariable("DELIVERED_CHILDREN", new TextObject("{=lbRMmZym}a healthy baby", null));
                }
                else if (stillbornCount == 0 && aliveOffsprings.Count == 2)
                {
                    textObject.SetTextVariable("DELIVERED_CHILDREN", new TextObject("{=EPbHr2DX}two healthy babies", null));
                }
                StringHelpers.SetCharacterProperties("MOTHER", mother.CharacterObject, textObject);
                InformationManager.AddQuickInformation(textObject, 0, null, "");
            }
        }



        // Token: 0x0200060F RID: 1551
        internal class Pregnancy
        {
            // Token: 0x06004380 RID: 17280 RVA: 0x001215A8 File Offset: 0x0011F7A8
            internal static void AutoGeneratedStaticCollectObjectsPregnancy(object o, List<object> collectedObjects)
            {
                ((KaosesPregnancyCampaignBehavior.Pregnancy)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
            }

            // Token: 0x06004381 RID: 17281 RVA: 0x001215B6 File Offset: 0x0011F7B6
            protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
            {
                collectedObjects.Add(Mother);
                collectedObjects.Add(Father);
                CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime(DueDate, collectedObjects);
            }

            // Token: 0x06004382 RID: 17282 RVA: 0x001215E1 File Offset: 0x0011F7E1
            internal static object AutoGeneratedGetMemberValueMother(object o)
            {
                return ((KaosesPregnancyCampaignBehavior.Pregnancy)o).Mother;
            }

            // Token: 0x06004383 RID: 17283 RVA: 0x001215EE File Offset: 0x0011F7EE
            internal static object AutoGeneratedGetMemberValueFather(object o)
            {
                return ((KaosesPregnancyCampaignBehavior.Pregnancy)o).Father;
            }

            // Token: 0x06004384 RID: 17284 RVA: 0x001215FB File Offset: 0x0011F7FB
            internal static object AutoGeneratedGetMemberValueDueDate(object o)
            {
                return ((KaosesPregnancyCampaignBehavior.Pregnancy)o).DueDate;
            }

            // Token: 0x06004385 RID: 17285 RVA: 0x0012160D File Offset: 0x0011F80D
            public Pregnancy(Hero pregnantHero, Hero father, CampaignTime dueDate)
            {
                Mother = pregnantHero;
                Father = father;
                DueDate = dueDate;
            }

            // Token: 0x040018A9 RID: 6313
            [SaveableField(1)]
            public readonly Hero Mother;

            // Token: 0x040018AA RID: 6314
            [SaveableField(2)]
            public readonly Hero Father;

            // Token: 0x040018AB RID: 6315
            [SaveableField(3)]
            public readonly CampaignTime DueDate;
        }

    }
}
