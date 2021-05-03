using HarmonyLib;
using KaosesTweaks.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(UrbanCharactersCampaignBehavior), "SpawnUrbanCharactersAtGameStart")]

    public static class SpawnUrbanCharactersPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = new List<CodeInstruction>(instructions);
            list.RemoveRange(147, 3);
            return list.AsEnumerable<CodeInstruction>();
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && settings.UnlimitedWanderersPatch;
    }
}
