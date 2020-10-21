using HorizonHotelWebsite.Models.Entities.booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Repositories
{
    public interface ICustomerBookingRepository
    {
        void CreateBooking(Booking booking);
    }
}
