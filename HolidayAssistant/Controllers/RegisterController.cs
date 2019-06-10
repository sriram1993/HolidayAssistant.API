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
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerRepo;

        public RegisterController(IRegisterService loginRepo)
        {
            _registerRepo = loginRepo;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterIDTO registerInput)
        {
            SaveStatus status = await _registerRepo.RegisterUser(registerInput);

            switch (status)
            {
                case SaveStatus.Success:
                    return Ok();

                case SaveStatus.AlreadyExists:
                    return Conflict();

                case SaveStatus.Failure:
                    return StatusCode(500);

                default:
                    return StatusCode(500);

            }
        }
    }
}