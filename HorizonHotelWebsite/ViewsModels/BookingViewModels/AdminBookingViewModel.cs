using HorizonHotelWebsite.Models.Entities.room;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Models.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.ViewsModels.BookingViewModels
{
    public class AdminBookingViewModel
    {
        public virtual User User { get; set; }
        [DisplayName("User Id")]
        [Required]
        public int UserId { get; set; }
        public virtual Room Room { get; set; }
        [Required]
        [DisplayName("Room Id")]
        public int RoomId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }
        [Required]
        [BookingDateCheck]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
      

    }
}
