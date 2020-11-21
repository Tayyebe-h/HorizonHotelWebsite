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
        [Display(Name = "Booking Id")]
        public int Id { get; set; }
        
        public virtual User User { get; set; }
        [Display(Name = "User Id")]
        public int UserId { get; set; }
       
        public virtual  Room Room { get; set; }
        [Display(Name = "Room Id")]
        public int RoomId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }
        [BookingDateCheck]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
        [BindNever]
        [ScaffoldColumn(false)]
        [Display(Name = "Booking placed at")]
        public DateTime BookingPlaced { get; set; }
        public bool Paid { get; set; }
      

    }
}
