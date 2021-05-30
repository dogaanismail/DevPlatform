using DevPlatform.Domain.Api;
using DevPlatform.Domain.Validation;
using DevPlatform.Tests.DevPlatform.Services.Tests;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace DevPlatform.Tests.DevPlatform.Domain.Tests.Validation.PortalValidation
{
    [TestFixture]
    public class PostCreateApiValidatorTests : ServiceTest
    {
        private PostCreateApiValidator _postCreateApiRequestValidator;

        [OneTimeSetUp]
        public void SetUp()
        {
            _postCreateApiRequestValidator = GetService<PostCreateApiValidator>();
        }

        [Test]
        public void ShouldHaveErrorWhenTextIsNullOrEmpty()
        {
            var model = new PostCreateApi
            {
                Text = null
            };

            _postCreateApiRequestValidator.ShouldHaveValidationErrorFor(x => x.Text, model);

            model.Text = string.Empty;
            _postCreateApiRequestValidator.ShouldHaveValidationErrorFor(x => x.Text, model);
        }
    }
}
