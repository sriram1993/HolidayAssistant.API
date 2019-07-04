using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HolidayAssistant.Hotels.Model;
using HolidayAssistant.Hotels.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HolidayAssistant.Hotels.Model.Enum;

namespace HolidayAssistant.Hotels.Controllers
{
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepo;

        public HotelsController(IHotelRepository hotelRepo)
        {
            _hotelRepo = hotelRepo;
        }

        [HttpPost]
        [Route("api/bookHotel")]
        public async Task<IActionResult> Save([FromBody]Hotel hotel)
        {
            SaveStatus status = await _hotelRepo.BookHotel(hotel);
            if (status == SaveStatus.Success)
                return Ok();
            else
                return StatusCode(500);
        }

        [HttpGet]
        [Route("api/getUserHotelDetails/{customerID}")]
        public async Task<ActionResult<List<Hotel>>> GetUserHotelDetails(int customerID)
        {
            List<Hotel> hotelDetailsList = await _hotelRepo.GetUserHotelBooking(customerID);

            if (hotelDetailsList.Count != 0)
                return Ok(hotelDetailsList);
            else if (hotelDetailsList.Count == 0)
                return NoContent();
            else
                return StatusCode(500);
        }

        [HttpPut]
        [Route("api/cancelHotel/{transactionID}")]
        public async Task<IActionResult> CancelHotel(int transactionID)
        {
            SaveStatus status = await _hotelRepo.CancelHotel(transactionID);
            if (status == SaveStatus.Success)
                return Ok();
            else
                return StatusCode(500);
        }
    }
}