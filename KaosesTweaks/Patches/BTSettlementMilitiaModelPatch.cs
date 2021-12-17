using HarmonyLib;
using KaosesTweaks.Settings;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Localization;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultSettlementMilitiaModel), "CalculateMilitiaChange")]
    class BTSettlementMilitiaModelPatch
    {
        static void Postfix(Settlement settlement, ref ExplainedNumber __result)
        {
            if (MCMSettings.Instance is { } settings && settings.SettlementMilitiaBonusEnabled && !(settlement is null))
            {
                if (settlement.IsCastle)
                {
                    __result.Add(settlement.Militia * 0.025f, new TextObject("{=gHnfFi1s}Retired", null));
                    __result.Add(settings.CastleMilitiaRetirementModifier * -settlement.Militia, new TextObject("{=gHnfFi1s}Retired", null));
                    __result.Add(settings.CastleMilitiaBonusFlat, new TextObject("Recruitment drive"));
                }
                if (settlement.IsTown)
                {
                    __result.Add(settlement.Militia * 0.025f, new TextObject("{=gHnfFi1s}Retired", null));
                    __result.Add(settings.TownMilitiaRetirementModifier * -settlement.Militia, new TextObject("{=gHnfFi1s}Retired", null));
                    __result.Add(settings.TownMilitiaBonusFlat, new TextObject("Citizen militia"));
                }
                if (settlement.IsVillage)
                {
                    __result.Add(settlement.Militia * 0.025f, new TextObject("{=gHnfFi1s}Retired", null));
                    __result.Add(settings.VillageMilitiaRetirementModifier * -settlement.Militia, new TextObject("{=gHnfFi1s}Retired", null));
                }
            }
            return;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.SettlementMilitiaBonusEnabled;
    }
}
