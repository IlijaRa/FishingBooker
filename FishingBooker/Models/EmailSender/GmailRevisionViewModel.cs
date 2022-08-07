using FishingBooker.Models.EmailSender;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class GmailRevisionViewModel
    {
        public Gmail gmail { get; set; }
        public Revision revision { get; set; }
    }
}