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
            if (User.IsInRole("Admin") || User.IsInRole("HeadAdmin"))
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
                return RedirectToAction("InstructorSchedule", "Manage");
            }
            else if (User.IsInRole("ValidCottageOwner"))
            {
                return RedirectToAction("CottageOwnerSchedule", "Manage");
            }
            else if (User.IsInRole("ValidShipOwner"))
            {
                return RedirectToAction("ShipOwnerSchedule", "Manage");
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
            var sum_active = 0.0;
            var sum_history = 0.0;

            float benefits = 0;

            int bad_experience_counter = 0;
            int good_experience_counter = 0;
            int instant_penalty = 0;

            int admins = 0;
            int clients = 0;
            int fishing_instructors = 0;
            int cottage_owners = 0;
            int ship_owners = 0;

            //int count = 0;
            AdminBusinessReportViewModel model = new AdminBusinessReportViewModel();
            List<ReservationToShowViewModel> reservations_to_show = new List<ReservationToShowViewModel>();
            List<ReservationToShowViewModel> history_reservations_to_show = new List<ReservationToShowViewModel>();
            //var data_instructor_revisions = RevisionCRUD.LoadConfirmedRevisionsForInstructor(User.Identity.GetUserName());
            var data_active_adventure_reservations = ReservationCRUD.LoadAdventureReservations();
            var data_active_cottage_reservations = ReservationCRUD.LoadCottageReservations();
            var data_active_ship_reservations = ReservationCRUD.LoadShipReservations();
            var data_history_reservations = ReservationCRUD.LoadReservationsFromHistory();
            var data_records = RecordCRUD.LoadRecords();
            var data_users = RegUserCRUD.LoadUsers();
            
            foreach (var record in data_records)
            {
                if (record.ImpressionType == Enums.RecordImpressionType.GoodExperience)
                    good_experience_counter++;
                else if (record.ImpressionType == Enums.RecordImpressionType.BadExperience)
                    bad_experience_counter++;
                else if (record.ImpressionType == Enums.RecordImpressionType.DidNotShowUp)
                    instant_penalty++;
            }

            foreach (var user in data_users)
            {
                if (user.Type == "Administrator" && user.Status == "Validated")
                {
                    admins++;
                }
                else if (user.Type == "Client" && user.Status == "Validated")
                {
                    clients++;
                }
                else if (user.Type == "FishingInstructor" && user.Status == "Validated")
                {
                    fishing_instructors++;
                }
                else if (user.Type == "CottageOwner" && user.Status == "Validated")
                {
                    cottage_owners++;
                }
                else if (user.Type == "ShipOwner" && user.Status == "Validated")
                {
                    ship_owners++;
                }
            }

            ViewData["GoodExperience"] = good_experience_counter;
            ViewData["BadExperience"] = bad_experience_counter;
            ViewData["InstantPenalty"] = instant_penalty;

            ViewData["Admins"] = admins;
            ViewData["Clients"] = clients;
            ViewData["FishingInstructors"] = fishing_instructors;
            ViewData["CottageOwners"] = cottage_owners;
            ViewData["ShipOwners"] = ship_owners;

            ViewData["Complaints"] = ClientComplaintCRUD.LoadClientComplaints().Count();
            ViewData["Revisions"] = RevisionCRUD.LoadRevisions().Count();
            ViewData["Records"] = RecordCRUD.LoadRecords().Count();
            ViewData["DeactivationRequests"] = DeactivationRequestCRUD.LoadDeactivationRequests().Count();
            ViewData["RegistrationRequests"] = DeactivationRequestCRUD.LoadDeactivationRequests().Count();
            //var data_adventure = AdventureCRUD.LoadAdventureById(advId);
            //var data_instructor = RegUserCRUD.LoadUserById(User.Identity.GetUserId());
            //var loyalty_scales = LoyaltyProgramCRUD.LoadLoyaltyScales();

            //model.AdventureId = advId;
            //model.AverageRate = data_adventure.Rating;

            ////sorting scales by min earned points
            //var temp = new LoyaltyScale();
            //for (int j = 0; j <= loyalty_scales.Count - 2; j++)
            //{
            //    for (int i = 0; i <= loyalty_scales.Count - 2; i++)
            //    {
            //        if (loyalty_scales[i].MinEarnedPoints > loyalty_scales[i + 1].MinEarnedPoints)
            //        {
            //            temp = loyalty_scales[i + 1];
            //            loyalty_scales[i + 1] = loyalty_scales[i];
            //            loyalty_scales[i] = temp;
            //        }
            //    }
            //}

            //foreach (var scale in loyalty_scales)
            //{
            //    if (scale.MinEarnedPoints <= data_instructor.TotalScalePoints)
            //    {
            //        benefits = scale.OwnerBenefits;
            //    }
            //}

            // racuna samo income za rezervacije koje su aktivne kod instruktora
            foreach (var reservation in data_active_adventure_reservations)
            {
                reservations_to_show.Add(new ReservationToShowViewModel
                {
                    Id = reservation.Id,
                    Place = reservation.Place,
                    ClientsEmailAddress = reservation.ClientsEmailAddress,
                    ActionTitle = reservation.Place,
                    StartDate = reservation.StartDate,
                    StartTime = reservation.StartTime.ToString(),
                    EndDate = reservation.EndDate,
                    EndTime = reservation.EndTime.ToString(),
                    Price = reservation.Price,
                    OwnerId = reservation.InstructorId,
                    IsReserved = reservation.IsReserved
                });
                if (reservation.IsReserved == true)
                    sum_active += Convert.ToDouble(reservation.Price);
            }
            model.Active_Income = sum_active + (sum_active * (benefits / 100));

            // racuna samo income za rezervacije koje su aktivne kod vlasnika vikendica
            foreach (var reservation in data_active_cottage_reservations)
            {
                reservations_to_show.Add(new ReservationToShowViewModel
                {
                    Id = reservation.Id,
                    Place = reservation.CottageName,
                    ClientsEmailAddress = reservation.ClientsEmailAddress,
                    ActionTitle = reservation.CottageName,
                    StartDate = reservation.StartDate,
                    StartTime = reservation.StartTime.ToString(),
                    EndDate = reservation.EndDate,
                    EndTime = reservation.EndTime.ToString(),
                    Price = reservation.Price,
                    OwnerId = reservation.OwnerId,
                    IsReserved = reservation.IsReserved
                });
                if (reservation.IsReserved == true)
                    sum_active += Convert.ToDouble(reservation.Price);
            }
            model.Active_Income = sum_active + (sum_active * (benefits / 100));

            // racuna samo income za rezervacije koje su aktivne kod vlasnika brodova
            foreach (var reservation in data_active_ship_reservations)
            {
                reservations_to_show.Add(new ReservationToShowViewModel
                {
                    Id = reservation.Id,
                    Place = reservation.ShipName,
                    ClientsEmailAddress = reservation.ClientsEmailAddress,
                    ActionTitle = reservation.ShipName,
                    StartDate = reservation.StartDate,
                    StartTime = reservation.StartTime.ToString(),
                    EndDate = reservation.EndDate,
                    EndTime = reservation.EndTime.ToString(),
                    Price = reservation.Price,
                    OwnerId = reservation.OwnerId,
                    IsReserved = reservation.IsReserved
                });
                if (reservation.IsReserved == true)
                    sum_active += Convert.ToDouble(reservation.Price);
            }
            model.Active_Income = sum_active + (sum_active * (benefits / 100));
            model.active_reservations = reservations_to_show;

            // racuna samo income za rezervacije koje su prosle
            foreach (var reservation in data_history_reservations)
            {
                history_reservations_to_show.Add(new ReservationToShowViewModel
                {
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
                sum_history += Convert.ToDouble(reservation.Price);
            }
            model.history_reservations = history_reservations_to_show;
            model.History_Income += sum_history;// + (sum_history * (benefits/100));

            return View(model);
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
            var sum_current = 0.0;
            var sum_history = 0.0;
            ClientIndexViewModel model = new ClientIndexViewModel();
            var data_adventure_reservations = ReservationCRUD.LoadAdventureReservationsByClient(User.Identity.GetUserName());
            var data_cottage_reservations = ReservationCRUD.LoadCottageReservationsByClient(User.Identity.GetUserName());
            var data_ship_reservations = ReservationCRUD.LoadShipReservationsByClient(User.Identity.GetUserName());
            var data_history_reservations = ReservationCRUD.LoadReservationsFromHistoryByClientsEmailAddress(User.Identity.GetUserName());
            List<ReservationToShowViewModel> fut_reservations = new List<ReservationToShowViewModel>();
            List<ReservationFromHistoryViewModel> his_reservations = new List<ReservationFromHistoryViewModel>();
            var data_client = RegUserCRUD.LoadUserById(User.Identity.GetUserId());
            var loyalty_scales = LoyaltyProgramCRUD.LoadLoyaltyScales();
            float benefits = 0;

            foreach (var scale in loyalty_scales)
            {
                if (scale.MinEarnedPoints <= data_client.TotalScalePoints)
                {
                    benefits = scale.OwnerBenefits;
                }
            }


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
                    Price = reservation.Price,
                    ScaledPrice = Convert.ToDouble(reservation.Price) - (Convert.ToDouble(reservation.Price) * (benefits / 100)),
                });
                sum_current += Convert.ToDouble(reservation.Price);
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
                    Price = reservation.Price,
                    ScaledPrice = Convert.ToDouble(reservation.Price) - (Convert.ToDouble(reservation.Price) * (benefits / 100)),
                });
                sum_current += Convert.ToDouble(reservation.Price);
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
                    Price = reservation.Price,
                    ScaledPrice = Convert.ToDouble(reservation.Price) - (Convert.ToDouble(reservation.Price) * (benefits / 100)),
                });
                sum_current += Convert.ToDouble(reservation.Price);

            }

            model.FutureOutcomes = sum_current - (sum_current * (benefits / 100));

            foreach (var reservation in data_history_reservations)
            {
                his_reservations.Add(new ReservationFromHistoryViewModel
                {
                    Id = reservation.Id,
                    ClientsEmailAddress = reservation.ClientsEmailAddress,
                    ActionTitle = reservation.ActionTitle,
                    StartDate = reservation.StartDate,
                    StartTime = reservation.StartTime.ToString(),
                    EndDate = reservation.EndDate,
                    EndTime = reservation.EndTime.ToString(),
                    Price = reservation.Price,
                    ScaledPrice = Convert.ToDouble(reservation.Price) - (Convert.ToDouble(reservation.Price) * (benefits / 100)),
                    OwnerId = reservation.OwnerId
                });
                sum_history += Convert.ToDouble(reservation.Price);
            }

            model.HistoryOutcomes = sum_history - (sum_history * (benefits / 100));

            model.future_reservations = fut_reservations;
            model.history_reservations = his_reservations;
            return View(model);
        }
    }
}