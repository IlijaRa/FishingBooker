using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class ShipOwner : RegUser
    {
        public Enums.RegistrationType RegType { get; set; }
        public string RegDescription { get; set; }
        public Enums.RoleType RoleType { get; set; }
    }
}
