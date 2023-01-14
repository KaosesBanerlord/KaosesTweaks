using KaosesCommon.Utils;
using KaosesTweaks.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects
{

    public class ItemModifiersBase
    {

        public ItemObject _item;
        public Config _settings;


        public ItemModifiersBase(ItemObject itemObject)
        {
            _item = itemObject;
            _settings = Factory.Settings;
        }

        protected void SetItemsValue(int multiplePriceValue, float multiplier = 0.0f, bool typeDebug = false)
        {
            if (typeDebug)
            {
                DebugValue(_item, multiplier);
            }
            typeof(ItemObject).GetProperty("Value").SetValue(_item, multiplePriceValue);
        }

        protected void SetItemsWeight(float multipleWeightValue, float multiplier = 0.0f, bool typeDebug = false)
        {
            if (typeDebug)
            {
                DebugWeight(_item, multiplier);
            }
            typeof(ItemObject).GetProperty("Weight").SetValue(_item, multipleWeightValue);

        }
        protected void SetItemsStack(float multiplier = 0.0f, bool typeDebug = false)
        {
            WeaponComponentData weaponData = _item.PrimaryWeapon;
            float tmpMax = weaponData.MaxDataValue * multiplier;
            short newMax = (short)tmpMax;
            if (typeDebug)
            {
                DebugStack(_item, newMax, multiplier);
            }
            typeof(WeaponComponentData).GetProperty("MaxDataValue").SetValue(weaponData, newMax);
        }


        protected void DebugValue(ItemObject item, float multiplier)
        {
            if (_settings.ItemDebugMode)
            {
                IM.MessageDebug("ID: " + item.Name.ToString() + "  Old Value: " + _item.Value + "  New Value: " + _item.Value * multiplier + " using multiplier: " + multiplier);
                //IM.MessageDebug(item.Name.ToString() + " Old Price: " + item.Value.ToString() + "  New Price: " + newValue.ToString() + " using multiplier: " + multiplier);
            }
        }

        protected void DebugWeight(ItemObject item, float multiplier)
        {
            if (_settings.ItemDebugMode)
            {
                IM.MessageDebug("ID: " + item.Name.ToString() + "  Old Weight: " + _item.Value + "  New Weight: " + _item.Value * multiplier + " using multiplier: " + multiplier);
                //IM.MessageDebug(item.Name.ToString() + " Old Weight: " + item.Weight.ToString() + "  New Weight: " + newValue.ToString() + " using multiplier: " + multiplier);
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
