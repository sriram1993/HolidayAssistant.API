using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HolidayAssistant.Flights.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [Route("flights")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "KLM", "Jet Airways" };
        }

        // GET api/values/5
        [HttpGet("flights/{id}")]
        public ActionResult<string> Get(int id)
        {
            return "Flight #5";
        }

    }
}
