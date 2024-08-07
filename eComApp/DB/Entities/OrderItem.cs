using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eComApp.DB.Entities
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;

        public int OrderId { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;

        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}