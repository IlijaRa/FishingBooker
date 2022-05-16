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
    }
}
