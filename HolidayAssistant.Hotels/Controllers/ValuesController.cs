using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HolidayAssistant.Hotels.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [Route("hotels")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Royal Paradise", "Vivanta" };
        }

    }
}
