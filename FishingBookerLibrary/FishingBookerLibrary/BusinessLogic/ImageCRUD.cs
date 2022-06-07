using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FishingBookerLibrary.BusinessLogic
{
    public class ImageCRUD
    {

        public static int SubmitImage(byte[] image, int advId)
        {
            Image data = new Image
            {
                image = image,
                AdventureId = advId
            };

            string sql = @"INSERT INTO dbo.Images (Image, AdventureId)
                           VALUES (@Image, @AdventureId);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static List<Image> LoadImagesByAdventureId(int adventureId)
        {
            string sql = @"SELECT *
                            FROM dbo.Images
                            WHERE AdventureId = @AdventureId;";
            return SSMSDataAccess.LoadImagesByAdventureId<Image>(sql, adventureId);
        }


    }
}
