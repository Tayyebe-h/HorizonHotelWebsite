﻿using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.room;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Entities.user
{
    public class User:IdentityUser<int>
    {
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }
        public String Role { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
