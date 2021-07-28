using DevPlatform.Business.Interfaces;
using DevPlatform.Core;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DevPlatform.Tests.DevPlatform.Services.Tests.Common
{
    [TestFixture]
    public class GeoLookupServiceTests : ServiceTest
    {
        private IGeoLookupService _geoLookupService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _geoLookupService = GetService<IGeoLookupService>();
        }

        [Test]
        public async Task ItShouldReturnEmptyCountryName()
        {
            var response = await _geoLookupService.LookupCountryNameAsync("127.0.0.1");
            response.Should().BeEmpty();
        }


        [Test]
        public async Task ItShouldReturnEmptyCountryIsoCode()
        {
            var response = await _geoLookupService.LookupCountryIsoCodeAsync("127.0.0.1");
            response.Should().BeEmpty();
        }

        [Test]
        public async Task ItShouldReturnCountryName()
        {
            var webHelper = GetService<IWebHelper>();

            var response = await _geoLookupService.LookupCountryNameAsync(webHelper.GetCurrentIpAddress());
            response.Should().NotBeEmpty();
        }

        [Test]
        public async Task ItShouldReturnCountryIsoCode()
        {
            var webHelper = GetService<IWebHelper>();

            var response = await _geoLookupService.LookupCountryIsoCodeAsync(webHelper.GetCurrentIpAddress());
            response.Should().NotBeEmpty();
        }

        [Test]
        public async Task ItShouldReturnNullCityAndCountry()
        {
            var response = await _geoLookupService.GetCityAndCountryInformationsAsync("127.0.0.1");
            response.CurrentCityName.Should().BeNull();
        }
    }
}
