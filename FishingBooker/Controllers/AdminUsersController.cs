using FishingBooker.Models;
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
                if (row.Status == "Validated")
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

        public ActionResult BlockUser(string email, string status)
        {
            RegUserCRUD.UpdateUserStatus(email, status);
            return RedirectToAction("AllUsers", "AdminUsers");
        }

        public ActionResult RegisterAdmin()
        {
            return View();
        }

        public ActionResult ViewRecords()
        {
            List<Record> data_records = ScheduleCRUD.LoadRecords();
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

        public ActionResult InformAboutAcceptedPenalty(string clientId, string instructorId)
        {

            var data_user = RegUserCRUD.LoadUsers().Find(x => x.Id == clientId);
            RegUserCRUD.AddPenalty(data_user.EmailAddress, data_user.Penalties + 1);

            var gmailToClient = new Gmail
            {
                To = data_user.EmailAddress,
                Subject = "Penalty",
                Body = @"We received a report of your indecent behavior from the instructor who was in charge. 
                         For that reason, we punish you with one penalty.

                Best regards,
                Admin team."

            };
            gmailToClient.SendEmail();

            var gmailToInstructor = new Gmail
            {
                To = User.Identity.GetUserName(),
                Subject = "Penalty",
                Body = @"An incident you reported with one of your clients was accepted and he was punished with penalty.

                Best regards,
                Admin team."
            };
            gmailToInstructor.SendEmail();

            return RedirectToAction("ViewRecords", "AdminUsers");
        }
    }
}