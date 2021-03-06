﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAppointment
{
    public class AppointmentServiceWithWrapperInterface : BaseService
    {

        public AppointmentServiceWithWrapperInterface(IAppointmentDao dao, IDateTimeProvider dateTimeProvider, 
            IConfigProvider configProvider, IUserProvider userProvider, IConfirmationCodeProvider confirmationCodeProvider)
        {
            this._appointmentDao = dao;
            this._dateTimeProvider = dateTimeProvider;
            this._configProvider = configProvider;
            this._userProvider = userProvider;
            this._confirmCodeProvider = confirmationCodeProvider;
        }

        public AppointmentServiceWithWrapperInterface()
        {
            this._appointmentDao = new AppointmentDao();
            this._dateTimeProvider = new DefaultDateTimeProvider();
            this._configProvider = new DefaultConfigProvider();
            this._userProvider = new DefaultUserProvider();
            this._confirmCodeProvider = new DefaultConfirmationCodeProvider();
        }


        private IAppointmentDao _appointmentDao;
        private IDateTimeProvider _dateTimeProvider;
        private IConfigProvider _configProvider;
        private IUserProvider _userProvider;
        private IConfirmationCodeProvider _confirmCodeProvider;

        public Appointment CreateAppointment(AppointmentRequest request)
        {
            if (request.AppointmentDate.Date <= _dateTimeProvider.Today)
                throw new Exception("Cannot make an appointment before today");

            int maxDays = Convert.ToInt32(_configProvider.GetValueWithDefault("MaxDays", "30"));
            if (request.AppointmentDate.Date > _dateTimeProvider.Today.AddDays(maxDays))
                throw new Exception($"Cannot make an appointment more than {maxDays} in the future");


            var confirmationCode = this._confirmCodeProvider.NewConfirmationCode();

            Appointment appt = new Appointment()
            {
                ConfirmationCode = confirmationCode,
                CustomerName = request.CustomerName,
                CarDescription = request.CarDescription,
                AppointmentDate = request.AppointmentDate,
                Hours = request.Hours,
                ServiceDescription = request.ServiceDescription,
                CreateTime = _dateTimeProvider.Now,
                CreateUser = _userProvider.CurrentUsername
            };
            _appointmentDao.InsertAppointment(appt);

            return appt;
        }



        public virtual String GetConfirmationCode()
        {
            return ConfirmationCodeGenerator.NewConfirmationCode();
        }

    }
}
