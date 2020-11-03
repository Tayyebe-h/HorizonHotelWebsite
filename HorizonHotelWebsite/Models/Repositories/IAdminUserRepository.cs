using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.user;

namespace HorizonHotelWebsite.Models.Repositories
{
    public interface IAdminUserRepository
    {
        
        public IEnumerable<User> AllUsers { get; }

        public User GetById(int? id);

        public User CreateUser(User user);

        public User DeleteUser(User user);

        public User Update(int id);

        public User UpdateWithUser(User user);
    }
}
