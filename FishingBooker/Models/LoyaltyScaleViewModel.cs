using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class LoyaltyScaleViewModel
    {
        public int Id { get; set; }
        public int LoyaltyProgramId { get; set; }


        [Display(Name = "Scale name*")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "You need to provide a name between 2-100 characters.")]
        [Required(ErrorMessage = "You need enter scale name.")]
        public string ScaleName { get; set; }


        [Display(Name = "Clients benefits for this scale(%)")]
        [Range(0, 100, ErrorMessage = "Percentage must be between 1 and 100!")]
        public int ClientsBenefits { get; set; }


        [Display(Name = "Owners benefits for this scale(%)")]
        [Range(0, 100, ErrorMessage = "Percentage must be between 1 and 100!")]
        public int OwnerBenefits { get; set; }


        [Display(Name = "Minumum earned points for a scale")]
        [Range(0, 999999, ErrorMessage = "Range is between 0 and 999999")]
        [Required(ErrorMessage = "You need to enter a number.")]
        public int MinEarnedPoints { get; set; }


        [Display(Name = "Picked color")]
        [Required(ErrorMessage = "You need to enter a number.")]
        public string PickedColor { get; set; }
    }
}