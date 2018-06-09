using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAppointment
{
    public class AppointmentService : BaseService
    {



        public Appointment CreateAppointment(AppointmentRequest request)
        {
            if (request.AppointmentDate.Date <= DateTime.Today)
                throw new Exception("Cannot make an appointment before today");

            int maxDays = Convert.ToInt32(Config.GetValueWithDefault("MaxDays", "30"));
            if (request.AppointmentDate.Date > DateTime.Today.AddDays(maxDays))
                throw new Exception($"Cannot make an appointment more than {maxDays} in the future");


            var confirmationCode = ConfirmationCodeGenerator.NewConfirmationCode();

            AppointmentDao dao = new AppointmentDao();
            Appointment appt = new Appointment()
            {
                ConfirmationCode = confirmationCode,
                CustomerName = request.CustomerName,
                CarDescription = request.CarDescription,
                AppointmentDate = request.AppointmentDate,
                Hours = request.Hours,
                ServiceDescription = request.ServiceDescription,
                CreateTime = DateTime.Now,
                CreateUser = this.CurrentUserName
            };
            dao.InsertAppointment(appt);

            return appt;
        }

    }
}
