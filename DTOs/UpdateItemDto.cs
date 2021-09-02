using System.ComponentModel.DataAnnotations;

namespace Catalog.DTOs
{
    public record UpdateItemDto
    {
        [Required]
        public string Name { get; init; }
        
        [Required]
        [Range(0, 1000)]
        public decimal Price { get; init; }
    }
}