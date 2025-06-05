using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.RepoDAL;

namespace RealEstate.Services
{
    public class LeaseAgreementServices: ILeaseAgreementRepo
    {
        RealEstateContext _context;
        public LeaseAgreementServices(RealEstateContext context)
        {
            _context = context;
        }
        public void AddLease(LeaseAgreement lease)
        {
            var data = _context.LeaseAgreements.Add(lease);
            _context.SaveChanges();
            return;
        }

        public IEnumerable<LeaseAgreement> GetAll()
        {
            return _context.LeaseAgreements.Include(l => l.Property).Include(l => l.Tenant).ToList();

        }
        public IEnumerable<LeaseAgreement> GetByTenantId(int tenantId)
        {
            return _context.LeaseAgreements.Include(l => l.Property).Where(l => l.TenantId == tenantId).ToList();
        }


        public LeaseAgreement GetById(int id)
        {
            return _context.LeaseAgreements.Include(l => l.Property).Include(l => l.Tenant).FirstOrDefault(l => l.LeaseId == id);
        }
        public void Update(LeaseAgreement lease)
        {
            _context.LeaseAgreements.Update(lease);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var lease = _context.LeaseAgreements.Find(id);
            if (lease != null) _context.LeaseAgreements.Remove(lease);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public IEnumerable<LeaseAgreement> GetDueThisMonth(int tenantId)
        {
            var today = DateTime.Today;

            return _context.LeaseAgreements
                .Include(l => l.Property)
                .Where(l => l.TenantId == tenantId &&
                            l.LeaseEndDate >= today &&
                            l.LeaseStartDate <= today &&
                            l.LeaseStartDate.Day == today.Day) // Optional: due day
                .ToList();
        }

    }
}
