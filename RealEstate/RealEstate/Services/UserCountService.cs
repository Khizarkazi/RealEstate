using RealEstate.Models;
using RealEstate.RepoDAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstate.Data;
using RealEstate.RepoDAL;

namespace RealEstate.Services
{
    public class UserCountService : IUserCountRepo
    {
        private readonly RealEstateContext _context;

        public UserCountService(RealEstateContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<UserRole, int>> GetUserCountsByRoleAsync()
        {
            return await _context.Users
                .GroupBy(u => u.Role)
                .Select(g => new { Role = g.Key, Count = g.Count() })
                .ToDictionaryAsync(
                    x => Enum.Parse<UserRole>(x.Role), // Convert string to UserRole enum
                    x => x.Count
                );
        }

    }
}




