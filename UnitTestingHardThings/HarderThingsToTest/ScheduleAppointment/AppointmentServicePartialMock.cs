using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAppointment
{
    public class AppointmentServicePartialMock : BaseService
    {

        public AppointmentServicePartialMock()
            : this(new AppointmentDao() )
        {

        }


        public AppointmentServicePartialMock(IAppointmentDao dao)
        {
            _appointmentDao = dao;
        }

        private IAppointmentDao _appointmentDao;


        public Appointment CreateAppointment(AppointmentRequest request)
        {
            if (request.AppointmentDate.Date <= this.Today)
                throw new ArgumentException("Cannot make an appointment before today");

            int maxDays = this.MaxAppointmentDays;
            if (request.AppointmentDate.Date > this.Today.AddDays(maxDays))
                throw new ArgumentException($"Cannot make an appointment more than {maxDays} in the future");


            var confirmationCode = this.GetConfirmationCode();

            
            Appointment appt = new Appointment()
            {
                ConfirmationCode = confirmationCode,
                CustomerName = request.CustomerName,
                CarDescription = request.CarDescription,
                AppointmentDate = request.AppointmentDate,
                Hours = request.Hours,
                ServiceDescription = request.ServiceDescription,
                CreateTime = this.Now,
                CreateUser = this.CurrentUserName
            };
            _appointmentDao.InsertAppointment(appt);

            return appt;
        }


        public virtual DateTime Today
        {
            get { return DateTime.Today; }
        }


        public virtual DateTime Now
        {
            get { return DateTime.Now; }
        }

        // New keyword is used because this is a sealed memeber
        // This is an unsavory choice and in real life one would probably inject an
        // interface that wraps the call rather than using a partial mock
        public new virtual String CurrentUserName
        {
            get { return base.CurrentUserName; }
        }


        public virtual int MaxAppointmentDays
        {
            get { return Convert.ToInt32(Config.GetValueWithDefault("MaxDays", "30"));   }
        }


        public virtual String GetConfirmationCode()
        {
            return ConfirmationCodeGenerator.NewConfirmationCode();
        }

    }
}
