using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Models.Repositories;
using HorizonHotelWebsite.ViewsModels;
using HorizonHotelWebsite.ViewsModels.UserManagerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace HorizonHotelWebsite.Controllers
{
    [Authorize(Policy = "UserRole")]
    public class AdminUserController : Controller
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly UserManager<User> _userManager;
        private readonly DataBaseContext _dataBaseContext;

        



        public AdminUserController(IAdminUserRepository adminUserRepository, UserManager<User> userManager, DataBaseContext dataBaseContext)
        {
            _adminUserRepository = adminUserRepository;
            _userManager = userManager;
            _dataBaseContext = dataBaseContext;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users
                .Select(u => new AdminUserViewModel
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    PhoneNumber = u.PhoneNumber,
                    EmailConfirmed = u.EmailConfirmed,
                    Role = u.Role,

                }).ToList();
            return View(users);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _adminUserRepository.GetById(id);
            
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

       
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AdminUserCreateViewModel adminUserCreateView)
        {
            if (ModelState.IsValid == false)
            {
                return View(adminUserCreateView);
            }
            
            User user = new User()
            {
                FirstName = adminUserCreateView.FirstName,
                LastName = adminUserCreateView.LastName,
                Email = adminUserCreateView.Email,
                PhoneNumber = adminUserCreateView.PhoneNumber,
                UserName = adminUserCreateView.Email,
                Role = adminUserCreateView.Role,

            };

            var result = _userManager.CreateAsync(user, adminUserCreateView.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;
            return View(adminUserCreateView);

        }
        

        public ActionResult Edit(int? id)
        {
            var _id = id.ToString();
            var user = _userManager.FindByIdAsync(_id).Result;

            if (user == null)
            {
                return NotFound();
            }
            AdminUserEditViewModel userEdit = new AdminUserEditViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber,

            };
            return View(userEdit );
           
        }

        [HttpPost]
        public ActionResult Edit(AdminUserEditViewModel userEdit)
        {
            var _id = userEdit.Id.ToString();
            var user = _userManager.FindByIdAsync(_id).Result;
            user.Id = userEdit.Id;
            user.FirstName = userEdit.FirstName;
            user.LastName = userEdit.LastName;
            user.UserName = userEdit.UserName;
            user.PhoneNumber = userEdit.PhoneNumber;
            user.Email = userEdit.Email;
            user.Role = userEdit.Role;

            var result = _userManager.UpdateAsync(user).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;
            return View(userEdit);
        } 

        public ActionResult Delete(int id)
        {
            
            var _id = id.ToString();
            var user = _userManager.FindByIdAsync(_id).Result;
            AdminUserDeleteViewModel deleteUser = new AdminUserDeleteViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
            };
            return View(deleteUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(AdminUserDeleteViewModel userDelete)
        {
            var _id = userDelete.Id.ToString();
            var user = _userManager.FindByIdAsync(_id).Result;
            var result = _userManager.DeleteAsync(user).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;
            return View(userDelete);
            
        }

        public ActionResult Bookings(int id)
        {
            var user = _adminUserRepository.GetById(id);
            return View(user);
        }
    }
}