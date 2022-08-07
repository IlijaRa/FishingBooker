using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class ClientComplaint
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }
        public string OwnerEmailAddress { get; set; }
        public string ClientsEmailAddress { get; set; }
        public string ActionTitle { get; set; }
        public string Reason { get; set; }
        public Enums.ClientComplaintStatus Status { get; set; }
        public byte[] ConcurrencyToken { get; set; }
    }
}
