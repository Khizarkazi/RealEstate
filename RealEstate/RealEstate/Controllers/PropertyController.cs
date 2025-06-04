using Microsoft.AspNetCore.Mvc;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.RepoDAL;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace RealEstate.Controllers
{
    public class PropertyController : Controller
    {
        IPropertiesRepo pro;
        public RealEstateContext db;

        public PropertyController(IPropertiesRepo pro, RealEstateContext db)
        {
            this.pro = pro;
            this.db=db;
        }

        public IActionResult Index()
        {
            var data = pro.GetAllProperties();
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




        public IActionResult usersidepage(string keyword, string city, string propertyType, string status, int page = 1)
        {
            int pageSize = 9;
            int totalProperties;

            var paginatedProperties = pro.GetPaginatedProperties(
                page, pageSize, out totalProperties,
                keyword, city, propertyType, status
            );

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProperties / pageSize);

            // Preserve filters in ViewBag for pagination
            ViewBag.Keyword = keyword;
            ViewBag.City = city;
            ViewBag.PropertyType = propertyType;
            ViewBag.Status = status;

            return View(paginatedProperties);
        }


        public IActionResult singlepage(int id)
        {
            var data = pro.GetPropertyById(id);
            return View(data);
        }

        

        public IActionResult singlepageformain(int id)
        {
            var data = pro.GetPropertyById(id);
            return View(data);
        }




    }
}
