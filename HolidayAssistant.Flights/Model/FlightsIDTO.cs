using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayAssistant.Flights.Model
{
    public class FlightsIDTO
    {
        public int TransactionID { get; set; }

        public int UserID { get; set; }

        public string Email { get; set; }

        public string MobileNo { get; set; }

        public string TicketStatus { get; set; }

        public int NoOfPersons { get; set; }

        public double PerTicketPrice { get; set; }

        public double TotalTicketPrice { get; set; }

        public List<FlightDetails> FlightDetails { get; set; }

        public List<PassengerDetails> PassengerDetails { get; set; }

    }
}
