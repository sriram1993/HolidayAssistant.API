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
            LoginStatus status = await _loginRepo.Login(login);

            switch (status)
            {
                case LoginStatus.Success:
                    return Ok("Login Successful");

                case LoginStatus.InvalidCredentials:
                    return Ok("Invalid Email/Password Combination!!");

                default:
                    return StatusCode(500, "Server Error");

            }
        }
    }
}