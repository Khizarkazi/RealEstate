using RealEstate.Models;

namespace RealEstate.RepoDAL
{
    public interface IPropertyRepo
    {
        Task<PropertyReportView> GetPropertyReportAsync();
    }
}
