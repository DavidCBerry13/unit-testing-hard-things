using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAppointment
{
    public class AppointmentDao : IAppointmentDao
    {


        private static int appointmentNumber = 1;


        public void InsertAppointment(Appointment appt)
        {
            appt.AppointmentId = appointmentNumber++;
        }

        public Appointment LoadAppointment(int appointmentId)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> LoadAppointments()
        {
            throw new NotImplementedException();
        }

        public List<Appointment> LoadAppointmentsForDay(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void UpdateAppointment(Appointment appt)
        {
            throw new NotImplementedException();
        }
    }
}
