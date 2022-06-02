using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.BusinessLogic
{
    public class CottageCRUD
    {
        public static List<Cottage> LoadCottages()
        {
            string sql = @"SELECT *
                           FROM dbo.Cottages;";

            return SSMSDataAccess.LoadData<Cottage>(sql);
        }

        public static List<string> LoadCottageTitlesByOwner(string ownerId)
        {
            string sql = @"SELECT Title
                            FROM dbo.Cottages
                            WHERE OwnerId = @OwnerId;";

            return SSMSDataAccess.LoadCottageTitlesByOwner<string>(sql, ownerId);
        }

        public static int UpdateRating(int cottageId, float rating, int ratingSum, int ratingCount)
        {

            Cottage data = new Cottage
            {
                Id = cottageId,
                Rating = rating,
                RatingSum = ratingSum,
                RatingCount = ratingCount
            };
            string sql = @" UPDATE dbo.Cottages
                            SET Rating = @Rating, RatingSum = @RatingSum, RatingCount = @RatingCount
                            WHERE Id = @Id;";


            return SSMSDataAccess.SaveData(sql, data);
        }

        public static Cottage LoadCottagesByName(string cottageTitle)
        {
            string sql = @"SELECT *
                            FROM dbo.Cottages
                            WHERE Title = @Title;";
            return SSMSDataAccess.LoadCottagesByName<Cottage>(sql, cottageTitle);
        }

    }
}
