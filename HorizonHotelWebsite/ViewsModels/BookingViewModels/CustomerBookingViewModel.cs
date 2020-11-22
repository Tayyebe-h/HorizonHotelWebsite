using HorizonHotelWebsite.Models.Entities.room;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Models.Services.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.ViewsModels.BookingViewModels
{
    public class CustomerBookingViewModel
    {
        
        public virtual User User { get; set; }
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [Required]
        public virtual Room Room { get; set; }
        [Display(Name = "Room Id")]
        public int RoomId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }
        [Required]
        [BookingDateCheckAttributeCVM]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

    }
}
