using DevPlatform.Business.Interfaces.Portal;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Repository.Generic;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevPlatform.Tests.DevPlatform.Services.Tests.Portal
{
    [TestFixture]
    public class PostVideoServiceTests : ServiceTest
    {
        private IPostVideoService _postVideoService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _postVideoService = GetService<IPostVideoService>();
        }

        [Test]
        public async Task ItShouldInsertPostVideo()
        {
            var getPost = GetService<IRepository<Post>>().GetAll().First();
            var user = GetService<IRepository<AppUser>>().GetAll().First();

            var postVideo = new PostVideo
            {
                PostId = getPost.Id,
                VideoUrl = "http://placehold.it/300x300",
                CreatedBy = user.Id
            };

            await _postVideoService.CreateAsync(postVideo);
            postVideo.Id.Should().BeGreaterThan(0);
            await GetService<IRepository<PostVideo>>().DeleteAsync(postVideo);
        }

        [Test]
        public async Task ItShouldInsertPostImageWithList()
        {
            var post = GetService<IRepository<Post>>().GetAll().First();
            var user = GetService<IRepository<AppUser>>().GetAll().First();

            List<PostVideo> postVideos = new();
            List<int> addedVideos = new();

            var postVideo = new PostVideo
            {
                PostId = post.Id,
                VideoUrl = "http://placehold.it/300x300",
                CreatedBy = user.Id
            };

            postVideos.Add(postVideo);

            foreach (var video in postVideos)
            {
                await _postVideoService.CreateAsync(video);
                addedVideos.Add(video.Id);
                await GetService<IRepository<PostVideo>>().DeleteAsync(video);
            }

            addedVideos.Should().NotBeNullOrEmpty();
            addedVideos.Should().HaveCountGreaterThan(1);
            addedVideos.Should().Contain(x => x > 1);
        }
    }
}
