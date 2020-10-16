using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.booking;
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
            throw new NotImplementedException();
        }
    }
}
