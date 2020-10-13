﻿using HorizonHotelWebsite.Models.Entities.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Services.GetUser
{
    public class GetUserView
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        public RoleName Role { get; set; }
    }
}