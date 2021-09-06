using DevPlatform.Business.Interfaces.Story;
using DevPlatform.Domain.Enumerations;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using StoryClass = DevPlatform.Core.Domain.Story.Story;

namespace DevPlatform.Tests.DevPlatform.Services.Tests.Story
{
    [TestFixture]
    public class StoryServiceTests : ServiceTest
    {
        private IStoryService _storyService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _storyService = GetService<IStoryService>();
        }

        [Test]
        public async Task ItShouldReturnNullStoryWhenStoryIdIsZero()
        {
            var story = await _storyService.GetByIdAsync(0);
            story.Should().BeNull();
        }

        [Test]
        public async Task ItShouldReturnNullStoryAsDtoWhenStoryIdIsZero()
        {
            var story = await _storyService.GetByIdAsDtoAsync(0);
            story.Should().BeNull();
        }

        [Test]
        public void ItShouldThrowExceptionIfStoryIsNullWhenStory()
        {
            Assert.Throws(typeof(ArgumentNullException), () => _storyService.DeleteAsync(null));
        }

        [Test]
        public void ItShouldThrowSqlExceptionIfStoryIsNullWhenInsertStory()
        {
            Assert.Throws<SqlException>(() => _storyService.CreateAsync(new StoryClass()));
        }

        [Test]
        public void ItShouldThrowIfStoryIsNullWhenUpdateStory()
        {
            Assert.Throws<ArgumentNullException>(() => _storyService.UpdateAsync(null));
        }

        [Test]
        public async Task ItShouldInsertStory()
        {
            var story = new StoryClass
            {
                Title = "example for test",
                Description = "example for description",
                StoryType = (int)StoryTypeEnum.StoryText,
                CreatedBy = 0
            };

            await _storyService.CreateAsync(story);
            story.Id.Should().BeGreaterThan(0);
        }
    }
}
