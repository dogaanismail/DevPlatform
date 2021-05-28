using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Story;
using DevPlatform.Repository.Generic;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DevPlatform.Tests.DevPlatform.Services.Tests.Story
{
    [TestFixture]
    public class StoryVideoServiceTests : ServiceTest
    {
        private IStoryVideoService _storyVideoService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _storyVideoService = GetService<IStoryVideoService>();
        }

        [Test]
        public void ItShouldInsertStoryVideo()
        {
            var getStory = GetService<IRepository<Core.Domain.Story.Story>>().GetList().First();
            var user = GetService<IRepository<AppUser>>().GetList().First();

            var storyVideo = new StoryVideo
            {
                StoryId = getStory.Id,
                VideoUrl = "http://placehold.it/300x300",
                CreatedBy = user.Id
            };

            _storyVideoService.Create(storyVideo);
            storyVideo.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void ItShouldInsertStoryVideoWithList()
        {
            var getStory = GetService<IRepository<Core.Domain.Story.Story>>().GetList().First();
            var user = GetService<IRepository<AppUser>>().GetList().First();

            List<StoryVideo> storyVideos = new();
            List<int> addedVideos = new();

            var storyVideo = new StoryVideo
            {
                StoryId = getStory.Id,
                VideoUrl = "http://placehold.it/300x300",
                CreatedBy = user.Id
            };

            storyVideos.Add(storyVideo);

            foreach (var video in storyVideos)
            {
                _storyVideoService.Create(video);
                addedVideos.Add(video.Id);
            }

            addedVideos.Should().NotBeNullOrEmpty();
            addedVideos.Should().HaveCountGreaterThan(1);
            addedVideos.Should().Contain(x => x > 1);
        }
    }
}
