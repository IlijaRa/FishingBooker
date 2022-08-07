using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class DeactivationRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string EmailAddress { get; set; }
        public string Reason { get; set; }
        public Enums.DeactivationRequestStatus Status { get; set; }
        public byte[] ConcurrencyToken { get; set; }
    }
}
