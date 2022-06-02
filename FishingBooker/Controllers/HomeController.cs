using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FishingBooker.Models;
using FishingBooker.Models.IndexViewModels;
using FishingBookerLibrary.BusinessLogic;
using FishingBookerLibrary.Models;
using Microsoft.AspNet.Identity;

namespace FishingBooker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("AdminIndex", "Home");
            }
            else if (User.IsInRole("HeadAdmin"))
            {
                return RedirectToAction("HeadAdminIndex", "Home");
            }
            else if (User.IsInRole("ValidClient"))
            {
                return RedirectToAction("ClientIndex", "Home");
            }
            else if (User.IsInRole("ValidFishingInstructor"))
            {
                return RedirectToAction("Index", "Manage");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AdminIndex()
        {
            return View();
        }
        public ActionResult HeadAdminIndex()
        {
            return View();
        }
        public ActionResult FishingInstructorIndex()
        {
            return View();
        }
        public ActionResult ClientIndex()
        {
            ClientIndexViewModel model = new ClientIndexViewModel();
            var data_adventure_reservations = ReservationCRUD.LoadAdventureReservationsByClient(User.Identity.GetUserName());
            var data_cottage_reservations = ReservationCRUD.LoadCottageReservationsByClient(User.Identity.GetUserName());
            var data_ship_reservations = ReservationCRUD.LoadShipReservationsByClient(User.Identity.GetUserName());
            var data_history_reservations = ReservationCRUD.LoadReservationsFromHistoryByClientsEmailAddress(User.Identity.GetUserName());
            List<ReservationToShowViewModel> fut_reservations = new List<ReservationToShowViewModel>();
            List<ReservationFromHistoryViewModel> his_reservations = new List<ReservationFromHistoryViewModel>();
            
            foreach (var reservation in data_adventure_reservations)
            {
                fut_reservations.Add(new ReservationToShowViewModel
                {
                    Id = reservation.Id,
                    //ClientsEmailAddress = reservation.ClientsEmailAddress,
                    ActionTitle = reservation.Place,
                    StartDate = reservation.StartDate,
                    StartTime = reservation.StartTime.ToString(),
                    EndDate = reservation.EndDate,
                    EndTime = reservation.EndTime.ToString(),
                    Price = reservation.Price
                });
            }

            foreach (var reservation in data_cottage_reservations)
            {
                fut_reservations.Add(new ReservationToShowViewModel
                {
                    Id = reservation.Id,
                    //ClientsEmailAddress = reservation.ClientsEmailAddress,
                    ActionTitle = reservation.CottageName,
                    StartDate = reservation.StartDate,
                    StartTime = reservation.StartTime.ToString(),
                    EndDate = reservation.EndDate,
                    EndTime = reservation.EndTime.ToString(),
                    Price = reservation.Price
                });
            }

            foreach (var reservation in data_ship_reservations)
            {
                fut_reservations.Add(new ReservationToShowViewModel
                {
                    Id = reservation.Id,
                    //ClientsEmailAddress = reservation.ClientsEmailAddress,
                    ActionTitle = reservation.ShipName,
                    StartDate = reservation.StartDate,
                    StartTime = reservation.StartTime.ToString(),
                    EndDate = reservation.EndDate,
                    EndTime = reservation.EndTime.ToString(),
                    Price = reservation.Price
                });
            }

            foreach (var reservation in data_history_reservations)
            {
                his_reservations.Add(new ReservationFromHistoryViewModel { 
                    Id = reservation.Id,
                    ClientsEmailAddress = reservation.ClientsEmailAddress,
                    ActionTitle = reservation.ActionTitle,
                    StartDate = reservation.StartDate,
                    StartTime = reservation.StartTime.ToString(),
                    EndDate = reservation.EndDate,
                    EndTime = reservation.EndTime.ToString(),
                    Price = reservation.Price,
                    OwnerId = reservation.OwnerId
                });
            }
            model.future_reservations = fut_reservations;
            model.history_reservations = his_reservations;
            return View(model);
        }
    }
}