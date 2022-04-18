using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.BusinessLogic
{
    public class RecordCRUD
    {
        public static int CreateRecord(string clientsEmailAddress,
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

        public static int DeleteRecord(int recordId)
        {
            Record data = new Record
            {
                Id = recordId
            };

            string sql = @" DELETE 
                            FROM dbo.Records 
                            WHERE Id = @Id;";

            return SSMSDataAccess.SaveData(sql, data);
        }
    }
}
