using HarmonyLib;
using KaosesTweaks.Settings;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.CampaignBehaviors;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(NotablesCampaignBehavior), "SpawnNotablesAtGameStart")]

    public static class SpawnNotablesAtGameStartPatch
    {

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = new List<CodeInstruction>(instructions);
            if (list.Count == 165)
            {
                list.RemoveRange(147, 3);
            }
            return list.AsEnumerable<CodeInstruction>();
        }

        public static void Postfix()
        {
            /*
                        if ((MCMSettings.Instance is { } settings && settings.ProductionTweakEnabled))
                        {
                            if (Statics._settings.SettlementsDebug)
                            {

                                IM.MessageDebug("DailyProductionAmount: original : " + __result.ToString() + "\r\n"
                                    + " OtherTweakAmount " + settings.ProductionOtherTweakAmount.ToString() + "\r\n"
                                    + " final " + (__result * settings.ProductionOtherTweakAmount).ToString() + "\r\n"
                                    );
                            }
                            __result *= settings.ProductionOtherTweakAmount;
                        }

                        if  (Campaign.Current.AliveHeroes != null && Statics._settings.WandererLocationDebug)
                        {
                            //Dictionary<Hero, string> wList = new Dictionary<Hero, string>();
                            Dictionary<string, string> wList = new Dictionary<string, string>();
                            foreach (Hero hero in Campaign.Current.AliveHeroes)
                            {
                                if (hero != null)
                                {
                                    if (hero.CharacterObject.Occupation == Occupation.Wanderer && hero != null)
                                    {
                                        if (hero.CurrentSettlement != null)
                                        {
                                            if (!wList.ContainsKey(hero.Name.ToString()))
                                            {
                                                wList.Add(hero.Name.ToString(), hero.CurrentSettlement.Name.ToString());
                                            }
                                            //IM.MessageDebug("Wanderer Name: " + hero.Name.ToString() + "   CurrentSettlement: " +hero.CurrentSettlement.Name.ToString());
                                        }
                                    }
                                }
                            }

                            foreach (KeyValuePair<string, string> entry in wList)
                            {
                                IM.MessageDebug("Wanderer Name: " + entry.Key.ToString() + "   CurrentSettlement: " + entry.Value.ToString());
                            }
                        }*/
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.UnlimitedWanderersPatch;
    }
}
