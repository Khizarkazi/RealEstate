using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.RepoDAL;

namespace RealEstate.Controllers
{
    public class InquiryController : Controller
    {
        private readonly IInquiryRepo _service;

        public InquiryController(IInquiryRepo service)
        {
            _service = service;
        }

        public IActionResult MyInquiries()
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
                return RedirectToAction("Login", "StartingPage");

            var data = _service.GetByUserId(userId);
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string message)
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
                return RedirectToAction("Login", "StartingPage");

            var inquiry = new Inquiry
            {
                Message = message,
                UserId = userId
            };

            _service.AddInquiry(inquiry);
            return RedirectToAction("MyInquiries");
        }

        // Admin/Agent/Seller View
        public IActionResult AdminInbox()
        {
            var data = _service.GetAll();
            return View(data);
        }

        public IActionResult Respond(int id)
        {
            var inquiry = _service.GetById(id);
            return View(inquiry);
        }

        [HttpPost]
        public IActionResult Respond(int id, string response)
        {
            var inquiry = _service.GetById(id);
            if (inquiry != null)
            {
                inquiry.Response = response;
                inquiry.Status = "Replied";
                _service.Update(inquiry);
            }

            return RedirectToAction("AdminInbox");
        }
    }
}
