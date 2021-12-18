using Helpers;
using KaosesPartySpeeds.Objects;
using KaosesTweaks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace KaosesPartySpeeds.Model
{
    class KaosesPartySpeedCalculatingModel : DefaultPartySpeedCalculatingModel
    {
        // Token: 0x06002E24 RID: 11812 RVA: 0x000C20E4 File Offset: 0x000C02E4
        public override ExplainedNumber CalculatePureSpeed(MobileParty mobileParty, bool includeDescriptions = false, int additionalTroopOnFootCount = 0, int additionalTroopOnHorseCount = 0)
        {
            if (mobileParty.Army != null && mobileParty.Army.LeaderParty.AttachedParties.Contains(mobileParty))
            {
                return CalculatePureSpeed(mobileParty.Army.LeaderParty, includeDescriptions, 0, 0);
            }
            PartyBase party = mobileParty.Party;
            int num = 0;
            float num2 = 0f;
            int num3 = 0;
            int num4 = mobileParty.MemberRoster.TotalManCount + additionalTroopOnFootCount + additionalTroopOnHorseCount;
            AddCargoStats(mobileParty, ref num, ref num2, ref num3);
            float num5 = mobileParty.ItemRoster.TotalWeight;
            new ExplainedNumber(0f, false, null);
            int num6 = (int)Campaign.Current.Models.InventoryCapacityModel.CalculateInventoryCapacity(mobileParty, false, additionalTroopOnFootCount, additionalTroopOnHorseCount, 0, false).ResultNumber;
            int num7 = party.NumberOfMenWithHorse + additionalTroopOnHorseCount;
            int num8 = party.NumberOfMenWithoutHorse + additionalTroopOnFootCount;
            int num9 = party.MemberRoster.TotalWounded;
            int num10 = party.PrisonRoster.TotalManCount;
            float morale = mobileParty.Morale;
            if (mobileParty.AttachedParties.Count != 0)
            {
                foreach (MobileParty mobileParty2 in mobileParty.AttachedParties)
                {
                    AddCargoStats(mobileParty2, ref num, ref num2, ref num3);
                    num4 += mobileParty2.MemberRoster.TotalManCount;
                    num5 += mobileParty2.ItemRoster.TotalWeight;
                    num6 += mobileParty2.InventoryCapacity;
                    num7 += mobileParty2.Party.NumberOfMenWithHorse;
                    num8 += mobileParty2.Party.NumberOfMenWithoutHorse;
                    num9 += mobileParty2.MemberRoster.TotalWounded;
                    num10 += mobileParty2.PrisonRoster.TotalManCount;
                }
            }
            float baseNumber = CalculateBaseSpeedForParty(num4);
            ExplainedNumber result = new ExplainedNumber(baseNumber, includeDescriptions, null);
            GetCavalryRatioModifier(mobileParty, num4, num7, ref result);
            GetFootmenPerkBonus(mobileParty, num4, num8, ref result);
            int num11 = Math.Min(num8, num);
            float mountedFootmenRatioModifier = GetMountedFootmenRatioModifier(num4, num11);
            result.AddFactor(mountedFootmenRatioModifier, _textMountedFootmen);
            if (mountedFootmenRatioModifier > 0f && mobileParty.LeaderHero != null && mobileParty.LeaderHero.GetPerkValue(DefaultPerks.Riding.NomadicTraditions))
            {
                result.AddFactor(mountedFootmenRatioModifier * DefaultPerks.Riding.NomadicTraditions.PrimaryBonus * 0.01f, DefaultPerks.Riding.NomadicTraditions.Name);
            }
            float num12 = Math.Min(num5, num6);
            if (num12 > 0f)
            {
                float cargoEffect = GetCargoEffect(num12, num6);
                result.AddFactor(cargoEffect, _textCargo);
            }
            if (num2 > num6)
            {
                float overBurdenedEffect = GetOverBurdenedEffect(num2 - num6, num6);
                result.AddFactor(overBurdenedEffect, _textOverburdened);
                if (mobileParty.HasPerk(DefaultPerks.Athletics.Energetic, false))
                {
                    result.AddFactor(overBurdenedEffect * DefaultPerks.Athletics.Energetic.PrimaryBonus * 0.01f, DefaultPerks.Athletics.Energetic.Name);
                }
                if (mobileParty.HasPerk(DefaultPerks.Scouting.Unburdened, false))
                {
                    result.AddFactor(overBurdenedEffect * DefaultPerks.Scouting.Unburdened.PrimaryBonus * 0.01f, DefaultPerks.Scouting.Unburdened.Name);
                }
            }
            if (mobileParty.HasPerk(DefaultPerks.Riding.SweepingWind, true))
            {
                result.AddFactor(DefaultPerks.Riding.SweepingWind.SecondaryBonus * 0.01f, DefaultPerks.Riding.SweepingWind.Name);
            }
            if (mobileParty.Party.NumberOfAllMembers > mobileParty.Party.PartySizeLimit)
            {
                float overPartySizeEffect = GetOverPartySizeEffect(mobileParty);
                result.AddFactor(overPartySizeEffect, _textOverPartySize);
            }
            num3 += Math.Max(0, num - num11);
            if (!mobileParty.IsVillager)
            {
                float herdingModifier = GetHerdingModifier(num4, num3);
                result.AddFactor(herdingModifier, _textHerd);
                if (mobileParty.HasPerk(DefaultPerks.Riding.Horde, false))
                {
                    result.AddFactor(-herdingModifier * DefaultPerks.Riding.Horde.PrimaryBonus * 0.01f, DefaultPerks.Riding.Horde.Name);
                }
            }
            float woundedModifier = GetWoundedModifier(num4, num9, mobileParty);
            result.AddFactor(woundedModifier, _textWounded);
            if (!mobileParty.IsCaravan)
            {
                if (mobileParty.Party.NumberOfPrisoners > mobileParty.Party.PrisonerSizeLimit)
                {
                    float overPrisonerSizeEffect = GetOverPrisonerSizeEffect(mobileParty);
                    result.AddFactor(overPrisonerSizeEffect, _textOverPrisonerSize);
                }
                float sizeModifierPrisoner = GetSizeModifierPrisoner(num4, num10);
                result.AddFactor(1f / sizeModifierPrisoner - 1f, _textPrisoners);
            }
            if (morale > 70f)
            {
                result.AddFactor(0.05f * ((morale - 70f) / 30f), _textHighMorale);
            }
            if (morale < 30f)
            {
                result.AddFactor(-0.1f * (1f - mobileParty.Morale / 30f), _textLowMorale);
            }
            if (mobileParty == MobileParty.MainParty)
            {
                float playerMapMovementSpeedBonusMultiplier = Campaign.Current.Models.DifficultyModel.GetPlayerMapMovementSpeedBonusMultiplier();
                if (playerMapMovementSpeedBonusMultiplier > 0f)
                {
                    result.AddFactor(playerMapMovementSpeedBonusMultiplier, GameTexts.FindText("str_game_difficulty", null));
                }
            }
            if (mobileParty.IsCaravan)
            {
                result.AddFactor(0.08f, _textCaravan);
            }
            if (mobileParty.IsDisorganized)
            {
                result.AddFactor(-0.4f, _textDisorganized);
            }
            result.LimitMin(1f);
            return result;
        }

        // Token: 0x06002E25 RID: 11813 RVA: 0x000C25F4 File Offset: 0x000C07F4
        private static void AddCargoStats(MobileParty mobileParty, ref int numberOfAvailableMounts, ref float totalWeightCarried, ref int herdSize)
        {
            ItemRoster itemRoster = mobileParty.ItemRoster;
            int numberOfPackAnimals = itemRoster.NumberOfPackAnimals;
            int numberOfLivestockAnimals = itemRoster.NumberOfLivestockAnimals;
            herdSize += numberOfPackAnimals + numberOfLivestockAnimals;
            numberOfAvailableMounts += itemRoster.NumberOfMounts;
            totalWeightCarried += itemRoster.TotalWeight;
        }

        // Token: 0x06002E26 RID: 11814 RVA: 0x000C2634 File Offset: 0x000C0834
        private float CalculateBaseSpeedForParty(int menCount)
        {
            return (float)(5.0 * Math.Pow(200f / (200f + menCount), 0.4000000059604645));
        }

        // Token: 0x06002E27 RID: 11815 RVA: 0x000C2660 File Offset: 0x000C0860
        public override ExplainedNumber CalculateFinalSpeed(MobileParty mobileParty, ExplainedNumber finalSpeed)
        {
            PartyBase party = mobileParty.Party;
            TerrainType faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(mobileParty.CurrentNavigationFace);
            Hero effectiveScout = mobileParty.EffectiveScout;
            if (faceTerrainType == TerrainType.Forest)
            {
                float num = 0f;
                if (effectiveScout != null && effectiveScout.GetPerkValue(DefaultPerks.Scouting.ForestKin))
                {
                    for (int i = 0; i < mobileParty.MemberRoster.Count; i++)
                    {
                        if (mobileParty.MemberRoster.GetCharacterAtIndex(i).DefaultFormationClass.Equals(FormationClass.Infantry))
                        {
                            num += mobileParty.MemberRoster.GetElementNumber(i);
                        }
                    }
                }
                float num2 = (num / mobileParty.MemberRoster.Count > 0.75f) ? -0.15f : -0.3f;
                finalSpeed.AddFactor(num2, _movingInForest);
                Hero leader = mobileParty.LeaderHero;
                if (leader != null && leader.CharacterObject.Culture.GetCultureCode() == CultureCode.Battania)
                {
                    float value = DefaultCulturalFeats.BattanianForestSpeedFeat.EffectBonus * -num2;
                    finalSpeed.AddFactor(value, _culture);
                }
            }
            else if (faceTerrainType == TerrainType.Water || faceTerrainType == TerrainType.River || faceTerrainType == TerrainType.Bridge || faceTerrainType == TerrainType.ShallowRiver)
            {
                finalSpeed.AddFactor(-0.3f, _fordEffect);
            }
            else if (effectiveScout != null)
            {
                if ((faceTerrainType == TerrainType.Plain || faceTerrainType == TerrainType.Steppe) && effectiveScout.GetPerkValue(DefaultPerks.Scouting.Pathfinder))
                {
                    finalSpeed.AddFactor(DefaultPerks.Scouting.Pathfinder.PrimaryBonus, null);
                }
                else if ((faceTerrainType == TerrainType.Desert || faceTerrainType == TerrainType.Dune) && effectiveScout.GetPerkValue(DefaultPerks.Scouting.DesertBorn))
                {
                    finalSpeed.AddFactor(DefaultPerks.Scouting.DesertBorn.PrimaryBonus, null);
                }
            }
            else if (faceTerrainType == TerrainType.Desert || faceTerrainType == TerrainType.Dune)
            {
                if (mobileParty.HasPerk(DefaultPerks.Scouting.DesertBorn, false))
                {
                    finalSpeed.AddFactor(DefaultPerks.Scouting.DesertBorn.PrimaryBonus, null);
                }
                if (mobileParty.LeaderHero == null || mobileParty.LeaderHero.Culture.GetCultureCode() != CultureCode.Aserai)
                {
                    finalSpeed.AddFactor(-0.1f, _desert);
                }
            }
            if (Campaign.Current.Models.MapWeatherModel.GetIsSnowTerrainInPos(mobileParty.Position2D.ToVec3(0f)))
            {
                finalSpeed.AddFactor(-0.1f, _snow);
            }
            if (Campaign.Current.IsNight)
            {
                finalSpeed.AddFactor(-0.25f, _night);
                if (effectiveScout != null && effectiveScout.GetPerkValue(DefaultPerks.Scouting.NightRunner))
                {
                    finalSpeed.AddFactor(DefaultPerks.Scouting.NightRunner.PrimaryBonus, null);
                }
            }
            else if (effectiveScout != null && effectiveScout.GetPerkValue(DefaultPerks.Scouting.DayTraveler))
            {
                finalSpeed.AddFactor(DefaultPerks.Scouting.DayTraveler.PrimaryBonus, null);
            }
            if (party.LeaderHero != null)
            {
                PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.Scouting.UncannyInsight, party.LeaderHero.CharacterObject, DefaultSkills.Scouting, true, ref finalSpeed, 200);
            }
            if (effectiveScout != null)
            {
                if (effectiveScout.GetPerkValue(DefaultPerks.Scouting.ForcedMarch) && mobileParty.Morale > 75f)
                {
                    finalSpeed.AddFactor(DefaultPerks.Scouting.ForcedMarch.PrimaryBonus, null);
                }
                if (mobileParty.DefaultBehavior == AiBehavior.EngageParty)
                {
                    MobileParty targetParty = mobileParty.TargetParty;
                    if (targetParty != null && targetParty.MapFaction.IsAtWarWith(mobileParty.MapFaction) && effectiveScout.GetPerkValue(DefaultPerks.Scouting.Tracker))
                    {
                        finalSpeed.AddFactor(DefaultPerks.Scouting.Tracker.SecondaryBonus, DefaultPerks.Scouting.Tracker.Name);
                    }
                }
            }
            Army army = mobileParty.Army;
            if (((army != null) ? army.LeaderParty : null) != null && mobileParty.Army.LeaderParty != mobileParty && mobileParty.AttachedTo != mobileParty.Army.LeaderParty && mobileParty.Army.LeaderParty.HasPerk(DefaultPerks.Tactics.CallToArms, false))
            {
                finalSpeed.AddFactor(DefaultPerks.Tactics.CallToArms.PrimaryBonus, DefaultPerks.Tactics.CallToArms.Name);
            }


            KaosesPartySpeed partySpeed = new KaosesPartySpeed(mobileParty);

            if (partySpeed.HasPartyModifiedSpeed())
            {
                finalSpeed.Add(partySpeed.ModifiedPartySpeed(), partySpeed.ExplainationMessage());
            }
            KaosesPartySpeed.GetDynamicSpeedChange(mobileParty, ref finalSpeed);

            finalSpeed.LimitMin(Statics._settings.KaosesmininumSpeedAmount);
            //finalSpeed.LimitMin(1f);
            return finalSpeed;
        }

        // Token: 0x06002E28 RID: 11816 RVA: 0x000C29FC File Offset: 0x000C0BFC
        private float GetCargoEffect(float weightCarried, int partyCapacity)
        {
            return -0.02f * weightCarried / partyCapacity;
        }

        // Token: 0x06002E29 RID: 11817 RVA: 0x000C2A09 File Offset: 0x000C0C09
        private float GetOverBurdenedEffect(float totalWeightCarried, int partyCapacity)
        {
            return -0.4f * (totalWeightCarried / partyCapacity);
        }

        // Token: 0x06002E2A RID: 11818 RVA: 0x000C2A18 File Offset: 0x000C0C18
        private float GetOverPartySizeEffect(MobileParty mobileParty)
        {
            int partySizeLimit = mobileParty.Party.PartySizeLimit;
            int numberOfAllMembers = mobileParty.Party.NumberOfAllMembers;
            return 1f / (numberOfAllMembers / (float)partySizeLimit) - 1f;
        }

        // Token: 0x06002E2B RID: 11819 RVA: 0x000C2A50 File Offset: 0x000C0C50
        private float GetOverPrisonerSizeEffect(MobileParty mobileParty)
        {
            int prisonerSizeLimit = mobileParty.Party.PrisonerSizeLimit;
            int numberOfPrisoners = mobileParty.Party.NumberOfPrisoners;
            return 1f / (numberOfPrisoners / (float)prisonerSizeLimit) - 1f;
        }

        // Token: 0x06002E2C RID: 11820 RVA: 0x000C2A86 File Offset: 0x000C0C86
        private float GetHerdingModifier(int totalMenCount, int herdSize)
        {
            herdSize -= totalMenCount;
            if (herdSize <= 0)
            {
                return 0f;
            }
            if (totalMenCount == 0)
            {
                return -0.8f;
            }
            return Math.Max(-0.8f, -0.3f * (herdSize / (float)totalMenCount));
        }

        // Token: 0x06002E2D RID: 11821 RVA: 0x000C2AB8 File Offset: 0x000C0CB8
        private float GetWoundedModifier(int totalMenCount, int numWounded, MobileParty party)
        {
            if (numWounded <= totalMenCount / 4)
            {
                return 0f;
            }
            if (totalMenCount == 0)
            {
                return -0.5f;
            }
            float baseNumber = Math.Max(-0.8f, -0.05f * numWounded / totalMenCount);
            ExplainedNumber explainedNumber = new ExplainedNumber(baseNumber, false, null);
            PerkHelper.AddPerkBonusForParty(DefaultPerks.Medicine.Sledges, party, true, ref explainedNumber);
            return explainedNumber.ResultNumber;
        }

        // Token: 0x06002E2E RID: 11822 RVA: 0x000C2B10 File Offset: 0x000C0D10
        private void GetCavalryRatioModifier(MobileParty party, int totalMenCount, int totalCavalryCount, ref ExplainedNumber result)
        {
            if (totalMenCount > 0 && totalCavalryCount > 0)
            {
                float value = 0.4f * totalCavalryCount / totalMenCount;
                result.AddFactor(value, _textCavalry);
            }
        }

        // Token: 0x06002E2F RID: 11823 RVA: 0x000C2B3E File Offset: 0x000C0D3E
        private float GetMountedFootmenRatioModifier(int totalMenCount, int totalCavalryCount)
        {
            if (totalMenCount == 0)
            {
                return 0f;
            }
            return 0.2f * totalCavalryCount / totalMenCount;
        }

        // Token: 0x06002E30 RID: 11824 RVA: 0x000C2B54 File Offset: 0x000C0D54
        private void GetFootmenPerkBonus(MobileParty party, int totalMenCount, int totalFootmenCount, ref ExplainedNumber result)
        {
            if (totalMenCount == 0)
            {
                return;
            }
            float num = totalFootmenCount / (float)totalMenCount;
            PerkHelper.AddPerkBonusForParty(DefaultPerks.Athletics.Strong, party, false, ref result);
        }

        // Token: 0x06002E31 RID: 11825 RVA: 0x000C2B6E File Offset: 0x000C0D6E
        private static float GetSizeModifierWounded(int totalMenCount, int totalWoundedMenCount)
        {
            return (float)Math.Pow((10f + totalMenCount) / (10f + totalMenCount - totalWoundedMenCount), 0.33000001311302185);
        }

        // Token: 0x06002E32 RID: 11826 RVA: 0x000C2B94 File Offset: 0x000C0D94
        private static float GetSizeModifierPrisoner(int totalMenCount, int totalPrisonerCount)
        {
            return (float)Math.Pow((10f + totalMenCount + totalPrisonerCount) / (10f + totalMenCount), 0.33000001311302185);
        }

        // Token: 0x04000FC6 RID: 4038
        private static readonly TextObject _textCargo = new TextObject("{=fSGY71wd}Cargo within capacity", null);

        // Token: 0x04000FC7 RID: 4039
        private static readonly TextObject _textOverburdened = new TextObject("{=xgO3cCgR}Overburdened", null);

        // Token: 0x04000FC8 RID: 4040
        private static readonly TextObject _textOverPartySize = new TextObject("{=bO5gL3FI}Men within party size", null);

        // Token: 0x04000FC9 RID: 4041
        private static readonly TextObject _textOverPrisonerSize = new TextObject("{=Ix8YjLPD}Men within prisoner size", null);

        // Token: 0x04000FCA RID: 4042
        private static readonly TextObject _textCavalry = new TextObject("{=YVGtcLHF}Cavalry", null);

        // Token: 0x04000FCB RID: 4043
        private static readonly TextObject _textKhuzaitCavalryBonus = new TextObject("{=yi07dBks}Khuzait Cavalry Bonus", null);

        // Token: 0x04000FCC RID: 4044
        private static readonly TextObject _textMountedFootmen = new TextObject("{=5bSWSaPl}Footmen on horses", null);

        // Token: 0x04000FCD RID: 4045
        private static readonly TextObject _textWounded = new TextObject("{=aLsVKIRy}Wounded Members", null);

        // Token: 0x04000FCE RID: 4046
        private static readonly TextObject _textPrisoners = new TextObject("{=N6QTvjMf}Prisoners", null);

        // Token: 0x04000FCF RID: 4047
        private static readonly TextObject _textHerd = new TextObject("{=NhAMSaWU}Herd", null);

        // Token: 0x04000FD0 RID: 4048
        private static readonly TextObject _textHighMorale = new TextObject("{=aDQcIGfH}High Morale", null);

        // Token: 0x04000FD1 RID: 4049
        private static readonly TextObject _textLowMorale = new TextObject("{=ydspCDIy}Low Morale", null);

        // Token: 0x04000FD2 RID: 4050
        private static readonly TextObject _textCaravan = new TextObject("{=vvabqi2w}Caravan", null);

        // Token: 0x04000FD3 RID: 4051
        private static readonly TextObject _textDisorganized = new TextObject("{=JuwBb2Yg}Disorganized", null);

        // Token: 0x04000FD4 RID: 4052
        private static readonly TextObject _movingInForest = new TextObject("{=rTFaZCdY}Forest", null);

        // Token: 0x04000FD5 RID: 4053
        private static readonly TextObject _fordEffect = new TextObject("{=NT5fwUuJ}Fording", null);

        // Token: 0x04000FD6 RID: 4054
        private static readonly TextObject _night = new TextObject("{=fAxjyMt5}Night", null);

        // Token: 0x04000FD7 RID: 4055
        private static readonly TextObject _snow = new TextObject("{=vLjgcdgB}Snow", null);

        // Token: 0x04000FD8 RID: 4056
        private static readonly TextObject _desert = new TextObject("{=ecUwABe2}Desert", null);

        // Token: 0x04000FD9 RID: 4057
        private static readonly TextObject _sturgiaSnowBonus = new TextObject("{=0VfEGekD}Sturgia Snow Bonus", null);

        // Token: 0x04000FDA RID: 4058
        private static readonly TextObject _culture = GameTexts.FindText("str_culture", null);

        // Token: 0x04000FDB RID: 4059
        private const float BaseSpeed = 5f;

        // Token: 0x04000FDC RID: 4060
        private const float MininumSpeed = 1f;

        // Token: 0x04000FDD RID: 4061
        private const float MovingAtForestEffect = -0.3f;

        // Token: 0x04000FDE RID: 4062
        private const float MovingAtWaterEffect = -0.3f;

        // Token: 0x04000FDF RID: 4063
        private const float MovingAtNightEffect = -0.25f;

        // Token: 0x04000FE0 RID: 4064
        private const float MovingOnSnowEffect = -0.1f;

        // Token: 0x04000FE1 RID: 4065
        private const float MovingInDesertEffect = -0.1f;

        // Token: 0x04000FE2 RID: 4066
        private const float CavalryEffect = 0.4f;

        // Token: 0x04000FE3 RID: 4067
        private const float MountedFootMenEffect = 0.2f;

        // Token: 0x04000FE4 RID: 4068
        private const float HerdEffect = -0.4f;

        // Token: 0x04000FE5 RID: 4069
        private const float WoundedEffect = -0.05f;

        // Token: 0x04000FE6 RID: 4070
        private const float CargoEffect = -0.02f;

        // Token: 0x04000FE7 RID: 4071
        private const float OverburdenedEffect = -0.4f;

        // Token: 0x04000FE8 RID: 4072
        private const float HighMoraleThresold = 70f;

        // Token: 0x04000FE9 RID: 4073
        private const float LowMoraleThresold = 30f;

        // Token: 0x04000FEA RID: 4074
        private const float HighMoraleEffect = 0.05f;

        // Token: 0x04000FEB RID: 4075
        private const float LowMoraleEffect = -0.1f;

        // Token: 0x04000FEC RID: 4076
        private const float DisorganizedEffect = -0.4f;

    }
}
