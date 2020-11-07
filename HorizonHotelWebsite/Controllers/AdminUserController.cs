using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Models.Repositories;
using HorizonHotelWebsite.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HorizonHotelWebsite.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly IAdminUserRepository _adminUserRepository;

        public AdminUserController(IAdminUserRepository adminUserRepository)
        {
            _adminUserRepository = adminUserRepository;
        }

        public IActionResult Index()
        {
            AdminUserViewModel adminUserViewModel = new AdminUserViewModel();
            adminUserViewModel.Users = _adminUserRepository.AllUsers;
            return View(adminUserViewModel);
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("UserId, FirstName, LastName, Phone, Email, Role")]
            User user)
        {
            AdminUserViewModel adminUserViewModel = new AdminUserViewModel();
            if (ModelState.IsValid)
            {
                _adminUserRepository.CreateUser(user);
                _adminUserRepository.UpdateWithUser(user);
                return RedirectToAction("Index");
            }

            return View(adminUserViewModel);
        }

        public ActionResult Edit(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("UserId, FirstName, LastName, Phone, Email, Role")]
            User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _adminUserRepository.UpdateWithUser(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            var user = _adminUserRepository.GetById(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,
            User user)
        {
            _adminUserRepository.DeleteUser(user);
            return RedirectToAction("Index");
        }

        public ActionResult Bookings(int? id)
        {
            var user = _adminUserRepository.GetById(id);
            return View(user);
        }
    }
}