using DevPlatform.Business.Interfaces.Question;
using DevPlatform.Core.Domain.Question;
using DevPlatform.Repository.Generic;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

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
        public async Task ItShouldReturnNullQuestionCommentWhenQuestionCommentIdIsZero()
        {
            var comment = await _questionCommentService.GetByIdAsync(0);
            comment.Should().BeNull();
        }

        [Test]
        public async Task ItShouldReturnNullQuestionCommentAsDtoWhenQuestionCommentIdIsZero()
        {
            var commentList = await _questionCommentService.GetQuestionCommentsByQuestionIdAsync(0);
            commentList.Should().HaveCount(0);
        }

        [Test]
        public void ItShouldThrowExceptionIfQuestionCommentIsNullWhenQuestionComment()
        {
            Assert.Throws(typeof(ArgumentNullException), () => _questionCommentService.DeleteAsync(null));
        }

        [Test]
        public void ItShouldThrowIfQuestionCommentIsNullWhenInsertQuestionComment()
        {
            Assert.Throws<ArgumentNullException>(() => _questionCommentService.CreateAsync(new QuestionComment()));
        }

        [Test]
        public void ItShouldThrowIfQuestionCommentIsNullWhenUpdateQuestionComment()
        {
            Assert.Throws<ArgumentNullException>(() => _questionCommentService.UpdateAsync(null));
        }

        [Test]
        public async Task ItShouldInsertQuestionComment()
        {
            var questionComment = new QuestionComment
            {
                Text = "example for test",
                QuestionId = 1,
                CreatedBy = 1
            };

            await _questionCommentService.CreateAsync(questionComment);
            questionComment.Id.Should().BeGreaterThan(0);
            await GetService<IRepository<QuestionComment>>().DeleteAsync(questionComment);
        }
    }
}
