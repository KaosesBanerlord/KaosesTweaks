using KaosesTweaks.Utils;
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
            float baseXp = MathF.Round(0.3f * (float)(this.GetCraftingMaterialItem(refineFormula.Output).Value * refineFormula.OutputCount));
            if (Statics._settings.SmithingXpModifiers)
            {
                baseXp *= Statics._settings.SmithingRefiningXpValue;
                if(Statics._settings.CraftingDebug)
                {
                    IM.MessageDebug("GetSkillXpForRefining  base: " + (MathF.Round(0.3f * (float)(this.GetCraftingMaterialItem(refineFormula.Output).Value * refineFormula.OutputCount))).ToString() + "  new :" + baseXp.ToString());
                }
            }
            return (int)baseXp;
        }

        // Token: 0x06002EC0 RID: 11968 RVA: 0x000C0FFC File Offset: 0x000BF1FC
        public override int GetSkillXpForSmelting(ItemObject item)
        {
            float baseXp = MathF.Round(0.02f * (float)item.Value);
            if (Statics._settings.SmithingXpModifiers)
            {
                baseXp *= Statics._settings.SmithingSmeltingXpValue;
                if (Statics._settings.CraftingDebug)
                {
                    IM.MessageDebug("GetSkillXpForSmelting  base: " + (MathF.Round(0.02f * (float)item.Value)).ToString() + "  new :" + baseXp.ToString());
                }
            }
            return (int)baseXp;
        }

        // Token: 0x06002EC1 RID: 11969 RVA: 0x000C1010 File Offset: 0x000BF210
        public override int GetSkillXpForSmithing(ItemObject item)
        {
            float baseXp = MathF.Round(0.1f * (float)item.Value);
            if (Statics._settings.SmithingXpModifiers)
            {
                baseXp *= Statics._settings.SmithingSmithingXpValue;
                if (Statics._settings.CraftingDebug)
                {
                    IM.MessageDebug("GetSkillXpForSmithing  base: " + (MathF.Round(0.1f * (float)item.Value)).ToString() + "  new :" + baseXp.ToString());
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
            }else
            {
                if(Statics._settings.CraftingStaminaTweakEnabled)
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
            int num = (int)(10 + tier6 * itemTier);
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

    }
}
