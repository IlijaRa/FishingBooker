using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class ReservationFromHistoryViewModel : ReservationToShowViewModel
    {
        [Display(Name = "Client percentage")]
        public int ClientPercentage { get; set; }


        [Display(Name = "Owner percentage")]
        public int OwnerPercentage { get; set; }


        [Display(Name = "Money flow percentage")]
        public int MoneyFlowPercentage { get; set; }
    }
}