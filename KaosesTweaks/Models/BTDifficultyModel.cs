using KaosesTweaks.Settings;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;


namespace KaosesTweaks.Models
{
    class BTDifficultyModel : DefaultDifficultyModel
    {
        public override float GetDamageToFriendsMultiplier()
        {
            return MCMSettings.Instance is { } settings && settings.DamageToFriendsTweakEnabled ? settings.DamageToFriendsMultiplier : base.GetDamageToFriendsMultiplier();
        }

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
    }
}
