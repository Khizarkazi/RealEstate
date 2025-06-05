using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.RepoDAL;

namespace RealEstate.Controllers
{
    public class PropertyReportController : Controller
    {
        private readonly IPropertyRepo _reportRepo;

        public PropertyReportController(IPropertyRepo reportRepo)
        {
            _reportRepo = reportRepo;
        }

        public async Task<IActionResult> PorpertyReportview()
        {
            var report = await _reportRepo.GetPropertyReportAsync();
            return View("PropReport", report);
        }
    }
}
