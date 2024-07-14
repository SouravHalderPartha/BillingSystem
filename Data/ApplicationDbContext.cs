using BillingSystem.Models;
using BillingSystem.Models.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BillingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof (EntityTypeConfiguration).Assembly);
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Client>().ToTable("Client");
        }

        public DbSet<BillingInvoice> BillingInvoices { get; set; }

        public DbSet<OpeningInvoice> OpeningInvoices { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Client> Clients { get; set; }
    }
}