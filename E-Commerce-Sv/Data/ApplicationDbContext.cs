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
    }
}
