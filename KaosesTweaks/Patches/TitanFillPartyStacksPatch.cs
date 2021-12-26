using HarmonyLib;
using Helpers;
using KaosesTweaks.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(MobileParty), "FillPartyStacks")]
    public class TitanFillPartyStacksPatch
    {
        private static bool HandlePartySizeMultipliers(ref MobileParty __instance, PartyTemplateObject pt, int troopNumberLimit)
        {
            if (MCMSettings.Instance is { } settings && settings.PartySizeMultipliersEnabled)
            {
                if (__instance.IsBandit || __instance.IsBanditBossParty)
                {
                    float num1 = (float)(0.400000005960464 + 0.800000011920929 * MiscHelper.GetGameProcess());
                    int num2 = MBRandom.RandomInt(2);
                    float num3 = num2 == 0 ? MBRandom.RandomFloat : (float)(MBRandom.RandomFloat * (double)MBRandom.RandomFloat * MBRandom.RandomFloat * 4.0);
                    float num4 = num2 == 0 ? (float)(num3 * 0.800000011920929 + 0.200000002980232) : 1f + num3;
                    float randomFloat1 = MBRandom.RandomFloat;
                    float randomFloat2 = MBRandom.RandomFloat;
                    float randomFloat3 = MBRandom.RandomFloat;
                    float f1 = pt.Stacks.Count > 0 ? pt.Stacks[0].MinValue + num1 * num4 * randomFloat1 * (pt.Stacks[0].MaxValue - pt.Stacks[0].MinValue) : 0.0f;
                    float f2 = pt.Stacks.Count > 1 ? pt.Stacks[1].MinValue + num1 * num4 * randomFloat2 * (pt.Stacks[1].MaxValue - pt.Stacks[1].MinValue) : 0.0f;
                    float f3 = pt.Stacks.Count > 2 ? pt.Stacks[2].MinValue + num1 * num4 * randomFloat3 * (pt.Stacks[2].MaxValue - pt.Stacks[2].MinValue) : 0.0f;
                    f1 *= settings.PartySizeBanditMultiplier;
                    f2 *= settings.PartySizeBanditMultiplier;
                    f3 *= settings.PartySizeBanditMultiplier;
                    __instance.AddElementToMemberRoster(pt.Stacks[0].Character, MBRandom.RoundRandomized(f1));
                    if (pt.Stacks.Count > 1)
                        __instance.AddElementToMemberRoster(pt.Stacks[1].Character, MBRandom.RoundRandomized(f2));
                    if (pt.Stacks.Count <= 2)
                        return false;
                    __instance.AddElementToMemberRoster(pt.Stacks[2].Character, MBRandom.RoundRandomized(f3));
                    return false;
                }

                if (__instance.IsVillager)
                {
                    for (int index = 0; index < pt.Stacks.Count; ++index)
                        __instance.AddElementToMemberRoster(pt.Stacks[0].Character, (int)(troopNumberLimit * settings.PartySizeVillagerMultiplier));
                    return false;
                }

                if (__instance.IsMilitia)
                {
                    if (troopNumberLimit < 0)
                    {
                        float gameProcess = MiscHelper.GetGameProcess();
                        for (int index = 0; index < pt.Stacks.Count; ++index)
                        {
                            int numberToAdd = (int)(gameProcess * (double)(pt.Stacks[index].MaxValue - pt.Stacks[index].MinValue)) + pt.Stacks[index].MinValue;
                            __instance.AddElementToMemberRoster(pt.Stacks[index].Character, numberToAdd * (int)settings.PartySizeMilitiaMultiplier);
                        }
                    }
                    else
                    {
                        for (int index1 = 0; index1 < troopNumberLimit; ++index1)
                        {
                            int index2 = -1;
                            float num5 = 0.0f;
                            for (int index3 = 0; index3 < pt.Stacks.Count; ++index3)
                                num5 += (float)((!__instance.IsGarrison || !pt.Stacks[index3].Character.IsRanged ? (!__instance.IsGarrison || pt.Stacks[index3].Character.IsMounted ? 1.0 : 2.0) : 6.0) * ((pt.Stacks[index3].MaxValue + pt.Stacks[index3].MinValue) / 2.0));
                            float num6 = MBRandom.RandomFloat * num5;
                            for (int index4 = 0; index4 < pt.Stacks.Count; ++index4)
                            {
                                num6 -= (float)((!__instance.IsGarrison || !pt.Stacks[index4].Character.IsRanged ? (!__instance.IsGarrison || pt.Stacks[index4].Character.IsMounted ? 1.0 : 2.0) : 6.0) * ((pt.Stacks[index4].MaxValue + pt.Stacks[index4].MinValue) / 2.0));
                                if (num6 < 0.0)
                                {
                                    index2 = index4;
                                    break;
                                }
                            }
                            if (index2 < 0)
                                index2 = 0;
                            __instance.AddElementToMemberRoster(pt.Stacks[index2].Character, 1 * (int)settings.PartySizeMilitiaMultiplier);
                        }
                    }

                    return false;
                }
            }
            return true;
        }

        private static bool HandlePartyCarvanSize(ref MobileParty __instance, PartyTemplateObject pt, int troopNumberLimit)
        {
            if (MCMSettings.Instance is { } settings && settings.PlayerCaravanPartySizeTweakEnabled)
            {
                if (__instance.IsCaravan && __instance.Party.Owner != null && __instance.Party.Owner == Hero.MainHero)
                {
                    troopNumberLimit = settings.PlayerCaravanPartySize;
                    if (troopNumberLimit < 0)
                    {
                        float gameProcess = MiscHelper.GetGameProcess();
                        for (int index = 0; index < pt.Stacks.Count; ++index)
                        {
                            int numberToAdd = (int)(gameProcess * (double)(pt.Stacks[index].MaxValue - pt.Stacks[index].MinValue)) + pt.Stacks[index].MinValue;
                            __instance.AddElementToMemberRoster(pt.Stacks[index].Character, numberToAdd);
                        }
                    }
                    else
                    {
                        for (int index1 = 0; index1 < troopNumberLimit; ++index1)
                        {
                            int index2 = -1;
                            float num5 = 0.0f;
                            for (int index3 = 0; index3 < pt.Stacks.Count; ++index3)
                                num5 += (float)((!__instance.IsGarrison || !pt.Stacks[index3].Character.IsRanged ? (!__instance.IsGarrison || pt.Stacks[index3].Character.IsMounted ? 1.0 : 2.0) : 6.0) * ((pt.Stacks[index3].MaxValue + pt.Stacks[index3].MinValue) / 2.0));
                            float num6 = MBRandom.RandomFloat * num5;
                            for (int index4 = 0; index4 < pt.Stacks.Count; ++index4)
                            {
                                num6 -= (float)((!__instance.IsGarrison || !pt.Stacks[index4].Character.IsRanged ? (!__instance.IsGarrison || pt.Stacks[index4].Character.IsMounted ? 1.0 : 2.0) : 6.0) * ((pt.Stacks[index4].MaxValue + pt.Stacks[index4].MinValue) / 2.0));
                                if (num6 < 0.0)
                                {
                                    index2 = index4;
                                    break;
                                }
                            }
                            if (index2 < 0)
                                index2 = 0;
                            __instance.AddElementToMemberRoster(pt.Stacks[index2].Character, 1);
                        }
                    }

                    return false;
                }
            }
            return true;
        }

        static bool Prefix(ref MobileParty __instance, PartyTemplateObject pt, int troopNumberLimit)
        {
            bool result = true;
            result = HandlePartySizeMultipliers(ref __instance, pt, troopNumberLimit);
            result = HandlePartyCarvanSize(ref __instance, pt, troopNumberLimit);
            return result;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && (settings.PartySizeMultipliersEnabled || settings.PlayerCaravanPartySizeTweakEnabled);
    }
}
