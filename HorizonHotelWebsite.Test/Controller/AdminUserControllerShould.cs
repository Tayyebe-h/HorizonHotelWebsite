using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HorizonHotelWebsite.Controllers;
using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.user;
using HorizonHotelWebsite.Models.Repositories;
using HorizonHotelWebsite.ViewsModels;
using HorizonHotelWebsite.ViewsModels.UserManagerViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HorizonHotelWebsite.Test.Controller
{
    public class AdminUserControllerShould
    {
        private readonly Mock<IAdminUserRepository> _mockRepository;
        private UserManager<User> _userManager = MockUserManager<User>(users).Object;
        private readonly DataBaseContext _dataBaseContext;
        private readonly AdminUserController _sut;

        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }

        private static List<User> users = new List<User>
        {
            new User{FirstName = "John", LastName = "Smith", Email = "john@smith.com",
                Role = "Customer", Id = 1 },
            new User{FirstName = "Ellen", LastName = "Bradford", Email = "ellen@gmail.com",
                Role = "Customer", Id = 2}
        };


        private readonly User _validUser = new User
        {
            FirstName = "John", LastName = "Smith", Email = "john@smith.com",
            Role = "Customer"
        };

        private User _invalidUser = new User
        {
            FirstName = "", LastName = "", Email = "John3Smith,com",
            Role = "Customer"
        };

        private readonly AdminUserCreateViewModel _invalidAdminUserCreateViewModel = new AdminUserCreateViewModel
        {
            FirstName = "",
            LastName = "",
            Email = "John3Smith,com",
            Role = "Customer"
        };

        private readonly AdminUserCreateViewModel _validAdminUserCreateViewModel = new AdminUserCreateViewModel
        {
            FirstName = "John",
            LastName = "Smith",
            Email = "john@smith.com",
            Role = "Customer"
        };


        public AdminUserControllerShould()
        {
            _mockRepository = new Mock<IAdminUserRepository>();
            _sut = new AdminUserController(_mockRepository.Object, _userManager, _dataBaseContext);
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

            IActionResult result = _sut.Create(_invalidAdminUserCreateViewModel);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnsViewWhenCreateHasValidModelState()
        {
            IActionResult result = _sut.Create(_validAdminUserCreateViewModel);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void ReturnNotFoundWhenEditPostHasInvalidModelState()
        {
            _sut.ModelState.AddModelError("x", "Test Error");

            IActionResult notFoundActionResult = _sut.Edit(_invalidUser.Id);

            Assert.IsType<NotFoundResult>(notFoundActionResult);
        }

        [Fact]
        public void ReturnsViewWhenEditPostHasValidModelState()
        {
            IActionResult result = _sut.Edit(1);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void ReturnsNotFoundWhenEditPostGetsInvalidId()
        {
            IActionResult result = _sut.Edit(_validUser.Id);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}