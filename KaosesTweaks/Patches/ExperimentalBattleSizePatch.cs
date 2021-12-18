using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using SandBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace KaosesTweaks.Patches
{

    [HarmonyPatch(typeof(BannerlordConfig), "GetRealBattleSize")]
    public class BattleSizePatchEx_GetRealBattleSize
    {
        static void Postfix(ref int __result)
        {
            if (MCMSettings.Instance is { } settings)
            {
                if (BattleSizePatchEx_PartyGroupTroopSupplier.mountfootratio * settings.BattleSizeEx * (1f + (Statics._settings.ReinforcementQuota * Statics._settings.SlotsForReinforcements * 0.01f)) > 2048)
                {
                    __result = (int)(2048 / BattleSizePatchEx_PartyGroupTroopSupplier.mountfootratio / (1f + (Statics._settings.ReinforcementQuota * Statics._settings.SlotsForReinforcements * 0.01f)));
                    IM.ColorRedMessage("Battlesize was adjusted to prevent crashing and ensure reinforcments.");
                    if (Statics._settings.BattleSizeDebug)
                    {
                        int SlotsForMounts = (int)(2048 - 2048 / BattleSizePatchEx_PartyGroupTroopSupplier.mountfootratio);
                        int SlotsForReinforcements = (int)(2048 - SlotsForMounts - (2048 - SlotsForMounts) / (1f + (Statics._settings.ReinforcementQuota * Statics._settings.SlotsForReinforcements * 0.01f)));
                        IM.ColorRedMessage("MountedRatio: " + BattleSizePatchEx_PartyGroupTroopSupplier.mountfootratio + " | Reserved: " + (1f + (Statics._settings.ReinforcementQuota * Statics._settings.SlotsForReinforcements * 0.01f)));
                        IM.ColorRedMessage("2048 - Slots mounts (" + SlotsForMounts + ") - Slots reinforcements(" + SlotsForReinforcements + ")");
                    }
                }
                else __result = settings.BattleSizeEx;
                IM.ColorRedMessage("Battlesize was set to " + __result + ".");
            }
        }
        static bool Prepare() => MCMSettings.Instance is { } settings && settings.BattleSizeTweakExEnabled;
    }


    [HarmonyPatch(typeof(MissionAgentSpawnLogic), "get_MaxNumberOfTroopsForMission")]
    internal class BattleSizePatchEx_get_MaxNumberOfTroopsForMission
    {
        private static void Postfix(ref int __result)
        {
            if (MCMSettings.Instance is { } settings)
            {
                if (BattleSizePatchEx_PartyGroupTroopSupplier.mountfootratio * settings.BattleSizeEx * (1f + (Statics._settings.ReinforcementQuota * Statics._settings.SlotsForReinforcements * 0.01f)) > 2048)
                {
                    __result = (int)(2048 / BattleSizePatchEx_PartyGroupTroopSupplier.mountfootratio / (1f + (Statics._settings.ReinforcementQuota * Statics._settings.SlotsForReinforcements * 0.01f)));
                }
                else __result = settings.BattleSizeEx;
            }
        }
        static bool Prepare() => MCMSettings.Instance is { } settings && settings.BattleSizeTweakExEnabled;
    }


    [HarmonyPatch(typeof(PartyGroupTroopSupplier), MethodType.Constructor, new Type[] { typeof(MapEvent), typeof(BattleSideEnum), typeof(FlattenedTroopRoster) })]
    public class BattleSizePatchEx_PartyGroupTroopSupplier
    {
        static void Postfix(MapEvent mapEvent, BattleSideEnum side, FlattenedTroopRoster priorTroops)
        {
            if (MCMSettings.Instance is { } settings)
            {
                BattleSizePatchEx_BattleSizeSpawnTick.AgentTrackerTroop.Clear();
                BattleSizePatchEx_BattleSizeSpawnTick.AgentTrackerMount.Clear();
                BattleSizePatchEx_BattleSizeSpawnTick.NumAttackers = 0;
                BattleSizePatchEx_BattleSizeSpawnTick.NumDefenders = 0;

                if (troops == 0 && mounts == 0)
                {
                    foreach (MapEventParty party in MapEvent.PlayerMapEvent.AttackerSide.Parties)
                    {
                        foreach (CharacterObject troop in party.Troops.Troops)
                        {
                            if (troop.IsMounted)
                            {
                                mounts += 1f;
                                troops += 1f;
                            }
                            else troops += 1f;
                        }
                    }
                    foreach (MapEventParty party in MapEvent.PlayerMapEvent.DefenderSide.Parties)
                    {
                        foreach (CharacterObject troop in party.Troops.Troops)
                        {
                            if (troop.IsMounted)
                            {
                                mounts += 1f;
                                troops += 1f;
                            }
                            else troops += 1f;
                        }
                    }
                }
                mountfootratio = 1f + mounts / troops * Statics._settings.BattleSizeExSafePuffer;
                if (MapEvent.PlayerMapEvent.EventType == MapEvent.BattleTypes.Siege)
                {
                    mountfootratio = 1f;
                    isSiege = true;
                }
            }
        }
        static bool Prepare() => MCMSettings.Instance is { } settings && settings.BattleSizeTweakExEnabled;
        public static float mountfootratio = 0f;
        public static float troops = 0f;
        public static float mounts = 0f;
        public static bool isSiege = false;
    }


    [HarmonyPatch(typeof(MissionAgentSpawnLogic), "BattleSizeSpawnTick")]
    internal class BattleSizePatchEx_BattleSizeSpawnTick
    {
        static bool Prepare() => MCMSettings.Instance is { } settings && settings.BattleSizeTweakExEnabled;
        public static int runs = 0;
        public static int NumberOfTroopsCanBeSpawned = 0;
        public static int NumAttackers = 0;
        public static int NumDefenders = 0;
        public static int removed = 0;
        public static List<Agent> AgentTrackerTroop = new();
        public static List<Agent> AgentTrackerMount = new();

        public static float GetMountRatio()
        {
            return BattleSizePatchEx_PartyGroupTroopSupplier.mountfootratio;
        }

        private static bool Prefix(MissionAgentSpawnLogic __instance)
        {
            if (NumberOfTroopsCanBeSpawned > __instance.NumberOfTroopsCanBeSpawned)
            {
                int NumAttackersNew = __instance.NumberOfActiveAttackerTroops;
                int NumDefendersNew = __instance.NumberOfActiveDefenderTroops;
                if (NumAttackers != 0 && NumDefenders != 0 && (NumAttackers < NumAttackersNew || NumDefenders < NumDefendersNew))
                {
                    IM.ColorRedMessage("Attackers got " + (NumAttackersNew - NumAttackers) + " reinforcements!");
                    IM.ColorRedMessage("Defenders got " + (NumDefendersNew - NumDefenders) + " reinforcements!");
                    IM.ColorRedMessage("Slots were: " + NumberOfTroopsCanBeSpawned + ".");
                }
            }
            NumAttackers = __instance.NumberOfActiveAttackerTroops;
            NumDefenders = __instance.NumberOfActiveDefenderTroops;
            runs += 1;
            int mountAgents = 0;
            int mountNoRider = 0;
            NumberOfTroopsCanBeSpawned = __instance.NumberOfTroopsCanBeSpawned;

            foreach (Agent agent in __instance.Mission.AllAgents)
            {
                if (agent.IsMount)
                {
                    if (!AgentTrackerMount.Contains(agent)) AgentTrackerMount.Add(agent);
                    mountAgents += 1;
                    if (agent.RiderAgent == null || agent.RiderAgent.State != AgentState.Active)
                    {
                        mountNoRider += 1;
                        if (mountNoRider > Statics._settings.RetreatHorses) agent.Retreat(__instance.Mission.GetClosestFleePositionForAgent(agent)); //Mounts retreat more, freeing up agents
                    }
                }
                else if (agent.IsHuman)
                {
                    if (!AgentTrackerTroop.Contains(agent)) AgentTrackerTroop.Add(agent);
                }
            }
            BattleSizePatchEx_PartyGroupTroopSupplier.mountfootratio = (BattleSizePatchEx_PartyGroupTroopSupplier.troops == AgentTrackerTroop.Count) ? 1f : 1f + (BattleSizePatchEx_PartyGroupTroopSupplier.mounts - AgentTrackerMount.Count) / (BattleSizePatchEx_PartyGroupTroopSupplier.troops - AgentTrackerTroop.Count) * Statics._settings.BattleSizeExSafePuffer;
            if (BattleSizePatchEx_PartyGroupTroopSupplier.isSiege == true) BattleSizePatchEx_PartyGroupTroopSupplier.mountfootratio = 1f;


            if (runs > 200)
            {
                if (Statics._settings.BattleSizeDebug)
                {
                    IM.ColorGreenMessage("---------REPORT START------------");
                    IM.ColorGreenMessage("Mounts: " + mountAgents + " | Troops: " + __instance.NumberOfActiveTroops + " | Agents: " + __instance.Mission.AllAgents.Count);
                    IM.ColorGreenMessage("To be spawned: " + __instance.NumberOfRemainingTroops + " | Slots available: " + __instance.NumberOfTroopsCanBeSpawned);
                    IM.ColorGreenMessage("Reinforcements mounted agent ratio: " + Math.Round(BattleSizePatchEx_PartyGroupTroopSupplier.mountfootratio, 2));
                }
                runs = 0;
                return true;
            }
            return true;
        }


        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {

            List<CodeInstruction> list = new List<CodeInstruction>(instructions);

            if (list.Count == 250)
            {
                list.Insert(72, new CodeInstruction(OpCodes.Conv_R4, null)); //  (MissionAgentSpawnLogic.MaxNumberOfAgentsForMission - base.Mission.AllAgents.Count) --> float
                list.Insert(74, new CodeInstruction(OpCodes.Conv_R4, null)); // >= numberOfTroopsCanBeSpawned --> float
                list[75] = new CodeInstruction(OpCodes.Call, SymbolExtensions.GetMethodInfo(() => GetMountRatio())); // >= numberOfTroopsCanBeSpawned * mountfootratio
                list[83].operand = Statics._settings.ReinforcementQuota * 0.01f; //>= (float)this._battleSize * _ReinforcementQuota * 0.01f_ --> Percentage of Battlesize each reinforcement
                list.RemoveRange(85, 6); // remove  _ || num4 >= 0.5f || num5 >= 0.5f_
            }

            return list.AsEnumerable<CodeInstruction>();
        }

        /* 
		int numberOfTroopsCanBeSpawned = this.NumberOfTroopsCanBeSpawned;
		if (this.NumberOfRemainingTroops > 0 && numberOfTroopsCanBeSpawned > 0)
		{
		AllSpawned					int num = this.DefenderActivePhase.TotalSpawnNumber + this.AttackerActivePhase.TotalSpawnNumber;
		DefenderCanSpawnAmount		int num2 = MBMath.Round((float)numberOfTroopsCanBeSpawned * (float)this.DefenderActivePhase.TotalSpawnNumber / (float)num);
		AttackerCanSpawnAmount		int num3 = numberOfTroopsCanBeSpawned - num2;
		Def % lost					float num4 = (float)(this.DefenderActivePhase.InitialSpawnedNumber - this._missionSides[0].NumberOfActiveTroops) / (float)this.DefenderActivePhase.InitialSpawnedNumber;
		Att % lost					float num5 = (float)(this.AttackerActivePhase.InitialSpawnedNumber - this._missionSides[1].NumberOfActiveTroops) / (float)this.AttackerActivePhase.InitialSpawnedNumber;
									if (MissionAgentSpawnLogic.MaxNumberOfAgentsForMission - base.Mission.AllAgents.Count >= numberOfTroopsCanBeSpawned * 2 && ((float)numberOfTroopsCanBeSpawned >= (float)this._battleSize * 0.1f || num4 >= 0.5f || num5 >= 0.5f))
									{
		Def % lost x 2					float num6 = num4 / 0.5f;
		Att % lost x 2					float num7 = num5 / 0.5f;
		Who is doing better				float num8 = MBMath.ClampFloat(num6 - num7, -1f, 1f);
		Attacker is doing better		if (num8 > 0f)
										{
											int num9 = MBMath.Round((float)num3 * num8);
											num3 -= num9;
											num2 += num9;
										}
		Defender is doing better		else if (num8 < 0f) 
										{
											num8 = MBMath.Absf(num8);
											int num10 = MBMath.Round((float)num2 * num8);
											num2 -= num10;
											num3 += num10;
										}
										int num11 = Math.Max(num2 - this.DefenderActivePhase.RemainingSpawnNumber, 0);
										int num12 = Math.Max(num3 - this.AttackerActivePhase.RemainingSpawnNumber, 0);
										if (num11 > 0 && num12 > 0)
										{
											num2 = this.DefenderActivePhase.RemainingSpawnNumber;
											num3 = this.AttackerActivePhase.RemainingSpawnNumber;
										}
										else if (num11 > 0)
										{
											num2 = this.DefenderActivePhase.RemainingSpawnNumber;
											num3 = Math.Min(num3 + num11, this.AttackerActivePhase.RemainingSpawnNumber);
										}
										else if (num12 > 0)
										{
											num3 = this.AttackerActivePhase.RemainingSpawnNumber;
											num2 = Math.Min(num2 + num12, this.DefenderActivePhase.RemainingSpawnNumber);
										}
										if (this._missionSides[0].TroopSpawningActive && num2 > 0)
										{
											this.DefenderActivePhase.RemainingSpawnNumber -= this._missionSides[0].SpawnTroops(num2, true, true);
										}
										if (this._missionSides[1].TroopSpawningActive && num3 > 0)
										{
											this.AttackerActivePhase.RemainingSpawnNumber -= this._missionSides[1].SpawnTroops(num3, true, true);
										}
									}
								}*/
    }
}
