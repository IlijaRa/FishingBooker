using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class AdventureFastReservationViewModel
    {
        public int Id { get; set; }


        [Display(Name = "Place*")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a place name between 2-50 characters.")]
        [Required(ErrorMessage = "You need to give us a place.")]
        public string Place { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "You need to enter a date.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss}")]
        [Required(ErrorMessage = "You need to enter time.")]
        [DataType(DataType.Time)]
        public string StartTime { get; set; }


        [RegularExpression("([0-9]+)")]
        [Required(ErrorMessage = "You need to give us a duration.")]
        public int Duration { get; set; }


        [RegularExpression("([0-9]+)")]
        [Display(Name = "Maximum number of people*")]
        [Required(ErrorMessage = "You need to enter a number.")]
        public int MaxNumberOfPeople { get; set; }


        [Display(Name = "Additional services")]
        [Required(ErrorMessage = "You need to enter additional services.")]
        public string AdditionalServices { get; set; }


        [Display(Name = "Price*")]
        [Required(ErrorMessage = "You need to enter a price.")]
        public decimal Price { get; set; }


        public bool isReserved { get; set; } = false;

        public int AdventureId { get; set; }
    }
}