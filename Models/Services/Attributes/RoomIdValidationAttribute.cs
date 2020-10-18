using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.room;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Composition.Convention;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Services
{
    public class RoomIdValidationAttribute:ValidationAttribute
    {
        private readonly DataBaseContext _dataBaseContext;
        public RoomIdValidationAttribute(DataBaseContext dataBaseContext) 
        {
            _dataBaseContext = dataBaseContext;
        }
        
        public string GetErrorMessage() =>
        $"This room doesn't exists.";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var booking = (Booking)validationContext.ObjectInstance;

                                    
            if (_dataBaseContext.Rooms.Any(R => R.RoomId == booking.Room.RoomId))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(GetErrorMessage());

        }
    }
}
