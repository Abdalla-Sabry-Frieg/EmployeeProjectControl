using EmployeeProject_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProject_MVC.Data
{
	public class ApplicationDbContext : DbContext 
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
			:base(options)
        {
        }

        public DbSet<Department> Departments  { get; set; }
        public DbSet<Employee> Employees  { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			

			// Add or Change Schema 
		//	modelBuilder.Entity<Employee>().ToTable("Employees", "HR");
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			//optionsBuilder.UseSqlServer($"Server=DESKTOP-0IAV4DA; Database=CRUDOperationDemo ; Trusted_Connection=true");
		}
	}
}
