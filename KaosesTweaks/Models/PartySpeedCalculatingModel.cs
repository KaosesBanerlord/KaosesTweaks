using Helpers;
using KaosesTweaks;
using KaosesTweaks.Objects;
using KaosesTweaks.Objects.PartySpeeds;
using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;


namespace KaosesTweaks.Models
{

    class PartySpeedCalculatingModel : DefaultPartySpeedCalculatingModel
    {
        private static readonly TextObject _textCargo = new TextObject("{=fSGY71wd}Cargo within capacity");
        private static readonly TextObject _textOverburdened = new TextObject("{=xgO3cCgR}Overburdened");
        private static readonly TextObject _textOverPartySize = new TextObject("{=bO5gL3FI}Men within party size");
        private static readonly TextObject _textOverPrisonerSize = new TextObject("{=Ix8YjLPD}Men within prisoner size");
        private static readonly TextObject _textCavalry = new TextObject("{=YVGtcLHF}Cavalry");
        private static readonly TextObject _textKhuzaitCavalryBonus = new TextObject("{=yi07dBks}Khuzait Cavalry Bonus");
        private static readonly TextObject _textMountedFootmen = new TextObject("{=5bSWSaPl}Footmen on horses");
        private static readonly TextObject _textWounded = new TextObject("{=aLsVKIRy}Wounded Members");
        private static readonly TextObject _textPrisoners = new TextObject("{=N6QTvjMf}Prisoners");
        private static readonly TextObject _textHerd = new TextObject("{=NhAMSaWU}Herd");
        private static readonly TextObject _textHighMorale = new TextObject("{=aDQcIGfH}High Morale");
        private static readonly TextObject _textLowMorale = new TextObject("{=ydspCDIy}Low Morale");
        private static readonly TextObject _textCaravan = new TextObject("{=vvabqi2w}Caravan");
        private static readonly TextObject _textDisorganized = new TextObject("{=JuwBb2Yg}Disorganized");
        private static readonly TextObject _movingInForest = new TextObject("{=rTFaZCdY}Forest");
        private static readonly TextObject _fordEffect = new TextObject("{=NT5fwUuJ}Fording");
        private static readonly TextObject _night = new TextObject("{=fAxjyMt5}Night");
        private static readonly TextObject _snow = new TextObject("{=vLjgcdgB}Snow");
        private static readonly TextObject _desert = new TextObject("{=ecUwABe2}Desert");
        private static readonly TextObject _sturgiaSnowBonus = new TextObject("{=0VfEGekD}Sturgia Snow Bonus");
        private static readonly TextObject _culture = GameTexts.FindText("str_culture");



        private const float MovingAtForestEffect = -0.3f;
        private const float MovingAtWaterEffect = -0.3f;
        private const float MovingAtNightEffect = -0.25f;
        private const float MovingOnSnowEffect = -0.1f;
        private const float MovingInDesertEffect = -0.1f;
        private const float CavalryEffect = 0.4f;
        private const float MountedFootMenEffect = 0.2f;
        private const float HerdEffect = -0.4f;
        private const float WoundedEffect = -0.05f;
        private const float CargoEffect = -0.02f;
        private const float OverburdenedEffect = -0.4f;
        private const float HighMoraleThresold = 70f;
        private const float LowMoraleThresold = 30f;
        private const float HighMoraleEffect = 0.05f;
        private const float LowMoraleEffect = -0.1f;
        private const float DisorganizedEffect = -0.4f;

        public override float BaseSpeed => 5f;

        public override float MinimumSpeed => Factory.Settings.KaosesmininumSpeedAmount;

        public override ExplainedNumber CalculateBaseSpeed(
          MobileParty mobileParty,
          bool includeDescriptions = false,
          int additionalTroopOnFootCount = 0,
          int additionalTroopOnHorseCount = 0)
        {
            PartyBase party = mobileParty.Party;
            int numberOfAvailableMounts = 0;
            float totalWeightCarried = 0.0f;
            int herdSize1 = 0;
            int num1 = mobileParty.MemberRoster.TotalManCount + additionalTroopOnFootCount + additionalTroopOnHorseCount;
            PartySpeedCalculatingModel.AddCargoStats(mobileParty, ref numberOfAvailableMounts, ref totalWeightCarried, ref herdSize1);
            float totalWeight = mobileParty.ItemRoster.TotalWeight;
            int resultNumber = (int)Campaign.Current.Models.InventoryCapacityModel.CalculateInventoryCapacity(mobileParty, additionalManOnFoot: additionalTroopOnFootCount, additionalSpareMounts: additionalTroopOnHorseCount).ResultNumber;
            int totalCavalryCount1 = party.NumberOfMenWithHorse + additionalTroopOnHorseCount;
            int num2 = party.NumberOfMenWithoutHorse + additionalTroopOnFootCount;
            int totalWounded = party.MemberRoster.TotalWounded;
            int totalManCount = party.PrisonRoster.TotalManCount;
            float morale = mobileParty.Morale;
            if (mobileParty.AttachedParties.Count != 0)
            {
                foreach (MobileParty attachedParty in mobileParty.AttachedParties)
                {
                    PartySpeedCalculatingModel.AddCargoStats(attachedParty, ref numberOfAvailableMounts, ref totalWeightCarried, ref herdSize1);
                    num1 += attachedParty.MemberRoster.TotalManCount;
                    totalWeight += attachedParty.ItemRoster.TotalWeight;
                    resultNumber += attachedParty.InventoryCapacity;
                    totalCavalryCount1 += attachedParty.Party.NumberOfMenWithHorse;
                    num2 += attachedParty.Party.NumberOfMenWithoutHorse;
                    totalWounded += attachedParty.MemberRoster.TotalWounded;
                    totalManCount += attachedParty.PrisonRoster.TotalManCount;
                }
            }
            ExplainedNumber result = new ExplainedNumber(this.CalculateBaseSpeedForParty(num1), includeDescriptions);
            this.GetCavalryRatioModifier(mobileParty, num1, totalCavalryCount1, ref result);
            this.GetFootmenPerkBonus(mobileParty, num1, num2, ref result);
            int totalCavalryCount2 = MathF.Min(num2, numberOfAvailableMounts);
            float footmenRatioModifier = this.GetMountedFootmenRatioModifier(num1, totalCavalryCount2);
            result.AddFactor(footmenRatioModifier, PartySpeedCalculatingModel._textMountedFootmen);
            if ((double)footmenRatioModifier > 0.0 && mobileParty.LeaderHero != null && mobileParty.LeaderHero.GetPerkValue(DefaultPerks.Riding.NomadicTraditions))
                result.AddFactor((float)((double)footmenRatioModifier * (double)DefaultPerks.Riding.NomadicTraditions.PrimaryBonus * 0.00999999977648258), DefaultPerks.Riding.NomadicTraditions.Name);
            float weightCarried = MathF.Min(totalWeight, (float)resultNumber);
            if ((double)weightCarried > 0.0)
            {
                float cargoEffect = this.GetCargoEffect(weightCarried, resultNumber);
                result.AddFactor(cargoEffect, PartySpeedCalculatingModel._textCargo);
            }
            if ((double)totalWeightCarried > (double)resultNumber)
            {
                float overBurdenedEffect = this.GetOverBurdenedEffect(totalWeightCarried - (float)resultNumber, resultNumber);
                result.AddFactor(overBurdenedEffect, PartySpeedCalculatingModel._textOverburdened);
                if (mobileParty.HasPerk(DefaultPerks.Athletics.Energetic))
                    result.AddFactor((float)((double)overBurdenedEffect * (double)DefaultPerks.Athletics.Energetic.PrimaryBonus * 0.00999999977648258), DefaultPerks.Athletics.Energetic.Name);
                if (mobileParty.HasPerk(DefaultPerks.Scouting.Unburdened))
                    result.AddFactor((float)((double)overBurdenedEffect * (double)DefaultPerks.Scouting.Unburdened.PrimaryBonus * 0.00999999977648258), DefaultPerks.Scouting.Unburdened.Name);
            }
            if (mobileParty.HasPerk(DefaultPerks.Riding.SweepingWind, true))
                result.AddFactor(DefaultPerks.Riding.SweepingWind.SecondaryBonus * 0.01f, DefaultPerks.Riding.SweepingWind.Name);
            if (mobileParty.Party.NumberOfAllMembers > mobileParty.Party.PartySizeLimit)
            {
                float overPartySizeEffect = this.GetOverPartySizeEffect(mobileParty);
                result.AddFactor(overPartySizeEffect, PartySpeedCalculatingModel._textOverPartySize);
            }
            int herdSize2 = herdSize1 + MathF.Max(0, numberOfAvailableMounts - totalCavalryCount2);
            if (!mobileParty.IsVillager)
            {
                float herdingModifier = this.GetHerdingModifier(num1, herdSize2);
                result.AddFactor(herdingModifier, PartySpeedCalculatingModel._textHerd);
                if (mobileParty.HasPerk(DefaultPerks.Riding.Shepherd))
                    result.AddFactor((float)(-(double)herdingModifier * (double)DefaultPerks.Riding.Shepherd.PrimaryBonus * 0.00999999977648258), DefaultPerks.Riding.Shepherd.Name);
            }
            float woundedModifier = this.GetWoundedModifier(num1, totalWounded, mobileParty);
            result.AddFactor(woundedModifier, PartySpeedCalculatingModel._textWounded);
            if (!mobileParty.IsCaravan)
            {
                if (mobileParty.Party.NumberOfPrisoners > mobileParty.Party.PrisonerSizeLimit)
                {
                    float prisonerSizeEffect = this.GetOverPrisonerSizeEffect(mobileParty);
                    result.AddFactor(prisonerSizeEffect, PartySpeedCalculatingModel._textOverPrisonerSize);
                }
                float modifierPrisoner = PartySpeedCalculatingModel.GetSizeModifierPrisoner(num1, totalManCount);
                result.AddFactor((float)(1.0 / (double)modifierPrisoner - 1.0), PartySpeedCalculatingModel._textPrisoners);
            }
            if ((double)morale > 70.0)
                result.AddFactor((float)(0.0500000007450581 * (((double)morale - 70.0) / 30.0)), PartySpeedCalculatingModel._textHighMorale);
            if ((double)morale < 30.0)
                result.AddFactor((float)(-0.100000001490116 * (1.0 - (double)mobileParty.Morale / 30.0)), PartySpeedCalculatingModel._textLowMorale);
            if (mobileParty == MobileParty.MainParty)
            {
                float speedBonusMultiplier = Campaign.Current.Models.DifficultyModel.GetPlayerMapMovementSpeedBonusMultiplier();
                if ((double)speedBonusMultiplier > 0.0)
                    result.AddFactor(speedBonusMultiplier, GameTexts.FindText("str_game_difficulty"));
            }
            if (mobileParty.IsCaravan)
                result.AddFactor(0.1f, PartySpeedCalculatingModel._textCaravan);
            if (mobileParty.IsDisorganized)
                result.AddFactor(-0.4f, PartySpeedCalculatingModel._textDisorganized);
            result.LimitMin(this.MinimumSpeed);
            return result;
        }

        private static void AddCargoStats(
          MobileParty mobileParty,
          ref int numberOfAvailableMounts,
          ref float totalWeightCarried,
          ref int herdSize)
        {
            ItemRoster itemRoster = mobileParty.ItemRoster;
            int numberOfPackAnimals = itemRoster.NumberOfPackAnimals;
            int livestockAnimals = itemRoster.NumberOfLivestockAnimals;
            herdSize += numberOfPackAnimals + livestockAnimals;
            numberOfAvailableMounts += itemRoster.NumberOfMounts;
            totalWeightCarried += itemRoster.TotalWeight;
        }

        private float CalculateBaseSpeedForParty(int menCount) => this.BaseSpeed * MathF.Pow((float)(200.0 / (200.0 + (double)menCount)), 0.4f);

        public override ExplainedNumber CalculateFinalSpeed(
          MobileParty mobileParty,
          ExplainedNumber finalSpeed)
        {
            if (mobileParty.IsCustomParty && !((CustomPartyComponent)mobileParty.PartyComponent).CustomPartyBaseSpeed.ApproximatelyEqualsTo(0.0f))
                finalSpeed = new ExplainedNumber(((CustomPartyComponent)mobileParty.PartyComponent).CustomPartyBaseSpeed);
            TerrainType faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(mobileParty.CurrentNavigationFace);
            Hero effectiveScout = mobileParty.EffectiveScout;
            switch (faceTerrainType)
            {
                case TerrainType.Water:
                case TerrainType.Bridge:
                case TerrainType.River:
                case TerrainType.ShallowRiver:
                    finalSpeed.AddFactor(-0.3f, PartySpeedCalculatingModel._fordEffect);
                    break;
                case TerrainType.Steppe:
                case TerrainType.Plain:
                    if (effectiveScout != null && effectiveScout.GetPerkValue(DefaultPerks.Scouting.Pathfinder))
                    {
                        finalSpeed.AddFactor(DefaultPerks.Scouting.Pathfinder.PrimaryBonus, DefaultPerks.Scouting.Pathfinder.Name);
                        break;
                    }
                    break;
                case TerrainType.Desert:
                case TerrainType.Dune:
                    if (!PartyBaseHelper.HasFeat(mobileParty.Party, DefaultCulturalFeats.AseraiDesertFeat))
                        finalSpeed.AddFactor(-0.1f, PartySpeedCalculatingModel._desert);
                    if (effectiveScout != null && effectiveScout.GetPerkValue(DefaultPerks.Scouting.DesertBorn))
                    {
                        finalSpeed.AddFactor(DefaultPerks.Scouting.DesertBorn.PrimaryBonus, DefaultPerks.Scouting.DesertBorn.Name);
                        break;
                    }
                    break;
                case TerrainType.Forest:
                    float num1 = 0.0f;
                    if (effectiveScout != null && effectiveScout.GetPerkValue(DefaultPerks.Scouting.ForestKin))
                    {
                        for (int index = 0; index < mobileParty.MemberRoster.Count; ++index)
                        {
                            if (mobileParty.MemberRoster.GetCharacterAtIndex(index).IsInfantry)
                                num1 += (float)mobileParty.MemberRoster.GetElementNumber(index);
                        }
                    }
                    float num2 = (double)num1 / (double)mobileParty.MemberRoster.Count > 0.75 ? -0.15f : -0.3f;
                    finalSpeed.AddFactor(num2, PartySpeedCalculatingModel._movingInForest);
                    if (PartyBaseHelper.HasFeat(mobileParty.Party, DefaultCulturalFeats.BattanianForestSpeedFeat))
                    {
                        float num3 = DefaultCulturalFeats.BattanianForestSpeedFeat.EffectBonus * 0.3f;
                        finalSpeed.AddFactor(num3, PartySpeedCalculatingModel._culture);
                        break;
                    }
                    break;
            }
            if (Campaign.Current.Models.MapWeatherModel.GetIsSnowTerrainInPos(mobileParty.Position2D.ToVec3()))
                finalSpeed.AddFactor(-0.1f, PartySpeedCalculatingModel._snow);
            if (Campaign.Current.IsNight)
            {
                finalSpeed.AddFactor(-0.25f, PartySpeedCalculatingModel._night);
                if (effectiveScout != null && effectiveScout.GetPerkValue(DefaultPerks.Scouting.NightRunner))
                    finalSpeed.AddFactor(DefaultPerks.Scouting.NightRunner.PrimaryBonus, DefaultPerks.Scouting.NightRunner.Name);
            }
            else if (effectiveScout != null && effectiveScout.GetPerkValue(DefaultPerks.Scouting.DayTraveler))
                finalSpeed.AddFactor(DefaultPerks.Scouting.DayTraveler.PrimaryBonus, DefaultPerks.Scouting.DayTraveler.Name);
            if (effectiveScout != null)
            {
                PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.Scouting.UncannyInsight, effectiveScout.CharacterObject, DefaultSkills.Scouting, true, ref finalSpeed, 200);
                if (effectiveScout.GetPerkValue(DefaultPerks.Scouting.ForcedMarch) && (double)mobileParty.Morale > 75.0)
                    finalSpeed.AddFactor(DefaultPerks.Scouting.ForcedMarch.PrimaryBonus, DefaultPerks.Scouting.ForcedMarch.Name);
                if (mobileParty.DefaultBehavior == AiBehavior.EngageParty)
                {
                    MobileParty targetParty = mobileParty.TargetParty;
                    if (targetParty != null && targetParty.MapFaction.IsAtWarWith(mobileParty.MapFaction) && effectiveScout.GetPerkValue(DefaultPerks.Scouting.Tracker))
                        finalSpeed.AddFactor(DefaultPerks.Scouting.Tracker.SecondaryBonus, DefaultPerks.Scouting.Tracker.Name);
                }
            }
            if (mobileParty.Army?.LeaderParty != null && mobileParty.Army.LeaderParty != mobileParty && mobileParty.AttachedTo != mobileParty.Army.LeaderParty && mobileParty.Army.LeaderParty.HasPerk(DefaultPerks.Tactics.CallToArms))
                finalSpeed.AddFactor(DefaultPerks.Tactics.CallToArms.PrimaryBonus, DefaultPerks.Tactics.CallToArms.Name);

            //~ Kaoses Party Speeds
            KaosesPartySpeed partySpeed = new KaosesPartySpeed(mobileParty);
            partySpeed.CalculateNewPartySpeed(ref finalSpeed);

            FleeingPartiesManager fleeingParties = new FleeingPartiesManager();
            fleeingParties.CheckPartyForChangingSpeed(mobileParty, ref finalSpeed);
            FleeingPartiesManager fpm = Factory.FleeingPartiesMgr;
            fpm.CheckPartyForChangingSpeed(mobileParty, ref finalSpeed);

            //KaosesPartySpeed.GetDynamicSpeedChange(mobileParty, ref finalSpeed);
            //~ Kaoses Party Speeds

            finalSpeed.LimitMin(this.MinimumSpeed);
            return finalSpeed;
        }

        private float GetCargoEffect(float weightCarried, int partyCapacity) => -0.02f * weightCarried / (float)partyCapacity;

        private float GetOverBurdenedEffect(float totalWeightCarried, int partyCapacity) => -0.4f * (totalWeightCarried / (float)partyCapacity);

        private float GetOverPartySizeEffect(MobileParty mobileParty)
        {
            int partySizeLimit = mobileParty.Party.PartySizeLimit;
            return (float)(1.0 / ((double)mobileParty.Party.NumberOfAllMembers / (double)partySizeLimit) - 1.0);
        }

        private float GetOverPrisonerSizeEffect(MobileParty mobileParty)
        {
            int prisonerSizeLimit = mobileParty.Party.PrisonerSizeLimit;
            return (float)(1.0 / ((double)mobileParty.Party.NumberOfPrisoners / (double)prisonerSizeLimit) - 1.0);
        }

        private float GetHerdingModifier(int totalMenCount, int herdSize)
        {
            herdSize -= totalMenCount;
            if (herdSize <= 0)
                return 0.0f;
            return totalMenCount == 0 ? -0.8f : MathF.Max(-0.8f, (float)(-0.300000011920929 * ((double)herdSize / (double)totalMenCount)));
        }

        private float GetWoundedModifier(int totalMenCount, int numWounded, MobileParty party)
        {
            if (numWounded <= totalMenCount / 4)
                return 0.0f;
            if (totalMenCount == 0)
                return -0.5f;
            ExplainedNumber stat = new ExplainedNumber(MathF.Max(-0.8f, -0.05f * (float)numWounded / (float)totalMenCount));
            PerkHelper.AddPerkBonusForParty(DefaultPerks.Medicine.Sledges, party, true, ref stat);
            return stat.ResultNumber;
        }

        private void GetCavalryRatioModifier(
          MobileParty party,
          int totalMenCount,
          int totalCavalryCount,
          ref ExplainedNumber result)
        {
            if (totalMenCount <= 0 || totalCavalryCount <= 0)
                return;
            float num = 0.4f * (float)totalCavalryCount / (float)totalMenCount;
            result.AddFactor(num, PartySpeedCalculatingModel._textCavalry);
        }

        private float GetMountedFootmenRatioModifier(int totalMenCount, int totalCavalryCount) => totalMenCount == 0 ? 0.0f : 0.2f * (float)totalCavalryCount / (float)totalMenCount;

        private void GetFootmenPerkBonus(
          MobileParty party,
          int totalMenCount,
          int totalFootmenCount,
          ref ExplainedNumber result)
        {
            if (totalMenCount == 0)
                return;
            float f = (float)totalFootmenCount / (float)totalMenCount;
            if (!party.HasPerk(DefaultPerks.Athletics.Strong, true) || f.ApproximatelyEqualsTo(0.0f))
                return;
            result.AddFactor(f * DefaultPerks.Athletics.Strong.SecondaryBonus, DefaultPerks.Athletics.Strong.Name);
        }

        private static float GetSizeModifierWounded(int totalMenCount, int totalWoundedMenCount) => MathF.Pow((float)((10.0 + (double)totalMenCount) / (10.0 + (double)totalMenCount - (double)totalWoundedMenCount)), 0.33f);

        private static float GetSizeModifierPrisoner(int totalMenCount, int totalPrisonerCount) => MathF.Pow((float)((10.0 + (double)totalMenCount + (double)totalPrisonerCount) / (10.0 + (double)totalMenCount)), 0.33f);


    }
}
