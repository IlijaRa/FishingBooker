using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class AdventureReservation : Reservation 
    {
        public int AdventureId { get; set; }
        public string Place { get; set; }
    }
}
