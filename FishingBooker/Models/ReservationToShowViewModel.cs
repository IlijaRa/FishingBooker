using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class ReservationToShowViewModel
    {
        public int Id { get; set; }


        [Display(Name = "Place*")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a place name between 2-50 characters.")]
        [Required(ErrorMessage = "You need to give us a place.")]
        public string Place { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address*")]
        [Required(ErrorMessage = "You need to give us your email address.")]
        public string ClientsEmailAddress { get; set; }


        [Display(Name = "Place*")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a place name between 2-50 characters.")]
        [Required(ErrorMessage = "You need to give us a place.")]
        public string ActionTitle { get; set; }


        [Display(Name = "Start date*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "You need to enter a date.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }


        [Display(Name = "Start time*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss}")]
        [Required(ErrorMessage = "You need to enter time.")]
        [DataType(DataType.Time)]
        public string StartTime { get; set; }


        [Display(Name = "End date*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "You need to enter a date.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }


        [Display(Name = "End time*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss}")]
        [Required(ErrorMessage = "You need to enter time.")]
        [DataType(DataType.Time)]
        public string EndTime { get; set; }


        //[RegularExpression("([0-9]+)")]
        //[Required(ErrorMessage = "You need to give us a duration.")]
        //public string Duration { get; set; }


        [Display(Name = "Valid until date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "You need to enter a date.")]
        [DataType(DataType.Date)]
        public DateTime ValidityPeriodDate { get; set; }


        [Display(Name = "Valid untiil time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss}")]
        [Required(ErrorMessage = "You need to enter time.")]
        [DataType(DataType.Time)]
        public string ValidityPeriodTime { get; set; }


        [Display(Name = "Full Price*")]
        [Required(ErrorMessage = "You need to enter a price.")]
        public decimal Price { get; set; }


        [Display(Name = "Scaled Price*")]
        [Required(ErrorMessage = "You need to enter a price.")]
        public double ScaledPrice { get; set; }


        [Display(Name = "Is reserved?")]
        public bool IsReserved { get; set; }


        public string OwnerId { get; set; }
    }
}