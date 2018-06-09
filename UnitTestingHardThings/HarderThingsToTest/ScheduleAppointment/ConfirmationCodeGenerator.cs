using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAppointment
{
    public static class ConfirmationCodeGenerator
    {

        public static String NewConfirmationCode()
        {
            Random r = new Random();
            var value = r.Next(10000, 10000000);

            return value.ToString("X8");
        }



    }
}
