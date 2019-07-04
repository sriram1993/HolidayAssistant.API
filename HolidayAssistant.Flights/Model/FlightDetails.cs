using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayAssistant.Flights.Model
{
    public class FlightDetails
    {
        public int FlightDetailsID { get; set; }

        public string ArrivalCity { get; set; }

        public string ArrivalTime { get; set; }

        public string DepartureCity { get; set; }

        public string DepartureTime { get; set; }

        public string Duration { get; set; }

        public string AircraftCode { get; set; }
    }
}
