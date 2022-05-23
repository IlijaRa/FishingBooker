using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using DHTMLX.Scheduler;
using DHTMLX.Common;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;

using FishingBooker.Models;
using Microsoft.AspNet.Identity;
using FishingBookerLibrary.BusinessLogic;

namespace FishingBooker.Controllers
{
    public class CalendarController : Controller
    {
        public ActionResult Index()
        {
            //Being initialized in that way, scheduler will use CalendarController.Data as a the datasource and CalendarController.Save to process changes
            var scheduler = new DHXScheduler(this);

            /*
             * It's possible to use different actions of the current controller
             *      var scheduler = new DHXScheduler(this);     
             *      scheduler.DataAction = "ActionName1";
             *      scheduler.SaveAction = "ActionName2";
             * 
             * Or to specify full paths
             *      var scheduler = new DHXScheduler();
             *      scheduler.DataAction = Url.Action("Data", "Calendar");
             *      scheduler.SaveAction = Url.Action("Save", "Calendar");
             */

            /*
             * The default codebase folder is ~/Scripts/dhtmlxScheduler. It can be overriden:
             *      scheduler.Codebase = Url.Content("~/customCodebaseFolder");
             */
            
 
            scheduler.InitialDate = new DateTime(2022, 09, 03);

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }

        public ContentResult Data()
        {
            var scheduler_data = new SchedulerAjaxData();
            List<CalendarEvent> calendar_events = new List<CalendarEvent>();
            var current_data = ReservationCRUD.LoadReservedAdventureReservationByInstructorId(User.Identity.GetUserId(), true);
            foreach (var reservation in current_data)
            {
                int day = 0;
                int month = 0;
                int year = 0;
                DateTime endDate = new DateTime();
                string[] duration = reservation.Duration.Split(',');

                if (duration[1].ToLower().Contains("days"))
                {
                    day = reservation.StartDate.Day + Convert.ToInt32(duration[0]);
                    month = reservation.StartDate.Month;
                    year = reservation.StartDate.Year;
                    endDate = new DateTime(year, month, day, Convert.ToInt32(reservation.StartTime.Hours), 
                                                             Convert.ToInt32(reservation.StartTime.Minutes), 
                                                             Convert.ToInt32(reservation.StartTime.Seconds));
                }
                else if(Convert.ToInt32(reservation.StartTime.Hours) + Convert.ToInt32(duration[0]) > 24)
                    {
                    day = reservation.StartDate.Day + 1;
                    month = reservation.StartDate.Month;
                    year = reservation.StartDate.Year;
                    endDate = new DateTime(year, month, day, Convert.ToInt32(reservation.StartTime.Hours),
                                                             Convert.ToInt32(reservation.StartTime.Minutes),
                                                             Convert.ToInt32(reservation.StartTime.Seconds));
                }
                else
                {
                    int hours = Convert.ToInt32(reservation.StartTime.Hours) + Convert.ToInt32(duration[0]);
                    int minutes = Convert.ToInt32(reservation.StartTime.Minutes);
                    int seconds = Convert.ToInt32(reservation.StartTime.Seconds);
                    endDate = new DateTime(reservation.StartDate.Year, reservation.StartDate.Month, reservation.StartDate.Day, hours, minutes, seconds);
                }
                calendar_events.Add(new CalendarEvent
                {
                    text = reservation.Place + " " + reservation.ClientsEmailAddress + " " + reservation.Price.ToString(),
                    start_date = reservation.StartDate,
                    end_date = endDate,
                });
            }

            scheduler_data.Add(calendar_events);

            return (ContentResult)scheduler_data;
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            
            try
            {
                var changedEvent = (CalendarEvent)DHXEventsHelper.Bind(typeof(CalendarEvent), actionValues);

     

                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        //do insert
                        //action.TargetId = changedEvent.id;//assign postoperational id
                        break;
                    case DataActionTypes.Delete:
                        //do delete
                        break;
                    default:// "update"                          
                        //do update
                        break;
                }
            }
            catch
            {
                action.Type = DataActionTypes.Error;
            }
            return (ContentResult)new AjaxSaveResponse(action);
        }
    }
}

