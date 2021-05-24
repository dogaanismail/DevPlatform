using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Repository.Generic;
using FluentAssertions;
using NUnit.Framework;
using System;

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
        public void ItShouldReturnNullPostWhenPostIdIsZero()
        {
            var post = _postService.GetById(0);
            post.Should().BeNull();
        }

        [Test]
        public void ItShouldReturnNullPostAsDtoWhenPostIdIsZero()
        {
            var post = _postService.GetByIdAsDto(0);
            post.Should().BeNull();
        }

        [Test]
        public void ItShouldThrowExceptionIfPostIsNullWhenPost()
        {
            Assert.Throws(typeof(AggregateException), () => _postService.Delete(null));
        }

        [Test]
        public void ItShouldThrowIfPostIsNullWhenInsertPost()
        {
            Assert.Throws<AggregateException>(() => _postService.Create(new Core.Domain.Portal.Post()));
        }

        [Test]
        public void ItShouldThrowIfPostIsNullWhenUpdatePost()
        {
            Assert.Throws<AggregateException>(() => _postService.Update(null));
        }

        [Test]
        public void ItShouldInsertPost()
        {
            var post = new Post
            {
                Text = "example for test",
                PostType = (int)PostTypeEnum.PostText,
                CreatedBy = 0
            };

            _postService.Create(post);
            post.Id.Should().BeGreaterThan(0);
            GetService<IRepository<Post>>().Delete(post);
        }
    }
}
