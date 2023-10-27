using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleCRUD.Web.Models;
using SimpleCRUD.Web.Models.ViewModels;
namespace SimpleCRUD.Web.DataAccess
{
    public class PeopleContext : IdentityDbContext<ApplicationUser>
    {
        public PeopleContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EmailAddresses> EmailAddresses { get; set; }

        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // You can add additional configurations here if needed.
        }
    }
}
