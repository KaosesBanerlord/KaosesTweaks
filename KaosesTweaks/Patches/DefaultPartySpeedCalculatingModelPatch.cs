using HarmonyLib;
using KaosesTweaks;
using System;
using TaleWorlds.CampaignSystem;
/*
namespace PartySpeeds.Patches
{
    [HarmonyPatch(typeof(DefaultPartySpeedCalculatingModel), "CalculateFinalSpeed")]
    public class CalculateFinalSpeedPatch
    {
        public static void Postfix(MobileParty mobileParty, ref ExplainedNumber __result)
        {
            if (Factory.Settings.KaosesStaticSpeedModifiersEnabled)
            {
                KaosesPartySpeed partySpeed = new KaosesPartySpeed(mobileParty);
                if (partySpeed.HasPartyModifiedSpeed())
                {
                    __result.Add(partySpeed.ModifiedPartySpeed(), partySpeed.ExplainationMessage());
                }
            }

            if (Factory.Settings.KaosesDynamicSpeedModifiersEnabled)
            {
                float reduction = 0f;
                if (mobileParty.ShortTermBehavior == AiBehavior.FleeToPoint)
                {
                    if (SubModule.FleeingParties.ContainsKey(mobileParty))
                    {
                        CampaignTime oldTime = SubModule.FleeingParties[mobileParty];
                        if (CampaignTime.Now.ToHours > oldTime.ToHours)
                        {
                            int fleeingHours = SubModule.FleeingHours[mobileParty];
                            reduction = Factory.Settings.DynamicFleeingSpeedReductionAmount * fleeingHours;
                            SubModule.FleeingHours[mobileParty] = fleeingHours + 1;
                            SubModule.FleeingParties[mobileParty] = CampaignTime.HoursFromNow(Factory.Settings.DynamicFleeingSpeedReductionHours);
                            SubModule.FleeingSpeedReduction[mobileParty] = reduction;
                        }else
                        {
                            if (SubModule.FleeingSpeedReduction.ContainsKey(mobileParty))
                            {
                                reduction = SubModule.FleeingSpeedReduction[mobileParty];
                            }
                        }
                    }
                    else
                    {
                        SubModule.FleeingParties.Add(mobileParty, CampaignTime.HoursFromNow(Factory.Settings.DynamicFleeingSpeedReductionHours));
                        SubModule.FleeingHours.Add(mobileParty, 1);
                        SubModule.FleeingSpeedReduction.Add(mobileParty, 0.0f);
                    }
                    if (reduction != 0)
                    {
                        __result.Add(reduction, null);
                    }
                }
                else
                {
                    if (SubModule.FleeingParties.ContainsKey(mobileParty))
                    {
                        CampaignTime oldTime = SubModule.FleeingParties[mobileParty];
                        if (CampaignTime.Now.ToHours > oldTime.ToHours)
                        {
                            SubModule.FleeingParties.Remove(mobileParty);
                            if (SubModule.FleeingHours.ContainsKey(mobileParty))
                            {
                                SubModule.FleeingHours.Remove(mobileParty);
                            }
                            if (SubModule.FleeingSpeedReduction.ContainsKey(mobileParty))
                            {
                                SubModule.FleeingSpeedReduction.Remove(mobileParty);
                            }
                        }
                    }
                }
            }
            __result.LimitMin(Factory.Settings.KaosesmininumSpeedAmount);
        }
        static bool Prepare() => Factory.Settings is { } settings && (settings.KaosesDynamicSpeedModifiersEnabled || Factory.Settings.KaosesStaticSpeedModifiersEnabled);
    }















    [HarmonyPatch(typeof(DefaultPartySpeedCalculatingModel), "CalculatePureSpeed")]
    public class CalculatePureSpeedPatch
    {
        public static void Postfix(MobileParty mobileParty, bool includeDescriptions, int additionalTroopOnFootCount, int additionalTroopOnHorseCount, ref ExplainedNumber __result)
        {
            //IM.DebugMessage($"CalculateFinalSpeed Postfix called");


            //int fleeingHours = SubModule.FleeingHours[mobileParty];
            //SubModule.FleeingHours.TryGetValue(mobileParty, out fleeingHours);
            //IM.DebugMessage($"fleeingHours  : {fleeingHours}");
            //IM.DebugMessage($"-0.3f * fleeingHours  : {-0.3f * fleeingHours}");
            //IM.DebugMessage($"CalculatePureSpeed modification ResultNumber  : {__result.ResultNumber}");
            //finalSpeed.AddFactor(num2, PartySpeed._movingInForest);
            //float reduction = -0.3f * fleeingHours;
            //__result.AddFactor(-0.25f, null); ;
            //SubModule.FleeingHours[mobileParty] = fleeingHours + 1;
            //SubModule.FleeingParties[mobileParty] = CampaignTime.HoursFromNow(1f);
            //IM.DebugMessage($"CalculatePureSpeed ResultNumber  : {__result.ResultNumber}");
   

            //return false;
        }
        //static bool Prepare() => Factory.Settings is { } settings && settings.PatchFillStacks;
    }







}
*/