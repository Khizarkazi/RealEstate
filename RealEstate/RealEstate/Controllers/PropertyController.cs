using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
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


        //public IActionResult singlepage(int id)
        //{
        //    var data = pro.GetPropertyById(id);


        //    return View(data);
        //}

        public IActionResult singlepage(int id)
        {
            var property = pro.GetPropertyById(id);

            var vm = new PropertyReviewViewModel
            {
                Property = property,
                Review = new Review()
            };

            return View(vm);
        }


        //[HttpPost]
        //public IActionResult singlepage(Property p)
        //{
        //    var data = new Review
        //    {
        //        PropertyId=p.PropertyId,
        //        UserId=p.UserId,
        //        Rating=p.review.Rating,
        //        ReviewText=p.review.ReviewText,
        //        ReviewDate=p.review.ReviewDate
        //    };

        //    db.Reviews.Add(data);
        //    db.SaveChanges();

        //    return RedirectToAction("singlepage");
        //}

        [HttpPost]
        public IActionResult singlepage(PropertyReviewViewModel vm)
        {
            db.Reviews.Add(vm.Review);
            db.SaveChanges();

            return RedirectToAction("singlepage", new { id = vm.Review.PropertyId });
        }






        public IActionResult singlepageformain(int id)
        {
            var data = pro.GetPropertyById(id);
            return View(data);
        }


        [HttpPost]
        public IActionResult Booking(PropertyReviewViewModel vs)
        {
             pro.booking(vs);
            TempData["msg"] = "Booking Done Succesfully";
            return RedirectToAction("fetcchbookingbyid");

            // Return to the same singlepage
            //return RedirectToAction("singlepage", new { id = vs.Review.PropertyId });
        }

        [HttpGet]
        public IActionResult bookingpage()
        {
            var data = pro.fetchbooking();            
            return View(data);
        }

        [HttpGet]
        public IActionResult fetcchbookingbyid(int userid)
        {
            userid = int.Parse(HttpContext.Session.GetString("UserID").ToString());
            var data = pro.fetcchbookingbyid(userid);            
            return View(data);
        }


        public IActionResult DeleteBookinguser(int id)
        {
            pro.deletebooking(id);
            TempData["delete"] = "Booking Deleted Succesfully";
            return RedirectToAction("fetcchbookingbyid");
        }



        //public IActionResult buy(int id)
        //{
        //    var data=db.Bookings.Find(id);

        //    var amt = db.Properties.Where(x => x.PropertyId == data.PropertyId).Select(x => x.Price).FirstOrDefault();

        //    var tran = new Transaction()
        //    {
        //        BookingId = data.BookingId,
        //        Amount =amt,
        //        PaymentMethod="Online",
        //        PaymentStatus="Success",
        //        TransactionDate=DateTime.Now
        //    };

        //    db.Transactions.Add(tran);
        //    db.SaveChanges();

        //    var dd= db.Bookings.Find(id);
        //    db.Bookings.Remove(dd);
        //    db.SaveChanges();

        //    return View(amt);
        //}

        public IActionResult Buy(int id)
        {
            var data = db.Bookings.Find(id);
            var amount = double.Parse(db.Properties
                           .Where(x => x.PropertyId == data.PropertyId)
                           .Select(x => x.Price)
                           .FirstOrDefault().ToString());

            ViewBag.BookingId = id;
            ViewBag.Amount = amount;

            return View();
        }


        //public IActionResult Buy(int id)
        //{
        //    var data = db.Bookings.Find(id);
        //    var amount = db.Properties
        //                   .Where(x => x.PropertyId == data.PropertyId)
        //                   .Select(x => x.Price)
        //                   .FirstOrDefault();

        //    RazorpayClient client = new RazorpayClient("rzp_test_Kl7588Yie2yJTV", "6dN9Nqs7M6HPFMlL45AhaTgp");

        //    Dictionary<string, object> options = new Dictionary<string, object>();
        //    options.Add("amount", amount * 100);
        //    options.Add("currency", "INR");
        //    options.Add("receipt", "order_rcptid_11");
        //    options.Add("payment_capture", 1);

        //    Razorpay.Api.Order order = client.Order.Create(options);
        //    ViewBag.OrderId = order["id"].ToString();
        //    ViewBag.Amount = amount;
        //    ViewBag.BookingId = id;

        //    return View();
        //}



        public IActionResult PaymentSuccess(string paymentId, int bookingId)
        {
            var data = db.Bookings.Find(bookingId);
            var amount = db.Properties
                           .Where(x => x.PropertyId == data.PropertyId)
                           .Select(x => x.Price)
                           .FirstOrDefault();

            var tran = new Transaction()
            {
                BookingId = data.BookingId,
                Amount = amount,
                PaymentMethod = "Online",
                PaymentStatus = "Success",
                TransactionDate = DateTime.Now,
               
            };

            db.Transactions.Add(tran);
            db.SaveChanges();
            db.Bookings.Remove(data);
            db.SaveChanges();

            return RedirectToAction("fetcchbookingbyid");
        }





    }
}
