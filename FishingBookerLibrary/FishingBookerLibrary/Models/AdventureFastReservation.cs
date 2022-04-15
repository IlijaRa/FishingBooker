using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class AdventureFastReservation : FastReservation 
    {
        public int AdventureId { get; set; }
        public string Place { get; set; }
    }
}
