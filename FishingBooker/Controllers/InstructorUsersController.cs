using FishingBooker.Models;
using FishingBookerLibrary.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FishingBookerLibrary.Models;
using System.Threading.Tasks;
using FishingBooker.Models.EmailSender;

namespace FishingBooker.Controllers
{
    //[Authorize(Roles = "ValidFishingInstructor")]
    public class InstructorUsersController : Controller
    {
        // GET: InstructorUsers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateAdventure()
        {
            ViewBag.Message = "Create adventure";
            AdventureViewModel adventure = new AdventureViewModel();
            adventure.CancellationPolicy = Enums.CancellationPolicyType.ForFree;
            return View(adventure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdventure(AdventureViewModel model)
        {
            if (ModelState.IsValid)
            {
                string address = model.Street + "," + model.AddressNumber + "," + model.City;
                AdventureCRUD.CreateAdventure(model.Title,
                                               address,
                                               model.PromotionDescription,
                                               model.BehaviourRules,
                                               model.AdditionalServices,
                                               model.Pricelist,
                                               //model.Price,
                                               model.MaxNumberOfPeople,
                                               model.FishingEquipment,
                                               model.CancellationPolicy,
                                               User.Identity.GetUserId());

                return RedirectToAction("ViewAdventures");
            }
            return View();
        }

        public ActionResult ViewAdventures()
        {
            ViewBag.Message = "Your adventures";

            var data = AdventureCRUD.LoadAdventures();
            List<AdventureViewModel> adventures = new List<AdventureViewModel>();

            foreach (var row in data)
            {
                if (row.InstructorId == User.Identity.GetUserId()) // ovo ce da propusta samo avanture od instruktora koji je trenutno ulogovan
                {
                    string[] address_split = row.Address.Split(',');
                    //string address = address_split[0] + " " + address_split[1] + "," + address_split[2];
                    adventures.Add(new AdventureViewModel
                    {
                        AdventureId = row.Id,
                        Title = row.Title,
                        Street = address_split[0],
                        AddressNumber = address_split[1],
                        City = address_split[2],
                        PromotionDescription = row.PromotionDescription,
                        BehaviourRules = row.BehaviourRules,
                        AdditionalServices = row.AdditionalServices,
                        Pricelist = row.Pricelist,
                        //Price = row.Price,
                        MaxNumberOfPeople = row.MaxNumberOfPeople,
                        FishingEquipment = row.FishingEquipment,
                        CancellationPolicy = row.CancellationPolicy
                    });
                }
            }

            return View(adventures);
        }

        public ActionResult SearchAdventures(string searching)
        {
            var data = AdventureCRUD.LoadAdventures();
            List<AdventureViewModel> found_adventures = new List<AdventureViewModel>();
            searching = searching.ToLower();

            foreach (var adventure in data)
            {
                if(adventure.InstructorId == User.Identity.GetUserId())
                {
                    decimal result = -1;
                    decimal.TryParse(searching, out result);
                    string[] address = adventure.Address.Split(',');
                    if (adventure.Title.ToLower().Contains(searching) ||
                        address[0].ToLower().Contains(searching) ||
                        address[1].ToLower().Contains(searching) ||
                        address[2].ToLower().Contains(searching) ||
                        adventure.PromotionDescription.ToLower().Contains(searching) ||
                        //adventure.Price == result ||
                        adventure.MaxNumberOfPeople == result)
                    {
                        found_adventures.Add(new AdventureViewModel
                        {
                            AdventureId = adventure.Id,
                            Title = adventure.Title,
                            Street = address[0],
                            AddressNumber = address[1],
                            City = address[2],
                            PromotionDescription = adventure.PromotionDescription,
                            BehaviourRules = adventure.BehaviourRules,
                            AdditionalServices = adventure.AdditionalServices,
                            Pricelist = adventure.Pricelist,
                            //Price = adventure.Price,
                            MaxNumberOfPeople = adventure.MaxNumberOfPeople,
                            FishingEquipment = adventure.FishingEquipment,
                            CancellationPolicy = adventure.CancellationPolicy
                        });
                    }
                }
            }

            return View(found_adventures);
        }

        public ActionResult EditAdventure(int advId)
        {
            ViewData["AdventureId"] = advId;
            
            EditAdventureViewModel edit_adventure = new EditAdventureViewModel();
            var data_adventures = AdventureCRUD.LoadAdventures();
            var data_fast_reservations = ReservationCRUD.LoadAdventureReservations();
            List<AdventureReservationViewModel> fast_reservations_list = new List<AdventureReservationViewModel>();
            //RegUser user = RegUserCRUD.LoadUsers().Find(x => x.Id == User.Identity.GetUserId());
            foreach (var rowa in data_adventures)
            {
                if (rowa.Id == advId)
                {
                    string[] address = rowa.Address.Split(',');
                    ViewData["MapSource"] = CalculateMapSource(rowa.Address);

                    edit_adventure.adventure.AdventureId = advId;
                    edit_adventure.adventure.Title = rowa.Title;
                    edit_adventure.adventure.Street = address[0];
                    edit_adventure.adventure.AddressNumber = address[1];
                    edit_adventure.adventure.City = address[2];
                    edit_adventure.adventure.PromotionDescription = rowa.PromotionDescription;
                    edit_adventure.adventure.BehaviourRules = rowa.BehaviourRules;
                    edit_adventure.adventure.AdditionalServices = rowa.AdditionalServices;
                    edit_adventure.adventure.Pricelist = rowa.Pricelist;
                    //edit_adventure.adventure.Price = rowa.Price;
                    edit_adventure.adventure.MaxNumberOfPeople = rowa.MaxNumberOfPeople;
                    edit_adventure.adventure.FishingEquipment = rowa.FishingEquipment;
                    edit_adventure.adventure.CancellationPolicy = rowa.CancellationPolicy;
                    edit_adventure.adventure.Biography = RegUserCRUD.LoadUserById(rowa.InstructorId).Biography;
                    edit_adventure.images = ImageCRUD.LoadImagesByAdventureId(advId);
                    break;
                }
            }

            foreach (var row in data_fast_reservations)
            {
                if (row.AdventureId == advId)
                {
                    fast_reservations_list.Add(new AdventureReservationViewModel
                    {
                        Id = row.Id,
                        Place = row.Place,
                        StartDate = row.StartDate,
                        StartTime = row.StartTime.ToString(),
                        EndDate = row.EndDate,
                        EndTime = row.EndTime.ToString(),
                        ValidityPeriodDate = row.ValidityPeriodDate,
                        ValidityPeriodTime = row.ValidityPeriodTime.ToString(),
                        MaxNumberOfPeople = row.MaxNumberOfPeople,
                        AdditionalServices = row.AdditionalServices,
                        Price = row.Price,
                        IsReserved = row.IsReserved
                    });
                }
            }

            edit_adventure.image.AdventureId = advId;

            edit_adventure.fast_reservations = fast_reservations_list;

            return View(edit_adventure);
        }

        private string CalculateMapSource(string FullAddress)
        {
            string[] address = FullAddress.Split(',');
            List<string> street = address[0].Split(' ').ToList();
            List<string> city = address[2].Split(' ').ToList();

            string MapSource ="https://maps.google.com/maps?q=";
            foreach (var part in street)
            {
                if (part == street.Last())
                    MapSource = MapSource + part + "," + "%20";
                else
                    MapSource = MapSource + part + "%20";
            }

            MapSource = MapSource + address[1] + "%20";

            foreach (var part in city)
            {
                if (city.Count == 1)
                    MapSource = MapSource + part;
                else
                    MapSource = MapSource + part + "%20";
            }
            MapSource = MapSource + "&t=k&z=13&ie=UTF8&iwloc=&output=embed";

            return MapSource;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdventure(EditAdventureViewModel model)
        {
            if (ModelState.IsValid)
            {
                string address = model.adventure.Street + "," + model.adventure.AddressNumber + "," + model.adventure.City;
                AdventureCRUD.UpdateAdventure(model.adventure.AdventureId,
                                                model.adventure.Title,
                                                address,
                                                model.adventure.PromotionDescription,
                                                model.adventure.BehaviourRules,
                                                model.adventure.AdditionalServices,
                                                model.adventure.Pricelist,
                                                //model.adventure.Price,
                                                model.adventure.MaxNumberOfPeople,
                                                model.adventure.FishingEquipment,
                                                model.adventure.CancellationPolicy);

                RegUserCRUD.UpdateBiography(User.Identity.GetUserId(), model.adventure.Biography);

                return RedirectToAction("EditAdventure", "InstructorUsers", new { advId = model.adventure.AdventureId });
            }
            return View("Error");
        }

        public ActionResult DeleteAdventure(int advId)
        {
            //System.Diagnostics.Debug.WriteLine(fastReservationId.ToString());
            AdventureCRUD.DeleteAdventure(advId);

            return RedirectToAction("ViewAdventures");
        }

        public ActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddImage(EditAdventureViewModel model, HttpPostedFileBase image1)
        {
            if (image1 != null)
            {
                model.image.image = new byte[image1.ContentLength];
                image1.InputStream.Read(model.image.image, 0, image1.ContentLength);
                ImageCRUD.SubmitImage(model.image.image, model.image.AdventureId);
                return RedirectToAction("EditAdventure", "InstructorUsers", new { advId = model.image.AdventureId });
            }
            else
                return View("ImageSubmitError");
        }

        public ActionResult AdventureDetails(int advId)
        {
            DetailsAdventureViewModel model = new DetailsAdventureViewModel();
            var adventure = AdventureCRUD.LoadAdventureById(advId);
            var instructor = RegUserCRUD.LoadUserById(adventure.InstructorId);
            var data_fast_reservations = ReservationCRUD.LoadAdventureReservations();
            List<AdventureReservationViewModel> fast_reservations_list = new List<AdventureReservationViewModel>();
            ViewData["MapSource"] = CalculateMapSource(adventure.Address);
            string[] address = adventure.Address.Split(',');
            model.adventure.AdventureId = advId;
            model.adventure.Title = adventure.Title;
            model.adventure.Street = address[0];
            model.adventure.AddressNumber = address[1];
            model.adventure.City = address[2];
            model.adventure.PromotionDescription = adventure.PromotionDescription;
            model.adventure.BehaviourRules = adventure.BehaviourRules;
            model.adventure.AdditionalServices = adventure.AdditionalServices;
            model.adventure.Pricelist = adventure.Pricelist;
            //model.Price = adventure.Price;
            model.adventure.MaxNumberOfPeople = adventure.MaxNumberOfPeople;
            model.adventure.FishingEquipment = adventure.FishingEquipment;
            model.adventure.CancellationPolicy = adventure.CancellationPolicy;
            model.adventure.Biography = instructor.Biography;
            model.images = ImageCRUD.LoadImagesByAdventureId(advId);

            foreach (var row in data_fast_reservations)
            {
                if (row.AdventureId == advId)
                {
                    fast_reservations_list.Add(new AdventureReservationViewModel
                    {
                        Id = row.Id,
                        Place = row.Place,
                        StartDate = row.StartDate,
                        StartTime = row.StartTime.ToString(),
                        EndDate = row.EndDate,
                        EndTime = row.EndTime.ToString(),
                        ValidityPeriodDate = row.ValidityPeriodDate,
                        ValidityPeriodTime = row.ValidityPeriodTime.ToString(),
                        MaxNumberOfPeople = row.MaxNumberOfPeople,
                        AdditionalServices = row.AdditionalServices,
                        Price = row.Price,
                        IsReserved = row.IsReserved
                    });
                }
            }

            model.fast_reservations = fast_reservations_list;

            return View(model);
        }


        public ActionResult CreateReservation(int AdventureId)
        {
            AdventureReservationViewModel model = new AdventureReservationViewModel();
            model.Place = "";
            model.StartDate = DateTime.Now;
            model.StartTime = null;
            model.EndDate = DateTime.Now;
            model.EndTime = null;
            model.ValidityPeriodDate = DateTime.Now;
            model.ValidityPeriodTime = null;
            model.MaxNumberOfPeople = 0;
            model.AdditionalServices = "";
            model.Price = 0;
            model.AdventureId = AdventureId;
            model.IsReserved = false;
            model.ClientsEmailAddress = null;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReservation(AdventureReservationViewModel model)
        {

            if (ModelState.IsValid)
            {
                TimeSpan starttime = TimeSpan.Parse(model.StartTime.ToString());
                TimeSpan endtime = TimeSpan.Parse(model.EndTime.ToString());
                TimeSpan validitytime = TimeSpan.Parse(model.ValidityPeriodTime.ToString());
                List<string> clientEmails = new List<string>();

                ReservationCRUD.CreateAdventureReservations(model.Place,
                                               model.StartDate,
                                               starttime,
                                               model.EndDate,
                                               endtime,
                                               model.ValidityPeriodDate,
                                               validitytime,
                                               model.MaxNumberOfPeople,
                                               model.AdditionalServices,
                                               model.Price,
                                               false,   // IsReserved
                                               null,    // ClientsEmailAddress
                                               Enums.ReservationType.Fast,
                                               model.AdventureId,
                                               User.Identity.GetUserId());

                var subscribedClients = AdventureCRUD.LoadSubscribersByAdventure(model.AdventureId);
                foreach (var subscriber in subscribedClients)
                {
                    Gmail gmail = new Gmail
                    {
                        To = subscriber,
                        Subject = "New action is available",
                        Body = @"Adventure that you are subscribed to has new action available
                                 Best wishes,
                                 Admin team."
                    };
                    gmail.SendEmail();
                }
                return RedirectToAction("EditAdventure", "InstructorUsers", new { advId = model.AdventureId });
            }
            return View();
        }

        public ActionResult DeleteFastReservation(int advId, int reservationId)
        {
            //System.Diagnostics.Debug.WriteLine(fastReservationId.ToString());
            if (ReservationCRUD.LoadAdventureReservationById(reservationId).IsReserved == false)
            {
                ReservationCRUD.DeleteAdventureReservation(reservationId);
                return RedirectToAction("EditAdventure", "InstructorUsers", new { advId = advId });
            }
            else
                return View("ReservationReservedWarning");
        }

        [HttpGet]
        public ActionResult CreateAdventureReservationForCurrentlyActiveUser()
        {
            var adventure_titles = AdventureCRUD.LoadAdventureTitlesByInstructor(User.Identity.GetUserId());
            var data_adventure_reservations = ReservationCRUD.LoadReservedAdventureReservationByInstructorId(User.Identity.GetUserId());
            AdventureReservationViewModel model = new AdventureReservationViewModel();
            List<SelectListItem> action_titles = new List<SelectListItem>();
            
            foreach (var reservation in data_adventure_reservations)
            {
                if(IsDateAndTimeOk(reservation) /*&& reservation.ClientsEmailAddress != null*/)
                {
                    model.ClientsEmailAddress = reservation.ClientsEmailAddress;
                    break;
                }
            }
            if (model.ClientsEmailAddress == null)
                return View("NoCurrentlyActiveClient");

            foreach (var adventure in adventure_titles)
            {
                action_titles.Add(new SelectListItem { Text = adventure, Value = adventure });
            }
            ViewBag.ActionTitles = action_titles;

            return View(model);
        }

        private bool IsDateAndTimeOk(AdventureReservation model)
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
        public ActionResult CreateAdventureReservationForCurrentlyActiveUser(AdventureReservationViewModel model)
        {
            try
            {
                TimeSpan starttime = TimeSpan.Parse(model.StartTime.ToString());
                TimeSpan endtime = TimeSpan.Parse(model.EndTime.ToString());
                TimeSpan validitytime = TimeSpan.Parse("00:00:00");
                if (IsDateAndTimeAllowed(User.Identity.GetUserId(), starttime, endtime, validitytime, model)) {
                    
                    var adventure = AdventureCRUD.LoadAdventuresByName(model.AdventureTitle);

                    ReservationCRUD.CreateAdventureReservations(model.Place,
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
                                               adventure.Id,
                                               User.Identity.GetUserId());

                    Gmail gmail = new Gmail
                    {
                        To = model.ClientsEmailAddress,
                        Subject = "New reservation",
                        Body = @"The instructor with whom you currently have 
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
                //TODO: return neku stranicu o greski
            }
            return View();
        }

        private static bool IsDateAndTimeAllowed(string userId, TimeSpan starttime, TimeSpan endtime, TimeSpan? validitytime, AdventureReservationViewModel model)
        {
            var data_adventure_reservations = ReservationCRUD.LoadAdventureReservationByInstructorId(userId);
            //var adv = AdventureCRUD.LoadAdventuresByName(model.AdventureTitle);
            var isOk = true;
            foreach (var adventure in data_adventure_reservations)
            {
                DateTime start_date = new DateTime(adventure.StartDate.Year, adventure.StartDate.Month, adventure.StartDate.Day, adventure.StartTime.Hours, adventure.StartTime.Minutes, adventure.StartTime.Seconds);
                DateTime end_date = new DateTime(adventure.EndDate.Year, adventure.EndDate.Month, adventure.EndDate.Day, adventure.EndTime.Hours, adventure.EndTime.Minutes, adventure.EndTime.Seconds);

                DateTime entered_start_date = new DateTime(model.StartDate.Year, model.StartDate.Month, model.StartDate.Day, starttime.Hours, starttime.Minutes, starttime.Seconds);
                DateTime entered_end_date = new DateTime(model.EndDate.Year, model.EndDate.Month, model.EndDate.Day, endtime.Hours, endtime.Minutes, endtime.Seconds);

                if ((start_date <= entered_start_date && entered_start_date <= end_date) ||
                    (start_date <= entered_end_date && entered_end_date <= end_date))
                {
                    isOk = false;
                }
            }
            return isOk;
        }

        public ActionResult BusinessReport(int advId)
        {
            var sum_active = 0.0;
            var sum_history = 0.0;
            float benefits = 0;
            //int count = 0;
            BusinessReportViewModel model = new BusinessReportViewModel();
            List<ReservationToShowViewModel> active_reservations_to_show = new List<ReservationToShowViewModel>();
            List<ReservationToShowViewModel> history_reservations_to_show = new List<ReservationToShowViewModel>();
            //var data_instructor_revisions = RevisionCRUD.LoadConfirmedRevisionsForInstructor(User.Identity.GetUserName());
            var data_instructor_active_reservations = ReservationCRUD.LoadAdventureReservationByInstructorId(User.Identity.GetUserId());
            var data_instructor_history_reservations = ReservationCRUD.LoadReservationsFromHistoryByOwnerId(User.Identity.GetUserId());
            var data_adventure = AdventureCRUD.LoadAdventureById(advId);
            var data_instructor = RegUserCRUD.LoadUserById(User.Identity.GetUserId());
            var loyalty_scales = LoyaltyProgramCRUD.LoadLoyaltyScales();

            model.AdventureId = advId;
            model.AverageRate = data_adventure.Rating;

            //sorting scales by min earned points
            var temp = new LoyaltyScale();
            for (int j = 0; j <= loyalty_scales.Count - 2; j++)
            {
                for (int i = 0; i <= loyalty_scales.Count - 2; i++)
                {
                    if (loyalty_scales[i].MinEarnedPoints > loyalty_scales[i + 1].MinEarnedPoints)
                    {
                        temp = loyalty_scales[i + 1];
                        loyalty_scales[i + 1] = loyalty_scales[i];
                        loyalty_scales[i] = temp;
                    }
                }
            }

            foreach (var scale in loyalty_scales)
            {
                if(scale.MinEarnedPoints <= data_instructor.TotalScalePoints)
                {
                    benefits = scale.OwnerBenefits;
                }
            }

            // racuna samo income za rezervacije koje su aktivne
            foreach (var reservation in data_instructor_active_reservations)
            {
                if (reservation.AdventureId == data_adventure.Id)
                {
                    active_reservations_to_show.Add(new ReservationToShowViewModel
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
                    if(reservation.IsReserved == true)
                        sum_active += Convert.ToDouble(reservation.Price);
                }
            }
            model.active_reservations = active_reservations_to_show;
            model.Active_Income = sum_active + (sum_active * (benefits / 100));

            // racuna samo income za rezervacije koje su prosle
            foreach (var reservation in data_instructor_history_reservations)
            {
                if (reservation.ActionTitle.Equals(data_adventure.Title)) {
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
            }

            model.history_reservations = history_reservations_to_show;
            model.History_Income += sum_history;// + (sum_history * (benefits/100));
            return View(model);
        }

        public ActionResult BusinessReportFilteredDate(BusinessReportViewModel filter_model)
        {
            var sum_active = 0.0;
            var sum_history = 0.0;
            int count = 0;
            BusinessReportViewModel model = new BusinessReportViewModel();
            List<ReservationToShowViewModel> active_reservations_to_show = new List<ReservationToShowViewModel>();
            List<ReservationToShowViewModel> history_reservations_to_show = new List<ReservationToShowViewModel>();
            var data_instructor_revisions = RevisionCRUD.LoadConfirmedRevisionsForInstructor(User.Identity.GetUserName());
            var data_instructor_active_reservations = ReservationCRUD.LoadAdventureReservationByInstructorId(User.Identity.GetUserId());
            var data_instructor_history_reservations = ReservationCRUD.LoadReservationsFromHistoryByOwnerId(User.Identity.GetUserId());
            var data_adventure = AdventureCRUD.LoadAdventureById(filter_model.AdventureId);
            var data_instructor = RegUserCRUD.LoadUserById(User.Identity.GetUserId());
            var loyalty_scales = LoyaltyProgramCRUD.LoadLoyaltyScales();
            float benefits = 0;

            foreach (var scale in loyalty_scales)
            {
                if (scale.MinEarnedPoints <= data_instructor.TotalScalePoints)
                {
                    benefits = scale.OwnerBenefits;
                }
            }
            model.AdventureId = filter_model.AdventureId;
            model.AverageRate = data_adventure.Rating;

            //foreach (var revision in data_instructor_revisions)
            //{
            //    if (revision.EntityTitle.Equals(data_adventure.Title))
            //    {
            //        sum += revision.ActionRating;
            //        count++;
            //    }
            //}

            //model.AverageRate = sum / count;

            // racuna samo income za rezervacije koje su trenutne
            foreach (var reservation in data_instructor_active_reservations)
            {
                if ((reservation.Place.Equals(data_adventure.Title)) && (filter_model.FromDate <= reservation.StartDate && reservation.EndDate <= filter_model.ToDate))
                {
                    active_reservations_to_show.Add(new ReservationToShowViewModel
                    {
                        Id = reservation.Id,
                        ClientsEmailAddress = reservation.ClientsEmailAddress,
                        ActionTitle = reservation.Place,
                        StartDate = reservation.StartDate,
                        StartTime = reservation.StartTime.ToString(),
                        EndDate = reservation.EndDate,
                        EndTime = reservation.EndTime.ToString(),
                        Price = reservation.Price,
                        OwnerId = reservation.InstructorId
                    });
                    sum_active += Convert.ToDouble(reservation.Price);
                }
            }
            model.active_reservations = active_reservations_to_show;
            model.Active_Income = sum_active + (sum_active * (benefits / 100));


            foreach (var reservation in data_instructor_history_reservations)
            {
                if ((reservation.ActionTitle.Equals(data_adventure.Title)) && (filter_model.FromDate <= reservation.StartDate && reservation.EndDate <= filter_model.ToDate))
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
            }
            model.history_reservations = history_reservations_to_show;
            model.History_Income += sum_history + (sum_history * (benefits / 100));
            return View(model);
        }

        //public ActionResult PopulateGraphDataWeek(int advId)
        //{
        //    var adventure_week_income = ReservationCRUD.LoadIncomePerDayOfWeek(advId);
        //    string[] adventure_week_income_string = new string[14];
        //    for (int i = 0; i < 7; i++)
        //    {
        //        adventure_week_income_string[i] = adventure_week_income[i].dayOfWeek.ToString();
        //    }
        //    for (int i = 7; i < 14; i++)
        //    {
        //        adventure_week_income_string[i] = adventure_week_income[i].Income.ToString();
        //    }
        //    return Json(adventure_week_income_string, JsonRequestBehavior.AllowGet);
        //}
    }
}