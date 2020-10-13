using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HorizonHotelWebsite.Controllers
{
    public class AdminRoomsController : Controller
    {
        // GET: AdminRoomsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminRoomsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminRoomsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminRoomsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminRoomsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminRoomsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminRoomsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminRoomsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
