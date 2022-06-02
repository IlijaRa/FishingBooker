using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.BusinessLogic
{
    public class ShipCRUD
    {
        public static List<Ship> LoadShips()
        {
            string sql = @"SELECT *
                           FROM dbo.Ships;";

            return SSMSDataAccess.LoadData<Ship>(sql);
        }

        public static List<string> LoadShipTitlesByOwner(string ownerId)
        {
            string sql = @"SELECT Title
                            FROM dbo.Ships
                            WHERE OwnerId = @OwnerId;";

            return SSMSDataAccess.LoadShipTitlesByOwner<string>(sql, ownerId);
        }

        public static int UpdateRating(int shipId, float rating, int ratingSum, int ratingCount)
        {

            Ship data = new Ship
            {
                Id = shipId,
                Rating = rating,
                RatingSum = ratingSum,
                RatingCount = ratingCount
            };

            string sql = @" UPDATE dbo.Ships
                            SET Rating = @Rating, RatingSum = @RatingSum, RatingCount = @RatingCount
                            WHERE Id = @Id;";


            return SSMSDataAccess.SaveData(sql, data);
        }

        public static Ship LoadShipsByName(string shipTitle)
        {
            string sql = @"SELECT *
                            FROM dbo.Ships
                            WHERE Title = @Title;";
            return SSMSDataAccess.LoadShipsByName<Ship>(sql, shipTitle);
        }

    }
}
