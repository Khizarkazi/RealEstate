using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models
{
    public class Review
    {

        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        public Property Property { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string ReviewText { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.Now;


    }
}
