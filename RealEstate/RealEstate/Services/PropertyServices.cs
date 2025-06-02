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



        public void UploadFile(IFormFile file, string fpath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(fpath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fpath));
            }

            FileStream stream = new FileStream(fpath, FileMode.Create);
            file.CopyTo(stream);
        }

        List<Models.Property> IPropertiesRepo.FetchProperties()
        {
            var data = db.Properties.ToList();
            return data;
        }
    }
}
