using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Entities.payment;
using HorizonHotelWebsite.Models.Repositories;
using HorizonHotelWebsite.ViewsModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HorizonHotelWebsite.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPayment _payment;
        public PaymentController(IPayment payment)
        {
            _payment = payment;
        }
        // GET: PaymentController
        public IActionResult Index()
        {
            PaymentViewModel paymentViewModel = new PaymentViewModel();
            paymentViewModel.Payments = _payment.AllPayments;
            return View(paymentViewModel);
        }

        // GET: PaymentController/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("PaymentId, CardNo, Name , ExpiryDate, cvvCode")]
        Payment payment)
        {
            PaymentViewModel paymentViewModel = new PaymentViewModel();
            if (ModelState.IsValid)
            {
                _payment.CreatePayment(payment);
                return RedirectToAction("Index");
            }
            return View(paymentViewModel);

        }

        //// GET: PaymentController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PaymentController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PaymentController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PaymentController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
