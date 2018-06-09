using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScheduleAppointment
{
    public static class Config
    {

        public static void LoadConfig(String file)
        {
            String[] lines = File.ReadAllLines(file);

            foreach (var line in lines)
            {
                if (!line.StartsWith("#"))
                {
                    int index = line.IndexOf('=');

                    if ( index != -1)
                    {
                        String key = line.Substring(0, index);
                        String value = line.Substring(index+1);

                        configValues.Add(key, value);
                    }
                }
            }

        }



        private static Dictionary<String, String> configValues;


        public static String GetValue(String key)
        {
            if (configValues.ContainsKey(key))
                return configValues[key];

            return null;
        }


        public static String GetValueWithDefault(String key, String defaultValue)
        {
            if (configValues.ContainsKey(key))
                return configValues[key];

            return defaultValue;
        }



    }
}
