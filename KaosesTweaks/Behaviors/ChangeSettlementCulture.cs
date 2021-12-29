using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace KaosesTweaks.Behaviors
{
    class ChangeSettlementCulture : CampaignBehaviorBase
    {
        private void OnSessionLaunched(CampaignGameStarter campaignGameStarter) => AddGameMenus(campaignGameStarter);

        public override void RegisterEvents()
        {
            CampaignEvents.ClanChangedKingdom.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(OnClanChangedKingdom));
            //CampaignEvents.ClanChangedKingdom.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, bool, bool>(this.OnClanChangedKingdom));
            CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(OnGameLoaded));
            //CampaignEvents.WeeklyTickSettlementEvent.AddNonSerializedListener(this, new Action<Settlement>(this.OnWeeklyTickSettlement));
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(OnSessionLaunched));
            CampaignEvents.DailyTickSettlementEvent.AddNonSerializedListener(this, new Action<Settlement>(OnDailyTickSettlement));
            CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(OnGameLoaded));
            CampaignEvents.OnSiegeAftermathAppliedEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement, SiegeAftermathCampaignBehavior.SiegeAftermath, Clan, Dictionary<MobileParty, float>>(OnSiegeAftermathApplied));
            CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(OnSettlementOwnerChanged));
        }

        private void OnGameLoaded(CampaignGameStarter obj)
        {
            Dictionary<Settlement, CultureObject> startingCultures = new();

            if (MCMSettings.Instance is { } settings)
            {
                UpdatePlayerOverride();
            }

            foreach (Settlement settlement in from settlement in Campaign.Current.Settlements where settlement.IsTown || settlement.IsCastle || settlement.IsVillage select settlement)
            {
                startingCultures.Add(settlement, settlement.Culture);
                if (MCMSettings.Instance is { } settings2)
                {
                    bool PlayerOverride = settlement.OwnerClan == Clan.PlayerClan && OverrideCulture != settlement.Culture;
                    bool KingdomOverride = settlement.OwnerClan != Clan.PlayerClan && settings2.ChangeToKingdomCulture && settlement.OwnerClan.Kingdom != null && settlement.OwnerClan.Kingdom.Culture != settlement.Culture;
                    bool ClanCulture = settlement.OwnerClan != Clan.PlayerClan && (!settings2.ChangeToKingdomCulture || settlement.OwnerClan.Kingdom == null) && settlement.OwnerClan.Culture != settlement.Culture;

                    if ((PlayerOverride || KingdomOverride || ClanCulture) && !WeekCounter.ContainsKey(settlement))
                    {
                        AddCounter(settlement);
                    }
                    else if ((PlayerOverride || KingdomOverride || ClanCulture) && IsSettlementDue(settlement))
                    {
                        Transform(settlement, false);
                    }
                }
            }
            initialCultureDictionary = startingCultures;
        }

        private void AddGameMenus(CampaignGameStarter campaignGameStarter)
        {
            campaignGameStarter.AddGameMenuOption("village", "village_culture_changer", "Culture Transformation", new GameMenuOption.OnConditionDelegate(Game_menu_village_change_culture_on_condition), new GameMenuOption.OnConsequenceDelegate(Game_menu_change_culture_on_consequence), false, 5, false);
            campaignGameStarter.AddGameMenuOption("town", "town_culture_changer", "Culture Transformation", new GameMenuOption.OnConditionDelegate(Game_menu_town_change_culture_on_condition), new GameMenuOption.OnConsequenceDelegate(Game_menu_change_culture_on_consequence), false, 5, false);
            campaignGameStarter.AddGameMenuOption("castle", "castle_culture_changer", "Culture Transformation", new GameMenuOption.OnConditionDelegate(Game_menu_castle_change_culture_on_condition), new GameMenuOption.OnConsequenceDelegate(Game_menu_change_culture_on_consequence), false, 5, false);
        }

        private void OnSiegeAftermathApplied(MobileParty arg1, Settlement settlement, SiegeAftermathCampaignBehavior.SiegeAftermath arg3, Clan arg4, Dictionary<MobileParty, float> arg5)
        {
            AddCounter(settlement);
        }

        private void OnSettlementOwnerChanged(Settlement settlement, bool arg2, Hero arg3, Hero arg4, Hero arg5, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
        {

            if (detail != ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail.BySiege)
            {
                if (settlement.OwnerClan == Clan.PlayerClan)
                {
                    UpdatePlayerOverride();
                }
                AddCounter(settlement);
            }
            else
            {
                settlement.Culture = initialCultureDictionary[settlement];
            }
        }


        // Token: 0x06000E45 RID: 3653 RVA: 0x000630F6 File Offset: 0x000612F6
        private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
        {
            if (MCMSettings.Instance is { } settings && settings.ChangeToKingdomCulture)
            {
                if (clan == Clan.PlayerClan)
                {
                    UpdatePlayerOverride();
                }
                else if (clan.Kingdom == null || clan.Kingdom.Culture != clan.Culture)
                {
                    foreach (Settlement settlement in from settlement in clan.Settlements where settlement.IsTown || settlement.IsCastle || settlement.IsVillage select settlement)
                    {
                        AddCounter(settlement);
                    }
                }
            }
        }

        public void Transform(Settlement settlement, bool removeTroops)
        {


            if (MCMSettings.Instance is { } settings && settlement.OwnerClan == Clan.PlayerClan)
            {
                UpdatePlayerOverride();
            }
            if (settlement.IsVillage || settlement.IsCastle || settlement.IsTown)
            {
                if (MCMSettings.Instance is { } settings2)
                {
                    bool PlayerOverride = settlement.OwnerClan == Clan.PlayerClan && OverrideCulture != settlement.Culture;
                    bool KingdomOverride = settlement.OwnerClan != Clan.PlayerClan && settings2.ChangeToKingdomCulture && settlement.OwnerClan.Kingdom != null && settlement.OwnerClan.Kingdom.Culture != settlement.Culture;
                    bool ClanCulture = settlement.OwnerClan != Clan.PlayerClan && (!settings2.ChangeToKingdomCulture || settlement.OwnerClan.Kingdom == null) && settlement.OwnerClan.Culture != settlement.Culture;

                    if (PlayerOverride || KingdomOverride || ClanCulture)
                    {
                        CultureObject? newculture = (settlement.OwnerClan == Clan.PlayerClan) ? OverrideCulture : (settings2.ChangeToKingdomCulture && settlement.OwnerClan.Kingdom != null) ? settlement.OwnerClan.Kingdom.Culture : settlement.OwnerClan.Culture;
                        if (newculture != null)
                        {
                            //dont switch last town of a culture to prevent bugs in vanilla
                            int count = 0;
                            if (settlement.IsTown)
                            {
                                foreach (Settlement Town in Campaign.Current.Settlements)
                                {
                                    if (Town.IsTown && Town.Culture == settlement.Culture)
                                    {
                                        count++;
                                    }
                                }
                            }
                            if (count != 1)
                            {
                                settlement.Culture = newculture;
                                if (removeTroops)
                                {
                                    RemoveTroopsfromNotable(settlement);
                                }
                                foreach (Village boundVillage in settlement.BoundVillages)
                                {
                                    if (removeTroops)
                                    {
                                        Transform(boundVillage.Settlement, true);
                                    }
                                    else
                                    {
                                        Transform(boundVillage.Settlement, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void UpdatePlayerOverride()
        {
            if (MCMSettings.Instance is { } settings)
            {
                OverrideCulture = null;
                foreach (CultureObject Culture in from kingdom in Campaign.Current.Kingdoms where settings.PlayerCultureOverride.SelectedValue == kingdom.Culture.StringId || (settings.PlayerCultureOverride.SelectedValue == "khergit" && kingdom.Culture.StringId == "rebkhu") select kingdom.Culture)
                {
                    OverrideCulture = Culture;
                    break;
                }
                if (OverrideCulture == null && settings.ChangeToKingdomCulture && Clan.PlayerClan.Kingdom != null)
                {
                    OverrideCulture = Clan.PlayerClan.Kingdom.Culture;
                }
                else if (OverrideCulture == null)
                {
                    OverrideCulture = Clan.PlayerClan.Culture;
                }
            }
        }

        public static void RemoveTroopsfromNotable(Settlement settlement)
        {
            if ((settlement.IsTown || settlement.IsVillage) && settlement.Notables != null)
            {
                foreach (Hero notable in settlement.Notables)
                {
                    if (notable.CanHaveRecruits)
                    {
                        for (int index = 0; index < 6; index++)
                        {
                            notable.VolunteerTypes[index] = null;
                        }
                    }
                }
            }
        }

        public void OnDailyTickSettlement(Settlement settlement)
        {
            if (WeekCounter.ContainsKey(settlement))
            {
                Dictionary<Settlement, int> dictionary = WeekCounter;
                if (dictionary[settlement] / 7 <= Statics._settings.TimeToChanceCulture)
                {
                    dictionary[settlement]++;
                    if (Statics._settings.CultureChangeDebug)
                    {
                        IM.MessageDebug($"OnDailyTickSettlement : {settlement.Name.ToString()} counter: {dictionary[settlement].ToString()}");
                        IM.MessageDebug($"OnDailyTickSettlement condition: {(dictionary[settlement] / 7 <= Statics._settings.TimeToChanceCulture).ToString()} ");
                        IM.MessageDebug($"OnDailyTickSettlement (dictionary[settlement] / 7) : {(dictionary[settlement] / 7).ToString()} ");
                        IM.MessageDebug($"OnDailyTickSettlement TimeToChanceCulture: {Statics._settings.TimeToChanceCulture.ToString()} ");
                    }

                    if (IsSettlementDue(settlement))
                    {
                        Transform(settlement, true);
                    }
                }

            }
        }

        public void OnWeeklyTickSettlement(Settlement settlement)
        {
            if (WeekCounter.ContainsKey(settlement))
            {
                Dictionary<Settlement, int> dictionary = WeekCounter;
                dictionary[settlement]++;
                if (Statics._settings.CultureChangeDebug)
                {
                    IM.MessageDebug($"OnWeeklyTickSettlement : {settlement.Name.ToString()} Added 1 week : {dictionary[settlement].ToString()} ");
                }

                if (IsSettlementDue(settlement))
                {
                    Transform(settlement, true);
                }
            }
        }

        public bool IsSettlementDue(Settlement settlement)
        {
            if (MCMSettings.Instance is { } settings && settings.TimeToChanceCulture > 0)
            {
                return WeekCounter[settlement] / 7 >= settings.TimeToChanceCulture;
            }
            else
            {
                return false;
            }
        }

        public void AddCounter(Settlement settlement)
        {
            if (settlement.IsVillage || settlement.IsCastle || settlement.IsTown)
            {
                if (WeekCounter.ContainsKey(settlement))
                {
                    if (Statics._settings.CultureChangeDebug)
                    {
                        IM.MessageDebug($"AddCounter : {settlement.Name.ToString()} set exisiting");
                    }
                    WeekCounter[settlement] = 0;
                }
                else
                {
                    if (Statics._settings.CultureChangeDebug)
                    {
                        IM.MessageDebug($"AddCounter : {settlement.Name.ToString()} add new");
                    }
                    WeekCounter.Add(settlement, 0);
                }
            }
        }

        public override void SyncData(IDataStore dataStore)
        {
            dataStore.SyncData("SettlementCultureTransformation", ref WeekCounter);
        }

        public static bool Game_menu_castle_change_culture_on_condition(MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Manage;
            return Settlement.CurrentSettlement.IsCastle;
        }
        public static bool Game_menu_town_change_culture_on_condition(MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Manage;
            return Settlement.CurrentSettlement.IsTown;
        }

        public static bool Game_menu_village_change_culture_on_condition(MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Manage;
            return Settlement.CurrentSettlement.IsVillage;
        }

        public static void Game_menu_change_culture_on_consequence(MenuCallbackArgs args)
        {
            if (MCMSettings.Instance is { } settings)
            {
                bool PlayerOverride = Settlement.CurrentSettlement.OwnerClan == Clan.PlayerClan && OverrideCulture != Settlement.CurrentSettlement.Culture;
                bool KingdomOverride = Settlement.CurrentSettlement.OwnerClan != Clan.PlayerClan && settings.ChangeToKingdomCulture && Settlement.CurrentSettlement.OwnerClan.Kingdom != null && Settlement.CurrentSettlement.OwnerClan.Kingdom.Culture != Settlement.CurrentSettlement.Culture;
                bool ClanCulture = Settlement.CurrentSettlement.OwnerClan != Clan.PlayerClan && (!settings.ChangeToKingdomCulture || Settlement.CurrentSettlement.OwnerClan.Kingdom == null) && Settlement.CurrentSettlement.OwnerClan.Culture != Settlement.CurrentSettlement.Culture;

                if (!WeekCounter.ContainsKey(Settlement.CurrentSettlement))
                {
                    InformationManager.DisplayMessage(new InformationMessage("The people in " + Settlement.CurrentSettlement.Name + " already appraise their owners culture."));
                }
                else if (PlayerOverride || KingdomOverride || ClanCulture)
                {
                    InformationManager.DisplayMessage(new InformationMessage("The people in " + Settlement.CurrentSettlement.Name + " seem to adopt their owners culture in " + (settings.TimeToChanceCulture - (WeekCounter.ContainsKey(Settlement.CurrentSettlement) ? (WeekCounter[Settlement.CurrentSettlement] / 7) : 00)) + " weeks."));
                }
                else
                {
                    InformationManager.DisplayMessage(new InformationMessage("The people in " + Settlement.CurrentSettlement.Name + " already appraise their owners culture."));
                }
            }
        }

        private static Dictionary<Settlement, CultureObject> initialCultureDictionary = new();
        public static Dictionary<Settlement, int> WeekCounter = new();
        private static CultureObject? OverrideCulture = new();
    }
}
