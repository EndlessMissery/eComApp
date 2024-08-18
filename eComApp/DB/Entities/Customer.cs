using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eComApp.DB.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Phone]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }
    }

}