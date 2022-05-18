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
                                          Enums.CancellationPolicyType cancellationPolicy,
                                          string instructorId)
        {
            Adventure data = new Adventure
            {
                Title = title,
                Address = address,
                PromotionDescription = promotion,
                Rating = 0,
                RatingSum = 0,
                RatingCount = 0,
                BehaviourRules = behaviour,
                AdditionalServices = additionalServices,
                Pricelist = pricelist,
                Price = price,
                MaxNumberOfPeople = maxNumberOfPeople,
                FishingEquipment = fishingEquipment,
                CancellationPolicy = cancellationPolicy,
                InstructorId = instructorId
            };

            string sql = @"INSERT INTO dbo.Adventures (Title, Address, PromotionDescription, Rating, RatingSum, RatingCount, BehaviourRules, AdditionalServices, Pricelist, Price, MaxNumberOfPeople, FishingEquipment, CancellationPolicy, InstructorId)
                           VALUES (@Title, @Address, @PromotionDescription, @Rating, @RatingSum, @RatingCount, @BehaviourRules, @AdditionalServices, @Pricelist, @Price, @MaxNumberOfPeople, @FishingEquipment, @CancellationPolicy, @InstructorId);";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int UpdateAdventure(int advId,
                                          string title,
                                          string address,
                                          string promotion,
                                          string behaviour,
                                          string additionalServices,
                                          string pricelist,
                                          decimal price,
                                          int maxNumberOfPeople,
                                          string fishingEquipment,
                                          Enums.CancellationPolicyType cancellationPolicy)
        {

            Adventure data = new Adventure
            {
                Id = advId,
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
            };

            string sql = @"UPDATE dbo.Adventures 
                           SET Title = @Title, Address = @Address, PromotionDescription = @PromotionDescription, BehaviourRules = @BehaviourRules, AdditionalServices = @AdditionalServices, Pricelist = @Pricelist, Price = @Price, MaxNumberOfPeople = @MaxNumberOfPeople, FishingEquipment = @FishingEquipment, CancellationPolicy = @CancellationPolicy
                           WHERE Id = @Id;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static int UpdateRating(int adventureId, float rating, int ratingSum, int ratingCount)
        {

            Adventure data = new Adventure
            {
                Id = adventureId,
                Rating = rating,
                RatingSum = ratingSum,
                RatingCount = ratingCount
            };
            string sql = @" UPDATE dbo.Adventures
                            SET Rating = @Rating, RatingSum = @RatingSum, RatingCount = @RatingCount
                            WHERE Id = @Id;";


            return SSMSDataAccess.SaveData(sql, data);
        }


        public static int DeleteAdventure(int adventureId)
        {
            Adventure data = new Adventure
            {
                Id = adventureId
            };

            string sql = @" DELETE 
                            FROM dbo.Adventures
                            WHERE Id = @Id;";

            return SSMSDataAccess.SaveData(sql, data);
        }

        public static List<Adventure> LoadAdventures()
        {
            string sql = @"SELECT *
                           FROM dbo.Adventures;";

            return SSMSDataAccess.LoadData<Adventure>(sql);
        }

        public static List<string> LoadAdventureTitlesByInstructor(string instructorId)
        {
            string sql = @"SELECT Title
                            FROM dbo.Adventures
                            WHERE InstructorId = @InstructorId;";
            return SSMSDataAccess.LoadAdventureTitlesByInstructor<string>(sql, instructorId);
        }
    }
}
