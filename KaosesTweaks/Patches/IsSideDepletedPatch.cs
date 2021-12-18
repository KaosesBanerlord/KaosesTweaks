using HarmonyLib;
using KaosesTweaks.Settings;
using SandBox.Source.Missions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using static TaleWorlds.MountAndBlade.Agent;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(HideoutMissionController), "IsSideDepleted")]
    class IsSideDepletedPatch
    {
        public static bool Notified { get; set; } = false;
        public static bool Yelled { get; set; } = false;
        public static bool Dueled { get; set; } = false;

        static void Postfix(HideoutMissionController __instance, ref bool __result, BattleSideEnum side, ref int ____hideoutMissionState, Team ____enemyTeam)
        {
            if (__result && side == BattleSideEnum.Attacker)
            {
                try
                {
                    if (HasTroopsRemaining(__instance, side))
                    {
                        if (PlayerIsDead() && MCMSettings.Instance is { } settings)
                        {
                            if (____hideoutMissionState == 5 || ____hideoutMissionState == 6)
                            {
                                if (settings.ContinueHideoutBattleOnPlayerLoseDuel)
                                {
                                    if (!Notified)
                                    {
                                        SetTeamsHostile(__instance, ____enemyTeam);
                                        FreeAgentsToMove(__instance);
                                        TryAlarmAgents(__instance);
                                        MakeAgentsYell(__instance);
                                        TrySetFormationsCharge(__instance, BattleSideEnum.Attacker);
                                        TrySetFormationsCharge(__instance, BattleSideEnum.Defender);
                                        InformationManager.DisplayMessage(new InformationMessage("You have lost the duel! Your men are avenging your defeat!"));
                                        Notified = true;
                                        Dueled = true;
                                    }

                                    if (____hideoutMissionState != 6)
                                        ____hideoutMissionState = 6;

                                    __result = false;
                                }
                            }
                            else
                            {
                                if (settings.ContinueHideoutBattleOnPlayerDeath && !Dueled)
                                {
                                    if (!Notified)
                                    {
                                        TrySetFormationsCharge(__instance, BattleSideEnum.Attacker);
                                        MakeAgentsYell(__instance, BattleSideEnum.Attacker);
                                        InformationManager.DisplayMessage(new InformationMessage("You have fallen in the attack. Your troops are charging to avenge you!"));
                                        Notified = true;
                                    }

                                    if (____hideoutMissionState != 1 && ____hideoutMissionState != 6)
                                        ____hideoutMissionState = 1;

                                    __result = false;
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message + "\n" + exception.StackTrace + "\n" + exception.InnerException);
                }
            }
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && (settings.ContinueHideoutBattleOnPlayerDeath || settings.ContinueHideoutBattleOnPlayerLoseDuel);


        private static bool HasTroopsRemaining(HideoutMissionController controller, BattleSideEnum side)
        {
            IList missionSides = (IList)typeof(HideoutMissionController).GetField("_missionSides", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(controller);
            object? mSide = missionSides[(int)side];
            int numTroops = (int)typeof(HideoutMissionController).GetNestedType("MissionSide", BindingFlags.NonPublic)
                .GetProperty("NumberOfActiveTroops", BindingFlags.Public | BindingFlags.Instance).GetValue(mSide);
            return numTroops > 0;
        }

        private static bool PlayerIsDead()
        {
            return Main == null || !Main.IsActive();
        }

        private static void TrySetFormationsCharge(HideoutMissionController controller, BattleSideEnum side)
        {
            List<Team> teams = (from t in controller.Mission.Teams
                                where t.Side == side
                                select t).ToList();
            if (teams != null && teams.Count > 0)
            {
                foreach (Team? team in teams)
                {
                    foreach (Formation? formation in team.Formations)
                    {

                        if (formation.GetReadonlyMovementOrderReference().OrderType != OrderType.Charge)
                            formation.SetMovementOrder(MovementOrder.MovementOrderCharge);
                        /*
                        if (formation.MovementOrder.OrderType != OrderType.Charge)
                            formation.MovementOrder = MovementOrder.MovementOrderCharge;*/
                    }
                }
            }
        }

        private static void TryAlarmAgents(HideoutMissionController controller)
        {
            foreach (Agent? agent in controller.Mission.Agents)
            {

                if (agent.IsAIControlled && agent.CurrentWatchState != WatchState.Alarmed)
                {
                    agent.SetWatchState(WatchState.Alarmed);
                }
            }
        }

        private static void MakeAgentsYell(HideoutMissionController controller, BattleSideEnum side)
        {
            foreach (Agent? agent in controller.Mission.Agents)
            {
                if (agent.IsActive() && agent.Team.Side == side)
                    agent.SetWantsToYell();
            }
        }

        private static void MakeAgentsYell(HideoutMissionController controller)
        {
            MakeAgentsYell(controller, BattleSideEnum.Attacker);
            MakeAgentsYell(controller, BattleSideEnum.Defender);
        }

        private static void SetTeamsHostile(HideoutMissionController controller, Team enemyTeam)
        {
            Team passivePlayerTeam = controller.Mission.Teams.Where((x) => x.Side == BattleSideEnum.None && x.Banner == controller.Mission.PlayerTeam.Banner).FirstOrDefault();
            Team passiveEnemyTeam = controller.Mission.Teams.Where((x) => x.Side == BattleSideEnum.None && x.Banner == enemyTeam.Banner).FirstOrDefault();

            if (passivePlayerTeam != null)
            {
                List<Agent> list = new List<Agent>(passivePlayerTeam.ActiveAgents);
                foreach (Agent? agent in list)
                {
                    agent.SetTeam(controller.Mission.Teams.Attacker, true);
                }
            }
            if (passiveEnemyTeam != null)
            {
                List<Agent> list = new List<Agent>(passiveEnemyTeam.ActiveAgents);
                foreach (Agent? agent in list)
                {
                    agent.SetTeam(controller.Mission.Teams.Defender, true);
                }
            }
            controller.Mission.Teams.Attacker.SetIsEnemyOf(controller.Mission.Teams.Defender, true);
        }

        private static void FreeAgentsToMove(HideoutMissionController controller)
        {
            foreach (Agent? agent in controller.Mission.Agents)
            {
                if (agent.IsActive())
                    agent.DisableScriptedMovement();
            }
        }
    }

    [HarmonyPatch(typeof(HideoutMissionController), "InitializeMission")]
    public class InitializeMissionPatch
    {
        static void Postfix()
        {
            IsSideDepletedPatch.Notified = false;
            IsSideDepletedPatch.Yelled = false;
            IsSideDepletedPatch.Dueled = false;
        }

        static bool Prepare() => MCMSettings.Instance is { } settings && (settings.ContinueHideoutBattleOnPlayerDeath || settings.ContinueHideoutBattleOnPlayerLoseDuel);
    }
}
