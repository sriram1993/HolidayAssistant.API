using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HolidayAssistant.Login.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<int>> Login()
        {
            await Task.Delay(1000);
            return Ok();
        }
    }
}