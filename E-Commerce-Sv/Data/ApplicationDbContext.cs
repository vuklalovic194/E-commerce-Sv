using E_Commerce_Sv.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Sv.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<OrderHeader> OrderHeaders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Wishlist> Wishlists { get; set; }
		public DbSet<Comment> Comments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
			base.OnModelCreating(modelBuilder);

			//modelBuilder.Entity<Product>().HasData(
				//new Product { CategoryId = 1 , Id = 1, Description = "graficka komponenta", Name = "AMD Radeon RX 580", Price = 150 },
				//new Product { CategoryId = 1 , Id = 2, Description = "graficka komponenta", Name = "NVIDIA GEFORCE GTX", Price = 250 });

			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 4, Name = "GRAPHIC CARDS", Description = "Graphic Cards that will suit ur needs in every aspect!" },
				new Category { Id = 5, Name = "GAMING DESCTOPS", Description = "Best of gaming desktops on market" },
				new Category { Id = 6, Name = "LAPTOPS", Description = "The fastest laptops on market" });
		}
	}
}
