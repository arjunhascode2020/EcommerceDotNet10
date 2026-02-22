using Catalog.Application.Commands;
using FluentValidation;

namespace Catalog.Application.Validators
{
    public class UpdateTypeCommandValidator : AbstractValidator<UpdateTypeCommand>
    {
        public UpdateTypeCommandValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty()
                .WithMessage("Product Type Id required.");
            RuleFor(t => t.Name)
                .NotEmpty()
                .WithMessage("Prodcut Type Name is required.")
                .MaximumLength(200)
                .WithMessage("Product Type Name cant npt excided more than 200 charcters.");

        }
    }
}
