using System;
using System.Collections.Generic;
using System.Text;
using HorizonHotelWebsite.Models.Entities.user;
using Xunit;

namespace HorizonHotelWebsite.Test.Model
{
    public class UserModelShould
    {
        private User _validUser = new User
        {
            FirstName = "John", LastName = "Smith", Email = "john@smith.com",
            Role = "Customer"
        };

        private User _invalidUser = new User
        {
            FirstName = "", LastName = "", Email = "John3Smith,com",
            Role = "Customer"
        };

        [Fact]
        public void HaveAnInitializedBookingList()
        {
            var sut = _validUser;

            Assert.NotNull(sut.Bookings);
        }
    }
}
