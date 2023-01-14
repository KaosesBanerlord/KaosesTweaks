using HarmonyLib;
using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CraftingSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using KaosesTweaks.Tweaks;
using System;
using System.Collections;
using System.Reflection;
using TaleWorlds.CampaignSystem.GameComponents;
using KaosesCommon.Utils;
using Helpers;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Party;

namespace KaosesTweaks.Patches
{
    //~ BT Tweaks
    [HarmonyPatch(typeof(MobilePartyHelper), "DesertTroopsFromParty")]
    public class DesertTroopsFromPartyPatch
    {
        private static MethodInfo? openPartMethodInfo;

        static bool Prefix(MobileParty party, int stackNo, int numberOfDeserters, int numberOfWoundedDeserters, ref TroopRoster desertedTroopList)
        {
            if (party.MemberRoster.Count <= stackNo)
            {
                TroopRosterElement elementCopyAtIndex = party.MemberRoster.GetElementCopyAtIndex(stackNo);
                party.MemberRoster.AddToCounts(elementCopyAtIndex.Character, -(numberOfDeserters + numberOfWoundedDeserters), false, -numberOfWoundedDeserters, 0, true, -1);
                if (desertedTroopList == null)
                {
                    desertedTroopList = TroopRoster.CreateDummyTroopRoster();
                }
                if (desertedTroopList != null)
                {
                    desertedTroopList.AddToCounts(elementCopyAtIndex.Character, numberOfDeserters + numberOfWoundedDeserters, false, numberOfWoundedDeserters, 0, true, -1);

                }
            }
            return false;
        }

        //static bool Prepare() => Factory.Settings is { } settings && (settings.SmithingEnergyDisable || settings.CraftingStaminaTweakEnabled) && Factory.Settings.MCMSmithingHarmoneyPatches;

        //private static void GetMethodInfo()
        //{
        //    openPartMethodInfo = typeof(CraftingCampaignBehavior).GetMethod("OpenPart", BindingFlags.NonPublic | BindingFlags.Instance);
        //}
    }
}
