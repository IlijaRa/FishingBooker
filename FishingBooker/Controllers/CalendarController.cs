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

            scheduler.Skin = DHXScheduler.Skins.ContrastWhite;
            //scheduler.Config.multi_day = true;//render multiday events
            scheduler.InitialDate = DateTime.Now;
            scheduler.Views.Items.RemoveAt(2);
            scheduler.Views.Add(new YearView());
            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            // adding special controls to lightbox
            scheduler.Lightbox.AddDefaults();

            var radio = new LightboxRadio("textColor", "Event type");

            var items = new List<object>(){
                new { key = "1", label = "Not reserved yet" },
                new { key = "2", label = "Not available" }
            };

            radio.AddOptions(items);
            scheduler.Lightbox.Add(radio);

            return View(scheduler);
        }

        public ContentResult Data()
        {
            var scheduler_data = new SchedulerAjaxData();
            List<CalendarEvent> calendar_events = new List<CalendarEvent>();
            var availability_for_standard_reservation = ScheduleCRUD.LoadOwnerAvailabilityForStandardReservation(User.Identity.GetUserId());

            if (User.IsInRole("ValidFishingInstructor"))
            {
                var instructor_reservations = ReservationCRUD.LoadAdventureReservationByInstructorId(User.Identity.GetUserId());
                var history_reservations = ReservationCRUD.LoadReservationsFromHistoryByOwnerId(User.Identity.GetUserId());
                var instructors_unavailabilities = ScheduleCRUD.LoadOwnerUnavailability(User.Identity.GetUserId());
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

                    if (reservation.ClientsEmailAddress != null && reservation.IsReserved == true)
                    {
                        calendar_events.Add(new CalendarEvent
                        {
                            id = reservation.Id,
                            event_type = Enums.CalendarEventType.Reserved,
                            text = reservation.Place + " " + reservation.ClientsEmailAddress + " " + reservation.Price.ToString() + " " + "- reserved",
                            start_date = startDate,
                            end_date = endDate,
                            color = "#46c267"
                        });
                    }
                    else
                    {
                        calendar_events.Add(new CalendarEvent
                        {
                            id = reservation.Id,
                            event_type = Enums.CalendarEventType.NotReserved,
                            text = "Not reserved yet",
                            start_date = startDate,
                            end_date = endDate,
                            color = "#e84d4d"
                        });
                    }
                    
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

                    calendar_events.Add(new CalendarEvent
                    {
                        id = reservation.Id,
                        event_type = Enums.CalendarEventType.History,
                        text = reservation.ActionTitle + " " + reservation.ClientsEmailAddress + " " + reservation.Price.ToString() + " " + "- finished",
                        start_date = startDate,
                        end_date = endDate,
                        color = "#6779f0"
                    });
                }

                foreach (var unavailability in instructors_unavailabilities)
                {
                    startDate = new DateTime(unavailability.FromDate.Year,
                                                 unavailability.FromDate.Month,
                                                 unavailability.FromDate.Day,
                                                 unavailability.FromTime.Hours,
                                                 unavailability.FromTime.Minutes,
                                                 unavailability.FromTime.Seconds);

                    endDate = new DateTime(unavailability.ToDate.Year,
                                           unavailability.ToDate.Month,
                                           unavailability.ToDate.Day,
                                           unavailability.ToTime.Hours,
                                           unavailability.ToTime.Minutes,
                                           unavailability.ToTime.Seconds);

                    calendar_events.Add(new CalendarEvent
                    {
                        id = unavailability.Id,
                        event_type = Enums.CalendarEventType.Unavailability,
                        text = "Not available",
                        start_date = startDate,
                        end_date = endDate,
                        color = "#332d2e",
                        textColor = "#f7f5f5"
                    });
                }
                calendar_events.Add(new CalendarEvent
                {
                    id = ScheduleCRUD.LoadOwnerAvailabilityForStandardReservation(User.Identity.GetUserId()).Id,
                    event_type = Enums.CalendarEventType.AvailabilityForStandardReservation,
                    text = "Available for standard reservations",
                    start_date = new DateTime(availability_for_standard_reservation.FromDate.Year,
                                              availability_for_standard_reservation.FromDate.Month,
                                              availability_for_standard_reservation.FromDate.Day,
                                              availability_for_standard_reservation.FromTime.Hours,
                                              availability_for_standard_reservation.FromTime.Minutes,
                                              availability_for_standard_reservation.FromTime.Seconds),
                    end_date = new DateTime(availability_for_standard_reservation.ToDate.Year,
                                              availability_for_standard_reservation.ToDate.Month,
                                              availability_for_standard_reservation.ToDate.Day,
                                              availability_for_standard_reservation.ToTime.Hours,
                                              availability_for_standard_reservation.ToTime.Minutes,
                                              availability_for_standard_reservation.ToTime.Seconds),
                    color = "#0ad2f5"
                });
                scheduler_data.Add(calendar_events);
            }

            else if (User.IsInRole("ValidCottageOwner"))
            {
                var cottage_owner_reservations = ReservationCRUD.LoadCottageReservationByOwnerId(User.Identity.GetUserId());
                var history_reservations = ReservationCRUD.LoadReservationsFromHistoryByOwnerId(User.Identity.GetUserId());
                var cottage_owner_unavailabilities = ScheduleCRUD.LoadOwnerUnavailability(User.Identity.GetUserId());
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

                    calendar_events.Add(new CalendarEvent
                    {
                        text = reservation.ActionTitle + " " + reservation.ClientsEmailAddress + " " + reservation.Price.ToString(),
                        start_date = startDate,
                        end_date = endDate,
                    });
                }

                foreach (var unavailability in cottage_owner_unavailabilities)
                {
                    startDate = new DateTime(unavailability.FromDate.Year,
                                                 unavailability.FromDate.Month,
                                                 unavailability.FromDate.Day,
                                                 unavailability.FromTime.Hours,
                                                 unavailability.FromTime.Minutes,
                                                 unavailability.FromTime.Seconds);

                    endDate = new DateTime(unavailability.ToDate.Year,
                                           unavailability.ToDate.Month,
                                           unavailability.ToDate.Day,
                                           unavailability.ToTime.Hours,
                                           unavailability.ToTime.Minutes,
                                           unavailability.ToTime.Seconds);

                    calendar_events.Add(new CalendarEvent
                    {
                        text = "Not available",
                        start_date = startDate,
                        end_date = endDate,
                    });
                }
                calendar_events.Add(new CalendarEvent
                {
                    text = "Available for standard reservations",
                    start_date = new DateTime(availability_for_standard_reservation.FromDate.Year,
                                              availability_for_standard_reservation.FromDate.Month,
                                              availability_for_standard_reservation.FromDate.Day,
                                              availability_for_standard_reservation.FromTime.Hours,
                                              availability_for_standard_reservation.FromTime.Minutes,
                                              availability_for_standard_reservation.FromTime.Seconds),
                    end_date = new DateTime(availability_for_standard_reservation.ToDate.Year,
                                              availability_for_standard_reservation.ToDate.Month,
                                              availability_for_standard_reservation.ToDate.Day,
                                              availability_for_standard_reservation.ToTime.Hours,
                                              availability_for_standard_reservation.ToTime.Minutes,
                                              availability_for_standard_reservation.ToTime.Seconds),
                });
                scheduler_data.Add(calendar_events);
            }

            else if (User.IsInRole("ValidShipOwner"))
            {
                var ship_owner_reservations = ReservationCRUD.LoadShipReservationByOwnerId(User.Identity.GetUserId());
                var history_reservations = ReservationCRUD.LoadReservationsFromHistoryByOwnerId(User.Identity.GetUserId());
                var ship_owner_unavailabilities = ScheduleCRUD.LoadOwnerUnavailability(User.Identity.GetUserId());
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

                    calendar_events.Add(new CalendarEvent
                    {
                        text = reservation.ActionTitle + " " + reservation.ClientsEmailAddress + " " + reservation.Price.ToString(),
                        start_date = startDate,
                        end_date = endDate,
                    });
                }

                foreach (var unavailability in ship_owner_unavailabilities)
                {
                    startDate = new DateTime(unavailability.FromDate.Year,
                                                 unavailability.FromDate.Month,
                                                 unavailability.FromDate.Day,
                                                 unavailability.FromTime.Hours,
                                                 unavailability.FromTime.Minutes,
                                                 unavailability.FromTime.Seconds);

                    endDate = new DateTime(unavailability.ToDate.Year,
                                           unavailability.ToDate.Month,
                                           unavailability.ToDate.Day,
                                           unavailability.ToTime.Hours,
                                           unavailability.ToTime.Minutes,
                                           unavailability.ToTime.Seconds);

                    calendar_events.Add(new CalendarEvent
                    {
                        text = "Not available",
                        start_date = startDate,
                        end_date = endDate,
                    });
                }

                calendar_events.Add(new CalendarEvent
                {
                    text = "Available for standard reservations",
                    start_date = new DateTime(availability_for_standard_reservation.FromDate.Year,
                                              availability_for_standard_reservation.FromDate.Month,
                                              availability_for_standard_reservation.FromDate.Day,
                                              availability_for_standard_reservation.FromTime.Hours,
                                              availability_for_standard_reservation.FromTime.Minutes,
                                              availability_for_standard_reservation.FromTime.Seconds),
                    end_date = new DateTime(availability_for_standard_reservation.ToDate.Year,
                                              availability_for_standard_reservation.ToDate.Month,
                                              availability_for_standard_reservation.ToDate.Day,
                                              availability_for_standard_reservation.ToTime.Hours,
                                              availability_for_standard_reservation.ToTime.Minutes,
                                              availability_for_standard_reservation.ToTime.Seconds),
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
                TimeSpan startTime = new TimeSpan(changedEvent.start_date.Hour, changedEvent.start_date.Minute, changedEvent.start_date.Second);
                TimeSpan endTime = new TimeSpan(changedEvent.end_date.Hour, changedEvent.end_date.Minute, changedEvent.end_date.Second);

                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        ScheduleCRUD.CreateUnavailability(changedEvent.start_date,
                                                            startTime,
                                                            changedEvent.end_date,
                                                            endTime,
                                                            User.Identity.GetUserId());
                        //return RedirectToAction("FillARecord", "Manage", new { clientsEmail = "clientsEmail@gmail.com"});
                        //do insert
                        //action.TargetId = changedEvent.id;//assign postoperational id
                        break;
                    case DataActionTypes.Delete:
                        ScheduleCRUD.DeleteUnavailability(changedEvent.start_date,
                                                            startTime,
                                                            changedEvent.end_date,
                                                            endTime,
                                                            User.Identity.GetUserId());
                        break;
                    default:// "update"                          
                        //TODO: update zavrsi
                        if (changedEvent.text == "Available for standard reservations")
                        {
                            ScheduleCRUD.UpdateOwnerAvailabilityForStandardReservation(changedEvent.start_date,
                                                            startTime,
                                                            changedEvent.end_date,
                                                            endTime,
                                                            User.Identity.GetUserId());
                        }
                        else if (changedEvent.text == "Not available")
                        {
                            ScheduleCRUD.UpdateUnavailability(changedEvent.id,
                                                            changedEvent.start_date,
                                                            startTime,
                                                            changedEvent.end_date,
                                                            endTime,
                                                            User.Identity.GetUserId());
                        }
                        else if (changedEvent.text.Contains("- reserved") || changedEvent.text.Contains("Not reserved yet")) {
                            if (User.IsInRole("ValidFishingInstructor")) {
                                ReservationCRUD.UpdateAdventureReservationDates(changedEvent.id,
                                                               changedEvent.start_date,
                                                               startTime,
                                                               changedEvent.end_date,
                                                               endTime,
                                                               User.Identity.GetUserId());
                            }
                            //TODO: odradi ovo ispod tako sto ces napraviti nove funkcije za update rezervacija za cottage i ship
                            //else if (User.IsInRole("ValidCottageOwner"))
                            //{
                            //    ReservationCRUD.UpdateAdventureReservationDates(changedEvent.id,
                            //                                   changedEvent.start_date,
                            //                                   startTime,
                            //                                   changedEvent.end_date,
                            //                                   endTime,
                            //                                   User.Identity.GetUserId());
                            //}
                            //else if (User.IsInRole("ValidShipOwner"))
                            //{
                            //    ReservationCRUD.UpdateAdventureReservationDates(changedEvent.id,
                            //                                   changedEvent.start_date,
                            //                                   startTime,
                            //                                   changedEvent.end_date,
                            //                                   endTime,
                            //                                   User.Identity.GetUserId());
                            //}

                        }
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

