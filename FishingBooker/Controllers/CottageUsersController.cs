using FishingBooker.Models;
using FishingBooker.Models.EmailSender;
using FishingBookerLibrary.BusinessLogic;
using FishingBookerLibrary.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FishingBooker.Controllers
{
    public class CottageUsersController : Controller
    {
        // GET: CottageUsers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewCottages()
        {
            ViewBag.Message = "Your cottages";

            var data = CottageCRUD.LoadCottages();
            List<CottageViewModel> cottages = new List<CottageViewModel>();

            foreach (var row in data)
            {
                if (row.OwnerId == User.Identity.GetUserId())
                {
                    string[] address_split = row.Address.Split(',');
                    //string address = address_split[0] + " " + address_split[1] + "," + address_split[2];
                    cottages.Add(new CottageViewModel
                    {
                        CottageId = row.Id,
                        Title = row.Title,
                        Street = address_split[0],
                        AddressNumber = address_split[1],
                        City = address_split[2],
                        PromotionDescription = row.PromotionDescription,
                        BehaviourRules = row.BehaviourRules,
                        AdditionalServices = row.AdditionalServices,
                        Pricelist = row.Pricelist,
                        NumberOfRooms = row.NumberOfRooms,
                        BedsPerRoom = row.BedsPerRoom
                    });
                }
            }

            return View(cottages);
        }

        [HttpGet]
        public ActionResult CreateCottageReservationForCurrentlyActiveUser()
        {
            var cottage_titles = CottageCRUD.LoadCottageTitlesByOwner(User.Identity.GetUserId());
            var data_cottage_reservations = ReservationCRUD.LoadCottageReservationByOwnerId(User.Identity.GetUserId());
            CottageReservationViewModel model = new CottageReservationViewModel();
            List<SelectListItem> action_titles = new List<SelectListItem>();

            foreach (var reservation in data_cottage_reservations)
            {
                if (IsDateAndTimeOk(reservation) && reservation.ClientsEmailAddress != null) // kada je reservation.ClientsEmailAddress razlicita od null znaci da ima neko ko je rezervisao taj termin
                {
                    model.ClientsEmailAddress = reservation.ClientsEmailAddress;
                    break;
                }
            }

            if (model.ClientsEmailAddress == null)
                return View("NoCurrentlyActiveClient");

            foreach (var cottage in cottage_titles)
            {
                action_titles.Add(new SelectListItem { Text = cottage, Value = cottage });
            }
            ViewBag.ActionTitles = action_titles;

            return View(model);
        }

        private bool IsDateAndTimeOk(CottageReservation model)
        {
            bool result = false;
            DateTime start_time = new DateTime(model.StartDate.Year, model.StartDate.Month, model.StartDate.Day, model.StartTime.Hours, model.StartTime.Minutes, model.StartTime.Seconds);
            DateTime end_time = new DateTime(model.EndDate.Year, model.EndDate.Month, model.EndDate.Day, model.EndTime.Hours, model.EndTime.Minutes, model.EndTime.Seconds);
            if (start_time <= DateTime.Now && DateTime.Now <= end_time)
            {
                result = true;
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCottageReservationForCurrentlyActiveUser(CottageReservationViewModel model)
        {
            try
            {
                TimeSpan starttime = TimeSpan.Parse(model.StartTime.ToString());
                TimeSpan endtime = TimeSpan.Parse(model.EndTime.ToString());
                TimeSpan validitytime = TimeSpan.Parse("00:00:00");

                if (IsDateAndTimeAllowed(User.Identity.GetUserId(), starttime, endtime, validitytime, model))
                {

                    var cottage = CottageCRUD.LoadCottagesByName(model.CottageName);

                    ReservationCRUD.CreateCottageReservations(model.CottageName,
                                           model.StartDate,
                                           starttime,
                                           model.EndDate,
                                           endtime,
                                           new DateTime(1900, 1, 1), // najstariji datum koji je dozvoljen za smalldatetime u bazi
                                           validitytime,
                                           model.MaxNumberOfPeople,
                                           model.AdditionalServices,
                                           model.Price,
                                           true,                         // IsReserved
                                           model.ClientsEmailAddress,
                                           Enums.ReservationType.Fast,
                                           cottage.Id,
                                           User.Identity.GetUserId());

                    Gmail gmail = new Gmail
                    {
                        To = model.ClientsEmailAddress,
                        Subject = "New reservation",
                        Body = @"The owner with whom you currently have 
                             an appointment has made another reservation for you.
                             Best wishes,
                             Admin team."
                    };
                    gmail.SendEmail();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("DateAndTimeNotAllowed");
                }
            }
            catch (Exception)
            {
                return View();
            }

        }

        private static bool IsDateAndTimeAllowed(string userId, TimeSpan starttime, TimeSpan endtime, TimeSpan? validitytime, CottageReservationViewModel model)
        {
            var data_cottage_reservations = ReservationCRUD.LoadCottageReservationByOwnerId(userId);
            var isOk = true;
            foreach (var cottage in data_cottage_reservations)
            {
                DateTime adventure_start_date = new DateTime(cottage.StartDate.Year, cottage.StartDate.Month, cottage.StartDate.Day, cottage.StartTime.Hours, cottage.StartTime.Minutes, cottage.StartTime.Seconds);
                DateTime adventure_end_date = new DateTime(cottage.EndDate.Year, cottage.EndDate.Month, cottage.EndDate.Day, cottage.EndTime.Hours, cottage.EndTime.Minutes, cottage.EndTime.Seconds);

                DateTime entered_start_date = new DateTime(model.StartDate.Year, model.StartDate.Month, model.StartDate.Day, starttime.Hours, starttime.Minutes, starttime.Seconds);
                DateTime entered_end_date = new DateTime(model.EndDate.Year, model.EndDate.Month, model.EndDate.Day, endtime.Hours, endtime.Minutes, endtime.Seconds);

                if ((adventure_start_date <= entered_start_date && entered_start_date <= adventure_end_date) ||
                    (adventure_start_date <= entered_end_date && entered_end_date <= adventure_end_date))
                {
                    isOk = false;
                }
            }
            return isOk;
        }

    }
}