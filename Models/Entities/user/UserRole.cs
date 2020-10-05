using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Entities.user
{
    public class UserRole
    {
        public long Id { get; set; }
        public User Users { get; set; }
        public long UserId { get; set; }
        public Role Roles { get; set; }
        public long RoleId { get; set; }
    }
}
