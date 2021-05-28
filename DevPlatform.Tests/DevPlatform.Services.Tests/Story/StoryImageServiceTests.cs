using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Repository.Generic;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DevPlatform.Tests.DevPlatform.Services.Tests.Story
{
    [TestFixture]
    public class StoryImageServiceTests : ServiceTest
    {
        private IStoryImageService _storyImageService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _storyImageService = GetService<IStoryImageService>();
        }

        [Test]
        public void ItShouldInsertStoryImage()
        {
            var getStory = GetService<IRepository<Core.Domain.Story.Story>>().GetList().First();
            var user = GetService<IRepository<AppUser>>().GetList().First();

            var storyImage = new Core.Domain.Story.StoryImage
            {
                StoryId = getStory.Id,
                ImageUrl = "http://placehold.it/300x300",
                CreatedBy = user.Id
            };

            _storyImageService.Create(storyImage);
            storyImage.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void ItShouldInsertStoryImageWithList()
        {
            var getStory = GetService<IRepository<Core.Domain.Story.Story>>().GetList().First();
            var user = GetService<IRepository<AppUser>>().GetList().First();

            List<Core.Domain.Story.StoryImage> storyImages = new();
            List<int> addedImages = new();

            var storyImage = new Core.Domain.Story.StoryImage
            {
                StoryId = getStory.Id,
                ImageUrl = "http://placehold.it/300x300",
                CreatedBy = user.Id
            };

            storyImages.Add(storyImage);

            foreach (var image in storyImages)
            {
                _storyImageService.Create(image);
                addedImages.Add(image.Id);
            }

            addedImages.Should().NotBeNullOrEmpty();
            addedImages.Should().HaveCountGreaterThan(1);
            addedImages.Should().Contain(x => x > 1);
        }
    }
}
