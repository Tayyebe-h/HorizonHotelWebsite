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
using Microsoft.AspNetCore.Mvc;

namespace HorizonHotelWebsite.Models.Entities.booking
{
    public class Booking
    {
        public int Id { get; set; }
        [Required]
        public virtual User User { get; set; }
        public int UserId { get; set; }
        [Required]
        public virtual  Room Room { get; set; }
        public int RoomId { get; set; }
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
