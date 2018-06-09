using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAppointment
{
    public class AppointmentRequest
    {

        public String CustomerName { get; set; }

        public String CarDescription { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int Hours { get; set; }

        public String ServiceDescription { get; set; }

    }
}
