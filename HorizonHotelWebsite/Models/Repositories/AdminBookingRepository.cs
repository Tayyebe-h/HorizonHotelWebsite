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
            bool RoomExists = _dataBaseContext.Rooms.Any(R => R.RoomId == booking.Room.RoomId);
            bool UserExists = _dataBaseContext.Userss.Any(U => U.Id == booking.UserId);
            if(!RoomExists)
                throw new Exception($"Room with Id {booking.Room.RoomId} does not exist");
            else if(!UserExists)
                throw new Exception($" User with Id {booking.UserId} does not exist");

            else if (RoomExists && UserExists)
            {
                booking.User= _dataBaseContext.Userss.Include(U => U.Bookings).SingleOrDefault(U => U.Id == booking.UserId);
                var bookable = CheckAvailability(booking);
                if (bookable)
                {
                    booking.BookingPlaced = DateTime.Now;
                    _dataBaseContext.Bookings.Add(booking);
                    _dataBaseContext.SaveChanges();
                }
            }
        }

        private bool CheckAvailability(Booking booking)
        {
            bool Bookable = true;

            booking.Room = _dataBaseContext.Rooms.Include(R => R.Bookings).SingleOrDefault(R => R.RoomId == booking.Room.RoomId);

            if (booking.Room.Bookings != null)
            {

                foreach (Booking B in booking.Room.Bookings)
                {
                    if (!(booking.CheckIn > B.CheckOut || booking.CheckOut < B.CheckIn) && B.Paid == true)
                    {
                        Bookable = false;
                        break;
                    }
                }
            }

            if (!Bookable)
                throw new Exception("The room in this time is not available.");
            return Bookable;
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
            return _dataBaseContext.Bookings.Include(B => B.Room).ThenInclude(R => R.Bookings).Include(B => B.User).ThenInclude(U => U.Bookings).FirstOrDefault(B => B.Id == id);
        }




        public void Update(Booking booking)
        {
            var room = _dataBaseContext.Rooms.Include(r => r.Bookings).SingleOrDefault(R => R.RoomId == booking.Room.RoomId);
            var user = _dataBaseContext.Userss.Include(r => r.Bookings).SingleOrDefault(U => U.Id == booking.UserId);
            var persistedBooking = _dataBaseContext.Bookings.SingleOrDefault(b => b.Id == booking.Id);
            if (room == null)
                throw new Exception($"Room with Id {booking.Room.RoomId} does not exist");
            if (user == null)
                throw new Exception($"User with Id {booking.UserId} does not exist");
            if (persistedBooking == null)
                throw new Exception($"Booking with Id {booking.Id} does not exist");
            bool Bookable = true;
            if (room.Bookings != null)
            {
                foreach (var B in room.Bookings)
                {
                    if (!(booking.CheckIn > B.CheckOut || booking.CheckOut < B.CheckIn) && (B.Id != booking.Id) && B.Paid == true)
                    {
                        Bookable = false;
                        break;
                    }
                }
            }
            if (!Bookable)
                throw new Exception("The room in this time is not available.");
            persistedBooking.User = user;
            persistedBooking.Room = room;
            persistedBooking.BookingPlaced = DateTime.Now;
            persistedBooking.CheckIn = booking.CheckIn;
            persistedBooking.CheckOut = booking.CheckOut;
            persistedBooking.Paid = booking.Paid;

            _dataBaseContext.SaveChanges();
        }

    }
}