using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Domain.Api;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace DevPlatform.Tests.DevPlatform.Services.Tests.Identity
{
    [TestFixture]
    public class UserServiceTests : ServiceTest
    {
        private IUserService _userService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _userService = GetService<IUserService>();
        }


        [Test]
        public void ItShouldReturnNullUserWhenUserIdIsZero()
        {
            var user = _userService.FindById(0);
            user.Should().BeNull();
        }

        [Test]
        public void ItShouldNotReturnUserDetail()
        {
            AppUserDetail appUserDetail = new()
            {
                FirstName = "User",
                LastName = "User",
                ProfilePhotoPath = "http://placehold.it/300x300",
                CoverPhotoPath = "http://placehold.it/1030x360"
            };

            GetService<IUserDetailService>().Create(appUserDetail);

            AppUser appUser = new()
            {
                UserName = "User",
                Email = "example@gmail.com",
                DetailId = appUserDetail.Id
            };

            _userService.Create(appUser);

            var user = _userService.FindByUserName(appUser.UserName);
            user.UserDetail.Should().NotBeNull();
        }

        [Test]
        public void ItShouldNotReturnUserDetailByUserName()
        {
            var user = _userService.FindByUserName("User");
            user.UserDetail.Should().NotBeNull();
        }

        [Test]
        public void ItShouldThrowExceptionIfUserNameIsNull()
        {
            Assert.Throws(typeof(ArgumentNullException), () => _userService.FindByUserName(string.Empty));
        }

        [Test]
        public void ItShouldThrowIfUserIsNullWhenInsertUser()
        {
            Assert.Throws<ArgumentNullException>(() => _userService.Create(null));
        }

        [Test]
        public void ItShouldThrowIfUserIsNullWhenUpdateUser()
        {
            Assert.Throws<ArgumentNullException>(() => _userService.Update(null));
        }

        [Test]
        public void ItShouldReturnAnErrorWhenPasswordsDoNotMatch()
        {
            RegisterApiRequest request = new();

            request.Password = "example";
            request.Password = "example2";

            var serviceResponse = _userService.Register(request);
            serviceResponse.Data.Should().NotBeNull();
        }
    }
}
