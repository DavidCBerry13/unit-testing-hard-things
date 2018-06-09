using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAppointment
{
    public interface IConfigProvider
    {

        String GetValue(String key);



        String GetValueWithDefault(String key, String defaultValue);


    }
}
