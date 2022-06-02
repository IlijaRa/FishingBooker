using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FishingBooker.Models.IndexViewModels
{
    public class ClientIndexViewModel
    {
        public IEnumerable<ReservationToShowViewModel> future_reservations { get; set; }
        public IEnumerable<ReservationFromHistoryViewModel> history_reservations { get; set; }
    }
}