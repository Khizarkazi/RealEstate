using RealEstate.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Migrations
{
    public class Samplesales
    {
        [Key]
        public int SaleId { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public int BuyerId { get; set; }
        public User Buyer { get; set; }

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
