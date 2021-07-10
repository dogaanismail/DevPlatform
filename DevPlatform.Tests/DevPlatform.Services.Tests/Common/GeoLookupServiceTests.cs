using DevPlatform.Business.Interfaces;
using DevPlatform.Core;
using FluentAssertions;
using NUnit.Framework;

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
        public void ItShouldReturnEmptyCountryName()
        {
            var response = _geoLookupService.LookupCountryName("127.0.0.1");
            response.Should().BeEmpty();
        }


        [Test]
        public void ItShouldReturnEmptyCountryIsoCode()
        {
            var response = _geoLookupService.LookupCountryIsoCode("127.0.0.1");
            response.Should().BeEmpty();
        }

        [Test]
        public void ItShouldReturnCountryName()
        {
            var webHelper = GetService<IWebHelper>();

            var response = _geoLookupService.LookupCountryName(webHelper.GetCurrentIpAddress());
            response.Should().NotBeEmpty();
        }

        [Test]
        public void ItShouldReturnCountryIsoCode()
        {
            var webHelper = GetService<IWebHelper>();

            var response = _geoLookupService.LookupCountryIsoCode(webHelper.GetCurrentIpAddress());
            response.Should().NotBeEmpty();
        }

        [Test]
        public void ItShouldReturnNullCityAndCountry()
        {
            var response = _geoLookupService.GetCityAndCountryInformations("127.0.0.1");
            response.CurrentCityName.Should().BeNull();
        }
    }
}
