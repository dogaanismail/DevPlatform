using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Story;
using DevPlatform.Repository.Generic;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoryClass = DevPlatform.Core.Domain.Story.Story;

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
        public async Task ItShouldInsertStoryImage()
        {
            var getStory = GetService<IRepository<StoryClass>>().GetAll().First();
            var user = GetService<IRepository<AppUser>>().GetAll().First();

            var storyImage = new StoryImage
            {
                StoryId = getStory.Id,
                ImageUrl = "http://placehold.it/300x300",
                CreatedBy = user.Id
            };

            await _storyImageService.CreateAsync(storyImage);
            storyImage.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public async Task ItShouldInsertStoryImageWithList()
        {
            var getStory = GetService<IRepository<StoryClass>>().GetAll().First();
            var user = GetService<IRepository<AppUser>>().GetAll().First();

            List<StoryImage> storyImages = new();
            List<int> addedImages = new();

            var storyImage = new StoryImage
            {
                StoryId = getStory.Id,
                ImageUrl = "http://placehold.it/300x300",
                CreatedBy = user.Id
            };

            storyImages.Add(storyImage);

            foreach (var image in storyImages)
            {
                await _storyImageService.CreateAsync(image);
                addedImages.Add(image.Id);
            }

            addedImages.Should().NotBeNullOrEmpty();
            addedImages.Should().HaveCountGreaterThan(1);
            addedImages.Should().Contain(x => x > 1);
        }
    }
}
