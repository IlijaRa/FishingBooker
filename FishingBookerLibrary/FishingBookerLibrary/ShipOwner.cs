using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary
{
    public class ShipOwner : RegUser
    {
        public RegistrationType RegType { get; set; }
        public string RegDescription { get; set; }
        public  RoleType RoleType { get; set; }
    }
}
