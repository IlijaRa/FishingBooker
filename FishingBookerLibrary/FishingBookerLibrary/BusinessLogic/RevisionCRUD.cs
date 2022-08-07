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
    public class RevisionCRUD
    {
        public static int CreateRevision(string clientsEmailAddress,
                                         string entityTitle,
                                         string ownersEmailAddress,
                                         string description,
                                         float actionRating,
                                         float ownerInstructorRating)
        {
            Revision data = new Revision
            {
                ClientsEmailAddress = clientsEmailAddress,
                EntityTitle = entityTitle,
                OwnersEmailAddress = ownersEmailAddress,
                Description = description,
                ActionRating = actionRating,
                OwnerInstructorRating = ownerInstructorRating,
                Status = Enums.RevisionStatus.Waiting
            };

            string sql = @"INSERT INTO dbo.Revisions (ClientsEmailAddress, EntityTitle, OwnersEmailAddress, Description, ActionRating, OwnerInstructorRating, State)
                           VALUES (@ClientsEmailAddress, @EntityTitle, @OwnersEmailAddress, @Description, @ActionRating, @OwnerInstructorRating, @State);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static string CheckIfRevisionAlreadyxists(string clientsEmail, string entityTitle, string ownersEmail)
        {
            string sql = @"SELECT *
                            FROM dbo.Revisions
                            WHERE ClientsEmailAddress = @ClientsEmailAddress AND EntityTitle = @EntityTitle AND OwnersEmailAddress = @OwnersEmailAddress;";

            return SSMSDataAccess.LoadRevisionByEmailsAndTitle(sql, clientsEmail, entityTitle, ownersEmail);
        }

        public static List<Revision> LoadRevisions()
        {
            string sql = @"SELECT *
                           FROM dbo.Revisions;";

            return SSMSDataAccess.LoadData<Revision>(sql);
        }

        public static List<Revision> LoadUnconfirmedRevisions()
        {
            string sql = @"SELECT *
                            FROM dbo.Revisions
                            WHERE State = @State;";

            return SSMSDataAccess.LoadUnconfirmedRevisions<Revision>(sql, false);
        }

        public static List<Revision> LoadConfirmedRevisionsForInstructor(string instructorsEmail)
        {
            string sql = @"SELECT *
                            FROM dbo.Revisions
                            WHERE OwnersEmailAddress = @OwnersEmailAddress AND State = @State;";

            return SSMSDataAccess.LoadConfirmedRevisionsForInstructor<Revision>(sql, instructorsEmail, true);
        }

        public static int UpdateConfirmStatus(int revisionId)
        {

            Revision data = new Revision
            {
                Id = revisionId,
                Status = Enums.RevisionStatus.Confirmed
            };
            string sql = @" UPDATE dbo.Revisions
                            SET State = @State
                            WHERE Id = @Id;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int UpdateRejectStatus(int revisionId)
        {

            Revision data = new Revision
            {
                Id = revisionId,
                Status = Enums.RevisionStatus.Rejected
            };
            string sql = @" UPDATE dbo.Revisions
                            SET State = @State
                            WHERE Id = @Id;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int DeleteRevisionById(int id)
        {
            Revision data = new Revision
            {
                Id = id
            };

            string sql = @" DELETE 
                            FROM dbo.Revisions 
                            WHERE Id = @Id;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int UpdateRevision(Revision revision)
        {
            string Sql = @"UPDATE dbo.Revisions 
                                SET ClientsEmailAddress = @ClientsEmailAddress, 
                                    EntityTitle = @EntityTitle, 
                                    OwnersEmailAddress = @OwnersEmailAddress, 
                                    Description = @Description, 
                                    ActionRating = @ActionRating, 
                                    OwnerInstructorRating = @OwnerInstructorRating, 
                                    Status = @Status
                                WHERE Id = @Id AND ConcurrencyToken = @ConcurrencyToken";

            var rowCount = -1;
            using (IDbConnection cnn = new SqlConnection(SSMSDataAccess.GettConnectionstring()))
            {
                rowCount = cnn.Execute(Sql, revision);
            }

            //if (rowCount == 0)
            //{
            //    throw new Exception("Oh no, someone else edited this record!");
            //}

            return rowCount;
        }

    }
}