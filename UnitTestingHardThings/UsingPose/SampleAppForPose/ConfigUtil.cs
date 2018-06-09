using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAppForPose
{
    public static class ConfigUtil
    {

        public static String GetAppSettingWithDefault(String key, String defaultValue)
        {
            var configValue = ConfigurationManager.AppSettings[key];

            return (!String.IsNullOrEmpty(configValue)) ? configValue : defaultValue;
        }




    }
}
