using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FishingBooker.Models
{
    public class ClientComplaintViewModel
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }

        [Display(Name = "Owner name*")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a name between 2-50 characters.")]
        [Required(ErrorMessage = "You need to give us first name.")]
        public string OwnerName { get; set; }


        [Display(Name = "Owner surname*")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a surname between 2-50 characters.")]
        [Required(ErrorMessage = "You need to give us last name.")]
        public string OwnerSurname { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Owner address*")]
        [Required(ErrorMessage = "You need to give us email address.")]
        public string OwnerEmailAddress { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Client address*")]
        [Required(ErrorMessage = "You need to give us email address.")]
        public string ClientsEmailAddress { get; set; }


        //[Display(Name = "Action title*")]
        //[StringLength(100, MinimumLength = 2, ErrorMessage = "You need to provide a name between 2-100 characters.")]
        //[Required(ErrorMessage = "You need to give us a title.")]
        //public IEnumerable<SelectListItem> ActionTitles { get; set; }

        [Display(Name = "Action title*")]
        [Required(ErrorMessage = "You need to give us a title.")]
        public string SelectedActionTitle { get; set; }

        //public List<SelectListItem> allowed_titles_to_select { get; set; } = new List<SelectListItem>();

        [Display(Name = "Reason*")]
        [Required(ErrorMessage = "You need to give us a reason.")]
        public string Reason { get; set; }
    }
}