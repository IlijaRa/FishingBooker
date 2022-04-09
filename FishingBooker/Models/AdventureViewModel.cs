using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class AdventureViewModel
    {
        public int AdventureId { get; set; }


        [Display(Name = "Title*")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "You need to provide a name between 2-100 characters.")]
        [Required(ErrorMessage = "You need to give us your title.")]
        public string Title { get; set; }


        [Display(Name = "Address*")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "You need to provide a surname between 2-100 characters.")]
        [Required(ErrorMessage = "You need to give us your address.")]
        public string Address { get; set; }


        [Display(Name = "Promotion description")]
        public string PromotionDescription { get; set; }


        [Display(Name = "Behaviour rules")]
        public string BehaviourRules { get; set; }


        [Display(Name = "Additional services")]
        public string AdditionalServices { get; set; }


        [Display(Name = "Pricelist*")]
        [Required(ErrorMessage = "You need to fill a pricelist.")]
        public string Pricelist { get; set; }


        [Display(Name = "Price*")]
        [Required(ErrorMessage = "You need to enter a price.")]
        public decimal Price { get; set; }


        [Display(Name = "Maximum number of people*")]
        [Required(ErrorMessage = "You need to enter a number.")]
        public int MaxNumberOfPeople { get; set; }


        [Display(Name = "Available equipment")]
        public string FishingEquipment { get; set; }


        [Display(Name = "Cancellation policy")]
        public string CancellationPolicy { get; set; }
    }
}