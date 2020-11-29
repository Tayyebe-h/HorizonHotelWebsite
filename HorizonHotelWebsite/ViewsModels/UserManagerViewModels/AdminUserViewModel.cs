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
        [Display(Name = "User Id")]
        [Key]
        public int UserId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email")]
        public string UserName { get; set; }
        [Display(Name = "Email Confirmed")]
        public bool EmailConfirmed { get; set; }
        public string Role { get; set; }
    }
}
