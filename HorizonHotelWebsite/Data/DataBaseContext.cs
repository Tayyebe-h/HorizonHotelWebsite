using Microsoft.EntityFrameworkCore;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.room;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.ViewsModels;
using HorizonHotelWebsite.Models.Entities.payment;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HorizonHotelWebsite.Data
{
    public class DataBaseContext: IdentityDbContext<IdentityUser>, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Userss { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Payment> Payments { get; set; }
    }
}
