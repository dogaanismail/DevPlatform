using DevPlatform.Domain.Api;
using FluentValidation;

namespace DevPlatform.Domain.Validation
{
    public class LoginApiRequestValidator : AbstractValidator<LoginApiRequest>
    {
        public LoginApiRequestValidator()
        {
            RuleFor(m => m.UserName)
                .NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(m => m.Password)
                .NotEmpty().WithMessage(ValidationMessage.Required);
        }
    }
}
