using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models
{
    public class LeaseAgreement
    {
        [Key]
        public int LeaseId { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        public Property Property { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        public User Tenant { get; set; }

        public DateTime LeaseStartDate { get; set; }

        public DateTime LeaseEndDate { get; set; }

        public decimal RentAmount { get; set; }

        public decimal SecurityDeposit { get; set; }

    }
}
