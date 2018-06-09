using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAppointment
{
    public interface IUserProvider
    {

        String CurrentUsername { get; }

    }
}
