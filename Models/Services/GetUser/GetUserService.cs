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
                FullName = p.FullName,
                Id = p.Id,
            }).ToList();

            return new ResultGetUser
            {

                Users = usersList,
            };
        }

    }
}

