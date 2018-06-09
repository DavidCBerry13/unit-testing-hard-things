using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleAppointment
{
    public interface IDateTimeProvider
    {


        DateTime Now { get; }

        DateTime UtcNow { get; }

        DateTime Today { get;  }


    }
}
