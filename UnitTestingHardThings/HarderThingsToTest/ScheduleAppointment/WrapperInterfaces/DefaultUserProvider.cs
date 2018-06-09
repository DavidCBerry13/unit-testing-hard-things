using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ScheduleAppointment
{
    public class DefaultUserProvider : IUserProvider
    {
        public string CurrentUsername => Thread.CurrentPrincipal.Identity.Name;
    }
}
