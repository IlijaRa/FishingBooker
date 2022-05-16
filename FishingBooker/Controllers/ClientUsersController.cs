using FishingBooker.Models;
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
            foreach (var history_reservation in data_history_reservations)
            {
                foreach (var row in data_users)
                {
                    if ((row.Type == "FishingInstructor" || row.Type == "CottageOwner" || row.Type == "ShipOwner") & history_reservation.OwnerId.Equals(row.Id))
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
                    }
                }
            }
            return View(users);
        }

        public ActionResult SearchUsers(string searching)
        {
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
            var user = RegUserCRUD.LoadUsers().Find(x => x.Id == ownerId);
            var data_history_reservations = ReservationCRUD.LoadReservationsFromHistoryByClientsEmailAddress(User.Identity.GetUserName());

            ClientComplaintViewModel complaint = new ClientComplaintViewModel();
            complaint.OwnerId = ownerId;
            complaint.OwnerName = user.Name;
            complaint.OwnerSurname = user.Surname;
            complaint.OwnerEmailAddress = user.EmailAddress;

            foreach (var history_reservation in data_history_reservations)
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
                        action_titles_to_show_on_page.Add(adventure);
                    }  
                }
                complaint.ActionTitles = action_titles_to_show_on_page.Select(x => new SelectListItem { Text = x, Value = x }).ToList();

                //foreach (var item in adventure_titles)
                //{
                //    complaint.ActionTitles.Add(new SelectListItem
                //    {
                //        Text = item,
                //        Value = item
                //    });
                //}
            }
            else if (user.Type == "CottageOwner")
            {
                var cottage_titles = CottageCRUD.LoadCottageTitlesByOwner(user.Id);
                foreach (var cottage in cottage_titles)
                {
                    if (action_titles_from_clients_history.Contains(cottage))
                    {
                        action_titles_to_show_on_page.Add(cottage);
                    }
                }
                complaint.ActionTitles = action_titles_to_show_on_page.Select(x => new SelectListItem { Text = x, Value = x }).ToList();

                //ViewBag.data = cottage_titles;
                //foreach (var item in cottage_titles)
                //{
                //    complaint.ActionTitles.Add(new SelectListItem
                //    {
                //        Text = item,
                //        Value = item
                //    });
                //}
            }
            else if (user.Type == "ShipOwner")
            {
                var ship_titles = ShipCRUD.LoadShipTitlesByOwner(user.Id);
                foreach (var ship in ship_titles)
                {
                    if (action_titles_from_clients_history.Contains(ship))
                    {
                        action_titles_to_show_on_page.Add(ship);
                    }
                }
                complaint.ActionTitles = action_titles_to_show_on_page.Select(x => new SelectListItem { Text = x, Value = x }).ToList();

                //ViewBag.data = ship_titles;
                //foreach (var item in ship_titles)
                //{
                //    complaint.ActionTitles.Add(new SelectListItem
                //    {
                //        Text = item,
                //        Value = item
                //    });
                //}
            }
            return View(complaint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeAComplaint(ClientComplaintViewModel model)
        {
            if (ModelState.IsValid)
            {
                var selectedActionTitle = model.ActionTitles; // Value property Gets or sets the value associated with the ListItem.
                //ClientComplaintCRUD.CreateClientComplaint(model.OwnerId,
                //                                              model.OwnerName,
                //                                              model.OwnerSurname,
                //                                              model.OwnerEmailAddress,
                //                                              User.Identity.GetUserName(),
                //                                              selectedActionTitle,
                //                                              model.Reason);

                return RedirectToAction("Index", "ClientUsers");

            }
            return View();
        }
    }
}