using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class DetailsAdventureViewModel : EditAdventureViewModel
    {
        [System.ComponentModel.DataAnnotations.Display(Name = "Images")]
        public List<Image> images { get; set; } = new List<Image>();
    }
}