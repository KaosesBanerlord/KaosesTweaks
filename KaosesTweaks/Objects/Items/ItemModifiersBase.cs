using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects
{
    public class ItemModifiersBase
    {

        public ItemObject _item;
        public MCMSettings _settings;


        public ItemModifiersBase(ItemObject itemObject)
        {
            _item = itemObject;
            _settings = Statics._settings;
        }

        protected void SetItemsValue(int multiplePriceValue, float multiplier = 0.0f)
        {
            DebugValue(_item, multiplePriceValue, multiplier);
            typeof(ItemObject).GetProperty("Value").SetValue(_item, multiplePriceValue);
        }

        protected void SetItemsWeight(float multipleWeightValue, float multiplier = 0.0f)
        {
            DebugWeight(_item, multipleWeightValue, multiplier);
            typeof(ItemObject).GetProperty("Weight").SetValue(_item, multipleWeightValue);

        }
        protected void SetItemsStack(float multiplier = 0.0f)
        {
            WeaponComponentData weaponData = _item.PrimaryWeapon;
            float tmpMax = weaponData.MaxDataValue * multiplier;
            short newMax = (short)tmpMax;
            DebugStack(_item, newMax, multiplier);
            typeof(WeaponComponentData).GetProperty("MaxDataValue").SetValue(weaponData, newMax);
        }


        protected void DebugValue(ItemObject item, float newValue, float multiplier)
        {
            if (_settings.ItemDebugMode)
            {
                IM.MessageDebug(item.Name.ToString() + " Old Price: " + item.Value.ToString() + "  New Price: " + newValue.ToString() + " using multiplier: " + multiplier);
            }
        }

        protected void DebugWeight(ItemObject item, float newValue, float multiplier)
        {
            if (_settings.ItemDebugMode)
            {
                IM.MessageDebug(item.Name.ToString() + " Old Weight: " + item.Weight.ToString() + "  New Weight: " + newValue.ToString() + " using multiplier: " + multiplier);
            }
        }

        protected void DebugStack(ItemObject item, float newValue, float multiplier)
        {
            if (_settings.ItemDebugMode)
            {
                WeaponComponentData weaponData = _item.PrimaryWeapon;
                float tmpMax = weaponData.MaxDataValue * multiplier;
                short newMax = (short)tmpMax;
                IM.MessageDebug(item.Name.ToString() + " Old Stack: " + weaponData.MaxDataValue.ToString() + "  New Stack: " + newValue.ToString() + " using multiplier: " + multiplier);
            }
        }

    }


}
