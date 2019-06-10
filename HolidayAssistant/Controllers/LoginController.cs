using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HolidayAssistant.Login.Model;
using HolidayAssistant.Login.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HolidayAssistant.Login.Model.Enum;

namespace HolidayAssistant.Login.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginRepo;

        public LoginController(ILoginService loginRepo)
        {
            _loginRepo = loginRepo;
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login([FromBody]LoginIDTO login)
        {
            LoginODTO status = await _loginRepo.Login(login);

           if(status != null)
            {
                return Ok(status);
            }
            else
            {
                return NotFound();
            }
        }
    }
}