using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Models;
using RealEstate.Data;

namespace RealEstate.Controllers
{
    public class ReportsController : Controller
    {
        private readonly RealEstateContext _context;

        public ReportsController(RealEstateContext context)
        {
            _context = context;
        }

        // Sales Report: Daily, Weekly, Monthly, Yearly
        public async Task<IActionResult> SalesReport(string period = "Monthly")
        {
            var today = DateTime.Today;
            List<string> labels = new List<string>();
            List<decimal> data = new List<decimal>();

            if (period == "Weekly")
            {
                var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
                for (int i = 0; i < 7; i++)
                {
                    var date = startOfWeek.AddDays(i);
                    var total = await _context.Transactions
                        .Where(t => t.TransactionDate.Date == date)
                        .SumAsync(t => (decimal?)t.Amount) ?? 0;

                    labels.Add(date.ToString("ddd")); // e.g., Mon, Tue
                    data.Add(total);
                }
            }
            else if (period == "Monthly")
            {
                var currentYear = today.Year;
                for (int i = 1; i <= 12; i++)
                {
                    var total = await _context.Transactions
                        .Where(t => t.TransactionDate.Year == currentYear && t.TransactionDate.Month == i)
                        .SumAsync(t => (decimal?)t.Amount) ?? 0;

                    labels.Add(new DateTime(currentYear, i, 1).ToString("MMM"));
                    data.Add(total);
                }
            }
            else if (period == "Yearly")
            {
                var startYear = today.Year - 4;
                for (int i = startYear; i <= today.Year; i++)
                {
                    var total = await _context.Transactions
                        .Where(t => t.TransactionDate.Year == i)
                        .SumAsync(t => (decimal?)t.Amount) ?? 0;

                    labels.Add(i.ToString());
                    data.Add(total);
                }
            }

            ViewBag.Labels = labels;
            ViewBag.Data = data;
            ViewBag.Period = period;

            return View();
        }

    }
}
