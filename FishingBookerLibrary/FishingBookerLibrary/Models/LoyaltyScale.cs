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
        public float ClientsBenefits { get; set; }
        public float OwnerBenefits { get; set; }
        public float MinEarnedPoints { get; set; }
    }
}
