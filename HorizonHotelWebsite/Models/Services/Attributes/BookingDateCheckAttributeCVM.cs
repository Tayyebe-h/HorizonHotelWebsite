using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.ViewsModels.BookingViewModels;

namespace HorizonHotelWebsite.Models.Services.Attributes
{
     public class BookingDateCheckAttributeCVM : ValidationAttribute
    {
        public BookingDateCheckAttributeCVM()
        {
        }

        public string GetErrorMessage() => $"Check out date must be after check in date and they can't be before today.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var booking = (CustomerBookingViewModel)validationContext.ObjectInstance;

            if (booking.CheckIn >= booking.CheckOut || booking.CheckIn < DateTime.Today)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
