//using HorizonHotelWebsite.Data;
//using HorizonHotelWebsite.Models.Entities.room;
//using HorizonHotelWebsite.Services.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Permissions;
//using System.Threading.Tasks;

//namespace HorizonHotelWebsite.Repositories
//{
//    //public class RoomRepo : IRoom
//    {
//        private readonly DataBaseContext _context;

//        public RoomRepo(DataBaseContext context)
//        {
//            _context = context;
//        }
//        public void Delete(Room room)
//        {
//            _context.Rooms.Remove(room);
//        }

//        public Room GetById(long Id)
//        {
//            return _context.Rooms.FirstOrDefault(x => x.Id == Id);
//        }

//        public ICollection<Room> GettAll()
//        {
//            return _context.Rooms.ToList();
//        }

//        public void Insert(Room room)
//        {
//            _context.Rooms.Add(room);
//        }

//        public void Update(Room room)
//        {
//            _context.Rooms.Update(room);
//        }
//    }
//}
