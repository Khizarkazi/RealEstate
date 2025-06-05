using Microsoft.EntityFrameworkCore;
using RealEstate.Migrations;
using RealEstate.Models;

namespace RealEstate.Data
{
    public class RealEstateContext:DbContext
    {

        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<LeaseAgreement> LeaseAgreements { get; set; }
        public DbSet<Models.Appointment> Appointments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Samplesales> Samplesales { get; set; }

    }
}
