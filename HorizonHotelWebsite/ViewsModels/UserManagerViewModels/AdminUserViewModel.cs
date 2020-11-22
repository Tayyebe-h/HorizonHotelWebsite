using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Models.Entities.user;

namespace HorizonHotelWebsite.ViewsModels.UserManagerViewModels
{
    public class AdminUserViewModel
    {
        //public IEnumerable<User> Users { get; set; }
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        [Display(Name = "Email")]
        public string UserName { get; set; }

            
        public bool EmailConfirmed { get; set; }

         public string Role { get; set; }

        
    }
}
