using KaosesCommon.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem;

namespace KaosesTweaks.Objects.PartySpeeds
{

    public class FleeingPartiesManager
    {
        private bool _debug;
        private bool _enabled;


        public FleeingPartiesManager()//MobileParty mobilePary
        {
            _debug = Factory.Settings.IsDebug;
            _enabled = Factory.Settings.KaosesDynamicSpeedModifiersEnabled;
            //_mobilePary = mobilePary;
        }

        public void CheckPartyForChangingSpeed(MobileParty mobileParty, ref ExplainedNumber finalSpeed)
        {
            if (_enabled)
            {
                ConcurrentDictionary<string, KaosesFleeingPartySpeed> fleeingList = Factory.KaosesFleeingPartiesList;
                //float reduction = 0f;
                if (mobileParty.ShortTermBehavior == AiBehavior.FleeToPoint)
                {
                    DebugMessage(mobileParty.StringId + "  Is Fleeing");
                    KaosesFleeingPartySpeed fleeingParty = new(mobileParty.StringId);
                    if (fleeingList.ContainsKey(mobileParty.StringId))
                    {
                        bool retrieved = false;
                        while (!retrieved)
                        {
                            retrieved = fleeingList.TryGetValue(mobileParty.StringId, out fleeingParty);
                        }

                        //fleeingParty = fleeingList[mobileParty.StringId];
                    }
                    else
                    {
                        //System.NullReferenceException
                        //HResult = 0x80004003
                        //Message = Object reference not set to an instance of an object.
                        //Source = mscorlib
                        //StackTrace:
                        //at System.Collections.Generic.Dictionary`2.Insert(TKey key, TValue value, Boolean add)
                        //at KaosesPartySpeeds.Objects.FleeingPartiesManager.CheckPartyForChangingSpeed(MobileParty mobileParty, ExplainedNumber & finalSpeed) 
                        //in U:\BannerLord\MyMods\KaosesPartySpeeds\KaosesPartySpeeds\Objects\FleeingPartiesManager.cs:line 41

                        try
                        {
                            bool added = false;
                            while (!added)
                            {
                                added = fleeingList.TryAdd(mobileParty.StringId, fleeingParty);
                            }

                        }
                        catch (Exception ex)
                        {
                            IM.MessageDebug(ex.ToString());
                            IM.MessageDebug("stringId: " + mobileParty.StringId);
                            //IM.MessageDebug();
                            IM.MessageDebug(fleeingParty.ToString());
                        }

                    }
                    fleeingParty.CheckPartyToApplyFleeingPenalties(ref finalSpeed);
                    bool updated = false;
                    //while (!updated)
                    //{                      
                    //updated = fleeingList.AddOrUpdate(mobileParty.StringId, fleeingParty, (mobileParty.StringId, fleeingParty) => fleeingParty);
                    //}
                    //fleeingList[mobileParty.StringId] = fleeingParty;
                    if (fleeingList.ContainsKey(mobileParty.StringId))
                    {
                        fleeingList[mobileParty.StringId] = fleeingParty;
                    }
                    else
                    {
                        bool added = false;
                        while (!added)
                        {
                            added = fleeingList.TryAdd(mobileParty.StringId, fleeingParty);
                        }
                    }
                    Factory.KaosesFleeingPartiesList = fleeingList;
                }
                else
                {
                    if (fleeingList.ContainsKey(mobileParty.StringId))
                    {
                        KaosesFleeingPartySpeed temp;
                        bool removed = false;
                        while (!removed)
                        {
                            removed = fleeingList.TryRemove(mobileParty.StringId, out temp);
                        }

                    }

                }
            }


        }


        private void DebugMessage(string message)
        {
            if (_debug)
            {
                //IM.MessageDebug(message);
            }
        }
    }
}
