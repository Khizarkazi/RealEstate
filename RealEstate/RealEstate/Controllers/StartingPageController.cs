using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Controllers
{
    public class StartingPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        
    }
}
