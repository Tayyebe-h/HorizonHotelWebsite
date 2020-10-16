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

        public string GetErrorMessage() => $"Check out date must be after check in date";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var booking = (Booking) validationContext.ObjectInstance;

            if (booking.CheckIn >= booking.CheckOut)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
