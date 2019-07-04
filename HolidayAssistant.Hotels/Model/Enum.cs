using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayAssistant.Hotels.Model
{
    public class Enum
    {
        public enum SaveStatus
        {
            Success = 0,
            Failure = -1,
            AlreadyExists = -2
        };

        public enum HotelBookingStatus
        {
            Booked = 1,
            Cancelled = 2
        }
    }
}
