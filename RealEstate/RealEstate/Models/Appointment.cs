using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models
{
    public class Appointment
    {

        [Key]
        public int AppointmentId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        public Property Property { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; }

    }
}
