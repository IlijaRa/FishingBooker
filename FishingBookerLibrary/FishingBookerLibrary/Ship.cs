using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary
{
    public class Ship
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string PromotionDescription { get; set; }
        public double Rating { get; set; }
        public ShipFastReservation FastReservation { get; set; }
        public string BehaviourRules { get; set; }
        public string AdditionalServices { get; set; }
        public string Pricelist { get; set; }
        public ShipSpecification ShipSpecification { get; set; }
        public string NavigationEquipment { get; set; }
        public List<string> FishingEquipment { get; set; }
        public CancellationPolicyType CancelationPolicy { get; set; }
    }
}
