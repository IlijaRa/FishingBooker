using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class Entity
    {
        /// <summary>
        /// Entity can be anything in between Cottage, Ship and Adventure
        /// </summary>
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string PromotionDescription { get; set; }
        public float Rating { get; set; }
        public float RatingSum { get; set; }
        public float RatingCount { get; set; }
        public string BehaviourRules { get; set; }
        public string AdditionalServices { get; set; }
        public string Pricelist { get; set; }
        public decimal Price { get; set; }
        //public List<FastReservation> FastReservations { get; set; }
        public List<RegularReservation> RegularReservations { get; set; }
    }
}
