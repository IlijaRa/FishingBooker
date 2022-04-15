using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class Adventure : Entity
    {
        public AdventureFastReservation AdventureFastReservation { get; set; }
        public int MaxNumberOfPeople { get; set; }
        public string FishingEquipment { get; set; }
        public CancellationPolicyType /*string*/ CancellationPolicy { get; set; }
        public string InstructorId { get; set; }
    }
}