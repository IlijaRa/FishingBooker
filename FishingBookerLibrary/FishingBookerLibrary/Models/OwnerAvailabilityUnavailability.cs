using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class OwnerAvailabilityUnavailability
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public TimeSpan FromTime { get; set; }
        public DateTime ToDate { get; set; }
        public TimeSpan ToTime { get; set; }
        public string OwnerId { get; set; }
        public Enums.AvailabilityUnavailabilityType Type { get; set; }
        public string Text { get; set; }
    }
}
