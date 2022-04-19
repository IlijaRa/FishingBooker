﻿using FishingBooker.Models;
using FishingBooker.Models.EmailSender;
using FishingBookerLibrary.BusinessLogic;
using FishingBookerLibrary.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FishingBooker.Controllers
{
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "HeadAdmin")]
    public class AdminUsersController : Controller
    {
        // GET: AdminUsers
        public ActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        public ActionResult InvalidUsers()
        {
            var data = RegUserCRUD.LoadUsers();
            List<RegUserViewModel> users = new List<RegUserViewModel>();
            foreach (var row in data)
            {
                if (row.Status == "Waiting")
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

            return View(users);
        }

        public ActionResult AllUsers()
        {
            // ova akcija ipak prikazuje samo one validirane korisnike
            var data = RegUserCRUD.LoadUsers();
            List<RegUserViewModel> users = new List<RegUserViewModel>();
            foreach (var row in data)
            {
                if (row.Status == "Validated" && row.Id != User.Identity.GetUserId())
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
            return View(users);
        }

        public ActionResult SearchUsers(string searching)
        {
            var data = RegUserCRUD.LoadUsers();
            List<RegUserViewModel> found_users = new List<RegUserViewModel>();
            searching = searching.ToLower();

            foreach (var user in data)
            {
                if(user.Status == "Validated")
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

        public ActionResult ValidateUser(string id, string email, string status, string type)
        {
            var gmail = new Gmail {
                To = email,
                Subject = "Account is validated",
                Body = @"Your account is validated, so you can sign in now.
                Thank you for your support.

                Best regards,
                Admin team."

            };
            gmail.SendEmail();
            RegUserCRUD.UpdateUserStatus(email, status);
            if(type.Equals("FishingInstructor"))
                RegUserCRUD.SetRoleInDB(id, Enums.RegistrationTypeInDB.ValidFishingInstructor);
            else if(type.Equals("CottageOwner"))
                RegUserCRUD.SetRoleInDB(id, Enums.RegistrationTypeInDB.ValidCottageOwner);
            else if(type.Equals("ShipOwner"))
                RegUserCRUD.SetRoleInDB(id, Enums.RegistrationTypeInDB.ValidShipOwner);
            else if(type.Equals("Administrator"))
                RegUserCRUD.SetRoleInDB(id, Enums.RegistrationTypeInDB.InvalidAdmin);

            return RedirectToAction("InvalidUsers", "AdminUsers");
        }

        public ActionResult RejectUser(string email)
        {
            var gmail = new Gmail
            {
                To = email,
                Subject = "Account is rejected",
                Body = @"Unfortunately, Your account is rejected.Try again with a different information.
                We hope you have an understanding.

                Best regards,
                Admin team."

            };
            gmail.SendEmail();
            int i = RegUserCRUD.DeleteUserByEmail(email);
            return RedirectToAction("InvalidUsers","AdminUsers");
        }

        public ActionResult DeleteUser(string email)
        {
            RegUserCRUD.DeleteUserByEmail(email);
            return RedirectToAction("AllUsers", "AdminUsers");
        }

        public ActionResult BlockUser(string email, string status)
        {
            RegUserCRUD.UpdateUserStatus(email, status);
            return RedirectToAction("AllUsers", "AdminUsers");
        }

        public ActionResult ViewRecords()
        {
            List<Record> data_records = RecordCRUD.LoadRecords();
            List<RecordViewModel> records = new List<RecordViewModel>();
            foreach (var record in data_records)
            {
                if(record.ImpressionType == Enums.RecordImpressionType.BadExperience)
                {
                    records.Add(new RecordViewModel
                    {
                        Id = record.Id,
                        ClientsEmailAddress = record.ClientsEmailAddress,
                        InstructorsEmailAddress = record.InstructorsEmailAddress,
                        Comment = record.Comment,
                        ImpressionType = record.ImpressionType,
                        ClientId = record.ClientId,
                        InstructorId = record.InstructorId
                    });
                }
            }
            return View(records);
        }

        public ActionResult InformAboutAcceptedPenalty(int recordId, string clientEmail, string instructorEmail)
        {

            var data_user = RegUserCRUD.LoadUsers().Find(x => x.EmailAddress == clientEmail);
            RegUserCRUD.AddPenalty(data_user.EmailAddress, data_user.Penalties + 1);
            try
            {
                var gmailToClient = new Gmail
                {
                    To = clientEmail,
                    Subject = "Penalty accepted",
                    Body = @"We received a report of your indecent behavior from the instructor who was in charge. 
                For that reason, we punish you with one penalty.

                Best regards,
                Admin team."

                };
                gmailToClient.SendEmail();

                var gmailToInstructor = new Gmail
                {
                    To = instructorEmail,
                    Subject = "Penalty accepted",
                    Body = @"An incident you reported with one of your clients was accepted and he is punished by penalty.

                Best regards,
                Admin team."
                };
                gmailToInstructor.SendEmail();

                RecordCRUD.DeleteRecord(recordId);

                return RedirectToAction("ViewRecords", "AdminUsers");

            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }

        public ActionResult InformAboutDeclinedPenalty(int recordId, string clientEmail, string instructorEmail)
        {

            try
            {
                var gmailToClient = new Gmail
                {
                    To = clientEmail,
                    Subject = "Penalty declined",
                    Body = @"We received a report of your indecent behavior from the instructor who was in charge. 
                But we will not punish you with a penalty because the reason is not too serious

                Best regards,
                Admin team."

                };
                gmailToClient.SendEmail();

                var gmailToInstructor = new Gmail
                {
                    To = instructorEmail,
                    Subject = "Penalty declined",
                    Body = @"An incident you reported with one of your clients was not accepted because the reason is not too serious.

                Best regards,
                Admin team."
                };
                gmailToInstructor.SendEmail();

                RecordCRUD.DeleteRecord(recordId);

                return RedirectToAction("ViewRecords", "AdminUsers");

            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        public ActionResult DetailsAboutUser(string userId)
        {
            DetailsAboutUserViewModel user_details = new DetailsAboutUserViewModel();
            var data_user = RegUserCRUD.LoadUsers().Find(x => x.Id == userId);
            var data_adventures = AdventureCRUD.LoadAdventures();

            user_details.user.UserId = data_user.Id;
            user_details.user.Name = data_user.Name;
            user_details.user.Surname = data_user.Surname;
            user_details.user.PhoneNumber = data_user.PhoneNumber;
            user_details.user.EmailAddress = data_user.EmailAddress;
            user_details.user.Type = data_user.Type;
            user_details.user.Address = data_user.Address;
            user_details.user.City = data_user.City;
            user_details.user.Country = data_user.Country;
            user_details.user.Description = data_user.Description;
            user_details.user.Biography = data_user.Biography;

            

            foreach (var row in data_adventures)
            {
                if (row.InstructorId == userId)
                {
                    string[] address_split = row.Address.Split(',');
                    //string address = address_split[0] + " " + address_split[1] + "," + address_split[2];
                    user_details.adventures.Add(new AdventureViewModel
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

            return View(user_details);
        }
    }
}