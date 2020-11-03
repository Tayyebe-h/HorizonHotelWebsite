using HorizonHotelWebsite.Models.Entities.room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Repositories
{
    public interface IAdminRoomRepo
    {
        public IEnumerable<Room> AllRooms { get; }

        public Room GetById(int? id);

        public Room CreateRoom(Room room);

        public Room DeleteRoom(int id);

        public Room Update(int id);

        public Room UpdateWithRoom(Room room);
    }
}
