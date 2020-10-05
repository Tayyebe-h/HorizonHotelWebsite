using HorizonHotelWebsite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Services.GetUsers
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _context;
        public GetUsersService(IDataBaseContext context)
        {
            _context = context;
        }


        public ReslutGetUserDto Execute(RequestGetUserDto request)
        {
            var users = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                users = users.Where(p => p.FullName.Contains(request.SearchKey) && p.Email.Contains(request.SearchKey));
            }
           
            var usersList = users.Select(p => new GetUsersDto
            {
                Email = p.Email,
                FullName = p.FullName,
                Id = p.Id,
            }).ToList();

            return new ReslutGetUserDto
            {
               
                Users = usersList,
            };
        }
        //    private readonly IDataBaseContext _context;
        //    public GetUsersService(IDataBaseContext context)
        //    {
        //        _context = context;
        //    }

        //    public List<GetUsersDto> Execute(string SearchKey)
        //    {
        //        var users = _context.Users.AsQueryable();
        //        if (string.IsNullOrWhiteSpace(SearchKey))
        //        {
        //            users = users.Where(p => p.FullName.Contains(SearchKey) && p.Email.Contains(SearchKey));
        //        }
        //        return users.Select(p => new GetUsersDto
        //        {
        //            Email = p.Email,
        //            FullName = p.FullName,
        //            Id = p.Id,

        //        }).ToList();
        //    }
    }
}
