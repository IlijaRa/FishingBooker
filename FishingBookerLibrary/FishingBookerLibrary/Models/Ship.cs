using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class Ship : Entity
    {
        public ShipReservation FastReservation { get; set; }
        public ShipSpecification ShipSpecification { get; set; }
        public string NavigationEquipment { get; set; }
        public string FishingEquipment { get; set; }
        public Enums.CancellationPolicyType CancelationPolicy { get; set; }
        public int SpecificationId { get; set; }
        public string OwnerId { get; set; }
    }
}
