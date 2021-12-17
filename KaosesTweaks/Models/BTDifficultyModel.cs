using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;


namespace KaosesTweaks.Models
{
    class BTDifficultyModel : DefaultDifficultyModel
    {
        /*
                public override float GetDamageToFriendsMultiplier()
                {
                    return MCMSettings.Instance is { } settings && settings.DamageToFriendsTweakEnabled ? settings.DamageToFriendsMultiplier : base.GetDamageToFriendsMultiplier();
                }
        */

        public override float GetDamageToPlayerMultiplier()
        {
            return MCMSettings.Instance is { } settings && settings.DamageToPlayerTweakEnabled ? settings.DamageToPlayerMultiplier : base.GetDamageToPlayerMultiplier();
        }

        public override float GetPlayerTroopsReceivedDamageMultiplier()
        {
            return MCMSettings.Instance is { } settings && settings.DamageToTroopsTweakEnabled ? settings.DamageToTroopsMultiplier : base.GetPlayerTroopsReceivedDamageMultiplier();
        }

        public override float GetCombatAIDifficultyMultiplier()
        {
            return MCMSettings.Instance is { } settings && settings.CombatAIDifficultyTweakEnabled ? settings.CombatAIDifficultyMultiplier : base.GetCombatAIDifficultyMultiplier();
        }

        public override float GetPlayerMapMovementSpeedBonusMultiplier()
        {
            return MCMSettings.Instance is { } settings && settings.PlayerMapMovementSpeedBonusTweakEnabled ? settings.PlayerMapMovementSpeedBonusMultiplier : base.GetPlayerMapMovementSpeedBonusMultiplier();
        }

        public override float GetPersuasionBonusChance()
        {
            return MCMSettings.Instance is { } settings && settings.PlayerPersuasionBonusChanceTweakEnabled ? settings.PlayerPersuasionBonusChanceMultiplier : base.GetPersuasionBonusChance();
        }

        public override float GetClanMemberDeathChanceMultiplier()
        {
            return MCMSettings.Instance is { } settings && settings.ClanMemberDeathChanceReductionTweakEnabled ? settings.ClanMemberDeathChanceReductionMultiplier : base.GetClanMemberDeathChanceMultiplier();
        }



        // Token: 0x06002C31 RID: 11313 RVA: 0x000AC2F0 File Offset: 0x000AA4F0
        /*
                public override int GetPlayerRecruitSlotBonus()
                {
                    switch (CampaignOptions.RecruitmentDifficulty)
                    {
                        case CampaignOptions.Difficulty.VeryEasy:
                            return 2;
                        case CampaignOptions.Difficulty.Easy:
                            return 1;
                        case CampaignOptions.Difficulty.Realistic:
                            return 0;
                        default:
                            return 0;
                    }
                }*/





    }
}
