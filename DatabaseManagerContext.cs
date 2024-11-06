using Microsoft.EntityFrameworkCore;

namespace EmployeeApp
{
    public class DatabaseManagerContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; } = null!;

        public DatabaseManagerContext()
        {
           //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Employees;Username=postgres;Password=20189311");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                        .HasIndex(employee => employee.Gender)
                        .HasDatabaseName("idx_gender");
        }
    }
}
