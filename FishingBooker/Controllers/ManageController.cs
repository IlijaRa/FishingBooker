using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FishingBooker.Models;
using FishingBookerLibrary.BusinessLogic;
using FishingBookerLibrary.Models;
using System.Globalization;
using System.Collections.Generic;
using System.Data.Entity;

namespace FishingBooker.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            decimal totalIncome = 0;
            int percentage = MoneyFlowCRUD.LoadMoneyFlow().Percentage;

            //var cottages = CottageCRUD.LoadCottages();
            var cottage_reservations = ReservationCRUD.LoadCottageReservations();

            //var adventures = AdventureCRUD.LoadAdventures();
            var adventure_reservations = ReservationCRUD.LoadAdventureReservations();

            //var ships = ShipCRUD.LoadShips();
            var ship_reservations = ReservationCRUD.LoadShipReservations();

            foreach (var reservation in cottage_reservations)
            {
                totalIncome = totalIncome + (reservation.Price * Convert.ToDecimal(percentage)/100);
            }

            foreach (var reservation in adventure_reservations)
            {
                totalIncome = totalIncome + (reservation.Price * Convert.ToDecimal(percentage) / 100);
            }

            foreach (var reservation in ship_reservations)
            {
                totalIncome = totalIncome + (reservation.Price * Convert.ToDecimal(percentage) / 100);
            }

            var userId = User.Identity.GetUserId();
            var data_owner = RegUserCRUD.LoadUserById(userId);
            var loyalty_scales = LoyaltyProgramCRUD.LoadLoyaltyScales();
            var loyalty_scale = new LoyaltyScale();
            var loyalty_scale_viewmodel = new LoyaltyScaleViewModel();
            //string loyaltyClass = "";

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
                if (scale.MinEarnedPoints <= data_owner.TotalScalePoints)
                {
                    loyalty_scale = scale;
                }
            }

            loyalty_scale_viewmodel.Id = loyalty_scale.Id;
            loyalty_scale_viewmodel.ScaleName = loyalty_scale.ScaleName;
            loyalty_scale_viewmodel.ClientsBenefits = loyalty_scale.ClientsBenefits;
            loyalty_scale_viewmodel.OwnerBenefits = loyalty_scale.OwnerBenefits;
            loyalty_scale_viewmodel.MinEarnedPoints = loyalty_scale.MinEarnedPoints;
            loyalty_scale_viewmodel.PickedColor = loyalty_scale.PickedColor;

            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
                LoyaltyClass = loyalty_scale_viewmodel,
                Percentage = percentage,
                TotalIncome = totalIncome
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMoneyFlow(IndexViewModel model) 
        {
            if (ModelState.IsValid)
            {
                MoneyFlowCRUD.UpdatePercentage(model.Percentage);
                return RedirectToAction("Index");
            }

            return View();
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var users = RegUserCRUD.LoadUsers();
            string user_type = "";

            foreach (var user in users)
            {
                if (user.EmailAddress == User.Identity.GetUserName())
                {
                    user_type = user.Type;
                    break;
                }
            }
            if(user_type == "Administrator")
            {
                if (model.NewPassword == "Admin123*")
                {
                    return View(model);
                }
                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    UserRoleCRUD.UpdateRoleInDB(user.Id, Enums.RegistrationTypeInDB.Admin);
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", "Home", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            else
            {
                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", "Home", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        // GET: /Manage/ChangeBasicInfo
        public ActionResult ChangeBasicInfo(string email)
        {
            var data = RegUserCRUD.LoadUsers();
            ChangeBasicInfoViewModel changeInfo = new ChangeBasicInfoViewModel();
            foreach (var row in data)
            {
                if (row.EmailAddress == email)
                {
                    changeInfo.Name = row.Name;
                    changeInfo.Surname = row.Surname;
                    changeInfo.PhoneNumber = row.PhoneNumber;
                    //changeInfo.EmailAddress = row.EmailAddress;
                    changeInfo.Address = row.Address;
                    changeInfo.City = row.City;
                    changeInfo.Country = row.Country;
                    changeInfo.Biography = row.Biography;
                    break;
                }

            }
            return View(changeInfo);
        }

        //
        // POST: /Manage/ChangeBasicInfo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeBasicInfo(ChangeBasicInfoViewModel model)
        {
            int i = RegUserCRUD.UpdateUserBasicInfo(model.Name,
                                            model.Surname,
                                            model.PhoneNumber,
                                            User.Identity.Name,
                                            model.Address,
                                            model.City,
                                            model.Country,
                                            model.Biography);

            if (i == 1)
                return RedirectToAction("Index", "Manage");
            else
            {
                // If we got this far, something failed, redisplay form
                return View(model);
            }

        }

        public ActionResult DeactivateAccount()
        {
            var user = RegUserCRUD.LoadUsers().Find(x => x.Id == User.Identity.GetUserId());

            DeactivationRequestViewModel deactivate = new DeactivationRequestViewModel();
            deactivate.UserName = user.Name;
            deactivate.UserSurname = user.Surname;
            deactivate.EmailAddress = user.EmailAddress;
            deactivate.Reason = "";

            return View(deactivate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateAccount(DeactivationRequestViewModel model)
        {
            var user = RegUserCRUD.LoadUsers().Find(x => x.Id == User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                DeactivationRequestCRUD.SendDeactivationRequest(user.Name,
                                                user.Surname,
                                                user.EmailAddress,
                                                model.Reason);
            }
            return View("DeactivationRequestSuccessful");
        }

        public ActionResult InstructorSchedule()
        {
            InstructorScheduleViewModel schedule = new InstructorScheduleViewModel();
            //var data_availability = ScheduleCRUD.LoadOwnerAvailabilityForStandardReservation(User.Identity.GetUserId());
            var data_history_reservations = ReservationCRUD.LoadReservationsFromHistory();
            var data_adventures = AdventureCRUD.LoadAdventures();

            //if (data_availability != null)
            //{
            //    schedule.availability.Id = data_availability.Id;
            //    schedule.availability.FromDate = data_availability.FromDate.ToString("yyyy-MM-dd");
            //    schedule.availability.FromTime = data_availability.FromTime.ToString();
            //    schedule.availability.ToDate = data_availability.ToDate.ToString("yyyy-MM-dd");
            //    schedule.availability.ToTime = data_availability.ToTime.ToString();
            //    schedule.availability.InstructorId = data_availability.InstructorId;
            //}

            foreach (var adventure in data_adventures)
            {
                if(adventure.InstructorId == User.Identity.GetUserId())
                {
                    List<Reservation> reservations = ReservationCRUD.LoadReservationsByAdventureId(adventure.Id);
                    foreach (var reservation in reservations)
                    {
                        if(reservation.IsReserved == true)
                        {
                            schedule.current_reservations.Add(new CurrentReservationViewModel
                            {
                                Id = reservation.Id,
                                ClientsEmailAddress = reservation.ClientsEmailAddress,
                                ActionTitle = adventure.Title,
                                StartDate = reservation.StartDate,
                                StartTime = reservation.StartTime.ToString(),
                                EndDate = reservation.EndDate,
                                EndTime = reservation.EndTime.ToString(),
                                Price = reservation.Price,
                                OwnerId = User.Identity.GetUserId()
                            });
                        }
                    }
                }
            }

            foreach (var reservation in data_history_reservations)
            {
                if (reservation.OwnerId == User.Identity.GetUserId())
                {
                    schedule.reservation_history.Add(new ReservationFromHistoryViewModel
                    {
                        Id = reservation.Id,
                        ClientsEmailAddress = reservation.ClientsEmailAddress,
                        ActionTitle = reservation.ActionTitle,
                        StartDate = reservation.StartDate,
                        StartTime = reservation.StartTime.ToString(),
                        EndDate = reservation.EndDate,
                        EndTime = reservation.EndTime.ToString(),
                        Price = reservation.Price,
                        OwnerId = reservation.OwnerId,
                    });
                }
            }

            return View(schedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InstructorSchedule(InstructorScheduleViewModel model)
        {
            //TODO: osiguraj ako nije unet datum a klikunto save
            TimeSpan fromTime = TimeSpan.Parse(model.availability.FromTime.ToString());
            TimeSpan toTime = TimeSpan.Parse(model.availability.ToTime.ToString());
            DateTime fromDate = DateTime.Parse(model.availability.FromDate.ToString());
            DateTime toDate = DateTime.Parse(model.availability.ToDate.ToString());

            //var instructor_availability = ScheduleCRUD.LoadOwnerAvailabilityForStandardReservation(User.Identity.GetUserId());

            //if(instructor_availability == null)
            //{
            //    ScheduleCRUD.CreateOwnerAvailabilityForStandardReservation(fromDate,
            //                                    fromTime,
            //                                    toDate,
            //                                    toTime,
            //                                    User.Identity.GetUserId());

            //    return RedirectToAction("InstructorSchedule", "Manage");
            //}
            //else
            //{
            //    ScheduleCRUD.UpdateOwnerAvailabilityForStandardReservation(fromDate,
            //                                    fromTime,
            //                                    toDate,
            //                                    toTime,
            //                                    User.Identity.GetUserId());

            //    return RedirectToAction("InstructorSchedule", "Manage");
            //}
            return View();
            
        }

        public ActionResult SearchHistoryReservation(string searching)
        {
            var data = ReservationCRUD.LoadReservationsFromHistory();
            List<ReservationFromHistoryViewModel> found_reservations = new List<ReservationFromHistoryViewModel>();
            searching = searching.ToLower();

            foreach (var reservation in data)
            {
                if (reservation.OwnerId == User.Identity.GetUserId())
                {
                    decimal result = -1;
                    decimal.TryParse(searching, out result);
                    if (reservation.ClientsEmailAddress.ToLower().Contains(searching) ||
                        reservation.ActionTitle.ToLower().Contains(searching) ||
                        reservation.Price == result)
                    {
                        found_reservations.Add(new ReservationFromHistoryViewModel
                        {
                            ClientsEmailAddress = reservation.ClientsEmailAddress,
                            ActionTitle = reservation.ActionTitle,
                            StartDate = reservation.StartDate,
                            StartTime = reservation.StartTime.ToString(),
                            EndDate = reservation.EndDate,
                            EndTime = reservation.EndTime.ToString(),
                            Price = reservation.Price,
                            OwnerId = reservation.OwnerId
                        });
                    }
                }
            }

            InstructorScheduleViewModel schedule = new InstructorScheduleViewModel();
            //var data_availability = ScheduleCRUD.LoadOwnerAvailabilityForStandardReservation(User.Identity.GetUserId());
            //var availability = RegUserCRUD.LoadAvailabilities().Find(x => x.InstructorId == User.Identity.GetUserId());
            var data_history_reservations = ReservationCRUD.LoadReservationsFromHistory();
            var data_adventures = AdventureCRUD.LoadAdventures();

            //if(data_availability != null)
            //{
            //    schedule.availability.Id = data_availability.Id;
            //    schedule.availability.FromDate = data_availability.FromDate.ToString("yyyy-MM-dd");
            //    schedule.availability.FromTime = data_availability.FromTime.ToString();
            //    schedule.availability.ToDate = data_availability.ToDate.ToString("yyyy-MM-dd");
            //    schedule.availability.ToTime = data_availability.ToTime.ToString();
            //    schedule.availability.InstructorId = data_availability.InstructorId;
            //}


            foreach (var adventure in data_adventures)
            {
                if (adventure.InstructorId == User.Identity.GetUserId())
                {
                    List<Reservation> reservations = ReservationCRUD.LoadReservationsByAdventureId(adventure.Id);
                    foreach (var reservation in reservations)
                    {
                        if (reservation.IsReserved == true)
                        {
                            schedule.current_reservations.Add(new CurrentReservationViewModel
                            {
                                Id = reservation.Id,
                                ClientsEmailAddress = reservation.ClientsEmailAddress,
                                ActionTitle = adventure.Title,
                                StartDate = reservation.StartDate,
                                StartTime = reservation.StartTime.ToString(),
                                EndDate = reservation.EndDate,
                                EndTime = reservation.EndTime.ToString(),
                                Price = reservation.Price,
                                OwnerId = User.Identity.GetUserId()
                            });
                        }
                    }
                }
            }
            schedule.reservation_history = found_reservations;

            return View(schedule);
        }


        public ActionResult ShipOwnerSchedule()
        {
            InstructorScheduleViewModel schedule = new InstructorScheduleViewModel();
            //var data_availability = ScheduleCRUD.LoadOwnerAvailabilityForStandardReservation(User.Identity.GetUserId());
            var data_history_reservations = ReservationCRUD.LoadReservationsFromHistory();
            var data_ships = ShipCRUD.LoadShips();

            //if(data_availability != null)
            //{
            //    schedule.availability.Id = data_availability.Id;
            //    schedule.availability.FromDate = data_availability.FromDate.ToString("yyyy-MM-dd");
            //    schedule.availability.FromTime = data_availability.FromTime.ToString();
            //    schedule.availability.ToDate = data_availability.ToDate.ToString("yyyy-MM-dd");
            //    schedule.availability.ToTime = data_availability.ToTime.ToString();
            //    schedule.availability.InstructorId = data_availability.InstructorId;
            //}


            foreach (var ship in data_ships)
            {
                if (ship.OwnerId == User.Identity.GetUserId())
                {
                    List<Reservation> reservations = ReservationCRUD.LoadReservationsByShipId(ship.Id);
                    foreach (var reservation in reservations)
                    {
                        if (reservation.IsReserved == true)
                        {
                            schedule.current_reservations.Add(new CurrentReservationViewModel
                            {
                                Id = reservation.Id,
                                ClientsEmailAddress = reservation.ClientsEmailAddress,
                                ActionTitle = ship.Title,
                                StartDate = reservation.StartDate,
                                StartTime = reservation.StartTime.ToString(),
                                EndDate = reservation.EndDate,
                                EndTime = reservation.EndTime.ToString(),
                                Price = reservation.Price,
                                OwnerId = User.Identity.GetUserId()
                            });
                        }
                    }
                }
            }

            foreach (var reservation in data_history_reservations)
            {
                if (reservation.OwnerId == User.Identity.GetUserId())
                {
                    schedule.reservation_history.Add(new ReservationFromHistoryViewModel
                    {
                        Id = reservation.Id,
                        ClientsEmailAddress = reservation.ClientsEmailAddress,
                        ActionTitle = reservation.ActionTitle,
                        StartDate = reservation.StartDate,
                        StartTime = reservation.StartTime.ToString(),
                        EndDate = reservation.EndDate,
                        EndTime = reservation.EndTime.ToString(),
                        Price = reservation.Price,
                        OwnerId = reservation.OwnerId,
                    });
                }
            }

            return View(schedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShipOwnerSchedule(InstructorScheduleViewModel model)
        {
            TimeSpan fromTime = TimeSpan.Parse(model.availability.FromTime.ToString());
            TimeSpan toTime = TimeSpan.Parse(model.availability.ToTime.ToString());
            DateTime fromDate = DateTime.Parse(model.availability.FromDate.ToString());
            DateTime toDate = DateTime.Parse(model.availability.ToDate.ToString());

            //var ship_owner_availability = ScheduleCRUD.LoadOwnerAvailabilityForStandardReservation(User.Identity.GetUserId());

            //if (ship_owner_availability == null)
            //{
            //    ScheduleCRUD.CreateOwnerAvailabilityForStandardReservation(fromDate,
            //                                    fromTime,
            //                                    toDate,
            //                                    toTime,
            //                                    User.Identity.GetUserId());

            //    return RedirectToAction("ShipOwnerSchedule", "Manage");
            //}
            //else
            //{
            //    ScheduleCRUD.UpdateOwnerAvailabilityForStandardReservation(fromDate,
            //                                    fromTime,
            //                                    toDate,
            //                                    toTime,
            //                                    User.Identity.GetUserId());

            //    return RedirectToAction("ShipOwnerSchedule", "Manage");
            //}
            return View();
        }

        public ActionResult SearchHistoryReservationShipOwner(string searching)
        {
            var data = ReservationCRUD.LoadReservationsFromHistory();
            List<ReservationFromHistoryViewModel> found_reservations = new List<ReservationFromHistoryViewModel>();
            searching = searching.ToLower();

            foreach (var reservation in data)
            {
                if (reservation.OwnerId == User.Identity.GetUserId())
                {
                    decimal result = -1;
                    decimal.TryParse(searching, out result);
                    if (reservation.ClientsEmailAddress.ToLower().Contains(searching) ||
                        reservation.ActionTitle.ToLower().Contains(searching) ||
                        reservation.Price == result)
                    {
                        found_reservations.Add(new ReservationFromHistoryViewModel
                        {
                            ClientsEmailAddress = reservation.ClientsEmailAddress,
                            ActionTitle = reservation.ActionTitle,
                            StartDate = reservation.StartDate,
                            StartTime = reservation.StartTime.ToString(),
                            EndDate = reservation.EndDate,
                            EndTime = reservation.EndTime.ToString(),
                            Price = reservation.Price,
                            OwnerId = reservation.OwnerId
                        });
                    }
                }
            }

            InstructorScheduleViewModel schedule = new InstructorScheduleViewModel();
            //var data_availability = ScheduleCRUD.LoadOwnerAvailabilityForStandardReservation(User.Identity.GetUserId());
            var data_history_reservations = ReservationCRUD.LoadReservationsFromHistory();
            var data_ships = ShipCRUD.LoadShips();

            //if(data_availability != null)
            //{
            //    schedule.availability.Id = data_availability.Id;
            //    schedule.availability.FromDate = data_availability.FromDate.ToString("yyyy-MM-dd");
            //    schedule.availability.FromTime = data_availability.FromTime.ToString();
            //    schedule.availability.ToDate = data_availability.ToDate.ToString("yyyy-MM-dd");
            //    schedule.availability.ToTime = data_availability.ToTime.ToString();
            //    schedule.availability.InstructorId = data_availability.InstructorId;
            //}
            

            foreach (var ship in data_ships)
            {
                if (ship.OwnerId == User.Identity.GetUserId())
                {
                    List<Reservation> reservations = ReservationCRUD.LoadReservationsByShipId(ship.Id);
                    foreach (var reservation in reservations)
                    {
                        if (reservation.IsReserved == true)
                        {
                            schedule.current_reservations.Add(new CurrentReservationViewModel
                            {
                                Id = reservation.Id,
                                ClientsEmailAddress = reservation.ClientsEmailAddress,
                                ActionTitle = ship.Title,
                                StartDate = reservation.StartDate,
                                StartTime = reservation.StartTime.ToString(),
                                EndDate = reservation.EndDate,
                                EndTime = reservation.EndTime.ToString(),
                                Price = reservation.Price,
                                OwnerId = User.Identity.GetUserId()
                            });
                        }
                    }
                }
            }
            schedule.reservation_history = found_reservations;

            return View(schedule);
        }

        public ActionResult CottageOwnerSchedule()
        {
            InstructorScheduleViewModel schedule = new InstructorScheduleViewModel();
            //var data_availability = ScheduleCRUD.LoadOwnerAvailabilityForStandardReservation(User.Identity.GetUserId());
            var data_history_reservations = ReservationCRUD.LoadReservationsFromHistory();
            var data_cottages = CottageCRUD.LoadCottages();

            //if (data_availability != null)
            //{
            //    schedule.availability.Id = data_availability.Id;
            //    schedule.availability.FromDate = data_availability.FromDate.ToString("yyyy-MM-dd");
            //    schedule.availability.FromTime = data_availability.FromTime.ToString();
            //    schedule.availability.ToDate = data_availability.ToDate.ToString("yyyy-MM-dd");
            //    schedule.availability.ToTime = data_availability.ToTime.ToString();
            //    schedule.availability.InstructorId = data_availability.InstructorId;
            //}


            foreach (var cottage in data_cottages)
            {
                if (cottage.OwnerId == User.Identity.GetUserId())
                {
                    List<Reservation> reservations = ReservationCRUD.LoadReservationsByCottageId(cottage.Id);
                    foreach (var reservation in reservations)
                    {
                        if (reservation.IsReserved == true)
                        {
                            schedule.current_reservations.Add(new CurrentReservationViewModel
                            {
                                Id = reservation.Id,
                                ClientsEmailAddress = reservation.ClientsEmailAddress,
                                ActionTitle = cottage.Title,
                                StartDate = reservation.StartDate,
                                StartTime = reservation.StartTime.ToString(),
                                EndDate = reservation.EndDate,
                                EndTime = reservation.EndTime.ToString(),
                                Price = reservation.Price,
                                OwnerId = User.Identity.GetUserId()
                            });
                        }
                    }
                }
            }

            foreach (var reservation in data_history_reservations)
            {
                if (reservation.OwnerId == User.Identity.GetUserId())
                {
                    schedule.reservation_history.Add(new ReservationFromHistoryViewModel
                    {
                        Id = reservation.Id,
                        ClientsEmailAddress = reservation.ClientsEmailAddress,
                        ActionTitle = reservation.ActionTitle,
                        StartDate = reservation.StartDate,
                        StartTime = reservation.StartTime.ToString(),
                        EndDate = reservation.EndDate,
                        EndTime = reservation.EndTime.ToString(),
                        Price = reservation.Price,
                        OwnerId = reservation.OwnerId,
                    });
                }
            }

            return View(schedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CottageOwnerSchedule(InstructorScheduleViewModel model)
        {
            TimeSpan fromTime = TimeSpan.Parse(model.availability.FromTime.ToString());
            TimeSpan toTime = TimeSpan.Parse(model.availability.ToTime.ToString());
            DateTime fromDate = DateTime.Parse(model.availability.FromDate.ToString());
            DateTime toDate = DateTime.Parse(model.availability.ToDate.ToString());

            //var cottage_owner_availability = ScheduleCRUD.LoadOwnerAvailabilityForStandardReservation(User.Identity.GetUserId());

            //if (cottage_owner_availability == null)
            //{
            //    ScheduleCRUD.CreateOwnerAvailabilityForStandardReservation(fromDate,
            //                                    fromTime,
            //                                    toDate,
            //                                    toTime,
            //                                    User.Identity.GetUserId());

            //    return RedirectToAction("CottageOwnerSchedule", "Manage");
            //}
            //else
            //{
            //    ScheduleCRUD.UpdateOwnerAvailabilityForStandardReservation(fromDate,
            //                                    fromTime,
            //                                    toDate,
            //                                    toTime,
            //                                    User.Identity.GetUserId());

            //    return RedirectToAction("CottageOwnerSchedule", "Manage");
            //}
            return View();
        }

        public ActionResult SearchHistoryReservationCottageOwner(string searching)
        {
            var data = ReservationCRUD.LoadReservationsFromHistory();
            List<ReservationFromHistoryViewModel> found_reservations = new List<ReservationFromHistoryViewModel>();
            searching = searching.ToLower();

            foreach (var reservation in data)
            {
                if (reservation.OwnerId == User.Identity.GetUserId())
                {
                    decimal result = -1;
                    decimal.TryParse(searching, out result);
                    if (reservation.ClientsEmailAddress.ToLower().Contains(searching) ||
                        reservation.ActionTitle.ToLower().Contains(searching) ||
                        reservation.Price == result)
                    {
                        found_reservations.Add(new ReservationFromHistoryViewModel
                        {
                            ClientsEmailAddress = reservation.ClientsEmailAddress,
                            ActionTitle = reservation.ActionTitle,
                            StartDate = reservation.StartDate,
                            StartTime = reservation.StartTime.ToString(),
                            EndDate = reservation.EndDate,
                            EndTime = reservation.EndTime.ToString(),
                            Price = reservation.Price,
                            OwnerId = reservation.OwnerId
                        });
                    }
                }
            }

            InstructorScheduleViewModel schedule = new InstructorScheduleViewModel();
            //var data_availability = ScheduleCRUD.LoadOwnerAvailabilityForStandardReservation(User.Identity.GetUserId());
            var data_history_reservations = ReservationCRUD.LoadReservationsFromHistory();
            var data_cottages = CottageCRUD.LoadCottages();

            //if(data_availability != null)
            //{
            //    schedule.availability.Id = data_availability.Id;
            //    schedule.availability.FromDate = data_availability.FromDate.ToString("yyyy-MM-dd");
            //    schedule.availability.FromTime = data_availability.FromTime.ToString();
            //    schedule.availability.ToDate = data_availability.ToDate.ToString("yyyy-MM-dd");
            //    schedule.availability.ToTime = data_availability.ToTime.ToString();
            //    schedule.availability.InstructorId = data_availability.InstructorId;
            //}


            foreach (var cottage in data_cottages)
            {
                if (cottage.OwnerId == User.Identity.GetUserId())
                {
                    List<Reservation> reservations = ReservationCRUD.LoadReservationsByCottageId(cottage.Id);
                    foreach (var reservation in reservations)
                    {
                        if (reservation.IsReserved == true)
                        {
                            schedule.current_reservations.Add(new CurrentReservationViewModel
                            {
                                Id = reservation.Id,
                                ClientsEmailAddress = reservation.ClientsEmailAddress,
                                ActionTitle = cottage.Title,
                                StartDate = reservation.StartDate,
                                StartTime = reservation.StartTime.ToString(),
                                EndDate = reservation.EndDate,
                                EndTime = reservation.EndTime.ToString(),
                                Price = reservation.Price,
                                OwnerId = User.Identity.GetUserId()
                            });
                        }
                    }
                }
            }
            schedule.reservation_history = found_reservations;

            return View(schedule);
        }


        public ActionResult ClientProfileInfo(string email)
        {
            var data_user = RegUserCRUD.LoadUsers().Find(x => x.EmailAddress == email);
            ClientProfileInfoViewModel data = new ClientProfileInfoViewModel()
            {
                Name = data_user.Name,
                Surname = data_user.Surname,
                PhoneNumber = data_user.PhoneNumber,
                EmailAddress = data_user.EmailAddress,
                Address = data_user.Address,
                City = data_user.City,
                Country = data_user.Country,
                Biography = data_user.Biography
            };
            return View(data);
        }

        public ActionResult FillARecord(string clientsEmail)
        {
            RecordViewModel record = new RecordViewModel();

            record.ClientsEmailAddress = clientsEmail;
            record.InstructorsEmailAddress = RegUserCRUD.LoadUsers().Find(x => x.Id == User.Identity.GetUserId()).EmailAddress;
            record.Comment = "";
            record.ClientId = RegUserCRUD.LoadUsers().Find(x => x.EmailAddress == clientsEmail).Id;
            record.InstructorId = User.Identity.GetUserId();

            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillARecord(RecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.ImpressionType == Enums.RecordImpressionType.DidNotShowUp)
                {
                    var data_user = RegUserCRUD.LoadUsers().Find(x => x.EmailAddress == model.ClientsEmailAddress);
                    RegUserCRUD.AddPenalty(model.ClientsEmailAddress, data_user.Penalties + 1);
                    RecordCRUD.CreateRecord(model.ClientsEmailAddress,
                                            model.InstructorsEmailAddress,
                                            model.Comment,
                                            model.ImpressionType,
                                            model.ClientId,
                                            model.InstructorId);
                }
                else if (model.ImpressionType == Enums.RecordImpressionType.BadExperience || model.ImpressionType == Enums.RecordImpressionType.GoodExperience)
                {
                    RecordCRUD.CreateRecord(model.ClientsEmailAddress,
                                            model.InstructorsEmailAddress,
                                            model.Comment,
                                            model.ImpressionType,
                                            model.ClientId,
                                            model.InstructorId);

                    //if (User.IsInRole("ValidFishingInstructor"))
                    //    return RedirectToAction("InstructorSchedule", "Manage");
                    //else if (User.IsInRole("ValidCottageOwner"))
                    //    return RedirectToAction("CottageOwnerSchedule", "Manage");
                    //else if (User.IsInRole("ValidShipOwner"))
                    //    return RedirectToAction("ShipOwnerSchedule", "Manage");
                }
                if (User.IsInRole("ValidFishingInstructor"))
                    return RedirectToAction("InstructorSchedule", "Manage");
                else if (User.IsInRole("ValidCottageOwner"))
                    return RedirectToAction("CottageOwnerSchedule", "Manage");
                else if (User.IsInRole("ValidShipOwner"))
                    return RedirectToAction("ShipOwnerSchedule", "Manage");
            }
            return RedirectToAction("FillARecord", "Manage", new { clientsEmail = model.ClientsEmailAddress });
            //return View();
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}