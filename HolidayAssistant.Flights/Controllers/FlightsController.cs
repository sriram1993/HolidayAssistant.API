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
        public async Task<IActionResult> Save([FromBody]FlightsIDTO flights)
        {
            SaveStatus status = await _flightRepo.Save(flights);
            return Ok();
        }

        [HttpGet]
        [Route("api/getUserFlightDetails/{customerID}")]
        public async Task<ActionResult<List<FlightsIDTO>>> GetUserFlightDetails(int customerID)
        {
            List<FlightsIDTO> flightDetailsList = await _flightRepo.GetUserFlightDetails(customerID);

            if (flightDetailsList.Count != 0)
                return Ok(flightDetailsList);
            else if (flightDetailsList.Count == 0)
                return NoContent();
            else
                return StatusCode(500);
        }

        [HttpPut]
        [Route("api/cancelTicket/{transactionID}")]
        public async Task<ActionResult> CancelFlight(int transactionID)
        {
            SaveStatus status = await _flightRepo.CancelFlight(transactionID);
            if (status == SaveStatus.Success)
                return Ok();
            else
                return StatusCode(500);
        }
    }
}