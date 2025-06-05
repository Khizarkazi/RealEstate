using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;  // <-- add this

namespace RealEstate.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ValidateNever]      // <-- add this
        public User User { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        [ValidateNever]      // <-- add this
        public Property Property { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        [StringLength(50)]
        public string AgentRemark { get; set; } = "Pending";

        [StringLength(50)]
        public string PreferredSlots { get; set; }

        [StringLength(20)]
        public string BuyerContactNumber { get; set; }

        [StringLength(100)]
        public string BuyerEmail { get; set; }
    }
}
