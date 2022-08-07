using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class RevisionViewModel
    {
        public int Id { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Clients email address*")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a name between 2-50 characters.")]
        [Required(ErrorMessage = "You need to give us email address.")]
        public string ClientsEmailAddress { get; set; }

        
        [Display(Name = "Title*")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "You need to provide a title between 2-100 characters.")]
        [Required(ErrorMessage = "You need to give us your title.")]
        public string EntityTitle { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Owner email address*")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a name between 2-50 characters.")]
        [Required(ErrorMessage = "You need to give us email address.")]
        public string OwnersEmailAddress { get; set; }


        [Display(Name = "Description")]
        [Required(ErrorMessage = "You need to fill a description.")]
        public string Description { get; set; }


        [Range(1, 10, ErrorMessage = "Rate need to be between 1-10.")]
        [RegularExpression("([0-9][0-9]?)")]
        [Display(Name = "Action rating")]
        [Required(ErrorMessage = "You need to rate.")]
        public float ActionRating { get; set; }


        [Range(1, 10, ErrorMessage = "Rate need to be between 1-10.")]
        [RegularExpression("([0-9][0-9]?)")]
        [Display(Name = "Owner/instructor rating")]
        [Required(ErrorMessage = "You need to rate.")]
        public float OwnerInstructorRating { get; set; }


        public Enums.RevisionStatus Status { get; set; }


        public byte[] ConcurrencyToken { get; set; }
    }
}