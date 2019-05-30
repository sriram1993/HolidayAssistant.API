using Dapper;
using HolidayAssistant.Login.Model;
using HolidayAssistant.Login.Service;
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
        public async Task<LoginStatus> Login(LoginIDTO loginInput)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var param = new DynamicParameters();
                    param.Add("@Email", loginInput.Email, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Password", loginInput.Password, DbType.String, direction: ParameterDirection.Input);


                    await conn.ExecuteAsync("usp_LoginUser", param, commandType: CommandType.StoredProcedure);

                    var retValue = param.Get<int>("@ReturnValue");
                    LoginStatus status = (LoginStatus)retValue;
                    return status;

                }
            }
            catch (Exception e)
            {
                return LoginStatus.Failure;
            }
        }

    }
}
