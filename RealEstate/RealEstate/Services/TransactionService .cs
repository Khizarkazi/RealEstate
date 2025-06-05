using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.RepoDAL;

namespace RealEstate.Services
{
    public class TransactionService: ITransactionRepo
    {
        private readonly RealEstateContext _context;

        public TransactionService(RealEstateContext context)
        {
            _context = context;
        }

        public IEnumerable<Transaction> GetTransactionsByUserId(int userId)
        {
            return _context.Transactions
                  .Include(t => t.Booking)
                  .Where(t => t.Booking.UserId == userId)
                  .OrderByDescending(t => t.TransactionDate)
                  .ToList();
        }

       
    }
}
