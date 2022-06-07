using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class BusinessReportViewModel
    {
        public int AdventureId { get; set; }


        [Display(Name = "Average rate")]
        public float AverageRate { get; set; }


        public IEnumerable<ReservationToShowViewModel> current_reservations { get; set; }


        public IEnumerable<ReservationToShowViewModel> reservations { get; set; }


        //it is used to filter reservations and their income
        [Display(Name = "From date*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "You need to enter a date.")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }


        //it is used to filter reservations and their income
        [Display(Name = "To date*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "You need to enter a date.")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }


        [Display(Name = "Income")]
        public double Income { get; set; }
    }
}