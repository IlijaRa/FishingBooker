using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class ClientSearchEntitiesViewModel
    {
        public string entity { get; set; }


        [Display(Name = "From date*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        //[Required(ErrorMessage = "You need to enter a date.")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }


        [Display(Name = "From time*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss}")]
        //[Required(ErrorMessage = "You need to enter time.")]
        [DataType(DataType.Time)]
        public string FromTime { get; set; }


        [Display(Name = "To date*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        //[Required(ErrorMessage = "You need to enter a date.")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }


        [Display(Name = "To time*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss}")]
        //[Required(ErrorMessage = "You need to enter time.")]
        [DataType(DataType.Time)]
        public string ToTime { get; set; }


        [Range(1, 200, ErrorMessage = "Rate need to be between 1-200.")]
        [RegularExpression("([0-9][0-9]?)")]
        //[Required(ErrorMessage = "You need to enter number of quests.")]
        [Display(Name = "Number of people")]
        public int NumberOfPeople { get; set; }


        [Range(1, 10, ErrorMessage = "Rate need to be between 1-10.")]
        [RegularExpression("([0-9][0-9]?)")]
        [Display(Name = "Action rating")]
        //[Required(ErrorMessage = "You need to enter min rating that you want.")]
        public double Rating { get; set; }


        public IEnumerable<AdventureViewModel> adventures { get; set; } = new List<AdventureViewModel>();


        public IEnumerable<CottageViewModel> cottages { get; set; } = new List<CottageViewModel>();


        public IEnumerable<ShipViewModel> ships { get; set; } = new List<ShipViewModel>();
    }
}