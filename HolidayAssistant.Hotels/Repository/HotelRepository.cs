using Dapper;
using HolidayAssistant.Hotels.Model;
using HolidayAssistant.Hotels.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static HolidayAssistant.Hotels.Model.Enum;

namespace HolidayAssistant.Hotels.Repository
{
    public class HotelRepository : IHotelRepository
    {
        public readonly IConfiguration _config;

        public HotelRepository(IConfiguration config)
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
        /// 
        /// </summary>
        /// <param name="hotels"></param>
        /// <returns></returns>
        public async Task<SaveStatus> BookHotel(Hotel hotels)
        {
            try
            {
                DataTable dtGuestDetails = new DataTable();
                dtGuestDetails.Columns.Add("FirstName", typeof(string));
                dtGuestDetails.Columns.Add("LastName", typeof(string));
                dtGuestDetails.Columns.Add("Age", typeof(string));
                dtGuestDetails.Columns.Add("Nationality", typeof(string));


                foreach (var hotelDet in hotels.GuestDetails)
                {
                    dtGuestDetails.Rows.Add(hotelDet.FirstName, hotelDet.LastName, hotelDet.Age, hotelDet.Nationality);
                }

                using (IDbConnection conn = Connection)
                {

                    var param = new DynamicParameters();
                    param.Add("@UserID", hotels.UserID, DbType.Int32, direction: ParameterDirection.Input);
                    param.Add("@Email", hotels.Email, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@MobileNo", hotels.MobileNo, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@BookingStatus", hotels.BookingStatus, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@NoOfGuests", hotels.NoOfGuests, DbType.Int32, direction: ParameterDirection.Input);
                    param.Add("@HotelName", hotels.HotelName, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@CityName", hotels.CityName, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Description", hotels.Description, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@BasePrice", hotels.BasePrice,DbType.String,direction: ParameterDirection.Input);
                    param.Add("@TotalPrice", hotels.TotalPrice, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@AddressLine", hotels.Address.AddressLine, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@PostalCode", hotels.Address.PostalCode, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@CountryCode", hotels.Address.CountryCode, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@RoomCategory", hotels.Category.RoomCategory, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Beds", hotels.Category.Beds, DbType.Int32, direction: ParameterDirection.Input);
                    param.Add("@BedType", hotels.Category.BedType, DbType.String, direction: ParameterDirection.Input);
                    param.Add("@GuestDetails", dtGuestDetails.AsTableValuedParameter("dbo.GuestDetails"));
                    param.Add("@ReturnValue", DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await conn.ExecuteAsync("usp_BookHotel", param, commandType: CommandType.StoredProcedure);

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

        public async Task<SaveStatus> CancelHotel(int hotelTransactionID)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var param = new DynamicParameters();
                    param.Add("@hotelTransactionID", hotelTransactionID, DbType.Int32, direction: ParameterDirection.Input);
                    param.Add("@ReturnValue", DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await conn.ExecuteAsync("usp_CancelHotel", param, commandType: CommandType.StoredProcedure);

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
        /// Get the user's hotel booking details
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<Hotel>> GetUserHotelBooking(int userID)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var param = new DynamicParameters();
                    param.Add("@UserID", userID, DbType.Int32, direction: ParameterDirection.Input);
                    Dictionary<int, Hotel> hotelInfoDict = new Dictionary<int, Hotel>();

                    var results = await conn.QueryAsync<Hotel, Address, Category,GuestDetails, Hotel>(
                        "usp_GetUserHotelDetails",
                        (hotel, address, category,guestDetails) =>
                        {
                            Hotel hotelsDto;

                            if (!hotelInfoDict.TryGetValue(hotel.HotelTransactionID, out hotelsDto))
                            {
                                hotelsDto = hotel;
                                hotelsDto.GuestDetails = new List<GuestDetails>();
                                hotelsDto.Address = new Address();
                                hotelsDto.Category = new Category();
                                hotelInfoDict.Add(hotel.HotelTransactionID, hotelsDto);
                            }

                            hotelsDto.Address = address;
                            hotelsDto.Category = category;
                            hotelsDto.GuestDetails.Add(guestDetails);
                            return hotelsDto;
                        },
                        param,
                         commandType: CommandType.StoredProcedure,
                        splitOn: "AddressLine,RoomCategory,GuestDetailsID"
                        );

                    List<Hotel> resultObj = hotelInfoDict?.Values.ToList();
                    return resultObj;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
