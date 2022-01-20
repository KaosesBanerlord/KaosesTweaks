using KaosesTweaks.Utils;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace KaosesTweaks.Models
{
    public class KaosesSmithingModel : DefaultSmithingModel
    {

        // Token: 0x06002EBF RID: 11967 RVA: 0x000C0FD4 File Offset: 0x000BF1D4
        public override int GetSkillXpForRefining(ref Crafting.RefiningFormula refineFormula)
        {
            float baseXp = MathF.Round(0.3f * (GetCraftingMaterialItem(refineFormula.Output).Value * refineFormula.OutputCount));
            if (Statics._settings.SmithingXpModifiers)
            {
                baseXp *= Statics._settings.SmithingRefiningXpValue;
                if (Statics._settings.CraftingDebug)
                {
                    IM.MessageDebug("GetSkillXpForRefining  base: " + MathF.Round(0.3f * (GetCraftingMaterialItem(refineFormula.Output).Value * refineFormula.OutputCount)).ToString() + "  new :" + baseXp.ToString());
                }
            }
            return (int)baseXp;
        }

        // Token: 0x06002EC0 RID: 11968 RVA: 0x000C0FFC File Offset: 0x000BF1FC
        public override int GetSkillXpForSmelting(ItemObject item)
        {
            float baseXp = MathF.Round(0.02f * item.Value);
            if (Statics._settings.SmithingXpModifiers)
            {
                baseXp *= Statics._settings.SmithingSmeltingXpValue;
                if (Statics._settings.CraftingDebug)
                {
                    IM.MessageDebug("GetSkillXpForSmelting  base: " + MathF.Round(0.02f * item.Value).ToString() + "  new :" + baseXp.ToString());
                }
            }
            return (int)baseXp;
        }

        // Token: 0x06002EC1 RID: 11969 RVA: 0x000C1010 File Offset: 0x000BF210
        public override int GetSkillXpForSmithingInFreeBuildMode(ItemObject item)
        {
            float baseXp = MathF.Round(0.02f * item.Value);
            if (Statics._settings.SmithingXpModifiers)
            {
                baseXp *= Statics._settings.SmithingSmithingXpValue;
                if (Statics._settings.CraftingDebug)
                {
                    IM.MessageDebug("GetSkillXpForSmithing  base: " + MathF.Round(0.02f * item.Value).ToString() + "  new :" + baseXp.ToString());
                }
            }
            return (int)baseXp;
        }

        // Token: 0x06002EC1 RID: 11969 RVA: 0x000C1010 File Offset: 0x000BF210
        public override int GetSkillXpForSmithingInCraftingOrderMode(ItemObject item)
        {
            float baseXp = MathF.Round(0.1f * item.Value);
            if (Statics._settings.SmithingXpModifiers)
            {
                baseXp *= Statics._settings.SmithingSmithingXpValue;
                if (Statics._settings.CraftingDebug)
                {
                    IM.MessageDebug("GetSkillXpForSmithing  base: " + MathF.Round(0.1f * item.Value).ToString() + "  new :" + baseXp.ToString());
                }
            }
            return (int)baseXp;
        }

        // Token: 0x06002EC2 RID: 11970 RVA: 0x000C1024 File Offset: 0x000BF224
        public override int GetEnergyCostForRefining(ref Crafting.RefiningFormula refineFormula, Hero hero)
        {
            int num = 6;
            if (Statics._settings.SmithingEnergyDisable)
            {
                if (Statics._settings.CraftingDebug)
                {
                    IM.MessageDebug("GetEnergyCostForRefining: DISABLED ");
                }
                num = 0;
            }
            else
            {
                if (Statics._settings.CraftingStaminaTweakEnabled)
                {
                    float tmp = num * Statics._settings.SmithingEnergyRefiningValue;
                    if (Statics._settings.CraftingDebug)
                    {
                        IM.MessageDebug("GetEnergyCostForRefining Old : " + num.ToString() + " New : " + tmp.ToString());
                    }
                    num = (int)tmp;
                }
            }
            if (hero.GetPerkValue(DefaultPerks.Crafting.PracticalRefiner))
            {
                num = (num + 1) / 2;
            }
            return num;
        }

        // Token: 0x06002EC3 RID: 11971 RVA: 0x000C1048 File Offset: 0x000BF248
        public override int GetEnergyCostForSmithing(ItemObject item, Hero hero)
        {
            int.TryParse(item.Tier.ToString(), out int itemTier);
            int tier6 = 6;
            //int num = (int)(10 + ItemObject.ItemTiers.Tier6 * item.Tier);
            int num = 10 + tier6 * itemTier;
            if (Statics._settings.SmithingEnergyDisable)
            {
                if (Statics._settings.CraftingDebug)
                {
                    IM.MessageDebug("GetEnergyCostForSmithing: DISABLED ");
                }
                num = 0;
            }
            else
            {
                if (Statics._settings.CraftingStaminaTweakEnabled)
                {
                    float tmp = num * Statics._settings.SmithingEnergySmithingValue;
                    if (Statics._settings.CraftingDebug)
                    {
                        IM.MessageDebug("GetEnergyCostForSmithing Old : " + num.ToString() + " New : " + tmp.ToString());
                    }
                    num = (int)tmp;
                }
            }
            if (hero.GetPerkValue(DefaultPerks.Crafting.PracticalSmith))
            {
                num = (num + 1) / 2;
            }
            return num;
        }

        // Token: 0x06002EC4 RID: 11972 RVA: 0x000C1078 File Offset: 0x000BF278
        public override int GetEnergyCostForSmelting(ItemObject item, Hero hero)
        {
            int num = 10;
            if (Statics._settings.SmithingEnergyDisable)
            {
                if (Statics._settings.CraftingDebug)
                {
                    IM.MessageDebug("GetEnergyCostForSmelting: DISABLED ");
                }
                num = 0;
            }
            else
            {
                if (Statics._settings.CraftingStaminaTweakEnabled)
                {
                    float tmp = num * Statics._settings.SmithingEnergySmeltingValue;
                    if (Statics._settings.CraftingDebug)
                    {
                        IM.MessageDebug("GetEnergyCostForSmelting Old : " + num.ToString() + " New : " + tmp.ToString());
                    }
                    num = (int)tmp;
                }
            }
            if (hero.GetPerkValue(DefaultPerks.Crafting.PracticalSmelter))
            {
                num = (num + 1) / 2;
            }
            return num;
        }

        // Token: 0x06002ED9 RID: 11993 RVA: 0x000C2A6C File Offset: 0x000C0C6C
        public override int GetModifierTierForSmithedWeapon(WeaponDesign weaponDesign, Hero hero)
        {
            if (!Statics._settings.CraftingQualityTweaksEnabled)
            {
                return base.GetModifierTierForSmithedWeapon(weaponDesign, hero);
            }
            else
            {

            }
            int num = CalculateWeaponDesignDifficulty(weaponDesign);
            int num2 = hero.CharacterObject.GetSkillValue(DefaultSkills.Crafting) - num;
            if (num2 < 0)
            {
                return GetPenaltyForLowSkill(num2);
            }
            float randomFloat = MBRandom.RandomFloat;
            if (hero.GetPerkValue(DefaultPerks.Crafting.ExperiencedSmith) && randomFloat < 0.2f)
            {
                return Statics._settings.CraftingQualityFineValue;
            }
            if (hero.GetPerkValue(DefaultPerks.Crafting.MasterSmith) && randomFloat < 0.35f)
            {
                return Statics._settings.CraftingQualityMasterValue;
            }
            if (hero.GetPerkValue(DefaultPerks.Crafting.LegendarySmith))
            {
                float num3 = 0.05f + Math.Max(0f, hero.GetSkillValue(DefaultSkills.Crafting) - 300) * 0.01f;
                if (randomFloat > 0.5f && randomFloat < 0.5f + num3)
                {
                    return Statics._settings.CraftingQualityLegendaryValue;
                }
            }
            return 0;
        }

        // Token: 0x06002EDC RID: 11996 RVA: 0x000C2B94 File Offset: 0x000C0D94
        //RefiningFormula(CraftingMaterials input1, int input1Count, CraftingMaterials input2, int input2Count, CraftingMaterials output, int outputCount = 1, CraftingMaterials output2 = CraftingMaterials.IronOre, int output2Count = 0);
        public override IEnumerable<Crafting.RefiningFormula> GetRefiningFormulas(Hero weaponsmith)
        {
            if (weaponsmith.GetPerkValue(DefaultPerks.Crafting.CharcoalMaker))
            {
                yield return new Crafting.RefiningFormula(CraftingMaterials.Wood, GetModifiedFormulaInputCost(2), CraftingMaterials.Iron1, GetModifiedFormulaInputCost(0), CraftingMaterials.Charcoal, GetModifiedFormulaOutPut(3), CraftingMaterials.IronOre, GetModifiedFormulaOutPut(0));
            }
            else
            {
                yield return new Crafting.RefiningFormula(CraftingMaterials.Wood, GetModifiedFormulaInputCost(2), CraftingMaterials.Iron1, GetModifiedFormulaInputCost(0), CraftingMaterials.Charcoal, GetModifiedFormulaOutPut(1), CraftingMaterials.IronOre, 0);
            }
            yield return new Crafting.RefiningFormula(CraftingMaterials.IronOre, GetModifiedFormulaInputCost(1), CraftingMaterials.Charcoal, GetModifiedFormulaInputCost(1), CraftingMaterials.Iron1, GetModifiedFormulaOutPut(weaponsmith.GetPerkValue(DefaultPerks.Crafting.IronMaker) ? 3 : 2), CraftingMaterials.IronOre, 0);
            yield return new Crafting.RefiningFormula(CraftingMaterials.Iron1, GetModifiedFormulaInputCost(1), CraftingMaterials.Charcoal, GetModifiedFormulaInputCost(1), CraftingMaterials.Iron2, GetModifiedFormulaOutPut(1), CraftingMaterials.IronOre, 0);
            yield return new Crafting.RefiningFormula(CraftingMaterials.Iron2, GetModifiedFormulaInputCost(2), CraftingMaterials.Charcoal, GetModifiedFormulaInputCost(1), CraftingMaterials.Iron3, GetModifiedFormulaOutPut(1), CraftingMaterials.Iron1, 1);
            if (weaponsmith.GetPerkValue(DefaultPerks.Crafting.SteelMaker))
            {
                yield return new Crafting.RefiningFormula(CraftingMaterials.Iron3, GetModifiedFormulaInputCost(2), CraftingMaterials.Charcoal, GetModifiedFormulaInputCost(1), CraftingMaterials.Iron4, GetModifiedFormulaOutPut(1), CraftingMaterials.Iron1, 1);
            }
            if (weaponsmith.GetPerkValue(DefaultPerks.Crafting.SteelMaker2))
            {
                yield return new Crafting.RefiningFormula(CraftingMaterials.Iron4, GetModifiedFormulaInputCost(2), CraftingMaterials.Charcoal, GetModifiedFormulaInputCost(1), CraftingMaterials.Iron5, GetModifiedFormulaOutPut(1), CraftingMaterials.Iron1, 1);
            }
            if (weaponsmith.GetPerkValue(DefaultPerks.Crafting.SteelMaker3))
            {
                yield return new Crafting.RefiningFormula(CraftingMaterials.Iron5, GetModifiedFormulaInputCost(2), CraftingMaterials.Charcoal, GetModifiedFormulaInputCost(1), CraftingMaterials.Iron6, GetModifiedFormulaOutPut(1), CraftingMaterials.Iron1, 1);
            }
            yield break;
        }

        protected int GetModifiedFormulaInputCost(int originalCost)
        {
            float multiplierCost = 1.0f;
            if (Statics._settings.RefiningFormulaTweaksEnabled)
            {
                multiplierCost = Statics._settings.RefiningFormulaInputCostValue;
            }
            int cost = MathF.Round(originalCost * multiplierCost);
            if (Statics._settings.CraftingDebug)
            {
                IM.MessageDebug($"GetRefiningFormulas originalCost: {originalCost}  NewCost: {cost}");
            }
            if (cost < 1 && originalCost != 0)
            {
                cost = 1;
            }
            else if (cost < 1 && originalCost == 0)
            {
                cost = 0;
            }
            return cost;
        }

        protected int GetModifiedFormulaOutPut(int originalOutPut)
        {
            float multiplierReward = 1.0f;
            if (Statics._settings.RefiningFormulaTweaksEnabled)
            {
                multiplierReward = Statics._settings.RefiningFormulaOutputValue;
            }
            int outPut = MathF.Round(originalOutPut * multiplierReward);
            if (Statics._settings.CraftingDebug)
            {
                IM.MessageDebug($"GetRefiningFormulas originalOutPut: {originalOutPut}  NewOutPut: {outPut}");
            }
            if (outPut < 1 && originalOutPut != 0)
            {
                outPut = 1;
            }
            else if (outPut < 1 && originalOutPut == 0)
            {
                outPut = 0;
            }
            return outPut;

        }


        // Token: 0x06002EDA RID: 11994 RVA: 0x000C2B28 File Offset: 0x000C0D28
        protected int GetPenaltyForLowSkill(int difference)
        {
            float num = MBRandom.RandomFloat;
            num += -0.01f * difference;
            if (num < 0.4f)
            {
                return -1;
            }
            if (num < 0.7f)
            {
                return -2;
            }
            if (num < 0.9f)
            {
                return -3;
            }
            if (num >= 1f)
            {
                return -5;
            }
            return -4;
        }

    }
}
