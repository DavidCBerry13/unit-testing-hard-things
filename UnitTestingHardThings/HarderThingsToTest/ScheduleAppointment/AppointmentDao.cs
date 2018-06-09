using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAppointment
{
    public class AppointmentDao
    {


        private static int appointmentNumber = 1;


        public void InsertAppointment(Appointment appt)
        {
            appt.AppointmentId = appointmentNumber++;
        }

    }
}
