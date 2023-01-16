using HarmonyLib;
using KaosesCommon.Utils;
using KaosesTweaks.Objects;
using KaosesTweaks.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.LinQuick;
using TaleWorlds.ObjectSystem;

namespace KaosesTweaks.Patches
{

    //[HarmonyReversePatch(HarmonyReversePatchType.Original)]
    [HarmonyPatch(typeof(CompanionsCampaignBehavior), "CreateCompanionAndAddToSettlement")]
    class CreateCompanionAndAddToSettlementPatch
    {
        private static bool Prefix(CompanionsCampaignBehavior __instance, Settlement settlement, ref Dictionary<CharacterObject, int> ____companionTemplates, ref int ____cachedCompanionCount)
        {

            //List<(CharacterObject, float)> weightList = new List<(CharacterObject, float)>();

            //IM.MessageDebug("CreateCompanionAndAddToSettlementPatch Prefix START: ___companionTemplates: " + ____companionTemplates.Count() + "  ____cachedCompanionCount: " + ____cachedCompanionCount.ToString());

            foreach (KeyValuePair<CharacterObject, int> companionTemplate1 in ____companionTemplates)
            {
                CharacterObject companionTemplate = companionTemplate1.Key;

                Settlement settlement1 = Town.AllTowns.GetRandomElementWithPredicate<Town>((Func<Town, bool>)(x => x.Culture == companionTemplate.Culture))?.Settlement;
                Settlement bornSettlement;
                if (settlement1 != null)
                {
                    List<Settlement> e = new List<Settlement>();
                    //foreach (Village allVillage in (IEnumerable<Village>)Campaign.Current.AllVillages)
                    foreach (Village allVillage in (IEnumerable<Village>)Village.All)
                    {
                        if ((double)Campaign.Current.Models.MapDistanceModel.GetDistance(allVillage.Settlement, settlement1) < 30.0)
                            e.Add(allVillage.Settlement);
                    }
                    bornSettlement = e.Count > 0 ? e.GetRandomElement<Settlement>().Village.Bound : settlement1;
                }
                else
                {
                    bornSettlement = Town.AllTowns.GetRandomElement<Town>().Settlement;
                }
                Hero specialHero = HeroCreator.CreateSpecialHero(companionTemplate, bornSettlement, age: (Campaign.Current.Models.AgeModel.HeroComesOfAge + 5 + MBRandom.RandomInt(27)));
                CreateCompanionAndAddToSettlementPatch.AdjustEquipment(specialHero);
                specialHero.ChangeState(Hero.CharacterStates.Active);
                EnterSettlementAction.ApplyForCharacterOnly(specialHero, settlement);
                //IM.MessageDebug("StringId: " + specialHero.StringId + "  Name: " + specialHero.Name);
                ____cachedCompanionCount++;
            }
            //IM.MessageDebug("CreateCompanionAndAddToSettlementPatch Prefix END: ___companionTemplates: " + ____companionTemplates.Count() + "  ____cachedCompanionCount: " + ____cachedCompanionCount.ToString());

            return false;
        }


        private static void AdjustEquipment(Hero hero)
        {
            CreateCompanionAndAddToSettlementPatch.AdjustEquipmentImp(hero.BattleEquipment);
            CreateCompanionAndAddToSettlementPatch.AdjustEquipmentImp(hero.CivilianEquipment);
        }

        private static void AdjustEquipmentImp(Equipment equipment)
        {
            ItemModifier itemModifier1 = MBObjectManager.Instance.GetObject<ItemModifier>("companion_armor");
            ItemModifier itemModifier2 = MBObjectManager.Instance.GetObject<ItemModifier>("companion_weapon");
            ItemModifier itemModifier3 = MBObjectManager.Instance.GetObject<ItemModifier>("companion_horse");
            for (EquipmentIndex index = EquipmentIndex.WeaponItemBeginSlot; index < EquipmentIndex.NumEquipmentSetSlots; ++index)
            {
                EquipmentElement equipmentElement = equipment[index];
                if (equipmentElement.Item != null)
                {
                    if (equipmentElement.Item.ArmorComponent != null)
                        equipment[index] = new EquipmentElement(equipmentElement.Item, itemModifier1);
                    else if (equipmentElement.Item.HorseComponent != null)
                        equipment[index] = new EquipmentElement(equipmentElement.Item, itemModifier3);
                    else if (equipmentElement.Item.WeaponComponent != null)
                        equipment[index] = new EquipmentElement(equipmentElement.Item, itemModifier2);
                }
            }
        }
        static bool Prepare() => Factory.Settings is { } settings && settings.UnlimitedWanderersPatch;
    }

    //[HarmonyReversePatch(HarmonyReversePatchType.Original)]
    /*    [HarmonyPatch(typeof(CompanionsCampaignBehavior), "CreateCompanionAndAddToSettlement")]
        class CreateCompanionAndAddToSettlementUnPatch
        {
            private static void Postfix(CompanionsCampaignBehavior __instance, Settlement settlement, Dictionary<CharacterObject, int> ____companionTemplates, int ____cachedCompanionCount)
            {
                IM.MessageDebug("___companionTemplates: " + ____companionTemplates.Count() + "  ____cachedCompanionCount: " + ____cachedCompanionCount.ToString());
                //return true;
            }

            static bool Prepare() => Factory.Settings is { } settings && !settings.UnlimitedWanderersPatch;
        }*/
}
