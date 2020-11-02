using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Entities.user;

namespace HorizonHotelWebsite.ViewsModels
{
    public class AdminUserViewModel
    {
        public IEnumerable<User> Users { get; set; }
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        //Left this out for now but we can discuss how to work with this later.
        //public string Password { get; set; }

        public RoleName Role { get; set; }

        //Could we have a link on the page to list of bookings?
    }
}
