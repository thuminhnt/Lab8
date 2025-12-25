using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [Required]
        public int CarModelId { get; set; }

        [ForeignKey(nameof(CarModelId))]
        public CarModel CarModel { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
