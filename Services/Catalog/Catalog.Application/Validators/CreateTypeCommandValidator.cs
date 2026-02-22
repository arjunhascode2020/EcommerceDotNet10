using Catalog.Application.Commands;
using FluentValidation;

namespace Catalog.Application.Validators
{
    public class CreateTypeCommandValidator : AbstractValidator<CreateTypeCommand>
    {
        public CreateTypeCommandValidator()
        {
            RuleFor(t => t.Name)
                  .NotEmpty()
                  .WithMessage("{PropertyType} is required.")
                  .MaximumLength(200)
                  .WithMessage("Product Type Name Can not excided from 200 charcters.");
        }
    }
}
