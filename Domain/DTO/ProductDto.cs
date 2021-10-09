using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
    }
}