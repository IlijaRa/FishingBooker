using Dapper;
using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.BusinessLogic
{
    public class DeactivationRequestCRUD
    {
        public static int SendDeactivationRequest(string name,
                                                    string surname,
                                                    string email,
                                                    string reason)
        {
            DeactivationRequest data = new DeactivationRequest
            {
                UserName = name,
                UserSurname = surname,
                EmailAddress = email,
                Reason = reason,
                Status = Enums.DeactivationRequestStatus.NotErased
            };

            string sql = @"INSERT INTO dbo.DeactivationRequests (UserName, UserSurname, EmailAddress, Reason, Status)
                           VALUES (@UserName, @UserSurname, @EmailAddress, @Reason, @Status);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static List<DeactivationRequest> LoadDeactivationRequests()
        {
            const string sql = @"SELECT *
                           FROM dbo.DeactivationRequests;";

            return SSMSDataAccess.LoadData<DeactivationRequest>(sql);
        }

        public static int DeleteDeactivationRequest(string email)
        {

            DeactivationRequest data = new DeactivationRequest
            {
                EmailAddress = email,
            };

            string sql = @" DELETE 
                            FROM dbo.DeactivationRequests 
                            WHERE EmailAddress = @EmailAddress;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        // Functions for concurrent acces to database

        //public async Task<DeactivationRequest> GetDeactivationRequestAsync()
        //{
        //    const string Sql = @"SELECT * FROM Employee";

        //    using (DbConnection cnn = new SqlConnection(SSMSDataAccess.GettConnectionstring()))
        //    {
        //        return cnn.SingleOrDefaultAsync(Sql);
        //    }

        //}

        //public static async Task<DeactivationRequest> GetDeactivationRequestAsync(string email)
        //{
        //    const string Sql = "SELECT * FROM dbo.DeactivationRequests WHERE EmailAddress = @EmailAddress";

        //    using (IDbConnection cnn = new SqlConnection(SSMSDataAccess.GettConnectionstring()))
        //    {
        //        return cnn.QueryFirstOrDefault<DeactivationRequest>(Sql, new { email });
        //    }

        //}

        public async Task<DeactivationRequest> InsertDeactivationRequestAsync(DeactivationRequest request)
        {
            const string Sql = @" INSERT INTO dbo.DeactivationRequests (UserName, UserSurnam, EmailAddress, Reason)
                                  VALUES ( @UserName, @UserSurname, @EmailAddress, @Reason )

                                  SELECT @Id = Id, @ConcurrencyToken = ConcurrencyToken
                                  FROM dbo.DeactivationRequests WHERE Id = SCOPE_IDENTITY()";
            
            using (IDbConnection cnn = new SqlConnection(SSMSDataAccess.GettConnectionstring()))
            {
                var @params = new DynamicParameters(request)
                .Output(request, e => e.ConcurrencyToken)
                .Output(request, e => e.Id);

                await cnn.ExecuteAsync(Sql, @params);
                return request;
            }

        }

        public static int UpdateDeactivationRequest(DeactivationRequest request)
        {
            string Sql = @"UPDATE dbo.DeactivationRequests 
                                SET UserName = @UserName, UserSurname = @UserSurname, EmailAddress = @EmailAddress, Reason = @Reason, Status = @Status
                                WHERE Id = @Id AND ConcurrencyToken = @ConcurrencyToken";

            var rowCount = -1;
            using (IDbConnection cnn = new SqlConnection(SSMSDataAccess.GettConnectionstring()))
            {
                rowCount = cnn.Execute(Sql, request);
            }

            if (rowCount == 0)
            {
                throw new Exception("Oh no, someone else edited this record!");
            }

            return rowCount;
        }
    }
}
