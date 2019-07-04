using Dapper;
using HolidayAssistant.Flights.Model;
using HolidayAssistant.Flights.Service;
using HolidayAssistant.Login.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static HolidayAssistant.Login.Model.Enum;

namespace HolidayAssistant.Flights.Repository
{
    public class FlightRepository : IFlightService
    {
        public readonly IConfiguration _config;

        public FlightRepository(IConfiguration config)
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

        public async Task<SaveStatus> CancelFlight(int transactionID)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var param = new DynamicParameters();
                    param.Add("@transactionID", transactionID, DbType.Int32, direction: ParameterDirection.Input);
                    param.Add("@ReturnValue", DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await conn.ExecuteAsync("usp_CancelTicket", param, commandType: CommandType.StoredProcedure);

                    var retValue = param.Get<int>("@ReturnValue");
                    SaveStatus status = (SaveStatus)retValue;
                    return status;

                }
            }
            catch (Exception e)
            {
                return SaveStatus.Failure;
            }
        }

        /// <summary>
        /// To get the Flight Details of the User
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public async Task<List<FlightsIDTO>> GetUserFlightDetails(int customerID)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var param = new DynamicParameters();
                    param.Add("@UserID", customerID, DbType.Int32, direction: ParameterDirection.Input);
                    Dictionary<int, FlightsIDTO> flightInfoDict = new Dictionary<int, FlightsIDTO>();

                    var results = await conn.QueryAsync<FlightsIDTO,FlightDetails,PassengerDetails,FlightsIDTO>(
                        "usp_GetUserFlightDetails",
                        (flight,flightDetails,passengerDetails) =>
                        {
                            FlightsIDTO flightsIdto;

                            if (!flightInfoDict.TryGetValue(flight.TransactionID, out flightsIdto))
                            {
                                flightsIdto = flight;
                                flightsIdto.FlightDetails = new List<FlightDetails>();
                                flightsIdto.PassengerDetails = new List<PassengerDetails>();
                                flightInfoDict.Add(flight.TransactionID, flightsIdto);
                            }

                        if (!flightsIdto.FlightDetails.Any(x => x.FlightDetailsID == flightDetails.FlightDetailsID))
                            flightsIdto.FlightDetails.Add(flightDetails);
                        if (!flightsIdto.PassengerDetails.Any(x => x.PassengerDetailsID == passengerDetails.PassengerDetailsID))
                                flightsIdto.PassengerDetails.Add(passengerDetails);
                            return flightsIdto;
                        },
                        param,
                         commandType: CommandType.StoredProcedure,
                        splitOn: "FlightDetailsID,PassengerDetailsID"
                        );

                    List<FlightsIDTO> resultObj = flightInfoDict?.Values.ToList();
                    return resultObj;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Book flight ticket
        /// </summary>
        /// <param name="flights"></param>
        /// <returns></returns>
        public async Task<SaveStatus> Save(FlightsIDTO flights)
        {
            try
            {
                DataTable dtFlightDetails = new DataTable();
                dtFlightDetails.Columns.Add("ArrivalCity", typeof(string));
                dtFlightDetails.Columns.Add("ArrivalTime", typeof(string));
                dtFlightDetails.Columns.Add("DepartureCity", typeof(string));
                dtFlightDetails.Columns.Add("DepartureTime", typeof(string));
                dtFlightDetails.Columns.Add("Duration", typeof(string));
                dtFlightDetails.Columns.Add("AircraftCode", typeof(string));

                DataTable dtPassengerDetails = new DataTable();
                dtPassengerDetails.Columns.Add("FirstName", typeof(string));
                dtPassengerDetails.Columns.Add("LastName", typeof(string));
                dtPassengerDetails.Columns.Add("Age", typeof(int));
                dtPassengerDetails.Columns.Add("Nationality", typeof(string));
                dtPassengerDetails.Columns.Add("PassportNo", typeof(string));

                foreach (var flightDet in flights.FlightDetails)
                {
                    dtFlightDetails.Rows.Add(flightDet.ArrivalCity, flightDet.ArrivalTime, flightDet.DepartureCity, flightDet.DepartureTime, flightDet.Duration, flightDet.AircraftCode);
                }

                foreach (var passengerDet in flights.PassengerDetails)
                {
                    dtPassengerDetails.Rows.Add(passengerDet.FirstName, passengerDet.LastName, passengerDet.Age, passengerDet.Nationality, passengerDet.PassportNo);
                }

                using (IDbConnection conn = Connection)
                {

                    var param = new DynamicParameters();
                    param.Add("@UserID", flights.UserID, DbType.Int32, direction: ParameterDirection.Input);
                    param.Add("@Email", flights.Email, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@MobileNo", flights.MobileNo, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Status", flights.TicketStatus, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@NoOfPersons", flights.NoOfPersons, DbType.Int32, direction: ParameterDirection.Input);
                    param.Add("@PerTicketPrice", flights.PerTicketPrice, DbType.Double, direction: ParameterDirection.Input);
                    param.Add("@TotalTicketPrice", flights.TotalTicketPrice, DbType.Double, direction: ParameterDirection.Input);
                    param.Add("@FlightDetails", dtFlightDetails.AsTableValuedParameter("dbo.FlightDetails"));
                    param.Add("@PassengerDetails", dtPassengerDetails.AsTableValuedParameter("dbo.PassengerDetails"));
                    param.Add("@ReturnValue", DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await conn.ExecuteAsync("usp_BookFlight", param, commandType: CommandType.StoredProcedure);

                    var retValue = param.Get<int>("@ReturnValue");
                    SaveStatus status = (SaveStatus)retValue;
                    return status;
                }
            }
            catch (Exception e)
            {
                return SaveStatus.Failure;
            }
        }
    }
}
