using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;

namespace FishingBookerLibrary.BusinessLogic
{
    public static class FastReservationCRUD
    {
        public static int CreateAdventureFastReservations(  string place,
                                                            DateTime startdate,
                                                            TimeSpan starttime,
                                                            int duration,
                                                            int maxNum,
                                                            string additionalServices,
                                                            decimal price,
                                                            int adventureId)
        {
            AdventureFastReservation data = new AdventureFastReservation
            {
                Place = place,
                StartDate = startdate,
                StartTime = starttime,
                Duration = duration,
                MaxNumberOfPeople = maxNum,
                AdditionalServices = additionalServices,
                Price = price,
                Discount = 0,
                AdventureId = adventureId
            };

            string sql = @"INSERT INTO dbo.AdventureFastReservations (Place, StartDate, StartTime, Duration, MaxNumberOfPeople, AdditionalServices, Price, Discount, AdventureId)
                           VALUES (@Place, @StartDate, @StartTime, @Duration, @MaxNumberOfPeople, @AdditionalServices, @Price, @Discount, @AdventureId);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int DeleteFastReservation(int reservationId)
        {
            FastReservation data = new FastReservation
            {
                Id = reservationId
            };

            string sql = @" DELETE 
                            FROM dbo.AdventureFastReservations
                            WHERE Id = @Id;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static List<AdventureFastReservation> LoadAdventureFastReservations()
        {
            string sql = @"SELECT *
                           FROM dbo.AdventureFastReservations;";

            return SSMSDataAccess.LoadData<AdventureFastReservation>(sql);
        }
    }
}
