using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAppointment
{
    public class Appointment
    {

        public int AppointmentId { get; set; }

        public String ConfirmationCode { get; set; }

        public String CustomerName { get; set; }

        public String CarDescription { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int Hours { get; set; }

        public String ServiceDescription { get; set; }

        public DateTime CreateTime { get; set; }

        public String CreateUser { get; set; }

    }
}
