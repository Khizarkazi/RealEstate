using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Data;

namespace RealEstate.Controllers
{
    public class SalesController : Controller
    {
        private readonly RealEstateContext _context;

        public SalesController(RealEstateContext context)
        {
            _context = context;
        }

<<<<<<< Updated upstream
=======
        //public IActionResult SalesChart()
        //{
        //    var salesData = _context.Sales
        //        .Where(s => s.Status == "Completed")
        //        .AsEnumerable()
        //        .GroupBy(s => new { s.SaleDate.Year, s.SaleDate.Month })
        //        .Select(g => new
        //        {
        //            Month = $"{g.Key.Year}-{g.Key.Month:D2}",
        //            TotalSales = g.Sum(s => s.SaleAmount)
        //        })
        //        .OrderBy(g => g.Month)
        //        .ToList();

        //    ViewBag.SalesData = salesData;

        //    return View();
        //}

>>>>>>> Stashed changes
        public IActionResult SalesChart(string filter = "monthly")
        {
            // Prepare filter dropdown
            ViewBag.FilterOptions = new SelectList(new[]
            {
            new { Value = "monthly", Text = "Monthly" },
            new { Value = "yearly", Text = "Yearly" }
        }, "Value", "Text", filter);

            // Get completed sales
            var sales = _context.Sales
                .Where(s => s.Status == "Completed")
                .AsEnumerable(); // switch to client-side for DateTime.ToString

            // Group by month or year
            var groupedSales = (filter == "yearly")
                ? sales.GroupBy(s => s.SaleDate.Year.ToString())
                       .Select(g => new { Period = g.Key, TotalSales = g.Sum(s => s.SaleAmount) })
                       .OrderBy(x => x.Period)
                : sales.GroupBy(s => s.SaleDate.ToString("yyyy-MM"))
                       .Select(g => new { Period = g.Key, TotalSales = g.Sum(s => s.SaleAmount) })
                       .OrderBy(x => x.Period);

            // Send to view
            ViewBag.Filter = filter;
            ViewBag.SalesData = groupedSales.ToList();

            return View();
        }




    }

}
