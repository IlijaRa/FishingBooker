using FishingBooker.Models;
using FishingBookerLibrary.BusinessLogic;
using FishingBookerLibrary.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FishingBooker.Controllers
{
    public class ClientUsersController : Controller
    {
        // GET: ClientUsers
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Complaint()
        {
            // ova akcija ipak prikazuje samo one validirane korisnike
            var data_users = RegUserCRUD.LoadUsers();
            var data_history_reservations = ReservationCRUD.LoadReservationsFromHistoryByClientsEmailAddress(User.Identity.GetUserName());
            List<RegUserViewModel> users = new List<RegUserViewModel>();
            List<string> user_ids = new List<string>();
            foreach (var h_reservations in data_history_reservations)
            {
                user_ids.Add(h_reservations.OwnerId);
            }
            List<string> unique_user_ids = user_ids.Distinct().ToList();

            foreach (var history_reservation in data_history_reservations)
            {
                foreach (var row in data_users)
                {
                    if ((row.Type == "FishingInstructor" || row.Type == "CottageOwner" || row.Type == "ShipOwner") & history_reservation.OwnerId.Equals(row.Id))
                    {
                        if (unique_user_ids.Contains(row.Id))
                        {
                            users.Add(new RegUserViewModel
                            {
                                UserId = row.Id,
                                Name = row.Name,
                                Surname = row.Surname,
                                PhoneNumber = row.PhoneNumber,
                                EmailAddress = row.EmailAddress,
                                Type = row.Type,
                                Address = row.Address,
                                City = row.City,
                                Country = row.Country,
                                Description = row.Description
                            });
                            unique_user_ids.Remove(row.Id);
                        }
                    }
                }
            }
            return View(users);
        }

        public ActionResult SearchUsers(string searching)
        {
            if(searching == "")
            {
                return RedirectToAction("Complaint", "ClientUsers");
            }
            var data = RegUserCRUD.LoadUsers();
            List<RegUserViewModel> found_users = new List<RegUserViewModel>();
            searching = searching.ToLower();

            foreach (var user in data)
            {
                if (user.Status == "Validated" && (user.Type == "FishingInstructor" || user.Type == "CottageOwner" || user.Type == "ShipOwner"))
                {
                    if (user.Name.ToLower().Contains(searching) ||
                    user.Surname.ToLower().Contains(searching) ||
                    user.PhoneNumber.ToLower().Contains(searching) ||
                    user.Type.Contains(searching) ||
                    user.Address.Contains(searching) ||
                    user.City.Contains(searching) ||
                    user.Country.Contains(searching))
                    {
                        found_users.Add(new RegUserViewModel
                        {
                            UserId = user.Id,
                            Name = user.Name,
                            Surname = user.Surname,
                            PhoneNumber = user.PhoneNumber,
                            EmailAddress = user.EmailAddress,
                            Type = user.Type,
                            Address = user.Address,
                            City = user.City,
                            Country = user.Country
                        });
                    }
                }

            }

            return View(found_users);
        }

        public ActionResult MakeAComplaint(string ownerId)
        {
            List<string> action_titles_from_clients_history = new List<string>();
            List<string> action_titles_to_show_on_page = new List<string>();
            List<SelectListItem> action_titles = new List<SelectListItem>();
            var user = RegUserCRUD.LoadUsers().Find(x => x.Id == ownerId);
            var data_history_reservations = ReservationCRUD.LoadReservationsFromHistoryByClientsEmailAddress(User.Identity.GetUserName());

            ClientComplaintViewModel complaint = new ClientComplaintViewModel();
            complaint.OwnerId = ownerId;
            complaint.OwnerName = user.Name;
            complaint.OwnerSurname = user.Surname;
            complaint.OwnerEmailAddress = user.EmailAddress;
            ViewData["OwnerId"] = ownerId;
            ViewData["OwnerName"] = user.Name;
            ViewData["OwnerSurname"] = user.Surname;
            ViewData["OwnerEmailAddress"] = user.EmailAddress;

            foreach (var history_reservation in data_history_reservations.Distinct())
            {
                action_titles_from_clients_history.Add(history_reservation.ActionTitle);
            }

            if (user.Type == "FishingInstructor")
            {
                var adventure_titles = AdventureCRUD.LoadAdventureTitlesByInstructor(user.Id);
                foreach (var adventure in adventure_titles)
                {
                    if (action_titles_from_clients_history.Contains(adventure))
                    {
                        action_titles.Add(new SelectListItem { Text = adventure, Value = adventure });
                    }  
                }
                //complaint.ActionTitles = action_titles_to_show_on_page.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
                ViewBag.ActionTitles = action_titles;
                //complaint.allowed_titles_to_select = action_titles;
            }
            else if (user.Type == "CottageOwner")
            {
                var cottage_titles = CottageCRUD.LoadCottageTitlesByOwner(user.Id);
                foreach (var cottage in cottage_titles)
                {
                    if (action_titles_from_clients_history.Contains(cottage))
                    {
                        action_titles.Add(new SelectListItem { Text = cottage, Value = cottage });
                    }
                }
                //complaint.ActionTitles = action_titles_to_show_on_page.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
                ViewBag.ActionTitles = action_titles;
                //complaint.allowed_titles_to_select = action_titles;
            }
            else if (user.Type == "ShipOwner")
            {
                var ship_titles = ShipCRUD.LoadShipTitlesByOwner(user.Id);
                foreach (var ship in ship_titles)
                {
                    if (action_titles_from_clients_history.Contains(ship))
                    {
                        action_titles.Add(new SelectListItem { Text = ship, Value = ship });
                    }
                }
                //complaint.ActionTitles = action_titles_to_show_on_page.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
                ViewBag.ActionTitles = action_titles;
                //complaint.allowed_titles_to_select = action_titles;
            }
            return View(complaint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeAComplaint(ClientComplaintViewModel model)
        {
            
            //model.ClientsEmailAddress = User.Identity.GetUserName();
            //ViewBag.ActionTitles = model.SelectedActionTitle;
            //if (ModelState.IsValid)
            //{
            if (model.SelectedActionTitle != null)
            {
                    var selectedActionTitle = model.SelectedActionTitle;
                ClientComplaintCRUD.CreateClientComplaint(model.OwnerId,
                                                              model.OwnerName,
                                                              model.OwnerSurname,
                                                              model.OwnerEmailAddress,
                                                              User.Identity.GetUserName(),
                                                              selectedActionTitle,
                                                              model.Reason);
                return RedirectToAction("Complaint", "ClientUsers");
            }
            else 
            {
                return View("ClientDidNotSelectActionTitle");
                //return RedirectToAction("MakeAComplaint", "ClientUsers", new { ownerId = model.OwnerId});
            }
        }

        public ActionResult AddRevision(string ownerId, string actionTitle)
        {

            RevisionViewModel model = new RevisionViewModel();
            model.ClientsEmailAddress = User.Identity.GetUserName();
            model.EntityTitle = actionTitle;
            model.OwnersEmailAddress = RegUserCRUD.LoadUsers().Find(x => x.Id == ownerId).EmailAddress;
            model.Description = null;
            model.ActionRating = 0;
            model.OwnerInstructorRating = 0;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRevision(RevisionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = RevisionCRUD.CheckIfRevisionAlreadyxists(model.ClientsEmailAddress, model.EntityTitle, model.OwnersEmailAddress);
                if(result == null)
                {
                    RevisionCRUD.CreateRevision(model.ClientsEmailAddress,
                                                                model.EntityTitle,
                                                                model.OwnersEmailAddress,
                                                                model.Description,
                                                                model.ActionRating,
                                                                model.OwnerInstructorRating);
                    return RedirectToAction("ClientIndex", "Home");
                }
                else
                {
                    return View("ClientAlreadyRated");
                }   
            }
            return View(model);
        }

        public ActionResult SearchEntities()
        {
            ClientSearchEntitiesViewModel model = new ClientSearchEntitiesViewModel();

            List<AdventureViewModel> adventures_viewmodel = new List<AdventureViewModel>();
            List<CottageViewModel> cottages_viewmodel = new List<CottageViewModel>();
            List<ShipViewModel> ships_viewmodel = new List<ShipViewModel>();

            var adventures = AdventureCRUD.LoadAdventures();
            var cottages = CottageCRUD.LoadCottages();
            var ships = ShipCRUD.LoadShips();

            foreach (var row in adventures)
            {
                string[] address_split = row.Address.Split(',');

                var services_list = new List<string>();
                string[] services_split = row.AdditionalServices.Split(',');
                foreach (var s in services_split)
                {
                    services_list.Add(s);
                }

                adventures_viewmodel.Add(new AdventureViewModel
                {
                    AdventureId = row.Id,
                    Title = row.Title,
                    Street = address_split[0],
                    AddressNumber = address_split[1],
                    City = address_split[2],
                    PromotionDescription = row.PromotionDescription,
                    Rating = row.Rating,
                    BehaviourRules = row.BehaviourRules,
                    AdditionalServices = services_list,
                    Pricelist = row.Pricelist,
                    MaxNumberOfPeople = row.MaxNumberOfPeople,
                    FishingEquipment = row.FishingEquipment,
                    CancellationPolicy = row.CancellationPolicy,
                    InstructorId = row.InstructorId
                });
            }

            foreach (var row in cottages)
            {
                string[] address_split = row.Address.Split(',');
                cottages_viewmodel.Add(new CottageViewModel
                {
                    CottageId = row.Id,
                    Title = row.Title,
                    Street = address_split[0],
                    AddressNumber = address_split[1],
                    City = address_split[2],
                    PromotionDescription = row.PromotionDescription,
                    Rating = row.Rating,
                    BehaviourRules = row.BehaviourRules,
                    AdditionalServices = row.AdditionalServices,
                    Pricelist = row.Pricelist,
                    NumberOfRooms = row.NumberOfRooms,
                    BedsPerRoom = row.BedsPerRoom,
                    OwnerId = row.OwnerId
                });
            }

            foreach (var row in ships)
            {
                string[] address_split = row.Address.Split(',');
                ships_viewmodel.Add(new ShipViewModel
                {
                    ShipId = row.Id,
                    Title = row.Title,
                    Street = address_split[0],
                    AddressNumber = address_split[1],
                    City = address_split[2],
                    PromotionDescription = row.PromotionDescription,
                    Rating = row.Rating,
                    BehaviourRules = row.BehaviourRules,
                    AdditionalServices = row.AdditionalServices,
                    Pricelist = row.Pricelist,
                    FishingEquipment = row.FishingEquipment,
                    NavigationEquipment = row.NavigationEquipment,
                    SpecificationId = row.SpecificationId,
                    OwnerId = row.OwnerId
                });
            }

            model.adventures = adventures_viewmodel;
            model.cottages = cottages_viewmodel;
            model.ships = ships_viewmodel;
            return View(model);
        }

        public ActionResult SearchEntitiesSearched(ClientSearchEntitiesViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                TimeSpan starttime = TimeSpan.Parse(model.FromTime);
                TimeSpan endtime = TimeSpan.Parse(model.ToTime);
                
                DateTime entered_start_date = new DateTime(model.FromDate.Year, model.FromDate.Month, model.FromDate.Day, starttime.Hours, starttime.Minutes, starttime.Seconds);
                DateTime entered_end_date = new DateTime(model.ToDate.Year, model.ToDate.Month, model.ToDate.Day, endtime.Hours, endtime.Minutes, endtime.Seconds);

                List<AdventureViewModel> adventures_viewmodel = new List<AdventureViewModel>();
                List<CottageViewModel> cottages_viewmodel = new List<CottageViewModel>();
                List<ShipViewModel> ships_viewmodel = new List<ShipViewModel>();

                if (model.entity == "Adventure")
                {
                    var adventures = AdventureCRUD.LoadAdventures();
                    var adventure_reservations = ReservationCRUD.LoadAdventureReservations();
                    var availabilities = ScheduleCRUD.LoadAvailabilities();

                    foreach (var availability in availabilities)
                    {
                        DateTime availability_start_date = new DateTime(availability.FromDate.Year, availability.FromDate.Month, availability.FromDate.Day, availability.FromTime.Hours, availability.FromTime.Minutes, availability.FromTime.Seconds);
                        DateTime availability_end_date = new DateTime(availability.ToDate.Year, availability.ToDate.Month, availability.ToDate.Day, availability.ToTime.Hours, availability.ToTime.Minutes, availability.ToTime.Seconds);

                        if ((availability_start_date < entered_start_date) && (entered_end_date < availability_end_date))
                        {
                            foreach (var reservation in adventure_reservations)
                            {

                                DateTime reservation_start_date = new DateTime(reservation.StartDate.Year, reservation.StartDate.Month, reservation.StartDate.Day, reservation.StartTime.Hours, reservation.StartTime.Minutes, reservation.StartTime.Seconds);
                                DateTime reservation_end_date = new DateTime(reservation.EndDate.Year, reservation.EndDate.Month, reservation.EndDate.Day, reservation.EndTime.Hours, reservation.EndTime.Minutes, reservation.EndTime.Seconds);
                                
                                if ((reservation_start_date < entered_start_date && entered_start_date < reservation_end_date) || (reservation_start_date < entered_end_date && entered_end_date < reservation_end_date) || (entered_start_date < reservation_start_date && reservation_end_date < entered_end_date))
                                {
                                    continue;
                                }

                                var adv = AdventureCRUD.LoadAdventureById(reservation.AdventureId);

                                if (model.Rating > adv.Rating || model.NumberOfPeople > adv.MaxNumberOfPeople)
                                {
                                    continue;
                                }

                                string[] address_split = adv.Address.Split(',');

                            var services_list = new List<string>();
                            string[] services_split = adv.AdditionalServices.Split(',');
                            foreach (var s in services_split)
                            {
                                services_list.Add(s);
                            }

                            adventures_viewmodel.Add(new AdventureViewModel
                                {
                                    AdventureId = adv.Id,
                                    Title = adv.Title,
                                    Street = address_split[0],
                                    AddressNumber = address_split[1],
                                    City = address_split[2],
                                    PromotionDescription = adv.PromotionDescription,
                                    Rating = adv.Rating,
                                    BehaviourRules = adv.BehaviourRules,
                                    AdditionalServices = services_list,
                                    Pricelist = adv.Pricelist,
                                    Price = adv.Price,
                                    MaxNumberOfPeople = adv.MaxNumberOfPeople,
                                    FishingEquipment = adv.FishingEquipment,
                                    CancellationPolicy = adv.CancellationPolicy,
                                    InstructorId = adv.InstructorId
                                });
                            }
                        }
                    }
                }
                else
                {
                    return View("Error");
                }
                
                model.adventures = adventures_viewmodel.GroupBy(x => x.AdventureId).Select(y => y.First());
                //model.adventures = adventures_viewmodel;/*.Select(x => x.AdventureId).Distinct();*/

                cottages_viewmodel.Select(x => x.CottageId).Distinct();
                model.cottages = cottages_viewmodel;

                ships_viewmodel.Select(x => x.ShipId).Distinct();
                model.ships = ships_viewmodel;
        //    }
            return View(model);
        }

        [HttpGet]
        public ActionResult SearchEntitiesSorted(string searching)
        {

            return View();
        }

        [HttpGet]
        public ActionResult CreateStandardAdventureReservation(int id, DateTime from_date, string from_time, DateTime to_date, string to_time, int no_people)
        {
            var adv = AdventureCRUD.LoadAdventureById(id);
            AdventureStandardReservationViewModel model = new AdventureStandardReservationViewModel();
            model.AdventureTitle = adv.Title;
            model.Place = adv.Title;
            model.StartDate = from_date.ToString("yyyy-MM-dd");
            model.StartTime = from_time;
            model.EndDate = to_date.ToString("yyyy-MM-dd");
            model.EndTime = to_time;
            model.ValidityPeriodDate = new DateTime(1753,1,1);
            model.ValidityPeriodTime = TimeSpan.MinValue.ToString();
            model.MaxNumberOfPeople = no_people;
            model.AdditionalServices = new List<string>();
            model.Price = adv.Price;
            model.IsReserved = false;
            model.ClientsEmailAddress = User.Identity.GetUserName();
            model.AdventureId = adv.Id;
            model.InstructorId = adv.InstructorId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStandardAdventureReservation(AdventureStandardReservationViewModel model)
        {

            if (ModelState.IsValid)
            {
                TimeSpan starttime = TimeSpan.Parse(model.StartTime.ToString());
                TimeSpan endtime = TimeSpan.Parse(model.EndTime.ToString());
                
                var validity_period_date = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                var validity_period_time = new TimeSpan(0, 0, 0);

                string additionalServices = "";
                foreach (var service in model.AdditionalServices)
                {
                    additionalServices = additionalServices + "," + service;
                }

                //using (Entities entities = new Entities())
                //using (DbContextTransaction scope = entities.Database.BeginTransaction())
                //{
                //    //Lock the table during this transaction
                //    entities.Database.ExecuteSqlCommand("SELECT TOP 1 KeyColumn FROM MyTable WITH (TABLOCKX, HOLDLOCK)");

                //    //Do your work with the locked table here...

                //    //Complete the scope here to commit, otherwise it will rollback
                //    //The table lock will be released after we exit the TransactionScope block
                //    scope.Commit();
                //}


                ReservationCRUD.CreateAdventureReservations(model.Place,
                                               Convert.ToDateTime(model.StartDate),
                                               starttime,
                                               Convert.ToDateTime(model.EndDate),
                                               endtime,
                                               validity_period_date,
                                               validity_period_time,
                                               model.MaxNumberOfPeople,
                                               additionalServices,
                                               model.Price,
                                               true,   // IsReserved
                                               model.ClientsEmailAddress,
                                               Enums.ReservationType.Regular,
                                               model.AdventureId,
                                               model.InstructorId);
            }

            return View();
        }
    }
}