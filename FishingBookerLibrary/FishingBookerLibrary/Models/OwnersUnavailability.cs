using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class OwnersUnavailability : InstructorAvailability
    {
        public string OwnerId { get; set; }
    }
}
