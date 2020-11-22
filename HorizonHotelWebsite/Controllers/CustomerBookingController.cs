using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Models.Repositories;
using HorizonHotelWebsite.ViewsModels.BookingViewModels;
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
        public IActionResult Index(CustomerBookingViewModel _booking)
        {
                                  
            if (ModelState.IsValid)
            {
                Booking booking = new Booking
                {
                    Room = _booking.Room,
                    RoomId = _booking.RoomId,
                    User = _userManager.GetUserAsync(User).Result,
                    UserId = _booking.UserId,
                    CheckIn = _booking.CheckIn,
                    CheckOut = _booking.CheckOut,
                    Paid = false,
                };
                 
                var totoalPrice = _bookingRepository.CreateBooking(booking);
                TempData["NewBookingID"] = booking.Id.ToString();
                TempData["TotalPrice"] = totoalPrice.ToString();
                return RedirectToAction("Create","Payment");
            }
            return View(_booking);
        }

       






    }
}
