using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class DeactivationRequestViewModel
    {
        public string UserName { get; set; }

        public string UserSurname { get; set; }

        [Display(Name = "Email address*")]
        public string EmailAddress { get; set; }

        public string Reason { get; set; }
    }
}