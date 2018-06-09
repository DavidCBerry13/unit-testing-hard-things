using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAppointment
{
    public class DefaultConfigProvider : IConfigProvider
    {


        public string GetValue(string key)
        {
            return Config.GetValue(key);
        }

        public string GetValueWithDefault(string key, string defaultValue)
        {
            return Config.GetValueWithDefault(key, defaultValue);
        }
    }
}
