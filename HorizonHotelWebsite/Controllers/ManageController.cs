using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Models.Repositories;
using HorizonHotelWebsite.ViewsModels;
using HorizonHotelWebsite.ViewsModels.ManagesViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorizonHotelWebsite.Controllers
{
    public class ManageController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataBaseContext _dataBaseContext;
        private readonly ILogger _logger;
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IAdminBookingRepository _BookingRepository;



        public ManageController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            DataBaseContext dataBaseContext,
            ILogger<ManageController> logger,
            IAdminUserRepository adminUserRepository,
            IAdminBookingRepository BookingRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataBaseContext = dataBaseContext;
            _logger = logger;
            _adminUserRepository = adminUserRepository;
            _BookingRepository = BookingRepository;
        }


        [TempData]
        public string StatusMessage { get; set; }

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return NotFound($"Unable to load user with ID'{_userManager.GetUserId(User)}'.");
            }
            var model = new IndexViewModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                StatusMessage = StatusMessage
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var applicationUser = _dataBaseContext.Userss.Where(s => s.Id == user.Id).SingleOrDefault();
            var userName = user.UserName;
            if(model.UserName != userName && model.UserName.Length > 0)
            {
                user.UserName = model.UserName;
            }

            var firstName = applicationUser.FirstName;
            if (model.FirstName != firstName && model.FirstName.Length > 0)
            {
                applicationUser.FirstName = model.FirstName;
            }

            var lastName = applicationUser.LastName;
            if (model.LastName != lastName && model.LastName.Length > 0)
            {
                applicationUser.LastName = model.LastName;
            }

            await _dataBaseContext.SaveChangesAsync();
            StatusMessage = "Your profile has been updated";

            return RedirectToAction(nameof(Index));
        }

        #endregion 


        #region ChangePassword

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

            }
            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }
            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User Changed Password");
            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(ChangePassword));

        }

        #endregion 


        #region SetPassword
        [HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var model = new SetPasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                AddErrors(addPasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Your password has been set.";

            return RedirectToAction(nameof(SetPassword));
        }

        #endregion


        #region Booking List

        [HttpGet]
        public async Task<ActionResult> Bookings()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID'{_userManager.GetUserId(User)}'.");
            }
            else
            {
                user = _adminUserRepository.GetById(user.Id);
            }
            return View(user);
        }

        #endregion

        #region Cancel Bookings

        [HttpGet]
        public async Task<ActionResult> CancelBookings()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user = _adminUserRepository.GetById(user.Id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelBookings(Booking booking)
        {
            
            if(DateTime.Today == booking.CheckIn)
            {
                return NotFound();
            }
            else
            {
                _BookingRepository.Delete(booking);
            }

            return RedirectToAction(nameof(Bookings));
        }

        #endregion

        #region AddErrors
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion

    }
}
