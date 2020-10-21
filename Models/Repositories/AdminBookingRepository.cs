using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.room;
using HorizonHotelWebsite.Models.Entities.user;
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

                User user = new User()
                {
                    FirstName = booking.User.FirstName,
                    LastName = booking.User.LastName,
                    Phone = booking.User.Phone,
                    Email = booking.User.Email,
                    Role = booking.User.Role,
                };

                booking.Room = _dataBaseContext.Rooms.SingleOrDefault(room => room.RoomId == booking.Room.RoomId);
               

                _dataBaseContext.Bookings.Add(booking);
               
                _dataBaseContext.Rooms.Update(booking.Room);
                
                _dataBaseContext.SaveChanges();

            }

        }
    }
}