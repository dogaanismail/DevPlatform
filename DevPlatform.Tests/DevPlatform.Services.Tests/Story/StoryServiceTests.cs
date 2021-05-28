using DevPlatform.Business.Interfaces;
using DevPlatform.Domain.Enumerations;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Data.SqlClient;

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
        public void ItShouldReturnNullStoryWhenStoryIdIsZero()
        {
            var story = _storyService.GetById(0);
            story.Should().BeNull();
        }

        [Test]
        public void ItShouldReturnNullStoryAsDtoWhenStoryIdIsZero()
        {
            var story = _storyService.GetByIdAsDto(0);
            story.Should().BeNull();
        }

        [Test]
        public void ItShouldThrowExceptionIfStoryIsNullWhenStory()
        {
            Assert.Throws(typeof(ArgumentNullException), () => _storyService.Delete(null));
        }

        [Test]
        public void ItShouldThrowSqlExceptionIfStoryIsNullWhenInsertStory()
        {
            Assert.Throws<SqlException>(() => _storyService.Create(new Core.Domain.Story.Story()));
        }

        [Test]
        public void ItShouldThrowIfStoryIsNullWhenUpdateStory()
        {
            Assert.Throws<ArgumentNullException>(() => _storyService.Update(null));
        }

        [Test]
        public void ItShouldInsertStory()
        {
            var story = new Core.Domain.Story.Story
            {
                Title = "example for test",
                Description = "example for description",
                StoryType = (int)StoryTypeEnum.StoryText,
                CreatedBy = 0
            };

            _storyService.Create(story);
            story.Id.Should().BeGreaterThan(0);
        }
    }
}
