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
    public class PostImageServiceTests : ServiceTest
    {
        private IPostImageService _postImageService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _postImageService = GetService<IPostImageService>();
        }

        [Test]
        public void ItShouldInsertPostImage()
        {
            var getPost = GetService<IRepository<Post>>().GetList().First();
            var user = GetService<IRepository<AppUser>>().GetList().First();

            var postImage = new PostImage
            {
                PostId = getPost.Id,
                ImageUrl = "http://placehold.it/300x300",
                CreatedBy = user.Id
            };

            _postImageService.Create(postImage);
            postImage.Id.Should().BeGreaterThan(0);
            GetService<IRepository<PostImage>>().Delete(postImage);
        }

        [Test]
        public void ItShouldInsertPostImageWithList()
        {
            var post = GetService<IRepository<Post>>().GetList().First();
            var user = GetService<IRepository<AppUser>>().GetList().First();

            List<PostImage> postImages = new();
            List<int> addedImages = new();

            var postImage = new PostImage
            {
                PostId = post.Id,
                ImageUrl = "http://placehold.it/300x300",
                CreatedBy = user.Id
            };

            postImages.Add(postImage);

            foreach (var image in postImages)
            {
                _postImageService.Create(image);
                addedImages.Add(image.Id);
                GetService<IRepository<PostImage>>().Delete(image);
            }

            addedImages.Should().NotBeNullOrEmpty();
            addedImages.Should().HaveCountGreaterThan(1);
            addedImages.Should().Contain(x => x > 1);
        }
    }
}
