using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.RepoDAL;

namespace RealEstate.Controllers
{
    public class UserCountController : Controller
    {
        private readonly IUserCountRepo _userRepository;

        public UserCountController(IUserCountRepo userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> UserRoleSummary()
        {
            var counts = await _userRepository.GetUserCountsByRoleAsync();
            var viewModel = new UserRoleCountViewModel
            {
                RoleCounts = counts
            };

            return View(viewModel);
        }
    }
}
