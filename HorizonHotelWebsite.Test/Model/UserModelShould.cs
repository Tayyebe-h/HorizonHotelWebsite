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
            UserId = 1, FirstName = "John", LastName = "Smith", Email = "john@smith.com", Phone = "0123456789",
            Password = "12345Ss!", Role = RoleName.Customer
        };

        private User _invalidUser = new User
        {
            UserId = 2, FirstName = "", LastName = "", Email = "John3Smith,com", Phone = "0123456789",
            Password = "12345Ss!", Role = RoleName.Customer
        };

        [Fact]
        public void HaveAnInitializedBookingList()
        {
            var sut = _validUser;

            Assert.NotNull(sut.Bookings);
        }

    }
}
