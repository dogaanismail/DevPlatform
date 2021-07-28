using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Domain.Api;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

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
        public async Task ItShouldReturnNullUserWhenUserIdIsZero()
        {
            var user = await _userService.FindByIdAsync(0);
            user.Should().BeNull();
        }

        [Test]
        public async Task ItShouldNotReturnUserDetail()
        {
            AppUserDetail appUserDetail = new()
            {
                FirstName = "User",
                LastName = "User",
                ProfilePhotoPath = "http://placehold.it/300x300",
                CoverPhotoPath = "http://placehold.it/1030x360"
            };

            await GetService<IUserDetailService>().CreateAsync(appUserDetail);

            AppUser appUser = new()
            {
                UserName = "User",
                Email = "example@gmail.com",
                DetailId = appUserDetail.Id
            };

            await _userService.CreateAsync(appUser);

            var user = await _userService.FindByUserNameAsync(appUser.UserName);
            user.UserDetail.Should().NotBeNull();
        }

        [Test]
        public async Task ItShouldNotReturnUserDetailByUserName()
        {
            var user = await _userService.FindByUserNameAsync("User");
            user.UserDetail.Should().NotBeNull();
        }

        [Test]
        public void ItShouldThrowExceptionIfUserNameIsNull()
        {
            Assert.Throws(typeof(ArgumentNullException), () => _userService.FindByUserNameAsync(string.Empty).Wait());
        }

        [Test]
        public void ItShouldThrowIfUserIsNullWhenInsertUser()
        {
            Assert.Throws<ArgumentNullException>(() => _userService.CreateAsync(null));
        }

        [Test]
        public void ItShouldThrowIfUserIsNullWhenUpdateUser()
        {
            Assert.Throws<ArgumentNullException>(() => _userService.UpdateAsync(null));
        }

        [Test]
        public async Task ItShouldReturnAnErrorWhenPasswordsDoNotMatch()
        {
            RegisterApiRequest request = new();

            request.Password = "example";
            request.Password = "example2";

            var serviceResponse = await _userService.RegisterAsync(request);
            serviceResponse.Data.Should().NotBeNull();
        }
    }
}
