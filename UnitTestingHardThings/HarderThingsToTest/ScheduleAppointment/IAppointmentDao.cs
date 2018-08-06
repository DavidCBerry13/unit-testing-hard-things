using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAppointment
{
    public interface IAppointmentDao
    {

        List<Appointment> LoadAppointments();

        List<Appointment> LoadAppointmentsForDay(DateTime date);

        Appointment LoadAppointment(int appointmentId);

        void InsertAppointment(Appointment appt);

        void UpdateAppointment(Appointment appt);


    }
}
