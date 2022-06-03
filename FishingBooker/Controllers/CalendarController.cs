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
using FishingBookerLibrary.Models;

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
            
 
            scheduler.InitialDate = DateTime.Now;

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }

        public ContentResult Data()
        {
            var scheduler_data = new SchedulerAjaxData();
            List<CalendarEvent> calendar_events = new List<CalendarEvent>();
            if (User.IsInRole("ValidFishingInstructor"))
            {
                var instructor_reservations = ReservationCRUD.LoadAdventureReservationByInstructorId(User.Identity.GetUserId());
                var history_reservations = ReservationCRUD.LoadReservationsFromHistoryByOwnerId(User.Identity.GetUserId());
                var instructors_availability = ScheduleCRUD.LoadInstructorAvailability(User.Identity.GetUserId());
                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();

                foreach (var reservation in instructor_reservations)
                {

                    startDate = new DateTime(reservation.StartDate.Year,
                                                 reservation.StartDate.Month,
                                                 reservation.StartDate.Day,
                                                 reservation.StartTime.Hours,
                                                 reservation.StartTime.Minutes,
                                                 reservation.StartTime.Seconds);

                    endDate = new DateTime(reservation.EndDate.Year,
                                           reservation.EndDate.Month,
                                           reservation.EndDate.Day,
                                           reservation.EndTime.Hours,
                                           reservation.EndTime.Minutes,
                                           reservation.EndTime.Seconds);

                    calendar_events.Add(new CalendarEvent
                    {
                        text = reservation.Place + " " + reservation.ClientsEmailAddress + " " + reservation.Price.ToString(),
                        start_date = startDate,
                        end_date = endDate,
                    });
                }

                foreach (var reservation in history_reservations)
                {

                    startDate = new DateTime(reservation.StartDate.Year,
                                                 reservation.StartDate.Month,
                                                 reservation.StartDate.Day,
                                                 reservation.StartTime.Hours,
                                                 reservation.StartTime.Minutes,
                                                 reservation.StartTime.Seconds);

                    endDate = new DateTime(reservation.EndDate.Year,
                                           reservation.EndDate.Month,
                                           reservation.EndDate.Day,
                                           reservation.EndTime.Hours,
                                           reservation.EndTime.Minutes,
                                           reservation.EndTime.Seconds);

                    if (reservation.ClientsEmailAddress != null)
                    {
                        calendar_events.Add(new CalendarEvent
                        {
                            text = reservation.ActionTitle + " " + reservation.ClientsEmailAddress + " " + reservation.Price.ToString(),
                            start_date = startDate,
                            end_date = endDate,
                        });
                    }
                    else
                    {
                        calendar_events.Add(new CalendarEvent
                        {
                            text = "Not reserved yet",
                            start_date = startDate,
                            end_date = endDate,
                        });
                    }
                }

                calendar_events.Add(new CalendarEvent
                {
                    text = "Not available",
                    start_date = instructors_availability.ToDate,
                    end_date = instructors_availability.FromDate
                });

                scheduler_data.Add(calendar_events);
            }

            else if (User.IsInRole("ValidCottageOwner"))
            {
                var cottage_owner_reservations = ReservationCRUD.LoadCottageReservationByOwnerId(User.Identity.GetUserId());
                var history_reservations = ReservationCRUD.LoadReservationsFromHistoryByOwnerId(User.Identity.GetUserId());
                var cottage_owner_availability = ScheduleCRUD.LoadInstructorAvailability(User.Identity.GetUserId());
                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();

                foreach (var reservation in cottage_owner_reservations)
                {

                    startDate = new DateTime(reservation.StartDate.Year,
                                                 reservation.StartDate.Month,
                                                 reservation.StartDate.Day,
                                                 reservation.StartTime.Hours,
                                                 reservation.StartTime.Minutes,
                                                 reservation.StartTime.Seconds);

                    endDate = new DateTime(reservation.EndDate.Year,
                                           reservation.EndDate.Month,
                                           reservation.EndDate.Day,
                                           reservation.EndTime.Hours,
                                           reservation.EndTime.Minutes,
                                           reservation.EndTime.Seconds);

                    calendar_events.Add(new CalendarEvent
                    {
                        text = reservation.CottageName + " " + reservation.ClientsEmailAddress + " " + reservation.Price.ToString(),
                        start_date = startDate,
                        end_date = endDate,
                    });
                }

                foreach (var reservation in history_reservations)
                {

                    startDate = new DateTime(reservation.StartDate.Year,
                                                 reservation.StartDate.Month,
                                                 reservation.StartDate.Day,
                                                 reservation.StartTime.Hours,
                                                 reservation.StartTime.Minutes,
                                                 reservation.StartTime.Seconds);

                    endDate = new DateTime(reservation.EndDate.Year,
                                           reservation.EndDate.Month,
                                           reservation.EndDate.Day,
                                           reservation.EndTime.Hours,
                                           reservation.EndTime.Minutes,
                                           reservation.EndTime.Seconds);

                    if (reservation.ClientsEmailAddress != null)
                    {
                        calendar_events.Add(new CalendarEvent
                        {
                            text = reservation.ActionTitle + " " + reservation.ClientsEmailAddress + " " + reservation.Price.ToString(),
                            start_date = startDate,
                            end_date = endDate,
                        });
                    }
                    else
                    {
                        calendar_events.Add(new CalendarEvent
                        {
                            text = "Not reserved yet",
                            start_date = startDate,
                            end_date = endDate,
                        });
                    }
                }

                calendar_events.Add(new CalendarEvent
                {
                    text = "Not available",
                    start_date = cottage_owner_availability.ToDate.AddDays(1),
                    end_date = cottage_owner_availability.FromDate.AddYears(1)
                });

                scheduler_data.Add(calendar_events);
            }

            else if (User.IsInRole("ValidShipOwner"))
            {
                var ship_owner_reservations = ReservationCRUD.LoadShipReservationByOwnerId(User.Identity.GetUserId());
                var history_reservations = ReservationCRUD.LoadReservationsFromHistoryByOwnerId(User.Identity.GetUserId());
                var ship_owner_availability = ScheduleCRUD.LoadInstructorAvailability(User.Identity.GetUserId());
                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();

                foreach (var reservation in ship_owner_reservations)
                {

                    startDate = new DateTime(reservation.StartDate.Year,
                                                 reservation.StartDate.Month,
                                                 reservation.StartDate.Day,
                                                 reservation.StartTime.Hours,
                                                 reservation.StartTime.Minutes,
                                                 reservation.StartTime.Seconds);

                    endDate = new DateTime(reservation.EndDate.Year,
                                           reservation.EndDate.Month,
                                           reservation.EndDate.Day,
                                           reservation.EndTime.Hours,
                                           reservation.EndTime.Minutes,
                                           reservation.EndTime.Seconds);

                    calendar_events.Add(new CalendarEvent
                    {
                        text = reservation.ShipName + " " + reservation.ClientsEmailAddress + " " + reservation.Price.ToString(),
                        start_date = startDate,
                        end_date = endDate,
                    });
                }

                foreach (var reservation in history_reservations)
                {

                    startDate = new DateTime(reservation.StartDate.Year,
                                                 reservation.StartDate.Month,
                                                 reservation.StartDate.Day,
                                                 reservation.StartTime.Hours,
                                                 reservation.StartTime.Minutes,
                                                 reservation.StartTime.Seconds);

                    endDate = new DateTime(reservation.EndDate.Year,
                                           reservation.EndDate.Month,
                                           reservation.EndDate.Day,
                                           reservation.EndTime.Hours,
                                           reservation.EndTime.Minutes,
                                           reservation.EndTime.Seconds);

                    if (reservation.ClientsEmailAddress != null)
                    {
                        calendar_events.Add(new CalendarEvent
                        {
                            text = reservation.ActionTitle + " " + reservation.ClientsEmailAddress + " " + reservation.Price.ToString(),
                            start_date = startDate,
                            end_date = endDate,
                        });
                    }
                    else
                    {
                        calendar_events.Add(new CalendarEvent
                        {
                            text = "Not reserved yet",
                            start_date = startDate,
                            end_date = endDate,
                        });
                    }
                }

                calendar_events.Add(new CalendarEvent
                {
                    text = "Not available",
                    start_date = ship_owner_availability.ToDate.AddDays(1),
                    end_date = ship_owner_availability.FromDate.AddYears(1)
                });

                scheduler_data.Add(calendar_events);
            }

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
                        TimeSpan startTime = new TimeSpan(changedEvent.start_date.Hour, changedEvent.start_date.Minute, changedEvent.start_date.Second);
                        TimeSpan endTime = new TimeSpan(changedEvent.end_date.Hour, changedEvent.end_date.Minute, changedEvent.end_date.Second);
                        ScheduleCRUD.UpdateAvailability(1,
                                                        changedEvent.start_date,
                                                        startTime,
                                                        changedEvent.end_date,
                                                        endTime,
                                                        User.Identity.GetUserId());
                        //return RedirectToAction("FillARecord", "Manage", new { clientsEmail = "clientsEmail@gmail.com"});
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

