//using HorizonHotelWebsite.Data;
//using HorizonHotelWebsite.Models.Entities.user;
//using HorizonHotelWebsite.Models.Services.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace HorizonHotelWebsite.Repositories
//{
//    public class UserRepo : IUser
//    {
//        private readonly DataBaseContext _context;

//        public UserRepo(DataBaseContext context)
//        {
//            _context = context;
//        }

//        public void Delete(User user)
//        {
//            _context.Users.Remove(user);
//        }

//        public User GetById(long Id)
//        {
//            return _context.Users.FirstOrDefault(x => x.Id == Id);
//        }

//        public ICollection<User> GettAll()
//        {
//            return _context.Users.ToList();
//        }

//        public void Insert(User user)
//        {
//            _context.Users.Add(user);
//        }

//        public void Update(User user)
//        {
//            _context.Users.Update(user);
//        }
//    }
//}
