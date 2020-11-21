using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HorizonHotelWebsite.Controllers
{  
    [Authorize]
    public class CustomerBookingController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICustomerBookingRepository _bookingRepository;
        public CustomerBookingController(ICustomerBookingRepository bookingRepository, UserManager<User> userManager)
        {
            _bookingRepository = bookingRepository;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Booking booking)
        {
            if (User.Identity.IsAuthenticated)
            {
                booking.User = _userManager.GetUserAsync(User).Result;
            }
            if (ModelState.IsValid)
            {
                _bookingRepository.CreateBooking(booking);
                return RedirectToAction("BookingComplete");
            }
            return View(booking);
        }

        public IActionResult BookingComplete()
        {
            ViewBag.BookingCompleteMessage = "The booking is added.";
            return View();
        }







    }
}
