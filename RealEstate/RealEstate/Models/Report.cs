using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        public string ReportType { get; set; }

        [Required]
        public string ReportData { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
