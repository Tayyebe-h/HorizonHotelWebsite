using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.payment;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Models.Repositories;
using HorizonHotelWebsite.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HorizonHotelWebsite.Controllers
{
    public class PaymentController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IPayment _payment;
        public PaymentController(IPayment payment, UserManager<User> userManager)
        {
            _payment = payment;
            _userManager = userManager;
        }
        // GET: PaymentController
        [Authorize(Policy = "UserRole")]
        public IActionResult Index()
        {
            PaymentViewModel paymentViewModel = new PaymentViewModel();
            paymentViewModel.Payments = _payment.AllPayments;
            return View(paymentViewModel);
        }

        // GET: PaymentController/Details/5
        [Authorize(Policy = "UserRole")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = _payment.GetById(id);

            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: PaymentController/Create
        [Authorize]
        public ActionResult Create()
        {
            var price = TempData["TotalPrice"];
            ViewBag.BookingPaymentMessage = $"To complete the booking process, you need to pay {price} kronor!";
            return View();
        }

        // POST: PaymentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("PaymentId, CardNo, Name , ExpiryDate, CvvCode")]
        Payment payment)
        {
            
            PaymentViewModel paymentViewModel = new PaymentViewModel();
            if (ModelState.IsValid)
            {
                var id = TempData["NewBookingID"];
                payment.BookingId = Convert.ToInt32(id);
                _payment.CreatePayment(payment);
                return RedirectToAction("BookingComplete");
            }
            return View(paymentViewModel);

        }
        [Authorize]
        public IActionResult BookingComplete()
        {
            ViewBag.BookingCompleteMessage = "Your booking is successfully added.";
            return View();
        }

    }
}
