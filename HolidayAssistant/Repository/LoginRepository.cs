using Dapper;
using HolidayAssistant.Login.Model;
using HolidayAssistant.Login.Service;
using HolidayAssistant.Login.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static HolidayAssistant.Login.Model.Enum;

namespace HolidayAssistant.Login.Repository
{
    public class LoginRepository : ILoginService
    {

        public readonly IConfiguration _config;

        public LoginRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DBConnection"));
            }
        }

        /// <summary>
        /// Check the Login Credentials of the user
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        public async Task<LoginODTO> Login(LoginIDTO loginInput)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    //Replace with stored procedure
                    string sQuery = "SELECT UserID ,Email,NEWID() AS Token  FROM UserDetails WHERE Email = @Email AND Password=@Password";
                    conn.Open();
                    var result = await conn.QueryFirstOrDefaultAsync<LoginODTO>(sQuery, new { Email = loginInput.Email, Password = EncodeDecodeBase64.Base64Encode(loginInput.Password) });
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
