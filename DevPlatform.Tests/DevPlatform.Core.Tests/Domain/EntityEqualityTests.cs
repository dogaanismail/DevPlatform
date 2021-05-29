using DevPlatform.Core.Domain.Portal;
using DevPlatform.Core.Domain.Story;
using FluentAssertions;
using NUnit.Framework;

namespace DevPlatform.Tests.DevPlatform.Core.Tests.Domain
{
    [TestFixture]
    public class EntityEqualityTests
    {
        [Test]
        public void TwoTransientEntitiesShouldNotBeEqual()
        {
            var p1 = new Post();
            var p2 = new Post();

            p1.Should().NotBe(p2, "Different transient entities should not be equal");
        }

        [Test]
        public void TwoReferencesToSameTransientEntityShouldBeEqual()
        {
            var p1 = new Post();
            var p2 = p1;

            p1.Should().Be(p2, "Two references to the same transient entity should be equal");
        }

        [Test]
        public void EntitiesWithDifferentIdShouldNotBeEqual()
        {
            var p1 = new Post { Id = 2 };
            var p2 = new Post { Id = 5 };

            p1.Should().NotBe(p2, "Entities with different ids should not be equal");
        }

        [Test]
        public void EntityShouldNotEqualTransientEntity()
        {
            var p1 = new Post { Id = 1 };
            var p2 = new Post();

            p1.Should().NotBe(p2, "Entity and transient entity should not be equal");
        }

        [Test]
        public void EntitiesWithSameIdButDifferentTypeShouldNotBeEqual()
        {
            const int id = 10;
            var p1 = new Post { Id = id };

            var c1 = new Story { Id = id };

            p1.Should().NotBe(c1, "Entities of different types should not be equal, even if they have the same id");
        }
    }
}
