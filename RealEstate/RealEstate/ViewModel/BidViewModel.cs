using RealEstate.Models;

namespace RealEstate.ViewModel
{
    public class BidViewModel
    {
        public Property Property { get; set; }
        public List<Bid> Bids { get; set; }
        public decimal YourBidAmount { get; set; }
    }
}
