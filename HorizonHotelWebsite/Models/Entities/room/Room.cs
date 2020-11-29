using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.user;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Services;

namespace HorizonHotelWebsite.Models.Entities.room
{
    public enum RoomType
    {
        [Display(Name = "Single Room")]
        SingleRoom,
        [Display(Name = "Double Room")]
        DoubleRoom,
        [Display(Name = "Twin Room")]
        TwinRoom,
        [Display(Name = "Suite Room")]
        Suite,
        [Display(Name = "Studio")]
        Studio
    }
    public class Room
    {
        [Display(Name = "Room Id")]
        public int RoomId { get; set; }
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; }
        [Display(Name = "Room Type")]
        public RoomType Type { get; set; }

        public decimal Price { get; set; }

        public virtual List<Booking> Bookings { get; set; }
      
        
    }
}
