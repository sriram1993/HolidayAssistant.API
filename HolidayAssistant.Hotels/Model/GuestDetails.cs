using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayAssistant.Hotels.Model
{
    public class GuestDetails
    {
        public int GuestDetailsID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Nationality { get; set; }
    }
}
