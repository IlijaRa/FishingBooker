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
        public static int CreateOwnerAvailabilityForStandardReservation(DateTime fromDate,
                                                TimeSpan fromTime,
                                                DateTime toDate,
                                                TimeSpan toTime,
                                                string instructorId)
        {

            OwnerAvailabilityForStandardReservation data = new OwnerAvailabilityForStandardReservation
            {
                FromDate = fromDate,
                FromTime = fromTime,
                ToDate = toDate,
                ToTime = toTime,
                InstructorId = instructorId
            };
            string sql = @" INSERT INTO dbo.OwnerAvailabilityStandardReservations(FromDate, FromTime, ToDate, ToTime, InstructorId)  
                            VALUES (@FromDate, @FromTime, @ToDate, @ToTime, @InstructorId);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int CreateUnavailability(DateTime fromDate,
                                                TimeSpan fromTime,
                                                DateTime toDate,
                                                TimeSpan toTime,
                                                string ownersId)
        {

            OwnersUnavailability data = new OwnersUnavailability
            {
                FromDate = fromDate,
                FromTime = fromTime,
                ToDate = toDate,
                ToTime = toTime,
                OwnerId = ownersId
            };
            string sql = @" INSERT INTO dbo.OwnerUnavailabilities(FromDate, FromTime, ToDate, ToTime, OwnerId)  
                            VALUES (@FromDate, @FromTime, @ToDate, @ToTime, @OwnerId);";

            return SSMSDataAccess.SaveData(sql, data);
        }


        public static int UpdateOwnerAvailabilityForStandardReservation(DateTime fromDate,
                                                                        TimeSpan fromTime,
                                                                        DateTime toDate,
                                                                        TimeSpan toTime,
                                                                        string instructorId)
        {

            OwnerAvailabilityForStandardReservation data = new OwnerAvailabilityForStandardReservation
            {
                FromDate = fromDate,
                FromTime = fromTime,
                ToDate = toDate,
                ToTime = toTime,
                InstructorId = instructorId
            };
            string sql = @" UPDATE dbo.OwnerAvailabilityStandardReservations
                            SET FromDate = @FromDate, FromTime = @FromTime, ToDate = @ToDate, ToTime = @ToTime  
                            WHERE InstructorId = @InstructorId;";

            return SSMSDataAccess.SaveData(sql, data);
        }


        public static int UpdateAvailability(   int id,
                                                DateTime fromDate,
                                                TimeSpan fromTime,
                                                DateTime toDate,
                                                TimeSpan toTime,
                                                string instructorId)
        {

            InstructorAvailability data = new InstructorAvailability
            {
                FromDate = fromDate,
                FromTime = fromTime,
                ToDate = toDate,
                ToTime = toTime,
                InstructorId = instructorId
            };
            string sql = @" UPDATE dbo.InstructorsAvailabilities
                            SET FromDate = @FromDate, FromTime = @FromTime, ToDate = @ToDate, ToTime = @ToTime  
                            WHERE InstructorId = @InstructorId;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static InstructorAvailability LoadOwnerAvailabilityForStandardReservation(string instructorId)
        {
            string sql = @"SELECT *
                            FROM dbo.OwnerAvailabilityStandardReservations
                            WHERE InstructorId = @InstructorId;";
            return SSMSDataAccess.LoadInstructorsAvailability<InstructorAvailability>(sql, instructorId);
        }

        public static InstructorAvailability LoadInstructorsAvailability(string instructorsId)
        {
            string sql = @"SELECT *
                            FROM dbo.InstructorsAvailabilities
                            WHERE InstructorId = @InstructorId;";
            return SSMSDataAccess.LoadInstructorsAvailability<InstructorAvailability>(sql, instructorsId);
        }

        public static List<InstructorAvailability> LoadAvailabilities()
        {
            string sql = @"SELECT *
                           FROM dbo.InstructorsAvailabilities;";

            return SSMSDataAccess.LoadData<InstructorAvailability>(sql);
        }

        //public static InstructorAvailability LoadInstructorAvailability(string instructorId)
        //{
        //    string sql = @"SELECT *
        //                    FROM dbo.InstructorsAvailabilities
        //                    WHERE InstructorId = @InstructorId;";
        //    return SSMSDataAccess.LoadInstructorsAvailability<InstructorAvailability>(sql, instructorId);
        //}

        public static List<OwnersUnavailability> LoadOwnerUnavailability(string ownerId)
        {
            string sql = @"SELECT *
                            FROM dbo.OwnerUnavailabilities
                            WHERE OwnerId = @OwnerId;";

            return SSMSDataAccess.LoadOwnerUnavailabilities<OwnersUnavailability>(sql, ownerId);
        }

    }
}
