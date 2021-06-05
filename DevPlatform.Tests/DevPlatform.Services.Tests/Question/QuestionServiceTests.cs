using DevPlatform.Business.Interfaces;
using FluentAssertions;
using NUnit.Framework;
using System;
using QuestionClass = DevPlatform.Core.Domain.Question.Question;

namespace DevPlatform.Tests.DevPlatform.Services.Tests.Question
{
    [TestFixture]
    public class QuestionServiceTests : ServiceTest
    {
        private IQuestionService _questionService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _questionService = GetService<IQuestionService>();
        }

        [Test]
        public void ItShouldReturnNullQuestionWhenQuestionIdIsZero()
        {
            var question = _questionService.GetById(0);
            question.Should().BeNull();
        }

        [Test]
        public void ItShouldReturnNullQuestionAsDtoWhenQuestionIdIsZero()
        {
            var question = _questionService.GetByIdAsDto(0);
            question.Should().BeNull();
        }

        [Test]
        public void ItShouldThrowExceptionIfQuestionIsNullWhenQuestion()
        {
            Assert.Throws(typeof(ArgumentNullException), () => _questionService.Delete(null));
        }

        [Test]
        public void ItShouldThrowIfQuestionIsNullWhenInsertQuestion()
        {
            Assert.Throws<ArgumentNullException>(() => _questionService.Create(new QuestionClass()));
        }

        [Test]
        public void ItShouldThrowIfQuestionIsNullWhenUpdateQuestion()
        {
            Assert.Throws<ArgumentNullException>(() => _questionService.Update(null));
        }

        [Test]
        public void ItShouldInsertQuestion()
        {
            var question = new QuestionClass
            {
                Title = "example for test",
                Description = "example for test",
                CreatedBy = 0
            };

            _questionService.Create(question);
            question.Id.Should().BeGreaterThan(0);
        }
    }
}
