using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class RegularReservation
    {
        public int Id { get; set; }
        public string ClientsEmailAddress { get; set; }
        public string EntityTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfGuests { get; set; }
        public string AddedAdditionalServices { get; set; }
    }
}
