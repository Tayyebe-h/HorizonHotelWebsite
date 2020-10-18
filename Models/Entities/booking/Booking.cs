using HorizonHotelWebsite.Models.Entities.room;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Models.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Services.Attributes;

namespace HorizonHotelWebsite.Models.Entities.booking
{
    public class Booking
    {
        public int Id { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        //[RoomIdValidation]
        public Room Room { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }
        [Required]
        [BookingDateCheck]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime BookingPlaced { get; set; }

    }
}
