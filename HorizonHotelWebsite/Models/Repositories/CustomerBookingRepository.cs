using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.room;
using HorizonHotelWebsite.Models.Entities.user;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Repositories
{
    public class CustomerBookingRepository : ICustomerBookingRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public CustomerBookingRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;

        }
        public decimal CreateBooking(Booking booking)
        {
            List<Room> SameTypeRooms = _dataBaseContext.Rooms.Where(R => R.Type == booking.Room.Type).Include(R => R.Bookings).ToList();
            if(SameTypeRooms == null)
            {
                throw new Exception($"A room with type  {booking.Room.Type} does not exist!");
            }
            List<Room> SelectedRooms = new List<Room>();
            bool Bookable = true;
            foreach(Room R in SameTypeRooms)
            {
                if (R.Bookings != null)
                {
                    foreach (Booking B in R.Bookings)
                    { 
                        if (!(booking.CheckIn > B.CheckOut || booking.CheckOut < B.CheckIn) && B.Paid == true)
                        {
                            Bookable = false;
                            break;
                        }
                    }

                     if (Bookable)
                        SelectedRooms.Add(R);
                }
                else
                {
                    SelectedRooms.Add(R);

                }
                    
            }

            if (!Bookable)
                throw new Exception($"A room with type  {booking.Room.Type} in this time is not available.");

            booking.BookingPlaced = DateTime.Now;
            booking.Room = SelectedRooms.FirstOrDefault();
           _dataBaseContext.Bookings.Add(booking);
           _dataBaseContext.SaveChanges();

            var bookedDays = (booking.CheckOut - booking.CheckIn).TotalDays;
            var  bookedDaysDecimal = Convert.ToDecimal(bookedDays);
            var totalPrice = bookedDaysDecimal * booking.Room.Price;

            return totalPrice;

        }
    }
}
