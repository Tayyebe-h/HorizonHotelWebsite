using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Entities.room;
using HorizonHotelWebsite.Models.Repositories;
using HorizonHotelWebsite.ViewsModels.RoomsViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HorizonHotelWebsite.Controllers
{
    public class AdminRoomsController : Controller
    {
        private readonly IAdminRoomRepo _adminRoomRepo;
        public AdminRoomsController(IAdminRoomRepo adminRoomRepo)
        {
            _adminRoomRepo = adminRoomRepo;
        }
        // GET: AdminRoomsController
        public ActionResult Index()
        {
            AdminRoomViewModel adminRoomViewModel = new AdminRoomViewModel();
            adminRoomViewModel.Rooms = _adminRoomRepo.AllRooms;
            return View(adminRoomViewModel);

        }

        // GET: AdminRoomsController/Details/5
        public ActionResult Details(int id)
        {
            AdminRoomViewModel model = new AdminRoomViewModel();
            model.Rooms = _adminRoomRepo.AllRooms;
            var room = model.Rooms.FirstOrDefault(r => r.RoomId == id);
            return View(room);
        }

        // GET: AdminRoomsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminRoomsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("RoomId, RoomNumber, Type, Price")] Room room)
        {
            if (ModelState.IsValid)
            {
                _adminRoomRepo.CreateRoom(room);
                _adminRoomRepo.SaveChanges(room.RoomId);
                return RedirectToAction("Index");
            }
            return View(room);
        }

        // GET: AdminRoomsController/Edit/5
        public ActionResult Edit(int id)
        {
            AdminRoomViewModel model = new AdminRoomViewModel();
            model.Rooms = _adminRoomRepo.AllRooms;
            var room = model.Rooms.FirstOrDefault(r => r.RoomId == id);
            return View(room);
        }

        // POST: AdminRoomsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("RoomId, RoomNumber, Type, Price")] Room room)
        {
            if (ModelState.IsValid)
            {
                //_adminRoomRepo.CreateRoom(room);
                _adminRoomRepo.CreateRoom(room);
                _adminRoomRepo.SaveChanges(room.RoomId);
                return RedirectToAction("Index");
            }
            return View(room);
        }

        // GET: AdminRoomsController/Delete/5
        public ActionResult Delete(int id)
        {

            AdminRoomViewModel model = new AdminRoomViewModel();
            model.Rooms = _adminRoomRepo.AllRooms;
            var room = model.Rooms.FirstOrDefault(r => r.RoomId == id);
            return View(room);
        }

        // POST: AdminRoomsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [Bind("RoomId, RoomNumber, Type, Price")] Room room)
        {
            //_adminRoomRepo.AllRooms(id);
            _adminRoomRepo.DeleteRoom(id);
            _adminRoomRepo.SaveChanges(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
