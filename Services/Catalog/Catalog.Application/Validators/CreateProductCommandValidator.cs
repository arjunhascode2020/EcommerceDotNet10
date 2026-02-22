using Catalog.Application.Commands;
using FluentValidation;

namespace Catalog.Application.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("{PropertyType} is required.")
                .MaximumLength(200)
                .WithMessage("Product Name Can not Excided from 200 characters.")
                ;
            RuleFor(p => p.Summary)
                .NotEmpty()
                .WithMessage("{PropertyType} is required.");
            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("{PropertyType} is required.");
            RuleFor(p => p.ImageFile)
                .NotEmpty()
                .WithMessage("{PropertyType} is required.");
            RuleFor(p => p.BrandId)
                .NotEmpty()
                .WithMessage("Product BrandId required.");
            RuleFor(p => p.TypeId)
                .NotEmpty()
                .WithMessage("Product TypeId required.");
            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("Prodcut Price Can not be negative or Zero.");
        }
    }
}
