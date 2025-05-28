using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [ForeignKey("Booking")]
        public int BookingId { get; set; }

        public Booking Booking { get; set; }

        public decimal Amount { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.Now;


    }
}
