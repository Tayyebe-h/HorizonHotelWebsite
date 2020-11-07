using HorizonHotelWebsite.Models.Entities.booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Repositories
{
    public interface IAdminBookingRepository
    {
        void CreateBooking(Booking booking);
        IEnumerable<Booking> GetAll();
        Booking GetByID(int? id);
        void Delete(Booking booking);
        void Update(Booking booking);
    }
}
