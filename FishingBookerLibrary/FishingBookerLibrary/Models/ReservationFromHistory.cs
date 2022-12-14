using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class ReservationFromHistory
    {
        public int Id { get; set; }
        public string ClientsEmailAddress { get; set; }
        public string ActionTitle { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Duration { get; set; }
        public decimal Price { get; set; }
        public string OwnerId { get; set; }
        public int ClientPercentage { get; set; }
        public int OwnerPercentage { get; set; }
        public int MoneyFlowPercentage { get; set; }
    }
}