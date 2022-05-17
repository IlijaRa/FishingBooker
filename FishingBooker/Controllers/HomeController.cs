using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FishingBooker.Models;
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
                return RedirectToAction("AdminIndex");
            }
            else if (User.IsInRole("HeadAdmin"))
            {
                return RedirectToAction("HeadAdminIndex");
            }
            else if (User.IsInRole("ValidClient"))
            {
                return RedirectToAction("ClientIndex");
            }
            else if (User.IsInRole("ValidFishingInstructor"))
            {
                return RedirectToAction("FishingInstructorIndex");
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
            ClientIndexViewModel model;
            return View(model);
        }
    }
}