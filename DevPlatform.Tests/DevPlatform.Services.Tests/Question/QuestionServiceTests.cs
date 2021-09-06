using DevPlatform.Business.Interfaces.Question;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
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
        public async Task ItShouldReturnNullQuestionWhenQuestionIdIsZero()
        {
            var question = await _questionService.GetByIdAsync(0);
            question.Should().BeNull();
        }

        [Test]
        public async Task ItShouldReturnNullQuestionAsDtoWhenQuestionIdIsZero()
        {
            var question = await _questionService.GetByIdAsDtoAsync(0);
            question.Should().BeNull();
        }

        [Test]
        public void ItShouldThrowExceptionIfQuestionIsNullWhenQuestion()
        {
            Assert.Throws(typeof(ArgumentNullException), () => _questionService.DeleteAsync(null));
        }

        [Test]
        public void ItShouldThrowIfQuestionIsNullWhenInsertQuestion()
        {
            Assert.Throws<ArgumentNullException>(() => _questionService.CreateAsync(new QuestionClass()));
        }

        [Test]
        public void ItShouldThrowIfQuestionIsNullWhenUpdateQuestion()
        {
            Assert.Throws<ArgumentNullException>(() => _questionService.UpdateAsync(null));
        }

        [Test]
        public async Task ItShouldInsertQuestion()
        {
            var question = new QuestionClass
            {
                Title = "example for test",
                Description = "example for test",
                Tags = "example,example2,example3",
                CreatedBy = 0
            };

            await _questionService.CreateAsync(question);
            question.Id.Should().BeGreaterThan(0);
        }
    }
}
