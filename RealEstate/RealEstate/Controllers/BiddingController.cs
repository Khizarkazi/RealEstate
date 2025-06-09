using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.ViewModel;
using System.Security.Claims;
using System;
using RealEstate.Data;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Controllers
{
  
    public class BiddingController : Controller
    {
        private readonly RealEstateContext _context;

        public BiddingController(RealEstateContext context)
        {
            _context = context;
        }

        public IActionResult Details(int propertyId)
        {
            var property = _context.Properties
                .Include(p => p.Owner)
                .FirstOrDefault(p => p.PropertyId == propertyId);

            if (property == null)
            {
                return NotFound(); // Or redirect to an error page
            }

            var bids = _context.Bids
                .Include(b => b.User) // Include the User to avoid null reference in view
                .Where(b => b.PropertyId == propertyId)
                .OrderByDescending(b => b.BidAmount)
                .ToList();

            var model = new BidViewModel
            {
                Property = property,
                Bids = bids
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult PlaceBid(int propertyId, decimal yourBidAmount)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); // Assume authentication
            var property = _context.Properties.FirstOrDefault(p => p.PropertyId == propertyId);
            var highestBid = _context.Bids
                .Where(b => b.PropertyId == propertyId)
                .OrderByDescending(b => b.BidAmount)
                .FirstOrDefault();

            if (property == null)
                return NotFound();

            if (yourBidAmount <= property.Price || (highestBid != null && yourBidAmount <= highestBid.BidAmount))
            {
                TempData["Error"] = "Your bid must be higher than the current highest bid.";
                return RedirectToAction("Details", new { propertyId });
            }

            var newBid = new Bid
            {
                PropertyId = propertyId,
                UserId = userId,
                BidAmount = yourBidAmount
            };

            _context.Bids.Add(newBid);
            _context.SaveChanges();

            TempData["Success"] = "Your bid was placed successfully!";
            return RedirectToAction("Details", new { propertyId });
        }
    }
}
