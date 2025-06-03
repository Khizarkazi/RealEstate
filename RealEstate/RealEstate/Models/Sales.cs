using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Sales
    {
        [Key]
        public int SaleId { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        [ForeignKey("Buyer")]
        public int BuyerId { get; set; }
        public User Buyer { get; set; }

        [ForeignKey("Transaction")]
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }

        [Required]
        public decimal SaleAmount { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string PaymentMethod { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }  // e.g., Completed, Refunded, Failed, Pending

        public string Notes { get; set; }
    }

}
