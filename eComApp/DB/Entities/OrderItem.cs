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
        public virtual Order Order { get; set; }

        public int OrderId { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}