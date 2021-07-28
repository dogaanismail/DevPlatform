using DevPlatform.Business.Interfaces;
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
    public class PostImageServiceTests : ServiceTest
    {
        private IPostImageService _postImageService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _postImageService = GetService<IPostImageService>();
        }

        [Test]
        public async Task ItShouldInsertPostImage()
        {
            var getPost = GetService<IRepository<Post>>().GetAll().First();
            var user = GetService<IRepository<AppUser>>().GetAll().First();

            var postImage = new PostImage
            {
                PostId = getPost.Id,
                ImageUrl = "http://placehold.it/300x300",
                CreatedBy = user.Id
            };

            await _postImageService.CreateAsync(postImage);
            postImage.Id.Should().BeGreaterThan(0);
            await GetService<IRepository<PostImage>>().DeleteAsync(postImage);
        }

        [Test]
        public async Task ItShouldInsertPostImageWithList()
        {
            var post = GetService<IRepository<Post>>().GetAll().First();
            var user = GetService<IRepository<AppUser>>().GetAll().First();

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
                await _postImageService.CreateAsync(image);
                addedImages.Add(image.Id);
                await GetService<IRepository<PostImage>>().DeleteAsync(image);
            }

            addedImages.Should().NotBeNullOrEmpty();
            addedImages.Should().HaveCountGreaterThan(1);
            addedImages.Should().Contain(x => x > 1);
        }
    }
}
