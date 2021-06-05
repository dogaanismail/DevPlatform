using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Question;
using DevPlatform.Repository.Generic;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace DevPlatform.Tests.DevPlatform.Services.Tests.Question
{
    [TestFixture]
    public class QuestionCommentServiceTests : ServiceTest
    {
        private IQuestionCommentService _questionCommentService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _questionCommentService = GetService<IQuestionCommentService>();
        }

        [Test]
        public void ItShouldReturnNullQuestionCommentWhenQuestionCommentIdIsZero()
        {
            var comment = _questionCommentService.GetById(0);
            comment.Should().BeNull();
        }

        [Test]
        public void ItShouldReturnNullQuestionCommentAsDtoWhenQuestionCommentIdIsZero()
        {
            var commentList = _questionCommentService.GetQuestionCommentsByQuestionId(0);
            commentList.Should().HaveCount(0);
        }

        [Test]
        public void ItShouldThrowExceptionIfQuestionCommentIsNullWhenQuestionComment()
        {
            Assert.Throws(typeof(ArgumentNullException), () => _questionCommentService.Delete(null));
        }

        [Test]
        public void ItShouldThrowIfQuestionCommentIsNullWhenInsertQuestionComment()
        {
            Assert.Throws<ArgumentNullException>(() => _questionCommentService.Create(new QuestionComment()));
        }

        [Test]
        public void ItShouldThrowIfQuestionCommentIsNullWhenUpdateQuestionComment()
        {
            Assert.Throws<ArgumentNullException>(() => _questionCommentService.Update(null));
        }

        [Test]
        public void ItShouldInsertQuestionComment()
        {
            var questionComment = new QuestionComment
            {
                Text = "example for test",
                QuestionId = 1,
                CreatedBy = 1
            };

            _questionCommentService.Create(questionComment);
            questionComment.Id.Should().BeGreaterThan(0);
            GetService<IRepository<QuestionComment>>().Delete(questionComment);
        }
    }
}
