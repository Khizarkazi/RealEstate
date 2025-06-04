using RealEstate.Models;

namespace RealEstate.RepoDAL
{
    public interface IPropertiesRepo
    {
        List<Property> GetAllProperties();

        void addproperty(Propertyviews view);

        Property GetPropertyById(int id);

        void updateproperty(Propertyviews id);

        void deleteproperty(int id);

        //List<Property> GetPaginatedProperties(int page, int pageSize, out int totalProperties);

        List<Models.Property> GetPaginatedProperties(
   int page, int pageSize, out int totalProperties,
   string keyword = null, string city = null, string propertyType = null, string status = null);


    }
}
