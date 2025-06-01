using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.RepoDAL;

namespace RealEstate.Controllers
{
    public class PropertyController : Controller
    {
        IPropertiesRepo pro;
        public PropertyController(IPropertiesRepo pro)
        {
            this.pro = pro;
        }

        public IActionResult Index()
        {
            var data = pro.FetchProperties();
            return View(data);

        }

        [HttpGet]
        public IActionResult AddProperty()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProperty(Propertyviews vie)
        {

            //vie.UserId = int.Parse(HttpContext.Session.GetString("UserID"));
            //vie.CreatedAt = DateTime.Now;


            if (ModelState.IsValid)
            {
                

                pro.addproperty(vie);
                TempData["msg"] = "Property Added Succesfully";
                return RedirectToAction("Index");

            }
            //else
            //{
            //    return View();
            //}
            // Log errors to see what's failing
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(vie); // Also return the same model to repopulate fields
        }

        

       
    }
}
