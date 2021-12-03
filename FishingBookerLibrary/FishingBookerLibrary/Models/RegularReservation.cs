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
        //UNDONE: public int NumberOfDays { get; set; } ne znam da li je potrebno za rezervisanje instruktora pecanja, tako da treba da razmotrimo
        public int NumberOfGuests { get; set; }
        public List<string> AddedAdditionalServices { get; set; }
    }
}
