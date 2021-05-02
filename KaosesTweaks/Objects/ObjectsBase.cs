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
    public class ObjectsBase
    {

        public ItemObject _item;
        public ISettingsProviderInterface _settings;


        public ObjectsBase(ItemObject itemObject)
        {
            _item = itemObject;
            _settings = Statics._settings;
        }

        protected void DebugValue(ItemObject item, float newValue, float multiplier)
        {
            if (_settings.Debug)
            {
                //Ux.MessageDebug(item.Name.ToString() + " Old Price: " + item.Value.ToString() + "  New Price: " + newValue.ToString() + " using multiplier: " + multiplier);
            }
            //else if (_settings.LogToFile)
            //{
                //Logging.Lm(item.Name.ToString() + "  tier: " +item.Tier.ToString()+ " Old Price: " + item.Value.ToString() + "  New Price: " + newValue.ToString() + " using multiplier: " + multiplier);
            //}
        }
        protected void DebugWeight(ItemObject item, float newValue, float multiplier)
        {
            if (_settings.Debug)
            {
                //Ux.MessageDebug(item.Name.ToString() + " Old Weight: " + item.Weight.ToString() + "  New Weight: " + newValue.ToString() + " using multiplier: " + multiplier);
            }
            //else if (_settings.LogToFile)
            //{
                //Logging.Lm(item.Name.ToString() + "  tier: " + item.Tier.ToString() + " Old Weight: " + item.Weight.ToString() + "  New Weight: " + newValue.ToString() + " using multiplier: " + multiplier);
            //}
        }

    }


}
