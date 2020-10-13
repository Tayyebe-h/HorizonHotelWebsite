using HorizonHotelWebsite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Services.GetUser
{
    public class GetUserService :IGetUserService
    {
        private readonly IDataBaseContext _context;
        public GetUserService(IDataBaseContext context)
        {
            _context = context;
        }


        public ResultGetUser Execute()
        {

            var users = _context.Users.AsQueryable();


            var usersList = users.Select(p => new GetUserView
            {
                Email = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Id = p.Id,
                Role = p.Role,
                Phone = p.Phone,
            }).ToList();

            return new ResultGetUser
            {

                Users = usersList,
            };
        }

    }
}

