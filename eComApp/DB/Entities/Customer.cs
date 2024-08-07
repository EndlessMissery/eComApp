using System.ComponentModel.DataAnnotations;

namespace eComApp.DB.Entities
{
    public class Customer
    {
        [Key]
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
        public string PhoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        [Required]
        public DateTime RegistrationDate { get; set; }
    }
}