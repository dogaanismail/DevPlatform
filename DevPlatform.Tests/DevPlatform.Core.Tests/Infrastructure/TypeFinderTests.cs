using DevPlatform.Core.Infrastructure;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace DevPlatform.Tests.DevPlatform.Core.Tests.Infrastructure
{
    [TestFixture]
    public class TypeFinderTests : BaseDevPlatformTest
    {
        [Test]
        public void TypeFinderBenchmarkFindings()
        {
            var finder = GetService<ITypeFinder>();
            var type = finder.FindClassesOfType<ISomeInterface>().ToList();
            type.Count.Should().Be(1);
            typeof(ISomeInterface).IsAssignableFrom(type.FirstOrDefault()).Should().BeTrue();
        }

        public interface ISomeInterface
        {
        }

        public class SomeClass : ISomeInterface
        {
        }
    }
}
