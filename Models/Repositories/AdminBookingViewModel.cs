using HorizonHotelWebsite.Models.Entities.room;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Models.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Repositories
{
    public class AdminBookingViewModel
    {
        public long Id { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        //[RoomIdValidation]
        public long RoomId{ get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
        [BindNever]
        public DateTime BookingPlaced { get; set; }

    }
}
