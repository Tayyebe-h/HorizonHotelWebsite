using HorizonHotelWebsite.Models.Entities.user;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Helpers
{
    public class AddMyClaim : UserClaimsPrincipalFactory<User>
    {
        public AddMyClaim(UserManager<User> userManager
            , IOptions<IdentityOptions> options): base(userManager,options)
        {

        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("UserRole", user.Role ));
            return identity;
        }
    }
}
