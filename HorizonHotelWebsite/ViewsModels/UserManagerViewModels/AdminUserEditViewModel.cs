using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.ViewsModels.UserManagerViewModels
{
    public class AdminUserEditViewModel
    {
        public int Id { get; set; }
        [Display (Name="First Name")]
        public string FirstName { get; set; }
        [Display (Name="Last Name")]
        public string LastName { get; set; }
        [Display (Name="User Name")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Display (Name="Phone Number")]
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
