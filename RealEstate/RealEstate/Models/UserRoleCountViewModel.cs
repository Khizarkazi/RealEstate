using System.Collections.Generic;
namespace RealEstate.Models
{
    public class UserRoleCountViewModel
    {
        public Dictionary<UserRole, int> RoleCounts { get; set; } = new();
    }
    public enum UserRole
    {
        Admin,
        //Owner,
        Agent,
        Buyer,
        Seller,
        Tenant
    }
}

