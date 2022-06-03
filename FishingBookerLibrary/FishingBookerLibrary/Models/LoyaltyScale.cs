using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class LoyaltyScale
    {
        public int Id { get; set; }
        public int LoyaltyProgramId { get; set; }
        public string ScaleName { get; set; }
        public decimal ClientsBenefits { get; set; }
        public decimal OwnerBenefits { get; set; }
        public decimal MinEarnedPoints { get; set; }
    }
}
