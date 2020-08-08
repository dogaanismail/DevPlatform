using DevPlatform.Domain.Api;
using FluentValidation;

namespace DevPlatform.Domain.Validation
{
    public class PostCreateApiValidator : AbstractValidator<PostCreateApi>
    {
        public PostCreateApiValidator()
        {
            RuleFor(m => m.Text)
                .Equal("undefined").WithMessage(ValidationMessage.Required)
               .NotEmpty().WithMessage(ValidationMessage.Required);
        }
    }
}
