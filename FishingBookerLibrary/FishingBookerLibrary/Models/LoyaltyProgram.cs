using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class LoyaltyProgram
    {
        public int Id { get; set; }
        public int PointsAfterSuccResClient { get; set; }
        public int PointsAfterSuccResOwner { get; set; }
        public List<LoyaltyScale> scales { get; set; } = new List<LoyaltyScale>();
    }
}
