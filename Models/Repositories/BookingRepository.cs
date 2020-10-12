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
    public class BookingRepository : IBookingRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public BookingRepository(DataBaseContext dataBaseContext)
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

                
                //_dataBaseContext.Users.Add(user);
                _dataBaseContext.Bookings.Add(booking);
                _dataBaseContext.Rooms.Update(booking.Room);

                _dataBaseContext.SaveChanges();


            }else{
                throw new Exception("This room doesn't exist.");
            };
           
            

        }
    }
}
