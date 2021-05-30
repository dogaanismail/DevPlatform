using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Repository.Generic;
using FluentAssertions;
using NUnit.Framework;
using System;

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
        public void ItShouldReturnNullPostCommentWhenPostCommentIdIsZero()
        {
            var comment = _postCommentService.GetById(0);
            comment.Should().BeNull();
        }

        [Test]
        public void ItShouldReturnNullPostCommentAsDtoWhenPostCommentIdIsZero()
        {
            var commentList = _postCommentService.GetPostCommentsByPostId(0);
            commentList.Should().HaveCount(0);
        }

        [Test]
        public void ItShouldThrowExceptionIfPostCommentIsNullWhenPostComment()
        {
            Assert.Throws(typeof(ArgumentNullException), () => _postCommentService.Delete(null));
        }

        [Test]
        public void ItShouldThrowIfPostCommentIsNullWhenInsertPostComment()
        {
            Assert.Throws<ArgumentNullException>(() => _postCommentService.Create(new PostComment()));
        }

        [Test]
        public void ItShouldThrowIfPostCommentIsNullWhenUpdatePostComment()
        {
            Assert.Throws<ArgumentNullException>(() => _postCommentService.Update(null));
        }

        [Test]
        public void ItShouldInsertPostComment()
        {
            var postComment = new PostComment
            {
                Text = "example for test",
                PostId = 1,
                CreatedBy = 1
            };

            _postCommentService.Create(postComment);
            postComment.Id.Should().BeGreaterThan(0);
            GetService<IRepository<PostComment>>().Delete(postComment);
        }
    }
}
