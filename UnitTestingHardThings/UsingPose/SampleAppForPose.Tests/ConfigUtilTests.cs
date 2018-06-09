using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pose;
using Xunit;
using System.Collections.Specialized;

namespace SampleAppForPose.Tests
{
    public class ConfigUtilTests
    {

        [Fact]
        public void WhenConfigFileDoesNotContainAppSetting_DefaultIsUsed()
        {
            // Arrange            
            Shim configShim = Shim.Replace(() => ConfigurationManager.AppSettings)
                .With(() => new NameValueCollection());

            PoseContext.Isolate(() =>
            {
                // Act
                var actual = ConfigUtil.GetAppSettingWithDefault("SomeKey", "DefaultValue");

                // Assert
                Assert.Equal("DefaultValue", actual);
            }, configShim);
        }


        [Fact]
        public void WhenConfigFileContainAppSetting_FileValueIsUsed()
        {
            // Arrange            
            Shim configShim = Shim.Replace(() => ConfigurationManager.AppSettings)
                .With(() => 
                    {
                        var settings = new NameValueCollection();
                        settings.Add("SomeKey", "FileValue");
                        return settings;
                    }
                
                );

            PoseContext.Isolate(() =>
            {
                // Act
                var actual = ConfigUtil.GetAppSettingWithDefault("SomeKey", "DefaultValue");

                // Assert
                Assert.Equal("FileValue", actual);
            }, configShim);

        }

    }
}
