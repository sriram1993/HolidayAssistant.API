using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayAssistant.Flights.Model
{
    public class PassengerDetails
    {
        public int PassengerDetailsID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Nationality { get; set; }

        public string PassportNo { get; set; }
    }
}
