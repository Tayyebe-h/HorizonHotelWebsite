﻿using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.room;
using HorizonHotelWebsite.Models.Entities.user;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Repositories
{
    public class AdminBookingRepository : IAdminBookingRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public AdminBookingRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public void CreateBooking(Booking booking)
        {
            bool roomExists = _dataBaseContext.Rooms.Any(R => R.RoomId == booking.Room.RoomId);

            if (!roomExists)
                throw new Exception($"Room with Id {booking.Room.RoomId} does not exist");
            else 
            {


                booking.BookingPlaced = DateTime.Now;
                bool Bookable = true;

                booking.Room = _dataBaseContext.Rooms.Include(R => R.Bookings).SingleOrDefault(R=> R.RoomId == booking.Room.RoomId);

                if (booking.Room.Bookings != null)
                {

                    foreach (Booking B in booking.Room.Bookings)
                    {
                        if (!(booking.CheckIn > B.CheckOut || booking.CheckOut < B.CheckIn))
                        {
                            Bookable = false;
                            break;
                        }
                    }
                }

                if(!Bookable)
                    throw new Exception("The room in this time is not available.");


                _dataBaseContext.Bookings.Add(booking);
               
                _dataBaseContext.Rooms.Update(booking.Room);
                
                _dataBaseContext.SaveChanges();
            }
        }

        public void Delete(Booking booking)
        {
            if (booking != null)
            {
                _dataBaseContext.Bookings.Remove(booking);
                _dataBaseContext.SaveChanges();
            }
        }

        public IEnumerable<Booking> GetAll()
        {
            return _dataBaseContext.Bookings.Include(B => B.Room).Include(B => B.User);
        }

        public Booking GetByID(int? id)
        {
            return _dataBaseContext.Bookings.Include(B => B.Room).Include(B => B.User).FirstOrDefault(B => B.Id == id);
        }

        public void Update(Booking booking)
        {
            bool RoomExists = _dataBaseContext.Rooms.Any(R => R.RoomId == booking.RoomId);
            bool UserExists = _dataBaseContext.Users.Any(U => U.UserId == booking.UserId);
            if(RoomExists && UserExists)
            {
                booking.BookingPlaced = DateTime.Now;
                //bool Bookable = true;
                //Room room = _dataBaseContext.Rooms.Include(R => R.Bookings).SingleOrDefault(R => R.RoomId == booking.RoomId);
                //room.Bookings.RemoveAll(B => B.Id == booking.Id);
                //if (room.Bookings != null)
                //{

                //    foreach (Booking B in room.Bookings)
                //    {
                //        if (!(booking.CheckIn > B.CheckOut || booking.CheckOut < B.CheckIn))
                //        {
                //            Bookable = false;
                //            break;
                //        }
                //    }
                //}

                //if (!Bookable)
                //    throw new Exception("The room in this time is not available.");

                _dataBaseContext.Bookings.Update(booking);
                _dataBaseContext.SaveChanges();


            }else if (!RoomExists)
                throw new Exception($"Room with Id {booking.RoomId} does not exist");
            else if (!UserExists)
                throw new Exception($" User with Id {booking.UserId} does not exist");


        }


    }
}