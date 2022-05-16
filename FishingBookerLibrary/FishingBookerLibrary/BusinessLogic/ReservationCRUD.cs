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
                                                            string duration,
                                                            int maxNum,
                                                            string additionalServices,
                                                            decimal price,
                                                            bool isReserved,
                                                            string clientsemailAddress,
                                                            Enums.ReservationType type,
                                                            int adventureId)
        {
            AdventureReservation data = new AdventureReservation
            {
                Place = place,
                StartDate = startdate,
                StartTime = starttime,
                Duration = duration,
                MaxNumberOfPeople = maxNum,
                AdditionalServices = additionalServices,
                Price = price,
                Discount = 0,
                IsReserved = isReserved,
                ClientsEmailAddress = clientsemailAddress,
                ReservationType = type,
                AdventureId = adventureId,
            };

            string sql = @"INSERT INTO dbo.AdventureReservations (Place, StartDate, StartTime, Duration, MaxNumberOfPeople, AdditionalServices, Price, Discount, IsReserved, ClientsEmailAddress, ReservationType, AdventureId)
                           VALUES (@Place, @StartDate, @StartTime, @Duration, @MaxNumberOfPeople, @AdditionalServices, @Price, @Discount, @IsReserved, @ClientsEmailAddress, @ReservationType, @AdventureId);";

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

        public static List<ReservationFromHistory> LoadReservationsFromHistory()
        {
            string sql = @"SELECT *
                           FROM dbo.ReservationHistory;";

            return SSMSDataAccess.LoadData<ReservationFromHistory>(sql);
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

    }
}
