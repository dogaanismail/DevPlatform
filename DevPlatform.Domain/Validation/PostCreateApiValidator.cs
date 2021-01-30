using DevPlatform.Domain.Api;
using FluentValidation;
using System;

namespace DevPlatform.Domain.Validation
{
    public class PostCreateApiValidator : AbstractValidator<PostCreateApi>
    {
        public PostCreateApiValidator()
        {
            RuleFor(p => p.Text).NotEmpty().When(post => String.IsNullOrEmpty(post.Text));
        }
    }
}
