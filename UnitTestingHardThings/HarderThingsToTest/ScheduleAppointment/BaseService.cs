using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ScheduleAppointment
{
    public class BaseService
    {




        public String CurrentUserName
        {
            get { return Thread.CurrentPrincipal.Identity.Name;  }
        }


    }
}
