using HorizonHotelWebsite.Models.Entities.room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Repositories
{
    public interface IAdminRoomRepo
    {
        //void CreateBooking(Room room);
        public IEnumerable<Room> AllRooms { get; }

        Room GetById(int Id);

        public Room CreateRoom(Room room);

        public Room DeleteRoom(int id);

        public Room SaveChanges(int id);
    }
}
