using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.RepoDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public class PropertyServices : IPropertiesRepo
    {
        private readonly RealEstateContext db;
        private readonly IWebHostEnvironment env;

        public PropertyServices(RealEstateContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        // ========== Property Methods ==========

        public void addproperty(Propertyviews view)
        {
            var mpath = new List<string>();

            foreach (var f in view.Pimg)
            {
                string path = env.WebRootPath; // wwwroot path
                string filepath = "Content/Images/" + f.FileName;
                string fullpath = Path.Combine(path, filepath);

                UploadFile(f, fullpath); // Save file

                mpath.Add(filepath); // Collect image path
            }

                                                                                      

            


            //var prod = new Property()

            var prod = new Models.Property()


            //var prod = new Property()


            {
                Title = view.Title,
                Description = view.Description,
                Price = view.Price,
                Address = view.Address,
                City = view.City,
                State = view.State,
                ZipCode = view.ZipCode,
                PropertyType = view.PropertyType,
                Status = view.Status,
                UserId = view.UserId,
                CreatedAt = view.CreatedAt,
                Pimg = string.Join(",", mpath), // Join all image paths
            };

            try
            {
                db.Properties.Add(prod);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB Save Error: " + ex.Message);
            }
        }

        public async Task AddPropertyAsync(Propertyviews model)
        {
            var mpath = new List<string>();

            foreach (var f in model.Pimg)
            {
                string path = env.WebRootPath;
                string folder = "Content/Images";
                string filepath = Path.Combine(folder, f.FileName);
                string fullpath = Path.Combine(path, filepath);

                UploadFile(f, fullpath);
                mpath.Add(filepath.Replace("\\", "/"));
            }

            var property = new Property()
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                Address = model.Address,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode,
                PropertyType = model.PropertyType,
                Status = model.Status,
                UserId = model.UserId,
                CreatedAt = model.CreatedAt,
                Pimg = string.Join(",", mpath),
            };

            db.Properties.Add(property);
            await db.SaveChangesAsync();
        }


        public List<Property> GetAllProperties()
        {
            return db.Properties.ToList();
        }

        public Property GetPropertyById(int id)
        {
            return db.Properties.Find(id);
        }

        public void updateproperty(Propertyviews id)
        {
            var existing = db.Properties.Find(id.PropertyId);
            if (existing == null) return;

            existing.Title = id.Title;
            existing.Description = id.Description;
            existing.Price = id.Price;
            existing.Address = id.Address;
            existing.City = id.City;
            existing.State = id.State;
            existing.ZipCode = id.ZipCode;
            existing.PropertyType = id.PropertyType;
            existing.Status = id.Status;
            existing.CreatedAt = id.CreatedAt;

            if (id.Pimg != null && id.Pimg.Any())
            {
                var mpath = new List<string>();

                foreach (var f in id.Pimg)
                {
                    string path = env.WebRootPath;
                    string folder = "Content/Images";
                    string filepath = Path.Combine(folder, f.FileName);
                    string fullpath = Path.Combine(path, filepath);

                    UploadFile(f, fullpath);

                    mpath.Add(filepath.Replace("\\", "/")); // ensure web-compatible paths
                }

                existing.Pimg = string.Join(",", mpath);
            }

            try
            {
                db.Properties.Update(existing);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB Save Error: " + ex.Message);
            }
        }

        public void deleteproperty(int id)
        {
            var dd = db.Properties.Find(id);
            if (dd != null)
            {
                db.Properties.Remove(dd);
                db.SaveChanges();
            }
        }





        //public List<Models.Property> GetPaginatedProperties(int page, int pageSize, out int totalProperties)
        //{
        //    var query = db.Properties.OrderByDescending(p => p.CreatedAt);
        //    totalProperties = query.Count();
        //    return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        //}

        public List<Models.Property> GetPaginatedProperties(
    int page, int pageSize, out int totalProperties,
    string keyword = null, string city = null, string propertyType = null, string status = null)

        //public List<Property> GetPaginatedProperties(int page, int pageSize, out int totalProperties)

        {
            var query = db.Properties.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(p => p.Title.Contains(keyword) || p.Description.Contains(keyword) || p.Address.Contains(keyword));

            if (!string.IsNullOrEmpty(city))
                query = query.Where(p => p.City.Contains(city));

            if (!string.IsNullOrEmpty(propertyType))
                query = query.Where(p => p.PropertyType.Contains(propertyType));

            if (!string.IsNullOrEmpty(status))
                query = query.Where(p => p.Status.Contains(status));

            query = query.OrderByDescending(p => p.CreatedAt);

            totalProperties = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    public void UploadFile(IFormFile file, string fpath)
        {
            string? directory = Path.GetDirectoryName(fpath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(fpath))
            {
                using (FileStream stream = new FileStream(fpath, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(stream);
                }
            }
        }



        public async Task UploadFileAsync(IFormFile file, string fpath)
        {
            string? directory = Path.GetDirectoryName(fpath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(fpath))
            {
                using (FileStream stream = new FileStream(fpath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(stream);
                }
            }
        }



       public void booking(PropertyReviewViewModel vs)
        {
            
            var data = new Booking
            {
                PropertyId = vs.Review.PropertyId,
                UserId=vs.Review.UserId,
                BookingDate=vs.Review.ReviewDate,
                Status="Inactive",


            };

            db.Bookings.Add(data);
            db.SaveChanges();
        }



       public List<Booking> fetchbooking()
        {
            return db.Bookings.ToList();
        }



        public List<Booking> fetcchbookingbyid(int userid)
        {
            return db.Bookings.Where(x => x.UserId==userid).ToList();
        }


       public void deletebooking(int id)
        {
            var dd = db.Bookings.Find(id);
            if (dd != null)
            {
                db.Bookings.Remove(dd);
                db.SaveChanges();
            }
        }



        public Booking getbookingid(int id)
        {
            var data= db.Bookings.Find(id);
            return data;
        }


        public void updatebooking(Booking id)
        {
            var data = db.Bookings.Find(id.BookingId);


            data.PropertyId = id.PropertyId;
            data.Status = id.Status;
            data.UserId=id.UserId;
            data.BookingDate=id.BookingDate;
            

            db.Bookings.Update(data);
            db.SaveChanges();
        }










        // ========== Appointment Methods ==========

        public Appointment AddAppointment(Appointment ap)
        {
            try
            {
                // Example: assign a default user for demo; in real app, assign logged-in user's id
                if (ap.UserId == 0) ap.UserId = 1;

                db.Appointments.Add(ap);
                db.SaveChanges();
                return ap;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving appointment: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        public List<Appointment> apn()
        {
            return db.Appointments
                     .OrderByDescending(a => a.AppointmentDate)
                     .ToList();
        }

        public List<Appointment> GetAppointmentsByBuyerEmail(string buyerEmail)
        {
            return db.Appointments
                     .Where(a => a.BuyerEmail == buyerEmail)
                     .OrderByDescending(a => a.AppointmentDate)
                     .ToList();
        }

        public List<Appointment> GetAllAppointments()
        {
            return db.Appointments.ToList();
        }

        public List<Property> GetPropertiesBySellerId(int sellerId)
        {
            return db.Properties
                     .Where(p => p.UserId == sellerId)
                     .OrderByDescending(p => p.CreatedAt)
                     .ToList();
        }

        public List<Appointment> GetAppointmentsForSeller(int sellerId)
        {
            var sellerPropertyIds = db.Properties
                                       .Where(p => p.UserId == sellerId)
                                       .Select(p => p.PropertyId)
                                       .ToList();

            return db.Appointments
                     .Where(a => sellerPropertyIds.Contains(a.PropertyId))
                     .OrderByDescending(a => a.AppointmentDate)
                     .ToList();
        }

    }
}
