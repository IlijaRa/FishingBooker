using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary
{
    public class FastReservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public int MaxNumberOfPeople { get; set; }
        public string AdditionalServices { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
    }
}
