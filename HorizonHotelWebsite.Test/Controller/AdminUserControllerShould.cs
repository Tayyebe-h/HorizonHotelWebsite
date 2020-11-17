using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HorizonHotelWebsite.Controllers;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Models.Repositories;                  
using HorizonHotelWebsite.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HorizonHotelWebsite.Test.Controller
{
    public class AdminUserControllerShould
    {
        private readonly Mock<IAdminUserRepository> _mockRepository;
        private readonly AdminUserController _sut;

        private readonly User _validUser = new User
        {
            UserId = 1, FirstName = "John", LastName = "Smith", Email = "john@smith.com", Phone = "0123456789",
            Password = "12345Ss!", Role = RoleName.Customer
        };

        private User _invalidUser = new User
        {
            UserId = 2, FirstName = "", LastName = "", Email = "John3Smith,com", Phone = "0123456789",
            Password = "12345Ss!", Role = RoleName.Customer
        };

        public AdminUserControllerShould()
        {
            _mockRepository = new Mock<IAdminUserRepository>();
            _sut = new AdminUserController(_mockRepository.Object);
        }

        [Fact]
        public void ReturnViewForIndex()
        {
            IActionResult result = _sut.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewWhenCreateHasInvalidModelState()
        {
            _sut.ModelState.AddModelError("x", "Test Error");

            IActionResult result = _sut.Create(_invalidUser);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnsViewWhenCreateHasValidModelState()
        {
            IActionResult result = _sut.Create(_validUser);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void ReturnViewWhenEditReceivesValidId()
        {
            IActionResult result = _sut.Edit(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewWhenEditPostHasInvalidModelState()
        {
            _sut.ModelState.AddModelError("x", "Test Error");

            IActionResult result = _sut.Edit(_invalidUser.UserId, _invalidUser);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnsViewWhenEditPostHasValidModelState()
        {
            IActionResult result = _sut.Edit(_validUser.UserId, _validUser);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void ReturnsNotFoundWhenEditPostGetsInvalidId()
        {
            IActionResult result = _sut.Edit(10, _validUser);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}