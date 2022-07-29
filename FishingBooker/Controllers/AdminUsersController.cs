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
                        Description = row.Description,
                        Biography = row.Biography
                    });
                }

            }

            return View(users);
        }

        //[AllowCrossSiteJson]
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
                UserRoleCRUD.SetRoleInDB(id, Enums.RegistrationTypeInDB.ValidFishingInstructor);
            else if(type.Equals("CottageOwner"))
                UserRoleCRUD.SetRoleInDB(id, Enums.RegistrationTypeInDB.ValidCottageOwner);
            else if(type.Equals("ShipOwner"))
                UserRoleCRUD.SetRoleInDB(id, Enums.RegistrationTypeInDB.ValidShipOwner);
            else if(type.Equals("Administrator"))
                UserRoleCRUD.SetRoleInDB(id, Enums.RegistrationTypeInDB.InvalidAdmin);
            else if (type.Equals("Client"))
                UserRoleCRUD.SetRoleInDB(id, Enums.RegistrationTypeInDB.ValidClient);

            return RedirectToAction("InvalidUsers", "AdminUsers");
        }

        public ActionResult RejectUserRegistration(string emailToSend)
        {
            Gmail gmail = new Gmail();
            gmail.To = emailToSend;

            return View(gmail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RejectUserRegistration(Gmail model)
        {
            if(ModelState.IsValid)
            {
                var gmail = new Gmail
                {
                    To = model.To,
                    Subject = model.Subject,
                    Body = model.Body
                };
                gmail.SendEmail();
                int i = RegUserCRUD.DeleteUserByEmail(model.To);
                return RedirectToAction("InvalidUsers", "AdminUsers");
            }
            else
            {
                return View();
            }

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
            var data_cottages = CottageCRUD.LoadCottages();
            var data_ships = ShipCRUD.LoadShips();

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

            if (data_user.Type == "Client")
            {
                return View("DetailsAboutUser", user_details);
            }

            else if (data_user.Type == "Administrator")
            {
                return View("DetailsAboutUser", user_details);
            }

            else if (data_user.Type == "FishingInstructor")
            {
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
                            //Price = row.Price,
                            MaxNumberOfPeople = row.MaxNumberOfPeople,
                            FishingEquipment = row.FishingEquipment,
                            CancellationPolicy = row.CancellationPolicy
                        });
                    }
                }

                return View("DetailsAboutInstructor", user_details);
            }

            else if (data_user.Type == "CottageOwner")
            {
                foreach (var row in data_cottages)
                {
                    if (row.OwnerId == userId)
                    {
                        string[] address_split = row.Address.Split(',');
                        //string address = address_split[0] + " " + address_split[1] + "," + address_split[2];
                        user_details.cottages.Add(new CottageViewModel
                        {
                            CottageId = row.Id,
                            Title = row.Title,
                            Street = address_split[0],
                            AddressNumber = address_split[1],
                            City = address_split[2],
                            PromotionDescription = row.PromotionDescription,
                            BehaviourRules = row.BehaviourRules,
                            AdditionalServices = row.AdditionalServices,
                            Pricelist = row.Pricelist,
                            NumberOfRooms = row.NumberOfRooms,
                            BedsPerRoom = row.BedsPerRoom
                        });
                    }
                }
                return View("DetailsAboutCottageOwner", user_details);
            }

            else if (data_user.Type == "ShipOwner")
            {
                foreach (var row in data_ships)
                {
                    if (row.OwnerId == userId)
                    {
                        string[] address_split = row.Address.Split(',');
                        //string address = address_split[0] + " " + address_split[1] + "," + address_split[2];
                        user_details.ships.Add(new ShipViewModel
                        {
                            ShipId = row.Id,
                            Title = row.Title,
                            Street = address_split[0],
                            AddressNumber = address_split[1],
                            City = address_split[2],
                            PromotionDescription = row.PromotionDescription,
                            BehaviourRules = row.BehaviourRules,
                            AdditionalServices = row.AdditionalServices,
                            Pricelist = row.Pricelist,
                            FishingEquipment = row.FishingEquipment,
                            NavigationEquipment = row.NavigationEquipment,
                            CancellationPolicy = row.CancelationPolicy,
                            SpecificationId = row.SpecificationId,
                            OwnerId = row.OwnerId
                        });
                    }
                }
                return View("DetailsAboutShipOwner", user_details);
            }
            
            return View(user_details);
        }

        [HttpGet]
        public ActionResult DetailsAboutInstructor(DetailsAboutUserViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult DetailsAboutCottageOwner(DetailsAboutUserViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult DetailsAboutShipOwner(DetailsAboutUserViewModel model)
        {
            return View(model);
        }

        public ActionResult ViewDeactivationRequests()
        {
            
            var data_requests = DeactivationRequestCRUD.LoadDeactivationRequests();
            List<DeactivationRequestViewModel> deactivation_requests = new List<DeactivationRequestViewModel>();

            foreach (var request in data_requests)
            {
                deactivation_requests.Add(new DeactivationRequestViewModel
                {
                    UserName = request.UserName,
                    UserSurname = request.UserSurname,
                    EmailAddress = request.EmailAddress,
                    Reason = request.Reason
                });
            }

            return View(deactivation_requests);
        }

        public ActionResult AcceptedDeactivationEmailForm(string emailToSend)
        {
            Gmail gmail = new Gmail();
            gmail.To = emailToSend;
            return View(gmail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptedDeactivationEmailForm(Gmail model)
        {
            if (ModelState.IsValid)
            {
                var gmail = new Gmail
                {
                    To = model.To,
                    Subject = model.Subject,
                    Body = model.Body
                };

                gmail.SendEmail();
                UserRoleCRUD.DeleteUserInUserRole(RegUserCRUD.LoadUsers().Find(x => x.EmailAddress.Equals(model.To)).Id);
                int i = RegUserCRUD.DeleteUserByEmail(model.To);
                DeactivationRequestCRUD.DeleteDeactivationRequest(model.To);
                return RedirectToAction("ViewDeactivationRequests", "AdminUsers");
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult RejectedDeactivationEmailForm(string emailToSend)
        {
            Gmail gmail = new Gmail();
            gmail.To = emailToSend;
            return View(gmail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RejectedDeactivationEmailForm(Gmail model)
        {
            if(ModelState.IsValid)
            {
                var gmail = new Gmail
                {
                    To = model.To,
                    Subject = model.Subject,
                    Body = model.Body
                };

                gmail.SendEmail();
                DeactivationRequestCRUD.DeleteDeactivationRequest(model.To);
                return RedirectToAction("ViewDeactivationRequests", "AdminUsers");
            }
            else
            {
                return View();
            }

        }

        public ActionResult ReadClientComplaints()
        {
            var data_complaints = ClientComplaintCRUD.LoadClientComplaints();
            List<ClientComplaintViewModel> client_complaints = new List<ClientComplaintViewModel>();

            foreach (var complaint in data_complaints)
            {
                client_complaints.Add(new ClientComplaintViewModel
                {
                    Id = complaint.Id,
                    OwnerId = complaint.OwnerId,
                    OwnerName = complaint.OwnerName,
                    OwnerSurname = complaint.OwnerSurname,
                    OwnerEmailAddress = complaint.OwnerEmailAddress,
                    ClientsEmailAddress = complaint.ClientsEmailAddress,
                    SelectedActionTitle = complaint.ActionTitle,
                    Reason = complaint.Reason
                });
            }

            return View(client_complaints);
        }

        public ActionResult AnswerToComplaint(int complaintId, string clientsEmail, string ownersEmail)
        {
            AnswerToComplaintViewModel answer = new AnswerToComplaintViewModel();

            answer.complaintId = complaintId;
            answer.client_gmail.To = clientsEmail;
            answer.owner_gmail.To = ownersEmail;
            return View(answer);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerToComplaint(AnswerToComplaintViewModel model)
        {
            if(ModelState.IsValid)
            {
                var client_gmail = new Gmail
                {
                    To = model.client_gmail.To,
                    Subject = model.client_gmail.Subject,
                    Body = model.client_gmail.Body
                };
                client_gmail.SendEmail();
                
                var owner_gmail = new Gmail
                {
                    To = model.owner_gmail.To,
                    Subject = model.owner_gmail.Subject,
                    Body = model.owner_gmail.Body
                };
                owner_gmail.SendEmail();

                ClientComplaintCRUD.DeleteClientComplaintById(model.complaintId);
                return RedirectToAction("ReadClientComplaints", "AdminUsers");
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult ReadRevisions()
        {
            var data_revisions = RevisionCRUD.LoadUnconfirmedRevisions();

            List<RevisionViewModel> revisions = new List<RevisionViewModel>();
            foreach (var revision in data_revisions)
            {
                revisions.Add(new RevisionViewModel { 
                    Id = revision.Id,
                    ClientsEmailAddress = revision.ClientsEmailAddress,
                    EntityTitle = revision.EntityTitle,
                    OwnersEmailAddress = revision.OwnersEmailAddress,
                    Description = revision.Description,
                    ActionRating = revision.ActionRating,
                    OwnerInstructorRating = revision.OwnerInstructorRating
                });
            }
            return View(revisions);
        }

        public ActionResult ConfirmRevision(int id, string ownersEmail, string entityTitle, int actionRating , int ownerRating)
        {
            try
            {
                var user = RegUserCRUD.LoadUsers().Find(x => x.EmailAddress == ownersEmail);
                user.RatingSum = user.RatingSum + ownerRating;
                user.RatingCount = user.RatingCount + 1;
                user.Rating = user.RatingSum / user.RatingCount;
                RegUserCRUD.UpdateRating(user.Id, user.Rating, user.RatingSum, user.RatingCount);

                if (user.Type.Equals("FishingInstructor"))
                {
                    var adventure = AdventureCRUD.LoadAdventures().Find(x => x.Title == entityTitle);
                    adventure.RatingSum = adventure.RatingSum + actionRating;
                    adventure.RatingCount = adventure.RatingCount + 1;
                    adventure.Rating = adventure.RatingSum / adventure.RatingCount;
                    AdventureCRUD.UpdateRating(adventure.Id, adventure.Rating, adventure.RatingSum, adventure.RatingCount);
                }
                else if (user.Type.Equals("CottageOwner"))
                {
                    var cottage = CottageCRUD.LoadCottages().Find(x => x.Title == entityTitle);
                    cottage.RatingSum = cottage.RatingSum + actionRating;
                    cottage.RatingCount = cottage.RatingCount + 1;
                    cottage.Rating = cottage.RatingSum / cottage.RatingCount;
                    CottageCRUD.UpdateRating(cottage.Id, cottage.Rating, cottage.RatingSum, cottage.RatingCount);
                }
                else if (user.Type.Equals("ShipOwner"))
                {
                    var ship = ShipCRUD.LoadShips().Find(x => x.Title == entityTitle);
                    ship.RatingSum = ship.RatingSum + actionRating;
                    ship.RatingCount = ship.RatingCount + 1;
                    ship.Rating = ship.RatingSum / ship.RatingCount;
                    ShipCRUD.UpdateRating(ship.Id, ship.Rating, ship.RatingSum, ship.RatingCount);
                }

                var gmail = new Gmail
                {
                    To = ownersEmail,
                    Subject = "New rating from a client",
                    Body = @"Dear, 
                         you have a new rating from your client.
                         Best wishes,
                         Admin team."
                };
                gmail.SendEmail();
                RevisionCRUD.UpdateStatus(id);
                return RedirectToAction("ReadRevisions", "AdminUsers");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult DenyRevision(int id)
        {
            try
            {
                RevisionCRUD.DeleteRevisionById(id);
            }
            catch
            {
                Console.WriteLine("Greska");
            }
            return RedirectToAction("ReadRevisions", "AdminUsers");
        }

        public ActionResult LoyaltyProgram()
        {
            LoyaltyProgramViewModel model = new LoyaltyProgramViewModel();
            var loyalty_program = LoyaltyProgramCRUD.LoadLoyaltyProgram(1);
            loyalty_program.scales = LoyaltyProgramCRUD.LoadLoyaltyScales();

            model.Id = loyalty_program.Id;
            model.PointsAfterSuccResClient = loyalty_program.PointsAfterSuccResClient;
            model.PointsAfterSuccResOwner = loyalty_program.PointsAfterSuccResOwner;

            foreach (var scale in loyalty_program.scales)
            {
                model.scales.Add(new LoyaltyScaleViewModel { 
                    Id = scale.Id,
                    ScaleName = scale.ScaleName,
                    ClientsBenefits = scale.ClientsBenefits,
                    OwnerBenefits = scale.OwnerBenefits,
                    MinEarnedPoints = scale.MinEarnedPoints
                });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveLoyaltyProgram(LoyaltyProgramViewModel model)
        {
            LoyaltyProgramCRUD.UpdateLoyaltyProgram(model.PointsAfterSuccResClient, model.PointsAfterSuccResOwner);
            return RedirectToAction("LoyaltyProgram", "AdminUsers");

            return View("Erorr");
        }


        [HttpGet]
        public ActionResult CreateScale()
        {
            LoyaltyScaleViewModel model = new LoyaltyScaleViewModel();
            model.LoyaltyProgramId = 1; // uvek ce biti ovaj id, jer ce postojati 1 program samo ce se azurirati itd...
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateScale(LoyaltyScaleViewModel model)
        {
            if(ModelState.IsValid)
            {
                LoyaltyProgramCRUD.CreateScale(model.ScaleName, model.ClientsBenefits, model.OwnerBenefits, model.MinEarnedPoints);
                return RedirectToAction("LoyaltyProgram", "AdminUsers");
            }

            return View();
        }

        public ActionResult DeleteScale(int scaleId)
        {
            if (ModelState.IsValid)
            {
                int i = LoyaltyProgramCRUD.DeleteScale(scaleId);
                if(i == 1)
                    return RedirectToAction("LoyaltyProgram", "AdminUsers");
                else
                    return View("Error");
            }

            return View();
        }
    }
}