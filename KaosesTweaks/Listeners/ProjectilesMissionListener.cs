using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;


namespace KaosesTweaks.Listeners
{
    public class ProjectilesMissionListener : IMissionListener
    {

        public List<MBGUID> itemsList = new List<MBGUID>();
        public Dictionary<MBGUID, short> itemStackList = new Dictionary<MBGUID, short>();

        public void OnEquipItemsFromSpawnEquipmentBegin(Agent agent, Agent.CreationType creationType)
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0018:Inline variable declaration", Justification = "<Pending>")]
        public void OnEquipItemsFromSpawnEquipment(Agent agent, Agent.CreationType creationType)
        {
            if (agent != null)
            {
                MissionWeapon weapon0;
                MissionWeapon weapon1;
                MissionWeapon weapon2;
                MissionWeapon weapon3;
                if (agent.IsHuman)//agent.IsMainAgent
                {
                    MissionEquipment meq = agent.Equipment;
                    EquipmentIndex eIndex = 0;

                    weapon0 = agent.Equipment[EquipmentIndex.Weapon0];
                    weapon1 = agent.Equipment[EquipmentIndex.Weapon1];
                    weapon2 = agent.Equipment[EquipmentIndex.Weapon2];
                    weapon3 = agent.Equipment[EquipmentIndex.Weapon3];

                    if (!weapon0.IsEmpty)
                    {
                        /*
                        if (weapon0.GetWeaponData().GetItemObject().ItemType == ItemTypeEnum.Thrown)
                        {
                            int ammoCount = 0;
                            float tmp2 = 0.0f;
                            short newAmmoCount = 0;
                            meq.GetAmmoCountAndIndexOfType(ItemObject.ItemTypeEnum.Thrown, out ammoCount, out eIndex, EquipmentIndex.Weapon0);
                            tmp2 = ammoCount * KaosProjectilesSettings.Instance.ThrownMultiplier;
                            newAmmoCount = (short)tmp2;
                            if (newAmmoCount != weapon0.CurrentUsageItem.MaxDataValue)
                            {
                                newAmmoCount = weapon0.CurrentUsageItem.MaxDataValue;
                            }
                            if (!itemsList.Contains(weapon0.GetWeaponData().GetItemObject().Id))
                            {
                                foreach (WeaponComponentData wcd in weapon0.Weapons)
                                {
                                    if (wcd != null)
                                    {
                                        if (wcd.IsRangedWeapon)
                                        {
                                            typeof(WeaponComponentData).GetProperty("MaxDataValue").SetValue(wcd, newAmmoCount);
                                        }
                                    }
                                }
                                weapon0.SetAmmo(weapon0);
                                itemsList.Add(weapon0.GetWeaponData().GetItemObject().Id);
                            }
                        }
                        */
                    }

                    if (!weapon1.IsEmpty)
                    {
                        /*
                        if (weapon1.GetWeaponData().GetItemObject().ItemType == ItemTypeEnum.Thrown)
                        {
                            int ammoCount1 = 0;
                            float tmp2 = 0.0f;
                            short newAmmoCount1 = 0;
                            meq.GetAmmoCountAndIndexOfType(ItemObject.ItemTypeEnum.Thrown, out ammoCount1, out eIndex, EquipmentIndex.Weapon1);
                            tmp2 = ammoCount1 * KaosProjectilesSettings.Instance.ThrownMultiplier;
                            newAmmoCount1 = (short)tmp2;
                            if (newAmmoCount1 != weapon1.CurrentUsageItem.MaxDataValue)
                            {
                                newAmmoCount1 = weapon1.CurrentUsageItem.MaxDataValue;
                            }
                            if (!itemsList.Contains(weapon1.GetWeaponData().GetItemObject().Id))
                            {
                                foreach (WeaponComponentData wcd in weapon1.Weapons)
                                {
                                    if (wcd != null)
                                    {
                                        if (wcd.IsRangedWeapon)
                                        {
                                            typeof(WeaponComponentData).GetProperty("MaxDataValue").SetValue(wcd, newAmmoCount1);
                                        }
                                    }
                                }
                                weapon1.SetAmmo(weapon1);
                                itemsList.Add(weapon1.GetWeaponData().GetItemObject().Id);
                            }
                        }
                        */
                    }

                    if (!weapon2.IsEmpty)
                    {
                        /*
                        if (weapon2.GetWeaponData().GetItemObject().ItemType == ItemTypeEnum.Thrown)
                        {
                            int ammoCount2 = 0;
                            float tmp2 = 0.0f;
                            short newAmmoCount2 = 0;
                            meq.GetAmmoCountAndIndexOfType(ItemObject.ItemTypeEnum.Thrown, out ammoCount2, out eIndex, EquipmentIndex.Weapon2);
                            tmp2 = ammoCount2 * KaosProjectilesSettings.Instance.ThrownMultiplier;
                            newAmmoCount2 = (short)tmp2;
                            if (newAmmoCount2 != weapon2.CurrentUsageItem.MaxDataValue)
                            {
                                newAmmoCount2 = weapon2.CurrentUsageItem.MaxDataValue;
                            }
                            if (!itemsList.Contains(weapon2.GetWeaponData().GetItemObject().Id))
                            {
                                foreach (WeaponComponentData wcd in weapon2.Weapons)
                                {
                                    if (wcd != null)
                                    {
                                        if (wcd.IsRangedWeapon)
                                        {
                                            typeof(WeaponComponentData).GetProperty("MaxDataValue").SetValue(wcd, newAmmoCount2);
                                        }
                                    }
                                }
                                weapon2.SetAmmo(weapon2);
                                itemsList.Add(weapon2.GetWeaponData().GetItemObject().Id);
                            }
                        }
                        */
                    }


                    if (!weapon3.IsEmpty)
                    {
                        /*
                        if (weapon3.GetWeaponData().GetItemObject().ItemType == ItemTypeEnum.Thrown)
                        {
                            int ammoCount3 = 0;
                            float tmp2 = 0.0f;
                            short newAmmoCount3 = 0;
                            meq.GetAmmoCountAndIndexOfType(ItemObject.ItemTypeEnum.Thrown, out ammoCount3, out eIndex, EquipmentIndex.Weapon3);
                            tmp2 = ammoCount3 * KaosProjectilesSettings.Instance.ThrownMultiplier;
                            //tmp2 = tmp2 / 2;
                            newAmmoCount3 = (short)tmp2;
                            if (newAmmoCount3 != weapon3.CurrentUsageItem.MaxDataValue)
                            {
                                newAmmoCount3 = weapon3.CurrentUsageItem.MaxDataValue;
                            }
                            if (!itemsList.Contains(weapon3.GetWeaponData().GetItemObject().Id))
                            {
                                foreach (WeaponComponentData wcd in weapon3.Weapons)
                                {
                                    if (wcd != null)
                                    {
                                        if (wcd.IsRangedWeapon)
                                        {
                                            typeof(WeaponComponentData).GetProperty("MaxDataValue").SetValue(wcd, newAmmoCount3);
                                        }
                                    }
                                }
                                weapon3.SetAmmo(weapon3);
                                itemsList.Add(weapon3.GetWeaponData().GetItemObject().Id);
                            }
                        }
                        */
                    }
                }
            }
        }
        public void OnEndMission()
        {
        }

        public void OnMissionModeChange(MissionMode oldMissionMode, bool atStart)
        {
        }

        public void OnConversationCharacterChanged()
        {
        }

        public void OnResetMission()
        {
        }

        public short GetNewAmmoSize(int ammo)
        {
            float tmp = ammo * Statics._settings.ThrownMultiplier;
            short ammoCount2 = (short)tmp;
            return ammoCount2;
        }

        public void OnDeploymentPlanMade(BattleSideEnum battleSide, bool isFirstPlan)
        {
        }
    }
}
