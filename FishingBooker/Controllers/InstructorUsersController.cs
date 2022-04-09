using FishingBooker.Models;
using FishingBookerLibrary.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace FishingBooker.Controllers
{
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
                AdventureCRUD.CreateAdventure( model.Title,
                                               model.Address,
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
                    adventures.Add(new AdventureViewModel
                    {
                        AdventureId = row.Id,
                        Title = row.Title,
                        Address = row.Address,
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
    }
}