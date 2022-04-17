using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class InstructorAvailabilityViewModel
    {
        public int Id { get; set; }


        [Display(Name = "From date*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "You need to enter a date.")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }


        [Display(Name = "From time*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss}")]
        [Required(ErrorMessage = "You need to enter time.")]
        [DataType(DataType.Time)]
        public string FromTime { get; set; }


        [Display(Name = "To date*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "You need to enter a date.")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }


        [Display(Name = "To time*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss}")]
        [Required(ErrorMessage = "You need to enter time.")]
        [DataType(DataType.Time)]
        public string ToTime { get; set; }


        public string InstructorId { get; set; }
    }
}