using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;

namespace RealEstate.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly RealEstateContext _context;

        public NotificationsController(RealEstateContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int userId)
        {
            var notes = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
            return View(notes);
        }

        [HttpPost]
        public async Task<IActionResult> MarkRead(int id)
        {
            var note = await _context.Notifications.FindAsync(id);
            if (note == null) return NotFound();
            note.IsRead = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { userId = note.UserId });
        }
    }
}
