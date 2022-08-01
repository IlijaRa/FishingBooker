using FishingBookerLibrary.Models;
using System;

namespace FishingBooker.Models
{
    public class CalendarEvent
    {
        //id, text, start_date and end_date properties are mandatory
        public int id { get; set; }
        public Enums.CalendarEventType event_type { get; set; }
        public string text { get; set; }
        public string clients_email { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string color { get; set; }
        public string textColor { get; set; }
    }
}