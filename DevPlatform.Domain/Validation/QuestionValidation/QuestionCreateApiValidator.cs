using DevPlatform.Domain.Api.QuestionApi;
using FluentValidation;

namespace DevPlatform.Domain.Validation.QuestionValidation
{
    public class QuestionCreateApiValidator : AbstractValidator<QuestionCreateApi>
    {
        public QuestionCreateApiValidator()
        {
            RuleFor(q => q.Title)
             .NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(q => q.Description)
            .NotEmpty().WithMessage(ValidationMessage.Required);
        }
    }
}
