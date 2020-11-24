using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.room;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Models.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.ViewsModels.ManagesViewModels
{
    public class BookingViewModel
    {
        [Display(Name = "Booking Id")]
        public int Id { get; set; }

        public virtual User User { get; set; }

        //public virtual Room Room { get; set; }

        public string Email { get; set; }

        //public string RoomNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Required]
        [BookingDateCheck]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
    }
}
