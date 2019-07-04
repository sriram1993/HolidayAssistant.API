using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayAssistant.Hotels.Model
{
    public class Hotel
    {
        public int HotelTransactionID { get; set; }
        
        public int UserID { get; set; }

        public string HotelName { get; set; }

        public string CityName { get; set; }

        public string MobileNo { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public string BasePrice { get; set; }

        public string TotalPrice { get; set; }

        public int NoOfGuests { get; set; }

        public string BookingStatus { get; set; }

        public Address Address { get; set; }

        public Category Category { get; set; }

        public List<GuestDetails> GuestDetails { get; set; }
    }
}
