using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
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
                Reason = reason
            };

            string sql = @"INSERT INTO dbo.DeactivationRequests (UserName, UserSurname, EmailAddress, Reason)
                           VALUES (@UserName, @UserSurname, @EmailAddress, @Reason);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static List<DeactivationRequest> LoadDeactivationRequests()
        {
            string sql = @"SELECT *
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

    }
}
