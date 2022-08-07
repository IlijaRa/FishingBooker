using FishingBooker.Models.EmailSender;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class GmailDeactivationRequestViewModel
    {
        public Gmail gmail { get; set; }
        public DeactivationRequest deactivation_request { get; set; }

    }
}