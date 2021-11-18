using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary
{
    public class ShipOwner
    {
        public RegistrationTypeEnum RegType { get; set; }
        public string RegDescription { get; set; }
        public  RoleTypeEnum RoleType { get; set; }
    }
}
