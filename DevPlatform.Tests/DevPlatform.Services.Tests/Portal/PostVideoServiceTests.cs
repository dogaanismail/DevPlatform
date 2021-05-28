using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Repository.Generic;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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
        public void ItShouldInsertPostVideo()
        {
            var getPost = GetService<IRepository<Post>>().GetList().First();
            var user = GetService<IRepository<AppUser>>().GetList().First();

            var postVideo = new PostVideo
            {
                PostId = getPost.Id,
                VideoUrl = "http://placehold.it/300x300",
                CreatedBy = user.Id
            };

            _postVideoService.Create(postVideo);
            postVideo.Id.Should().BeGreaterThan(0);
            GetService<IRepository<PostVideo>>().Delete(postVideo);
        }

        [Test]
        public void ItShouldInsertPostImageWithList()
        {
            var post = GetService<IRepository<Post>>().GetList().First();
            var user = GetService<IRepository<AppUser>>().GetList().First();

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
                _postVideoService.Create(video);
                addedVideos.Add(video.Id);
                GetService<IRepository<PostVideo>>().Delete(video);
            }

            addedVideos.Should().NotBeNullOrEmpty();
            addedVideos.Should().HaveCountGreaterThan(1);
            addedVideos.Should().Contain(x => x > 1);
        }
    }
}
