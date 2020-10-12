using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HorizonHotelWebsite.Areas.Admin.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
