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

        List<Property> GetPaginatedProperties(int page, int pageSize, out int totalProperties);


    }
}
