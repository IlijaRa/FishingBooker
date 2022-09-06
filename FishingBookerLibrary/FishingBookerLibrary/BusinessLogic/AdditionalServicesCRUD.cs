using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.BusinessLogic
{
    public class AdditionalServicesCRUD
    {
        public static List<AdditionalService> LoadAdditionalServices()
        {
            string sql = @"SELECT *
                           FROM dbo.AdditionalServices;";

            return SSMSDataAccess.LoadData<AdditionalService>(sql);
        }
    }
}
