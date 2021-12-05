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
                                     string description){
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

            string sql = @"INSERT INTO dbo.RegUser (Name, Surname, PhoneNumber, EmailAddress, Password, Type, Address, City, Country, Description, Status)
                           VALUES (@Name, @Surname, @PhoneNumber, @EmailAddress, @Password, @Type, @Address, @City, @Country, @Description, @Status);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static List<RegUser> LoadUsers()
        {
            string sql = @"SELECT *
                           FROM dbo.RegUser;";

            return SSMSDataAccess.LoadData<RegUser>(sql);
        }
    }
}
