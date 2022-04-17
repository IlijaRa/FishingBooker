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

namespace FishingBooker.Controllers
{
    [Authorize(Roles = "ValidFishingInstructor")]
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
            return View();
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
                                               model.Price,
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
                        Price = row.Price,
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
                        adventure.Price == result ||
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
                            Price = adventure.Price,
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
            RegUser user = RegUserCRUD.LoadUsers().Find(x => x.Id == User.Identity.GetUserId());

            foreach (var rowa in data_adventures)
            {
                if (rowa.Id == advId)
                {
                    string[] address = rowa.Address.Split(',');
                    ViewData["MapSource"] = CalculateMapSource(rowa.Address);

                    edit_adventure.adventure.AdventureId = rowa.Id;
                    edit_adventure.adventure.Title = rowa.Title;
                    edit_adventure.adventure.Street = address[0];
                    edit_adventure.adventure.AddressNumber = address[1];
                    edit_adventure.adventure.City = address[2];
                    edit_adventure.adventure.PromotionDescription = rowa.PromotionDescription;
                    edit_adventure.adventure.BehaviourRules = rowa.BehaviourRules;
                    edit_adventure.adventure.AdditionalServices = rowa.AdditionalServices;
                    edit_adventure.adventure.Pricelist = rowa.Pricelist;
                    edit_adventure.adventure.Price = rowa.Price;
                    edit_adventure.adventure.MaxNumberOfPeople = rowa.MaxNumberOfPeople;
                    edit_adventure.adventure.FishingEquipment = rowa.FishingEquipment;
                    edit_adventure.adventure.CancellationPolicy = rowa.CancellationPolicy;
                    edit_adventure.adventure.Biography = user.Biography;
                    break;
                }
            }

            foreach (var row in data_fast_reservations)
            {
                if (row.AdventureId == advId)
                {
                    string[] duration_split = row.Duration.Split(',');
                    int result = -1;
                    int.TryParse(duration_split[0], out result);
                    
                    fast_reservations_list.Add(new AdventureReservationViewModel
                    {
                        Id = row.Id,
                        Place = row.Place,
                        StartDate = row.StartDate,
                        StartTime = row.StartTime.ToString(),
                        Duration = result,
                        DaysHours = duration_split[1],
                        MaxNumberOfPeople = row.MaxNumberOfPeople,
                        AdditionalServices = row.AdditionalServices,
                        Price = row.Price,
                        IsReserved = row.IsReserved
                    });
                }
            }

            edit_adventure.fast_reservations = fast_reservations_list;

            return View(edit_adventure);
        }

        private string CalculateMapSource(string FullAddress)
        {
            string[] address = FullAddress.Split(',');
            List<string> street = address[0].Split(' ').ToList();
            List<string> city = address[2].Split(' ').ToList();

            string MapSource ="https://maps.google.com/maps?q=";// Narodnog%20Fronta,%2010%20Novi%20Sad&t=k&z=13&ie=UTF8&iwloc=&output=embed
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
            string address = model.adventure.Street + "," + model.adventure.AddressNumber + "," + model.adventure.City;
            AdventureCRUD.UpdateAdventure(model.adventure.AdventureId,
                                            model.adventure.Title,
                                            address,
                                            model.adventure.PromotionDescription,
                                            model.adventure.BehaviourRules,
                                            model.adventure.AdditionalServices,
                                            model.adventure.Pricelist,
                                            model.adventure.Price,
                                            model.adventure.MaxNumberOfPeople,
                                            model.adventure.FishingEquipment,
                                            model.adventure.CancellationPolicy);

            RegUserCRUD.UpdateBiography(User.Identity.GetUserId(), model.adventure.Biography);

            return RedirectToAction("EditAdventure", "InstructorUsers", new { advId = model.adventure.AdventureId });
        }

        public ActionResult DeleteAdventure(int advId)
        {
            //System.Diagnostics.Debug.WriteLine(fastReservationId.ToString());
            AdventureCRUD.DeleteAdventure(advId);

            return RedirectToAction("ViewAdventures");
        }

        public ActionResult CreateReservation(int AdventureId)
        {
            AdventureReservationViewModel model = new AdventureReservationViewModel();
            model.Place = "";
            model.StartDate = DateTime.Now;
            model.StartTime = null;
            model.Duration = 0;
            model.DaysHours = null;
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
                TimeSpan time = TimeSpan.Parse(model.StartTime.ToString());
                string duration = model.Duration.ToString() + "," +model.DaysHours;

                ReservationCRUD.CreateAdventureReservations(model.Place,
                                               model.StartDate,
                                               time,
                                               duration,
                                               model.MaxNumberOfPeople,
                                               model.AdditionalServices,
                                               model.Price,
                                               false,   // IsReserved
                                               null,    // ClientsEmailAddress
                                               Enums.ReservationType.Fast,
                                               model.AdventureId);

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
                return View("ReservationReservedError");
        }
    }
}