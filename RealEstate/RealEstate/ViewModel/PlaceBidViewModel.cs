using System.ComponentModel.DataAnnotations;

namespace RealEstate.ViewModel
{
    public class PlaceBidViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyTitle { get; set; }
        public decimal Price { get; set; }

        public int UserId { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Bid must be greater than 0")]
        public decimal BidAmount { get; set; }
    }
}
