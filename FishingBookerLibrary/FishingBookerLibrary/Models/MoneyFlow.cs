using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.Models
{
    public class MoneyFlow
    {
        public int Id { get; set; } = 1;
        public int Percentage { get; set; }
        public decimal TotalSum { get; set; }
    }
}
