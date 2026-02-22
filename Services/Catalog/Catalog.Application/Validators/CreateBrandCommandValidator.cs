using Catalog.Application.Commands;
using FluentValidation;

namespace Catalog.Application.Validators
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty()
                .WithMessage("{PropertyType} is required.")
                .MaximumLength(200)
                .WithMessage("Brand Name Can not Excided from 200 charatcers");
        }
    }
}
