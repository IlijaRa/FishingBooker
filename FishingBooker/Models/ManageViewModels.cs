using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace FishingBooker.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }

        [Display(Name = "Percentage for a system(%)")]
        [Range(1, 100, ErrorMessage = "Percentage must be between 1 and 100!")]
        public decimal Percentage { get; set; }// koristi samo admin
                                                // 
        [Display(Name = "Total income")]
        public decimal TotalIncome { get; set; }// koristi samo admin  

        [Display(Name = "Loyalty class member")]
        public string LoyaltyClassMember { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeBasicInfoViewModel
    {
        [Display(Name = "First name*")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a name between 2-50 characters.")]
        [Required(ErrorMessage = "You need to give us your first name.")]
        public string Name { get; set; }


        [Display(Name = "Last name*")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a surname between 2-50 characters.")]
        [Required(ErrorMessage = "You need to give us your last name.")]
        public string Surname { get; set; }


        [Display(Name = "Phone number*")]
        public string PhoneNumber { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address*")]
        [Required(ErrorMessage = "You need to give us your email address.")]
        public string EmailAddress { get; set; }


        [Display(Name = "Address*")]
        [Required(ErrorMessage = "You need to enter your address.")]
        public string Address { get; set; }


        [Display(Name = "City*")]
        [Required(ErrorMessage = "You need to enter city.")]
        public string City { get; set; }


        [Display(Name = "Country*")]
        [Required(ErrorMessage = "You need to enter country.")]
        public string Country { get; set; }

    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}