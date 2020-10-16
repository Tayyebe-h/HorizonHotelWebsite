using HorizonHotelWebsite.Models.Entities.room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.ViewsModels.RoomsViewModel
{
    public class AdminRoomCreateViewModel
    {
        public string RoomNumber { get; set; }

        public RoomType Type { get; set; }
        public decimal Price { get; set; }
    }
}
