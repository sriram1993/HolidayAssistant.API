using HolidayAssistant.Login.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HolidayAssistant.Login.Model.Enum;

namespace HolidayAssistant.Login.Service
{
    public interface ILoginService
    {
        Task<LoginStatus> Login(LoginIDTO login);
    }
}
