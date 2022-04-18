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

        public static int UpdateAvailability(int id,
                                                DateTime fromDate,
                                                TimeSpan fromTime,
                                                DateTime toDate,
                                                TimeSpan toTime,
                                                string instructorId)
        {

            InstructorAvailability data = new InstructorAvailability
            {
                Id = id,
                FromDate = fromDate,
                FromTime = fromTime,
                ToDate = toDate,
                ToTime = toTime,
                InstructorId = instructorId
            };
            string sql = @" UPDATE dbo.InstructorsAvailabilities
                            SET FromDate = @FromDate, FromTime = @FromTime, ToDate = @ToDate, ToTime = @ToTime  
                            WHERE Id = @Id;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int CreateRecord( string clientsEmailAddress,
                                        string instructorsEmailAddress,
                                        string comment,
                                        Enums.RecordImpressionType impressionType,
                                        string clientId,
                                        string instructorId)
        {
            Record data = new Record
            {
                ClientsEmailAddress = clientsEmailAddress,
                InstructorsEmailAddress = instructorsEmailAddress,
                Comment = comment,
                ImpressionType = impressionType,
                ClientId = clientId,
                InstructorId = instructorId
            };

            string sql = @"INSERT INTO dbo.Records (ClientsEmailAddress, InstructorsEmailAddress, Comment, ImpressionType, ClientId, InstructorId)
                           VALUES (@ClientsEmailAddress, @InstructorsEmailAddress, @Comment, @ImpressionType, @ClientId, @InstructorId);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static List<Record> LoadRecords()
        {
            string sql = @"SELECT *
                           FROM dbo.Records";

            return SSMSDataAccess.LoadData<Record>(sql);
        }

    }
}
