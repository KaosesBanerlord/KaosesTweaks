using KaosesTweaks.Settings;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.GameComponents;

namespace KaosesTweaks.Models
{
    public class BTAgeModel : DefaultAgeModel
    {
        public override int BecomeInfantAge => MCMSettings.Instance is { } settings && settings.AgeTweaksEnabled ? settings.BecomeInfantAge : base.BecomeInfantAge;

        public override int BecomeChildAge => MCMSettings.Instance is { } settings && settings.AgeTweaksEnabled ? settings.BecomeChildAge : base.BecomeChildAge;

        public override int BecomeTeenagerAge => MCMSettings.Instance is { } settings && settings.AgeTweaksEnabled ? settings.BecomeTeenagerAge : base.BecomeTeenagerAge;

        public override int HeroComesOfAge => MCMSettings.Instance is { } settings && settings.AgeTweaksEnabled ? settings.HeroComesOfAge : base.HeroComesOfAge;

        public override int BecomeOldAge => MCMSettings.Instance is { } settings && settings.AgeTweaksEnabled ? settings.BecomeOldAge : base.BecomeOldAge;

        public override int MaxAge => MCMSettings.Instance is { } settings && settings.AgeTweaksEnabled ? settings.MaxAge : base.MaxAge;

        public IEnumerable<string> GetConfigErrors()
        {
            if (MaxAge <= BecomeOldAge)
                yield return "\'Max Age\' must be greater than \'Become Old \'Age\'.";
            if (BecomeOldAge <= HeroComesOfAge)
                yield return "\'Become Old Age\' must be greater than \'Hero Comes Of Age\'.";
            if (HeroComesOfAge <= BecomeTeenagerAge)
                yield return "\'Hero Comes Of Age\' must be greater than \'Become Teenager Age\'.";
            if (BecomeTeenagerAge <= BecomeChildAge)
                yield return "\'Become Teenager Age\' must be greater than \'Become Child Age\'";
            if (BecomeChildAge <= BecomeInfantAge)
                yield return "\'Become Child Age\' must be greater than \'Become Infant Age\'";
        }
    }
}
