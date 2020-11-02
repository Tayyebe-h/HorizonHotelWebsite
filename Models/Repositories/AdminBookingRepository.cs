using HorizonHotelWebsite.Data;
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
                throw new Exception($"Room with number {booking.Room.RoomId} does not exist");
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
    }
}