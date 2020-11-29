using HorizonHotelWebsite.Models.Entities.room;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.ViewsModels.RoomsViewModel
{
    public class AdminRoomViewModel
    {
        [Display(Name="Room Id")]
        public int RoomId { get; set; }
        [Display(Name="Room Number")]
        public string RoomNumber { get; set; }
        [Display(Name="Room Type")]
        public RoomType Type { get; set; }
        public decimal Price { get; set; }

        public IEnumerable<Room> Rooms { get; set; }

    }
}
