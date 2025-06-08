using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Inquiry
    {
        [Key]
        public int InquiryId { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending"; // Pending, Replied, Closed

        public string? Response { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; } 
    }
}
