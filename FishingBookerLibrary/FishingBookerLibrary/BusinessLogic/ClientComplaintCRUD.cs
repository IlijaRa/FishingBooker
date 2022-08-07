using Dapper;
using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.BusinessLogic
{
    public class ClientComplaintCRUD
    {
        public static int CreateClientComplaint(string ownerId,
                                                string ownerName,
                                                string ownerSurname,
                                                string ownerEmail,
                                                string clientsEmail,
                                                string actionTitle,
                                                string reason)
        {
            ClientComplaint data = new ClientComplaint
            {
                OwnerId = ownerId,
                OwnerName = ownerName,
                OwnerSurname = ownerSurname,
                OwnerEmailAddress = ownerEmail,
                ClientsEmailAddress = clientsEmail,
                ActionTitle = actionTitle,
                Reason = reason,
                Status = Enums.ClientComplaintStatus.Waiting
            };

            string sql = @"INSERT INTO dbo.ClientComplaints (OwnerId, OwnerName, OwnerSurname, OwnerEmailAddress, ClientsEmailAddress, ActionTitle, Reason, Status)
                           VALUES (@OwnerId, @OwnerName, @OwnerSurname, @OwnerEmailAddress, @ClientsEmailAddress, @ActionTitle, @Reason, @Status);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static List<ClientComplaint> LoadClientComplaints()
        {
            string sql = @"SELECT *
                           FROM dbo.ClientComplaints;";

            return SSMSDataAccess.LoadData<ClientComplaint>(sql);
        }

        public static int DeleteClientComplaintById(int complaintId)
        {

            ClientComplaint data = new ClientComplaint
            {
                Id = complaintId,
            };

            string sql = @" DELETE 
                            FROM dbo.ClientComplaints 
                            WHERE Id = @Id;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int UpdateClientsComplaint(ClientComplaint complaint)
        {
            string Sql = @"UPDATE dbo.ClientComplaints 
                                SET OwnerName = @OwnerName, 
                                    OwnerSurname = @OwnerSurname, 
                                    OwnerEmailAddress = @OwnerEmailAddress, 
                                    ClientsEmailAddress = @ClientsEmailAddress, 
                                    ActionTitle = @ActionTitle, 
                                    Reason = @Reason, 
                                    Status = @Status
                                WHERE Id = @Id AND ConcurrencyToken = @ConcurrencyToken";

            var rowCount = -1;
            using (IDbConnection cnn = new SqlConnection(SSMSDataAccess.GettConnectionstring()))
            {
                rowCount = cnn.Execute(Sql, complaint);
            }

            //if (rowCount == 0)
            //{
            //    throw new Exception("Oh no, someone else edited this record!");
            //}

            return rowCount;
        }
    }
}