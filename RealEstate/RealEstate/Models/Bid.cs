using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Bid
    {
        [Key]
        public int BidId { get; set; }

        [Required]
        [ForeignKey("PropertyId")]
        public int PropertyId { get; set; }

        [NotMapped]
        public Property Property { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        
        public ProfilePicView User { get; set; }

        [Required]
        public decimal BidAmount { get; set; }

        public DateTime BidTime { get; set; } = DateTime.Now;

        public bool IsWinningBid { get; set; } = false;
    }
}
