using HorizonHotelWebsite.Models.Entities.booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Repositories
{
    interface ICustomerBookingRepository
    {
        void CreateBooking(Booking booking);
    }
}
