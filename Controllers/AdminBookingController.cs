﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HorizonHotelWebsite.Controllers
{
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
        public IActionResult Index(Booking booking)
        {
            
            if (ModelState.IsValid)
            {
                _bookingRepository.CreateBooking(booking);
                return RedirectToAction("BookingComplete");
            }
            return View(booking);
        }

        public IActionResult BookingComplete()
        {
            ViewBag.BookingCompleteMessage = "New booking is added.";
            return View();
        }
    }
}