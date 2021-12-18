using HarmonyLib;
using KaosesTweaks.Settings;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Barterables;
namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(JoinKingdomAsClanBarterable), "GetUnitValueForFaction")]
    class BTBarterablePatches
    {
        static void Postfix(ref int __result, IFaction factionForEvaluation, Kingdom ___TargetKingdom)
        {
            if (!(MCMSettings.Instance is { } settings)) return;

            Hero factionLeader = factionForEvaluation.Leader;

            if (___TargetKingdom.MapFaction == factionForEvaluation.MapFaction || ___TargetKingdom.MapFaction != Hero.MainHero.MapFaction || ___TargetKingdom.Leader != Hero.MainHero) return;

            if (factionLeader == null || factionLeader.IsFactionLeader) return;

            if (settings.BarterablesTweaksEnabled)
            {
                double cost = __result * settings.BarterablesJoinKingdomAsClanAdjustment;

                __result = (int)Math.Round(cost);
            }

            if (settings.BarterablesJoinKingdomAsClanAltFormulaEnabled)
            {
                __result /= 10;

                int relations = Hero.MainHero.GetRelation(factionLeader);
                if (relations > 100) relations = 99;

                double percent = Math.Abs(((double)relations / 100) - 1);

                double num2 = (relations > -1) ? (__result * percent) : __result * percent * 100;

                __result = (int)Math.Round(num2);
            }
        }

        static bool Prepare => MCMSettings.Instance is { } settings && settings.BarterablesTweaksEnabled;
    }
}
