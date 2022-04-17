using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class FishingInstructor: RegUser
    {
        public Enums.RegistrationType RegType { get; set; }
        public string RegDescription { get; set; }
        public Adventure adventure { get; set; }
        public double Rating { get; set; }
        //public string Biography { get; set; }
    }
}
