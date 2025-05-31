using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Controllers
{
    public class TenantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }

    }
}
