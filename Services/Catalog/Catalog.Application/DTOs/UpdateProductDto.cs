using System.ComponentModel.DataAnnotations;

namespace Catalog.Application.DTOs
{
    public record class UpdateProductDto
    {
        [Required]
        public string Name { get; init; } = string.Empty;
        [Required]
        public string Summary { get; init; } = string.Empty;
        [Required]
        public string Description { get; init; } = string.Empty;
        [Required]
        public string ImageFile { get; init; } = string.Empty;
        [Required]
        public string BrandId { get; init; } = string.Empty;
        [Required]
        public string TypeId { get; init; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; init; } = 0;


    }

}
