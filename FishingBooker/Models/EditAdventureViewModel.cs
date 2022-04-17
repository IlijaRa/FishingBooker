using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class EditAdventureViewModel
    {
        public AdventureInstructorViewModel adventure { get; set; } = new AdventureInstructorViewModel();
        public IEnumerable<AdventureReservationViewModel> fast_reservations { get; set; } //= new List<AdventureFastReservationViewModel>();
    }
}