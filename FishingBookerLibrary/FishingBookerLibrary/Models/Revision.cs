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
        public string ClientsEmailAddress { get; set; }
        public string EntityTitle { get; set; }
        public string OwnersEmailAddress { get; set; }
        public string Description { get; set; }
        public int ActionRating { get; set; }
        public int OwnerInstructorRating { get; set; }
        public bool State { get; set; }
    }
}
