using E_Commerce_Sv.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Sv.Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>().HasData(
				new Product { Category = "Desktop", Id = 1, CategoryId = 1, Description = "graficka komponenta", Name = "AMD Radeon RX 580", Price = 150 },
				new Product { Category = "Desktop", Id = 2, CategoryId = 1, Description = "graficka komponenta", Name = "NVIDIA GEFORCE GTX", Price = 250 });
		}
	}
}
