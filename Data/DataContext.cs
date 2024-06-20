using AtmEquityProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AtmEquityProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {   
        }
        public DbSet<Atm> Atms { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "System",
                    LastName = "",
                    Username = "System",
                    Password = "System",
                }
            );
        }

    }
}
