using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string ClientsEmailAddress { get; set; }
        public string InstructorsEmailAddress { get; set; }
        public string Comment { get; set; }
        public Enums.RecordImpressionType ImpressionType { get; set; }
        public string ClientId { get; set; }
        public string InstructorId { get; set; }
    }
}
