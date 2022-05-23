using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public string Duration { get; set; }
        public DateTime ValidityPeriodDate { get; set; }
        public TimeSpan ValidityPeriodTime { get; set; }
        public int MaxNumberOfPeople { get; set; }
        public string AdditionalServices { get; set; }
        public decimal Price { get; set; }
        public double Discount { get; set; }
        public bool IsReserved { get; set; }
        public string ClientsEmailAddress { get; set; }
        public Enums.ReservationType ReservationType { get; set; }
    }
}
