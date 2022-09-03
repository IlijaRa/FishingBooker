using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;

namespace FishingBookerLibrary.BusinessLogic
{
    public static class ReservationCRUD
    {
        public static int CreateAdventureReservations(  string place,
                                                            DateTime startdate,
                                                            TimeSpan starttime,
                                                            DateTime enddate,
                                                            TimeSpan endtime,
                                                            DateTime validitydate,
                                                            TimeSpan validitytime,
                                                            int maxNum,
                                                            string additionalServices,
                                                            decimal price,
                                                            bool isReserved,
                                                            string clientsemailAddress,
                                                            Enums.ReservationType type,
                                                            int adventureId,
                                                            string instructorId)
        {
            AdventureReservation data = new AdventureReservation
            {
                CreationTime = DateTime.Now,
                Place = place,
                StartDate = startdate,
                StartTime = starttime,
                EndDate = enddate,
                EndTime = endtime,
                ValidityPeriodDate = validitydate,
                ValidityPeriodTime = validitytime,
                dayOfWeek = Convert.ToInt32(startdate.DayOfWeek),
                Month = startdate.Month,
                Year = startdate.Year,
                MaxNumberOfPeople = maxNum,
                AdditionalServices = additionalServices,
                Price = price,
                Discount = 0,
                IsReserved = isReserved,
                ClientsEmailAddress = clientsemailAddress,
                ReservationType = type,
                AdventureId = adventureId,
                InstructorId = instructorId
            };

            string sql = @"BEGIN TRANSACTION
                                INSERT INTO dbo.AdventureReservations (Place, CreationTime, StartDate, StartTime, EndDate, EndTime, ValidityPeriodDate, ValidityPeriodTime, dayOfWeek, Month, Year, MaxNumberOfPeople, AdditionalServices, Price, Discount, IsReserved, ClientsEmailAddress, ReservationType, AdventureId, InstructorId)
                                VALUES (@Place, @CreationTime, @StartDate, @StartTime, @EndDate, @EndTime, @ValidityPeriodDate, @ValidityPeriodTime, @dayOfWeek, @Month, @Year, @MaxNumberOfPeople, @AdditionalServices, @Price, @Discount, @IsReserved, @ClientsEmailAddress, @ReservationType, @AdventureId, @InstructorId);
                           COMMIT";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int CreateAdventureReservationsSerializable(string place,
                                                            DateTime startdate,
                                                            TimeSpan starttime,
                                                            DateTime enddate,
                                                            TimeSpan endtime,
                                                            DateTime validitydate,
                                                            TimeSpan validitytime,
                                                            int maxNum,
                                                            string additionalServices,
                                                            decimal price,
                                                            bool isReserved,
                                                            string clientsemailAddress,
                                                            Enums.ReservationType type,
                                                            int adventureId,
                                                            string instructorId)
        {
            try
            {
                AdventureReservation data = new AdventureReservation
                {
                    CreationTime = DateTime.Now,
                    Place = place,
                    StartDate = startdate,
                    StartTime = starttime,
                    EndDate = enddate,
                    EndTime = endtime,
                    ValidityPeriodDate = validitydate,
                    ValidityPeriodTime = validitytime,
                    dayOfWeek = Convert.ToInt32(startdate.DayOfWeek),
                    Month = startdate.Month,
                    Year = startdate.Year,
                    MaxNumberOfPeople = maxNum,
                    AdditionalServices = additionalServices,
                    Price = price,
                    Discount = 0,
                    IsReserved = isReserved,
                    ClientsEmailAddress = clientsemailAddress,
                    ReservationType = type,
                    AdventureId = adventureId,
                    InstructorId = instructorId
                };
                string sql = @" SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                                BEGIN TRANSACTION
                                SELECT * FROM dbo.AdventureReservations
                                WAITFOR DELAY '00:00:10'
                                    INSERT INTO dbo.AdventureReservations WITH(TABLOCKX) (Place, CreationTime, StartDate, StartTime, EndDate, EndTime, ValidityPeriodDate, ValidityPeriodTime, dayOfWeek, Month, Year, MaxNumberOfPeople, AdditionalServices, Price, Discount, IsReserved, ClientsEmailAddress, ReservationType, AdventureId, InstructorId)
		                            VALUES (@Place, @CreationTime, @StartDate, @StartTime, @EndDate, @EndTime, @ValidityPeriodDate, @ValidityPeriodTime, @dayOfWeek, @Month, @Year, @MaxNumberOfPeople, @AdditionalServices, @Price, @Discount, @IsReserved, @ClientsEmailAddress, @ReservationType, @AdventureId, @InstructorId);
                                COMMIT TRANSACTION";

                return SSMSDataAccess.SaveData(sql, data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int CreateCottageReservations(string cottageName,
                                                            DateTime startdate,
                                                            TimeSpan starttime,
                                                            DateTime enddate,
                                                            TimeSpan endtime,
                                                            DateTime validitydate,
                                                            TimeSpan validitytime,
                                                            int maxNum,
                                                            string additionalServices,
                                                            decimal price,
                                                            bool isReserved,
                                                            string clientsemailAddress,
                                                            Enums.ReservationType type,
                                                            int cottageId,
                                                            string ownerId)
        {
            CottageReservation data = new CottageReservation
            {
                CottageName = cottageName,
                StartDate = startdate,
                StartTime = starttime,
                EndDate = enddate,
                EndTime = endtime,
                ValidityPeriodDate = validitydate,
                ValidityPeriodTime = validitytime,
                MaxNumberOfPeople = maxNum,
                AdditionalServices = additionalServices,
                Price = price,
                Discount = 0,
                IsReserved = isReserved,
                ClientsEmailAddress = clientsemailAddress,
                ReservationType = type,
                CottageId = cottageId,
                OwnerId = ownerId
            };

            string sql = @"INSERT INTO dbo.CottageReservations (CottageName, StartDate, StartTime, EndDate, EndTime, ValidityPeriodDate, ValidityPeriodTime, MaxNumberOfPeople, AdditionalServices, Price, Discount, IsReserved, ClientsEmailAddress, ReservationType, CottageId, OwnerId)
                           VALUES (@CottageName, @StartDate, @StartTime, @EndDate, @EndTime, @ValidityPeriodDate, @ValidityPeriodTime, @MaxNumberOfPeople, @AdditionalServices, @Price, @Discount, @IsReserved, @ClientsEmailAddress, @ReservationType, @CottageId, @OwnerId);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int CreateShipReservations(string shipName,
                                                            DateTime startdate,
                                                            TimeSpan starttime,
                                                            DateTime enddate,
                                                            TimeSpan endtime,
                                                            DateTime validitydate,
                                                            TimeSpan validitytime,
                                                            int maxNum,
                                                            string additionalServices,
                                                            decimal price,
                                                            bool isReserved,
                                                            string clientsemailAddress,
                                                            Enums.ReservationType type,
                                                            int shipId,
                                                            string ownerId)
        {
            ShipReservation data = new ShipReservation
            {
                ShipName = shipName,
                StartDate = startdate,
                StartTime = starttime,
                EndDate = enddate,
                EndTime = endtime,
                ValidityPeriodDate = validitydate,
                ValidityPeriodTime = validitytime,
                MaxNumberOfPeople = maxNum,
                AdditionalServices = additionalServices,
                Price = price,
                Discount = 0,
                IsReserved = isReserved,
                ClientsEmailAddress = clientsemailAddress,
                ReservationType = type,
                ShipId = shipId,
                OwnerId = ownerId
            };

            string sql = @"INSERT INTO dbo.ShipReservations (ShipName, StartDate, StartTime, EndDate, EndTime, ValidityPeriodDate, ValidityPeriodTime, MaxNumberOfPeople, AdditionalServices, Price, Discount, IsReserved, ClientsEmailAddress, ReservationType, ShipId, OwnerId)
                           VALUES (@ShipName, @StartDate, @StartTime, @EndDate, @EndTime, @ValidityPeriodDate, @ValidityPeriodTime, @MaxNumberOfPeople, @AdditionalServices, @Price, @Discount, @IsReserved, @ClientsEmailAddress, @ReservationType, @ShipId, @OwnerId);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int UpdateAdventureReservationDates(int id,
                                                DateTime fromDate,
                                                TimeSpan fromTime,
                                                DateTime toDate,
                                                TimeSpan toTime,
                                                string instructorId)
        {

            AdventureReservation data = new AdventureReservation
            {
                Id = id,
                StartDate = fromDate,
                StartTime = fromTime,
                EndDate = toDate,
                EndTime = toTime,
                InstructorId = instructorId
            };

            string sql = @" UPDATE dbo.AdventureReservations
                            SET StartDate = @StartDate, StartTime = @StartTime, EndDate = @EndDate, EndTime = @EndTime  
                            WHERE Id = @Id AND InstructorId = @InstructorId;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int UpdateCottageReservationDates(int id,
                                                DateTime fromDate,
                                                TimeSpan fromTime,
                                                DateTime toDate,
                                                TimeSpan toTime,
                                                string ownerId)
        {

            CottageReservation data = new CottageReservation
            {
                Id = id,
                StartDate = fromDate,
                StartTime = fromTime,
                EndDate = toDate,
                EndTime = toTime,
                OwnerId = ownerId
            };

            string sql = @" UPDATE dbo.CottageReservations
                            SET StartDate = @StartDate, StartTime = @StartTime, EndDate = @EndDate, EndTime = @EndTime  
                            WHERE Id = @Id AND OwnerId = @OwnerId;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int UpdateShipReservationDates(int id,
                                                DateTime fromDate,
                                                TimeSpan fromTime,
                                                DateTime toDate,
                                                TimeSpan toTime,
                                                string ownerId)
        {

            ShipReservation data = new ShipReservation
            {
                Id = id,
                StartDate = fromDate,
                StartTime = fromTime,
                EndDate = toDate,
                EndTime = toTime,
                OwnerId = ownerId
            };

            string sql = @" UPDATE dbo.ShipReservations
                            SET StartDate = @StartDate, StartTime = @StartTime, EndDate = @EndDate, EndTime = @EndTime  
                            WHERE Id = @Id AND OwnerId = @OwnerId;";

            return SSMSDataAccess.SaveData(sql, data);
        }


        public static int DeleteAdventureReservation(int reservationId)
        {
            Reservation data = new Reservation
            {
                Id = reservationId
            };

            string sql = @" DELETE 
                            FROM dbo.AdventureReservations
                            WHERE Id = @Id;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int DeleteCottageReservation(int reservationId)
        {
            Reservation data = new Reservation
            {
                Id = reservationId
            };

            string sql = @" DELETE 
                            FROM dbo.CottageReservations
                            WHERE Id = @Id;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int DeleteShipReservation(int reservationId)
        {
            Reservation data = new Reservation
            {
                Id = reservationId
            };

            string sql = @" DELETE 
                            FROM dbo.ShipReservations
                            WHERE Id = @Id;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static List<AdventureReservation> LoadAdventureReservations()
        {
            string sql = @"SELECT *
                           FROM dbo.AdventureReservations;";

            return SSMSDataAccess.LoadData<AdventureReservation>(sql);
        }

        public static AdventureReservation LoadAdventureReservationById(int reservationId) 
        {
            string sql = @"SELECT *
                            FROM dbo.AdventureReservations
                            WHERE Id = @Id;";
            return SSMSDataAccess.LoadReservationById<AdventureReservation>(sql, reservationId);
        }

        public static List<AdventureReservation> LoadAdventureReservationByInstructorId(string instructorId)
        {
            string sql = @"SELECT *
                            FROM dbo.AdventureReservations
                            WHERE InstructorId = @InstructorId;";
            return SSMSDataAccess.LoadAdventureReservationsByInstructorId<AdventureReservation>(sql, instructorId);
        }

        public static List<AdventureReservation> LoadReservedAdventureReservationByInstructorId(string instructorId)
        {
            string sql = @"SELECT *
                            FROM dbo.AdventureReservations
                            WHERE InstructorId = @InstructorId AND IsReserved = 'True';";
            return SSMSDataAccess.LoadAdventureReservationsByInstructorId<AdventureReservation>(sql, instructorId);
        }

        public static List<CottageReservation> LoadCottageReservationByOwnerId(string ownerId)
        {
            string sql = @"SELECT *
                            FROM dbo.CottageReservations
                            WHERE OwnerId = @OwnerId;";
            return SSMSDataAccess.LoadCottageReservationsByOwnerId<CottageReservation>(sql, ownerId);
        }

        public static List<ShipReservation> LoadShipReservationByOwnerId(string ownerId)
        {
            string sql = @"SELECT *
                            FROM dbo.ShipReservations
                            WHERE OwnerId = @OwnerId;";
            return SSMSDataAccess.LoadShipReservationsByOwnerId<ShipReservation>(sql, ownerId);
        }

        public static List<AdventureReservation> LoadReservedAdventureReservationByInstructorId(string instructorId, bool isReserved)
        {
            string sql = @"SELECT *
                            FROM dbo.AdventureReservations
                            WHERE InstructorId = @InstructorId AND IsReserved = @IsReserved;";
            return SSMSDataAccess.LoadReservationsByInstructorId<AdventureReservation>(sql, instructorId, isReserved);
        }

        public static List<AdventureReservation> LoadNonReservedAdventureReservationByInstructorId(string instructorId, bool isReserved)
        {
            string sql = @"SELECT *
                            FROM dbo.AdventureReservations
                            WHERE InstructorId = @InstructorId AND IsReserved = @IsReserved;";
            return SSMSDataAccess.LoadReservationsByInstructorId<AdventureReservation>(sql, instructorId, isReserved);
        }

        public static List<ReservationFromHistory> LoadReservationsFromHistory()
        {
            string sql = @"SELECT *
                           FROM dbo.ReservationHistory;";

            return SSMSDataAccess.LoadData<ReservationFromHistory>(sql);
        }

        public static List<ReservationFromHistory> LoadReservationsFromHistoryByOwnerId(string ownerId)
        {
            string sql = @"SELECT *
                           FROM dbo.ReservationHistory
                           WHERE OwnerId = @OwnerId;";
            return SSMSDataAccess.LoadHistoryReservationByOwnerId<ReservationFromHistory>(sql, ownerId);
        }

        public static List<ReservationFromHistory> LoadReservationsFromHistoryByClientsEmailAddress(string emailAddress)
        {
            string sql = @"SELECT *
                           FROM dbo.ReservationHistory
                           WHERE ClientsEmailAddress = @ClientsEmailAddress;";
            return SSMSDataAccess.LoadHistoryReservationByClientsEmailAddress<ReservationFromHistory>(sql, emailAddress);
        }

        public static List<Reservation> LoadReservationsByAdventureId(int adventureId)
        {
            string sql = @"SELECT *
                           FROM dbo.AdventureReservations
                           WHERE AdventureId = @AdventureId;";

            return SSMSDataAccess.LoadReservationsByAdventureId<Reservation>(sql, adventureId);
        }

        public static List<AdventureReservation> LoadAdventureReservationsByAdventureId(int adventureId)
        {
            string sql = @"SELECT *
                           FROM dbo.AdventureReservations
                           WHERE AdventureId = @AdventureId;";

            return SSMSDataAccess.LoadReservationsByAdventureId<AdventureReservation>(sql, adventureId);
        }

        public static List<Reservation> LoadReservationsByShipId(int shipId)
        {
            string sql = @"SELECT *
                           FROM dbo.ShipReservations
                           WHERE ShipId = @ShipId;";

            return SSMSDataAccess.LoadReservationsByShipId<Reservation>(sql, shipId);
        }

        public static List<Reservation> LoadReservationsByCottageId(int cottageId)
        {
            string sql = @"SELECT *
                           FROM dbo.CottageReservations
                           WHERE CottageId = @CottageId;";

            return SSMSDataAccess.LoadReservationsByCottageId<Reservation>(sql, cottageId);
        }

        public static List<AdventureReservation> LoadAdventureReservationsByClient(string clientEmail)
        {
            string sql = @"SELECT *
                           FROM dbo.AdventureReservations
                           WHERE ClientsEmailAddress = @ClientsEmailAddress;";

            return SSMSDataAccess.LoadCurrentReservationByClientsEmailAddress<AdventureReservation>(sql, clientEmail);
        }

        public static List<CottageReservation> LoadCottageReservationsByClient(string clientEmail)
        {
            string sql = @"SELECT *
                           FROM dbo.CottageReservations
                           WHERE ClientsEmailAddress = @ClientsEmailAddress;";

            return SSMSDataAccess.LoadCurrentReservationByClientsEmailAddress<CottageReservation>(sql, clientEmail);
        }

        public static List<ShipReservation> LoadShipReservationsByClient(string clientEmail)
        {
            string sql = @"SELECT *
                           FROM dbo.ShipReservations
                           WHERE ClientsEmailAddress = @ClientsEmailAddress;";

            return SSMSDataAccess.LoadCurrentReservationByClientsEmailAddress<ShipReservation>(sql, clientEmail);
        }

        public static List<CottageReservation> LoadCottageReservations()
        {
            string sql = @"SELECT *
                           FROM dbo.CottageReservations;";

            return SSMSDataAccess.LoadData<CottageReservation>(sql);
        }

        public static List<ShipReservation> LoadShipReservations()
        {
            string sql = @"SELECT *
                           FROM dbo.ShipReservations;";

            return SSMSDataAccess.LoadData<ShipReservation>(sql);
        }

        //public static List<DayOfWeekIncome> LoadIncomePerDayOfWeek(int advId)
        //{
        //    string sql = @"SELECT dayOfWeek, SUM(Price)
        //                   FROM AdventureReservations
        //                   WHERE AdventureId = @AdventureId
        //                   GROUP BY dayOfWeek";

        //    return SSMSDataAccess.LoadIncomePerDayOfWeek<DayOfWeekIncome>(sql, advId);
        //}

    }
}
