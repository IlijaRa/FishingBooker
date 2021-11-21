using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary
{
    public class Adventure : Entity
    {
        public AdventureFastReservation AdventureFastReservation { get; set; }
        public int MaxNumberOfPeople { get; set; }
        public List<string> FishingEquipment { get; set; }
        public CancellationPolicyType CancellationPolicy { get; set; }
    }
}
