using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HorizonHotelWebsite.Services.GetUsers;

namespace HorizonHotelWebsite.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly IGetUsersService _getUsersService;
        public UsersController(IGetUsersService getUsersService)
        {
            _getUsersService = getUsersService;
            
        }
        [Area("Admin")]
        public IActionResult Index(string searchkey)
        {
            return View(_getUsersService.Execute(new RequestGetUserDto
            {
               
                SearchKey = searchkey,
            }));
        }
    }
}
