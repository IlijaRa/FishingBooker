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
using Newtonsoft.Json.Linq;
using static Microsoft.ClearScript.V8.V8CpuProfile;
using System.Net;

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

            //default textbox and date-time
            scheduler.Lightbox.AddDefaults();

            // adding special controls to lightbox
            //radiobutton
            var radio = new LightboxRadio("radio_button", "Event type");
            var items = new List<object>(){
                new { key = "1", label = "Available" },
                new { key = "2", label = "Not available" }
            };
            radio.AddOptions(items);
            radio.Vertical = true;
            radio.MapTo = "radio_button";
            scheduler.Lightbox.Add(radio);

            //profile button
            ////default buttons
            //scheduler.Config.buttons_left = new LightboxButtonList{
            //    LightboxButtonList.Save,
            //    LightboxButtonList.Cancel
            //};
            //scheduler.Config.buttons_right = new LightboxButtonList{
            //    LightboxButtonList.Delete
            //};

            // new button
            scheduler.Config.buttons_right.Add(new EventButton
            {
                Label = "Profile",
                OnClick = "get_email",
                Name = "Profile"
            });

            return View(scheduler);
        }

        public ContentResult Data()
        {
            var scheduler_data = new SchedulerAjaxData();
            List<CalendarEvent> calendar_events = new List<CalendarEvent>();
            

            if (User.IsInRole("ValidFishingInstructor"))
            {
                var adventure_reservations = ReservationCRUD.LoadAdventureReservationByInstructorId(User.Identity.GetUserId());
                var instructors_unavailabilities = ScheduleCRUD.LoadOwnerUnavailabilities(User.Identity.GetUserId());
                var instructors_availabilities = ScheduleCRUD.LoadOwnerAvailabilities(User.Identity.GetUserId());

                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();
                
                foreach (var reservation in adventure_reservations)
                {

                    startDate = new DateTime(reservation.StartDate.Year,reservation.StartDate.Month,reservation.StartDate.Day,reservation.StartTime.Hours,reservation.StartTime.Minutes,reservation.StartTime.Seconds);
                    endDate = new DateTime(reservation.EndDate.Year,reservation.EndDate.Month,reservation.EndDate.Day,reservation.EndTime.Hours,reservation.EndTime.Minutes,reservation.EndTime.Seconds);

                    if (reservation.ClientsEmailAddress != null && reservation.IsReserved == true)
                    {
                        calendar_events.Add(new CalendarEvent
                        {
                            id = reservation.Id,
                            event_type = Enums.CalendarEventType.Reserved,
                            text = reservation.Place + " " + reservation.ClientsEmailAddress + " " + reservation.Price.ToString() + " " + "- Reserved",
                            clients_email = reservation.ClientsEmailAddress,
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
                            text = reservation.Place + " " + reservation.Price.ToString() + "- Not reserved yet",
                            start_date = startDate,
                            end_date = endDate,
                            color = "#e84d4d"
                        });
                    }
                    
                }

                foreach (var unavailability in instructors_unavailabilities)
                {
                    startDate = new DateTime(unavailability.FromDate.Year,unavailability.FromDate.Month,unavailability.FromDate.Day,unavailability.FromTime.Hours,unavailability.FromTime.Minutes,unavailability.FromTime.Seconds);
                    endDate = new DateTime(unavailability.ToDate.Year,unavailability.ToDate.Month,unavailability.ToDate.Day,unavailability.ToTime.Hours,unavailability.ToTime.Minutes,unavailability.ToTime.Seconds);

                    calendar_events.Add(new CalendarEvent
                    {
                        id = unavailability.Id,
                        event_type = Enums.CalendarEventType.Unavailability,
                        text = unavailability.Text,
                        start_date = startDate,
                        end_date = endDate,
                        color = "#332d2e",
                        textColor = "#f7f5f5"
                    });
                }
                foreach (var availability in instructors_availabilities)
                {
                    startDate = new DateTime(availability.FromDate.Year, availability.FromDate.Month, availability.FromDate.Day, availability.FromTime.Hours, availability.FromTime.Minutes, availability.FromTime.Seconds);
                    endDate = new DateTime(availability.ToDate.Year, availability.ToDate.Month, availability.ToDate.Day, availability.ToTime.Hours, availability.ToTime.Minutes, availability.ToTime.Seconds);

                    calendar_events.Add(new CalendarEvent
                    {
                        id = availability.Id,
                        event_type = Enums.CalendarEventType.Availability,
                        text = availability.Text,
                        start_date = startDate,
                        end_date = endDate,
                        color = "#0B5ED7"
                    });
                }
                scheduler_data.Add(calendar_events);
            }

            else if (User.IsInRole("ValidCottageOwner"))
            {
                var cottage_owner_reservations = ReservationCRUD.LoadCottageReservationByOwnerId(User.Identity.GetUserId());
                var cottage_owner_unavailabilities = ScheduleCRUD.LoadOwnerUnavailabilities(User.Identity.GetUserId());
                var cottage_owner_availabilities = ScheduleCRUD.LoadOwnerAvailabilities(User.Identity.GetUserId());
                //var availability_for_standard_reservation = ScheduleCRUD.LoadOwnerAvailabilityForStandardReservation(User.Identity.GetUserId());

                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();

                foreach (var reservation in cottage_owner_reservations)
                {

                    startDate = new DateTime(reservation.StartDate.Year, reservation.StartDate.Month, reservation.StartDate.Day, reservation.StartTime.Hours, reservation.StartTime.Minutes, reservation.StartTime.Seconds);
                    endDate = new DateTime(reservation.EndDate.Year, reservation.EndDate.Month, reservation.EndDate.Day, reservation.EndTime.Hours, reservation.EndTime.Minutes, reservation.EndTime.Seconds);

                    if (reservation.ClientsEmailAddress != null && reservation.IsReserved == true)
                    {
                        calendar_events.Add(new CalendarEvent
                        {
                            id = reservation.Id,
                            event_type = Enums.CalendarEventType.Reserved,
                            text = reservation.CottageName + " " + reservation.ClientsEmailAddress + " " + reservation.Price.ToString() + " " + "- Reserved",
                            clients_email = reservation.ClientsEmailAddress,
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
                            text = reservation.CottageName + " " + reservation.Price.ToString() + "- Not reserved yet",
                            start_date = startDate,
                            end_date = endDate,
                            color = "#e84d4d"
                        });
                    }
                }

                foreach (var unavailability in cottage_owner_unavailabilities)
                {
                    startDate = new DateTime(unavailability.FromDate.Year, unavailability.FromDate.Month, unavailability.FromDate.Day, unavailability.FromTime.Hours, unavailability.FromTime.Minutes, unavailability.FromTime.Seconds);
                    endDate = new DateTime(unavailability.ToDate.Year, unavailability.ToDate.Month, unavailability.ToDate.Day, unavailability.ToTime.Hours, unavailability.ToTime.Minutes, unavailability.ToTime.Seconds);

                    calendar_events.Add(new CalendarEvent
                    {
                        id = unavailability.Id,
                        event_type = Enums.CalendarEventType.Unavailability,
                        text = unavailability.Text,
                        start_date = startDate,
                        end_date = endDate,
                        color = "#332d2e",
                        textColor = "#f7f5f5"
                    });
                }
                foreach (var availability in cottage_owner_availabilities)
                {
                    startDate = new DateTime(availability.FromDate.Year, availability.FromDate.Month, availability.FromDate.Day, availability.FromTime.Hours, availability.FromTime.Minutes, availability.FromTime.Seconds);
                    endDate = new DateTime(availability.ToDate.Year, availability.ToDate.Month, availability.ToDate.Day, availability.ToTime.Hours, availability.ToTime.Minutes, availability.ToTime.Seconds);

                    calendar_events.Add(new CalendarEvent
                    {
                        id = availability.Id,
                        event_type = Enums.CalendarEventType.Availability,
                        text = availability.Text,
                        start_date = startDate,
                        end_date = endDate,
                        color = "#0B5ED7"
                    });
                }
                //calendar_events.Add(new CalendarEvent
                //{
                //    text = "Available for standard reservations",
                //    start_date = new DateTime(availability_for_standard_reservation.FromDate.Year,
                //                              availability_for_standard_reservation.FromDate.Month,
                //                              availability_for_standard_reservation.FromDate.Day,
                //                              availability_for_standard_reservation.FromTime.Hours,
                //                              availability_for_standard_reservation.FromTime.Minutes,
                //                              availability_for_standard_reservation.FromTime.Seconds),
                //    end_date = new DateTime(availability_for_standard_reservation.ToDate.Year,
                //                              availability_for_standard_reservation.ToDate.Month,
                //                              availability_for_standard_reservation.ToDate.Day,
                //                              availability_for_standard_reservation.ToTime.Hours,
                //                              availability_for_standard_reservation.ToTime.Minutes,
                //                              availability_for_standard_reservation.ToTime.Seconds),
                //});
                scheduler_data.Add(calendar_events);
            }

            else if (User.IsInRole("ValidShipOwner"))
            {
                var ship_owner_reservations = ReservationCRUD.LoadShipReservationByOwnerId(User.Identity.GetUserId());
                var ship_owner_unavailabilities = ScheduleCRUD.LoadOwnerUnavailabilities(User.Identity.GetUserId());
                var ship_owner_availabilities = ScheduleCRUD.LoadOwnerAvailabilities(User.Identity.GetUserId());

                //var availability_for_standard_reservation = ScheduleCRUD.LoadOwnerAvailabilityForStandardReservation(User.Identity.GetUserId());

                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();

                foreach (var reservation in ship_owner_reservations)
                {

                    startDate = new DateTime(reservation.StartDate.Year, reservation.StartDate.Month, reservation.StartDate.Day, reservation.StartTime.Hours, reservation.StartTime.Minutes, reservation.StartTime.Seconds);
                    endDate = new DateTime(reservation.EndDate.Year, reservation.EndDate.Month, reservation.EndDate.Day, reservation.EndTime.Hours, reservation.EndTime.Minutes, reservation.EndTime.Seconds);

                    if (reservation.ClientsEmailAddress != null && reservation.IsReserved == true)
                    {
                        calendar_events.Add(new CalendarEvent
                        {
                            id = reservation.Id,
                            event_type = Enums.CalendarEventType.Reserved,
                            text = reservation.ShipName + " " + reservation.ClientsEmailAddress + " " + reservation.Price.ToString() + " " + "- Reserved",
                            clients_email = reservation.ClientsEmailAddress,
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
                            text = reservation.ShipName + " " + reservation.Price.ToString() + "- Not reserved yet",
                            start_date = startDate,
                            end_date = endDate,
                            color = "#e84d4d"
                        });
                    }
                }

                foreach (var unavailability in ship_owner_unavailabilities)
                {
                    startDate = new DateTime(unavailability.FromDate.Year, unavailability.FromDate.Month, unavailability.FromDate.Day, unavailability.FromTime.Hours, unavailability.FromTime.Minutes, unavailability.FromTime.Seconds);
                    endDate = new DateTime(unavailability.ToDate.Year, unavailability.ToDate.Month, unavailability.ToDate.Day, unavailability.ToTime.Hours, unavailability.ToTime.Minutes, unavailability.ToTime.Seconds);

                    calendar_events.Add(new CalendarEvent
                    {
                        id = unavailability.Id,
                        event_type = Enums.CalendarEventType.Unavailability,
                        text = unavailability.Text,
                        start_date = startDate,
                        end_date = endDate,
                        color = "#332d2e",
                        textColor = "#f7f5f5"
                    });
                }

                foreach (var availability in ship_owner_availabilities)
                {
                    startDate = new DateTime(availability.FromDate.Year, availability.FromDate.Month, availability.FromDate.Day, availability.FromTime.Hours, availability.FromTime.Minutes, availability.FromTime.Seconds);
                    endDate = new DateTime(availability.ToDate.Year, availability.ToDate.Month, availability.ToDate.Day, availability.ToTime.Hours, availability.ToTime.Minutes, availability.ToTime.Seconds);

                    calendar_events.Add(new CalendarEvent
                    {
                        id = availability.Id,
                        event_type = Enums.CalendarEventType.Availability,
                        text = availability.Text,
                        start_date = startDate,
                        end_date = endDate,
                        color = "#0B5ED7"
                    });
                }

                //calendar_events.Add(new CalendarEvent
                //{
                //    text = "Available for standard reservations",
                //    start_date = new DateTime(availability_for_standard_reservation.FromDate.Year,
                //                              availability_for_standard_reservation.FromDate.Month,
                //                              availability_for_standard_reservation.FromDate.Day,
                //                              availability_for_standard_reservation.FromTime.Hours,
                //                              availability_for_standard_reservation.FromTime.Minutes,
                //                              availability_for_standard_reservation.FromTime.Seconds),
                //    end_date = new DateTime(availability_for_standard_reservation.ToDate.Year,
                //                              availability_for_standard_reservation.ToDate.Month,
                //                              availability_for_standard_reservation.ToDate.Day,
                //                              availability_for_standard_reservation.ToTime.Hours,
                //                              availability_for_standard_reservation.ToTime.Minutes,
                //                              availability_for_standard_reservation.ToTime.Seconds),
                //});

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
                        if (changedEvent.radio_button == "1")
                        {
                            ScheduleCRUD.CreateAvailability(changedEvent.start_date,
                                                            startTime,
                                                            changedEvent.end_date,
                                                            endTime,
                                                            User.Identity.GetUserId(),
                                                            changedEvent.text);
                        }
                        else if (changedEvent.radio_button == "2")
                        {
                            ScheduleCRUD.CreateUnavailability(changedEvent.start_date,
                                                            startTime,
                                                            changedEvent.end_date,
                                                            endTime,
                                                            User.Identity.GetUserId(),
                                                            changedEvent.text);
                        }
                        break;
                    case DataActionTypes.Delete:
                        if (changedEvent.event_type == Enums.CalendarEventType.Availability)
                        {
                            ScheduleCRUD.DeleteAvailability(changedEvent.id,
                                                            User.Identity.GetUserId());
                        }
                        else if (changedEvent.event_type == Enums.CalendarEventType.Unavailability) 
                        {
                            ScheduleCRUD.DeleteUnavailability(changedEvent.id,
                                                              User.Identity.GetUserId());
                        }
                        else if (changedEvent.event_type == Enums.CalendarEventType.Reserved || changedEvent.event_type == Enums.CalendarEventType.NotReserved)
                        {
                            if (User.IsInRole("ValidFishingInstructor"))
                            {
                                ReservationCRUD.DeleteAdventureReservation(changedEvent.id);
                            }
                            else if (User.IsInRole("ValidCottageOwner"))
                            {
                                ReservationCRUD.DeleteCottageReservation(changedEvent.id);
                            }
                            else if (User.IsInRole("ValidShipOwner"))
                            {
                                ReservationCRUD.DeleteShipReservation(changedEvent.id);
                            }
                        }
                        break;
                    default:
                        if (changedEvent.event_type == Enums.CalendarEventType.Availability)
                        {
                            //if ((changedEvent.radio_button == "1")/* || (changedEvent.radio_button == "null")*/)
                            //{
                            //    ScheduleCRUD.UpdateAvailability(changedEvent.id,
                            //                                changedEvent.start_date,
                            //                                startTime,
                            //                                changedEvent.end_date,
                            //                                endTime,
                            //                                User.Identity.GetUserId(),
                            //                                changedEvent.text);
                            //}
                            if (changedEvent.radio_button == "2")
                            {
                                ScheduleCRUD.DeleteAvailability(changedEvent.id, User.Identity.GetUserId());
                                ScheduleCRUD.CreateUnavailability(changedEvent.start_date,
                                                            startTime,
                                                            changedEvent.end_date,
                                                            endTime,
                                                            User.Identity.GetUserId(),
                                                            changedEvent.text);
                            }
                            else
                            {
                                ScheduleCRUD.UpdateAvailability(changedEvent.id,
                                                            changedEvent.start_date,
                                                            startTime,
                                                            changedEvent.end_date,
                                                            endTime,
                                                            User.Identity.GetUserId(),
                                                            changedEvent.text);
                            }
                        }
                        else if (changedEvent.event_type == Enums.CalendarEventType.Unavailability) 
                        {
                            //if ((changedEvent.radio_button == "2")/* || (changedEvent.radio_button == "null")*/)
                            //{
                            //    ScheduleCRUD.UpdateUnavailability(changedEvent.id,
                            //                                    changedEvent.start_date,
                            //                                    startTime,
                            //                                    changedEvent.end_date,
                            //                                    endTime,
                            //                                    User.Identity.GetUserId(),
                            //                                    changedEvent.text);
                            //}
                            if(changedEvent.radio_button == "1")
                            {
                                ScheduleCRUD.DeleteUnavailability(changedEvent.id, User.Identity.GetUserId());
                                ScheduleCRUD.CreateAvailability(changedEvent.start_date,
                                                            startTime,
                                                            changedEvent.end_date,
                                                            endTime,
                                                            User.Identity.GetUserId(),
                                                            changedEvent.text);
                            }
                            else
                            {
                                ScheduleCRUD.UpdateUnavailability(changedEvent.id,
                                                                changedEvent.start_date,
                                                                startTime,
                                                                changedEvent.end_date,
                                                                endTime,
                                                                User.Identity.GetUserId(),
                                                                changedEvent.text);
                            }
                        }
                        else if (changedEvent.event_type == Enums.CalendarEventType.Reserved || changedEvent.event_type == Enums.CalendarEventType.NotReserved)
                        {
                            if (User.IsInRole("ValidFishingInstructor"))
                            {
                                ReservationCRUD.UpdateAdventureReservationDates(changedEvent.id,
                                                               changedEvent.start_date,
                                                               startTime,
                                                               changedEvent.end_date,
                                                               endTime,
                                                               User.Identity.GetUserId());
                            }
                            else if (User.IsInRole("ValidCottageOwner"))
                            {
                                ReservationCRUD.UpdateCottageReservationDates(changedEvent.id,
                                                               changedEvent.start_date,
                                                               startTime,
                                                               changedEvent.end_date,
                                                               endTime,
                                                               User.Identity.GetUserId());
                            }
                            else if (User.IsInRole("ValidShipOwner"))
                            {
                                ReservationCRUD.UpdateShipReservationDates(changedEvent.id,
                                                               changedEvent.start_date,
                                                               startTime,
                                                               changedEvent.end_date,
                                                               endTime,
                                                               User.Identity.GetUserId());
                            }

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

