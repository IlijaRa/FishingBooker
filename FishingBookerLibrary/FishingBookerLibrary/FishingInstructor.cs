using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary
{
    public class FishingInstructor: RegUser
    {
        public RegistrationTypeEnum RegType { get; set; }
        public string RegDescription { get; set; }
        //UNDONE: public Adventure adventure { get; set; }
        public double Rating { get; set; }
        public string Biography { get; set; }
    }
}
