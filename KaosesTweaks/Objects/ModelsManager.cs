using KaosesCommon.Utils;
using KaosesTweaks.Models;
using KaosesTweaks.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;

namespace KaosesTweaks.Objects
{
    public class ModelsManager
    {
        private CampaignGameStarter _campaignGameStarter;

        public ModelsManager(CampaignGameStarter campaignGameStarter)
        {
            _campaignGameStarter = campaignGameStarter;
        }

        public void AddGameModels()
        {
            if (_campaignGameStarter != null && Config.Instance is { } settings)
            {
                if (settings.MCMClanModifiers)
                {
                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loaded Kaoses Clan Model Override");
                    }
                    _campaignGameStarter.AddModel(new ClanTierModel());
                }
                if (settings.KaosesStaticSpeedModifiersEnabled || settings.KaosesDynamicSpeedModifiersEnabled)
                {

                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loaded Kaoses Party Speed model Model Override");
                    }
                    _campaignGameStarter.AddModel(new PartySpeedCalculatingModel());
                }
                if (settings.HideoutBattleTroopLimitTweakEnabled)
                {
                    /*
                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loaded Kaoses Bandit Density model Model Override");
                    }*/
                    //_campaignGameStarter.AddModel(new KaosesBanditDensityModel());
                }
                if ((settings.PartyWageTweaksEnabled && !settings.PartyWageTweaksHarmonyEnabled) || (settings.KingdomBalanceStrengthEnabled && !settings.KingdomBalanceStrengthHarmonyEnabled))
                {
                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loaded BT Wage model Model Override");
                    }
                    _campaignGameStarter.AddModel(new PartyWageModel());
                }
                if (settings.MCMArmy)
                {
                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loaded Kaoses Army Model Override");
                    }
                    _campaignGameStarter.AddModel(new ArmyManagementCalculationModel());
                }
                if (settings.MCMBattleRewardModifiers && !settings.BattleRewardModifiersPatchOnly)
                {
                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loading Kaoses Battle rewards Model");
                    }
                    _campaignGameStarter.AddModel(new BattleRewardModel());
                }
                if (settings.MCMCharacterDevlopmentModifiers || Factory.Settings.LearningRateMultiplier != 1.0 || Factory.Settings.LearningLimitEnabled)
                {
                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loaded Kaoses Character Development Model Override");
                    }
                    _campaignGameStarter.AddModel(new CharacterDevelopmentModel());
                }
                if (settings.MCMPregnancyModifiers)
                {
                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loaded Kaoses Pregnancy Model Override");
                    }
                    _campaignGameStarter.AddModel(new PregnancyModel());
                }
                if (settings.MCMSmithingModifiers && !settings.MCMSmithingHarmoneyPatches)
                {
                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loaded Kaoses Smithing Model Override");
                    }
                    _campaignGameStarter.AddModel(new SmithingModel());
                }
                if (settings.PartyFoodConsumptionEnabled)
                {
                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loaded Kaoses party Food Consumption Model Override");
                    }
                    _campaignGameStarter.AddModel(new MobilePartyFoodConsumptionModel());
                }
                if (settings.DifficultyTweakEnabled)
                {
                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loaded BT Difficulty Model Override");
                    }
                    _campaignGameStarter.AddModel(new DifficultyModel());
                }
                if (settings.SettlementMilitiaEliteSpawnRateBonusEnabled)
                {
                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loaded BT Settlement Militia Model Override");
                    }
                    _campaignGameStarter.AddModel(new SettlementMilitiaModel());
                }
                if (settings.AgeTweaksEnabled)
                {
                    AgeModel model = new();
                    List<string> configErrors = model.GetConfigErrors().ToList();

                    if (configErrors.Any())
                    {
                        StringBuilder sb = new();
                        sb.AppendLine("There is a configuration error in the \'Age\' tweaks from Bannerlord Tweaks.");
                        sb.AppendLine("Please check the below errors and fix the age settings in the settings menu:");
                        sb.AppendLine();
                        foreach (string? e in configErrors)
                            sb.AppendLine(e);
                        sb.AppendLine();
                        sb.AppendLine("The age tweaks will not be applied until these errors have been resolved.");
                        sb.Append("Note that this is only a warning message and not a crash.");
                        //MessageBox.Show(sb.ToString(), "Configuration Error in Age Tweaks");
                        IM.ShowMessageBox(sb.ToString(), "Configuration Error in Age Tweaks");
                    }
                    else
                    {
                        if (settings.IsDebug)
                        {
                            IM.MessageDebug("Loaded BT Age Model Override");
                        }
                        _campaignGameStarter.AddModel(new AgeModel());
                    }

                }
                if (settings.SiegeTweaksEnabled)
                {
                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loaded BT Siege Model Override");
                    }
                    _campaignGameStarter.AddModel(new SiegeEventModel());
                }
                if (settings.MaxWorkshopCountTweakEnabled || settings.WorkshopBuyingCostTweakEnabled || settings.WorkshopEffectivnessEnabled)
                {
                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loaded BT Workshop Model Override");
                    }
                    _campaignGameStarter.AddModel(new WorkshopModel());
                }
                if (settings.TroopExperienceTweakEnabled || settings.ArenaHeroExperienceMultiplierEnabled || settings.TournamentHeroExperienceMultiplierEnabled)
                {
                    if (settings.IsDebug)
                    {
                        IM.MessageDebug("Loaded BT ComabatXP Model Override");
                    }
                    _campaignGameStarter.AddModel(new CombatXpModel());
                }
            }
        }
    }
}
