using DevPlatform.Domain.Api;
using DevPlatform.Domain.Validation;
using DevPlatform.Tests.DevPlatform.Services.Tests;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace DevPlatform.Tests.DevPlatform.Domain.Tests.Validation.AccountValidation
{
    [TestFixture]
    public class LoginApiRequestValidatorTests : ServiceTest
    {
        private LoginApiRequestValidator _loginApiRequestValidator;

        [OneTimeSetUp]
        public void SetUp()
        {
            _loginApiRequestValidator = GetService<LoginApiRequestValidator>();
        }

        [Test]
        public void ShouldHaveErrorWhenUserNameIsNullOrEmpty()
        {
            var model = new LoginApiRequest
            {
                UserName = null
            };

            _loginApiRequestValidator.ShouldHaveValidationErrorFor(x => x.UserName, model);

            model.UserName = string.Empty;
            _loginApiRequestValidator.ShouldHaveValidationErrorFor(x => x.UserName, model);
        }

        [Test]
        public void ShouldHaveErrorWhenPasswordIsNullOrEmpty()
        {
            var model = new LoginApiRequest
            {
                Password = null
            };

            _loginApiRequestValidator.ShouldHaveValidationErrorFor(x => x.Password, model);

            model.Password = string.Empty;
            _loginApiRequestValidator.ShouldHaveValidationErrorFor(x => x.Password, model);
        }
    }
}
