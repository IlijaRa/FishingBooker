using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class ShipReservation : Reservation
    {
        public string ShipName { get; set; }
        public int ShipId { get; set; }
        public string OwnerId { get; set; }
    }
}
