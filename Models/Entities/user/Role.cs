using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Entities.user
{
    public enum RoleName
    {
        Admin,
        Operator,
        Customer
    }
    public class Role
    {
        public long Id { get; set; }
        public RoleName Name { get; set; }
        public ICollection<UserRole> UserRole { get; set; }
    }
}

