using HolidayAssistant.Flights.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HolidayAssistant.Login.Model.Enum;

namespace HolidayAssistant.Flights.Service
{
    public interface IFlightService
    {
        Task<SaveStatus> Save(FlightsIDTO flights);

        Task<List<FlightsIDTO>> GetUserFlightDetails(int customerID);

        Task<SaveStatus> CancelFlight(int transactionID);
    }
}
