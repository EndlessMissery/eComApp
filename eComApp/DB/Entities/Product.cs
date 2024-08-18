using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eComApp.DB.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [NotMapped]
        public bool IsActive { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}