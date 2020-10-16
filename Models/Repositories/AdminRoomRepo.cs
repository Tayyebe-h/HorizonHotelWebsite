﻿using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Repositories
{
    public class AdminRoomRepo : IAdminRoomRepo
    {
        private readonly DataBaseContext _dataBaseContext;

        public AdminRoomRepo(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public IEnumerable<Room> AllRooms
        {
            get
            {
                return _dataBaseContext.Rooms;
            }
        }

        public Room CreateRoom(Room room)
        {
            if (room != null)
            {
                _dataBaseContext.Rooms.Add(room);
                _dataBaseContext.SaveChanges();
                return room;
            }
            return room;
        }


        public Room DeleteRoom(int id)
        {
            var room = GetById(id);
            if (room != null)
            {
                _dataBaseContext.Rooms.Remove(room);
                _dataBaseContext.SaveChanges();
                return room;
            }
            return room;
        }

        public Room GetById(int Id)
        {
            return _dataBaseContext.Rooms.FirstOrDefault(r => r.RoomId == Id);
        }

        public Room SaveChanges(int id)
        {
            var room = GetById(id);

            if (room != null)
            {
                _dataBaseContext.Rooms.Update(room);
                _dataBaseContext.SaveChanges();
                return room;
            }
            return room;
        }
    }
}