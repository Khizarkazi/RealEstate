using Microsoft.AspNetCore.Mvc;
using RealEstate.RepoDAL;

namespace RealEstate.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepo _service;

        public TransactionController(ITransactionRepo service)
        {
            _service = service;
        }

        public IActionResult History()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
            {
                TempData["Error"] = "Please login first.";
                return RedirectToAction("Login", "Admin");
            }

            var transactions = _service.GetTransactionsByUserId(userId);
            return View(transactions);
        }
    }
}
