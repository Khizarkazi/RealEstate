using RealEstate.Models;

namespace RealEstate.RepoDAL
{
    public interface ITransactionRepo
    {
        IEnumerable<Transaction> GetTransactionsByUserId(int userId); // Based on Booking's UserId
    }
}
