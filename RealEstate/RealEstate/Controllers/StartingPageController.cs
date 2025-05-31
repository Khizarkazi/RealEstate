using Microsoft.AspNetCore.Mvc;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    public class StartingPageController : Controller
    {
        RealEstateContext db;
        private readonly IWebHostEnvironment env;
        public StartingPageController(RealEstateContext db,IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
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

        [HttpPost]
            public IActionResult Login(string Email, string Password)
            {
                var user = db.Users.SingleOrDefault(x => x.Email == Email);

                if (user == null)
                {
                    TempData["erroremail"] = "Invalid EmailId";
                    return View();
                }

                if (user.PasswordHash != Password) // Consider using hashing comparison
                {
                    TempData["errorpass"] = "Invalid Password";
                    return View();
                }

                // Set session
                //HttpContext.Session.SetString("UserEmail", user.Email);
                //HttpContext.Session.SetString("UserRole", user.Role);

                // Redirect based on role
                return RedirectToDashboard(user.Role);


            }
            private IActionResult RedirectToDashboard(string role)
        {
            switch (role)
            {
                case "Admin":
                    return RedirectToAction("AdminDashboard", "Dashboard");

                case "Agent":
                    return RedirectToAction("AgentDashboard", "Dashboard");

                case "Buyer":
                    return RedirectToAction("BuyerDashboard", "Dashboard");

                case "Seller":
                    return RedirectToAction("SellerDashboard", "Dashboard");

                case "Tenant":
                    return RedirectToAction("TenantDashboard", "Dashboard");

                default:
                    TempData["errorrole"] = "Unknown role";
                    return RedirectToAction("Login");
            }
        }

    }


}
