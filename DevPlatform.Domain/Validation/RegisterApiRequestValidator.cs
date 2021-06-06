using DevPlatform.Domain.Api;
using FluentValidation;

namespace DevPlatform.Domain.Validation
{
    public class RegisterApiRequestValidator : AbstractValidator<RegisterApiRequest>
    {
        public RegisterApiRequestValidator()
        {
            RuleFor(m => m.FirstName)
              .NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(m => m.LastName)
              .NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(m => m.Password)
              .NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(m => m.RePassword)
             .NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(m => m.Email)
            .NotEmpty().WithMessage(ValidationMessage.Required);
        }
    }
}
