using HolidayAssistant.Hotels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HolidayAssistant.Hotels.Model.Enum;

namespace HolidayAssistant.Hotels.Service
{
    public interface IHotelRepository
    {
        Task<SaveStatus> BookHotel(Hotel hotels);

        Task<SaveStatus> CancelHotel(int hotelTransactionID);

        Task<List<Hotel>> GetUserHotelBooking(int userID);
    }
}
