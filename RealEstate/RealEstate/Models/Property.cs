using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        public string Pimg { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required, MaxLength(255)]
        public string Address { get; set; }

        public string City { get; set; }
        public string State { get; set; }

        [MaxLength(10)]
        public string ZipCode { get; set; }

        [Required]
        public string PropertyType { get; set; }

        [Required]
        public string Status { get; set; }

        [ForeignKey("Owner")]
        public int UserId { get; set; }

        public User Owner { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

       

        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<LeaseAgreement>? LeaseAgreements { get; set; }


    }
}
