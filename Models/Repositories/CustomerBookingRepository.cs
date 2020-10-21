using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.room;
using HorizonHotelWebsite.Models.Entities.user;
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
        public void CreateBooking(Booking booking)
        {
            List<Room> SameTypeRooms = _dataBaseContext.Rooms.Where(R => R.Type == booking.Room.Type).ToList();
            List<Room> SelectedRooms = new List<Room>();
            foreach(Room R in SameTypeRooms)
            {
                if (R.Bookings != null)
                {
                    foreach (Booking B in R.Bookings)


                        if (booking.CheckIn > B.CheckOut || booking.CheckOut < B.CheckIn)
                        {
                            SelectedRooms.Add(R);

                        }
                }
                    
                SelectedRooms.Add(R);
               
                
            }

            if (SelectedRooms == null)
                throw new Exception($"A room with type  {booking.Room.RoomId} in this period of time is not available!");
            booking.BookingPlaced = DateTime.Now;
            booking.User.Role = RoleName.Customer;

            User user = new User()
            {
                FirstName = booking.User.FirstName,
                LastName = booking.User.LastName,
                Phone = booking.User.Phone,
                Email = booking.User.Email,
                Role = booking.User.Role,
            };

            booking.Room = SelectedRooms.FirstOrDefault();
           _dataBaseContext.Bookings.Add(booking);
            
            _dataBaseContext.Rooms.Update(booking.Room);
            
            _dataBaseContext.SaveChanges();


        }
    }
}
