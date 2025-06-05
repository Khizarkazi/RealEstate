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
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Samplesales> Samplesales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
           .Property(u => u.Role)
           .HasConversion<string>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Property>()
                .Property(p => p.PropertyType)
                .HasConversion<string>();

            //modelBuilder.Entity<Property>()
            //    .Property(p => p.ListingType)
            //    .HasConversion<string>();

            modelBuilder.Entity<Property>()
                .Property(p => p.Status)
                .HasConversion<string>();

            //modelBuilder.Entity<Property>()
            //    .Property(p => p.OwnerRole)
            //    .HasConversion<string>();

            //modelBuilder.Entity<Booking>()
            //    .Property(b => b.Status)
            //    .HasConversion<string>();

            //modelBuilder.Entity<LeaseAgreement>()
            //    .Property(l => l.LeaseStatus)
            //    .HasConversion<string>();

            //modelBuilder.Entity<Transaction>()
            //    .Property(t => t.PaymentStatus)
            //    .HasConversion<string>();

            //modelBuilder.Entity<Transaction>()
            //    .Property(t => t.TransactionType)
            //    .HasConversion<string>();

            //modelBuilder.Entity<Appointment>()
            //    .Property(a => a.Status)
            //    .HasConversion<string>();

            //modelBuilder.Entity<Rent>()
            //    .Property(r => r.RentStatus)
            //    .HasConversion<string>();

            //modelBuilder.Entity<Report>()
            //    .Property(r => r.ReportCategory)
            //    .HasConversion<string>();


            // Avoid multiple cascade paths by restricting some deletes

            // Booking: cascade delete on Property, restrict on User
            //modelBuilder.Entity<Booking>()
            //    .HasOne(b => b.Property)
            //    .WithMany(p => p.Bookings)
            //    .HasForeignKey(b => b.PropertyId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Booking>()
            //    .HasOne(b => b.User)
            //    .WithMany(u => u.Bookings)
            //    .HasForeignKey(b => b.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// LeaseAgreement: cascade on Property, restrict on Tenant
            //modelBuilder.Entity<LeaseAgreement>()
            //    .HasOne(l => l.Property)
            //    .WithMany(p => p.LeaseAgreements)
            //    .HasForeignKey(l => l.PropertyId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<LeaseAgreement>()
            //    .HasOne(l => l.Tenant)
            //    .WithMany(u => u.LeaseAgreements)
            //    .HasForeignKey(l => l.TenantId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// Review: cascade on Property, restrict on User
            //modelBuilder.Entity<Review>()
            //    .HasOne(r => r.Property)
            //    .WithMany(p => p.Reviews)
            //    .HasForeignKey(r => r.PropertyId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Review>()
            //    .HasOne(r => r.User)
            //    .WithMany(u => u.Reviews)
            //    .HasForeignKey(r => r.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// Appointment: cascade on Property, restrict on User
            //modelBuilder.Entity<Appointment>()
            //    .HasOne(a => a.Property)
            //    .WithMany(p => p.Appointments)
            //    .HasForeignKey(a => a.PropertyId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Appointment>()
            //    .HasOne(a => a.User)
            //    .WithMany(u => u.Appointments)
            //    .HasForeignKey(a => a.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// Transaction: restrict on both to avoid cascade cycles
            //modelBuilder.Entity<Transaction>()
            //    .HasOne(t => t.User)
            //    .WithMany(u => u.Transactions)
            //    .HasForeignKey(t => t.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Transaction>()
            //    .HasOne(t => t.Property)
            //    .WithMany(p => p.Transactions)
            //    .HasForeignKey(t => t.PropertyId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //Property - Owner relation: restrict to avoid cascade loops
            //modelBuilder.Entity<Property>()
            //    .HasOne(p => p.Owner)
            //    .WithMany(u => u.Properties)
            //    .HasForeignKey(p => p.OwnerId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }

    }
}
