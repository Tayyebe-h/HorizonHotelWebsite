using Microsoft.EntityFrameworkCore;
using HorizonHotelWebsite.Models.Entities.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Entities.room;
using HorizonHotelWebsite.Models.Entities.booking;

namespace HorizonHotelWebsite.Services.Interfaces
{
    public interface IDataBaseContext
    {
        DbSet<User> Userss { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Booking> Bookings { get; set; }




        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
