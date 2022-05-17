using FishingBooker.Models.EmailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FishingBooker.Models
{
    public class AnswerToComplaintViewModel
    {
        public int complaintId { get; set; }
        public Gmail client_gmail { get; set; } = new Gmail();
        public Gmail owner_gmail { get; set; } = new Gmail();
    }
}