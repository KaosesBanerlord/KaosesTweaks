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
            if (Statics._settings.SmithingRefiningXpModifiers)
            {
                //Logging.Lm("Original XP ForRefining: "+ baseXp.ToString());
                baseXp *= Statics._settings.SmithingRefiningXpValue;
                //Logging.Lm("new XP ForRefining: " + baseXp.ToString());
            }
            return (int)baseXp;
        }

        // Token: 0x06002EC0 RID: 11968 RVA: 0x000C0FFC File Offset: 0x000BF1FC
        public override int GetSkillXpForSmelting(ItemObject item)
        {
            float baseXp = MathF.Round(0.02f * (float)item.Value);
            if (Statics._settings.SmithingSmeltingXpModifiers)
            {
                //Logging.Lm("Original XP ForSmelting: " + baseXp.ToString());
                baseXp *= Statics._settings.SmithingSmeltingXpValue;
                //Logging.Lm("new XP ForSmelting: " + baseXp.ToString());
            }
            return (int)baseXp;
        }

        // Token: 0x06002EC1 RID: 11969 RVA: 0x000C1010 File Offset: 0x000BF210
        public override int GetSkillXpForSmithing(ItemObject item)
        {
            float baseXp = MathF.Round(0.1f * (float)item.Value);
            if (Statics._settings.SmithingSmithingXpModifiers)
            {
                //Logging.Lm("Original XP XpForSmithing: " + baseXp.ToString());
                baseXp *= Statics._settings.SmithingSmithingXpValue;
                //Logging.Lm("new XP XpForSmithing: " + baseXp.ToString());
            }
            return (int)baseXp;
        }

        // Token: 0x06002EC2 RID: 11970 RVA: 0x000C1024 File Offset: 0x000BF224
        public override int GetEnergyCostForRefining(ref Crafting.RefiningFormula refineFormula, Hero hero)
        {
            int num = 6;
            if (Statics._settings.SmithingEnergyDisable)
            {
                //Logging.Lm("GetEnergyCostForRefining: DISABLED ");
                num = 0;
            }else
            {
                if(Statics._settings.SmithingEnergyRefiningModifiers)
                {
                    //Logging.Lm("GetEnergyCostForRefining Old : "+ num.ToString());
                    float tmp = num * Statics._settings.SmithingEnergyRefiningValue;
                    num = (int)tmp;
                    //Logging.Lm("GetEnergyCostForRefining New : " + num.ToString());
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
            //int itemTier = 0;
            int.TryParse(item.Tier.ToString(), out int itemTier);
            int tier6 = 6;
            //int num = (int)(10 + ItemObject.ItemTiers.Tier6 * item.Tier);
            int num = (int)(10 + tier6 * itemTier);
            if (Statics._settings.SmithingEnergyDisable)
            {
                //Logging.Lm("GetEnergyCostForSmithing: DISABLED ");
                num = 0;
            }
            else
            {
                if (Statics._settings.SmithingEnergySmithingModifiers)
                {
                    //Logging.Lm("GetEnergyCostForSmithing Old : " + num.ToString());
                    float tmp = num * Statics._settings.SmithingEnergySmithingValue;
                    num = (int)tmp;
                    //Logging.Lm("GetEnergyCostForSmithing New : " + num.ToString());
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
                //Logging.Lm("GetEnergyCostForSmelting: DISABLED ");
                num = 0;
            }
            else
            {
                if (Statics._settings.SmithingEnergySmeltingModifiers)
                {
                    //Logging.Lm("GetEnergyCostForSmelting Old : " + num.ToString());
                    float tmp = num * Statics._settings.SmithingEnergySmeltingValue;
                    num = (int)tmp;
                    //Logging.Lm("GetEnergyCostForSmelting New : " + num.ToString());
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
