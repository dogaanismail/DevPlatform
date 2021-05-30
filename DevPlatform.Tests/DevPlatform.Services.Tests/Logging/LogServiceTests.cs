using DevPlatform.Business.Interfaces;
using DevPlatform.Domain.Enumerations;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DevPlatform.Tests.DevPlatform.Services.Tests.Logging
{
    [TestFixture]
    public class LogServiceTests : ServiceTest
    {
        private ILogService _logService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _logService = GetService<ILogService>();
        }

        [Test]
        public async Task TaskItShouldInserLog()
        {
            var result = await _logService.InsertLogAsync(LogLevel.Error, "Example short message", "Example full message");
            result.Id.Should().BeGreaterThan(0);
        }
    }
}
