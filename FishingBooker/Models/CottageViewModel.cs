using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class CottageViewModel
    {
        public int CottageId { get; set; }


        [Display(Name = "Title*")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "You need to provide a name between 2-100 characters.")]
        [Required(ErrorMessage = "You need to give us your title.")]
        public string Title { get; set; }


        [Display(Name = "Street*")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "You need to provide a street name between 2-100 characters.")]
        [Required(ErrorMessage = "You need to give us your street name.")]
        public string Street { get; set; }


        [Display(Name = "Address number*")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "You need to provide an address number between 1-10 characters.")]
        [Required(ErrorMessage = "You need to give us your address number.")]
        public string AddressNumber { get; set; }


        [Display(Name = "City*")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a city name between 2-50 characters.")]
        [Required(ErrorMessage = "You need to give us your city.")]
        public string City { get; set; }


        [Display(Name = "Promotion description")]
        public string PromotionDescription { get; set; }


        [Display(Name = "Behaviour rules")]
        public string BehaviourRules { get; set; }


        [Display(Name = "Additional services")]
        public string AdditionalServices { get; set; }


        [Display(Name = "Pricelist*")]
        [Required(ErrorMessage = "You need to fill a pricelist.")]
        public string Pricelist { get; set; }


        [RegularExpression("([0-9]+)")]
        [Display(Name = "Number of rooms*")]
        [Required(ErrorMessage = "You need to enter a number.")]
        public int NumberOfRooms { get; set; }


        [RegularExpression("([0-9]+)")]
        [Display(Name = "Number of beds per room*")]
        [Required(ErrorMessage = "You need to enter a number.")]
        public int BedsPerRoom { get; set; }

    }
}