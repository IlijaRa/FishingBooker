using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.BusinessLogic
{
    public static class RegUserCRUD
    {
        public static int CreateUser(string name,
                                     string surname,
                                     string phone,
                                     string email,
                                     string password,
                                     string type,
                                     string address,
                                     string city,
                                     string country,
                                     string description)
        {
            RegUser data = new RegUser
            {
                Name = name,
                Surname = surname,
                PhoneNumber = phone,
                EmailAddress = email,
                Password = password,
                Type = type,
                Address = address,
                City = city,
                Country = country,
                Description = description
            };

            string sql = @"INSERT INTO dbo.RegUsers (Name, Surname, PhoneNumber, EmailAddress, Password, Type, Address, City, Country, Description, Status)
                           VALUES (@Name, @Surname, @PhoneNumber, @EmailAddress, @Password, @Type, @Address, @City, @Country, @Description, @Status);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int UpdateUserBasicInfo(string name,
                                              string surname,
                                              string phone,
                                              string email,
                                              string address,
                                              string city,
                                              string country)
        {

            RegUser data = new RegUser
            {
                Name = name,
                Surname = surname,
                PhoneNumber = phone,
                EmailAddress = email,
                Address = address,
                City = city,
                Country = country
            };
            string sql = @" UPDATE dbo.RegUsers
                            SET Name = @Name, Surname = @Surname, PhoneNumber = @PhoneNumber, Address = @Address, City = @City, Country = @Country  
                            WHERE EmailAddress = @EmailAddress;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int UpdateUserStatus(string email, string status)
        {

            RegUser data = new RegUser
            {
                EmailAddress = email,
                Status = status
            };
            string sql = @" UPDATE dbo.RegUsers
                            SET Status = @Status
                            WHERE EmailAddress = @EmailAddress;";
            

            return SSMSDataAccess.SaveData(sql, data);
        }


        public static List<RegUser> LoadUsers()
        {
            string sql = @"SELECT Name, Surname, PhoneNumber, EmailAddress, Password, Type, Address, City, Country, Description, Status
                           FROM dbo.RegUsers;";

            return SSMSDataAccess.LoadData<RegUser>(sql);
        }

        // ne radi
        //public static List<RegUser> LoadUserByEmail(string email)
        //{
        //    RegUser data = new RegUser
        //    {
        //        EmailAddress = email
        //    };

        //    string sql = @"SELECT Name, Surname, PhoneNumber, EmailAddress, Password, Type, Address, City, Country, Description, Status
        //                   FROM dbo.RegUsers
        //                   WHERE EmailAddress = @EmailAddress;";

        //    return SSMSDataAccess.LoadData<RegUser>(sql);
        //}

        public static int DeleteUserByEmail(string email)
        {
            RegUser data = new RegUser
            {
                EmailAddress = email
            };

            string sql = @" DELETE 
                            FROM dbo.RegUsers 
                            WHERE EmailAddress = @EmailAddress;";

            return SSMSDataAccess.SaveData(sql, data);
        }

    }
}
