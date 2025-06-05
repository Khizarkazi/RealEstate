using RealEstate.RepoDAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstate.Data;
using RealEstate.Models;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Services
{
    public class PropertyReportService : IPropertyRepo
    {
        private readonly RealEstateContext _context;

        public PropertyReportService(RealEstateContext context)
        {
            _context = context;
        }

        public async Task<PropertyReportView> GetPropertyReportAsync()
        {
            var properties = await _context.Properties.ToListAsync();

            var statusCounts = properties
                .GroupBy(p => p.Status)
                .ToDictionary(g => g.Key.ToString(), g => g.Count()); // Enum to string


            var cityCounts = properties
                .GroupBy(p => p.City)
                .ToDictionary(g => g.Key ?? "Unknown", g => g.Count());




            return new PropertyReportView
            {
                StatusCounts = statusCounts,
                CityCounts = cityCounts,

            };
        }
    }
}


