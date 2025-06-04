using Microsoft.AspNetCore.Mvc;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.RepoDAL;
using System.Data;

namespace RealEstate.Controllers
{
    public class StartingPageController : Controller
    {
        RealEstateContext db;
        private readonly IWebHostEnvironment env;
        IPropertiesRepo pro;
        public StartingPageController(RealEstateContext db,IWebHostEnvironment env, IPropertiesRepo pro)
        {
            this.db = db;
            this.env = env;
            this.pro = pro;
        }
        //public IActionResult Index(int page = 1)
        //{
        //    int pageSize = 9;
        //    int totalProperties;
        //    var paginatedProperties = pro.GetPaginatedProperties(page, pageSize, out totalProperties);

        //    ViewBag.CurrentPage = page;
        //    ViewBag.TotalPages = (int)Math.Ceiling((double)totalProperties / pageSize);

        //    return View(paginatedProperties);
        //}

        public IActionResult Index(string keyword, string city, string propertyType, string status, int page = 1)
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


        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Login(string Email, string PasswordHash)
        {
            var user = db.Users.SingleOrDefault(x => x.Email == Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid EmailId");
                return View();
            }

            if (user.PasswordHash != PasswordHash)  // Replace with hashed comparison in production
            {
                ModelState.AddModelError("", "Invalid Password");
                return View();
            }

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserID", user.UserId.ToString());

            switch (user.Role)
            {
                case "Admin":
                    return RedirectToAction("Dashboard", "Admin");

                case "Agent":
                    return RedirectToAction("Dashboard", "Agent");

                case "Buyer":
                    return RedirectToAction("Dashboard", "Buyer");

                case "Seller":
                    return RedirectToAction("Dashboard", "Seller");

                case "Tenant":
                    return RedirectToAction("Dashboard", "Tenant");

                default:
                    ModelState.AddModelError("", "Unknown role");
                    return View();
            }
        }



        public IActionResult Register()
        {
            return View();
        }
        public IActionResult AgentDashboard()
        {
            return View();
        }

        public void UploadFile(IFormFile file, string fpath)
        {
            using (var stream = new FileStream(fpath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }



        [HttpPost]
        
        public IActionResult Register(ProfilePicView e)

        {
            e.CreatedAt=DateTime.Now;
            if (ModelState.IsValid)
            {
                string path = env.WebRootPath;//fetch www.root folder location
                string filepath = "Content/Images/" + e.ProfilePicture.FileName;//path of your file
                string fullpath = Path.Combine(path, filepath);
               

                if (e.ProfilePicture != null && e.ProfilePicture.Length > 0)
                {
                    UploadFile(e.ProfilePicture, fullpath);
                }

                var prod = new User()
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    Role = e.Role,
                    PasswordHash = e.PasswordHash, // consider hashing
                    ProfilePicture = "/"+filepath,

                    CreatedAt=e.CreatedAt

                };

                
                    db.Users.Add(prod);
                    db.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Register");
            }
        }

        //    private IActionResult RedirectToDashboard(string role)
        //{
        //    switch (role)
        //    {
        //        case "Admin":
        //            return RedirectToAction("Dashboard", "Admin");

        //        case "Agent":
        //            return RedirectToAction("AgentDashboard", "Dashboard");

        //        case "Buyer":
        //            return RedirectToAction("BuyerDashboard", "Dashboard");

        //        case "Seller":
        //            return RedirectToAction("SellerDashboard", "Dashboard");

        //        case "Tenant":
        //            return RedirectToAction("TenantDashboard", "Dashboard");

        //        default:
        //            TempData["errorrole"] = "Unknown role";
        //            return RedirectToAction("Login");
        //    }
        //}



        public IActionResult Logout()
        {
            
            HttpContext.Session.Clear();

             
            return RedirectToAction("Login", "StartingPage");
        }




    }


}
