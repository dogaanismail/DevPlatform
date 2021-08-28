using DevPlatform.Domain.Api.StoryApi;
using FluentValidation;

namespace DevPlatform.Domain.Validation.StoryValidation
{
    public class StoryCreateApiValidator : AbstractValidator<StoryCreateApi>
    {
        public StoryCreateApiValidator()
        {
            RuleFor(m => m.Photo)
              .NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(m => m.Title)
                .NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(m => m.Description)
               .NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(p => p.Title).NotEqual("undefined").WithMessage(x => string.Format(ValidationMessage.InvalidValue, nameof(x.Title)));

            RuleFor(p => p.Description).NotEqual("undefined").WithMessage(x => string.Format(ValidationMessage.InvalidValue, nameof(x.Description)));
        }
    }
}
