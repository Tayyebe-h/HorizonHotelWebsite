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
            bool has = _dataBaseContext.Rooms.Any(R => R.RoomId == booking.Room.RoomId);

            if (has)
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
                
               
                booking.Room = new Room
                {
                    RoomId = booking.Room.RoomId,
                    RoomNumber = _dataBaseContext.Rooms.Where(R => R.RoomId == booking.Room.RoomId).Select(R => R.RoomNumber).FirstOrDefault(),
                    Price = _dataBaseContext.Rooms.Where(R => R.RoomId == booking.Room.RoomId).Select(R => R.Price).FirstOrDefault(),
                    Type = _dataBaseContext.Rooms.Where(R => R.RoomId == booking.Room.RoomId).Select(R => R.Type).FirstOrDefault(),
                    Bookings = _dataBaseContext.Rooms.Where(R => R.RoomId == booking.Room.RoomId).SelectMany(R => R.Bookings).ToList(),

                };

                user.Bookings.Add(booking);

                _dataBaseContext.Bookings.Add(booking);
               
                _dataBaseContext.Rooms.Update(booking.Room);

               

                _dataBaseContext.SaveChanges();
            }
            else
            {
                throw new Exception("This room doesn't exist.");
            }
        }
    }
}