using DevPlatform.Core.Domain.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DevPlatform.Tests.DevPlatform.Services.Tests.Identity.LinqToDb.Identity
{
    [TestFixture]
    public class UserManagerTests : ServiceTest
    {
        private UserManager<AppUser> _userManager;

        [OneTimeSetUp]
        public void SetUp()
        {
            _userManager = GetService<UserManager<AppUser>>();
        }

        public async Task ItShouldInsertUserWithIdentity()
        {
            AppUser appUser = new()
            {
                UserName = "User",
                Email = "Email",
                DetailId = 1
            };

            var result = await _userManager.CreateAsync(appUser);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
        }
    }
}
