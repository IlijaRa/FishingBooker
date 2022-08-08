using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.BusinessLogic
{
    public class ScheduleCRUD
    {
        //public static int CreateOwnerAvailabilityForStandardReservation(DateTime fromDate,
        //                                        TimeSpan fromTime,
        //                                        DateTime toDate,
        //                                        TimeSpan toTime,
        //                                        string instructorId)
        //{

        //    OwnerAvailabilityForStandardReservation data = new OwnerAvailabilityForStandardReservation
        //    {
        //        FromDate = fromDate,
        //        FromTime = fromTime,
        //        ToDate = toDate,
        //        ToTime = toTime,
        //        InstructorId = instructorId
        //    };
        //    string sql = @" INSERT INTO dbo.OwnerAvailabilityStandardReservations(FromDate, FromTime, ToDate, ToTime, InstructorId)  
        //                    VALUES (@FromDate, @FromTime, @ToDate, @ToTime, @InstructorId);";

        //    return SSMSDataAccess.SaveData(sql, data);
        //}

        public static int CreateAvailability(DateTime fromDate,
                                                TimeSpan fromTime,
                                                DateTime toDate,
                                                TimeSpan toTime,
                                                string ownersId,
                                                string text)
        {

            OwnerAvailabilityUnavailability data = new OwnerAvailabilityUnavailability
            {
                FromDate = fromDate,
                FromTime = fromTime,
                ToDate = toDate,
                ToTime = toTime,
                OwnerId = ownersId,
                Type = Enums.AvailabilityUnavailabilityType.Availability,
                Text = text
            };
            string sql = @" INSERT INTO dbo.OwnerAvailabilitiesUnavailabilities(FromDate, FromTime, ToDate, ToTime, OwnerId, Type, Text)  
                            VALUES (@FromDate, @FromTime, @ToDate, @ToTime, @OwnerId, @Type, @Text);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int CreateUnavailability(DateTime fromDate,
                                                TimeSpan fromTime,
                                                DateTime toDate,
                                                TimeSpan toTime,
                                                string ownersId,
                                                string text)
        {

            OwnersUnavailability data = new OwnersUnavailability
            {
                FromDate = fromDate,
                FromTime = fromTime,
                ToDate = toDate,
                ToTime = toTime,
                OwnerId = ownersId,
                Type = Enums.AvailabilityUnavailabilityType.Unavailability,
                Text = text
            };
            string sql = @" INSERT INTO dbo.OwnerAvailabilitiesUnavailabilities(FromDate, FromTime, ToDate, ToTime, OwnerId, Type, Text)  
                            VALUES (@FromDate, @FromTime, @ToDate, @ToTime, @OwnerId, @Type, @Text);";

            return SSMSDataAccess.SaveData(sql, data);
        }


        //public static int UpdateOwnerAvailabilityForStandardReservation(DateTime fromDate,
        //                                                                TimeSpan fromTime,
        //                                                                DateTime toDate,
        //                                                                TimeSpan toTime,
        //                                                                string instructorId)
        //{

        //    OwnerAvailabilityForStandardReservation data = new OwnerAvailabilityForStandardReservation
        //    {
        //        FromDate = fromDate,
        //        FromTime = fromTime,
        //        ToDate = toDate,
        //        ToTime = toTime,
        //        InstructorId = instructorId
        //    };
        //    string sql = @" UPDATE dbo.OwnerAvailabilityStandardReservations
        //                    SET FromDate = @FromDate, FromTime = @FromTime, ToDate = @ToDate, ToTime = @ToTime  
        //                    WHERE InstructorId = @InstructorId;";

        //    return SSMSDataAccess.SaveData(sql, data);
        //}


        public static int UpdateAvailability(   int id,
                                                DateTime fromDate,
                                                TimeSpan fromTime,
                                                DateTime toDate,
                                                TimeSpan toTime,
                                                string ownerId,
                                                string text)
        {

            OwnerAvailabilityUnavailability data = new OwnerAvailabilityUnavailability
            {
                Id = id,
                FromDate = fromDate,
                FromTime = fromTime,
                ToDate = toDate,
                ToTime = toTime,
                OwnerId = ownerId,
                Type = Enums.AvailabilityUnavailabilityType.Availability,
                Text = text
            };
            string sql = @" UPDATE dbo.OwnerAvailabilitiesUnavailabilities
                            SET FromDate = @FromDate, FromTime = @FromTime, ToDate = @ToDate, ToTime = @ToTime, Text = @Text
                            WHERE Id = @Id AND Type = @Type;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int UpdateUnavailability( int id,
                                                DateTime fromDate,
                                                TimeSpan fromTime,
                                                DateTime toDate,
                                                TimeSpan toTime,
                                                string ownersId,
                                                string text)
        {

            OwnerAvailabilityUnavailability data = new OwnerAvailabilityUnavailability
            {
                Id = id,
                FromDate = fromDate,
                FromTime = fromTime,
                ToDate = toDate,
                ToTime = toTime,
                OwnerId = ownersId,
                Type = Enums.AvailabilityUnavailabilityType.Unavailability,
                Text = text
            };

            string sql = @" UPDATE dbo.OwnerAvailabilitiesUnavailabilities
                            SET FromDate = @FromDate, FromTime = @FromTime, ToDate = @ToDate, ToTime = @ToTime, Text = @Text 
                            WHERE Id = @Id AND Type = @Type;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int DeleteAvailability(   int id,
                                                string ownersId)
        {
            OwnerAvailabilityUnavailability data = new OwnerAvailabilityUnavailability
            {
                Id = id,
                OwnerId = ownersId,
                Type = Enums.AvailabilityUnavailabilityType.Availability
            };

            string sql = @" DELETE 
                            FROM dbo.OwnerAvailabilitiesUnavailabilities
                            WHERE Id = @Id AND OwnerId = @OwnerId AND Type = @Type;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int DeleteUnavailability(int id,
                                                string ownersId)
        {
            OwnerAvailabilityUnavailability data = new OwnerAvailabilityUnavailability
            {
                Id = id,
                OwnerId = ownersId,
                Type = Enums.AvailabilityUnavailabilityType.Unavailability
            };

            string sql = @" DELETE 
                            FROM dbo.OwnerAvailabilitiesUnavailabilities
                            WHERE Id = @Id AND OwnerId = @OwnerId AND Type = @Type;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        //public static InstructorAvailability LoadOwnerAvailabilityForStandardReservation(string instructorId)
        //{
        //    string sql = @"SELECT *
        //                    FROM dbo.OwnerAvailabilityStandardReservations
        //                    WHERE InstructorId = @InstructorId;";
        //    return SSMSDataAccess.LoadOwnerAvailability<InstructorAvailability>(sql, instructorId);
        //}

        public static List<OwnerAvailabilityUnavailability> LoadOwnerAvailabilities(string ownerId)
        {
            string sql = @"SELECT *
                            FROM dbo.OwnerAvailabilitiesUnavailabilities
                            WHERE OwnerId = @OwnerId AND Type = @Type;";
            return SSMSDataAccess.LoadOwnerAvailabilitiesUnavailabilities<OwnerAvailabilityUnavailability>(sql, ownerId, 0);
        }

        public static List<OwnerAvailabilityUnavailability> LoadAvailabilities()
        {
            string sql = @"SELECT *
                            FROM dbo.OwnerAvailabilitiesUnavailabilities
                            WHERE Type = @Type;";
            return SSMSDataAccess.LoadAvailabilitiesUnavailabilities<OwnerAvailabilityUnavailability>(sql, 0);
        }

        public static List<OwnerAvailabilityUnavailability> LoadUnavailabilities()
        {
            string sql = @"SELECT *
                            FROM dbo.OwnerAvailabilitiesUnavailabilities
                            WHERE Type = @Type;";
            return SSMSDataAccess.LoadAvailabilitiesUnavailabilities<OwnerAvailabilityUnavailability>(sql, 1);
        }

        public static List<OwnerAvailabilityUnavailability> LoadOwnerUnavailabilities(string ownerId)
        {
            string sql = @"SELECT *
                            FROM dbo.OwnerAvailabilitiesUnavailabilities
                            WHERE OwnerId = @OwnerId AND Type = @Type;";

            return SSMSDataAccess.LoadOwnerAvailabilitiesUnavailabilities<OwnerAvailabilityUnavailability>(sql, ownerId, 1);
        }     
    }
}
