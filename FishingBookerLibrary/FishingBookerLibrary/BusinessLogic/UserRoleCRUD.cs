using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.BusinessLogic
{
    public class UserRoleCRUD
    {
        public static int SetRoleInDB(string userId, Enums.RegistrationTypeInDB roleId)
        {
            UserRole data = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            string sql = @"INSERT INTO dbo.UserRole (UserId, RoleId)
                           VALUES (@UserId, @RoleId);";
            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int UpdateRoleInDB(string userId, Enums.RegistrationTypeInDB roleId)
        {
            UserRole data = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            string sql = @"UPDATE dbo.UserRole 
                           SET RoleId = @RoleId
                           WHERE UserId = @UserId;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int DeleteUserInUserRole(string userId)
        {
            UserRole data = new UserRole
            {
                UserId = userId
            };

            string sql = @" DELETE 
                            FROM dbo.UserRole 
                            WHERE UserId = @UserId;";

            return SSMSDataAccess.SaveData(sql, data);
        }
    }
}
