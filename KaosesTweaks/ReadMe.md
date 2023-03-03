# Kaoses Tweaks

## Description
Kaoses Tweaks mod for bannerlords. 

## ChangeLog
- 0.0.1 
  - Initial Creation of Module code.

## Todo





            CharacterConfig
            CharacterFactory
            BattleConfig
            BattleFactory
            CraftingConfig
            CraftingFactory
            AmmoConfig
            AmmoFactory
            SizeConfig
            SizeFactory
            SpeedsConfig
            SpeedsFactory
            SettlementConfig
            SettlementFactory
            TradeConfig
            TradeFactory
            WagesConfig
            WagesFactory
            WorkshopConfig
            WorkshopFactory
            KillingBanditsConfig
            KillingBanditsFactory

                        // Set any Drop downs manually here
                        config.skillMode = skillMode.SelectedValue.ToString();

                        if (skillMode.SelectedValue.ToString() == "Global")
                        {
                            config.SkillXpUseIndividualMultiplers = false;
                            config.SkillXpUseGlobalMultipler = true;
                        }
                        else if (skillMode.SelectedValue.ToString() == "Individual")
                        {
                            config.SkillXpUseIndividualMultiplers = true;
                            config.SkillXpUseGlobalMultipler = false;
                        }
                        else
                        {
                            config.SkillXpUseIndividualMultiplers = false;
                            config.SkillXpUseGlobalMultipler = false;
                        }