using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.BusinessLogic
{
    public class AdventureCRUD
    {
        public static int CreateAdventure(string title,
                                          string address,
                                          string promotion,
                                          string behaviour,
                                          string additionalServices,
                                          string pricelist,
                                          decimal price,
                                          int maxNumberOfPeople,
                                          string fishingEquipment,
                                          string cancellationPolicy,
                                          string instructorId)
        {
            Adventure data = new Adventure
            {
                Title = title,
                Address = address,
                PromotionDescription = promotion,
                BehaviourRules = behaviour,
                AdditionalServices = additionalServices,
                Pricelist = pricelist,
                Price = price,
                MaxNumberOfPeople = maxNumberOfPeople,
                FishingEquipment = fishingEquipment,
                CancellationPolicy = cancellationPolicy,
                InstructorId = instructorId
            };

            string sql = @"INSERT INTO dbo.Adventures (Title, Address, PromotionDescription, BehaviourRules, AdditionalServices, Pricelist, Price, MaxNumberOfPeople, FishingEquipment, CancellationPolicy, InstructorId)
                           VALUES (@Title, @Address, @PromotionDescription, @BehaviourRules, @AdditionalServices, @Pricelist, @Price, @MaxNumberOfPeople, @FishingEquipment, @CancellationPolicy, @InstructorId);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static List<Adventure> LoadAdventures()
        {
            string sql = @"SELECT *
                           FROM dbo.Adventures;";

            return SSMSDataAccess.LoadData<Adventure>(sql);
        }
    }
}
