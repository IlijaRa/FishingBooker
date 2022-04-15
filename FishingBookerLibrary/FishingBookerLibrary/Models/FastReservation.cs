using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class FastReservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public int Duration { get; set; }
        public int MaxNumberOfPeople { get; set; }
        public string AdditionalServices { get; set; }
        public decimal Price { get; set; }
        public double Discount { get; set; }
    }
}
