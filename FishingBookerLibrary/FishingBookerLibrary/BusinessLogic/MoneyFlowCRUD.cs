using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.BusinessLogic
{
    public class MoneyFlowCRUD
    {
        public static MoneyFlow LoadMoneyFlow()
        {
            string sql = @"SELECT *
                           FROM dbo.MoneyFlow;";

            return SSMSDataAccess.LoadData<MoneyFlow>(sql).FirstOrDefault();
        }

        public static int UpdatePercentage(decimal percentage)
        {

            MoneyFlow data = new MoneyFlow
            {
                Id = 1,
                Percentage = percentage
            };
            string sql = @" UPDATE dbo.MoneyFlow
                            SET Percentage = @Percentage
                            WHERE Id = @Id;";


            return SSMSDataAccess.SaveData(sql, data);
        }
    }
}
