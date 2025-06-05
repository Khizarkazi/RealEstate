using RealEstate.Models;

namespace RealEstate.RepoDAL
{
    public interface ILeaseAgreementRepo
    {
        IEnumerable<LeaseAgreement> GetAll();
        void AddLease(LeaseAgreement lease);
        IEnumerable<LeaseAgreement> GetByTenantId(int tenantId);
        IEnumerable<LeaseAgreement> GetDueThisMonth(int tenantId);
        LeaseAgreement GetById(int id);
        void Update(LeaseAgreement lease);
        void Delete(int id);
        void Save();
    }
}
