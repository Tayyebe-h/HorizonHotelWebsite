using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HorizonHotelWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingController : Controller
    {
        
        private readonly IAdminBookingRepository _bookingRepository;
        public BookingController(IAdminBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

       
        public IActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult Index(Booking booking)
        {

            if (ModelState.IsValid)
            {
                _bookingRepository.CreateBooking(booking);
                return RedirectToAction("BookingIsComplete");
            }
            return View(booking);
        }
        
        public IActionResult BookingIsComplete()
        {
            ViewBag.BookingCompleteMessage = "Now the booking is added.";
            return View();
        }
    }
}

