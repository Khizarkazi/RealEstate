using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Bid
    {
        [Key]
        public int BidId { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public Property Property { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public ProfilePicView User { get; set; }

        [Required]
        public decimal BidAmount { get; set; }

        public DateTime BidTime { get; set; } = DateTime.Now;
    }
}
