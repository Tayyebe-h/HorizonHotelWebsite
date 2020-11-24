using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.ViewsModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorizonHotelWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, DataBaseContext dataBaseContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataBaseContext = dataBaseContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if(ModelState.IsValid == false)
            {
                return View(register);
            }
            var Admin = _dataBaseContext.Userss.Where(u => u.Role == "Admin").FirstOrDefault();
            string role;
            if (Admin == null)
            {
                role = "Admin";
            }
            else
            {
                role = "Customer";
            }
            User user = new User()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                UserName = register.Email,
                Role = role,

            };

           
            var result = _userManager.CreateAsync(user, register.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("UserRegisted");
            }

            string message = "";
            foreach(var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;
            return View(register);
            
        }
        public IActionResult UserRegisted()
        {
            ViewBag.UserCreatedMessage = "You are successfully registered.";
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
            });
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userManager.FindByNameAsync(login.UserName).Result;
            _signInManager.SignOutAsync();
            if (user == null)
            {
                ViewBag.message = "This email is not registered in our database.";
                return View(login);
            }
            var result = _signInManager.PasswordSignInAsync(user, login.Password, login.IsPersistent, true).Result;
            if (result.Succeeded == true)
            {
                return Redirect(login.ReturnUrl);
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");


            return View();
        }


        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
             return RedirectToAction("Index", "Home");
        }
    }

}
