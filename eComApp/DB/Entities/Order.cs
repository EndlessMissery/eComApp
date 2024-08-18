using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eComApp.DB.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.New;

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}