﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayAssistant.Login.Model
{
    public class LoginODTO
    {
        public int UserID { get; set; }

        public string Email { get; set; }

        public Guid Token { get; set; }
    }
}
