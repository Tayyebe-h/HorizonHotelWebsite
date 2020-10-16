using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.room;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Room room = new Room();
            //room = value;

            var id = Convert.ToUInt32(value);
            
            if (_dataBaseContext.Rooms.Any(R => R.RoomId == id))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(GetErrorMessage());

        }
    }
}
