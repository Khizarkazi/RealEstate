using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        public Property Property { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        public string Status { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

    }
}
