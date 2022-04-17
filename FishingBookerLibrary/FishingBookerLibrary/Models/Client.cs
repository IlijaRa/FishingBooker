using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class Client : RegUser
    {
        public Enums.ClientCategoryType ClientCategory { get; set; }
        public int NumberOfPenalties { get; set; } = 0;
        public List<Reservation> FastReservations { get; set; }
        public List<RegularReservation> RegularReservations { get; set; }
    }
}
