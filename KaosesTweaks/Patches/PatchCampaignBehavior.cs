using HarmonyLib;
using StoryMode.GameComponents.CampaignBehaviors;
using StoryMode.StoryModeObjects;
using StoryMode.StoryModePhases;
using StoryMode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem;
using KaosesTweaks.Objects;

namespace KaosesTweaks.Patches
{
    public class PatchCampaignBehavior
    {
        [HarmonyPatch(typeof(TrainingFieldCampaignBehavior), "OnCharacterCreationIsOver")]
        public class PatchTrainingFieldCampaignBehavior
        {
            // Skip the tutorial.
            private static void Prefix(ref bool ___SkipTutorialMission)
            {
                if (!Factory.Settings.SkipTutorial)
                {
                    return;
                }
                ___SkipTutorialMission = true;
                TutorialPhase.Instance.PlayerTalkedWithBrotherForTheFirstTime();
                StoryModeManager.Current.MainStoryLine.CompleteTutorialPhase(true);
            }
        }

        [HarmonyPatch(typeof(TutorialPhaseCampaignBehavior), "OnStoryModeTutorialEnded")]
        public class CSPatchTutorialPhaseCampaignBehavior
        {
            // Skip the vanilla code that sets the player's items and gold.
            private static bool Prefix()
            {
                if (!Factory.Settings.SkipTutorial)
                {
                    return true;
                }
                DisableHeroAction.Apply(StoryModeHeroes.ElderBrother);
                StoryModeHeroes.ElderBrother.Clan = CampaignData.NeutralFaction;
                return false;
            }
        }

        //static bool Prepare => Factory.Settings is { } settings && settings.SkipTutorial;
    }
}
