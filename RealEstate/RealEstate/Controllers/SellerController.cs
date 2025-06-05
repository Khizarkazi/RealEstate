using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.RepoDAL;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealEstate.Controllers
{
    public class SellerController : Controller
    {
        private readonly IPropertiesRepo _propertiesRepo;

        public SellerController(IPropertiesRepo propertiesRepo)
        {
            _propertiesRepo = propertiesRepo;
        }

        [HttpGet]
        public IActionResult AddProperty()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProperty(Propertyviews model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                model.CreatedAt = DateTime.Now;

                // Get logged-in user id (example, adjust based on your auth setup)
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    model.UserId = int.Parse(userIdClaim.Value);
                }
                else
                {
                    // For testing fallback user id
                    model.UserId = 1;
                }

                await _propertiesRepo.AddPropertyAsync(model);

                TempData["Success"] = "Property added successfully.";
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error adding property: " + ex.Message);
                return View(model);
            }
        }

        public IActionResult Dashboard()
        {
            // Implement your dashboard logic here
            return View();
        }
    }
}
