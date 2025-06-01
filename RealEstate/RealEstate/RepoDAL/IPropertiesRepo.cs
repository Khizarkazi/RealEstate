using RealEstate.Models;

namespace RealEstate.RepoDAL
{
    public interface IPropertiesRepo
    {
        List<Property> FetchProperties();

        void addproperty(Propertyviews view);
    }
}
