using HarmonyLib;
using KaosesTweaks.Settings;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultSettlementFoodModel), "CalculateTownFoodStocksChange")]
    class SettlementFoodModelPatch
    {
        static void Postfix(Town town, ref ExplainedNumber __result)
        {
            if (MCMSettings.Instance is { } settings && settings.SettlementFoodBonusEnabled && !(town is null))
            {
                if (settings.SettlementProsperityFoodMalusTweakEnabled && settings.SettlementProsperityFoodMalusDivisor != 50)
                {
                    float malus = town.Owner.Settlement.Prosperity / 50f;
                    TextObject prosperityTextObj = GameTexts.FindText("str_prosperity", null);
                    __result.Add(malus, prosperityTextObj);

                    malus = -town.Owner.Settlement.Prosperity / settings.SettlementProsperityFoodMalusDivisor;

                    //IM.MessageDebug("Patches CalculateTownFoodStocksChange Tweak: " + settings.SettlementProsperityFoodMalusDivisor.ToString());

                    __result.Add(malus, prosperityTextObj);
                }
                if (town.IsCastle)
                {
                    __result.Add(Math.Abs(__result.ResultNumber) * (settings.CastleFoodBonus - 1), new TextObject("Military rations"));

                    //IM.MessageDebug("Patches CastleFoodBonus Tweak: " + settings.CastleFoodBonus.ToString());

                }


                else if (town.IsTown)
                {
                    __result.Add(Math.Abs(__result.ResultNumber) * (settings.TownFoodBonus - 1), new TextObject("Citizen food drive"));

                    //IM.MessageDebug("Patches TownFoodBonus Tweak: " + settings.TownFoodBonus.ToString());

                }
            }
            return;
        }
        static bool Prepare() => MCMSettings.Instance is { } settings && settings.SettlementFoodBonusEnabled;
    }
}
