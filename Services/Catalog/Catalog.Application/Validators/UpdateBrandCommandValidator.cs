using Catalog.Application.Commands;
using FluentValidation;

namespace Catalog.Application.Validators
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(b => b.Id)
                .NotEmpty()
                .WithMessage("Product Brand Id is required.");
            RuleFor(b => b.Name)
                .NotEmpty()
                .WithMessage("{PropertyType} is required.")
                .MaximumLength(200)
                .WithMessage("Product Brand Name Can not excided more than 200 characters.");

        }
    }
}
