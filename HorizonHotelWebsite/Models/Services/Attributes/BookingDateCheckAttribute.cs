using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Entities.booking;

namespace HorizonHotelWebsite.Models.Services.Attributes
{
    //[AttributeUsage(AttributeTargets.Property)]
    public class BookingDateCheckAttribute : ValidationAttribute
    {
        public BookingDateCheckAttribute()
        {
        }

        public string GetErrorMessage() => $"Check out date must be after check in date and they can't be before today.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var booking = (Booking) validationContext.ObjectInstance;

            if (booking.CheckIn >= booking.CheckOut || booking.CheckIn < DateTime.Today)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
