using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class CottageReservation : Reservation
    {
        public string CottageName { get; set; }
        public int CottageId { get; set; }
        public string OwnerId { get; set; }
        
    }
}
