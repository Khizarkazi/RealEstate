using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Notification
    {
        [Key] 
        public int Id { get; set; }

        [Required] 
        public int UserId { get; set; }
        [ForeignKey("UserId")] 
        public ProfilePicView User { get; set; }

        [Required] 
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
