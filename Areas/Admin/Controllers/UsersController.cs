using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using HorizonHotelWebsite.Models.Services.GetUser;

namespace HorizonHotelWebsite.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly IGetUserService _getUserService;
        public UsersController(IGetUserService getUserService)
        {
            _getUserService = getUserService;
            
        }
        [Area("Admin")]
        public IActionResult Index()
        {
            
            return View(_getUserService.Execute());
        }
       
    }
}
