using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class Revision
    {
        /// <summary>
        /// Atributes OwnersName and OwnersSurname will be generated automatically when EntityTitle is entered, so Client doesn't need to fill those fields
        /// </summary>
        public int Id { get; set; }
        public string ClientsName { get; set; }
        public string ClientsSurname { get; set; }
        public string EntityTitle { get; set; }
        public string OwnersName { get; set; }
        public string OwnersSurname { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public bool Status { get; set; } = false;
    }
}
