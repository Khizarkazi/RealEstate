using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models
{
    public class ProfilePicView
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Phone, MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; } // Use enum or custom validation if needed
        [NotMapped]
        public IFormFile ProfilePicture { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Property>? Properties { get; set; }

    }
}
