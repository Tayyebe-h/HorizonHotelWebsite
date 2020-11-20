using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Data;
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
        private readonly IAdminBookingRepository _adminBookingRepository;



        public ManageController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            DataBaseContext dataBaseContext,
            ILogger<ManageController> logger,
            IAdminBookingRepository adminBookingRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataBaseContext = dataBaseContext;
            _logger = logger;
            _adminBookingRepository = adminBookingRepository;
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
                Email = user.Email
            };
            return View(model);
        }

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


        [HttpGet]
        public async Task<IActionResult> Bookings()
        {

            //var user = _adminUserRepository.GetById(id);
            //return View(user);
            var user = await _userManager.GetUserAsync(User);
            var booking = _dataBaseContext.Bookings.Where(s => s.Id == user.Id).SingleOrDefault();

            var model = new BookingViewModel
            {
                Id = booking.Id,
                Email = user.Email,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut
            };

            //var  = _dataBaseContext.Bookings.(u => u.Id == id);


            return View(user);
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
