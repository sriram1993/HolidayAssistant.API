using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayAssistant.Flights.Model
{
    public class PriceDetails
    {
        public int NoOfPersons { get; set; }

        public double PerTicketPrice { get; set; }

        public double TotalTicketPrice { get; set; }
    }
}
