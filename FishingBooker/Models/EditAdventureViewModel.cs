using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class EditAdventureViewModel
    {
        [Required(ErrorMessage = "You need to import an image.")]
        public Image image { get; set; } = new Image();
        public AdventureInstructorViewModel adventure { get; set; } = new AdventureInstructorViewModel();
        public IEnumerable<AdventureReservationViewModel> fast_reservations { get; set; } //= new List<AdventureFastReservationViewModel>();
        
        [Display(Name = "Images")]
        public List<Image> images { get; set; } = new List<Image>();
    }
}