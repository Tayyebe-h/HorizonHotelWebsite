using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.user;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Entities.room
{
    public enum RoomType
    {
        SingleRoom,
        DoubleRoom,
        TwinRoom,
        Suite,
        Studio

    }
    public class Room
    {
        public long Id { get; set; }
        [Required]
        public string RoomNumber { get; set; }
        [Required]
        public RoomType Type { get; set; }
        public ICollection<Booking> Bookings { get; set; }
      
        
    }
}
