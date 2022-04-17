using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class InstructorScheduleViewModel
    {
        public InstructorAvailabilityViewModel availability { get; set; } = new InstructorAvailabilityViewModel();
        public List<CurrentReservationViewModel> current_reservations { get; set; } = new List<CurrentReservationViewModel>();
        public List<ReservationFromHistoryViewModel> reservation_history { get; set; } = new List<ReservationFromHistoryViewModel>();
    }
}