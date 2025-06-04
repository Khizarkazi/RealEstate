using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.RepoDAL;

namespace RealEstate.Services
{
    public class PropertyServices : IPropertiesRepo
    {

        public RealEstateContext db;
        private IWebHostEnvironment env;

        public PropertyServices(RealEstateContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }



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

            var prod = new Models.Property()
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

        public List<Models.Property> GetAllProperties()
        {
            var data = db.Properties.ToList();
            return data;
        }



        public Models.Property GetPropertyById(int id)
        {
            var data = db.Properties.Find(id);
            return data;
        }



        //public void UploadFile(IFormFile file, string fpath)
        //{
        //    if (!Directory.Exists(Path.GetDirectoryName(fpath)))
        //    {
        //        Directory.CreateDirectory(Path.GetDirectoryName(fpath));
        //    }

        //    FileStream stream = new FileStream(fpath, FileMode.Create);
        //    file.CopyTo(stream);
        //}
        public void UploadFile(IFormFile file, string fpath)
        {
            
            string? directory = Path.GetDirectoryName(fpath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }            
            if (!System.IO.File.Exists(fpath))
            {
                
                using (FileStream stream = new FileStream(fpath, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(stream);
                }
            }
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
            db.Properties.Remove(dd);
            db.SaveChanges();
        }




        public List<Models.Property> GetPaginatedProperties(int page, int pageSize, out int totalProperties)
        {
            var query = db.Properties.OrderByDescending(p => p.CreatedAt);
            totalProperties = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }



    }
}
