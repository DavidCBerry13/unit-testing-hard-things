using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class CustomerPhone
    {

        public int CustomerPhoneId { get; set; }

        public int CustomerId { get; set; }

        public PhoneType PhoneType { get; set; }

        public String PhoneNumber { get; set; }

    }
}
