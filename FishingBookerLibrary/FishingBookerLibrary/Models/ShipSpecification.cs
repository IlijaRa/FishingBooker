using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class ShipSpecification
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public double Length { get; set; }
        public string EngineNumber { get; set; }
        public double EnginePower { get; set; }
        public double MaxSpeed { get; set; }
        public int PeopleCapacity { get; set; }
    }
}
