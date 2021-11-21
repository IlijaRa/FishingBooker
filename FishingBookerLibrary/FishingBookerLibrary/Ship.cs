using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary
{
    public class Ship : Entity
    {
        public ShipFastReservation FastReservation { get; set; }
        public ShipSpecification ShipSpecification { get; set; }
        public string NavigationEquipment { get; set; }
        public List<string> FishingEquipment { get; set; }
        public CancellationPolicyType CancelationPolicy { get; set; }
    }
}
