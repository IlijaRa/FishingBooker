using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class LoyaltyProgramViewModel
    {
        public int Id { get; set; }


        [RegularExpression("([0-9]+)")]
        [Display(Name = "Points after successful reservation(Client)*")]
        [Required(ErrorMessage = "You need to enter a number.")]
        public int PointsAfterSuccResClient { get; set; }


        [RegularExpression("([0-9]+)")]
        [Display(Name = "Points after successful reservation(Owner)*")]
        [Required(ErrorMessage = "You need to enter a number.")]
        public int PointsAfterSuccResOwner { get; set; }


        public List<LoyaltyScaleViewModel> scales { get; set; } = new List<LoyaltyScaleViewModel>();
    }
}