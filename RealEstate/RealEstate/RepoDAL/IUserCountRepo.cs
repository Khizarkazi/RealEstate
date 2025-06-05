using RealEstate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.RepoDAL
{
    public interface IUserCountRepo
    {
        Task<Dictionary<UserRole, int>> GetUserCountsByRoleAsync();
    }
}
