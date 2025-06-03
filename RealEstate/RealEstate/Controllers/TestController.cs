using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    public class TestController : Controller
    {
        private readonly RealEstateContext _context;

        public TestController(RealEstateContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetChartData(string filter)
        {
            var transactions = _context.Transactions.AsQueryable();

            var now = DateTime.Now;
            List<string> labels = new();
            List<decimal> amounts = new();

            switch (filter)
            {
                case "Daily":
                    var today = now.Date;
                    var dailyData = transactions
                        .Where(t => t.TransactionDate.Date == today)
                        .GroupBy(t => t.TransactionDate.ToString("HH:mm"))
                        .Select(g => new { Label = g.Key, Amount = g.Sum(t => t.Amount) })
                        .ToList();
                    labels = dailyData.Select(d => d.Label).ToList();
                    amounts = dailyData.Select(d => d.Amount).ToList();
                    break;

                case "Weekly":
                    var startOfWeek = now.Date.AddDays(-(int)now.DayOfWeek);
                    var weeklyData = transactions
                        .Where(t => t.TransactionDate >= startOfWeek)
                        .GroupBy(t => t.TransactionDate.DayOfWeek.ToString())
                        .Select(g => new { Label = g.Key, Amount = g.Sum(t => t.Amount) })
                        .ToList();
                    labels = weeklyData.Select(d => d.Label).ToList();
                    amounts = weeklyData.Select(d => d.Amount).ToList();
                    break;

                case "Monthly":
                    var currentMonth = now.Month;
                    var monthlyData = transactions
                        .Where(t => t.TransactionDate.Month == currentMonth)
                        .GroupBy(t => t.TransactionDate.Day.ToString())
                        .Select(g => new { Label = g.Key, Amount = g.Sum(t => t.Amount) })
                        .ToList();
                    labels = monthlyData.Select(d => d.Label).ToList();
                    amounts = monthlyData.Select(d => d.Amount).ToList();
                    break;

                case "Yearly":
                    var currentYear = now.Year;
                    var yearlyData = transactions
                        .Where(t => t.TransactionDate.Year == currentYear)
                        .GroupBy(t => t.TransactionDate.ToString("MMMM"))
                        .Select(g => new { Label = g.Key, Amount = g.Sum(t => t.Amount) })
                        .ToList();
                    labels = yearlyData.Select(d => d.Label).ToList();
                    amounts = yearlyData.Select(d => d.Amount).ToList();
                    break;
            }

            return Json(new { labels, amounts });
        }


    }
}
