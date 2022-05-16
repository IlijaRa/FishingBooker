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
    }
}
