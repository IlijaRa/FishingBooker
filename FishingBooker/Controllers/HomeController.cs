using FishingBooker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FishingBookerLibrary.BusinessLogic;
using FishingBookerLibrary.Models.Security;

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
                CustomPasswordHasher hasher = new CustomPasswordHasher();

                RegUserCRUD.CreateEmployee( user.Name, 
                                            user.Surname, 
                                            user.PhoneNumber, 
                                            user.EmailAddress,
                                            hasher.HashPassword(user.Password), //user.Password,
                                            user.Address,
                                            user.City, 
                                            user.Country);
                return RedirectToAction("Index");
            }
            return View();
        }

        //public ActionResult ViewUsers()
        //{
        //    var datatable = RegUserCRUD.LoadEmployees();
        //    List<RegUser> users = new List<RegUser>();

        //    foreach(var row in datatable)
        //    {
        //        users.Add(new RegUser
        //        {
        //            UserId = row.Id,
        //            Name = row.Name,
        //            Surname = row.Surname,
        //            PhoneNumber = row.PhoneNumber,
        //            EmailAddress = row.EmailAddress,
        //            Password = row.Password,
        //            Address = row.Address,
        //            City = row.City,
        //            Country = row.Country
        //        });
        //    }

        //    return View(users);
        //}
    }
}