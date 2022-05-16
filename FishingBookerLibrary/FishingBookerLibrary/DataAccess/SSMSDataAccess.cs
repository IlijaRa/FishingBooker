using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FishingBookerLibrary.DataAccess
{
    public static class SSMSDataAccess
    {
        public static string GettConnectionstring(string connectionName = "FishingBookerDB")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static T LoadSingleData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return (T)cnn.Query(sql);
            }
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Execute(sql, data);
            }
        }

        public static T LoadReservationById<T>(string sql, int reservationId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { Id = reservationId }).FirstOrDefault();
            }
        }

        public static List<T> LoadReservationsByAdventureId<T>(string sql, int adventureId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { AdventureId = adventureId }).ToList();
            }
        }

        public static List<T> LoadHistoryReservationByClientsEmailAddress<T>(string sql, string emailAddress)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { ClientsEmailAddress = emailAddress }).ToList();
            }
        }

        public static T LoadInstructorsAvailability<T>(string sql, string instructorId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { InstructorId = instructorId }).FirstOrDefault();
            }
        }

        public static List<T> LoadAdventureTitlesByInstructor<T>(string sql, string instructorId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { InstructorId = instructorId }).ToList();
            }
        }

        public static List<T> LoadCottageTitlesByOwner<T>(string sql, string ownerId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { OwnerId = ownerId }).ToList();
            }
        }

        public static List<T> LoadShipTitlesByOwner<T>(string sql, string ownerId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { OwnerId = ownerId }).ToList();
            }
        }

    }
}
