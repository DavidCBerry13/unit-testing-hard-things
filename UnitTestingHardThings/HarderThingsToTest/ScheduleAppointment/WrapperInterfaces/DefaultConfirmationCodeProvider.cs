using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAppointment
{
    public class DefaultConfirmationCodeProvider : IConfirmationCodeProvider
    {


        public string NewConfirmationCode()
        {
            return ConfirmationCodeGenerator.NewConfirmationCode();
        }
    }
}
