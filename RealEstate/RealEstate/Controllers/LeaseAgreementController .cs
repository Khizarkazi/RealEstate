using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.RepoDAL;

namespace RealEstate.Controllers
{
    public class LeaseAgreementController : Controller
    {
        ILeaseAgreementRepo _leaseAgreement;
        RealEstateContext _context;
        public LeaseAgreementController(ILeaseAgreementRepo leaseAgreement, RealEstateContext context)
        {
            _leaseAgreement = leaseAgreement;
            _context = context;
        }
        public IActionResult Index()
        {
            var leases = _leaseAgreement.GetAll();
            return View(leases);

        }

        public IActionResult AddLease()
        {
            ViewBag.Properties = new SelectList(_context.Properties, "PropertyId", "Title");
            ViewBag.Tenants = new SelectList(_context.Users.Where(u => u.Role == "Tenant"), "UserId", "Email");
            return View();
        }
        [HttpPost]
        public IActionResult AddLease(LeaseAgreement leaseAgreement)
        {
            _leaseAgreement.AddLease(leaseAgreement);
            return RedirectToAction("Index");


            //ViewBag.Properties = new SelectList(_context.Properties, "PropertyId", "Title");
            //ViewBag.Tenants = new SelectList(_context.Users.Where(u => u.Role == "Tenant"), "UserId", "Email");
            //return RedirectToAction("Index");
        }
        public IActionResult MyLeases()
        {
            var userIdStr = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
            {
                return RedirectToAction("MyLeases");
            }

            var leases = _leaseAgreement.GetByTenantId(userId);
            return View("MyLeases", leases);
        }
        public IActionResult Edit(int id)
        {
            var lease = _leaseAgreement.GetById(id);
            if (lease == null) return NotFound();

            PopulateDropdowns();
            return View("Edit", lease);
        }

        [HttpPost]
        public IActionResult Edit(LeaseAgreement leaseAgreement)
        {
            PopulateDropdowns();
            _leaseAgreement.Update(leaseAgreement);
            return RedirectToAction("Index");




        }

        public IActionResult Delete(int id)
        {
            _leaseAgreement.Delete(id);
            _leaseAgreement.Save();
            return RedirectToAction("Index");
        }

        private void PopulateDropdowns()
        {
            ViewBag.Properties = new SelectList(_context.Properties, "PropertyId", "Title");
            ViewBag.Tenants = new SelectList(_context.Users.Where(u => u.Role == "Tenant"), "UserId", "Email");
        }
        public IActionResult RentAlerts()
        {
            var userIdStr = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int tenantId))
            {
                TempData["Error"] = "Please login first.";
                return RedirectToAction("Login", "Account");
            }

            var dueLeases = _leaseAgreement.GetDueThisMonth(tenantId);
            return View(dueLeases);
        }
    }
}
