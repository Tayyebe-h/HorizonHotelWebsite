

using HorizonHotelWebsite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Services.GetUsers
{
    public interface IGetUsersService
    {
        
        ReslutGetUserDto Execute(RequestGetUserDto requestGetUserDto);
    }
}
