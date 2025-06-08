using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.RepoDAL;

namespace RealEstate.Services
{
    public class InquiryService: IInquiryRepo
    {
        public RealEstateContext _context;
        public InquiryService(RealEstateContext _context)
        {
            this._context = _context;
        }

        public void AddInquiry(Inquiry inquiry)
        {
            var data=_context.Inquiries.Add(inquiry);
            _context.SaveChanges();
        }

        public IEnumerable<Inquiry> GetAll()
        {
            var data=_context.Inquiries.Include(i => i.User).OrderByDescending(i => i.CreatedAt);
            return data;
        }

        public Inquiry GetById(int id)
        {
           var data=_context.Inquiries.Include(i=>i.User).FirstOrDefault(i=>i.InquiryId== id);
           return data;
        }

        public IEnumerable<Inquiry> GetByUserId(int userId)
        {
            var data=_context.Inquiries.Where(i=>i.UserId==userId).Include(i=>i.User).OrderByDescending(i=>i.CreatedAt);
            return data;
        }

        public void Update(Inquiry inquiry)
        {
            var data=_context.Inquiries.Update(inquiry);
            _context.SaveChanges();
        }
    }
}
