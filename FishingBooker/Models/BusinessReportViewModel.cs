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


        public IEnumerable<ReservationToShowViewModel> active_reservations { get; set; }


        public IEnumerable<ReservationFromHistoryViewModel> history_reservations { get; set; }


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

        //[Display(Name = "Day of week")]
        //[Required(ErrorMessage = "You need to enter day of week.")]
        //public int DayOfWeek { get; set; }


        //[Display(Name = "Month")]
        //[Required(ErrorMessage = "You need to enter a month.")]
        //public int Month { get; set; }


        //[Display(Name = "Year")]
        //[Required(ErrorMessage = "You need to enter a year.")]
        //public int Year { get; set; }


        [Display(Name = "Income")]
        public double Active_Income { get; set; }


        [Display(Name = "Income")]
        public double History_Income { get; set; }
    }
}