using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary
{
    public class Cottage
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string PromotionDescription { get; set; }
        public double Rating { get; set; }
        public CottageFastReservation FastReservation { get; set; }
        public string BehaviourRules { get; set; }
        public string AdditionalServices { get; set; }
        public string Pricelist { get; set; }
        public double Price { get; set; }
        public List<FastReservation> FastReservations { get; set; }
        public List<RegularReservation> RegularReservations { get; set; }
        public int NumberOfRooms { get; set; }
        public int BedsPerRoom { get; set; }

    }
}
