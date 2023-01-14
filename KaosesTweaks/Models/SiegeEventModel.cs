using KaosesTweaks.Objects;
using KaosesTweaks.Settings;
using System.Runtime;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;

namespace KaosesTweaks.Models
{

    class SiegeEventModel : DefaultSiegeEventModel
    {
        public override float GetConstructionProgressPerHour(SiegeEngineType type, SiegeEvent siegeEvent, ISiegeEventSide side)
        {
            if (Factory.Settings is { } settings)
                return base.GetConstructionProgressPerHour(type, siegeEvent, side) * settings.SiegeConstructionProgressPerDayMultiplier;
            else
                return base.GetConstructionProgressPerHour(type, siegeEvent, side);
        }

        public override int GetColleteralDamageCasualties(SiegeEngineType siegeEngineType, MobileParty party)
        {
            if (Factory.Settings is { } settings)
                return base.GetColleteralDamageCasualties(siegeEngineType, party) + settings.SiegeCollateralDamageCasualties;
            else
                return base.GetColleteralDamageCasualties(siegeEngineType, party);
        }

        public override int GetSiegeEngineDestructionCasualties(SiegeEvent siegeEvent, BattleSideEnum side, SiegeEngineType destroyedSiegeEngine)
        {
            if (Factory.Settings is { } settings)
                return base.GetSiegeEngineDestructionCasualties(siegeEvent, side, destroyedSiegeEngine) + settings.SiegeDestructionCasualties;
            else
                return base.GetSiegeEngineDestructionCasualties(siegeEvent, side, destroyedSiegeEngine);
        }
    }
}
