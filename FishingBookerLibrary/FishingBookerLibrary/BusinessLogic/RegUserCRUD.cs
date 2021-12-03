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
        public static int CreateEmployee(string name, 
                                         string surname,
                                         string phone, 
                                         string email,
                                         string password,
                                         string address,
                                         string city, 
                                         string country){
            RegUser data = new RegUser
            {
                Name = name,
                Surname = surname,
                PhoneNumber = phone,
                EmailAddress = email,
                Password = password,
                Address = address,
                City = city,
                Country = country
            };

            string sql = @"INSERT INTO dbo.RegUser (Name, Surname, PhoneNumber, EmailAddress, Password, Address, City, Country)
                           VALUES (@Name, @Surname, @PhoneNumber, @EmailAddress, @Password, @Address, @City, @Country);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static List<RegUser> LoadEmployees()
        {
            string sql = @"SELECT *
                           FROM dbo.RegUser;";

            return SSMSDataAccess.LoadData<RegUser>(sql);
        }
    }
}
