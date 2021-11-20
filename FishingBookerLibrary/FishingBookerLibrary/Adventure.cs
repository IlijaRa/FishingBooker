using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary
{
    public class Adventure
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string PromotionDescription { get; set; }
        public double Rating { get; set; }
        public AdventureFastReservation AdventureFastReservation { get; set; }
        public string BehaviourRules { get; set; }
        public string AdditionalServices { get; set; }
        public string Pricelist { get; set; }
        public int MaxNumberOfPeople { get; set; }
        public List<string> FishingEquipment { get; set; }
        public CancellationPolicyType CancellationPolicy { get; set; }
    }
}
