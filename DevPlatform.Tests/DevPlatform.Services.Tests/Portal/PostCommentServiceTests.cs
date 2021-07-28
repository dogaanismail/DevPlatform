using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Repository.Generic;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DevPlatform.Tests.DevPlatform.Services.Tests.Portal
{
    [TestFixture]
    public class PostCommentServiceTests : ServiceTest
    {
        private IPostCommentService _postCommentService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _postCommentService = GetService<IPostCommentService>();
        }

        [Test]
        public async Task ItShouldReturnNullPostCommentWhenPostCommentIdIsZero()
        {
            var comment = await _postCommentService.GetByIdAsync(0);
            comment.Should().BeNull();
        }

        [Test]
        public async Task ItShouldReturnNullPostCommentAsDtoWhenPostCommentIdIsZero()
        {
            var commentList = await _postCommentService.GetPostCommentsByPostIdAsync(0);
            commentList.Should().HaveCount(0);
        }

        [Test]
        public void ItShouldThrowExceptionIfPostCommentIsNullWhenPostComment()
        {
            Assert.Throws(typeof(ArgumentNullException), () => _postCommentService.DeleteAsync(null));
        }

        [Test]
        public void ItShouldThrowIfPostCommentIsNullWhenInsertPostComment()
        {
            Assert.Throws<ArgumentNullException>(() => _postCommentService.CreateAsync(new PostComment()));
        }

        [Test]
        public void ItShouldThrowIfPostCommentIsNullWhenUpdatePostComment()
        {
            Assert.Throws<ArgumentNullException>(() => _postCommentService.UpdateAsync(null));
        }

        [Test]
        public async Task ItShouldInsertPostComment()
        {
            var postComment = new PostComment
            {
                Text = "example for test",
                PostId = 1,
                CreatedBy = 1
            };

            await _postCommentService.CreateAsync(postComment);
            postComment.Id.Should().BeGreaterThan(0);
            await GetService<IRepository<PostComment>>().DeleteAsync(postComment);
        }
    }
}
