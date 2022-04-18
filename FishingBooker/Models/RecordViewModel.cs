using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class RecordViewModel
    {
        public int Id { get; set; }


        [Display(Name = "Clients email address")]
        public string ClientsEmailAddress { get; set; }


        [Display(Name = "Your email address")]
        public string InstructorsEmailAddress { get; set; }


        [Display(Name = "Leave a comment")]
        public string Comment { get; set; }


        [Display(Name = "Choose your impression")]
        public Enums.RecordImpressionType ImpressionType { get; set; }


        public string ClientId { get; set; }


        public string InstructorId { get; set; }
    }
}