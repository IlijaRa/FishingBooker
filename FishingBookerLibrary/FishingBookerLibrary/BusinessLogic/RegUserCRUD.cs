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
                                     string description,
                                     string biography)
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
                Description = description,
                Biography = biography,
            };

            string sql = @"INSERT INTO dbo.RegUsers (Name, Surname, PhoneNumber, EmailAddress, Password, Type, Address, City, Country, Description, Biography, Status, Penalties)
                           VALUES (@Name, @Surname, @PhoneNumber, @EmailAddress, @Password, @Type, @Address, @City, @Country, @Description, @Biography, @Status, @Penalties);";

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

        public static int UpdateBiography(string userId, string biography)
        {
            RegUser data = new RegUser
            {
                Id = userId,
                Biography = biography
            };

            string sql = @" UPDATE dbo.RegUsers
                            SET Biography = @Biography
                            WHERE Id = @Id";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int UpdateRating(string userId, float rating, float ratingSum, float ratingCount)
        {

            RegUser data = new RegUser
            {
                Id = userId,
                Rating = rating,
                RatingSum = ratingSum,
                RatingCount = ratingCount
            };
            string sql = @" UPDATE dbo.RegUsers
                            SET Rating = @Rating, RatingSum = @RatingSum, RatingCount = @RatingCount
                            WHERE Id = @Id;";


            return SSMSDataAccess.SaveData(sql, data);
        }

        public static List<RegUser> LoadUsers()
        {
            string sql = @"SELECT *
                           FROM dbo.RegUsers;";

            return SSMSDataAccess.LoadData<RegUser>(sql);
        }

        // ne radi where Id = @Id
        //public static RegUser LoadUser(string userId)
        //{

        //    string sql = @"SELECT *
        //                   FROM dbo.RegUsers
        //                   WHERE Id = @Id;";

        //    return SSMSDataAccess.LoadSingleData<RegUser>(sql);
        //}

        // ne radi
        //public static List<RegUser> LoadUserByEmail(string email)
        //{
        //    RegUser data = new RegUser
        //    {
        //        EmailAddress = email
        //    };

        //    string sql = @"SELECT *
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

        public static RegUser LoadUserById(string userId)
        {
            string sql = @"SELECT *
                            FROM dbo.RegUsers
                            WHERE Id = @Id;";

            return SSMSDataAccess.LoadUserById<RegUser>(sql, userId);
        }

        

        public static int AddPenalty(string clientEmail, int numberOfPenalties)
        {

            RegUser data = new RegUser
            {
                EmailAddress = clientEmail,
                Penalties = numberOfPenalties
            };

            string sql = @" UPDATE dbo.RegUsers
                            SET Penalties = @Penalties  
                            WHERE EmailAddress = @EmailAddress;";

            return SSMSDataAccess.SaveData(sql, data);
        }
    }
}
