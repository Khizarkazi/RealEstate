using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.RepoDAL;

namespace RealEstate.Controllers
{
    public class AgentController : Controller
    {

        IPropertiesRepo pro;
        public AgentController(IPropertiesRepo pro)
        {
            this.pro = pro;
        }


        public IActionResult Index()
        {
            var data = pro.GetAllProperties();
            return View(data);
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddProperty()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProperty(Propertyviews vie)
        {

            vie.UserId = int.Parse(HttpContext.Session.GetString("UserID").ToString());
            vie.CreatedAt = DateTime.Now;



            pro.addproperty(vie);
            TempData["msg"] = "Property Added Succesfully";
            return RedirectToAction("Index");


        }



        public IActionResult Edit(int id)
        {


            var data = pro.GetPropertyById(id);
            if (data == null)
            {
                return NotFound();
            }

            var viewModel = new Propertyviews
            {
                PropertyId = data.PropertyId,
                Title = data.Title,
                Description = data.Description,
                Price = data.Price,
                Address = data.Address,
                City = data.City,
                State = data.State,
                ZipCode = data.ZipCode,
                PropertyType = data.PropertyType,
                Status = data.Status,
                CreatedAt = data.CreatedAt,
                Pimg = new List<IFormFile>() // for new image uploads
            };

            return View(viewModel);
        }

        // POST: Property/Edit
        [HttpPost]

        public IActionResult Edit(Propertyviews vi)
        {

            pro.updateproperty(vi);
            TempData["update"] = "Property updated successfully!";
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            pro.deleteproperty(id);
            TempData["delete"] = "Property Deleted Succesfully";
            return RedirectToAction("Index");
        }





    }
}
