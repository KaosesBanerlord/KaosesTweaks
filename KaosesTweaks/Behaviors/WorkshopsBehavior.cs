using Helpers;
using KaosesCommon.Utils;
using KaosesTweaks.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace KaosesTweaks.Behaviors
{
    public class WorkshopsBehavior : WorkshopsCampaignBehavior
    {
        private const string TransactionStringID = "str_workshop_profits";
        private const int WorkShopCount = 4;
        private readonly Dictionary<ItemCategory, List<ItemObject>> _itemsInCategory = new Dictionary<ItemCategory, List<ItemObject>>();
        private Dictionary<Workshop, int> _playerOwnedWorkshopsDaysInBankruptcy = new Dictionary<Workshop, int>();

        public override void RegisterEvents()
        {
            CampaignEvents.DailyTickTownEvent.AddNonSerializedListener((object)this, new Action<Town>(this.DailyTickTown));
            CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener((object)this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
            CampaignEvents.OnNewGameCreatedPartialFollowUpEvent.AddNonSerializedListener((object)this, new Action<CampaignGameStarter, int>(this.OnNewGameCreatedPartialFollowUp));
            CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener((object)this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
            CampaignEvents.HeroKilledEvent.AddNonSerializedListener((object)this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
            CampaignEvents.WarDeclared.AddNonSerializedListener((object)this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
            CampaignEvents.OnWorkshopChangedEvent.AddNonSerializedListener((object)this, new Action<Workshop, Hero, WorkshopType>(this.OnWorkshopChanged));
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener((object)this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
        }

        private void OnNewGameCreated(CampaignGameStarter obj) => this.FillItemsInAllCategories();

        private void OnSessionLaunched(CampaignGameStarter obj) => this.FillItemsInAllCategories();

        public override void SyncData(IDataStore dataStore) => dataStore.SyncData<Dictionary<Workshop, int>>("_playerOwnedWorkshopsDaysInBankruptcy", ref this._playerOwnedWorkshopsDaysInBankruptcy);

        private void OnNewGameCreatedPartialFollowUp(CampaignGameStarter starter, int i)
        {
            if (i < 10)
                return;
            if (i == 10)
            {
                this.InitializeWorkshops();
                this.BuildWorkshopsAtGameStart();
            }
            if (i % 20 != 0)
                return;
            this.RunTownShopsAtGameStart();
        }

        private void FillItemsInAllCategories()
        {
            foreach (ItemObject objectType in Game.Current.ObjectManager.GetObjectTypeList<ItemObject>())
            {
                if (WorkshopsBehavior.IsProducable(objectType))
                {
                    ItemCategory itemCategory = objectType.ItemCategory;
                    if (itemCategory != null)
                    {
                        List<ItemObject> itemObjectList;
                        if (!this._itemsInCategory.TryGetValue(itemCategory, out itemObjectList))
                        {
                            itemObjectList = new List<ItemObject>();
                            this._itemsInCategory[itemCategory] = itemObjectList;
                        }
                        itemObjectList.Add(objectType);
                    }
                }
            }
        }

        private void OnWorkshopChanged(Workshop workshop, Hero oldOwner, WorkshopType oldType)
        {
            if (oldOwner == null || !oldOwner.IsHumanPlayerCharacter)
                return;
            this.RemoveBankruptcyIfExist(workshop);
        }

        private void RemoveBankruptcyIfExist(Workshop workshop)
        {
            if (!this._playerOwnedWorkshopsDaysInBankruptcy.ContainsKey(workshop))
                return;
            this._playerOwnedWorkshopsDaysInBankruptcy.Remove(workshop);
        }

        private static bool IsProducable(ItemObject item) => !item.MultiplayerItem && !item.NotMerchandise && !item.IsCraftedByPlayer;

        private void DailyTickTown(Town town)
        {
            if (town.InRebelliousState)
                return;
            foreach (Workshop workshop in town.Workshops)
            {
                this.RunTownWorkshop(town, workshop);
                this.HandleWorkshopConstruction(workshop);
                this.HandleDailyExpense(workshop);
            }
        }

        private void OnHeroKilled(
          Hero victim,
          Hero killer,
          KillCharacterAction.KillCharacterActionDetail detail,
          bool showNotification = true)
        {
            if (victim.IsHumanPlayerCharacter)
                return;
            foreach (Workshop workshop in victim.OwnedWorkshops.ToList<Workshop>())
            {
                Hero newOwner = Campaign.Current.Models.WorkshopModel.SelectNextOwnerForWorkshop(workshop.Settlement.Town, workshop, workshop.Owner);
                if (newOwner != null)
                    ChangeOwnerOfWorkshopAction.ApplyByDeath(workshop, newOwner);
            }
        }

        private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail declareWarDetail)
        {
            if (!Factory.Settings.KeepWorkshopsOnWarDeclaration)
            {
                IFaction faction3 = faction1 == Hero.MainHero.MapFaction ? faction1 : (faction2 == Hero.MainHero.MapFaction ? faction2 : (IFaction)null);
                if (faction3 == null)
                    return;
                IFaction faction4 = faction3 != faction1 ? faction1 : faction2;
                int count = Hero.MainHero.OwnedWorkshops.Count;
                List<Workshop> list = Hero.MainHero.OwnedWorkshops.ToList<Workshop>();
                for (int index = 0; index < count; ++index)
                {
                    Workshop workshop = list[index];
                    if (workshop != null && workshop.Settlement.MapFaction == faction4)
                    {
                        Hero newOwner = Campaign.Current.Models.WorkshopModel.SelectNextOwnerForWorkshop(workshop.Settlement.Town, workshop, workshop.Owner);
                        if (newOwner != null)
                        {
                            WorkshopType workshopType = this.DecideBestWorkshopType(workshop.Settlement, false, workshop.WorkshopType);
                            ChangeOwnerOfWorkshopAction.ApplyByWarDeclaration(workshop, newOwner, workshopType, Campaign.Current.Models.WorkshopModel.GetInitialCapital(1), true);
                        }
                    }
                }
            }
        }

        private void ChangeWorkshopOwnerByBankruptcy(Workshop workshop)
        {
            if (!Factory.Settings.KeepWorkshopsOnBankruptcy)
            {
                int sellingCost = Campaign.Current.Models.WorkshopModel.GetSellingCost(workshop);
                Hero newOwner = Campaign.Current.Models.WorkshopModel.SelectNextOwnerForWorkshop(workshop.Settlement.Town, workshop, workshop.Owner, sellingCost);
                if (newOwner == null)
                    return;
                WorkshopType workshopType = this.DecideBestWorkshopType(workshop.Settlement, false, workshop.WorkshopType);
                ChangeOwnerOfWorkshopAction.ApplyByBankruptcy(workshop, newOwner, workshopType, Campaign.Current.Models.WorkshopModel.GetInitialCapital(1), true, sellingCost);
            }
        }

        private void HandleDailyExpense(Workshop shop)
        {
            if (!shop.IsRunning)
                return;
            int num = shop.Expense;
            if (shop?.Owner.Clan?.Leader != null && shop.Owner.Clan.Leader == shop.Owner && shop.Owner.GetPerkValue(DefaultPerks.Trade.MarketDealer))
                num = MathF.Round((float)num * (1f + DefaultPerks.Trade.MarketDealer.PrimaryBonus));
            if (shop.Capital >= num)
            {
                shop.ChangeGold(-num);
                this.RemoveBankruptcyIfExist(shop);
            }
            else if (shop.CanBeDowngraded)
                shop.Downgrade();
            else
                this.DeclareBankruptcy(shop);
        }

        private void DeclareBankruptcy(Workshop workshop)
        {
            if (workshop.Owner.IsHumanPlayerCharacter)
            {
                if (!this._playerOwnedWorkshopsDaysInBankruptcy.ContainsKey(workshop))
                {
                    this._playerOwnedWorkshopsDaysInBankruptcy.Add(workshop, 1);
                }
                else
                {
                    this._playerOwnedWorkshopsDaysInBankruptcy[workshop]++;
                    if (this._playerOwnedWorkshopsDaysInBankruptcy[workshop] < Campaign.Current.Models.WorkshopModel.DaysForPlayerSaveWorkshopFromBankruptcy)
                        return;
                    this.ChangeWorkshopOwnerByBankruptcy(workshop);
                    this._playerOwnedWorkshopsDaysInBankruptcy.Remove(workshop);
                }
            }
            else
                this.ChangeWorkshopOwnerByBankruptcy(workshop);
        }

        private void HandleWorkshopConstruction(Workshop workshop)
        {
            if (workshop.ConstructionTimeRemained <= 0)
                return;
            workshop.ApplyDailyConstruction();
        }

        private EquipmentElement GetRandomItem(
          ItemCategory itemGroupBase,
          Town townComponent)
        {
            EquipmentElement randomItemAux = this.GetRandomItemAux(itemGroupBase, townComponent);
            return randomItemAux.Item != null ? randomItemAux : this.GetRandomItemAux(itemGroupBase);
        }

        private EquipmentElement GetRandomItemAux(
          ItemCategory itemGroupBase,
          Town townComponent = null)
        {
            float num1 = 0.0f;
            ItemObject itemObject1 = (ItemObject)null;
            ItemModifier itemModifier = (ItemModifier)null;
            List<ItemObject> itemObjectList;
            if (this._itemsInCategory.TryGetValue(itemGroupBase, out itemObjectList))
            {
                foreach (ItemObject itemObject2 in itemObjectList)
                {
                    if ((townComponent == null || this.IsItemPreferredForTown(itemObject2, townComponent)) && itemObject2.ItemCategory == itemGroupBase)
                    {
                        float num2 = (float)(1.0 / ((double)MathF.Max(100, itemObject2.Value) + 100.0));
                        if ((double)MBRandom.RandomFloat * ((double)num1 + (double)num2) >= (double)num1)
                            itemObject1 = itemObject2;
                        num1 += num2;
                    }
                }
                ItemModifierGroup itemModifierGroup = itemObject1?.ItemComponent?.ItemModifierGroup;
                if (itemModifierGroup != null)
                    itemModifier = itemModifierGroup.GetRandomItemModifierProductionScoreBased();
            }
            return new EquipmentElement(itemObject1, itemModifier);
        }

        private void OnSettlementOwnerChanged(
          Settlement settlement,
          bool openToClaim,
          Hero newSettlementOwner,
          Hero oldSettlementOwner,
          Hero capturerHero,
          ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
        {
            if (!settlement.IsTown)
                return;
            foreach (Workshop workshop in settlement.Town.Workshops)
            {
                if (workshop.Owner != null && workshop.Owner.MapFaction.IsAtWarWith(newSettlementOwner.MapFaction) && workshop.Owner.GetPerkValue(DefaultPerks.Trade.RapidDevelopment))
                    GiveGoldAction.ApplyBetweenCharacters((Hero)null, workshop.Owner, MathF.Round(DefaultPerks.Trade.RapidDevelopment.PrimaryBonus));
            }
        }

        private float FindTotalInputDensityScore(
          Settlement bornSettlement,
          WorkshopType workshop,
          IDictionary<ItemCategory, float> productionDict,
          bool atGameStart)
        {
            int num1 = 0;
            for (int index = 0; index < bornSettlement.Town.Workshops.Length; ++index)
            {
                if (bornSettlement.Town.Workshops[index].WorkshopType == workshop)
                    ++num1;
            }
            float num2 = 0.01f;
            float num3 = 0.0f;
            foreach (WorkshopType.Production production in (IEnumerable<WorkshopType.Production>)workshop.Productions)
            {
                bool flag = false;
                foreach ((ItemCategory, int) output in (IEnumerable<(ItemCategory, int)>)production.Outputs)
                {
                    if (output.Item1.IsTradeGood)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    foreach ((ItemCategory itemCategory, int num4) in (IEnumerable<(ItemCategory, int)>)production.Inputs)
                    {
                        float num5;
                        if (productionDict.TryGetValue(itemCategory, out num5))
                            num2 += num5 / (production.ConversionSpeed * (float)num4);
                        if (!atGameStart)
                        {
                            float priceFactor = bornSettlement.Town.MarketData.GetPriceFactor(itemCategory);
                            num3 += Math.Max(0.0f, 1f - priceFactor);
                        }
                    }
                }
            }
            float x = 1f + (float)(num1 * 6);
            return MathF.Pow(num2 * ((float)workshop.Frequency * (float)(1.0 / Math.Pow((double)x, 3.0))) + num3, 0.6f);
        }

        private void BuildWorkshopForHeroAtGameStart(Hero ownerHero)
        {
            Settlement bornSettlement = ownerHero.BornSettlement;
            WorkshopType workshopType = this.DecideBestWorkshopType(bornSettlement, true);
            if (workshopType == null)
                return;
            Hero hero = ownerHero;
            int index1 = -1;
            for (int index2 = 0; index2 < bornSettlement.Town.Workshops.Length; ++index2)
            {
                if (bornSettlement.Town.Workshops[index2].WorkshopType == null)
                {
                    index1 = index2;
                    break;
                }
            }
            if (index1 < 0)
                return;
            bornSettlement.Town.Workshops[index1].SetWorkshop(hero, workshopType, Campaign.Current.Models.WorkshopModel.GetInitialCapital(1));
            TextObject firstName;
            TextObject fullName;
            NameGenerator.Current.GenerateHeroNameAndHeroFullName(hero, out firstName, out fullName);
            hero.SetName(fullName, firstName);
        }

        private WorkshopType DecideBestWorkshopType(
          Settlement currentSettlement,
          bool atGameStart,
          WorkshopType workshopToExclude = null)
        {
            IDictionary<ItemCategory, float> productionDict = (IDictionary<ItemCategory, float>)new Dictionary<ItemCategory, float>();
            foreach (Village village in Village.All.Where<Village>((Func<Village, bool>)(x => x.TradeBound == currentSettlement)))
            {
                foreach ((ItemObject, float) production in (IEnumerable<(ItemObject, float)>)village.VillageType.Productions)
                {
                    ItemCategory key = production.Item1.ItemCategory;
                    if (key != DefaultItemCategories.Grain || village.VillageType.PrimaryProduction == DefaultItems.Grain)
                    {
                        float num1 = production.Item2;
                        if (key == DefaultItemCategories.Cow || key == DefaultItemCategories.Hog)
                            key = DefaultItemCategories.Hides;
                        if (key == DefaultItemCategories.Sheep)
                            key = DefaultItemCategories.Wool;
                        float num2;
                        if (productionDict.TryGetValue(key, out num2))
                            productionDict[key] = num2 + num1;
                        else
                            productionDict.Add(key, num1);
                    }
                }
            }
            Dictionary<WorkshopType, float> dictionary = new Dictionary<WorkshopType, float>();
            float num3 = 0.0f;
            foreach (WorkshopType workshopType in WorkshopType.All)
            {
                if (!workshopType.IsHidden && (workshopToExclude == null || workshopToExclude != workshopType))
                {
                    float inputDensityScore = this.FindTotalInputDensityScore(currentSettlement, workshopType, productionDict, atGameStart);
                    dictionary.Add(workshopType, inputDensityScore);
                    num3 += inputDensityScore;
                }
            }
            float num4 = num3 * MBRandom.RandomFloat;
            WorkshopType workshopType1 = (WorkshopType)null;
            foreach (WorkshopType key in WorkshopType.All)
            {
                if (!key.IsHidden && (workshopToExclude == null || workshopToExclude != key))
                {
                    num4 -= dictionary[key];
                    if ((double)num4 < 0.0)
                    {
                        workshopType1 = key;
                        break;
                    }
                }
            }
            if (workshopType1 == null)
                workshopType1 = WorkshopType.All[MBRandom.RandomInt(1, WorkshopType.All.Count)];
            return workshopType1;
        }

        private void InitializeWorkshops()
        {
            foreach (Town allTown in (IEnumerable<Town>)Town.AllTowns)
                allTown.InitializeWorkshops(4);
        }

        private void BuildWorkshopsAtGameStart()
        {
            foreach (Town allTown in (IEnumerable<Town>)Town.AllTowns)
            {
                this.BuildArtisanWorkshop(allTown);
                for (int index = 1; index < allTown.Workshops.Length; ++index)
                    this.BuildWorkshopForHeroAtGameStart(this.SelectRandomOwner(allTown));
            }
        }

        private void BuildArtisanWorkshop(Town town)
        {
            Hero newOwner = town.Settlement.Notables.FirstOrDefault<Hero>((Func<Hero, bool>)(x => x.IsArtisan)) ?? town.Settlement.Notables.FirstOrDefault<Hero>();
            if (newOwner == null)
                return;
            WorkshopType workshopType = WorkshopType.Find("artisans");
            town.Workshops[0].SetWorkshop(newOwner, workshopType, Campaign.Current.Models.WorkshopModel.GetInitialCapital(1), false);
        }

        private Hero SelectRandomOwner(Town town)
        {
            Hero hero = (Hero)null;
            Settlement settlement = town.Settlement;
            float num1 = 0.0f;
            foreach (Hero notable in settlement.Notables)
            {
                float num2 = notable.Power / MathF.Pow(10f, (float)notable.OwnedWorkshops.Count);
                num1 += num2;
            }
            float num3 = num1 * MBRandom.RandomFloat;
            foreach (Hero notable in settlement.Notables)
            {
                int count = notable.OwnedWorkshops.Count;
                float num4 = notable.Power / MathF.Pow(10f, (float)count);
                num3 -= num4;
                if ((double)num3 < 0.0)
                {
                    hero = notable;
                    break;
                }
            }
            return hero;
        }

        private void RunTownShopsAtGameStart()
        {
            foreach (Town allTown in (IEnumerable<Town>)Town.AllTowns)
            {
                foreach (Workshop workshop in allTown.Workshops)
                    this.RunTownWorkshop(allTown, workshop);
            }
        }

        private void RunTownWorkshop(Town townComponent, Workshop workshop, bool willBeSold = true)
        {
            if (!workshop.IsRunning || this._playerOwnedWorkshopsDaysInBankruptcy.ContainsKey(workshop))
                return;
            WorkshopType workshopType = workshop.WorkshopType;
            bool flag1 = false;
            bool flag2 = false;
            for (int index = 0; index < workshopType.Productions.Count; ++index)
            {
                float num1 = workshop.GetProductionProgress(index);
                if ((double)num1 > 1.0)
                    num1 = 1f;
                float effectToProduction = Campaign.Current.Models.WorkshopModel.GetPolicyEffectToProduction(townComponent);
                ExplainedNumber bonuses = new ExplainedNumber(workshopType.Productions[index].ConversionSpeed * effectToProduction);
                if (townComponent.Governor != null && townComponent.Governor.GetPerkValue(DefaultPerks.Trade.MercenaryConnections))
                    PerkHelper.AddPerkBonusForTown(DefaultPerks.Trade.MercenaryConnections, townComponent, ref bonuses);
                if (workshop.Owner != null)
                    PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Steward.Sweatshops, workshop.Owner.CharacterObject, true, ref bonuses);
                float num2 = num1 + bonuses.ResultNumber;
                if ((double)num2 >= 1.0)
                {
                    bool flag3 = true;
                    for (bool flag4 = true; flag4 && (double)num2 >= 1.0; --num2)
                    {
                        flag4 = this.DoProduction(workshopType.Productions[index], workshop, townComponent);
                        if (!flag4 & flag3)
                            flag1 = true;
                        else if (flag4)
                            flag2 = true;
                    }
                }
                workshop.SetProgress(index, num2);
            }
            if (flag2)
            {
                workshop.ResetNotRunnedDays();
            }
            else
            {
                if (!flag1)
                    return;
                workshop.IncreaseNotRunnedDays();
            }
        }

        private static bool DetermineTownHasSufficientInputs(
          WorkshopType.Production production,
          Town town,
          out int inputMaterialCost)
        {
            IReadOnlyList<(ItemCategory, int)> inputs = production.Inputs;
            inputMaterialCost = 0;
            foreach ((ItemCategory itemCategory, int a) in (IEnumerable<(ItemCategory, int)>)inputs)
            {
                ItemRoster itemRoster = town.Owner.ItemRoster;
                int tmp = 0;
                for (int index = 0; index < itemRoster.Count; ++index)
                {
                    ItemObject itemAtIndex = itemRoster.GetItemAtIndex(index);
                    if (itemAtIndex.ItemCategory == itemCategory)
                    {
                        int elementNumber = itemRoster.GetElementNumber(index);
                        int num = MathF.Min(a, elementNumber);
                        tmp -= num;
                        inputMaterialCost += town.GetItemPrice(itemAtIndex, (MobileParty)null, false) * num;
                    }
                }
                if (tmp > 0)
                    return false;
            }
            return true;
        }

        private bool DoProduction(WorkshopType.Production production, Workshop workshop, Town town)
        {
            List<(EquipmentElement, int)> source = new List<(EquipmentElement, int)>();
            IReadOnlyList<(ItemCategory, int)> inputs = production.Inputs;
            int inputMaterialCost;
            if (!WorkshopsBehavior.DetermineTownHasSufficientInputs(production, town, out inputMaterialCost))
                return false;
            int num1 = 0;
            int num2 = 0;
            for (int index1 = 0; index1 < production.Outputs.Count; ++index1)
            {
                int num3 = production.Outputs[index1].Item2;
                num1 += num3;
                for (int index2 = 0; index2 < num3; ++index2)
                {
                    EquipmentElement randomItem = this.GetRandomItem(production.Outputs[index1].Item1, town);
                    source.Add((randomItem, 1));
                    num2 += town.GetItemPrice(randomItem, (MobileParty)null, true);
                }
            }
            bool doNotEffectCapital = false;
            if (workshop.WorkshopType.Productions.Count > 1)
            {
                foreach ((ItemCategory, int) input in (IEnumerable<(ItemCategory, int)>)production.Inputs)
                {
                    if (input.Item1 != null && !input.Item1.IsTradeGood)
                    {
                        doNotEffectCapital = true;
                        break;
                    }
                }
                if (!doNotEffectCapital)
                {
                    foreach ((ItemCategory, int) output in (IEnumerable<(ItemCategory, int)>)production.Outputs)
                    {
                        if (output.Item1 != null && !output.Item1.IsTradeGood)
                        {
                            doNotEffectCapital = true;
                            break;
                        }
                    }
                }
            }
            float num4 = workshop.WorkshopType.IsHidden ? (float)inputMaterialCost : (float)inputMaterialCost + 200f / production.ConversionSpeed;
            if (Campaign.Current.GameStarted && (double)num2 <= (double)num4 || num2 > town.Gold && !doNotEffectCapital || inputMaterialCost > workshop.Capital || source.Sum<(EquipmentElement, int)>((Func<(EquipmentElement, int), int>)(t => t.Item2)) != num1)
                return false;
            foreach ((EquipmentElement, int) valueTuple in source)
            {
                Town town1 = town;
                WorkshopsBehavior.ProduceOutput(valueTuple.Item1, town1, workshop, valueTuple.Item2, doNotEffectCapital);
            }
            foreach ((ItemCategory, int) tuple in (IEnumerable<(ItemCategory, int)>)inputs)
                WorkshopsBehavior.ConsumeInput(tuple.Item1, town, workshop, doNotEffectCapital);
            return true;
        }

        private static void ProduceOutput(
          EquipmentElement outputItem,
          Town town,
          Workshop workshop,
          int count,
          bool doNotEffectCapital)
        {
            int itemPrice = town.GetItemPrice(outputItem, (MobileParty)null, false);
            town.Owner.ItemRoster.AddToCounts(outputItem, count);
            if (Campaign.Current.GameStarted && !doNotEffectCapital)
            {
                int goldChange = 0;
                if (Factory.Settings.EnableWorkshopSellTweak)
                {
                    float baseline = Math.Min(1000, itemPrice) * count;
                    float num = Math.Min(1000, itemPrice) * count * (Factory.Settings.WorkshopSellTweak - 1f);
                    if (Factory.Settings.WorkshopsDebug)
                    {
                        IM.MessageDebug("WorkShop Behavior: ProduceOutput: baseline: " + baseline + " Tweaked Amount " + num + "using sell tweak: " + Factory.Settings.WorkshopSellTweak);
                    }
                    goldChange = (int)num;
                }
                else
                {
                    goldChange = MathF.Min(1000, itemPrice) * count;
                }
                workshop.ChangeGold(goldChange);
                town.ChangeGold(-goldChange);
            }
            CampaignEventDispatcher.Instance.OnItemProduced(outputItem.Item, town.Owner.Settlement, count);
        }

        private static void ConsumeInput(
          ItemCategory productionInput,
          Town town,
          Workshop workshop,
          bool doNotEffectCapital)
        {
            ItemRoster itemRoster = town.Owner.ItemRoster;
            int index = itemRoster.FindIndex((Predicate<ItemObject>)(x => x.ItemCategory == productionInput));
            if (index < 0)
                return;
            ItemObject itemAtIndex = itemRoster.GetItemAtIndex(index);
            itemRoster.AddToCounts(itemAtIndex, -1);
            int itemPrice = town.GetItemPrice(itemAtIndex, (MobileParty)null, false);
            if (Campaign.Current.GameStarted && !doNotEffectCapital)
            {
                if (Factory.Settings.EnableWorkshopBuyTweak)
                {
                    float num = itemPrice * (Factory.Settings.WorkshopBuyTweak - 1f);
                    itemPrice = (int)num;
                    if (Factory.Settings.WorkshopsDebug)
                    {
                        IM.MessageDebug("WorkShop Behavior: ProduceOutput: itemPrice: " + itemPrice + " Tweaked Amount " + num + "using buy tweak: " + Factory.Settings.WorkshopBuyTweak);
                    }
                }
                workshop.ChangeGold(-itemPrice);
                town.ChangeGold(itemPrice);
            }
            CampaignEventDispatcher.Instance.OnItemConsumed(itemAtIndex, town.Owner.Settlement, 1);
        }

        private bool IsItemPreferredForTown(ItemObject item, Town townComponent) => item.Culture == null || item.Culture.StringId == "neutral_culture" || item.Culture == townComponent.Culture;
    }
}
