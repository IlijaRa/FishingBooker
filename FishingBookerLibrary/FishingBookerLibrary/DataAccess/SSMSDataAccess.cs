using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FishingBookerLibrary.Models;

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

        public static T LoadUserById<T>(string sql, string userId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { Id = userId }).FirstOrDefault();
            }
        }

        public static T LoadLoyaltyProgram<T>(string sql, int loyaltyId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { Id = loyaltyId }).FirstOrDefault();
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

        public static List<T> LoadAdventureReservationsByInstructorId<T>(string sql, string instructorId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { InstructorId = instructorId }).ToList();
            }
        }

        public static List<T> LoadCottageReservationsByOwnerId<T>(string sql, string ownerId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { OwnerId = ownerId }).ToList();
            }
        }

        public static List<T> LoadShipReservationsByOwnerId<T>(string sql, string ownerId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { OwnerId = ownerId }).ToList();
            }
        }

        public static List<T> LoadReservationsByInstructorId<T>(string sql, string instructorId, bool isReserved)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { InstructorId = instructorId, IsReserved = isReserved}).ToList();
            }
        }

        public static List<T> LoadReservationsByAdventureId<T>(string sql, int adventureId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { AdventureId = adventureId }).ToList();
            }
        }

        public static List<T> LoadReservationsByShipId<T>(string sql, int shipId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { ShipId = shipId }).ToList();
            }
        }

        public static List<T> LoadReservationsByCottageId<T>(string sql, int cottageId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { CottageId = cottageId }).ToList();
            }
        }

        public static List<T> LoadHistoryReservationByClientsEmailAddress<T>(string sql, string emailAddress)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { ClientsEmailAddress = emailAddress }).ToList();
            }
        }

        public static List<T> LoadCurrentReservationByClientsEmailAddress<T>(string sql, string emailAddress)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { ClientsEmailAddress = emailAddress }).ToList();
            }
        }

        public static List<T> LoadOwnerAvailabilitiesUnavailabilities<T>(string sql, string ownerId, int type)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { OwnerId = ownerId, Type = type }).ToList();
            }
        }

        //public static List<T> LoadOwnerUnavailabilities<T>(string sql, string ownerId)
        //{
        //    using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
        //    {
        //        return cnn.Query<T>(sql, new { OwnerId = ownerId }).ToList();
        //    }
        //}

        public static List<T> LoadImagesByAdventureId<T>(string sql, int adventureId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { AdventureId = adventureId }).ToList();
            }
        }

        public static T LoadRevisionByEmailsAndTitle<T>(string sql, T clientsEmail, T entityTitle, T ownersEmail)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { ClientsEmailAddress = clientsEmail, EntityTitle = entityTitle, OwnersEmailAddress = ownersEmail }).FirstOrDefault();
            }
        }

        public static List<T> LoadAdventureByInstructorId<T>(string sql, string instructorId)
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

        public static List<T> LoadUnconfirmedRevisions<T>(string sql, bool state)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { State = state }).ToList();
            }
        }

        public static List<T> LoadConfirmedRevisionsForInstructor<T>(string sql, string instructorEmail, bool state)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { OwnersEmailAddress = instructorEmail, State = state }).ToList();
            }
        }

        public static List<T> LoadSubscribersByAdventure<T>(string sql, int adventureId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { AdventureId = adventureId }).ToList();
            }
        }

        public static List<T> LoadHistoryReservationByOwnerId<T>(string sql, string ownerId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { OwnerId = ownerId }).ToList();
            }
        }

        public static T LoadAdventuresByName<T>(string sql, string adventureTitle)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { Title = adventureTitle }).FirstOrDefault();
            }
        }

        public static T LoadAdventureById<T>(string sql, int adventureId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { Id = adventureId }).FirstOrDefault();
            }
        }

        public static T LoadCottagesByName<T>(string sql, string cottageTitle)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { Title = cottageTitle }).FirstOrDefault();
            }
        }

        public static T LoadShipsByName<T>(string sql, string shipTitle)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { Title = shipTitle }).FirstOrDefault();
            }
        }

        public static List<T> LoadIncomePerDayOfWeek<T>(string sql, int advId)
        {
            using (IDbConnection cnn = new SqlConnection(GettConnectionstring()))
            {
                return cnn.Query<T>(sql, new { AdventureId = advId }).ToList();
            }
        }
    }
}
