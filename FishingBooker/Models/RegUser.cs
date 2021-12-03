using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class RegUser
    {
        public int UserId { get; set; }


        [Display(Name = "First name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a name between 2-50 characters.")]
        [Required(ErrorMessage = "You need to give us your first name.")]
        public string Name { get; set; }


        [Display(Name = "Last name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a surname between 2-50 characters.")]
        [Required(ErrorMessage = "You need to give us your last name.")]
        public string Surname { get; set; }


        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "You need to give us your email address.")]
        public string EmailAddress { get; set; }


        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "You need to provide a password between 8-50 characters.")]
        [Required(ErrorMessage = "Password is essencial.")]
        public string Password { get; set; }


        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Your confirmation does not match.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Address")]
        public string Address { get; set; }


        [Display(Name = "City")]
        public string City { get; set; }


        [Display(Name = "Country")]
        public string Country { get; set; }
    }
}