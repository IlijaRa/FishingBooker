using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models.IndexViewModels
{
    public class ClientIndexViewModel
    {
        public IEnumerable<ReservationToShowViewModel> future_reservations { get; set; }


        [Display(Name = "Future outcomes")]
        public double FutureOutcomes { get; set; }

        public IEnumerable<ReservationFromHistoryViewModel> history_reservations { get; set; }


        [Display(Name = "History outcomes")]
        public double HistoryOutcomes { get; set; }
    }
}