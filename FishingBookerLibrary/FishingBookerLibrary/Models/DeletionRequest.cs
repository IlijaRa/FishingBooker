using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class DeletionRequest
    {
        /// <summary>
        /// Request for account deletion
        /// </summary>
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Description { get; set; }
    }
}
