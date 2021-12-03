using FishingBooker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FishingBookerLibrary.BusinessLogic;

namespace FishingBooker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
        public ActionResult SignUp()
        {
            ViewBag.Message = "Your register page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(RegUser user)
        {
            if (ModelState.IsValid)
            {
                RegUserCRUD.CreateEmployee( user.Name, 
                                            user.Surname, 
                                            user.PhoneNumber, 
                                            user.EmailAddress, 
                                            user.Address, 
                                            user.City, 
                                            user.Country);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}