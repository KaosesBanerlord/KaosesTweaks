using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace KaosesTweaks.Models
{
    public class KaosesCharacterDevelopmentModel : DefaultCharacterDevelopmentModel
    {

        // Token: 0x17000B0F RID: 2831
        // (get) Token: 0x06002BD0 RID: 11216 RVA: 0x000A858F File Offset: 0x000A678F
        public override int LevelsPerAttributePoint
        {
            get
            {
                if (Statics._settings.CharacterLevelsPerAttributeModifiers)
                {
                    return Statics._settings.CharacterLevelsPerAttributeValue;
                }
                return 4;
            }
        }

        // Token: 0x17000B10 RID: 2832
        // (get) Token: 0x06002BD1 RID: 11217 RVA: 0x000A8592 File Offset: 0x000A6792
        public override int FocusPointsPerLevel
        {
            get
            {
                if (Statics._settings.CharacterFocusPerLevelModifiers)
                {
                    return Statics._settings.CharacterFocusPerLevelValue;
                }
                return 1;
            }
        }
        /*
         TODO have multiplier for these
         */

        // Token: 0x06002BD7 RID: 11223 RVA: 0x000A8638 File Offset: 0x000A6838
        public override float CalculateLearningRate(Hero hero, SkillObject skill)
        {
            int level = hero.Level;
            int attributeValue = hero.GetAttributeValue(skill.CharacterAttributeEnum);
            int focus = hero.HeroDeveloper.GetFocus(skill);
            int skillValue = hero.GetSkillValue(skill);
            float LearningRate = CalculateLearningRate(attributeValue, focus, skillValue, level, skill.CharacterAttribute.Name, false).ResultNumber;
            return LearningRate;
        }


        // Token: 0x06002BD8 RID: 11224 RVA: 0x000A8690 File Offset: 0x000A6890
        public override ExplainedNumber CalculateLearningRate(int attributeValue, int focusValue, int skillValue, int characterLevel, TextObject attributeName, bool includeDescriptions = false)
        {
            float learningMultiplier = 1.0f;

            if (Statics._settings.LearningRateEnabled)
            {
                learningMultiplier =  Statics._settings.LearningRateMultiplier;
            }

            float baseNumber = (20f / (10f + (float)characterLevel) * learningMultiplier);
            ExplainedNumber result = new ExplainedNumber(baseNumber, true, null);

            //Ux.MessageDebug("KaosesCharacterDevelopmentModel: base result " + result.ResultNumber.ToString());


            result.AddFactor(((0.4f * (float)attributeValue) * learningMultiplier), attributeName);
            //Ux.MessageDebug("KaosesCharacterDevelopmentModel: attributeName result " + result.ResultNumber.ToString());
            result.AddFactor(((float)focusValue * 1f) * learningMultiplier, _skillFocusText);
            //Ux.MessageDebug("KaosesCharacterDevelopmentModel: base _skillFocusText " + result.ResultNumber.ToString());

            int num = MBMath.Round(this.CalculateLearningLimit(attributeValue, focusValue, null, false).ResultNumber);
            float test = 0.0f;
            int num2 = 0;
            if (skillValue > num)
            {
                num2 = skillValue - num;
                result.AddFactor(-1f - 0.1f * (float)num2, _overLimitText);
                test = -1f - 0.1f * (float)num2;
            }

            result.LimitMin(0f);
/*
           
            float att1 = 0.4f * (float)attributeValue;
            float fv1 = (float)focusValue * 1f;

                Ux.MessageDebug("KaosesCharacterDevelopmentModel: CalculateLearningRate   " 
                    + "  base is 20f / (10f + (float)characterLevel) " + (20f / (10f + (float)characterLevel)).ToString()   /// 0.61
                    + "  baseNumber " + baseNumber.ToString()                                                               /// 0.61
                    + "  attributeName " + (0.4f * (float)attributeValue).ToString()                                        /// result 1.575758  ?? +0.97  num = baseNumber * num * 0.01f;
                    + "  = " + ((float)Math.Round((double)att1, 3) * 100f).ToString()                                       /// 160  eg 0.61 * 160 * 0.01  = 0.97
                    + "  _skillFocusText " + ((float)focusValue * 1f).ToString()                                            /// 4  ??? +2.42   num = baseNumber * num * 0.01f;
                    + "  = " + ((float)Math.Round((double)fv1, 3) * 100f).ToString()                                        /// 400  eg 0.61 * 400 * 0.01  =  2.42 
                    + "  skillValue " + skillValue.ToString()                                                               /// 4
                    + "  num " + num.ToString() + "   MBMath.Round(this.CalculateLearningLimit(attributeValue, focusValue, null, false).ResultNumber);   "
                    + "  skillValue > num  " + (skillValue > num).ToString()
                    + "  if (skillValue > num) "
                    + "  ->num2 = skillValue - num  " + (-1f - 0.1f * (float)num2).ToString()  
                    + "  ->test (-1f - 0.1f * (float)num2)  " + test.ToString()  
                    );
*/


            return result;
        }




        // Token: 0x04000EA0 RID: 3744
        private static TextObject _attributeText = new TextObject("{=AT6v10NK}Attribute", null);

        // Token: 0x04000EA1 RID: 3745
        private static TextObject _skillFocusText = new TextObject("{=MRktqZwu}Skill Focus", null);

        // Token: 0x04000EA2 RID: 3746
        private static TextObject _overLimitText = new TextObject("{=bcA7ZuyO}Learning Limit Exceeded", null);



        /*
         Maybe dont need to copy this function over
         */
        // Token: 0x06002BD6 RID: 11222 RVA: 0x000A85EC File Offset: 0x000A67EC

        public override ExplainedNumber CalculateLearningLimit(int attributeValue, int focusValue, TextObject attributeName, bool includeDescriptions = false)
        {
            ExplainedNumber result = new ExplainedNumber(0f, includeDescriptions, null);
            result.Add((float)((attributeValue - 1) * 10), attributeName, null);
            result.Add((float)(focusValue * 30), _skillFocusText, null);
            result.LimitMin(0f);
            return result;
        }



        // Token: 0x04000E8D RID: 3725
        private const int MaxCharacterLevels = 62;

        // Token: 0x04000E8E RID: 3726
        private const int MaxAttributeLevel = 11;

        // Token: 0x04000E8F RID: 3727
        private const int SkillPointsAtLevel1 = 1;

        // Token: 0x04000E90 RID: 3728
        private const int SkillPointsGainNeededInitialValue = 1000;

        // Token: 0x04000E91 RID: 3729
        private const int SkillPointsGainNeededIncreasePerLevel = 1000;

        // Token: 0x04000E92 RID: 3730
        private readonly int[] _skillsRequiredForLevel = new int[63];

        // Token: 0x04000E93 RID: 3731
        private const int FocusPointsPerLevelConst = 1;

        // Token: 0x04000E94 RID: 3732
        private const int LevelsPerAttributePointConst = 4;

        // Token: 0x04000E95 RID: 3733
        private const int FocusPointCostToOpenSkillConst = 0;

        // Token: 0x04000E96 RID: 3734
        private const int FocusPointsAtStartConst = 5;

        // Token: 0x04000E97 RID: 3735
        private const int AttributePointsAtStartConst = 15;

        // Token: 0x04000E98 RID: 3736
        private const int MaxSkillLevels = 1024;

        // Token: 0x04000E99 RID: 3737
        private readonly int[] _xpRequiredForSkillLevel = new int[1024];

        // Token: 0x04000E9A RID: 3738
        private const int XpRequirementForFirstLevel = 30;

        // Token: 0x04000E9B RID: 3739
        private const int MaxSkillPoint = 2147483647;

        // Token: 0x04000E9C RID: 3740
        private const int traitThreshold1 = 1000;

        // Token: 0x04000E9D RID: 3741
        private const int traitThreshold2 = 4000;

        // Token: 0x04000E9E RID: 3742
        private const int traitMaxValue1 = 2500;

        // Token: 0x04000E9F RID: 3743
        private const int traitMaxValue2 = 6000;

        // Token: 0x04000EA0 RID: 3744



    }


}


