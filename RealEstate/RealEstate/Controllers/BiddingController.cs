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

        // GET: Show the bidding page
        public async Task<IActionResult> Place(int propertyId)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            if (property == null) return NotFound();

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = string.IsNullOrEmpty(userIdString) ? 0 : int.Parse(userIdString);

            var viewModel = new PlaceBidViewModel
            {
                PropertyId = property.PropertyId,
                PropertyTitle = property.Title,
                Price = property.Price,
                UserId = userId
            };

            return View(viewModel);
        }

        // POST: Submit a bid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Place(PlaceBidViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reload property info if validation failed
                var property = await _context.Properties.FindAsync(model.PropertyId);
                if (property != null)
                {
                    model.PropertyTitle = property.Title;
                    model.Price = property.Price;
                }
                return View(model);
            }

            var bid = new Bid
            {
                PropertyId = model.PropertyId,
                UserId = model.UserId,
                BidAmount = model.BidAmount,
                BidTime = DateTime.Now
            };

            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Properties", new { id = model.PropertyId });
        }

        // List bids
        [HttpGet]
        public async Task<IActionResult> List(int propertyId)
        {
            var prop = await _context.Properties
                .Include(p => p.Bids).ThenInclude(b => b.User)
                .FirstOrDefaultAsync(p => p.PropertyId == propertyId);
            if (prop == null) return NotFound();
            return View(prop);
        }

        // Accept bid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int bidId)
        {
            var bid = await _context.Bids
                .Include(b => b.Property)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BidId == bidId);

            if (bid == null) return NotFound();

            bid.IsWinningBid = true;
            bid.Property.WinningBidId = bid.BidId;

            _context.Notifications.Add(new Notification
            {
                UserId = bid.UserId,
                Message = $"Congrats! You've won the bid for '{bid.Property.Title}'",
                CreatedAt = DateTime.Now
            });

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List), new { propertyId = bid.PropertyId });
        }
        [HttpGet] [ValidateAntiForgeryToken]
        // GET: Show buyer's bids and status (accepted or not)
        public async Task<IActionResult> MyBids()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();
            int userId = int.Parse(userIdString);

            var myBids = await _context.Bids
                .Where(b => b.UserId == userId)
                .Include(b => b.Property)
                .ToListAsync();

            return View(myBids);
        }
    }

}
