using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HolidayAssistant.Flights.Model;
using HolidayAssistant.Flights.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HolidayAssistant.Login.Model.Enum;

namespace HolidayAssistant.Flights.Controllers
{
    [ApiController]
    public class FlightsController : ControllerBase
    {

        private readonly IFlightService _flightRepo;

        public FlightsController(IFlightService flightRepo)
        {
            _flightRepo = flightRepo;
        }

        [HttpPost]
        [Route("api/bookFlight")]
        public async Task<SaveStatus> Save([FromBody]FlightsIDTO flights)
        {
            await Task.Delay(1000);
            return SaveStatus.Success;
        }

        [HttpGet]
        [Route("api/getUserFlightDetails")]
        public async Task<FlightsIDTO> GetUserFlightDetails(int customerID)
        {
            await Task.Delay(1000);
            return new FlightsIDTO();
        }

        [HttpPut]
        [Route("api/cancelTicket")]
        public async Task<SaveStatus> CancelFlight([FromBody]FlightsIDTO flights)
        {
            await Task.Delay(1000);
            return SaveStatus.Success;
        }
    }
}