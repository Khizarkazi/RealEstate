using Microsoft.AspNetCore.Mvc;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.RepoDAL;

namespace RealEstate.Controllers
{
    public class AdminController : Controller
    {

        IPropertiesRepo pro;
        public RealEstateContext db;

        public AdminController(IPropertiesRepo pro, RealEstateContext db)
        {
            this.pro = pro;
            this.db = db;
        }



        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        
        public IActionResult allbookings()
        {
            var data = pro.fetchbooking();
            return View(data);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = pro.getbookingid(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Booking id)
        {
            pro.updatebooking(id);
            TempData["update"] = "Booking updated successfully!";
            return RedirectToAction("allbookings");
        }

    }
}
