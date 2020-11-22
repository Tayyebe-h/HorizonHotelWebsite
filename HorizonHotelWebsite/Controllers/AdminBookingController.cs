using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Repositories;
using HorizonHotelWebsite.ViewsModels.BookingViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HorizonHotelWebsite.Controllers
{
    [Authorize(Policy ="UserRole")]
    public class AdminBookingController : Controller
    {
        private readonly IAdminBookingRepository _bookingRepository;
        public AdminBookingController(IAdminBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

       
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(AdminBookingViewModel booking)
        {

            if (ModelState.IsValid)
            {
                Booking newBooking = new Booking
                {
                    Room = booking.Room,
                    RoomId = booking.RoomId,
                    User = booking.User,
                    UserId = booking.UserId,
                    CheckIn= booking.CheckIn,
                    CheckOut= booking.CheckOut,
                    Paid= booking.Paid,
                };
                _bookingRepository.CreateBooking(newBooking);
                return RedirectToAction("BookingComplete");
            }
          
            return View(booking);
        }

        public IActionResult BookingComplete()
        {
            ViewBag.BookingCompleteMessage = "The booking is added.";
            return View();
        }

        public IActionResult List()
        {
                             
            return View(_bookingRepository.GetAll());
        }

        public ActionResult Details(int? id)
        {
            var booking = _bookingRepository.GetByID(id);
            return View(booking);
        }

        public ActionResult Edit(int? id)
        {
            var booking = _bookingRepository.GetByID(id);
            return View(booking);

        }

        [HttpPost]
        public ActionResult Edit(Booking booking)
        {
            
            if (ModelState.IsValid)
            {
                _bookingRepository.Update(booking);
                return RedirectToAction("List");
            }

            return View(booking);

        }


        public ActionResult Delete(int? id)
        {
            var booking = _bookingRepository.GetByID(id);
            return View(booking);
        }

       
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Booking booking = new Booking();
            booking.Id = id;
            _bookingRepository.Delete(booking);
            return RedirectToAction("List");
        }


    }
}
