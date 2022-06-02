using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Place = place,
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
                AdventureId = adventureId,
                InstructorId = instructorId
            };

            string sql = @"INSERT INTO dbo.AdventureReservations (Place, StartDate, StartTime, EndDate, EndTime, ValidityPeriodDate, ValidityPeriodTime, MaxNumberOfPeople, AdditionalServices, Price, Discount, IsReserved, ClientsEmailAddress, ReservationType, AdventureId, InstructorId)
                           VALUES (@Place, @StartDate, @StartTime, @EndDate, @EndTime, @ValidityPeriodDate, @ValidityPeriodTime, @MaxNumberOfPeople, @AdditionalServices, @Price, @Discount, @IsReserved, @ClientsEmailAddress, @ReservationType, @AdventureId, @InstructorId);";

            return SSMSDataAccess.SaveData(sql, data);
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

    }
}
