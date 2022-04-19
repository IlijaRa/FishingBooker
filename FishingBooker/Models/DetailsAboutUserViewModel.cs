using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class DetailsAboutUserViewModel
    {
        public RegUserViewModel user { get; set; } = new RegUserViewModel();
        public List<AdventureViewModel> adventures { get; set; } = new List<AdventureViewModel>();
    }
}