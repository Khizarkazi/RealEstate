using RealEstate.Models;

namespace RealEstate.RepoDAL
{
    public interface IInquiryRepo
    {
        void AddInquiry(Inquiry inquiry);
        IEnumerable<Inquiry> GetAll();
        IEnumerable<Inquiry> GetByUserId(int userId);
        Inquiry GetById(int id);
        void Update(Inquiry inquiry);
    }
}
