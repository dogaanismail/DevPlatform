using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Enumerations;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DevPlatform.Tests.DevPlatform.Services.Tests.Portal
{
    [TestFixture]
    public class PostServiceTests : ServiceTest
    {
        private IPostService _postService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _postService = GetService<IPostService>();
        }

        [Test]
        public async Task ItShouldReturnNullPostWhenPostIdIsZero()
        {
            var post = await _postService.GetByIdAsync(0);
            post.Should().BeNull();
        }

        [Test]
        public async Task ItShouldReturnNullPostAsDtoWhenPostIdIsZero()
        {
            var post = await _postService.GetByIdAsDtoAsync(0);
            post.Should().BeNull();
        }

        [Test]
        public void ItShouldThrowExceptionIfPostIsNullWhenPost()
        {
            Assert.Throws(typeof(ArgumentNullException), () => _postService.DeleteAsync(null));
        }

        [Test]
        public void ItShouldThrowIfPostIsNullWhenInsertPost()
        {
            Assert.Throws<ArgumentNullException>(() => _postService.CreateAsync(new Post()));
        }

        [Test]
        public void ItShouldThrowIfPostIsNullWhenUpdatePost()
        {
            Assert.Throws<ArgumentNullException>(() => _postService.UpdateAsync(null));
        }

        [Test]
        public async Task ItShouldInsertPost()
        {
            var post = new Post
            {
                Text = "example for test",
                PostType = (int)PostTypeEnum.PostText,
                CreatedBy = 0
            };

            await _postService.CreateAsync(post);
            post.Id.Should().BeGreaterThan(0);
        }
    }
}
