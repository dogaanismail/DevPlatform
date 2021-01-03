using DevPlatform.Domain.Api.AlbumApi;
using FluentValidation;

namespace DevPlatform.Domain.Validation.AlbumValidation
{
    public class AlbumCreateApiValidator :  AbstractValidator<AlbumCreateApi>
    {
        public AlbumCreateApiValidator()
        {
            RuleFor(m => m.Images)
              .NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(m => m.Name)
                .NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(m => m.Place)
               .NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(m => m.Tag)
               .NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(m => m.Date)
               .NotEmpty().WithMessage(ValidationMessage.Required);
        }
    }
}
