using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;
using TaleWorlds.LinQuick;

namespace KaosesTweaks.Behaviors
{
    public class CompanionsBehavior : CompanionsCampaignBehavior
    {
        /*    protected const int CompanionMoveRandomIndex = 5;
            protected const float DesiredCompanionPerTown = 0.6f;
            protected Dictionary<CharacterObject, int> _companionTemplates;
            protected int _cachedCompanionCount;

            public CompanionsBehavior()
            {
                this._companionTemplates = new Dictionary<CharacterObject, int>();
                this._cachedCompanionCount = 0;
            }

            public override void RegisterEvents()
            {
                CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener((object)this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
                CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener((object)this, new Action<CampaignGameStarter>(this.OnGameLoaded));
                CampaignEvents.HeroKilledEvent.AddNonSerializedListener((object)this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
                CampaignEvents.DailyTickEvent.AddNonSerializedListener((object)this, new Action(this.DailyTick));
                CampaignEvents.NewCompanionAdded.AddNonSerializedListener((object)this, new Action<Hero>(this.OnPlayerRecruitedCompanion));
                CampaignEvents.CompanionRemoved.AddNonSerializedListener((object)this, new Action<Hero, RemoveCompanionAction.RemoveCompanionDetail>(this.OnPlayerKickedCompanion));
            }

            protected void OnPlayerKickedCompanion(
              Hero companion,
              RemoveCompanionAction.RemoveCompanionDetail detail)
            {
                ++this._cachedCompanionCount;
            }

            protected void OnPlayerRecruitedCompanion(Hero companion) => --this._cachedCompanionCount;

            protected void OnHeroKilled(
              Hero victim,
              Hero killer,
              KillCharacterAction.KillCharacterActionDetail detail,
              bool showNotification = true)
            {
                if (!victim.IsWanderer || victim.IsPlayerCompanion)
                    return;
                --this._cachedCompanionCount;
            }

            protected void DailyTick()
            {
                this.SwapCompanions();
                this.SpawnNewCompanionIfNeeded();
            }

            protected void SpawnNewCompanionIfNeeded()
            {
                if ((double)this._cachedCompanionCount >= (double)Town.AllTowns.Count * 0.600000023841858)
                    return;
                IEnumerable<Town> e = Town.AllTowns.Where<Town>((Func<Town, bool>)(x => !x.Settlement.HeroesWithoutParty.AnyQ<Hero>((Func<Hero, bool>)(y => y.IsWanderer && !y.IsPlayerCompanion))));
                this.CreateCompanionAndAddToSettlement((e != null ? e.GetRandomElementInefficiently<Town>().Settlement : (Settlement)null) ?? Town.AllTowns.GetRandomElement<Town>().Settlement);
            }

            protected void SwapCompanions()
            {
                int maxValue = Town.AllTowns.Count / 5;
                int num = MBRandom.RandomInt(Town.AllTowns.Count % 5);
                Town allTown1 = Town.AllTowns[num + MBRandom.RandomInt(maxValue)];
                Hero hero1 = allTown1.Settlement.HeroesWithoutParty.Where<Hero>((Func<Hero, bool>)(x => x.IsWanderer && x.CompanionOf == null)).GetRandomElementInefficiently<Hero>();
                for (int index = 1; index < 5; ++index)
                {
                    Town allTown2 = Town.AllTowns[index * maxValue + num + MBRandom.RandomInt(maxValue)];
                    IEnumerable<Hero> heroes = allTown2.Settlement.HeroesWithoutParty.Where<Hero>((Func<Hero, bool>)(x => x.IsWanderer && x.CompanionOf == null));
                    Hero hero2 = (Hero)null;
                    if (heroes.Any<Hero>())
                    {
                        hero2 = heroes.GetRandomElementInefficiently<Hero>();
                        LeaveSettlementAction.ApplyForCharacterOnly(hero2);
                    }
                    if (hero1 != null)
                        EnterSettlementAction.ApplyForCharacterOnly(hero1, allTown2.Settlement);
                    hero1 = hero2;
                }
                if (hero1 == null)
                    return;
                EnterSettlementAction.ApplyForCharacterOnly(hero1, allTown1.Settlement);
            }

            public override void SyncData(IDataStore dataStore)
            {
            }

            protected void OnNewGameCreated(CampaignGameStarter starter)
            {
                this.InitializeCompanionTemplateList();
                List<Town> list = Town.AllTowns.ToList<Town>();
                list.Shuffle<Town>();
                for (int index = 0; (double)index < (double)list.Count * 0.600000023841858; ++index)
                    this.CreateCompanionAndAddToSettlement(list[index].Settlement);
            }

            protected void OnGameLoaded(CampaignGameStarter campaignGameStarter)
            {
                this.InitializeCompanionTemplateList();
                this._cachedCompanionCount = Hero.AllAliveHeroes.CountQ<Hero>((Func<Hero, bool>)(x => x.IsWanderer && !x.IsPlayerCompanion));
            }

            protected void AdjustEquipment(Hero hero)
            {
                this.AdjustEquipmentImp(hero.BattleEquipment);
                this.AdjustEquipmentImp(hero.CivilianEquipment);
            }

            protected void AdjustEquipmentImp(Equipment equipment)
            {
                ItemModifier itemModifier1 = MBObjectManager.Instance.GetObject<ItemModifier>("companion_armor");
                ItemModifier itemModifier2 = MBObjectManager.Instance.GetObject<ItemModifier>("companion_weapon");
                ItemModifier itemModifier3 = MBObjectManager.Instance.GetObject<ItemModifier>("companion_horse");
                for (EquipmentIndex index = EquipmentIndex.WeaponItemBeginSlot; index < EquipmentIndex.NumEquipmentSetSlots; ++index)
                {
                    EquipmentElement equipmentElement = equipment[index];
                    if (equipmentElement.Item != null)
                    {
                        if (equipmentElement.Item.ArmorComponent != null)
                            equipment[index] = new EquipmentElement(equipmentElement.Item, itemModifier1);
                        else if (equipmentElement.Item.HorseComponent != null)
                            equipment[index] = new EquipmentElement(equipmentElement.Item, itemModifier3);
                        else if (equipmentElement.Item.WeaponComponent != null)
                            equipment[index] = new EquipmentElement(equipmentElement.Item, itemModifier2);
                    }
                }
            }

            protected void InitializeCompanionTemplateList()
            {
                this._companionTemplates = new Dictionary<CharacterObject, int>();
                foreach (CultureObject objectType in MBObjectManager.Instance.GetObjectTypeList<CultureObject>())
                {
                    if (objectType.IsMainCulture)
                    {
                        foreach (CharacterObject key in objectType.NotableAndWandererTemplates.WhereQ<CharacterObject>((Func<CharacterObject, bool>)(x => x.Occupation == Occupation.Wanderer)))
                            this._companionTemplates.Add(key, 0);
                    }
                }
                foreach (Hero allAliveHero in Hero.AllAliveHeroes)
                {
                    if (allAliveHero.IsWanderer)
                    {
                        if (this._companionTemplates.ContainsKey(allAliveHero.Template))
                            this._companionTemplates[allAliveHero.Template]++;
                        else
                            this._companionTemplates.Add(allAliveHero.Template, 1);
                    }
                }
                foreach (Hero deadOrDisabledHero in Hero.DeadOrDisabledHeroes)
                {
                    if (deadOrDisabledHero.IsWanderer)
                    {
                        if (this._companionTemplates.ContainsKey(deadOrDisabledHero.Template))
                            this._companionTemplates[deadOrDisabledHero.Template]++;
                        else
                            this._companionTemplates.Add(deadOrDisabledHero.Template, 1);
                    }
                }
            }

            protected void CreateCompanionAndAddToSettlement(Settlement settlement)
            {
                List<(CharacterObject, float)> weightList = new List<(CharacterObject, float)>();
                foreach (KeyValuePair<CharacterObject, int> companionTemplate1 in this._companionTemplates)
                {
                    CharacterObject key = companionTemplate1.Key;
                    float num = (float)(2.0 / (Math.Pow((double)companionTemplate1.Value, 2.0) + 0.100000001490116));
                    weightList.Add((key, num));
                }
                CharacterObject companionTemplate = MBRandom.ChooseWeighted<CharacterObject>(weightList);
                this._companionTemplates[companionTemplate]++;
                Settlement settlement1 = Town.AllTowns.GetRandomElementWithPredicate<Town>((Func<Town, bool>)(x => x.Culture == companionTemplate.Culture))?.Settlement;
                Settlement bornSettlement;
                if (settlement1 != null)
                {
                    List<Settlement> e = new List<Settlement>();
                    foreach (Village allVillage in (IEnumerable<Village>)Village.All)
                    {
                        if ((double)Campaign.Current.Models.MapDistanceModel.GetDistance(allVillage.Settlement, settlement1) < 30.0)
                            e.Add(allVillage.Settlement);
                    }
                    bornSettlement = e.Count > 0 ? e.GetRandomElement<Settlement>().Village.Bound : settlement1;
                }
                else
                    bornSettlement = Town.AllTowns.GetRandomElement<Town>().Settlement;
                Hero specialHero = HeroCreator.CreateSpecialHero(companionTemplate, bornSettlement, age: (Campaign.Current.Models.AgeModel.HeroComesOfAge + 5 + MBRandom.RandomInt(27)));
                this.AdjustEquipment(specialHero);
                specialHero.ChangeState(Hero.CharacterStates.Active);
                EnterSettlementAction.ApplyForCharacterOnly(specialHero, settlement);
                ++this._cachedCompanionCount;
            }*/
    }
}

