using KaosesTweaks.Utils;
using System;
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
            int attributeValue = hero.GetAttributeValue(skill.CharacterAttribute);
            int focus = hero.HeroDeveloper.GetFocus(skill);
            int skillValue = hero.GetSkillValue(skill);
            if (Statics._settings.LearningDebug)
            {
                IM.MessageDebug("KT CalculateLearningRate: " + skill.CharacterAttribute.Name.ToString());
            }
            float LearningRate = CalculateLearningRate(attributeValue, focus, skillValue, level, skill.CharacterAttribute.Name, false).ResultNumber;
            return LearningRate;
        }

        // Token: 0x06002BD8 RID: 11224 RVA: 0x000A8690 File Offset: 0x000A6890
        public override ExplainedNumber CalculateLearningRate(int attributeValue, int focusValue, int skillValue, int characterLevel, TextObject attributeName, bool includeDescriptions = false)
        {
            float learningMultiplier = 1.0f;
            TextObject attrText = attributeName;
            TextObject focusText = _skillFocusText;
            if (Statics._settings.LearningRateEnabled)
            {
                learningMultiplier = Statics._settings.LearningRateMultiplier;
                if (Statics._settings.LearningDebug)
                {
                    IM.MessageDebug("KT attributeName: " + attributeName.ToString());
                }
                attrText = new TextObject("KT " + attributeName.ToString(), null);
                focusText = new TextObject("KT " + _skillFocusText, null);
            }
            ExplainedNumber result = new ExplainedNumber(1.25f * learningMultiplier, true, null);
            result.AddFactor(0.4f * attributeValue, attrText);
            result.AddFactor(focusValue * 1f, focusText);
            int num = MathF.Round(CalculateLearningLimit(attributeValue, focusValue, attributeName, false).ResultNumber);
            int num2 = 0;
            if (skillValue > num)
            {
                num2 = skillValue - num;
                if (Statics._settings.LearningDebug)
                {
                    IM.MessageDebug("_overLimitText REDUCED VALUE: " + num2.ToString());
                }
                result.AddFactor(-1f - 0.1f * num2, _overLimitText);
            }
            result.LimitMin(0f);
            return result;
        }

        // Token: 0x06002BD8 RID: 11224 RVA: 0x000A84C8 File Offset: 0x000A66C8
        public override ExplainedNumber CalculateLearningLimit(int attributeValue, int focusValue, TextObject attributeName, bool includeDescriptions = false)
        {
            ExplainedNumber result = new ExplainedNumber(0f, includeDescriptions, null);

            if (Statics._settings.LearningLimitEnabled)
            {
                result.Add(attributeValue * 10 * Statics._settings.LearningLimitMultiplier, attributeName, null);
                result.Add(focusValue * 30 * Statics._settings.LearningLimitMultiplier, _skillFocusText, null);
            }
            else
            {
                result.Add(attributeValue * 10, attributeName, null);
                result.Add(focusValue * 1 * 30, _skillFocusText, null);
            }

            result.LimitMin(0f);
            return result;
        }

        #region Required Source Code
        // Token: 0x04000EA0 RID: 3744

        // Token: 0x04000EA1 RID: 3745
        private static TextObject _skillFocusText = new TextObject("{=MRktqZwu}Skill Focus", null);

        // Token: 0x04000EA2 RID: 3746
        private static TextObject _overLimitText = new TextObject("{=bcA7ZuyO}Learning Limit Exceeded", null);

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
        #endregion


    }






}


